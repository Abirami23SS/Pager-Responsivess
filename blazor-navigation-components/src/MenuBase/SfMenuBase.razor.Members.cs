using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Syncfusion.Blazor.Navigations.Internal
{
    public partial class SfMenuBase<TValue>
    {
        /// <summary> 
        /// Gets or sets the child content for the menu including HTML element. If the child content is not specified, menu is rendered using <see cref="Items"/> property. 
        /// </summary> 
        /// <value> 
        /// The template content. The default value is <c>null</c>. 
        /// </value> 
        /// <remarks> 
        /// The child content which is specified within <c>SfMenu</c> tag directive is either a string or HTML Element. The menu item is also specified using <see cref="Items"/> property. 
        /// </remarks> 
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary> 
        /// Gets or sets CSS class string to customize the appearance of menu. 
        /// </summary> 
        /// <value> 
        /// Accepts a CSS class string separated by space to customize the appearance of menu. The default value is <c>String.Empty</c>. 
        /// </value> 
        [Parameter]
        public string CssClass { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets a value that indicates whether to enable or disable the right to left direction in menu bar.
        /// </summary> 
        /// <value> 
        /// <c>true</c>, if the right to left direction is enabled for menu bar. The default value is <c>false</c>. 
        /// </value> 
        [Parameter]
        public bool EnableRtl { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the menu items with its properties which will be rendered as ContextMenu.
        /// </summary>
        [Parameter]
        public List<TValue> Items { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether to enable or disable the mouse click option to show the sub menu. 
        /// </summary> 
        /// <value> 
        /// <c>true</c>, if the sub menu will open only on mouse click. The default value is <c>false</c>. 
        /// </value> 
        [Parameter]
        public bool ShowItemOnClick { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether to enable or disable the scrollable option in menu bar.
        /// </summary>
        [Parameter]
        public bool EnableScrolling { get; set; }
    }
}