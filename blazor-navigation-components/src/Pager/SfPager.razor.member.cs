using System.ComponentModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components;

namespace Syncfusion.Blazor.Navigations
{

    public partial class SfPager : SfBaseComponent
    {

        /// <summary>
        /// Gets or sets the ID of the Pager component.
        /// </summary>
        /// <value>
        /// Property which maps the given value to the Pager ID.
        /// </value>

        [Parameter]
        public string? ID { get; set; }

        /// <summary>
        /// Defines the child content which is given inside the pager component.
        /// </summary>
        /// <exclude/>
        [Parameter]
        [JsonIgnore]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// Gets or sets the current page number of the Pager.
        /// </summary>
        /// <value>
        /// The entered page number is shown when the Pager is rendered, by default the value is 1.
        /// </value>
        /// <remarks>
        /// If the given current page number is greater than the total number of pages present in the pager, then by default the first page will be the current page.
        /// </remarks>
        [Parameter]
        [DefaultValue(1)]
        [JsonPropertyName("currentPage")]
        public int CurrentPage { get; set; } = 1;
        private int _currentPage { get; set; }

        /// <summary>
        /// Gets or sets the text to append as a prefix with numeric values in the Pager.
        /// </summary>
        /// <value>
        /// A prefix for numeric item. The default value is <c>string.Empty</c>.
        /// </value>

        [Parameter]
        [DefaultValue(null)]
        [JsonPropertyName("numericItemPrefix")]
        public string? NumericItemPrefix { get; set; }
        private string? _numericItemPrefix { get; set; }

        /// <summary>
        /// Gets or sets whether to enable or disable the Pager message, displayed on the right side of the Pager icons.
        /// </summary>
        /// <value>
        /// <b>true</b> If `ShowPagerMessage` is set to true, the pager information such as the current page, total pages, and total records count is displayed.
        /// </value>
        /// <remarks>
        /// By default the ShowPagerMessage is set to true.
        /// </remarks>

        [Parameter]
        [DefaultValue(true)]
        [JsonPropertyName("showPagerMessage")]
        public bool ShowPagerMessage { get; set; } = true;
        private bool _showPagerMessage { get; set; }

        /// <summary>
        /// Gets or sets whether to enable the persistence in the Pager, It will allow access to store the current state of the Pager.
        /// </summary>
        /// <value>
        /// <b>true</b> If `EnablePersistence` is set to true, It will store the pager state such as current page, page size, current focus, and pager dropdown in the 
        /// window.localStorage when the component is disposed.
        /// </value>
        /// <remarks>
        /// By default the EnablePersistence is set to false.
        /// </remarks>

        [Parameter]
        [DefaultValue(false)]
        [JsonPropertyName("enablePersistence")]
        public bool EnablePersistence { get; set; }
        private bool _enablePersistence { get; set; }

        //TODO: Actually its belongs to DataManager. As of now, we have comment this.
        ///// <summary>
        ///// If EnableQueryString set to true,
        ///// then it pass current page information as a query string along with the URL while navigating to other page.
        ///// </summary>
        //[Parameter]
        //[DefaultValue(false)]
        //[JsonPropertyName("enableQueryString")]
        //public bool EnableQueryString { get; set; }
        //private bool _enableQueryString { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the number of page numeric buttons shown on the pager user interface.
        /// </summary>
        /// <value>
        /// The number of page numeric buttons shown on the pager UI.
        /// </value>
        /// <remarks>
        /// If <code>NumericItemsCount</code> is 5 and the total number of pages is <c>20</c>, 
        /// then the pager will display numeric buttons for pages 1 to 5. 
        /// Users can navigate beyond this range using the next page and end-page buttons. 
        /// Also, if the given numeric items count is greater than the total number of pages, 
        /// then all the pages are shown in the Pager component.
        /// </remarks>

        [Parameter]
        [DefaultValue(10)]
        [JsonPropertyName("numericItemsCount")]
        public int NumericItemsCount { get; set; } = 10;
        private int _numericItemsCount { get; set; }

        /// <summary>
        /// Gets or sets the number of items shown on a single page.
        /// </summary>
        /// <value>
        /// The number of items shown on a single page, by default the value is 12. 
        /// </value>
        /// <remarks>
        /// When the given page size is greater than the total items present in the Pager, then all the items are dispalyed in the current page.
        /// </remarks>
        [Parameter]
        [DefaultValue(12)]
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; } = 12;
        private int _pageSize { get; set; }

        /// <summary>
        /// Gets or sets the list of items in the Pager dropdown. 
        /// Update what number of items can be rendered on a page by changing the dropdown value.
        /// </summary>
        /// <value>
        ///  The list of items to be shown in the Pager dropdown, by default the value is set to null.
        /// </value>
        /// <remarks>
        /// When one of the given page sizes is greater than the total items present in the Pager, when the particular dropdown is chosen all the items are dispalyed in the current page.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// <SfPager PageSizes=@pagesizes PageSize=10 TotalItemsCount=100 >
        /// </SfPager>
        /// @code{
        ///    public List<int> pagesizes = new List<int> { 5, 10, 12, 20 };
        /// }
        /// ]]></code>
        /// </example>
        [Parameter]
        [DefaultValue(null)]
        [JsonPropertyName("pageSizes")]
        public List<int>? PageSizes { get; set; }
        private List<int>? _pageSizes { get; set; }

       /// <summary>
        /// Gets or sets whether to add a All as one of the option in the Pager dropdown list.
        /// Total items can be rendered while changing the Pager dropdown value as All.
        /// </summary>
        /// <value>
        /// <b>true</b> If `ShowAllInPageSizes` is set to true, then added All value as one of the option in <see cref="PageSizes"/>.
        /// </value>
        /// <remarks>
        /// <see cref="PageSizes"/> should be updated with list items for this property, otherwise it's not needed. The default value is false.
        /// </remarks>

        [Parameter]
        [DefaultValue(false)]
        [JsonPropertyName("showAllInPageSizes")]
        public bool ShowAllInPageSizes { get; set; }

        /// <summary>
        /// Gets or sets the template to customize the pager UI with customized elements instead of the default UI.
        /// </summary>
        /// <value>
        /// The template content.
        /// </value>
        /// <remarks>
        /// Use the template’s context parameter to access a <see cref="PagerTemplateContext"/> and its fields.
        /// </remarks>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// <SfPager @ref = "Page" TotalItemsCount="100" PageSize="10">
        ///<Template>
        /// @{
        ///     var pagerContext = (context as PagerTemplateContext);
        ///     <span>
        ///         Page<SfNumericTextBox TValue="int" Value=@pagerContext.CurrentPage Width="40px" ShowSpinButton="false">
        ///             <NumericTextBoxEvents TValue = "int" ValueChange="PageValueHandler"></NumericTextBoxEvents>
        ///         </SfNumericTextBox> of<b> @pagerContext.TotalPages</b> pages.
        ///     </span>
        /// }
        /// </Template>
        /// </SfPager>
        /// @code {
        /// SfPager Page;
        /// private async Task PageValueHandler(ChangeEventArgs<int> args)
        /// {
        ///      await Page.GoToPageAsync(args.Value);
        /// }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        [Parameter]
        [DefaultValue(null)]
        [JsonIgnore]
        public RenderFragment<object>? Template { get; set; }

        /// <summary>
        /// Gets or sets the total number of items, to calculate <see cref="TotalPages"/> count based on <see cref="PageSize"/>.
        /// </summary>
        /// <value>
        /// The number of total items present in the pager component to calculate <see cref="TotalPages"/>.
        /// </value>
        [Parameter]
        [DefaultValue(default(int))]
        [JsonPropertyName("totalItemsCount")]
        public int TotalItemsCount { get; set; }
        private int _totalItemsCount { get; set; }

        /// <summary>
        /// Gets or sets the CSS class name, that can be appended with the root element of the Pager. One or more custom CSS classes can be added to the Pager.
        /// </summary>
        /// <value>
        /// Property in which the CSS classes are added. The default value is an empty string.
        /// </value>

        [Parameter]
        [DefaultValue("")]
        public string? CssClass { get; set; }
        private string? _cssClass { get; set; }


        /// <summary>
        /// An event that is raised when the numeric item is clicked.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <remarks>
        /// This event handler receives a <see cref="Syncfusion.Blazor.Navigations.PagerItemClickEventArgs "/> object which provides the details of new page navigation.
        /// </remarks>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// <SfPager ItemClick="HandletemClick" PageSize=5 NumericItemsCount=4 TotalItemsCount=100>
        /// </SfPager>
        /// @code{
        ///    public void HandletemClick(PagerItemClickEventArgs args)
        ///    {
        ///       // Enter the code here
        ///    }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        [Parameter]
        public EventCallback<PagerItemClickEventArgs> ItemClick { get; set; }

        /// <summary>
        /// An event that is raised when navigating to a new page.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <remarks>
        /// This event handler receives a <see cref="Syncfusion.Blazor.Navigations.PageChangingEventArgs "/> object which provides the details of new page navigation.
        /// </remarks>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// <SfPager PageSize=5 PageChanging="HandlePageChanging" NumericItemsCount=4 TotalItemsCount=100>
        /// </SfPager>
        /// @code{
        ///    public void HandlePageChanging(PageChangingEventArgs args)
        ///    {
        ///       // Enter the code here 
        ///    }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        [Parameter]
        public EventCallback<PageChangingEventArgs> PageChanging { get; set; }

        /// <summary>
        /// An event that is raised when navigated to a new page.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <remarks>
        /// This event handler receives a <see cref="Syncfusion.Blazor.Navigations.PageChangedEventArgs "/> object which provides the details of navigated page.
        /// </remarks>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// <SfPager PageSize=5 PageChanged="HandlePageChanged" NumericItemsCount=4 TotalItemsCount=100>
        /// </SfPager>
        /// @code{
        ///    public void HandlePageChanged(PageChangedEventArgs args)
        ///    {
        ///       // Enter the code here  
        ///    }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        [Parameter]
        public EventCallback<PageChangedEventArgs> PageChanged { get; set; }

        /// <summary>
        /// An event that is raised when Pager component is created.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// <SfPager PageSize=5 Created="HandlePagerCreated" TotalItemsCount=100>
        /// </SfPager>
        /// @code{
        ///    public void HandlePagerCreated()
        ///    {
        ///     // Enter the code here...
        ///    }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        [Parameter]
        public EventCallback Created { get; set; }
        
        /// <summary>
        /// An event that is raised, While dynamically change the pager size by using pager dropdown.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <remarks>
        /// This event handler receives a <see cref="Syncfusion.Blazor.Navigations.PageSizeChangedArgs "/> object which provides the details of updated pagesize.
        /// </remarks>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// <SfPager PageSize=5 PageSizes=@pagesizes PageSizeChanged="HandlePageSizeChanged" TotalItemsCount=100>
        /// </SfPager>
        /// @code{
        ///    public List<int> pagesizes = new List<int> { 5, 10, 12, 20 }; 
        ///    public void HandlePageSizeChanged(PageSizeChangedArgs args)
        ///    {
        ///     // Enter the code here...
        ///    }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        [Parameter]
        public EventCallback<PageSizeChangedArgs> PageSizeChanged { get; set; }

        /// <summary>
        /// An event that is raised, when dynamically changing the page size using the page sizes dropdown.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <remarks>
        /// This event handler receives a <see cref="Syncfusion.Blazor.Navigations.PageSizeChangingArgs "/> object which provides the details of current pagesize.
        /// </remarks>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// <SfPager PageSize=5 PageSizes=@pagesizes PageSizeChanging="HandlePageSizeChanging" TotalItemsCount=100 >
        /// </SfPager>
        /// @code{
        ///    public List<int> pagesizes = new List<int> { 5, 10, 12, 20 };    
        ///    public void HandlePageSizeChanging(PageSizeChangingArgs args)
        ///    {
        ///     // Enter the code here...    
        ///    }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        [Parameter]
        public EventCallback<PageSizeChangingArgs> PageSizeChanging { get; set; }
    }
}