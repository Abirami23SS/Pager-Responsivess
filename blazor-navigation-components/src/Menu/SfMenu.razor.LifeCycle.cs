using System.Threading.Tasks;
using Syncfusion.Blazor.Internal;
using Syncfusion.Blazor.Navigations.Internal;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Menu is a graphical user interface that serve as navigation headers for your application.
    /// </summary>
    public partial class SfMenu<TValue> : SfMenuBase<TValue>
    {
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(true);
            enableScrolling = EnableScrolling;
            hamburgerMode = HamburgerMode;
            orientation = Orientation;
            Initialize();
            ScriptModules = SfScriptModules.SfMenu;
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync().ConfigureAwait(true);
            enableScrolling = NotifyPropertyChanges(nameof(EnableScrolling), EnableScrolling, enableScrolling);
            hamburgerMode = NotifyPropertyChanges(nameof(HamburgerMode), HamburgerMode, hamburgerMode);
            orientation = NotifyPropertyChanges(nameof(Orientation), Orientation, orientation);
            if (PropertyChanges.Count > 0)
            {
                foreach (string key in PropertyChanges.Keys)
                {
                    if (key == nameof(EnableScrolling))
                    {
                        await InvokeMethod(UPDATESCROLL, dataId, EnableScrolling, EnableRtl || SyncfusionService.options.EnableRtl).ConfigureAwait(true);
                    }
                    else if (key == nameof(HamburgerMode))
                    {
                        if (hamburgerMode)
                        {
                            await InvokeMethod(UPDATESCROLL, dataId, EnableScrolling, EnableRtl || SyncfusionService.options.EnableRtl).ConfigureAwait(true);
                        }

                        if (SubMenuOpen)
                        {
                            SubMenuOpen = false;
                        }

                        if (closeMenu)
                        {
                            closeMenu = false;
                        }

                        ClsCollection.Clear();
                        NavIdx.Clear();
                        Initialize();
                    }
                    else if (key == nameof(Orientation))
                    {
                        isOrientationScroll = EnableScrolling;
                    }
                    else
                    {
                        Initialize();
                    }
                }
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender).ConfigureAwait(true);
            if (firstRender)
            {
                if (Fields == null)
                {
                    Fields = new MenuFieldSettings();
                }

                if (Items != null)
                {
                    StateHasChanged();
                }

                if (Delegates != null)
                {
                    if (Delegates.Created.HasDelegate)
                    {
                        await Delegates.Created.InvokeAsync(new { Name = CREATED }).ConfigureAwait(true);
                    }
                }
                else
                {
                    if (SelfRefDelegates != null && SelfRefDelegates.Created.HasDelegate)
                    {
                        await SelfRefDelegates.Created.InvokeAsync(new { Name = CREATED }).ConfigureAwait(true);
                    }
                }
            }

            if (isOrientationScroll)
            {
                isOrientationScroll = false;
                await InvokeMethod(ORIENTATIONSCROLL, dataId, EnableScrolling, EnableRtl || SyncfusionService.options.EnableRtl, Orientation.ToString()).ConfigureAwait(true);
            }

            if (OpenEventArgs != null)
            {
                var eventArgs = OpenEventArgs;
                OpenEventArgs = null!;
                await TriggerOpenCloseEvent(eventArgs, true, true).ConfigureAwait(true);
            }

            if (OpenMenuEventArgs != null)
            {
                var eventArgs = OpenMenuEventArgs;
                OpenMenuEventArgs = null!;
                await TriggerOpenCloseEvent(eventArgs, true, true).ConfigureAwait(true);
            }

            if (CloseEventArgs != null)
            {
                var eventArgs = CloseEventArgs;
                CloseEventArgs = null!;
                await TriggerOpenCloseEvent(eventArgs, false, false).ConfigureAwait(true);
            }

            if (CloseMenuEventArgs != null)
            {
                var eventArgs = CloseMenuEventArgs;
                CloseMenuEventArgs = null!;
                await TriggerOpenCloseEvent(eventArgs, false, false).ConfigureAwait(true);
            }
        }

        internal override async Task OnAfterScriptRendered()
        {
            var args = new MenuOptions() { dataId = dataId, Element = Element, EnableScrolling = EnableScrolling, IsRtl = EnableRtl || SyncfusionService.options.EnableRtl, AnimationSettings = AnimationSettingsObj };
            await InvokeMethod(INITIALIZE, args, DotnetObjectReference).ConfigureAwait(true);
        }

        internal override void ComponentDispose()
        {
            if (IsRendered)
            {
                InvokeMethod(DESTROY, dataId).ContinueWith(t => { }, TaskScheduler.Current);
                WindowInstanceDispose(dataId).ConfigureAwait(false);
            }
        }
    }
}
