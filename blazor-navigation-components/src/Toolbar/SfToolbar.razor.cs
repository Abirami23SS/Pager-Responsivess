using System;
using System.Collections.Generic;
using Syncfusion.Blazor.Navigations.Internal;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Web;
using Syncfusion.Blazor.Internal;
using System.ComponentModel;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Syncfusion.Blazor.Gantt, PublicKey=00240000048000009400000006020000002400005253413100040000010001002382fcb1069523ce72d849497a557a445c151eaf4007aa79adef551a8204ca7f728e5378607d85695b16f129ec35bf4af15dcf6d3581deb8bb0debb239c33e7f1271a37c7f60f1044ae417730f5082abee5f9ec568a8a4cef04074394755706376e982dc6f9d15430faaad385ae8f00a77ef1c97517f1a1517004ce78028b9ce")]
[assembly: InternalsVisibleTo("Syncfusion.Blazor.SfPdfViewer, PublicKey=00240000048000009400000006020000002400005253413100040000010001002382fcb1069523ce72d849497a557a445c151eaf4007aa79adef551a8204ca7f728e5378607d85695b16f129ec35bf4af15dcf6d3581deb8bb0debb239c33e7f1271a37c7f60f1044ae417730f5082abee5f9ec568a8a4cef04074394755706376e982dc6f9d15430faaad385ae8f00a77ef1c97517f1a1517004ce78028b9ce")]
[assembly: InternalsVisibleTo("Syncfusion.Blazor.PdfViewer, PublicKey=00240000048000009400000006020000002400005253413100040000010001002382fcb1069523ce72d849497a557a445c151eaf4007aa79adef551a8204ca7f728e5378607d85695b16f129ec35bf4af15dcf6d3581deb8bb0debb239c33e7f1271a37c7f60f1044ae417730f5082abee5f9ec568a8a4cef04074394755706376e982dc6f9d15430faaad385ae8f00a77ef1c97517f1a1517004ce78028b9ce")]
[assembly: InternalsVisibleTo("Syncfusion.Blazor.ImageEditor, PublicKey=00240000048000009400000006020000002400005253413100040000010001002382fcb1069523ce72d849497a557a445c151eaf4007aa79adef551a8204ca7f728e5378607d85695b16f129ec35bf4af15dcf6d3581deb8bb0debb239c33e7f1271a37c7f60f1044ae417730f5082abee5f9ec568a8a4cef04074394755706376e982dc6f9d15430faaad385ae8f00a77ef1c97517f1a1517004ce78028b9ce")]
namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// The Toolbar control contains a list of toolbar items that are aligned horizontally.
    /// </summary>
    /// <remarks>
    /// Toolbar items can be populated by specifying <see cref="ToolbarItem"/> within <see cref="Navigations.ToolbarItems"/> tag directive.
    /// </remarks>
    /// <example>
    /// In the below code example, a basic toolbar component initialized with <see cref="Navigations.ToolbarItems"/> tag directive.
    /// <code><![CDATA[
    /// <SfToolbar>
    ///     <ToolbarItems>
    ///         <ToolbarItem Type="ItemType.Button" Text="Cut" PrefixIcon="e-icons e-cut"></ToolbarItem>
    ///         <ToolbarItem Type="ItemType.Button" Text="Copy" PrefixIcon="e-icons e-copy"></ToolbarItem>
    ///         <ToolbarItem Type="ItemType.Button" Text="Paste" PrefixIcon="e-icons e-paste"></ToolbarItem>
    ///         <ToolbarItem Type="ItemType.Separator"></ToolbarItem>
    ///         <ToolbarItem Type="ItemType.Input">
    ///             <Template>
    ///                 <input @bind="inputValue" type="text" />
    ///             </Template>
    ///         </ToolbarItem>
    ///     </ToolbarItems>
    /// </SfToolbar>
    /// @code {
    ///     private string inputValue { get; set; }
    /// }
    /// ]]></code>
    /// </example>
    public partial class SfToolbar : SfBaseComponent
    {
        private const string SPACE = " ";
        private const string RTL = "e-rtl";
        private const string VERTICAL_CLASS = "e-vertical";
        private const string VERTICAL_LEFT_CLASS = "e-vertical-left";
        private const string ROLE = "role";
        private const string TOOLBAR = "toolbar";
        private const string ARIA_DISABLED = "aria-disabled";
        private const string ARIA_ORIENTATION = "aria-orientation";
        private const string VERTICAL = "vertical";
        private const string HORIZONTAL = "horizontal";
        private const string CSSCLASS = "CssClass";
        private const string WIDTH = "Width";
        private const string HEIGHT = "Height";
        private const string OVERFLOWMODE = "OverflowMode";
        private const string ENABLERTL = "EnableRtl";
        private const string SCROLLSTEP = "ScrollStep";
        private const string ENABLECOLLISION = "EnableCollision";
        private const string ALLOWKEYBOARD = "AllowKeyboard";
        private const string OVERFLOWMODECHANGED = "OverflowModeChanged";
        private const string TOOLBARCLICKED = "ToolbarClicked";
        private const string CSSCLASS_NAME = "cssClass";
        private const string WIDTHNAME = "width";
        private const string HEIGHTNAME = "height";
        private const string OVERFLOW = "overflowMode";
        private const string RTL_ENABLE = "enableRtl";
        private const string SCROLL = "scrollStep";
        private const string COLLISION = "enableCollision";
        private const string KEYBOARD = "allowKeyboard";
        private const string CLIENT_ITEMS = "items";
        private const string ISVERTICAl = "isVertical";
        private const string ISVERTICAL_LEFT = "isVerticalLeft";
        private const string ITEM_CLICK = "click";
        private const string TOOLBAR_CLICKED = "clicked";
        private const string ITEMS_CHANGED = "OnItemsChanged";
        private const string INITIAL_LOAD = "InitialLoad";
        private const string STYLE = "data-sf-style";
        private Dictionary<string, object> containerAttributes = new Dictionary<string, object>();
        private bool shouldRender = true;

        #region Internal variables
        internal bool PreventPropChange { get; set; }

        internal List<ItemModel> ToolbarItems { get; set; } = new List<ItemModel>();

        internal EventAggregator EventAggregator { get; set; } = new EventAggregator();

        internal ToolbarEvents? Delegates { get; set; }

        internal bool IsItemChanged { get; set; }

        internal bool IsLoaded { get; set; }

        internal bool IsVertical { get; set; }

        internal bool IsVerticalLeft { get; set; }

        internal string dataId = "sfToolbar-" + Guid.NewGuid().ToString();

        internal bool IgnoreDisabled { get; set; }

        internal bool IsDispose { get; set; }
        #endregion

        #region Private variables

        private string ToolbarClass { get; set; } = "e-control e-toolbar e-spacer-toolbar e-lib";

        private bool IsInitialModeMultiRow { get; set; }

        #endregion

        private bool SetItems()
        {
            if (Items != null)
            {
                ToolbarItems = new List<ItemModel>();
                for (var i = 0; i < Items.Count; i++)
                {
                    if (!Items[i].ItemFromTag)
                    {
                        if (string.IsNullOrEmpty(Items[i].Id))
                        {
                            Items[i] = ToolbarItem.SetId(Items[i]);
                        }

                        ItemModel item = new ItemModel()
                        {
                            Align = Items[i].Align,
                            CssClass = Items[i].CssClass,
                            HtmlAttributes = Items[i].HtmlAttributes,
                            Id = Items[i].Id,
                            Overflow = Items[i].Overflow,
                            PrefixIcon = Items[i].PrefixIcon,
                            Disabled = Items[i].Disabled,
                            ShowAlwaysInPopup = Items[i].ShowAlwaysInPopup,
                            ShowTextOn = Items[i].ShowTextOn,
                            SuffixIcon = Items[i].SuffixIcon,
                            Template = Items[i].Template,
                            Text = Items[i].Text,
                            TooltipText = Items[i].TooltipText,
                            TabIndex = Items[i].TabIndex,
                            Type = Items[i].Type,
                            Visible = Items[i].Visible,
                            Width = Items[i].Width,
                            Index = i
                        };
                        ToolbarItems.Add(item);
                    }
                }
            }

            return ToolbarItems.Count != 0;
        }

        private Dictionary<string, object> GetInstance()
        {
            Dictionary<string, object> toolbarObj = new Dictionary<string, object>();
            toolbarObj.Add(KEYBOARD, AllowKeyboard);
            toolbarObj.Add(CSSCLASS_NAME, CssClass);
            toolbarObj.Add(COLLISION, EnableCollision);
            toolbarObj.Add(RTL_ENABLE, EnableRtl || SyncfusionService.options.EnableRtl);
            toolbarObj.Add(HEIGHTNAME, Height);
            toolbarObj.Add(CLIENT_ITEMS, Items);
            toolbarObj.Add(OVERFLOW, OverflowMode);
            toolbarObj.Add(SCROLL, ScrollStep);
            toolbarObj.Add(WIDTHNAME, Width);
            toolbarObj.Add(ISVERTICAl, IsVertical);
            toolbarObj.Add(ISVERTICAL_LEFT, IsVerticalLeft);
            return toolbarObj;
        }

        private void UpdateHtmlAttributes()
        {
            if (HtmlAttributes != null)
            {
                foreach (var item in HtmlAttributes)
                {
                    if (item.Key == "class")
                    {
                        ToolbarClass += " " + item.Value;
                    }
                    else if (item.Key.Equals("style", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!containerAttributes.ContainsKey(STYLE) && !HtmlAttributes.ContainsKey(STYLE))
                        {
                            SfBaseUtils.UpdateDictionary(STYLE, item.Value, containerAttributes);
                        }
                    }
                    else
                    {
                        SfBaseUtils.UpdateDictionary(item.Key, item.Value, containerAttributes);
                    }
                }
            }
        }

        #region Internal methods
        internal static ItemModel GetItem(ToolbarItem toolbarItem)
        {
            ItemModel item = new ItemModel();
            if (toolbarItem != null)
            {
                item.Align = toolbarItem.Align;
                item.CssClass = toolbarItem.CssClass;
                item.HtmlAttributes = toolbarItem.HtmlAttributes;
                item.Id = toolbarItem.Id;
                item.Overflow = toolbarItem.Overflow;
                item.PrefixIcon = toolbarItem.PrefixIcon;
                item.Disabled = toolbarItem.Disabled;
                item.ShowAlwaysInPopup = toolbarItem.ShowAlwaysInPopup;
                item.ShowTextOn = toolbarItem.ShowTextOn;
                item.SuffixIcon = toolbarItem.SuffixIcon;
                item.Template = toolbarItem.Template;
                item.Text = toolbarItem.Text;
                item.TooltipText = toolbarItem.TooltipText;
                item.TabIndex = toolbarItem.TabIndex;
                item.Type = toolbarItem.Type;
                item.Visible = toolbarItem.Visible;
                item.Width = toolbarItem.Width;
            }

            return item;
        }

        internal void UpdateLocalProperties()
        {
            if (!string.IsNullOrEmpty(CssClass))
            {
                ToolbarClass = ToolbarClass + SPACE + CssClass;
            }

            if (EnableRtl || SyncfusionService.options.EnableRtl)
            {
                ToolbarClass += SPACE + RTL;
            }

            if (ToolbarClass.Contains(VERTICAL_CLASS, StringComparison.CurrentCulture))
            {
                IsVertical = true;
            }

            if (ToolbarClass.Contains(VERTICAL_LEFT_CLASS, StringComparison.CurrentCulture))
            {
                IsVerticalLeft = true;
            }

            containerAttributes.Add(ROLE, TOOLBAR);
            containerAttributes.Add(ARIA_DISABLED, false);
            containerAttributes.Add(ARIA_ORIENTATION, IsVertical ? VERTICAL : HORIZONTAL);
            UpdateHtmlAttributes();
        }

        internal void SetRtl(bool isEnableRtl)
        {
            EnableRtl = enableRtl = NotifyPropertyChanges(ENABLERTL, isEnableRtl, enableRtl);
            StateHasChanged();
        }

        internal async Task OnPropertyChangeHandler()
        {
            if (PropertyChanges.ContainsKey(OVERFLOWMODE))
            {
                await InvokeMethod("sfBlazor.Toolbar.setOverflowMode", dataId, OverflowMode).ConfigureAwait(true);
                EventAggregator.Notify(OVERFLOWMODECHANGED, null);
            }

            if (PropertyChanges.ContainsKey(CSSCLASS) && !PreventPropChange)
            {
                await InvokeMethod("sfBlazor.Toolbar.setCssClass", dataId, CssClass).ConfigureAwait(true);
            }

            if (PropertyChanges.ContainsKey(WIDTH) && !PreventPropChange)
            {
                await InvokeMethod("sfBlazor.Toolbar.setWidth", dataId, Width).ConfigureAwait(true);
            }

            if (PropertyChanges.ContainsKey(HEIGHT) && !PreventPropChange)
            {
                await InvokeMethod("sfBlazor.Toolbar.setHeight", dataId, Height).ConfigureAwait(true);
            }

            PreventPropChange = false;

            if (PropertyChanges.ContainsKey(ENABLERTL))
            {
                await InvokeMethod("sfBlazor.Toolbar.setEnableRTL", dataId, EnableRtl).ConfigureAwait(true);
            }

            if (PropertyChanges.ContainsKey(SCROLLSTEP))
            {
                await InvokeMethod("sfBlazor.Toolbar.setScrollStep", dataId, ScrollStep).ConfigureAwait(true);
            }

            if (PropertyChanges.ContainsKey(ENABLECOLLISION))
            {
                await InvokeMethod("sfBlazor.Toolbar.setEnableCollision", dataId, EnableCollision).ConfigureAwait(true);
            }

            if (PropertyChanges.ContainsKey(ALLOWKEYBOARD))
            {
                await InvokeMethod("sfBlazor.Toolbar.setAllowKeyboard", dataId, AllowKeyboard).ConfigureAwait(true);
            }
        }
        #endregion

        #region JSInterop methods

        /// <exclude />
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task TriggerClickEvent(MouseEventArgs e, int? trgParentDataIndex, ItemModel clickedItem)
        {
            ClickEventArgs args = new ClickEventArgs()
            {
                Name = ITEM_CLICK,
                OriginalEvent = e
            };
            ToolbarEventArgs tbarArgs = new ToolbarEventArgs()
            {
                TargetParentDataIndex = trgParentDataIndex
            };
            ToolbarItem item = null;
            if (trgParentDataIndex != null && trgParentDataIndex >= 0 && clickedItem != null)
            {
                item = Items.Find(x => x.Id == clickedItem.Id);
            }

            if (!IgnoreDisabled && item != null && item.Disabled)
            {
                return;
            }

            if (item != null)
            {
                args.Item = new ItemModel()
                {
                    Align = item.Align,
                    CssClass = item.CssClass,
                    Disabled = item.Disabled,
                    HtmlAttributes = item.HtmlAttributes,
                    Id = item.Id,
                    Overflow = item.Overflow,
                    PrefixIcon = item.PrefixIcon,
                    ShowAlwaysInPopup = item.ShowAlwaysInPopup,
                    ShowTextOn = item.ShowTextOn,
                    SuffixIcon = item.SuffixIcon,
                    Template = item.Template,
                    Text = item.Text,
                    TooltipText = item.TooltipText,
                    TabIndex = item.TabIndex,
                    Type = item.Type,
                    Visible = item.Visible,
                    Width = item.Width
                };
                if (item.OnClick.HasDelegate)
                {
                    await item.OnClick.InvokeAsync(args).ConfigureAwait(true);
                }
            }

            if (!args.Cancel)
            {
                args.Name = TOOLBAR_CLICKED;
                await SfBaseUtils.InvokeEvent<ClickEventArgs>(Delegates?.Clicked, args).ConfigureAwait(true);
                EventAggregator.Notify(TOOLBARCLICKED, tbarArgs);
                await EventAggregator.NotifyAsync(TOOLBARCLICKED, args).ConfigureAwait(true);
                if (!args.Cancel && OverflowMode == OverflowMode.Popup && args.Item != null && args.Item.Type != ItemType.Input)
                {
                    await InvokeMethod("sfBlazor.Toolbar.hidePopup", new object[] { dataId, trgParentDataIndex }).ConfigureAwait(true);
                }
            }
        }
        #endregion
    }
}
