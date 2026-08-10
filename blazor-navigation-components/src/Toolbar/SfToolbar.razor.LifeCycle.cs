using System.Threading.Tasks;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Internal;
using System;

namespace Syncfusion.Blazor.Navigations
{
    public partial class SfToolbar : SfBaseComponent
    {
        /// <summary>
        /// Method invoked when the component is ready to start.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        protected override async Task OnInitializedAsync()
        {
            ScriptModules = SfScriptModules.SfToolbar;
            UpdateLocalProperties();
            await base.OnInitializedAsync().ConfigureAwait(true);
            allowKeyboard = AllowKeyboard;
            cssClass = CssClass;
            enableCollision = EnableCollision;
            enableRtl = EnableRtl;
            height = Height;
            overflowMode = OverflowMode;
            scrollStep = ScrollStep;
            width = Width;
            if (OverflowMode == OverflowMode.MultiRow)
            {
                IsInitialModeMultiRow = true;
            }
        }

        /// <summary>
        /// Method invoked when the component has received parameters from its parent in the render tree,
        /// and the incoming values have been assigned to properties.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync().ConfigureAwait(true);
            allowKeyboard = NotifyPropertyChanges(ALLOWKEYBOARD, AllowKeyboard, allowKeyboard);
            cssClass = NotifyPropertyChanges(CSSCLASS, CssClass, cssClass);
            enableCollision = NotifyPropertyChanges(ENABLECOLLISION, EnableCollision, enableCollision);
            enableRtl = NotifyPropertyChanges(ENABLERTL, EnableRtl, enableRtl);
            height = NotifyPropertyChanges(HEIGHT, Height, height);
            overflowMode = NotifyPropertyChanges(OVERFLOWMODE, OverflowMode, overflowMode);
            scrollStep = NotifyPropertyChanges(SCROLLSTEP, ScrollStep, scrollStep);
            width = NotifyPropertyChanges(WIDTH, Width, width);

            if (PropertyChanges.Count > 0)
            {
                await OnPropertyChangeHandler().ConfigureAwait(true);
            }
        }

        /// <summary>
        /// Method invoked after each time the component has been rendered.
        /// </summary>
        /// <param name="firstRender">Set to true for the first time component rendering; otherwise gets false.</param>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (firstRender)
                {
                    bool isStateChanged = SetItems();
                    if (isStateChanged)
                    {
                        StateHasChanged();
                    }
                }

                await base.OnAfterRenderAsync(firstRender).ConfigureAwait(true);
                if (IsLoaded && IsItemChanged)
                {
                    IsItemChanged = false;
                    await InvokeMethod("sfBlazor.Toolbar.serverItemsRerender", new object[] { dataId, Items, firstRender }).ConfigureAwait(true);
                    EventAggregator.Notify(ITEMS_CHANGED, null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        internal override async Task OnAfterScriptRendered()
        {
            IsLoaded = true;
            if (JSRuntime is IJSInProcessRuntime)
            {
                await Task.Yield();
            }
            if (!IsDispose)
            {
                await InvokeMethod("sfBlazor.Toolbar.initialize", new object[] { dataId, Element, GetInstance(), DotnetObjectReference }).ConfigureAwait(true);
            }
            EventAggregator.Notify(INITIAL_LOAD, null);
            await EventAggregator.NotifyAsync(INITIAL_LOAD, null).ConfigureAwait(true);
            await SfBaseUtils.InvokeEvent<object>(Delegates?.Created, null).ConfigureAwait(true);
        }

        protected override bool ShouldRender()
        {
            bool isPreventRender = shouldRender;
            shouldRender = true;
            return isPreventRender;
        }

        internal override async void ComponentDispose()
        {
            IsDispose = true;
            if (IsRendered)
            {
                try
                {
                    await InvokeMethod("sfBlazor.Toolbar.destroy", dataId).ConfigureAwait(true);
                    await SfBaseUtils.InvokeEvent<object>(Delegates?.Destroyed, null).ConfigureAwait(true);
                    await WindowInstanceDispose(dataId).ConfigureAwait(false);
                }
                catch (TaskCanceledException ex)
                {
                    Console.WriteLine($"The operation was canceled: {ex.Message}");
                }
            }
        }
    }
}
