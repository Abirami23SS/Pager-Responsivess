using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Internal;
using Syncfusion.Blazor.Navigations.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.JSInterop;

[assembly: InternalsVisibleTo("Syncfusion.Blazor.TreeGrid, PublicKey=00240000048000009400000006020000002400005253413100040000010001002382fcb1069523ce72d849497a557a445c151eaf4007aa79adef551a8204ca7f728e5378607d85695b16f129ec35bf4af15dcf6d3581deb8bb0debb239c33e7f1271a37c7f60f1044ae417730f5082abee5f9ec568a8a4cef04074394755706376e982dc6f9d15430faaad385ae8f00a77ef1c97517f1a1517004ce78028b9ce")]
[assembly: InternalsVisibleTo("Syncfusion.Blazor.Grids, PublicKey=00240000048000009400000006020000002400005253413100040000010001002382fcb1069523ce72d849497a557a445c151eaf4007aa79adef551a8204ca7f728e5378607d85695b16f129ec35bf4af15dcf6d3581deb8bb0debb239c33e7f1271a37c7f60f1044ae417730f5082abee5f9ec568a8a4cef04074394755706376e982dc6f9d15430faaad385ae8f00a77ef1c97517f1a1517004ce78028b9ce")]

namespace Syncfusion.Blazor.Navigations
{
    public partial class SfPager : SfBaseComponent
    {
        //Class constant variables
        private const string tabKey = "Tab";
        private const string shiftTabKey = "ShiftTab";
        private const string enterKey = "Enter";
        private const string spaceKey = "Space";
        private const string arrowLeftKey = "ArrowLeft";
        private const string arrowRightKey = "ArrowRight";
        private const string homeKey = "Home";
        private const string endKey = "End";
        private const string pageUpKey = "PageUp";
        private const string pageDownKey = "PageDown";
        private const string altPageUpKey = "AltPageUp";
        private const string altPageDownKey = "AltPageDown";
        private const string ctrlAltPageUpKey = "CtrlAltPageUp";
        private const string ctrlAltPageDownKey = "CtrlAltPageDown";
        private const string firstPage = "FirstPage";
        private const string previousPage = "PreviousPage";
        private const string nextPage = "NextPage";
        private const string lastPage = "LastPage";
        private const string nextPagerCount = "NextPagerCount";
        private const string previousPagerCount = "PreviousPagerCount";

        //Class private variables        
        private ElementReference element { get; set; }

        internal void Init() => _dotnetRef = Create();

        private DotNetObjectReference<SfPager>? _dotnetRef { get; set; }

        internal DotNetObjectReference<SfPager> Create() => DotNetObjectReference.Create<SfPager>(this);

        internal DotNetObjectReference<SfPager> GetRef() => _dotnetRef ?? Create();

        private bool isPerPageNeeded { get; set; }
        private int numericCount { get; set; }
        private int calculatedNumericItemsForEllipsis { get; set; }
        private bool ellipsisNavigationInProgress { get; set; }
        private string? currentFocus { get; set; }
        private bool isCurrentFocusChanged { get; set; }

        internal bool isPagerRefreshed { get; set; }

        private int[] visibleNumericItems { get; set; } = Array.Empty<int>();
        private int visibleNumericStartIndex { get; set; }
        private int visibleNumericEndIndex { get; set; }
        private bool hasVisibleNumericRange { get; set; }
        private int[] visibleItemCountHistory { get; set; } = new int[2];

        private int lastVisibleDigitCategory { get; set; }
        private int visibleItemCountHistoryCount { get; set; }
        private int pageStart { get; set; } = 1;
        private bool enablePrev { get; set; }
        private bool enableNext { get; set; }
        private bool enablePrevSet { get; set; }
        private bool enableNextSet { get; set; }
        private bool enableLeftDots { get; set; }
        private bool enableRightDots { get; set; }
        private List<PagerDropdownModel>? dropdownData { get; set; }
        private bool showDropdown { get; set; }
        private int dropdownValue { get; set; }

        private List<string> directParamKeys { get; set; } = new List<string>();

        private int currentPage { get; set; } = 1;
        private Dictionary<string, object> directParameters { get; set; } = new Dictionary<string, object>();

        private bool _isPreventRender { get; set; }

        private static JsonSerializerOptions _jsonSettings = new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

        private static JsonSerializerOptions _persistJsonSettings = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString,
            Converters = {
                                new DateTimeZoneHandlingConverter(),
                                new JsonStringEnumConverter(),
                                new ExpandoObjectConverter(),
            }
        };

        //Class internal variables        
        internal bool IsRerendering { get; set; }

        internal string DataId = "SfPager-" + Guid.NewGuid().ToString();
        internal string CurrentFocus { get; set; } = "0";
        internal bool ShowExternalMessage { get; set; }
        internal string? ExternalMessage { get; set; }

        internal int TotalPages { get; set; }

        //used for dependent Grid component
        internal bool EnableRtl { get; set; }

        internal bool suppressFocus { get; set; }
        internal bool isRenderedFromGrid { get; set; }
        internal bool adaptivePagerMessage { get; set; }
        internal bool IsRenderedFromTreeGrid { get; set; }

        internal static string EnsureTabIndex(string focus)
        {
            return !string.IsNullOrEmpty(focus) ? "0" : "-1";
        }

        /// <summary>
        /// Processing the property value changes and invoking the events for two-way bindings.
        /// </summary>
        internal object UpdateProperty(string propertyName, object publicValue, object privateValue)
        {
            var finalResult = publicValue;
            if (!EqualityComparer<object>.Default.Equals(publicValue, privateValue))
            {
                // Get the direct parameter value
                var directParam = directParameters.TryGetValue(propertyName, out object? value) ? value : publicValue;
                var isPropertyBinding = !SfBaseUtils.Equals<object>(publicValue, directParam) && IsRerendering;

                // Validate and assign public or private values to the property based on changes
                finalResult = (isPropertyBinding || !this.IsRendered) ? publicValue : privateValue;


                if (isPropertyBinding)
                {
                    directParameters[propertyName] = finalResult;
                    SfBaseUtils.UpdateDictionary(propertyName, finalResult, PropertyChanges);
                }
            }

            return finalResult;
        }

        internal static string GetLocalizedNumber(int value)
        {
            var localizedValue = Intl.GetNumericFormat<int>(value, "n0");
            return localizedValue.ToString();
        }
        private void ResetVisibleNumericRange()
        {
            visibleNumericStartIndex = 0;
            visibleNumericEndIndex = 0;
            hasVisibleNumericRange = false;
        }

        private void TrackVisibleItemCount(int value)
        {
            if (visibleItemCountHistory.Length == 0)
            {
                return;
            }

            if (visibleItemCountHistoryCount < visibleItemCountHistory.Length)
            {
                visibleItemCountHistory[visibleItemCountHistoryCount++] = value;
                return;
            }

            Array.Copy(visibleItemCountHistory, 1, visibleItemCountHistory, 0, visibleItemCountHistory.Length - 1);
            visibleItemCountHistory[visibleItemCountHistory.Length - 1] = value;
        }

        private int GetRenderStartIndex()
        {
            if (hasVisibleNumericRange && visibleNumericStartIndex > 0)
            {
                return visibleNumericStartIndex;
            }

            return pageStart;
        }

        private int GetRenderEndIndex()
        {
            if (hasVisibleNumericRange && visibleNumericEndIndex >= visibleNumericStartIndex)
            {
                return Math.Min(visibleNumericEndIndex, TotalPages);
            }

            return Math.Min(pageStart + numericCount, TotalPages);
        }

        internal class PagerDropdownModel
        {
            public string? DisplayText { get; set; }
            public int Value { get; set; }
        }
        private async Task InvokeStateChange()
        {
            await OnParametersSetAsync().ConfigureAwait(true);
            await InvokeAsync(() => StateHasChanged()).ConfigureAwait(true);
        }

        private async Task NavigateToPage(int pageNo, bool invokedViaMethod = false)
        {
            if (pageNo < 1 || pageNo > TotalPages)
            {
                return;
            }
            int prevNo = CurrentPage;
            var args = new PageChangingEventArgs() { CurrentPage = pageNo, PreviousPage = prevNo, Cancel = false };
            if (PageChanging.HasDelegate)
            {
                await PageChanging.InvokeAsync(args).ConfigureAwait(true);
                if (args.Cancel)
                {
                    return;
                }
            }
            UpdatePagerProperties("CurrentPage", pageNo);
            if (!invokedViaMethod)
            {
                currentFocus = pageNo.ToString(CultureInfo.CurrentCulture);
                if (CurrentFocus != currentFocus)
                {
                    CurrentFocus = currentFocus;
                    isCurrentFocusChanged = true;
                }
            }

            EnsureCurrentPage();
            pageStart = GetPageStart();
            EnsurePagerSets();
            if (!SkipPage(prevNo))
            {
                var changedArgs = new PageChangedEventArgs() { CurrentPage = pageNo, PreviousPage = prevNo };
                if (PageChanged.HasDelegate)
                {
                    await PageChanged.InvokeAsync(changedArgs).ConfigureAwait(true);
                }
            }
            await InvokeStateChange().ConfigureAwait(true);
        }

        internal int EnsurePagerTabIndex()
        {
            return TotalItemsCount == 0 && TotalPages == 0 || Template != null ? -1 : 0;
        }


        private void BlurHandler()
        {
            if (!isCurrentFocusChanged)
            {
                CurrentFocus = "-1";
            }
        }

        internal async void ClientFocus(string direction, bool enablePrev, bool enableNext)
        {
            if (direction.Equals("PreviousPage", StringComparison.Ordinal) && !enablePrev)
            {
                await InvokeMethod("sfBlazor.Pager.currentPageFocus", new object[] { DataId, "PreviousPage", CurrentPage }).ConfigureAwait(true);
            }
            if (direction.Equals("NextPage", StringComparison.Ordinal) && !enableNext)
            {
                await InvokeMethod("sfBlazor.Pager.currentPageFocus", new object[] { DataId, "NextPage", CurrentPage }).ConfigureAwait(true);
            }
        }

        internal void EnsureFocus(string direction, bool enablePrev, bool enableNext)
        {
            if (direction.Equals("PreviousPage", StringComparison.Ordinal) && !enablePrev)
            {
                CurrentFocus = CurrentPage.ToString(CultureInfo.InvariantCulture);
            }
            if (direction.Equals("NextPage", StringComparison.Ordinal) && !enableNext)
            {
                CurrentFocus = CurrentPage.ToString(CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Called from JavaScript to set the calculated numeric items count for ellipsis navigation.
        /// This method is invoked after the JS calculates available width for numeric items.
        /// </summary>
        /// <param name="numericItemsCount">The number of numeric items that can fit in the available space.</param>
        //public void SetCalculatedNumericItemsForEllipsis(int numericItemsCount)
        //{
        //    calculatedNumericItemsForEllipsis = numericItemsCount;
        //}

        internal void EnsurePagerSets()
        {
            // Use calculated numeric items count if available (from ellipsis navigation)
            // Otherwise use the NumericItemsCount property
            int effectiveNumericItemsCount = calculatedNumericItemsForEllipsis > 0
                ? calculatedNumericItemsForEllipsis
                : NumericItemsCount;

            numericCount = (pageStart + effectiveNumericItemsCount - 1) > TotalPages
                ? (TotalPages - pageStart)
                : effectiveNumericItemsCount - 1;

            enablePrevSet = enablePrev = TotalPages != 0 && CurrentPage != 1;
            enableNextSet = enableNext = TotalPages != 0 && CurrentPage != TotalPages;
            enableLeftDots = TotalPages != 0 && GetRenderStartIndex() > 1;
            enableRightDots = TotalPages != 0 && GetRenderEndIndex() < TotalPages;

            // Reset the calculated count after use
            calculatedNumericItemsForEllipsis = 0;

            //if (TotalPages != 0 && visibleNumericEndIndex == TotalPages)
            //{
            //    enableNextSet = enableNext = false;
            //}
        }

        internal void EnsureDropdown()
        {
            if (ShowAllInPageSizes && dropdownData != null && TotalItemsCount != 0)
            {
                var pagerAllDropdownData = dropdownData.FirstOrDefault(x => x.DisplayText == _localizer.GetText("Pager_All"));
                if (pagerAllDropdownData != null)
                {
                    pagerAllDropdownData.Value = TotalItemsCount;
                }

            }
            if (IsRendered && dropdownValue == PageSize || string.Equals(dropdownValue.ToString(CultureInfo.InvariantCulture), PageSize.ToString(CultureInfo.InvariantCulture), StringComparison.Ordinal)) { return; }
            if (PageSizes == null) { return; }
            if (!IsRendered || (IsRenderedFromTreeGrid && dropdownData == null))
            {
                dropdownData = new List<PagerDropdownModel>();
                foreach (var item in PageSizes)
                {
                    dropdownData.Add(new PagerDropdownModel() { Value = item, DisplayText = GetLocalizedNumber(item) });
                }
                if (ShowAllInPageSizes)
                {
                    dropdownData.Add(new PagerDropdownModel() { Value = TotalItemsCount, DisplayText = _localizer.GetText("Pager_All") });
                }
            }
            showDropdown = true;
            if (dropdownData != null && dropdownData.Any(x => x.Value == PageSize))
            {
                dropdownValue = PageSize;
            }
            else if (PageSize == TotalItemsCount && dropdownData != null && dropdownData.Any(x => x.DisplayText == _localizer.GetText("Pager_All")))
            {
                dropdownValue = 0;
            }
            else
            {
                dropdownValue = -1; // set -1 if PageSize not matching in the dropdownData list(Custom PagesSizes)
            }
        }
        internal async Task PageSizeChangeHandler(ChangeEventArgs<int, PagerDropdownModel> changeEventArgs)
        {
            await UpdatePageSizeFromDropdownValue(changeEventArgs.Value).ConfigureAwait(true);
        }

        private async Task UpdatePageSizeFromDropdownValue(object updateDropdownValue)
        {
            int value = !int.TryParse(updateDropdownValue?.ToString(), out value) ? 0 : value;
            if (PageSizeChanging.HasDelegate)
            {
                string selectedPageSize = value == 0 ? _localizer.GetText("Pager_All") : value.ToString(CultureInfo.InvariantCulture);
                var tempTotalPages = (int)(Math.Ceiling((double)(TotalItemsCount / (double)value)));
                var args = new PageSizeChangingArgs() { Cancel = false, PreviousPageSize = PageSize, TotalPages = value == 0 ? 1 : tempTotalPages, SelectedPageSize = selectedPageSize };
                _isPreventRender = true;
                await PageSizeChanging.InvokeAsync(args).ConfigureAwait(true);
                if (args.Cancel)
                {
                    return;
                }
            }
            if (value == 0 && dropdownData != null && dropdownData.Any(x => x.Value != PageSize))
            {
                UpdatePagerProperties("PageSize", PageSize);
            }
            else
            {
                UpdatePagerProperties("PageSize", value);
            }
            //Setting calculated properties
            if (TotalItemsCount > 0)
            {
                TotalPages = (int)(Math.Ceiling((double)(TotalItemsCount / (double)PageSize)));
                CurrentPage = CurrentPage > TotalPages ? TotalPages : CurrentPage;
                pageStart = GetPageStart();
                EnsurePagerSets();
            }
            _isPreventRender = false;
            if (PageSizeChanged.HasDelegate)
                await PageSizeChanged.InvokeAsync(new PageSizeChangedArgs() { CurrentPageSize = PageSize, TotalPages = TotalPages, CurrentPage = CurrentPage }).ConfigureAwait(true);
        }


        internal int GetPageStart()
        {
            if (CurrentPage >= pageStart && CurrentPage < pageStart + NumericItemsCount)
            {
                return pageStart;
            }

            var dividend = (double)((CurrentPage - 1)) / (double)(NumericItemsCount);
            if (dividend == 1 || dividend == 0)
            {
                return CurrentPage;
            }

            var start = CurrentPage;
            while ((start - 1) % NumericItemsCount != 0)
            {
                start--;
            }

            return start;
        }

        private int GetPageStartForNavigation(string direction)
        {
            var visibleItemsCount = hasVisibleNumericRange && visibleNumericEndIndex >= visibleNumericStartIndex
                ? visibleNumericEndIndex - visibleNumericStartIndex + 1
                : numericCount + 1;

            if (direction.Equals("NextPage", StringComparison.Ordinal))
            {
                if (hasVisibleNumericRange && visibleNumericEndIndex > 0 && CurrentPage > visibleNumericEndIndex)
                {
                    return Math.Min(CurrentPage, TotalPages);
                }

                if (CurrentPage > pageStart + numericCount)
                {
                    return Math.Min(CurrentPage, TotalPages);
                }
            }
            else if (direction.Equals("PreviousPage", StringComparison.Ordinal))
            {
                if (hasVisibleNumericRange && visibleNumericStartIndex > 0 && CurrentPage < visibleNumericStartIndex)
                {
                    return Math.Max(pageStart - visibleItemsCount, 1);
                }

                if (CurrentPage < pageStart)
                {
                    return Math.Max(pageStart - visibleItemsCount, 1);
                }
            }

            return pageStart;
        }

        internal bool SkipPage(int prevPage) => prevPage == CurrentPage;

        internal void EnsureCurrentPage()
            => CurrentPage = CurrentPage < 1 ? 1 : CurrentPage > TotalPages ? TotalPages : CurrentPage;

        internal async Task ItemClickHandler(MouseEventArgs clickArgs, int activePage)
        {
            var prevPage = CurrentPage;
            if (ItemClick.HasDelegate && clickArgs != null)
            {
                await ItemClick.InvokeAsync(new PagerItemClickEventArgs() { CurrentPage = activePage, PreviousPage = prevPage }).ConfigureAwait(true);
            }
            var args = new PageChangingEventArgs() { CurrentPage = activePage, PreviousPage = prevPage, Cancel = false };
            if (PageChanging.HasDelegate)
            {
                await PageChanging.InvokeAsync(args).ConfigureAwait(true);
                if (args.Cancel)
                {
                    return;
                }
            }
            CurrentPage = _currentPage = activePage;
            CurrentFocus = CurrentPage.ToString(CultureInfo.InvariantCulture);
            enablePrevSet = enablePrev = CurrentPage != 1;
            enableNextSet = enableNext = CurrentPage != TotalPages;
            suppressFocus = false;
            await InvokeStateChange().ConfigureAwait(true);
            if (!SkipPage(prevPage))
            {
                var changedArgs = new PageChangedEventArgs() { CurrentPage = CurrentPage, PreviousPage = prevPage };
                if (PageChanged.HasDelegate)
                {
                    await PageChanged.InvokeAsync(changedArgs).ConfigureAwait(true);
                }
            }
        }

        internal async Task PreviousNextIconClickHandler(string direction, MouseEventArgs? clickArgs = null)
        {
            if ((!enableNextSet && direction.Equals("NextPage", StringComparison.Ordinal)) || (!enablePrevSet && direction.Equals("PreviousPage", StringComparison.Ordinal)))
            {
                return;
            }

            var prevPage = CurrentPage;
            var currentPage = direction.Equals("PreviousPage", StringComparison.Ordinal) ? (CurrentPage - 1) : (CurrentPage + 1);
            if (ItemClick.HasDelegate && clickArgs != null)
            {
                await ItemClick.InvokeAsync(new PagerItemClickEventArgs() { CurrentPage = currentPage, PreviousPage = prevPage }).ConfigureAwait(true);
            }
            var args = new PageChangingEventArgs() { CurrentPage = currentPage, PreviousPage = prevPage, Cancel = false };
            if (PageChanging.HasDelegate)
            {
                await PageChanging.InvokeAsync(args).ConfigureAwait(true);
                if (args.Cancel)
                {
                    return;
                }
            }
            if (direction.Equals("PreviousPage", StringComparison.Ordinal))
            {
                CurrentPage = _currentPage -= 1;
                CurrentFocus = "PreviousPage";
            }
            if (direction.Equals("NextPage", StringComparison.Ordinal))
            {
                CurrentPage = _currentPage += 1;
                CurrentFocus = "NextPage";
            }
            EnsureCurrentPage();
            var previousPageStart = pageStart;
            pageStart = GetPageStartForNavigation(direction);
            if (pageStart != previousPageStart)
            {
                int visibleItemCount = 0;
                if (hasVisibleNumericRange && visibleNumericEndIndex >= visibleNumericStartIndex)
                {
                    visibleItemCount = visibleNumericEndIndex - visibleNumericStartIndex + 1;
                }

                TrackVisibleItemCount(visibleItemCount);
                if (TotalPages == visibleNumericEndIndex)
                {
                    visibleItemCountHistory.Max();
                    visibleItemCount = visibleItemCountHistory.Max();
                }
                if (visibleItemCount > 0)
                {
                    if (direction == "PreviousPage")
                    {
                        visibleNumericStartIndex = Math.Abs(visibleNumericStartIndex - visibleItemCount);
                        visibleNumericEndIndex = Math.Min(visibleNumericStartIndex + visibleItemCount - 1, TotalPages);
                        hasVisibleNumericRange = true;
                        pageStart = visibleNumericStartIndex;
                    }
                    else if (direction == "NextPage")
                    {
                        visibleNumericStartIndex = pageStart;
                        visibleNumericEndIndex = Math.Min(pageStart + visibleItemCount - 1, TotalPages);
                        hasVisibleNumericRange = true;
                        pageStart = visibleNumericStartIndex;
                    }

                }
                else
                {
                    ResetVisibleNumericRange();
                }
            }
            EnsurePagerSets();
            EnsureFocus(direction, enablePrevSet, enableNextSet);
            if (!SkipPage(prevPage))
            {
                var changedArgs = new PageChangedEventArgs() { CurrentPage = CurrentPage, PreviousPage = prevPage };
                if (PageChanged.HasDelegate)
                {
                    await PageChanged.InvokeAsync(changedArgs).ConfigureAwait(true);
                }
            }
            ClientFocus(direction, enablePrevSet, enableNextSet);
            isPerPageNeeded = true;
        }

        internal async Task FirstLastIconClickHandler(string direction, MouseEventArgs? clickArgs = null)
        {
            if ((!enableNextSet && direction.Equals("NextPage", StringComparison.Ordinal)) || (!enablePrevSet && direction.Equals("PreviousPage", StringComparison.Ordinal)))
            {
                return;
            }
            var prevPage = CurrentPage;
            int visibleItemCount = 0;
            if (hasVisibleNumericRange && visibleNumericEndIndex >= visibleNumericStartIndex)
            {
                visibleItemCount = visibleNumericEndIndex - visibleNumericStartIndex + 1;
            }

            TrackVisibleItemCount(visibleItemCount);

            var currentPage = direction.Equals("PreviousPage", StringComparison.Ordinal) ? 1 : TotalPages;
            if (ItemClick.HasDelegate && clickArgs != null)
            {
                await ItemClick.InvokeAsync(new PagerItemClickEventArgs() { CurrentPage = currentPage, PreviousPage = prevPage }).ConfigureAwait(true);
            }
            var args = new PageChangingEventArgs() { CurrentPage = currentPage, PreviousPage = prevPage, Cancel = false };
            if (PageChanging.HasDelegate)
            {
                await PageChanging.InvokeAsync(args).ConfigureAwait(true);
                if (args.Cancel)
                {
                    return;
                }
            }
            if (direction.Equals("PreviousPage", StringComparison.Ordinal))
            {
                pageStart = 1;
                CurrentPage = _currentPage = 1;
                currentFocus = CurrentFocus = "FirstPage";
            }
            if (direction.Equals("NextPage", StringComparison.Ordinal))
            {
                CurrentPage = _currentPage = TotalPages;
                pageStart = GetPageStart();
                currentFocus = CurrentFocus = "LastPage";
            }
            if (TotalPages == visibleNumericEndIndex)
            {
                visibleItemCountHistory.Max();
                visibleItemCount = visibleItemCountHistory.Max();
            }
            if (visibleItemCount > 0)
            {
                if (direction == "PreviousPage")
                {
                    visibleNumericStartIndex = pageStart;
                    visibleNumericEndIndex = Math.Min(visibleNumericStartIndex + visibleItemCount - 1, TotalPages);
                    hasVisibleNumericRange = true;
                    pageStart = visibleNumericStartIndex;
                }
                else if (direction == "NextPage")
                {
                    //visibleItemCount = visibleItemCountHistory.Min();
                    visibleNumericStartIndex = TotalPages - visibleItemCount + 1;
                    visibleNumericEndIndex = TotalPages;
                    hasVisibleNumericRange = true;
                    pageStart = visibleNumericStartIndex;
                }

            }
            isCurrentFocusChanged = prevPage != CurrentPage;
            EnsureCurrentPage();
            EnsurePagerSets();
            EnsureFocus(direction, enablePrevSet, enableNextSet);
            if (!SkipPage(prevPage))
            {
                var changedArgs = new PageChangedEventArgs() { CurrentPage = CurrentPage, PreviousPage = prevPage };
                if (PageChanged.HasDelegate)
                {
                    await PageChanged.InvokeAsync(changedArgs).ConfigureAwait(true);
                }
            }
            isPerPageNeeded = true;
        }

        internal async Task EllipsisButtonClickHandler(string direction, MouseEventArgs? eventArgs = null)
        {
            var prevPage = CurrentPage;
            int visibleItemCount = 0;
            if (hasVisibleNumericRange && visibleNumericEndIndex >= visibleNumericStartIndex)
            {
                visibleItemCount = visibleNumericEndIndex - visibleNumericStartIndex + 1;
            }

            TrackVisibleItemCount(visibleItemCount);
            if (TotalPages == visibleNumericEndIndex)
            {
                visibleItemCountHistory.Max();
                visibleItemCount = visibleItemCountHistory.Max();
            }
            // Calculate pageStart for the next block based on currently visible range
            int nextPageStart;
            if (direction == "PreviousPage")
            {
                nextPageStart = (pageStart - NumericItemsCount) <= 0 ? 1 : (pageStart - NumericItemsCount);
                if (pageStart - visibleItemCount >= 0)
                {
                    nextPageStart = Math.Abs(pageStart - visibleItemCount);
                }
            }
            else
            {
                if (hasVisibleNumericRange && visibleNumericEndIndex > 0)
                {
                    // The next block should start right after the last visible page
                    nextPageStart = visibleNumericEndIndex + 1;
                }
                else
                {
                    // Fallback: if no visible range info yet, calculate from pageStart + visible count
                    // numericCount represents how many items are currently visible based on digit width
                    nextPageStart = (pageStart + numericCount) > TotalPages ? TotalPages : (pageStart + numericCount);
                }
            }

            // Ensure nextPageStart doesn't exceed TotalPages
            nextPageStart = Math.Min(Math.Max(1, nextPageStart), TotalPages);
            // The currentPage to navigate to should be within the new block
            var currentPage = nextPageStart;

            if (ItemClick.HasDelegate && eventArgs != null)
            {
                await ItemClick.InvokeAsync(new PagerItemClickEventArgs() { CurrentPage = currentPage, PreviousPage = prevPage }).ConfigureAwait(true);
            }
            var args = new PageChangingEventArgs() { CurrentPage = currentPage, PreviousPage = prevPage, Cancel = false };
            if (PageChanging.HasDelegate)
            {
                await PageChanging.InvokeAsync(args).ConfigureAwait(true);
                if (args.Cancel)
                {
                    return;
                }
            }
            CurrentPage = _currentPage = currentPage;
            EnsureCurrentPage();

            // CRITICAL: Set pageStart directly to nextPageStart instead of recalculating via GetPageStart()
            // This ensures we render from the first hidden page (e.g., page 6) not from CurrentPage alignment
            pageStart = nextPageStart;
            if (visibleItemCount > 0)
            {
                visibleNumericStartIndex = nextPageStart;
                visibleNumericEndIndex = Math.Min(nextPageStart + visibleItemCount - 1, TotalPages);
                hasVisibleNumericRange = true;
            }
            else
            {
                visibleNumericStartIndex = 0;
                visibleNumericEndIndex = 0;
                hasVisibleNumericRange = false;
            }

            isCurrentFocusChanged = (prevPage != currentPage);
            if (direction == "PreviousPage")
            {
                currentFocus = CurrentFocus = "PreviousPagerCount";
            }
            if (direction == "NextPage")
            {
                currentFocus = CurrentFocus = "NextPagerCount";
            }

            EnsurePagerSets();
            EnsureFocus(direction, enableLeftDots, enableRightDots);
            if (!SkipPage(prevPage))
            {
                var changedArgs = new PageChangedEventArgs() { CurrentPage = CurrentPage, PreviousPage = prevPage };
                if (PageChanged.HasDelegate)
                {
                    await PageChanged.InvokeAsync(changedArgs).ConfigureAwait(true);
                }
            }
            isPerPageNeeded = true;
        }

        //internal async Task EllipsisButtonClickHandler(string direction, MouseEventArgs? eventArgs = null)
        //{
        //    var prevPage = CurrentPage;
        //    var currentPage = direction == "PreviousPage" ? ((pageStart - NumericItemsCount) <= 0 ? 1 : pageStart - NumericItemsCount) : ((pageStart + NumericItemsCount) > TotalPages ? TotalPages : pageStart + NumericItemsCount);
        //    if (ItemClick.HasDelegate && eventArgs != null)
        //    {
        //        await ItemClick.InvokeAsync(new PagerItemClickEventArgs() { CurrentPage = currentPage, PreviousPage = prevPage }).ConfigureAwait(true);
        //    }
        //    var args = new PageChangingEventArgs() { CurrentPage = currentPage, PreviousPage = prevPage, Cancel = false };
        //    if (PageChanging.HasDelegate)
        //    {
        //        await PageChanging.InvokeAsync(args).ConfigureAwait(true);
        //        if (args.Cancel)
        //        {
        //            return;
        //        }
        //    }
        //    if (direction == "PreviousPage")
        //    {
        //        pageStart = ((pageStart - NumericItemsCount) <= 0 ? 1 : pageStart - NumericItemsCount);
        //        currentFocus = CurrentFocus = "PreviousPagerCount";
        //    }
        //    if (direction == "NextPage")
        //    {
        //        pageStart = ((pageStart + NumericItemsCount) > TotalPages ? pageStart : pageStart + NumericItemsCount);
        //        currentFocus = CurrentFocus = "NextPagerCount";
        //    }
        //    isCurrentFocusChanged = (CurrentPage != currentPage);
        //    CurrentPage = _currentPage = currentPage;
        //    EnsureCurrentPage();
        //    EnsurePagerSets();
        //    EnsureFocus(direction, enableLeftDots, enableRightDots);
        //    if (!SkipPage(prevPage))
        //    {
        //        var changedArgs = new PageChangedEventArgs() { CurrentPage = CurrentPage, PreviousPage = prevPage };
        //        if (PageChanged.HasDelegate)
        //        {
        //            await PageChanged.InvokeAsync(changedArgs).ConfigureAwait(true);
        //        }
        //    }
        //}

        internal string GetClassNames()
        {
            string classNames = "sf-pager e-control e-pager e-lib";

            if (EnableRtl || SyncfusionService.options.EnableRtl)
            {
                classNames = $"{classNames} e-rtl";
            }
            if (!string.IsNullOrEmpty(CssClass))
            {
                classNames = classNames.Contains(CssClass, StringComparison.Ordinal) ? classNames : SfBaseUtils.AddClass(classNames, CssClass);
            }
            return classNames;
        }

        internal void UpdatePagerProperties(string key, object value)
        {
            if (key == nameof(TotalPages))
            {
                TotalPages = (int)value;
            }
            else if (key == nameof(CurrentPage))
            {
                CurrentPage = _currentPage = (int)value;
            }
            else if (key == nameof(PageSize))
            {
                PageSize = _pageSize = (int)value;
            }
            else if (key == nameof(NumericItemsCount))
            {
                NumericItemsCount = _numericItemsCount = (int)value;
            }
            else if (key == nameof(ExternalMessage))
            {
                ExternalMessage = (string)value;
            }
        }

        internal static string SerializeModel(SfPager comp)
        {
            IDictionary<string, object> model = new Dictionary<string, object>()
            {
                { "currentPage", comp.CurrentPage }, { "currentFocus", comp.CurrentFocus },
                { "totalItemCount", comp.TotalItemsCount }, { "numericItemsCount", comp.NumericItemsCount },
                { "pageSize", comp.PageSize },{"dropdownValue", comp.dropdownValue}
            };
            return JsonSerializer.Serialize(model, _jsonSettings);
        }


        internal async Task SetLocalStorage() => await InvokeMethod("window.localStorage.setItem", new object[] { ID!, SerializeModel(this) }).ConfigureAwait(true);

        internal void PersistProperties(string properties)
        {
            var PersistProp = JsonSerializer.Deserialize<Dictionary<string, object>>(properties.ToString(), _persistJsonSettings);
            if (PersistProp != null)
            {

                UpdatePagerProperties("CurrentPage", PersistProp["currentPage"]);
                UpdatePagerProperties("NumericItemsCount", PersistProp["numericItemsCount"]);
                UpdatePagerProperties("PageSize", PersistProp["pageSize"]);

                CurrentFocus = PersistProp["currentFocus"].ToString() ?? string.Empty;
                TotalItemsCount = (int)PersistProp["totalItemCount"];
                PageSizes = PersistProp.TryGetValue("pageSizes", out var pageSizesValue) ? pageSizesValue as List<int> ?? null! : null!;
                dropdownValue = (int)PersistProp["dropdownValue"];
            }
        }

        /// <summary>
        /// Handles keydown events on the Go to Last Page button to set the current focus and trigger navigation.
        /// </summary>
        internal async Task HandleLastPageKeyDown(KeyboardEventArgs e)
        {
            var keyCombination = e.GetKeyCombination();
            if (keyCombination == enterKey || keyCombination == spaceKey)
            {
                CurrentFocus = "LastPage";
                await FirstLastIconClickHandler(nextPage).ConfigureAwait(true);
            }
        }

        internal async Task ProcessPagerKeyDown(KeyboardEventArgs e, int activePage = 0, string action = "")
        {
            suppressFocus = false;
            var keyCombination = e.GetKeyCombination();
            if (keyCombination == tabKey || keyCombination == shiftTabKey)
            {
                await TabShiftKeyCombination(keyCombination).ConfigureAwait(true);
            }
            if (keyCombination == enterKey || keyCombination == spaceKey)
            {
                await EnterSpaceKeyAction().ConfigureAwait(true);
            }
            else if (!e.ShiftKey)
            {
                switch (keyCombination)
                {
                    case arrowRightKey:
                    case pageDownKey:
                        await GoToNextPageAsync().ConfigureAwait(true);
                        break;
                    case arrowLeftKey:
                    case previousPage:
                    case pageUpKey:
                        await GoToPreviousPageAsync().ConfigureAwait(true);
                        break;
                    case homeKey:
                    case ctrlAltPageUpKey:
                        await GoToFirstPageAsync().ConfigureAwait(true);
                        break;
                    case endKey:
                    case ctrlAltPageDownKey:
                        await GoToLastPageAsync().ConfigureAwait(true);
                        break;
                    case altPageUpKey:
                        await EllipsisButtonClickHandler(previousPage).ConfigureAwait(true);
                        break;
                    case altPageDownKey:
                        await EllipsisButtonClickHandler(nextPage).ConfigureAwait(true);
                        break;
                }
            }
        }

        private async Task TabShiftKeyCombination(string key)
        {

            string currentFocus = await InvokeMethod<string>("sfBlazor.Pager.pagerFocus", false, new object[] { DataId, key }).ConfigureAwait(true);
            if (!string.IsNullOrEmpty(currentFocus))
            {
                CurrentFocus = !string.IsNullOrEmpty(NumericItemPrefix) ? currentFocus.Split(NumericItemPrefix).Last() : currentFocus;
                await InvokeStateChange().ConfigureAwait(true);
            }
        }
        private async Task EnterSpaceKeyAction()
        {

            if (int.TryParse(CurrentFocus, out int CurrentPageFocus))
            {
                currentPage = CurrentPageFocus;
                await NavigateToPage(currentPage).ConfigureAwait(true);
            }
            else if (CurrentFocus == firstPage)
            {
                await FirstLastIconClickHandler(previousPage).ConfigureAwait(true);
            }
            else if (CurrentFocus == previousPage)
            {
                await PreviousNextIconClickHandler(previousPage).ConfigureAwait(true);
            }
            else if (CurrentFocus == nextPage)
            {
                await PreviousNextIconClickHandler(nextPage).ConfigureAwait(true);
            }
            else if (CurrentFocus == lastPage)
            {
                await FirstLastIconClickHandler(nextPage).ConfigureAwait(true);
            }
            else if (CurrentFocus == previousPagerCount)
            {
                await EllipsisButtonClickHandler(previousPage).ConfigureAwait(true);
            }
            else if (CurrentFocus == nextPagerCount)
            {
                await EllipsisButtonClickHandler(nextPage).ConfigureAwait(true);
            }
        }

        internal override void ComponentDispose()
        {
            if (IsRendered)
            {
                InvokeMethod("sfBlazor.Pager.destroy", new object[] { DataId }).ContinueWith(t => { }, TaskScheduler.Current);
            }
            _dotnetRef?.Dispose();
        }


        /// <summary>
        /// Updates the adaptive pager message state based on device type and refreshes the component.
        /// </summary>
        /// <param name="args">True if the device is mobile; otherwise, false.</param>
        [JSInvokable]
        public void UpdateVisibleNumericRange(int startIndex, int endIndex)
        {
            if (startIndex <= 0 || endIndex <= 0 || startIndex > endIndex)
            {
                return;
            }

            if (startIndex == 1 && endIndex >= TotalPages)
            {
                return;
            }

            if (hasVisibleNumericRange && visibleNumericStartIndex == startIndex && visibleNumericEndIndex == endIndex)
            {
                return;
            }

            visibleNumericStartIndex = startIndex;
            visibleNumericEndIndex = Math.Min(endIndex, TotalPages);
            hasVisibleNumericRange = true;
            StateHasChanged();
        }

        [JSInvokable]
        public void IsMobileDevice(bool args)
        {
            adaptivePagerMessage = args;
            StateHasChanged();
        }
    }
}
