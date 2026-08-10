using Microsoft.AspNetCore.Components.Web;

namespace Syncfusion.Blazor.Navigations
{

    /// <summary>
    /// Provides information about the <see cref="SfBreadcrumb.ItemRendering"/> event callback.
    /// </summary>
    public class BreadcrumbItemRenderingEventArgs
    {
        /// <summary>
        /// Gets or sets the Breadcrumb item that is being render. 
        /// </summary>
        /// <value>
        /// The BreadcrumbItem object that represents the item being rendered.
        /// </value>
        public BreadcrumbItem Item { get; internal set; }

        /// <summary>
        /// Gets or sets whether the rendering of Breadcrumb item should be canceled or not.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the event should be canceled. Otherwise, <c>false</c>.
        /// </value>
        public bool Cancel { get; set; }
    }

    /// <summary>
    /// Provides information about the <see cref="SfBreadcrumb.ItemClicked"/> event callback.
    /// </summary>
    public class BreadcrumbClickedEventArgs
    {
        /// <summary>
        /// Gets the clicked Breadcrumb item.
        /// </summary>
        /// <value>
        /// The BreadcrumbItem object that represents the clicked Breadcrumb item.
        /// </value>
        public BreadcrumbItem Item { get; internal set; }
    }
}
