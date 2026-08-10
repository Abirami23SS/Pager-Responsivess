namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Specifies how to display Breadcrumb items in <see cref="SfBreadcrumb"/> component when the Breadcrumb items exceeds Breadcrumb container or <see cref="SfBreadcrumb.MaxItems"/> property.
    /// </summary>
    public enum BreadcrumbOverflowMode
    {
        /// <summary>
        /// Shows the number of Breadcrumb items that can be accommodated within the container space, and creates a sub menu with the remaining items.
        /// </summary>
        Menu,

        /// <summary>
        /// The specified <see cref="SfBreadcrumb.MaxItems"/> count will be visible and the remaining items will be hidden. While clicking on the previous item, the hidden item will become visible.
        /// </summary>
        Hidden,

        /// <summary>
        /// Only the first and last items will be visible, and the remaining items will be hidden with collapsed icon. When the collapsed icon is clicked, all items become visible and scroll will be enabled if the space is not enough to show all items.
        /// </summary>
        Collapsed,

        /// <summary>
        /// Wraps the items on multiple lines when the Breadcrumb’s width exceeds the container space.
        /// </summary>
        Wrap,

        /// <summary>
        /// Shows an HTML scroll bar when the Breadcrumb’s width exceeds the container space.
        /// </summary>
        Scroll,

        /// <summary>
        /// Shows all the items on a single line.
        /// </summary>
        None
    }
}
