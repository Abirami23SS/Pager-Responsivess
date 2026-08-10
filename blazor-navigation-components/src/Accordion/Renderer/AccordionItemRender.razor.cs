using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;
using Syncfusion.Blazor.Internal;
using System.Collections.Generic;
using System;

namespace Syncfusion.Blazor.Navigations.Internal
{
    /// <summary>
    /// Specifies the accordion item renderer.
    /// </summary>
    public partial class AccordionItemRender: SfOwningComponentBase
    {
        private const string ITEM_SELECT = "e-acrdn-item e-select";
        private const string ITEMUNDERSCO = "acrdn_item_";
        private const string HEADERUNDERSCO = "acrdn_header_";
        private const string PANELUNDERSCO = "acrdn_panel_";
        private const string SPACE = " ";
        private const string EXPAND_STATE = "e-expand-state";
        private const string ARIA_DISABLED = "aria-disabled";
        private const string FALSE = "false";
        private const string TRUE = "true";
        private const string TABINDEX = "tabindex";
        private const string ZERO = "0";
        private const string ARIA_CONTROLS = "aria-controls";
        private const string ARIA_LABELLEDBY = "aria-labelledby";
        private const string ACCORDION_PANEL = "e-acrdn-panel";
        private const string CONTENT_HIDE = "e-content-hide";
        private const string TGL_COLLAPSE_ICON = "e-tgl-collapse-icon";
        private const string ICONS = "e-icons";
        private const string ARIA_EXPANDED = "aria-expanded";
        private const string ARIA_HIDDEN = "aria-hidden";
        private const string SELECTED = "e-selected";
        private const string SELECTED_ACTIVE = "e-selected e-active";
        private const string ACTIVE = "e-active";
        private const string EXPAND_ICON = "e-expand-icon";
        private const string HIDE = "e-hide";
        private const string OVERLAY = "e-overlay";
        private const string ACCORDIONHEADER = "e-acrdn-header";
        private const string BUTTON = "button";
        private const string ACCORDIONHEADERCONTENT = "e-acrdn-header-content";
        private const string TOGGLEICON = "e-toggle-icon";
        private const string REGION = "region";
        private const string ACCORDIONCONTENT = "e-acrdn-content";
        private const string ACCORDIONHEADERICON = "e-acrdn-header-icon";

        [CascadingParameter]
        private SfAccordion Parent { get; set; }

        private string? ToggleCss { get; set; }

        private string ItemCss { get; set; } = ITEM_SELECT;

        private string? ContentCss { get; set; }

        private string? HeaderIconCss { get; set; }

        private IDictionary<string, object> ItemAttributes { get; set; } = new Dictionary<string, object>();

        private Dictionary<string, object> HeaderAttributes { get; set; } = new Dictionary<string, object>();

        private Dictionary<string, object> ContentAttributes { get; set; } = new Dictionary<string, object>();

        private string ItemId { get; set; } = SfBaseUtils.GenerateID(ITEMUNDERSCO);

        private string HeaderId { get; set; } = SfBaseUtils.GenerateID(HEADERUNDERSCO);

        private string ContentId { get; set; } = SfBaseUtils.GenerateID(PANELUNDERSCO);

        private string? CssClass { get; set; }

        private bool? Disabled { get; set; }

        private string? IconCss { get; set; }

        private bool? Visible { get; set; }

        private bool? IsExpandedFromIndex { get; set; }

        /// <summary>
        /// Gets or sets the accordion item.
        /// </summary>
        [Parameter]
        public AccordionItem Item { get; set; }

        private bool IsItemClick { get; set; }

        /// <summary>
        /// Method invoked when the component is ready to start.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        protected override async Task OnInitializedAsync()
        {
            SetInitialItem(Item);
            await base.OnInitializedAsync().ConfigureAwait(true);
        }

        /// <summary>
        /// Method invoked when the component has received parameters from its parent in the render tree,
        /// and the incoming values have been assigned to properties.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync().ConfigureAwait(true);
            SetItemAttributes(Item);
        }

        /// <summary>
        /// Method invoked after each time the component has been rendered.
        /// </summary>
        /// <param name="firstRender">Set to true for the first time component rendering; otherwise gets false.</param>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender).ConfigureAwait(true);
            if (IsItemClick)
            {
                IsItemClick = false;
                await Parent.AfterContentRender(HeaderElement).ConfigureAwait(true);
            }
        }

        private void SetInitialItem(AccordionItem item)
        {
            if (item.Expanded || Parent.ExpandedItem.Contains(item))
            {
                item.IsContentRendered = true;
            }

            if ((Parent.ExpandedItem.Count == 1) && Parent.ExpandedItem[Parent.ExpandedItem.Count - 1] == item)
            {
                ItemCss = SfBaseUtils.AddClass(ItemCss, EXPAND_STATE);
            }

            HeaderAttributes.Add(ARIA_DISABLED, FALSE);
            HeaderAttributes.Add(TABINDEX, ZERO);
            HeaderAttributes.Add(ARIA_CONTROLS, ContentId);
            ContentAttributes.Add(ARIA_LABELLEDBY, HeaderId);
            SetItemAttributes(item);
        }

        private void SetItemAttributes(AccordionItem item)
        {
            if (item.Expanded)
            {
                item.IsContentRendered = true;
            }

            if (item.Expanded != item.IsExpanded || item.IsExpandedFromIndex != IsExpandedFromIndex)
            {
                item.IsExpanded = item.Expanded;
                IsExpandedFromIndex = item.IsExpandedFromIndex;
                if (!item.Expanded && !item.IsExpandedFromIndex)
                {
                    ContentCss = ACCORDION_PANEL + SPACE + CONTENT_HIDE;
                    ToggleCss = TGL_COLLAPSE_ICON + SPACE + ICONS;
                    HeaderAttributes[ARIA_EXPANDED] = FALSE;
                    ContentAttributes[ARIA_HIDDEN] = TRUE;
                    ItemCss = SfBaseUtils.RemoveClass(ItemCss, SELECTED_ACTIVE);
                    ItemCss = SfBaseUtils.RemoveClass(ItemCss, EXPAND_STATE);
                }
                else
                {
                    if (!ItemCss.Contains(SPACE + SELECTED + SPACE + ACTIVE, StringComparison.CurrentCulture) && (!string.IsNullOrEmpty(item.Content) || item.ContentTemplate != null))
                    {
                        ItemCss = SfBaseUtils.AddClass(ItemCss, SELECTED_ACTIVE);
                        if (item.IsExpandedFromIndex)
                        {
                            ItemCss = SfBaseUtils.AddClass(ItemCss, EXPAND_STATE);
                        }
                    }

                    ContentCss = ACCORDION_PANEL;
                    ToggleCss = TGL_COLLAPSE_ICON + SPACE + ICONS + SPACE + EXPAND_ICON;
                    HeaderAttributes[ARIA_EXPANDED] = TRUE;
                    ContentAttributes[ARIA_HIDDEN] = FALSE;
                }
            }

            if (item.IconCss != IconCss)
            {
                IconCss = item.IconCss;
                HeaderIconCss = string.Empty;
                if (!string.IsNullOrEmpty(item.IconCss))
                {
                    HeaderIconCss = item.IconCss + SPACE + ICONS;
                }
            }

            if (item.Visible != Visible)
            {
                Visible = item.Visible;
                if (!item.Visible)
                {
                    ItemCss = SfBaseUtils.AddClass(ItemCss, HIDE);
                }
                else
                {
                    ItemCss = SfBaseUtils.RemoveClass(ItemCss, HIDE);
                }
            }

            if (item.Disabled != Disabled)
            {
                Disabled = item.Disabled;
                if (item.Disabled)
                {
                    ItemCss = SfBaseUtils.AddClass(ItemCss, OVERLAY);
                    HeaderAttributes[ARIA_DISABLED] = TRUE;
                    HeaderAttributes[TABINDEX] = SPACE;
                }
                else
                {
                    ItemCss = SfBaseUtils.RemoveClass(ItemCss, OVERLAY);
                    HeaderAttributes[ARIA_DISABLED] = FALSE;
                    HeaderAttributes[TABINDEX] = ZERO;
                }
            }

            if (item.CssClass != CssClass)
            {
                if (!string.IsNullOrEmpty(CssClass))
                {
                    ItemCss = SfBaseUtils.RemoveClass(ItemCss, CssClass);
                }

                CssClass = item.CssClass;
                if (!string.IsNullOrEmpty(item.CssClass))
                {
                    ItemCss = SfBaseUtils.AddClass(ItemCss, item.CssClass);
                }
            }
        }

        private async Task ItemClickHandler(MouseEventArgs e)
        {
            if (Item != null && Item.Disabled)
            {
                return;
            }
            IsItemClick = true;
            if (Item != null)
            {
                Item.IsContentRendered = true;
            }
            if (Item != null)
            {                
                await Parent.TriggerClickedEvent(e, Item).ConfigureAwait(true);
            }
        }

        /// <summary>
        /// Dispose unmanaged resources in the Syncfusion Blazor component.
        /// </summary>
        /// <param name="disposing">Boolean value to dispose the object.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                ItemAttributes = null;
                HeaderAttributes = null;
                ContentAttributes = null;
                Parent = null;
            }
        }
    }
}