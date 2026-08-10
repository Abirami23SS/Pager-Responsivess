using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using Syncfusion.Blazor.Internal;

namespace Syncfusion.Blazor.Navigations
{
    public partial class SfToolbar : SfBaseComponent
    {
        private bool allowKeyboard;
        private string? cssClass;
        private bool enableCollision;
        private bool enableRtl;
        private string? height;
        private OverflowMode overflowMode;
        private int scrollStep;
        private string? width;
        private Dictionary<string, object> htmlAttributesValue;

        /// <summary>
        /// Gets or sets the unique Id value for toolbar component.
        /// </summary>
        /// <value>
        /// If we set the id, then the id value set for toolbar element. The default value is `null`.
        /// </value>
        [Parameter]
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the child content of toolbar component.
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary> 
        /// Gets or sets whether the allow keyboard interaction in toolbar. 
        /// </summary> 
        /// <value>
        /// false, the keyboard interaction in toolbar, The default value is `true`.
        /// </value>
        [Parameter]
        public bool AllowKeyboard { get; set; } = true;

        /// <summary>
        /// Gets or sets the custom classes to customize the toolbar component.  
        /// </summary>
        /// <value>
        /// If we set the css class, then the custom class is applied for toolbar element. The default value is `null`. 
        /// </value> 
        /// <example>
        /// <code><![CDATA[ 
        /// <SfToolbar CssClass="custom-toolbar"></SfToolbar> 
        /// ]]></code>
        /// </example>
        [Parameter]
        public string CssClass { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets whether enable or disable popup collision to display the popup based on viewport window. 
        /// </summary>
        /// <value>
        /// false, the popup collision is disabled. The default value is `true`. 
        /// </value>
        /// <remarks>
        /// This property is applicable only when <see cref="OverflowMode.Popup"/> or <see cref="OverflowMode.Extended"/> is used.
        /// </remarks>
        [Parameter]
        public bool EnableCollision { get; set; } = true;

        /// <summary>
        /// Gets or sets whether the right to left direction is enabled for toolbar component.
        /// </summary>
        /// <value> 
        /// true, the right to left direction is enabled for toolbar component. The default value is `false`. 
        /// </value> 
        [Parameter]
        public bool EnableRtl { get; set; }

        /// <summary> 
        /// Gets or sets the height of the toolbar element in pixels/number/percentage. 
        /// </summary> 
        /// <value> 
        /// If we set the height value, then the toolbar will render based on specified height otherwise the default height value `auto` is set.  
        /// </value> 
        /// <example> 
        /// <code><![CDATA[ 
        /// <SfToolbar Height="500px"></SfToolbar> 
        /// ]]></code> 
        /// </example> 
        /// <remarks>
        /// If we set number values, then it is considered as pixels.
        /// </remarks>
        [Parameter]
        public string Height { get; set; } = "auto";

        /// <summary>
        /// Gets or sets the list of toolbar items that will be populated using the <see cref="Navigations.ToolbarItems"/> tag directive. 
        /// </summary>
        /// <value>
        /// <see cref="Navigations.ToolbarItems"/>
        /// </value>
        [Parameter]
        public List<ToolbarItem> Items { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates how to display toolbar items when the toolbar content exceeds the viewing area.
        /// </summary>
        /// <value>
        /// One of the <see cref="Navigations.OverflowMode"/> enumeration. The default value is <see cref="OverflowMode.Scrollable"/>
        /// </value>
        /// <remarks>
        /// If the <c>OverflowMode</c> is <c>Scrollable</c>, all the elements are displayed in a single line with enabled horizontal scrolling.
        /// If the <c>OverflowMode</c> is <c>Popup</c>, prioritized elements are displayed on the toolbar and the rest of elements are moved to the popup.
        /// If the popup content overflows the height of the page, the rest of the elements will be hidden.
        /// If the <c>OverflowMode</c> is <c>MultiRow</c>, overflow toolbar items are displayed as in-line of toolbar.
        /// If the <c>OverflowMode</c> is <c>Extended</c>, hides the overflow items in next row. The extended content were shown when click on the expand icon.
        /// </remarks>
        [Parameter]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OverflowMode OverflowMode { get; set; }

        /// <summary>
        /// Gets or sets the scrolling distance of toolbar scroller.
        /// </summary>
        /// <value> 
        /// When the left/right navigation icon is clicked, then the toolbar scrolled based on the specified value otherwise the default value `0` is set.
        /// </value>
        /// <example>
        /// <code><![CDATA[
        /// <SfToolbar ScrollStep="50"></SfToolbar> 
        /// ]]></code>
        /// </example>
        /// <remarks>
        /// This property is applicable only when <see cref="OverflowMode.Scrollable"/> is set.
        /// </remarks>
        [Parameter]
        public int ScrollStep { get; set; }

        /// <summary> 
        /// Gets or sets the width of the toolbar element in pixels/number/percentage. 
        /// </summary> 
        /// <value> 
        /// If we set the width value, then the toolbar will render based on specified width otherwise the default width value `auto` is set.  
        /// </value> 
        /// <example>
        /// <code><![CDATA[ 
        /// <SfToolbar Width="500px"></SfToolbar> 
        /// ]]></code> 
        /// </example> 
        /// <remarks>
        /// If we set number values, then it is considered as pixels.
        /// </remarks>
        [Parameter]
        public string Width { get; set; } = "auto";

        /// <summary> 
        /// Gets or sets a collection of additional attributes that will applied to the toolbar element. 
        /// </summary> 
        /// <remarks>
        /// Additional attributes can be added by specifying as inline attributes or by specifying <c>@attributes</c> directive.
        /// </remarks> 
        /// <value> 
        /// It allows the toolbar component to render non-declared attributes. The default value is `null`. 
        /// </value>
        /// <example> 
        /// <code><![CDATA[ 
        /// <SfToolbar title="Search toolbar"></SfToolbar> 
        /// ]]></code> 
        /// </example>
        [Parameter(CaptureUnmatchedValues = true)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Dictionary<string, object> HtmlAttributes
        {
            get => htmlAttributesValue;
            set => htmlAttributesValue = SfBaseUtils.SanitizeHtmlAttributes(value);
        }

        internal void UpdateChildProperties(List<ToolbarItem> toolbarItems)
        {
            Items = toolbarItems;
        }
    }
}