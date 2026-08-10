using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Buttons;
using System.Collections.Generic;
using Syncfusion.Blazor.Internal;
using System;
using System.Globalization;
using Microsoft.AspNetCore.Components.Web;
using System.Text.Json.Serialization;

namespace Syncfusion.Blazor.Navigations.Internal
{
    /// <summary>
    /// Specifies toolbar content.
    /// </summary>
    public partial class ToolbarContent : SfOwningComponentBase
    {
        [CascadingParameter]
        private SfToolbar Parent { get; set; }

        private const string SPACE = " ";
        private const string TOOLBAR_ITEM = "e-toolbar-item";
        private const string TOOLBAR_BUTTON = "e-tbar-btn";
        private const string TEMPLATE = "e-template";
        private const string TOOLBAR_TEXT = "e-toolbar-text";
        private const string TOOLBAR_BUTTON_TEXT = "e-tbtn-txt";
        private const string TOOLBAR_BUTTON_ALIGN = "e-tbtn-align";
        private const string ICONS = "e-icons";
        private const string SEPARATOR = "e-separator";
        private const string SPACER = "e-spacer";
        private const string POPUP_TEXT = "e-popup-text";
        private const string OVERFLOW_SHOW = "e-overflow-show";
        private const string OVERFLOW_HIDE = "e-overflow-hide";
        private const string POPUP_ALONE = "e-popup-alone";
        private const string OVERLAY = "e-overlay";
        private const string HIDDEN = "e-hidden";
        private const string TYPE = "type";
        private const string BUTTON = "button";
        private const string TAB_INDEX = "tabindex";
        private const string DATA_TAB_INDEX = "data-tabindex";
        private const string STYLE = "data-sf-style";
        private const string WIDTH = "width:";
        private const string ICON_BUTTON = "e-icon-btn";
        private const string TITLE = "title";
        private const string ARIA_LABEL = "aria-label";
        private const string ICON_CLASS = "e-btn-icon";
        private const string ROOT = "e-control e-btn e-lib";
        private Dictionary<string, object> buttonAttributes = new Dictionary<string, object>();
        private Dictionary<string, object> itemAttributes = new Dictionary<string, object>();
        private ItemModel? item;

        /// <summary>
        /// Defines the toolbar item model.
        /// </summary>
        [Parameter]
        public ItemModel Item { get; set; }

        /// <summary>
        /// Defines toolbar item index.
        /// </summary>
        [Parameter]
        public int Index { get; set; }

        private string? ItemCss { get; set; }

        private string? ButtonCss { get; set; }

        private string? ButtonIconCss { get; set; }
        private string? btnClass;
        private IconPosition ButtonIconPosition { get; set; }

        /// <summary>
        /// Method invoked when the component has received parameters from its parent in the render tree,
        /// and the incoming values have been assigned to properties.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync().ConfigureAwait(true);
            if (Item != null && Item != item)
            {
                item = Item;
                ItemCss = TOOLBAR_ITEM;
                ButtonCss = TOOLBAR_BUTTON;
                if (Item.Template != null)
                {
                    ItemCss += SPACE + TEMPLATE;
                }
                else if (Item.Type == ItemType.Button)
                {
                    if (!string.IsNullOrEmpty(Item.Text))
                    {
                        ButtonCss += SPACE + TOOLBAR_BUTTON_TEXT;
                    }
                    else
                    {
                        ItemCss += SPACE + TOOLBAR_BUTTON_ALIGN;
                    }

                    if (!string.IsNullOrEmpty(Item.PrefixIcon) || !string.IsNullOrEmpty(Item.SuffixIcon))
                    {
                        if ((!string.IsNullOrEmpty(Item.PrefixIcon) && !string.IsNullOrEmpty(Item.SuffixIcon)) || !string.IsNullOrEmpty(Item.PrefixIcon))
                        {
                            ButtonIconCss = GetIconCss(Item.PrefixIcon);
                            ButtonIconPosition = IconPosition.Left;
                        }
                        else
                        {
                            ButtonIconCss = GetIconCss(Item.SuffixIcon);
                            ButtonIconPosition = IconPosition.Right;
                        }
                    }
                }
                else if (Item.Type == ItemType.Separator)
                {
                    ItemCss += SPACE + SEPARATOR;
                }
                else if (Item.Type == ItemType.Spacer)
                {
                    ItemCss += SPACE + SPACER;
                }

                SetItemCss();
                if (!string.IsNullOrEmpty(ButtonIconCss) && string.IsNullOrEmpty(Item.Text))
                {
                    ButtonCss = SfBaseUtils.AddClass(ButtonCss, ICON_BUTTON);
                }

                if (!string.IsNullOrEmpty(Item.TooltipText))
                {
                    itemAttributes.Clear();
                    itemAttributes.Add(TITLE, Item.TooltipText);
                }

                buttonAttributes.Clear();
                buttonAttributes.Add(TYPE, BUTTON);
                buttonAttributes.Add(STYLE, WIDTH + @Item.Width);
                buttonAttributes.Add(TAB_INDEX, Item.TabIndex.ToString(CultureInfo.InvariantCulture));
                buttonAttributes.Add(DATA_TAB_INDEX, Item.TabIndex.ToString(CultureInfo.InvariantCulture));

                if (!string.IsNullOrEmpty(Item.Text))
                {
                    buttonAttributes.Add(ARIA_LABEL, @Item.Text);
                }
                else if (!string.IsNullOrEmpty(Item.TooltipText))
                {
                    buttonAttributes.Add(ARIA_LABEL, @Item.TooltipText);
                }
                InitRender();
            }
        }

        private void SetItemCss()
        {
            if (Item.ShowTextOn == DisplayMode.Toolbar)
            {
                ItemCss += SPACE + TOOLBAR_TEXT + SPACE + TOOLBAR_BUTTON_ALIGN;
            }
            else if (Item.ShowTextOn == DisplayMode.Overflow)
            {
                ItemCss += SPACE + POPUP_TEXT;
            }

            if (Item.Overflow == OverflowOption.Show)
            {
                ItemCss += SPACE + OVERFLOW_SHOW;
            }
            else if (Item.Overflow == OverflowOption.Hide && Item.Type != ItemType.Separator)
            {
                ItemCss += SPACE + OVERFLOW_HIDE;
            }

            if (Item.Overflow != OverflowOption.Show && Item.ShowAlwaysInPopup && Item.Type != ItemType.Separator)
            {
                ItemCss += SPACE + POPUP_ALONE;
            }

            if (Item.Disabled)
            {
                ItemCss += SPACE + OVERLAY;
            }

            if (!Item.Visible)
            {
                ItemCss += SPACE + HIDDEN;
            }

            if (Item.CssClass != null)
            {
                ItemCss = ItemCss + SPACE + Item.CssClass;
            }
        }

        private void InitRender()
        {
            btnClass = ROOT;
            if (!string.IsNullOrEmpty(ButtonCss))
            {
                btnClass += SPACE + ButtonCss;
            }

            if (!string.IsNullOrEmpty(ButtonIconCss))
            {
                ButtonIconCss += SPACE + ICON_CLASS;
                if (string.IsNullOrEmpty(Item.Text))
                {
                    btnClass += SPACE + ICON_BUTTON;
                }
                else
                {
                    ButtonIconCss += " e-icon-" + ButtonIconPosition.ToString().ToLower(CultureInfo.CurrentCulture);
                    if (ButtonIconPosition == IconPosition.Top || ButtonIconPosition == IconPosition.Bottom)
                    {
                        btnClass += SPACE + "e-" + ButtonIconPosition.ToString().ToLower(CultureInfo.CurrentCulture) + "-icon-btn";
                    }
                }
            }

        }

        private async Task OnItemClick(MouseEventArgs e)
        {
            if (item != null && !item.Disabled)
            {
                await Parent.TriggerClickEvent(e, Index, Item).ConfigureAwait(true);
            }
        }

        /// <summary>
        /// Dispose unmanaged resources in the Syncfusion Blazor toolbar component.
        /// </summary>
        /// <param name="disposing">Boolean value to dispose the object.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                buttonAttributes = null;
                itemAttributes = null;
            }
        }

        private static string GetIconCss(string className)
        {
            string[] classElement = className.Split(" ");
            for (int i = 0; i < classElement.Length; i++)
            {
                if (classElement[i].StartsWith("e-", StringComparison.Ordinal))
                {
                    return className + SPACE + ICONS;
                }
            }
            return className;
        }
    }

    /// <summary>
    /// A class that holds options to control the toolbar item clicked action.
    /// </summary>
    public class ToolbarEventArgs
    {
        /// <summary>
        /// Gets or sets the data index.
        /// </summary>
        public int? TargetParentDataIndex { get; set; }

        /// <summary>
        /// Gets or sets the toolbar item index.
        /// </summary>
        [JsonPropertyName("toolbarItemIndex")]
        public int? ToolbarItemIndex { get; set; }

        /// <summary>
        /// Gets or sets the item from popup element.
        /// </summary>
        [JsonPropertyName("isPopupElement")]
        public bool IsPopupElement { get; set; }
    }
}