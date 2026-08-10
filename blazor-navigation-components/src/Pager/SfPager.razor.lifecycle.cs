using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Internal;
using Syncfusion.Blazor.Navigations.Internal;

namespace Syncfusion.Blazor.Navigations
{

    public partial class SfPager : SfBaseComponent
    {
        /// <summary>
        /// Method invoked when the component is ready to start.
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(true);
            this._currentPage = this.CurrentPage;
            this._numericItemPrefix = this.NumericItemPrefix;            
            this._showPagerMessage = this.ShowPagerMessage;
            this._enablePersistence = this.EnablePersistence;
            //this._enableQueryString = this.EnableQueryString;            
            this._numericItemsCount = this.NumericItemsCount;
            this._pageSize = this.PageSize;
            this._pageSizes = this.PageSizes;           
            this._totalItemsCount = this.TotalItemsCount;
            this._cssClass = this.CssClass;
            ScriptModules = SfScriptModules.SfPager;

        }

        /// <summary>
        /// Sets parameters supplied by the component's parent in the render tree.
        /// </summary>
        /// <param name="parameters.">The component parameters..</param>
        public override Task SetParametersAsync(ParameterView parameters)
        {
            parameters.SetParameterProperties(this);
            if (directParamKeys.Count == 0)
            {
                foreach (var parameter in parameters)
                {
                    if (!parameter.Cascading)
                    {
                        directParamKeys.Add(parameter.Name);
                    }
                }
            }

            return base.SetParametersAsync(parameters);
        }

        /// <summary>
        /// Method invoked when the component has received parameters from its parent in the render tree.
        /// </summary>
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync().ConfigureAwait(true);

            if (!SfBaseUtils.Equals(CurrentPage, _currentPage) || !SfBaseUtils.Equals(NumericItemPrefix, _numericItemPrefix)
                || !SfBaseUtils.Equals(ShowPagerMessage, _showPagerMessage)
                || !SfBaseUtils.Equals(EnablePersistence, _enablePersistence)                
                || !SfBaseUtils.Equals(NumericItemsCount, _numericItemsCount)
                || !SfBaseUtils.Equals(PageSizes, _pageSizes)                
                || !SfBaseUtils.Equals(PageSize, _pageSize))
            {
                _currentPage = CurrentPage;
                _numericItemPrefix = NumericItemPrefix;                
                _showPagerMessage = ShowPagerMessage;
                _enablePersistence = EnablePersistence;
                //_enableQueryString = EnableQueryString;             
                _numericItemsCount = NumericItemsCount;
                if (isRenderedFromGrid && PageSize != _pageSize)
                {
                    _pageSize = PageSize;
                }
                else
                {
                    _pageSize = PageSize = (int)this.UpdateProperty(nameof(PageSize), PageSize, _pageSize);
                }
                _pageSizes = PageSizes;                
            }
            //Setting calculated properties
            TotalPages = (TotalItemsCount % PageSize == 0) ? (TotalItemsCount / PageSize) :
                  (int)(Math.Ceiling((double)(TotalItemsCount / (double)PageSize)));
            pageStart = GetPageStart();
            if (!_isPreventRender)
            {
                EnsurePagerSets();
                EnsureDropdown();
            }
        }

        /// <summary>
        /// Method invoked after each time the component has been rendered.
        /// </summary>
        /// <param name="firstRender">Set to true for the first time component rendering; otherwise gets false.</param>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender).ConfigureAwait(true);
            if (firstRender)
            {
                // Added direct parameters from directParamKeys
                foreach (var key in directParamKeys)
                {
                    directParameters = directParameters == null ? new Dictionary<string, object>() : directParameters;
                    var initValue = GetType().GetProperty(key)?.GetValue(this);
                    SfBaseUtils.UpdateDictionary(key, initValue, directParameters);
                }
            }

            if (this.Template == null && isCurrentFocusChanged && currentFocus != null)
            {
                isCurrentFocusChanged = false;
                await InvokeMethod("sfBlazor.Pager.currentPageFocus", new object[] { DataId, currentFocus }).ConfigureAwait(true);
            }
            if (firstRender && Created.HasDelegate)
            {
                await Created.InvokeAsync(null).ConfigureAwait(true);
            }
            if (firstRender && PageSizes != null)
            {
                await InvokeMethod<string>("sfBlazor.Pager.refresh", false, new object[] { DataId }).ConfigureAwait(true);
            }
            if (isPerPageNeeded)
            {
                await InvokeMethod<string>("sfBlazor.Pager.setPageSizeState", false, new object[] { DataId }).ConfigureAwait(true);
                isPerPageNeeded = false;
            }
            if (EnablePersistence)
            {
                await SetLocalStorage().ConfigureAwait(true);
            }
            IsRerendering = true;
        }


        /// <summary>
        /// Determines whether the component should re-render.
        /// </summary>
        /// <returns>True if the component should render; otherwise, false.</returns>
        protected override bool ShouldRender()
        {
            return !_isPreventRender;
        }

        internal override async Task OnAfterScriptRendered()
        {
            Init();
            await InvokeMethod("sfBlazor.Pager.initialize", new object[] { DataId, element, _dotnetRef! }).ConfigureAwait(true);
            if (EnablePersistence)
            {
                var localStorageValue = await InvokeMethod<string>("window.localStorage.getItem", false, new object[] { ID! }).ConfigureAwait(true);
                if (localStorageValue != null)
                {
                    PersistProperties(localStorageValue);
                    await InvokeStateChange().ConfigureAwait(true);
                }
            }
        }

    }

}