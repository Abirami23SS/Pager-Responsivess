using System.Threading.Tasks;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Syncfusion.Blazor.Navigations
{
    public partial class SfTab : SfBaseComponent
    {
        /// <summary>
        /// Method invoked when the component is ready to start.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        protected override async Task OnInitializedAsync()
        {
            if (string.IsNullOrEmpty(ID))
            {
                if (EnablePersistence)
                {
                    throw new InvalidOperationException("The ID property of Tab must not be null or Empty when using EnablePersistance.");
                }
                ID = TABPREFIX + Guid.NewGuid().ToString();
            }

            ScriptModules = SfScriptModules.SfTab;
            UpdateLocalProperties();
            await base.OnInitializedAsync().ConfigureAwait(true);
            UpdateAnimationProperties(Animation);
            cssClass = CssClass;
            enableRtl = EnableRtl;
            headerPlacement = HeaderPlacement;
            height = Height;
            tabitems = Items;
            overflowMode = OverflowMode;
            scrollStep = ScrollStep;
            selectedItem = SelectedItem;
            showCloseButton = ShowCloseButton;
            width = Width;
            allowDragAndDrop = AllowDragAndDrop;
            if (IsStaticServerRendering())
            {
                await UpdateToolbarItems().ConfigureAwait(true);
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
            animation = NotifyPropertyChanges(ANIMATION, Animation, animation);
            cssClass = NotifyPropertyChanges(CSSCLASS, CssClass, cssClass);
            enableRtl = NotifyPropertyChanges(ENABLE_RTL, EnableRtl, enableRtl);
            headerPlacement = NotifyPropertyChanges(HEADER_PLACEMENT, HeaderPlacement, headerPlacement);
            height = NotifyPropertyChanges(HEIGHT, Height, height);
            tabitems = NotifyPropertyChanges(ITEMS, Items, tabitems);
            overflowMode = NotifyPropertyChanges(OVERFLOWMODE, OverflowMode, overflowMode);
            scrollStep = NotifyPropertyChanges(SCROLLSTEP, ScrollStep, scrollStep);
            selectedItem = NotifyPropertyChanges(SELECTED_ITEM, SelectedItem, selectedItem);
            showCloseButton = NotifyPropertyChanges(SHOWCLOSEBUTTON, ShowCloseButton, showCloseButton);
            width = NotifyPropertyChanges(WIDTH, Width, width);
            allowDragAndDrop = NotifyPropertyChanges(ALLOWDRAGANDDROP, AllowDragAndDrop, allowDragAndDrop);
            if (PropertyChanges.Count > 0 && !IsSelectedItemChanged)
            {
                await OnPropertyChangeHandler().ConfigureAwait(true);
            }
            if (IsSelectedItemChanged)
            {
                PropertyChanges.Clear();
                IsSelectedItemChanged = false;
            }
        }

        /// <summary>
        /// Method invoked after each time the component has been rendered.
        /// </summary>
        /// <param name="firstRender">Set to true for the first time component rendering; otherwise gets false.</param>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            bool isTabItemPropertyChanged = IsTabItemChanged;
            if (firstRender && EnablePersistence)
            {
                string localStorage = await InvokeMethod<string>("window.localStorage.getItem", false, new object[] { $"tab{ID}" }).ConfigureAwait(true);
                if (!string.IsNullOrEmpty(localStorage))
                {
                    int persistedIndex = Convert.ToInt32(localStorage, CultureInfo.InvariantCulture);
                    await SfBaseUtils.UpdateProperty(persistedIndex, selectedItem, SelectedItemChanged).ConfigureAwait(true);
                    SelectedItem = selectedItem = persistedIndex;
                    SfBaseUtils.UpdateDictionary(SELECTED_ITEM, SelectedItem, PropertyChanges);
                    SelectContent();
                }
            }
            if (firstRender || (PropertyChanges.Count > 0 || isTabItemPropertyChanged))
            {
                IJSInProcessRuntime? runtime = JSRuntime as IJSInProcessRuntime;

                // Client side blazor will fail if it is single threaded https://github.com/dotnet/aspnetcore/issues/14253
                if (runtime != null)
                {
                    await Task.Yield();
                }

                if (firstRender && !(Items != null && Items.Count > 0))
                {
                    if (Items == null)
                    {
                        Items = new List<TabItem>();
                    }

                    if (Delegates?.Created.HasDelegate == true)
                        await Delegates.Created.InvokeAsync(new { Name = "Created" }).ConfigureAwait(true);
                    IsCreatedEvent = true;
                }

                if (firstRender || (PropertyChanges.ContainsKey(ITEMS) || PropertyChanges.ContainsKey(SELECTED_ITEM) || isTabItemPropertyChanged))
                {
                    await SetToolbarItems().ConfigureAwait(true);
                    if ((Toolbar != null) && (PropertyChanges.ContainsKey(ITEMS) || PropertyChanges.ContainsKey(SELECTED_ITEM) || isTabItemPropertyChanged))
                    {
                        IsPreventFocus = true;
                        Toolbar.IsItemChanged = true;
                    }

                    IsTabItemChanged = false;
                    StateHasChanged();
                }
            }

            if (isSwitchTabClick && LoadOn != ContentLoad.Init)
            {
                int targetIndexCopy = tabTargetIndex;
                SelectingEventArgs selectingArgsCopy = tabSelectingEventArgs;
                tabSelectingEventArgs = null;
                tabTargetIndex = 0;
                await SwitchTabClick(targetIndexCopy, selectingArgsCopy).ConfigureAwait(true);
            }
            if (isSwitchTabUpdate && LoadOn != ContentLoad.Init)
            {
                await SwitchTabUpdate(tabTargetIndex, tabSelectingEventArgs).ConfigureAwait(true);
                tabSelectingEventArgs = null;
                tabTargetIndex = 0;
            }

            await base.OnAfterRenderAsync(firstRender).ConfigureAwait(true);
        }

        internal override async Task OnAfterScriptRendered()
        {
            IsTabScriptLoaded = true;
            if (!IsCreatedEvent)
            {
                IsCreatedEvent = Delegates == null || !Delegates.Created.HasDelegate;
            }

            await InvokeMethod("sfBlazor.Tab.initialize", new object[] { dataId, Element, GetInstance(), DotnetObjectReference }).ConfigureAwait(true);

            if (SyncfusionService.options.EnableRippleEffect && Toolbar != null)
            {
                await SfBaseUtils.RippleEffect(JSRuntime, Toolbar.Element, new RippleSettings() { Selector = ".e-tab-wrap" }).ConfigureAwait(true);
            }
        }

        protected override bool ShouldRender()
        {
            bool tmp = shouldRender;
            shouldRender = true;
            return tmp;
        }

        /// <summary>
        /// Prevents the Tab render. This method will internally sets value to be returned from ShouldRender method.
        /// </summary>
        /// <param name="preventRender">Default value is true. Toggles the ShouldRender method value.</param>
        public void PreventRender(bool preventRender = true) => shouldRender = !preventRender;

        internal override async void ComponentDispose()
        {
            try
            {
                if (IsRendered)
                {
                    await InvokeMethod("sfBlazor.Tab.destroy", new object[] { dataId, $"tab{ID}", SelectedItem }).ConfigureAwait(true);

                    if (Delegates?.Destroyed.HasDelegate == true)
                    {
                        await Delegates.Destroyed.InvokeAsync(null).ConfigureAwait(true);
                    }

                    await WindowInstanceDispose(dataId).ConfigureAwait(false);
                }
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine($"The operation was canceled: {ex.Message}");
            }
        }
    }
}
