using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;


namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Represents a class that provides information about <see cref="SfPager"/> which as context for <see cref="SfPager.Template"/>.
    /// </summary>
    public class PagerTemplateContext
    {
        /// <summary>
        /// Gets the page number of the current page displayed in the pager.
        /// </summary>
        /// <value>
        /// The page number of the current page displayed.
        /// </value>
        [DefaultValue(1)]
        public int CurrentPage { get; internal set; }

        /// <summary>
        /// Gets or sets the number of items shown on a single page of the pager.
        /// </summary>
        /// <value>
        /// The number of items shown on a single page.
        /// </value>
        [DefaultValue(12)]
        public int PageSize { get; set; }

        /// <summary>
        /// Gets the list of items in the Pager dropdown.
        /// </summary>
        [DefaultValue(null)]
        public List<int>? PageSizes { get; internal set; }

        /// <summary>
        /// Gets the total number of items, to calculate <see cref="TotalPages"/> count based on <see cref="PageSize"/>.
        /// </summary>
        /// <value>
        /// Total number of items.
        /// </value>
        [DefaultValue(default(int))]
        public int TotalItemsCount { get; internal set; }

        /// <summary>
        /// Gets the total number of pages calculated using <see cref="TotalItemsCount"/> and <see cref="PageSize"/>.
        /// </summary>
        /// <value>
        /// Total number of pages.
        /// </value>
        [DefaultValue(default(int))]
        public int TotalPages { get; internal set; }
    }

    /// <summary>
    /// Provides information about the <see cref="SfPager.ItemClick"/> event.
    /// </summary>
    public class PagerItemClickEventArgs
    {
        /// <summary>
        /// Gets the page number of the current page displayed on the Pager.
        /// </summary>
        /// <value>
        /// The page number of the current page.
        /// </value>
        public int CurrentPage { get; internal set; }

        /// <summary>
        /// Gets the page number of the previous page displayed on the pager.
        /// </summary>
        /// <value>
        /// The page number of the previous page.
        /// </value>
        public int PreviousPage { get; internal set; }
    }

    /// <summary>
    /// Provides information about the <see cref="SfPager.PageChanging"/> event.
    /// </summary>
    /// <remarks>
    /// You can cancel the page changing action by setting a property in the event handler.
    /// </remarks>
    public class PageChangingEventArgs
    {
        /// <summary>
        /// Gets or sets a value that indicates whether to cancel the page changing action of the pager.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the event should be canceled. Otherwise, <c>false</c>.
        /// </value>
        public bool Cancel { get; set; }

        /// <summary>
        /// Gets the page number of the current page displayed on the Pager.
        /// </summary>
        /// <value>
        /// The page number of the current page.
        /// </value>
        public int CurrentPage { get; internal set; }

        /// <summary>
        /// Gets the page number of the previous page displayed on the pager.
        /// </summary>
        /// <value>
        /// The page number of the previous page.
        /// </value>
        public int PreviousPage { get; internal set; }
    }

    /// <summary>
    /// Provides information about the <see cref="SfPager.PageChanged"/> event.
    /// </summary>
    public class PageChangedEventArgs
    {
        /// <summary>
        /// Gets the page number of the current page displayed on the Pager.
        /// </summary>
        /// <value>
        /// The page number of the current page.
        /// </value>
        public int CurrentPage { get; internal set; }

        /// <summary>
        /// Gets the page number of the previous page displayed on the pager.
        /// </summary>
        /// <value>
        /// The page number of the previous page.
        /// </value>
        public int PreviousPage { get; internal set; }
    }

    /// <summary>
    /// Provides information about the <see cref="SfPager.PageSizeChanged"/> event.
    /// </summary>
    public class PageSizeChangedArgs
    {
        /// <summary>
        /// Gets or sets the number of items displaying on the single page of the pager.
        /// </summary>
        /// <value>
        /// The number of items shown on a single page.
        /// </value>
        public int CurrentPageSize { get; set; }

        /// <summary>
        /// Gets the total number of pages calculated using <see cref="SfPager.TotalItemsCount"/> and <see cref="SfPager.PageSize"/>.
        /// </summary>
        /// <value>
        /// Total number of pages.
        /// </value>
        public int TotalPages { get; internal set; }

        /// <summary>
        /// Gets the page number of the current page displayed on the pager.
        /// </summary>
        /// <value>
        /// The page number of the current page.
        /// </value>
        public int CurrentPage { get; internal set; }
    }

    /// <summary>
    /// Provides information about the <see cref="SfPager.PageSizeChanging"/> event.
    /// </summary>
    public class PageSizeChangingArgs
    {
        /// <summary>
        /// Cancels the current action and prevents it from getting the current page size changed.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the event should be canceled. Otherwise, <c>false</c>.
        /// </value>
        public bool Cancel { get; set; }

        /// <summary>
        /// Gets the number of items rendered on a page previously.
        /// </summary>
        /// <value>
        /// Current <see cref="SfPager.PageSize"/> which is the number of items shown on a single page.
        /// </value>
        public int PreviousPageSize { get; internal set; }

        /// <summary>
        /// Gets the currently selected value from the Pager dropdown, which no of items going to render on a page.
        /// </summary>
        /// <value>
        /// The new <see cref="SfPager.PageSize"/> which is the number of items shown on a single page.
        /// </value>
        public string? SelectedPageSize { get; internal set; }

        /// <summary>
        /// Gets the count of the total number of pages available in the Pager.
        /// </summary>
        /// <value>
        /// Total number of pages.
        /// </value>
        public int TotalPages { get; internal set; }
    }

}