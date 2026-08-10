using System.Threading.Tasks;
using System.Globalization;


namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Methods partial class
    /// </summary>    
    public partial class SfPager : SfBaseComponent
    {

        /// <summary>
        /// Navigates to the given page number in the Pager, it can also fetch the given page by traversing between the next and previous pagers if present in the Pager component.
        /// </summary>
        /// <param name="pageNo">Enter the page number to be shown in the Pager. </param>
        /// <remarks>
        /// When the given page number is greater than the total pages, then no actions will be performed in the Pager.
        /// </remarks>
        /// <returns><see cref="System.Threading.Tasks.Task"/>.</returns>
        /// <example>
        /// <code><![CDATA[
        /// <button @onclick="HandleButtonClick">GoToPage</button>
        /// <SfPager @ref="Pager" PageSize=5 NumericItemsCount=4 TotalItemsCount=100>
        ///  ........
        /// </SfPager>
        /// @code{
        ///    SfPager Pager;
        ///    private async Task HandleButtonClick()
        ///    {
        ///      await Pager.GoToPageAsync(10); // pass the page number here.    
        ///    }
        /// }
        /// ]]>
        /// </code>
        /// </example>

        public async Task GoToPageAsync(int pageNo)
        {
            await NavigateToPage(pageNo, true).ConfigureAwait(true);
        }

        /// <summary>
        /// Navigates to the last page in the Pager by traversing the next pagers, if present in the Pager component.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        /// <remarks>
        /// If the current selected page is the last page of the Pager, then no actions will be performed in the Pager component.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// <button @onclick="HandleButtonClick">GoToLastPage</button>
        /// <SfPager @ref="Pager" PageSize=5 NumericItemsCount=4 TotalItemsCount=100>
        ///  ........
        /// </SfPager>
        /// @code{
        ///    SfPager Pager;
        ///    private async Task HandleButtonClick()
        ///    {
        ///      await Pager.GoToLastPageAsync();
        ///    }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public async Task GoToLastPageAsync()
        {
            await NavigateToPage(TotalPages).ConfigureAwait(true);
        }

        /// <summary>
        /// Navigates to the first page in the Pager, by traversing the previous pagers if present in the Pager component.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        /// <remarks>
        /// If the current selected page is the first page of the Pager, then no actions will be performed in the Pager component.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// <button @onclick="HandleButtonClick">GoToFirstPage</button>
        /// <SfPager @ref="Pager" PageSize=5 NumericItemsCount=4 TotalItemsCount=100>
        ///  ........
        /// </SfPager>
        /// @code{
        ///    SfPager Pager;
        ///    private async Task HandleButtonClick()
        ///    {
        ///      await Pager.GoToFirstPageAsync();
        ///    }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public async Task GoToFirstPageAsync()
        {
            await NavigateToPage(1).ConfigureAwait(true);
        }

        /// <summary>
        /// Navigate to the next page in the Pager. Consider the currently selected page is the last numeric item of the Pager, then it's loaded the next set of numeric items if it exists.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        /// <remarks>
        /// If the currently selected page is the last page of the Pager and if the next page icon and the last page icon is disabled, then no actions will be performed.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// <button @onclick="HandleButtonClick">GoToNextPage</button>
        /// <SfPager @ref="Pager" PageSize=5 NumericItemsCount=4 TotalItemsCount=100>
        ///  ........
        /// </SfPager>
        /// @code{
        ///    SfPager Pager;
        ///    private async Task HandleButtonClick()
        ///    {
        ///      await Pager.GoToNextPageAsync();
        ///    }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public async Task GoToNextPageAsync()
        {
            if (!enableNextSet)
            {
                return;
            }
            await NavigateToPage(CurrentPage + 1).ConfigureAwait(true);
        }

        /// <summary>
        /// Navigate to the previous page in the Pager. Consider the currently selected page is the first numeric item of the Pager, then it's loaded the previous set of numeric items if it exists.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        /// <remarks>
        /// If the currently selected page is the first page of the Pager and if the previous icon and the previous page icon is disabled, then no actions will be performed.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// <button @onclick="HandleButtonClick">GoToPreviousPage</button>
        /// <SfPager @ref="Pager" PageSize=5 NumericItemsCount=4 TotalItemsCount=100>
        ///  ........
        /// </SfPager>
        /// @code{
        ///    SfPager Pager;
        ///    private async Task HandleButtonClick()
        ///    {
        ///      await Pager.GoToPreviousPageAsync();
        ///    }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public async Task GoToPreviousPageAsync()
        {
            if (!enablePrevSet)
            {
                return;
            }
            await NavigateToPage(CurrentPage - 1).ConfigureAwait(true);
        }


        /// <summary>
        /// Used to update the page size of the Pager to change the number of items that can be rendered on a Page.
        /// </summary>
        /// <param name="pageSize">The number of items to be shown on a page. </param>
        /// <returns><see cref="System.Threading.Tasks.Task"/>.</returns>
        /// <remarks>
        /// By changing the page size, the Pager component dynamically updates the total number of pages, numeric item count, next, and previous pagers count according to the given page size and updates the UI.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// <button @onclick="HandleButtonClick">UpdatePageSize</button>
        /// <SfPager @ref="Pager" PageSize=5 NumericItemsCount=4 TotalItemsCount=100>
        ///  ........
        /// </SfPager>
        /// @code{
        ///    SfPager Pager;
        ///    private async Task HandleButtonClick()
        ///    {
        ///      await Pager.UpdatePageSizeAsync(10); // pass the page size here.    
        ///    }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public async Task UpdatePageSizeAsync(int pageSize)
        {
            UpdatePagerProperties("PageSize", pageSize);
            await InvokeStateChange().ConfigureAwait(true);
        }

        /// <summary>
        /// Used to update the number of numeric elements shown in the Pager component, and the next pager icon is used to get the next set of entered numeric pages count, likewise the previous pager icon is used to traverse backward between the pages.
        /// </summary>
        /// <param name="numericItemCount">The given number of numeric elements are shown in the Pager. </param>
        /// <returns><see cref="System.Threading.Tasks.Task"/>.</returns>
        /// <remarks>
        /// If the given numeric item count is greater than the total pages present in the Pager, then all the numeric items are shown in the Pager component.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// <button @onclick="HandleButtonClick">UpdateNumericItemsCount</button>
        /// <SfPager @ref="Pager" PageSize=5 NumericItemsCount=4 TotalItemsCount=100>
        ///  ........
        /// </SfPager>
        /// @code{
        ///    SfPager Pager;
        ///    private async Task HandleButtonClick()
        ///    {
        ///      await Pager.UpdateNumericItemsCountAsync(6); // pass the numeric items count here.    
        ///    }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public async Task UpdateNumericItemsCountAsync(int numericItemCount)
        {
            UpdatePagerProperties("NumericItemsCount", numericItemCount);
            await InvokeStateChange().ConfigureAwait(true);
        }

        /// <summary>
        ///  Applies all the property changes and re-renders the component again.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        /// <example>
        /// <code><![CDATA[
        /// <button @onclick="HandleButtonClick">Refresh</button>
        /// <SfPager @ref="Pager" PageSize=5 NumericItemsCount=4 TotalItemsCount=100>
        ///  ........
        /// </SfPager>
        /// @code{
        ///    SfPager Pager;
        ///    private async Task HandleButtonClick()
        ///    {
        ///      await Pager.UpdatePageSizeAsync(10);
        ///      await Pager.RefreshAsync();
        ///    }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        public async Task RefreshAsync()
        {
            await InvokeStateChange().ConfigureAwait(true);
        }

    }
}