using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using Syncfusion.Blazor.Internal;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// A class that represents toolbar component item of <see cref="SfToolbar"/> component.
    /// </summary>
    /// <remarks>
    /// You can render icon only, text only, icon and text toolbar item by specifying value to corresponding property.
    /// </remarks>
    /// <example>
    /// In the below code example, a basic toolbar item has been added using <see cref="ToolbarItem"/> tag directive.
    /// <code><![CDATA[
    /// <SfToolbar>
    ///     <ToolbarItems>
    ///         <ToolbarItem Text="Cut" PrefixIcon="e-icons e-cut"></ToolbarItem>
    ///         <ToolbarItem Text="Copy" PrefixIcon="e-icons e-copy"></ToolbarItem>
    ///         <ToolbarItem Text="Paste" PrefixIcon="e-icons e-paste"></ToolbarItem>
    ///     </ToolbarItems>
    /// </SfToolbar>
    /// ]]></code>
    /// </example>
    public partial class ToolbarItem : SfOwningComponentBase
    {
        private const string TOOLBARITEM = "e-toolbar-item";
        private ItemAlign align;
        private string? cssClass;
        private Dictionary<string, object>? htmlAttributes;
        private OverflowOption overflow;
        private string? prefixIcon;
        private bool showAlwaysInPopup;
        private DisplayMode showTextOn;
        private string? suffixIcon;
        private string? text;
        private string? tooltipText;
        private int tabIndex;
        private ItemType type;
        private bool visible;
        private string? width;
        private Dictionary<string, object> htmlAttributesValue;

        [CascadingParameter]
        [JsonIgnore]
        internal ToolbarItems Parent { get; set; }

        [CascadingParameter]
        [JsonIgnore]
        internal SfToolbar BaseParent { get; set; }

        internal int Index { get; set; } = -1;

        internal bool ItemFromTag { get; set; }

        private ItemModel? Item { get; set; }

        /// <summary>
        /// Gets or sets the child content for the toolbar item.
        /// </summary>
        /// <value>
        /// The value used to build the content.
        /// </value>
        [Parameter]
        [JsonIgnore]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Event triggers when click the toolbar item.
        /// </summary>
        /// <remarks>
        /// You can get the clicked toolbar item details.
        /// </remarks>
        /// <example>
        /// In the below code example, the clicked toolbar item text can be obtained from the <c>OnClick</c> event.
        /// <code><![CDATA[
        /// <SfToolbar>
        ///     <ToolbarItems>
        ///         <ToolbarItem Text="Cut" TooltipText="Cut" OnClick="ItemClick"></ToolbarItem>
        ///         <ToolbarItem Text="Copy" TooltipText="Copy"></ToolbarItem>
        ///         <ToolbarItem Text="Paste" TooltipText="Paste"></ToolbarItem>
        ///     </ToolbarItems>
        /// </SfToolbar>
        /// @code {
        ///     public void ItemClick(ClickEventArgs args)
        ///         string clickedText = args.Item.Text;
        ///     }
        /// }
        /// ]]></code>
        /// </example>
        [Parameter]
        [JsonIgnore]
        public EventCallback<ClickEventArgs> OnClick { get; set; }

        /// <summary>
        /// Gets or sets the location for aligning toolbar items on the toolbar.
        /// </summary>
        /// <value>
        /// One of the <see cref="ItemAlign"/> enumeration. The default value is <see cref="ItemAlign.Left"/>
        /// </value>
        /// <remarks>
        /// If the <c>ItemAlign</c> is <c>Left</c>, the toolbar item aligned from left side of toolbar.
        /// If the <c>ItemAlign</c> is <c>Center</c>, the toolbar item aligned from center of toolbar.
        /// If the <c>ItemAlign</c> is <c>Right</c>, the toolbar item aligned from right side of toolbar.
        /// Each item will be aligned according to the <c>Align</c> property.
        /// </remarks>
        [Parameter]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ItemAlign Align { get; set; }

        /// <summary>
        /// Gets or sets the classes for toolbar item to customize the toolbar item.
        /// </summary>
        /// <value> 
        /// If we set the css class, then the custom class is applied for toolbar item. The default value is <c>string.Empty</c>. 
        /// </value>
        /// <example> 
        /// <code><![CDATA[ 
        /// <SfToolbar>
        ///     <ToolbarItems>
        ///         <ToolbarItem Text="Cut" CssClass="item1"></ToolbarItem>
        ///     </ToolbarItems>
        /// </SfToolbar>
        /// ]]></code> 
        /// </example> 
        [Parameter]
        public string CssClass { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets whether the toolbar item is disabled or not.
        /// </summary>
        /// <value>
        /// <c>true</c>, to disable the toolbar item. The default value is <c>false</c>.
        /// </value>
        [Parameter]
        public bool Disabled { get; set; }

        /// <summary>
        /// Gets or sets a collection of additional attributes that will be applied to the toolbar item element.
        /// </summary>
        /// <value> 
        /// It allows the toolbar item to render non-declared attributes. The default value is `null`. 
        /// </value>
        /// <remarks>
        /// Additional attributes can be added by specifying as inline attributes or by specifying <c>@attributes</c> directive.
        /// </remarks>
        /// <example>
        /// In the below code example, title attribute were added for toolbar item.
        /// <code><![CDATA[
        /// <SfToolbar>
        ///     <ToolbarItems>
        ///         <ToolbarItem Text="Cut" id="item1"></ToolbarItem>
        ///     </ToolbarItems>
        /// </SfToolbar>
        /// ]]></code>
        /// </example>
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> HtmlAttributes
        {
            get => htmlAttributesValue;
            set => htmlAttributesValue = SfBaseUtils.SanitizeHtmlAttributes(value);
        }

        /// <summary>
        /// Gets or sets the unique ID for toolbar button or input element.
        /// </summary>
        /// <value>
        /// Accepts a string value. The default value is <c>String.Empty</c>.
        /// </value>
        [Parameter]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value that indicates whether to display the toolbar item on toolbar or not, when the content is too large to fit available space.
        /// </summary>
        /// <value>
        /// One of the <see cref="OverflowOption"/> enumeration. The default value is <see cref="OverflowOption.None"/>
        /// </value>
        /// <remarks>
        /// If the <c>OverflowOption</c> is <c>Show</c>, always shows the toolbar item as the primary priority on the Toolbar.
        /// If the <c>OverflowOption</c> is <c>Hide</c>, always shows the toolbar item as the secondary priority on the popup.
        /// If the <c>OverflowOption</c> is <c>None</c>, no priority were set for toolbar item, as per normal order moves to popup when content exceeds.
        /// This property is applicable only when <see cref="OverflowMode"/> is <c>Popup</c> of <c>Extended</c>.
        /// </remarks>
        [Parameter]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OverflowOption Overflow { get; set; }

        /// <summary>
        /// Gets or sets the classes to display an icon for toolbar button item.
        /// </summary>
        /// <value>
        /// Accepts a string value. The default value is <c>String.Empty</c>.
        /// </value>
        /// <remarks>
        /// The icon will be positioned before the text content if <see cref="Text"/> is available, otherwise the icon alone will be rendered.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// <SfToolbar>
        ///     <ToolbarItems>
        ///         <ToolbarItem Text="Cut" PrefixIcon="e-icons e-cut"></ToolbarItem>
        ///     </ToolbarItems>
        /// </SfToolbar>
        /// ]]></code>
        /// </example>
        [Parameter]
        public string PrefixIcon { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the toolbar items whether to display always in popup or not.
        /// </summary>
        /// <value>
        /// <c>true</c>, to render the toolbar item in popup. The default value is <c>false</c>.
        /// </value>
        /// <remarks>
        /// It allows to maintain toolbar item on popup always but it does not work for toolbar priority items.
        /// This property is applicable only when <see cref="OverflowMode"/> is <c>Popup</c> of <c>Extended</c>.
        /// </remarks>
        [Parameter]
        public bool ShowAlwaysInPopup { get; set; }

        /// <summary> 
        /// Gets or sets the tab order of the Toolbar items. When positive values assigned, it allows to switch focus to the next/previous toolbar items with Tab/ShiftTab keys.
        /// </summary> 
        /// <value> 
        /// Tab index of toolbar item. The default value is `-1`. 
        /// </value> 
        /// <remarks>
        /// By default, user can able to switch between items only via arrow keys.
        /// If the value is set to 0 for all tool bar items, then tab switches based on element order.
        /// </remarks>
        [Parameter]
        public int TabIndex { get; set; } = -1;

        /// <summary>
        /// Gets or sets a value that indicates whether to display the button text on toolbar or popup.
        /// </summary>
        /// <value>
        /// One of the <see cref="DisplayMode"/> enumeration. The default value is <see cref="DisplayMode.Both"/>
        /// </value>
        /// <remarks>
        /// If the <c>DisplayMode</c> is <c>Toolbar</c>, text will be displayed on Toolbar only.
        /// If the <c>DisplayMode</c> is <c>Overflow</c>, text will be displayed only when content overflows to popup.
        /// If the <c>DisplayMode</c> is <c>Both</c>, text will be displayed on both popup and Toolbar.
        /// </remarks>
        [Parameter]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DisplayMode ShowTextOn { get; set; }

        /// <summary>
        /// Gets or sets the classes to display an icon for toolbar button item.
        /// </summary>
        /// <value>
        /// Accepts a string value. The default value is <c>String.Empty</c>.
        /// </value>
        /// <remarks>
        /// The icon will be positioned after the text content if <see cref="Text"/> is available, otherwise the icon alone will be rendered.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// <SfToolbar>
        ///     <ToolbarItems>
        ///         <ToolbarItem Text="Cut" SuffixIcon="e-icons e-cut"></ToolbarItem>
        ///     </ToolbarItems>
        /// </SfToolbar>
        /// ]]></code>
        /// </example>
        [Parameter]
        public string SuffixIcon { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the HTML element content for the toolbar item.
        /// </summary>
        /// <value>
        /// The value used to build the content.
        /// </value>
        /// <example>
        /// <code><![CDATA[
        /// <SfToolbar>
        ///     <ToolbarItems>
        ///         <ToolbarItem>
        ///             <Template>
        ///                 <input type='checkbox' title="Accept" checked/>
        ///             </Template>
        ///         </ToolbarItem>
        ///     </ToolbarItems>
        /// </SfToolbar>
        /// ]]></code>
        /// </example>
        [Parameter]
        [JsonIgnore]
        public RenderFragment Template { get; set; }

        /// <summary>
        /// Gets or sets the text content for toolbar button item.
        /// </summary>
        /// <value>
        /// Accepts a string value. The default value is <c>String.Empty</c>.
        /// </value>
        /// <example>
        /// <code><![CDATA[
        /// <SfToolbar>
        ///     <ToolbarItems>
        ///         <ToolbarItem Text="Cut"></ToolbarItem>
        ///     </ToolbarItems>
        /// </SfToolbar>
        /// ]]></code>
        /// </example>
        [Parameter]
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the tooltip text content to be displayed on hovering the toolbar button item.
        /// </summary>
        /// <value>
        /// Accepts a string value. The default value is <c>String.Empty</c>.
        /// </value>
        /// <example>
        /// <code><![CDATA[
        /// <SfToolbar>
        ///     <ToolbarItems>
        ///         <ToolbarItem Text="Cut" TooltipText="Cut"></ToolbarItem>
        ///     </ToolbarItems>
        /// </SfToolbar>
        /// ]]></code>
        /// </example>
        [Parameter]
        public string TooltipText { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value that indicates the type of toolbar item to be rendered in toolbar.
        /// </summary>
        /// <value>
        /// One of the <see cref="ItemType"/> enumeration. The default value is <see cref="ItemType.Button"/>
        /// </value>
        /// <remarks>
        /// If the <c>ItemType</c> is <c>Button</c>, creates the Button control with its given properties like text, prefixIcon, etc.
        /// If the <c>ItemType</c> is <c>Separator</c>, adds a horizontal line that separates the toolbar item.
        /// If the <c>ItemType</c> is <c>Spacer</c>, adds a space that separates the toolbar item.
        /// If the <c>ItemType</c> is <c>Input</c>, creates an input element that is applicable to template rendering with Syncfusion controls like DropDownList, AutoComplete, etc.
        /// </remarks>
        [Parameter]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ItemType Type { get; set; }

        /// <summary>
        /// Gets or sets whether the toolbar item is hidden or not.
        /// </summary>
        /// <value>
        /// <c>false</c>, to hide the toolbar item. The default value is <c>true</c>.
        /// </value>
        [Parameter]
        public bool Visible { get; set; } = true;

        /// <summary> 
        /// Gets or sets the width of the toolbar button item in pixels/number/percentage. 
        /// </summary> 
        /// <value> 
        /// If we set the width value, then the toolbar button item will render based on specified width otherwise the default width value <c>auto</c> is set.  
        /// </value>
        [Parameter]
        public string Width { get; set; } = "auto";

        internal static ToolbarItem SetId(ToolbarItem item)
        {
            item.Id = SfBaseUtils.GenerateID(TOOLBARITEM);
            return item;
        }

        /// <summary>
        /// Method invoked when the component is ready to start.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(true);
            Index = Parent.UpdateChildProperty(this);
            UpdateIdAttribute();
            align = Align;
            cssClass = CssClass;
            htmlAttributes = HtmlAttributes;
            overflow = Overflow;
            prefixIcon = PrefixIcon;
            showAlwaysInPopup = ShowAlwaysInPopup;
            showTextOn = ShowTextOn;
            suffixIcon = SuffixIcon;
            text = Text;
            tooltipText = TooltipText;
            tabIndex = TabIndex;
            type = Type;
            visible = Visible;
            width = Width;
            BaseParent.IsItemChanged = true;
            ItemFromTag = true;
            if (string.IsNullOrEmpty(Id))
            {
                Id = SfBaseUtils.GenerateID(TOOLBARITEM);
            }
        }

        /// <summary>
        /// Method invoked when the component has received parameters from its parent in the render tree,
        /// and the incoming values have been assigned to properties.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync().ConfigureAwait(true);
            UpdateIdAttribute();
            if (Align != align || CssClass != cssClass || Overflow != overflow || PrefixIcon != prefixIcon || TabIndex != tabIndex||
                ShowAlwaysInPopup != showAlwaysInPopup || ShowTextOn != showTextOn || SuffixIcon != suffixIcon || Text != text ||
                TooltipText != tooltipText || Type != type || Visible != visible || Width != width || !SfBaseUtils.Equals(HtmlAttributes, htmlAttributes))
            {
                align = Align;
                cssClass = CssClass;
                htmlAttributes = HtmlAttributes;
                overflow = Overflow;
                prefixIcon = PrefixIcon;
                showAlwaysInPopup = ShowAlwaysInPopup;
                showTextOn = ShowTextOn;
                suffixIcon = SuffixIcon;
                text = Text;
                tooltipText = TooltipText;
                tabIndex = TabIndex;
                type = Type;
                visible = Visible;
                width = Width;
                BaseParent.IsItemChanged = true;
            }

            if (string.IsNullOrEmpty(Id))
            {
                Id = SfBaseUtils.GenerateID(TOOLBARITEM);
            }

            Item = SfToolbar.GetItem(this);
        }

        private void UpdateIdAttribute()
        {
            if (HtmlAttributes != null)
            {
                foreach (var item in HtmlAttributes)
                {
                    if (item.Key == "id")
                    {
                        Id = item.Value.ToString() ?? string.Empty;
                    }
                }
            }
        }

        internal void EnableItem(bool isDisabled)
        {
            Disabled = isDisabled;
        }

        internal void VisibleItem(bool isVisible)
        {
            Visible = isVisible;
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
                if (Parent.Items != null && Parent.Items.Contains(this))
                {
                    Parent.Items.Remove(this);
                    BaseParent.IsItemChanged = true;
                }

                Parent = null;
                BaseParent = null;
                ChildContent = null;
            }
        }
    }
}