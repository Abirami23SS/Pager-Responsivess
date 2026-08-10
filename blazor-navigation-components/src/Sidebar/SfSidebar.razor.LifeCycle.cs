using System;
using System.Threading.Tasks;
using Syncfusion.Blazor.Internal;
using System.Collections.Generic;
using System.Linq;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Partial Class SfSidebar.
    /// </summary>
    public partial class SfSidebar : SfBaseComponent
    {
       

        /// <summary>
        /// Method invoked when the component is ready to start.
        /// </summary>
        /// <returns>="Task".</returns>
        protected override async Task OnInitializedAsync()
        {
            if (string.IsNullOrEmpty(ID))
            {
                ID = SfBaseUtils.GenerateID(IDPREFIX);
            }

            await base.OnInitializedAsync().ConfigureAwait(true);
            SidebarCloseOnDocumentClick = CloseOnDocumentClick;
            _enableDock = EnableDock;
            SidebarIsOpen = IsOpen;
            SidebarPosition = Position;
            SliderShowBackdrop = ShowBackdrop;
            SidebarType = Type;
            SidebarWidth = Width;
            ScriptModules = SfScriptModules.SfSidebar;
            try
            {
                GetClass();
                GetStyle();
                SetDock();
                UpdateAttributes();
            }
            catch
            {
                if (!IsDisposed)
                    throw;
            }
        }

        /// <summary>
        /// Method invoked when any changes in component state occurs.
        /// </summary>
        /// <returns>="Task".</returns>
        protected async override Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync().ConfigureAwait(true);
            SidebarCloseOnDocumentClick = NotifyPropertyChanges(nameof(CloseOnDocumentClick), CloseOnDocumentClick, SidebarCloseOnDocumentClick);
            SidebarPosition = NotifyPropertyChanges(nameof(Position), Position, SidebarPosition);
            SliderShowBackdrop = NotifyPropertyChanges(nameof(ShowBackdrop), ShowBackdrop, SliderShowBackdrop);
            SidebarType = NotifyPropertyChanges(nameof(Type), Type, SidebarType);
            SidebarWidth = NotifyPropertyChanges(nameof(Width), Width, SidebarWidth);
            _enableDock = NotifyPropertyChanges(nameof(EnableDock), EnableDock, _enableDock);

            try
            {
                if (EnablePersistence && IsRendered && !SfBaseUtils.Equals(IsOpen, SidebarIsOpen))
                {
                    await SetLocalStorage(ID, SerializeModel()).ConfigureAwait(true);
                }

                SidebarIsOpen = NotifyPropertyChanges(nameof(IsOpen), IsOpen, SidebarIsOpen);
                if (PropertyChanges.Count > 0)
                {
                    await SidebarPropertyChange(PropertyChanges).ConfigureAwait(true);
                }
                if (SfSidebarContainer != null && !openState)
                {
                    isDeviceMode = SyncfusionService.IsDeviceMode;
                    await SidebarInitRender().ConfigureAwait(true);
                }

                UpdateAttributes();
            }
            catch
            {
                if (!IsDisposed)
                    throw;
            }
        }

        /// <summary>
        /// Method invoked after each time the component has been rendered.
        /// </summary>
        /// <param name="firstRender">Set to true for the first time component rendering; otherwise gets false.</param>
        /// <returns>="Task".</returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender).ConfigureAwait(true);
            if (firstRender)
            {
                if (Created.HasDelegate)
                    await Created.InvokeAsync(null).ConfigureAwait(true);
                isVisible = openState;
            }
            if (propertyKeys?.Count > 0)
            {
                HashSet<string> keysToIgnore = new HashSet<string>
                {
                    nameof(IsOpen),
                    nameof(Position),
                    nameof(EnableDock)
                };

                bool requiresJsInteropUpdate = propertyKeys.Any(p => !keysToIgnore.Contains(p.Key));
                if (requiresJsInteropUpdate && SfSidebarContainer == null)
                    await InvokeMethod("sfBlazor.Sidebar.onPropertyChange", new object[] { dataId, GetProperties() }).ConfigureAwait(true);
                propertyKeys.Clear();
            }
        }

        internal override async Task OnAfterScriptRendered()
        {
            try
            {
                if (SfSidebarContainer == null)
                {
                    isDeviceMode = SyncfusionService.IsDeviceMode;
                    await PersistProperties().ConfigureAwait(true);
                    isMediaQueryOpen = await InvokeMethod<bool>("sfBlazor.Sidebar.initialize", false, new object[] { dataId, element, DotnetObjectReference, GetProperties() }).ConfigureAwait(true);
                    await SidebarInitRender().ConfigureAwait(true);
                    if (EnablePersistence)
                    {
                        GetClass();
                        GetStyle();
                        UpdateAttributes();
                        StateHasChanged();
                    }
                }
            }
            catch
            {
                if (!IsDisposed)
                    throw;
            }
            
        }

        private async Task PersistProperties()
        {
            if (EnablePersistence)
            {
                PersistenceValues localStorageValue = await InvokeMethod<PersistenceValues>("window.localStorage.getItem", true, new object[] { ID }).ConfigureAwait(true);
                if (localStorageValue == null)
                {
                    await SetLocalStorage(ID, SerializeModel()).ConfigureAwait(true);
                }
                else
                {
                    IsOpen = SidebarIsOpen = await SfBaseUtils.UpdateProperty<bool>(localStorageValue.IsOpen, SidebarIsOpen, IsOpenChanged).ConfigureAwait(true);
                }
            }
        }
    }
}
