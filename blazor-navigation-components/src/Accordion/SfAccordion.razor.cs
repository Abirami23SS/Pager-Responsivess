using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Internal;
using System.Linq;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Accordion is a vertically collapsible panel that displays one or more panels at a time.
    /// </summary>
    /// <remarks>
    /// Accordion items can be populated by specifying <see cref="AccordionItem"/> within <see cref="AccordionItems"/> tag directive.
    /// </remarks>
    /// <example>
    /// In the below code example, a basic accordion component initialized with <see cref="AccordionItems"/> tag directive.
    /// <code><![CDATA[
    /// <SfAccordion>
    ///     <AccordionItems>
    ///         <AccordionItem Header="ASP.NET">
    ///             <ContentTemplate>
    ///                 Microsoft ASP.NET is a set of technologies in the Microsoft .NET Framework for building Web applications and XML Web services.
    ///             </ContentTemplate>
    ///         </AccordionItem>
    ///         <AccordionItem Header="ASP.NET MVC">
    ///             <ContentTemplate>
    ///                 The Model-View-Controller (MVC) architectural pattern separates an application into three main components: the model, the view, and the controller.
    ///             </ContentTemplate>
    ///         </AccordionItem>
    ///         <AccordionItem Header="JavaScript">
    ///             <ContentTemplate>
    ///                 JavaScript (JS) is an interpreted computer programming language. It was originally implemented as part of web browsers so that client-side scripts could interact with the user, control the browser, communicate asynchronously, and alter the document content that was displayed.
    ///             </ContentTemplate>
    ///         </AccordionItem>
    ///     </AccordionItems>
    /// </SfAccordion>
    /// ]]></code>
    /// </example>
    public partial class SfAccordion : SfBaseComponent
    {
        private const string SPACE = " ";
        private const string RTL = "e-rtl";
        private const string ACCORDIONPREFIX = "accordion-";
        private const string ANIMATION = "animation";
        private const string ENABLE_PERSISTENCE = "enablePersistence";
        private const string EXPAND_MODE = "expandMode";
        private const string EXPANDED_INDICES = "expandedIndices";
        private const string CLASS = "class";
        private const string ACCORDION_CLICKED = "clicked";
        private const string ACCORDION_EXPANDING = "expanding";
        private const string ACCORDION_EXPANDED = "expanded";
        private const string ACCORDION_COLLAPSING = "collapsing";
        private const string ACCORDION_COLLAPSED = "collapsed";
        private const string CREATED_ENABLED = "createdEnabled";
        private const string STYLE = "data-sf-style";
        private Dictionary<string, object> rootAttributes = new Dictionary<string, object>();
        private bool shouldRender = true;

        internal List<AccordionItem>? ExpandedItem { get; set; }

        internal AccordionEvents? Delegates { get; set; }

        internal bool IsItemChanged { get; set; }

        internal string dataId = "sfAccordion-" + Guid.NewGuid().ToString();

        private string AccordionClass { get; set; } = "e-control e-accordion";

        private static AccordionItemModel GetItem(AccordionItem accordionItem)
        {
            AccordionItemModel item = new AccordionItemModel();
            if (accordionItem != null)
            {
                item.Id = accordionItem.Id;
                item.Content = accordionItem.Content;
                item.CssClass = accordionItem.CssClass;
                item.Disabled = accordionItem.Disabled;
                item.Expanded = accordionItem.Expanded;
                item.Header = accordionItem.Header;
                item.IconCss = accordionItem.IconCss;
                item.Visible = accordionItem.Visible;
            }

            return item;
        }

        private Dictionary<string, object> GetInstance()
        {
            Dictionary<string, object> accordionObj = new Dictionary<string, object>();
            accordionObj.Add(ANIMATION, AnimationSettings);
            accordionObj.Add(ENABLE_PERSISTENCE, EnablePersistence);
            accordionObj.Add(EXPAND_MODE, ExpandMode);
            accordionObj.Add(EXPANDED_INDICES, ExpandedIndices);
            accordionObj.Add(CREATED_ENABLED, Delegates?.Created.HasDelegate ?? false);
            return accordionObj;
        }

        private void SetItems()
        {
            int i = 0;
            ExpandedItem = new List<AccordionItem>();
            foreach (AccordionItem item in Items)
            {
                item.IsExpandedFromIndex = false;
                if (ExpandedIndices != null && ExpandedIndices.Contains(i))
                {
                    item.IsExpandedFromIndex = true;
                    item.IsContentRendered = true;
                }

                if ((item.Expanded || item.IsExpandedFromIndex) && (!string.IsNullOrEmpty(item.Content) || item.ContentTemplate != null))
                {
                    ExpandedItem.Add(item);
                }

                i++;
            }
        }

        private void UpdateLocalProperties()
        {
            ExpandedItem = new List<AccordionItem>();
            if (EnableRtl || SyncfusionService.options.EnableRtl)
            {
                AccordionClass += SPACE + RTL;
            }
            UpdateHtmlAttributes();
        }

        private void UpdateHtmlAttributes()
        {
            string style = $"{$"width: {SfBaseUtils.FormatUnit(Width)}"};{$"height: {SfBaseUtils.FormatUnit(Height)}"};";
            SfBaseUtils.UpdateDictionary(STYLE, style, rootAttributes);
            if (HtmlAttributes != null)
            {
                foreach (var item in HtmlAttributes)
                {
                    if (item.Key == CLASS)
                    {
                        AccordionClass += SPACE + item.Value;
                    }
                    else if (item.Key == STYLE   || item.Key == "style")
                    {
                        if (rootAttributes.ContainsKey(STYLE))
                        {
                            rootAttributes[STYLE] += item.Value.ToString();
                        }
                        else
                        {
                            SfBaseUtils.UpdateDictionary(STYLE, item.Value, rootAttributes);
                        }
                    }
                    else
                    {
                        SfBaseUtils.UpdateDictionary(item.Key, item.Value, rootAttributes);
                    }
                }
            }
        }

        internal async Task OnPropertyChangeHandler()
        {
            if (PropertyChanges.ContainsKey(nameof(Width)) || PropertyChanges.ContainsKey(nameof(Height)))
            {
                UpdateHtmlAttributes();
            }
            if (PropertyChanges.ContainsKey(nameof(EnableRtl)) || PropertyChanges.ContainsKey(nameof(ExpandMode)))
            {
                bool isRtlChanged = PropertyChanges.ContainsKey(nameof(EnableRtl));
                bool isExpandModeChanged = PropertyChanges.ContainsKey(nameof(ExpandMode));
                await InvokeMethod("sfBlazor.Accordion.setExpandModeAndRTL", new object[] { dataId, EnableRtl, ExpandMode, isRtlChanged, isExpandModeChanged}).ConfigureAwait(true);
            }

            if (PropertyChanges.ContainsKey(nameof(ExpandedIndices)) && !IsExpandIndicesChanged)
            {
                UpdateExpandedIndices();
            }

            IsExpandIndicesChanged = false;
        }

        private void UpdateExpandedIndices()
        {
            if (Items != null && Items.Count > 0)
            {
                SetItems();
            }

            StateHasChanged();
        }

        private async Task SetTaskYield()
        {
            IJSInProcessRuntime? runtime = this.JSRuntime as IJSInProcessRuntime;
            if (runtime != null)
            {
                await Task.Yield();
            }
        }

        internal async Task TriggerClickedEvent(MouseEventArgs e, AccordionItem item)
        {
            if(Delegates?.Clicked.HasDelegate == true)
            {
                AccordionClickArgs args = new AccordionClickArgs()
                {
                    Name = ACCORDION_CLICKED,
                    OriginalEvent = e,
                };
                args.Item = GetItem(item);
                await Delegates.Clicked.InvokeAsync(args).ConfigureAwait(true);
            }
        }

        internal async Task AfterContentRender(ElementReference headerElement)
        {
            await InvokeMethod("sfBlazor.Accordion.afterContentRender", new object[] { dataId, headerElement, AnimationSettings }).ConfigureAwait(true);
        }

        #region JSInterop methods

        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task CreatedEvent()
        {
            if (Delegates?.Created.HasDelegate == true)
                await Delegates.Created.InvokeAsync(null).ConfigureAwait(true);
        }

        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void OnAccordionClick(int index)
        {
            if (Items != null && Items.Count > 0 && Items[index] != null)
            {
                Items[index].IsContentRendered = true;
            }

            StateHasChanged();
        }

        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task TriggerExpandingEvent(int? targetIndex)
        {
            ExpandEventArgs args = new ExpandEventArgs()
            {
                Name = ACCORDION_EXPANDING,
                Index = targetIndex.Value,
                IsExpanded = true,
                Cancel = false
            };
            AccordionItem item = null;
            if (Items != null && Items.Count > 0 && targetIndex != null && targetIndex >= 0 && targetIndex < Items.Count)
            {
                item = Items[targetIndex.Value];
            }

            args.Item = GetItem(item);
            bool isCancelled = false;
            if(Delegates?.Expanding.HasDelegate == true)
            {
                await Delegates.Expanding.InvokeAsync(args).ConfigureAwait(true);
                isCancelled = args.Cancel;
            }
            if (!isCancelled)
            {
                await InvokeMethod("sfBlazor.Accordion.expandingItem", new object[] { dataId, args }).ConfigureAwait(true);
            }
        }

        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task TriggerExpandedEvent(ExpandEventArgs args)
        {
            if (args != null)
            {
                ExpandedEventArgs expandedArgs = new ExpandedEventArgs()
                {
                    Name = ACCORDION_EXPANDED,
                    Index = args.Index,
                    IsExpanded = args.IsExpanded,
                    Item = args.Item
                };
                await SfBaseUtils.InvokeEvent<ExpandedEventArgs>(Delegates?.Expanded, expandedArgs).ConfigureAwait(true);
                UpdateExpandedIndices(expandedArgs.Index, expandedArgs.IsExpanded);
                if (Items[args.Index].ExpandedChanged.HasDelegate)
                {
                    await InvokeMethod("sfBlazor.Accordion.itemExpandedOrCollapsed", new object[] { dataId, args }).ConfigureAwait(true);
                }
            }
        }

        private async void UpdateExpandedIndices(int index, bool isExpanded)
        {
            if (ExpandedIndices != null && !ExpandedIndices.Contains(index))
            {
                IsExpandIndicesChanged = true;
                if (Items != null && Items.Count > index)
                {
                    Items[index].IsExpandedFromIndex = true;
                }

                List<int> indices = ExpandedIndices.ToList();
                indices.Add(index);
                ExpandedIndices = indices.ToArray();
                await SetTaskYield().ConfigureAwait(true); // To resolve animation lag issue in WASM application
                ExpandedIndices = expandedIndices = await SfBaseUtils.UpdateProperty(ExpandedIndices, expandedIndices, ExpandedIndicesChanged).ConfigureAwait(true);
            }
            else
            {
                if (Items != null && Items.Count > index)
                {
                    await Items[index].UpdateExpandedValue(isExpanded).ConfigureAwait(true);
                }
            }
        }

        private async void UpdateCollapsedIndices(int index, bool isExpanded)
        {
            if (ExpandedIndices != null)
            {
                IsExpandIndicesChanged = true;
                if (Items != null && Items.Count > index)
                {
                    Items[index].IsExpandedFromIndex = false;
                }

                ExpandedIndices = ExpandedIndices.Where(val => val != index).ToArray();
                await SetTaskYield().ConfigureAwait(true); // To resolve animation lag issue in WASM application
                ExpandedIndices = expandedIndices = await SfBaseUtils.UpdateProperty(ExpandedIndices, expandedIndices, ExpandedIndicesChanged).ConfigureAwait(true);
            }
            else
            {
                if (Items != null && Items.Count > index)
                {
                    await Items[index].UpdateExpandedValue(isExpanded).ConfigureAwait(true);
                }
            }
        }

        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task TriggerCollapsingEvent(int? targetIndex)
        {
            CollapseEventArgs args = new CollapseEventArgs()
            {
                Name = ACCORDION_COLLAPSING,
                Index = targetIndex.Value,
                IsExpanded = false,
                Cancel = false
            };
            AccordionItem item = null;
            if ((Items != null && Items.Count > 0) && (targetIndex != null && targetIndex >= 0 && targetIndex < Items.Count))
            {
                item = Items[targetIndex.Value];
            }

            args.Item = GetItem(item);
            await SfBaseUtils.InvokeEvent<CollapseEventArgs>(Delegates?.Collapsing, args).ConfigureAwait(true);
            if (!args.Cancel)
            {
                await InvokeMethod("sfBlazor.Accordion.collapsingItem", new object[] { dataId, args }).ConfigureAwait(true);
            }
        }

        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task TriggerCollapsedEvent(ExpandEventArgs args)
        {
            if (args != null)
            {
                CollapsedEventArgs collapsedArgs = new CollapsedEventArgs()
                {
                    Name = ACCORDION_COLLAPSED,
                    Index = args.Index,
                    IsExpanded = args.IsExpanded,
                    Item = args.Item
                };
                await SfBaseUtils.InvokeEvent<CollapsedEventArgs>(Delegates?.Collapsed, collapsedArgs).ConfigureAwait(true);
                UpdateCollapsedIndices(collapsedArgs.Index, collapsedArgs.IsExpanded);
                if (Items[args.Index].ExpandedChanged.HasDelegate)
                {
                    await InvokeMethod("sfBlazor.Accordion.itemExpandedOrCollapsed", new object[] { dataId, args }).ConfigureAwait(true);
                }
            }
        }
        #endregion
    }
}
