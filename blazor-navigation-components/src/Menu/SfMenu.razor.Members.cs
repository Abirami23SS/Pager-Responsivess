using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Navigations.Internal;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Menu is a graphical user interface that serve as navigation headers for your application.
    /// </summary>
    public partial class SfMenu<TValue> : SfMenuBase<TValue>
    {
        /// <summary>
        /// Gets or sets a value that indicates whether to enable or disable the hamburger mode.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the hamburger mode can be enabled. Otherwise, <c>false</c>.
        /// </value>
        [Parameter]
        public bool HamburgerMode { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the orientation of menu whether it can be horizontal or vertical.
        /// </summary>
        /// <value> 
        /// One of the <see cref="Orientation"/> enumeration. The default value is <c>Orientation.Horizontal</c>/> 
        /// </value> 
        /// <remarks> 
        /// If the <c>Orientation</c> is <c>Horizontal</c>, the menu items will be aligned horizontally. 
        /// If the <c>Orientation</c> is <c>Vertical</c>, the menu items will be aligned vertically. 
        /// </remarks> 
        [Parameter]
        public Orientation Orientation { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the target element to open/close Menu while click in Hamburger mode.
        /// </summary>
        /// <value>
        /// Accepts the string value. The default value is empty.
        /// </value>
        [Parameter]
        public string Target { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value that indicates the title text for hamburger mode in Menu.
        /// </summary>
        /// <value>
        /// Accepts the string value. The default value is <c>HEADERTITLE</c>.
        /// </value>
        [Parameter]
        public string Title { get; set; } = HEADERTITLE;
    }
}