using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using System.Text.Json.Serialization;
using Syncfusion.Blazor.Internal;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Partial Class SfSidebar.
    /// </summary>
    public partial class SfSidebar 
    {
        /// <summary>
        /// Sets id attribute for the sidebar element.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        [Parameter]
        public string ID { get; set; }

        /// <summary>
        /// Specifies the child content.
        /// </summary>
        /// <value>
        /// Accepts a RenderFragment that defines the content of the child element.
        /// </value>
        [Parameter]
        [JsonIgnore]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Gets or sets a boolean value to enable or disable the animation transitions on expanding or collapsing the Sidebar.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the animation can be enabled. Otherwise, <c>false</c>. The default value is <c>true</c>.
        /// </value>
        [Parameter]
        public bool Animate { get; set; } = true;

        /// <summary>
        /// Gets or sets a boolean value which indicates whether the Sidebar needs to be closed or not when the document area is clicked.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the sidebar will be closed when the document area is clicked.
        /// </value>
        [Parameter]
        public bool CloseOnDocumentClick { get; set; }
        private bool SidebarCloseOnDocumentClick { get; set; }

        /// <summary>
        /// Gets or sets the size of the Sidebar in dock state. Dock size can be set in pixel values.
        /// </summary>
        /// <value>
        /// Accepts the string value. The default value is <c>auto</c>.
        /// </value>
        [Parameter]
        public string DockSize { get; set; } = "auto";

        /// <summary>
        /// Gets or sets the docking state of the component.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the docking state can be enabled. Otherwise, <c>false</c>.
        /// </value>
        [Parameter]
        public bool EnableDock { get; set; }
        private bool _enableDock { get; set; }

        /// <summary>
        /// Gets or sets a boolean value to enable or disable the expand or collapse of Sidebar while swiping in the touch devices.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the gestures can be enabled. Otherwise, <c>false</c>. The default value is <c>true</c>.
        /// </value>
        /// <remarks>
        /// To avoid expand or collapse while swiping in desktop, this property can be set to `false`
        /// </remarks>
        [Parameter]
        public bool EnableGestures { get; set; } = true;

        /// <summary>
        /// Gets or sets a boolean value to enable or disable the persisting component's state between page reloads. If enabled, isOpen state will be persisted.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the persistence can be enabled. Otherwise, <c>false</c>.
        /// </value>
        [Parameter]
        public bool EnablePersistence { get; set; }

        /// <summary>
        /// Gets or sets a boolean value to enable or disable rendering the Sidebar in right to left direction.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the right to left direction can be enabled for the component. Otherwise, <c>false</c>. The default value is <c>false</c>.
        /// </value>
        [Parameter]
        public bool EnableRtl { get; set; }

        /// <summary>
        /// Gets or sets a boolean value which indicates whether the Sidebar component's state is open or close.
        /// When the Sidebar type is set to `Auto`,
        /// the component will be expanded in the desktop and collapsed in the mobile mode regardless of the isOpen property.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the sidebar is in open position. Otherwise, <c>false</c>.
        /// </value>
        [Parameter]
        public bool IsOpen { get; set; }

        private bool SidebarIsOpen { get; set; }

        /// <summary>
        /// Gets or sets a event callback when the `IsOpen` value of Sidebar is changed.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        [Parameter]
        public EventCallback<bool> IsOpenChanged { get; set; }

        /// <summary>
        /// Gets or sets the media query string for resolution, when opens the Sidebar.
        /// Example: assigning media query value to '(min-width: 600px)' will open the sidebar component only when the provided resolution is met else the sidebar will be in closed state.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        [Parameter]
        public string MediaQuery { get; set; }

        /// <summary>
        /// Gets or sets the position of the Sidebar.
        /// </summary>
        [Parameter]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SidebarPosition Position { get; set; }
        private SidebarPosition SidebarPosition { get; set; }

        /// <summary>
        /// Gets or sets whether to apply overlay options to the main content or not when the Sidebar is in an open state.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the back drop option can be enabled. Otherwise, <c>false</c>.
        /// </value>
        [Parameter]
        public bool ShowBackdrop { get; set; }
        private bool SliderShowBackdrop { get; set; }

        /// <summary>
        /// Gets or sets the target element where the sidebar will be placed.
        /// </summary>
        /// <value>
        /// A string value that identifies the target element.
        /// </value>
        [Parameter]
        public string Target { get; set; }

        /// <summary>
        /// Gets or sets the expanding types of the Sidebar.
        /// `Over` - The sidebar floats over the main content area.
        /// `Push` - The sidebar pushes the main content area to appear side-by-side and shrinks the main content within the screen width.
        /// `Slide` - The sidebar translates the x and y positions of the main content area based on the sidebar width.
        /// The main content area will not be adjusted within the screen width.
        ///  `Auto` - Sidebar with `Over` type in mobile resolution and `Push` type in other higher resolutions.
        /// </summary>
        [Parameter]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SidebarType Type { get; set; } = SidebarType.Auto;
        private SidebarType SidebarType { get; set; }

        /// <summary>
        /// Gets or sets the width of the Sidebar. By default, the width of the Sidebar sets based on the size of its content.
        /// Width can also be set in pixel values.
        /// </summary>
        /// <value>
        /// Accepts the string value. The default value is <c>auto</c>.
        /// </value>
        [Parameter]
        public string Width { get; set; } = "auto";
        private string? SidebarWidth { get; set; }

        /// <summary>
        /// Gets or sets the z-index of the Sidebar. It is applicable only when sidebar act as the overlay type.
        /// </summary>
        /// <value>
        /// Accepts an integer value. The default value is <c>1000</c>.
        /// </value>
        [Parameter]
        public int ZIndex { get; set; } = 1000;

        private Dictionary<string, object>? _htmlAttributes;
        /// <summary>
        /// You can add the additional html attributes such as disabled, value, and more to the root element.
        /// </summary>
        /// <value>
        /// A dictionary of additional html attributes for the root element of the component.
        /// </value>
        [Parameter(CaptureUnmatchedValues = true)]
        [Obsolete("This property is deprecated.Use @attributes to set additional attributes for sidebar element.")]
        public Dictionary<string, object> HtmlAttributes {
            get => _htmlAttributes;
            set
            {
                _htmlAttributes = SfBaseUtils.SanitizeHtmlAttributes(value);
                SidebarHtmlAttributes = _htmlAttributes;
            }
        }

        internal Dictionary<string, object>? SidebarHtmlAttributes { get; set; }
    }
}