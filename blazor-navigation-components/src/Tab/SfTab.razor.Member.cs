using System.Text.Json.Serialization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace Syncfusion.Blazor.Navigations
{
    public partial class SfTab : SfBaseComponent
    {
        private TabAnimationSettings? animation { get; set; }
        private string? cssClass;
        private bool allowDragAndDrop;
        private bool enableRtl;
        private HeaderPosition headerPlacement;
        private string? height;
        private List<TabItem>? tabitems;
        private OverflowMode overflowMode;
        private int scrollStep;
        private int selectedItem;
        private bool showCloseButton;
        private string? width;

        /// <summary>
        /// Gets or sets the unique Id value for tab component.
        /// </summary>
        /// <value>
        /// If we set the id, then the id value set for tab element. The default value is `null`.
        /// </value>
        [Parameter]
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the child content of tab component.
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Gets or sets the animation to appear while activating the <see cref="TabItem"/>.
        /// </summary>
        /// <value>
        /// <see cref="TabAnimationSettings"/> value is applied for previous/next tab switching, By default `null` value is set.
        /// </value>
        [Parameter]
        public TabAnimationSettings Animation { get; set; }

        /// <summary>
        /// Gets or sets the custom classes to customize the tab component.  
        /// </summary>
        /// <value>
        /// A string containing one or more CSS classes separated by spaces, applied to the tab element. The default value is <c>string.Empty</c>.
        /// </value>
        /// <example>
        /// <code><![CDATA[
        /// <SfTab CssClass="custom-tab"></SfTab>
        /// ]]></code>
        /// </example>
        [Parameter]
        public string CssClass { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets whether to persist component's state between page reloads. When set to <c>true</c>, the <see cref="SelectedItem" /> property is persisted.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the component's state persistence is enabled. The default value is <c>false</c>.
        /// </value>
        /// <remarks>
        /// Component's <see cref="SelectedItem"/> property will be stored in browser local storage to persist component's state when page reloads.
        /// It is mandatory to provide <see cref="ID"/> to persist <c>SelectedItem</c> property.
        /// </remarks>
        [Parameter]
        public bool EnablePersistence { get; set; }

        /// <summary> 
        /// Gets or sets whether the tab allows drag and drop over tab items. 
        /// </summary> 
        /// <value>
        /// false, the drag and drop action in tab, The default value is `true`.
        /// </value>
        /// <remarks>
        /// Tab item has been reordered based on the dropped item.
        /// </remarks>
        [Parameter]
        public bool AllowDragAndDrop { get; set; }

        /// <summary> 
        /// Gets or sets the area to move the draggable element, Outside that area dragging will be restricted.
        /// </summary> 
        /// <value>
        /// Accepts a string value. The default value is <c>null</c>.
        /// </value>
        /// <example>
        /// In below example tab items can be able to drag with in the `e-tab-container` selector.
        /// <code><![CDATA[ 
        /// <div class="e-tab-container">
        ///     <SfTab ID="tab_1" AllowDragAndDrop="true" DragArea=".e-tab-container"></SfTab>
        /// </div>
        /// ]]></code>
        /// </example> 
        /// <remarks>
        /// By default, the draggable element movement occurs with in tabitems.
        /// <c>DragArea</c> value is applied only when <see cref="AllowDragAndDrop"/> property is enabled.
        /// </remarks>
        [Parameter]
        public string DragArea { get; set; }

        /// <summary>
        /// Gets or sets whether the right to left direction is enabled for tab component.
        /// </summary>
        /// <value> 
        /// true, the right to left direction is enabled for tab component. The default value is `false`. 
        /// </value>
        [Parameter]
        public bool EnableRtl { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies the orientation of the tab header.
        /// </summary>
        /// <value>
        /// One of the <see cref="HeaderPosition"/> enumeration. The default value is <see cref="HeaderPosition.Top"/>
        /// </value>
        /// <remarks>
        /// If the <c>HeaderPosition</c> is <c>Top</c>, Places the Tab header on the top.
        /// If the <c>HeaderPosition</c> is <c>Bottom</c>, Places the Tab header at the bottom.
        /// If the <c>HeaderPosition</c> is <c>Left</c>, Places the Tab header at the left.
        /// If the <c>HeaderPosition</c> is <c>Right</c>, Places the Tab header at the right.
        /// </remarks>
        [Parameter]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public HeaderPosition HeaderPlacement { get; set; }

        /// <summary> 
        /// Gets or sets the height of the tab element in pixels/number/percentage.
        /// </summary> 
        /// <value> 
        /// If we set the height value, then the tab will render based on specified height otherwise the default height value `auto` is set.  
        /// </value>
        /// <remarks>
        /// If we set number values, then it is considered as pixels.
        /// By default, Tab height is set based on the height of its parent.
        /// </remarks>
        [Parameter]
        public string Height { get; set; } = "auto";

        /// <summary>
        /// Gets or sets the list of tab items that will be populated using the <see cref="TabItems"/> tag directive. 
        /// </summary>
        /// <value>
        /// <see cref="TabItems"/>
        /// </value>
        [Parameter]
        public List<TabItem> Items { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies the content render modes of tab component.
        /// </summary>
        /// <value>
        /// One of the <see cref="ContentLoad"/> enumeration. The default value is <see cref="ContentLoad.Dynamic"/>
        /// </value>
        /// <remarks>
        /// If the <c>ContentLoad</c> is <c>Dynamic</c>, renders the tab content dynamically when switching its header.
        /// If the <c>ContentLoad</c> is <c>Init</c>, renders all the tab content on initial loading.
        /// If the <c>ContentLoad</c> is <c>Demand</c>, renders the tab content when required but keeps the content once it is rendered.
        /// </remarks>
        [Parameter]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ContentLoad LoadOn { get; set; }

        /// <summary>
        /// Gets or sets a culture which overrides the global culture and localization value for this component.
        /// </summary>
        /// <value>
        /// Accepts a string value. The default value is <c>string.Empty</c>.
        /// </value>
        /// <remarks>
        /// By default global culture is 'en-US'.
        /// </remarks>
        [Parameter]
        public string Locale { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value that indicates how to display tab header items when the tab exceeds the viewing area.
        /// </summary>
        /// <value>
        /// One of the <see cref="Navigations.OverflowMode"/> enumeration. The default value is <see cref="OverflowMode.Scrollable"/>
        /// </value>
        /// <remarks>
        /// If the <c>OverflowMode</c> is <c>Scrollable</c>, all the elements are displayed in a single line with enabled horizontal scrolling.
        /// If the <c>OverflowMode</c> is <c>Popup</c>, tab container will hold the items that can be placed within the available space and the rest of the items will be moved to the popup.
        /// </remarks>
        [Parameter]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OverflowMode OverflowMode { get; set; }

        /// <summary>
        /// Gets or sets the scrolling distance of tab header when scrolling is enabled.
        /// </summary>
        /// <value>
        /// When the left/right navigation icon is clicked, then the tab header scrolled based on the specified value otherwise the default value `0` is set.
        /// </value>
        /// <example>
        /// <code><![CDATA[
        /// <SfTab ScrollStep="50"></SfTab> 
        /// ]]></code>
        /// </example>
        /// <remarks>
        /// This property is applicable only when <see cref="OverflowMode.Scrollable"/> is set.
        /// </remarks>
        [Parameter]
        public int ScrollStep { get; set; }

        /// <summary>
        /// Gets or sets the index of active tab item. 
        /// </summary> 
        /// <value> 
        /// If we set the index of tab item, then the specified index were set as selected tab item otherwise the default <c>0</c> value is set.  
        /// </value> 
        /// <example> 
        /// <code><![CDATA[ 
        /// <STab SelectedItem="1"></SfTab>
        /// ]]></code>
        /// </example>
        [Parameter]
        public int SelectedItem { get; set; }

        /// <summary>
        /// Invokes when index of selected item were changed.
        /// </summary>
        /// <value> 
        /// Fired when selected item index changes.
        /// </value>
        [Parameter]
        public EventCallback<int> SelectedItemChanged { get; set; }

        /// <summary>
        /// Gets or sets whether the tab transition should occur or not when performing Touch/Mouse swipe action.
        /// </summary>
        /// <remarks>
        /// <para>SwipeMode = <c>TabSwipeMode.Touch | TabSwipeMode.Mouse</c>: Enables swiping for both touch and mouse input.</para>
        /// <para>SwipeMode = <c>TabSwipeMode.Touch</c>: Enables swiping only for touch input.</para>
        /// <para>SwipeMode = <c>TabSwipeMoe.Mouse</c>: Enables swiping only for mouse input.</para>
        /// <para>SwipeMode = <c>~TabSwipeMode.Touch | ~TabSwipeMode.Mouse</c>: Disables swiping for both touch and mouse input.</para>
        /// </remarks>
        /// <value>
        /// One of the <see cref="TabSwipeMode"/> enumeration values that represents the swiping action for the tabs.
        /// The default value is <c>TabSwipeMode.Touch | TabSwipeMode.Mouse</c>
        /// </value>
        [Parameter]
        [DefaultValue(TabSwipeMode.Touch | TabSwipeMode.Mouse)]
        public TabSwipeMode SwipeMode { get; set; } = TabSwipeMode.Touch | TabSwipeMode.Mouse;

        /// <summary>
        /// Gets or sets whether to show the close button in the tab header or not.
        /// </summary>
        /// <value>
        /// true, to show the close button in tab header. The default value is <c>false</c> is set.
        /// </value>
        [Parameter]
        public bool ShowCloseButton { get; set; }
        
        /// <summary>
        /// Gets or sets whether to re-order tab items to show active tab item in the header area or popup when OverflowMode is popup.
        /// </summary>
        /// <value>
        /// true, if active tab item should be visible in header area instead of pop-up; otherwise, false. The default value is true.
        /// </value>
        /// <remarks>
        /// <see cref="ReorderActiveTab"/> Property is only applicable when OverflowMode is popup.
        /// </remarks>
        [Parameter]
        public bool ReorderActiveTab { get; set; } = true;

        /// <summary> 
        /// Gets or sets the width of the tab element in pixels/number/percentage. 
        /// </summary> 
        /// <value> 
        /// If we set the width value, then the tab will render based on specified width otherwise the default width value `100%` is set.  
        /// </value>
        /// <remarks>
        /// If we set number values, then it is considered as pixels.
        /// By default, Tab width sets based on the width of its parent.
        /// </remarks>
        [Parameter]
        public string Width { get; set; } = "100%";

        /// <summary>
        /// Gets or sets a value that indicates whether re-initialize the tab content on every <see cref="TabItem"/> initialization.
        /// </summary>
        /// <remarks>
        /// Use this property to control the rendering behavior of tab content in relation to the Blazor stream rendering feature.
        /// </remarks>
        /// <value>
        /// <c>true</c> to enable <see cref="SfTab"/> re-initialization on <see cref="TabItem"/> component initialization; otherwise,  <c>false</c>.
        /// </value>
        [Parameter]
        public bool ShouldReinitialize { get; set; } = false;

        /// <summary> 
        /// Gets or sets a collection of additional attributes that will applied to the tab element. 
        /// </summary> 
        /// <remarks>
        /// Additional attributes can be added by specifying as in-line attributes or by specifying <c>@attributes</c> directive.
        /// </remarks> 
        /// <value> 
        /// It allows the tab component to render non-declared attributes. The default value is `null`. 
        /// </value>
        /// <example> 
        /// <code><![CDATA[ 
        /// <SfTab tabindex="0"></SfTab>
        /// ]]></code>
        /// </example>
        [Parameter(CaptureUnmatchedValues = true)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Dictionary<string, object> HtmlAttributes { get; set; }

        internal void UpdateItemProperties(List<TabItem> item)
        {
            Items = tabitems = item;
        }

        internal void UpdateAnimationProperties(TabAnimationSettings animationSettings)
        {
            TabAnimationSettings? animate = null;
            if (SyncfusionService.options.Animation == GlobalAnimationMode.Disable)
            {
                Animation = animation = animate;
                return;
            }
            if ((SyncfusionService.options.Animation == GlobalAnimationMode.Default) || (SyncfusionService.options.Animation == GlobalAnimationMode.Enable))
            {
                animate = animationSettings;
            }
            if (animationSettings == null)
            {
                animate = new TabAnimationSettings();
                animate.UpdateNextProperties(animate.Next);
                animate.UpdatePreviousProperties(animate.Previous);
            }
            Animation = animation = animate;
        }
    }
}
