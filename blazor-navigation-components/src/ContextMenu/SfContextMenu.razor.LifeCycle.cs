using System.Threading.Tasks;
using Syncfusion.Blazor.Internal;
using Syncfusion.Blazor.Navigations.Internal;

namespace Syncfusion.Blazor.Navigations 
{
    /// <summary>
    /// ContextMenu is a graphical user interface that appears on the user right click/touch hold operation.
    /// </summary>
    public partial class SfContextMenu<TValue> : SfMenuBase<TValue>
    {
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(true);
            filter = Filter;
            target = Target;
            showOn = OpenActionEvents;
            closeOn = CloseActionEvents;
            Initialize();
            ScriptModules = SfScriptModules.SfContextMenu;
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync().ConfigureAwait(true);
            filter = NotifyPropertyChanges(nameof(Filter), Filter, filter);
            target = NotifyPropertyChanges(nameof(Target), Target, target);
            showOn = NotifyPropertyChanges(nameof(OpenActionEvents), OpenActionEvents, showOn);
            if (PropertyChanges.Count > 0)
            {
                foreach (string key in PropertyChanges.Keys)
                {
                    if (key == nameof(EnableRtl) || key == nameof(CssClass))
                    {
                        Initialize();
                    }
                    else
                    {
                        await InvokeMethod(PROPERTYCHANGED, dataId, key, key == nameof(Target) ? Target : key == nameof(OpenActionEvents) ? showOn : Filter).ConfigureAwait(true);
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

                if (Delegates?.Created.HasDelegate == true)
                    await Delegates.Created.InvokeAsync(new { Name = CREATED }).ConfigureAwait(true);
            }
            else
            {
                if (CloseEventArgs != null)
                {
                    if(Delegates?.Closed.HasDelegate == true)
                    {
                        await Delegates.Closed.InvokeAsync(CloseEventArgs).ConfigureAwait(true);
                    }
                    CloseEventArgs = null!;
                }
                if (IsDevice && !IsMenu && NavIdx.Count > 1 && isReposition)
                {
                    await InvokeMethod(CMENUPOS, dataId, 0, 0, EnableRtl || SyncfusionService.options.EnableRtl, true, true, scrollHeight, IsDevice).ConfigureAwait(true);
                    isReposition = false;
                }
                if (NavIdx.Count > 1 && OpenEventArgs != null)
                {
                    var eventArgs = OpenEventArgs;
                    OpenEventArgs = null!;
                    if (IsDevice)
                    {
                        await InvokeMethod(CMENUPOS, dataId, 0, 0, EnableRtl || SyncfusionService.options.EnableRtl, true, true, scrollHeight, IsDevice).ConfigureAwait(true);
                        if (isSubMenuDevice) { isReposition = true; isSubMenuDevice = false; }
                    }
                    else if (!IsMenu)
                    {
                        await InvokeMethod(SUBMENUPOS, dataId, EnableRtl || SyncfusionService.options.EnableRtl, ShowItemOnClick, Top == null && Left == null, scrollHeight).ConfigureAwait(true);
                    }
                    if (Parent != null && IsMenu)
                    {
                        if (Parent.NavigationIndex == null || Parent.NavigationIndex.Count == 0)
                        {
                            Close();
                        }
                        var args = new MenuOptions()
                        {
                            dataId = Parent.dataId,
                            ItemIndex = (Parent?.NavigationIndex != null && Parent.NavigationIndex.Count > 0) ? Parent.NavigationIndex[0] : 0,
                            ShowItemOnClick = Parent?.ShowItemOnClick ?? false,
                            EnableScrolling = Parent?.EnableScrolling ?? false,
                            IsVertical = Parent?.Orientation == Orientation.Vertical,
                            IsRtl = EnableRtl || SyncfusionService.options.EnableRtl,
                            ScrollHeight = Parent?.ScrollHeight ?? 0
                        };
                        args.Popup = Element;
                        args.popupDataId = dataId;
                        await InvokeMethod(MENUSUBMENUPOS, args, EnterKey).ConfigureAwait(true);
                        EnterKey = false;
                    }
                    if (Delegates?.Opened.HasDelegate == true)
                        await Delegates.Opened.InvokeAsync(eventArgs).ConfigureAwait(true);
                    StateHasChanged();
                }
                else if (NavIdx.Count == 1 && OpenEventArgs != null)
                {
                    var eventArgs = OpenEventArgs;
                    if (manualOpen)
                    {
                        var cancel = manualOpen = false;
                        OpenEventArgs = null!;
                        if (IsMenu)
                        {
                            var evtArgs = await TriggerBeforeOpenCloseEvent(Items[0], Items, ONOPEN, true).ConfigureAwait(true);
                            cancel = evtArgs.Cancel;
                        }
                        if (!IsMenu)
                        {
                            await Task.Yield();
                            await InvokeMethod(CMENUPOS, dataId, Left, Top, EnableRtl || SyncfusionService.options.EnableRtl, false, isCollision, scrollHeight, IsDevice).ConfigureAwait(true);
                        }

                        if (!cancel && !string.IsNullOrEmpty(cmenuHidden))
                        {
                            if (Delegates?.Opened.HasDelegate == true)
                                await Delegates.Opened.InvokeAsync(eventArgs).ConfigureAwait(true);
                        }

                        StateHasChanged();
                    }
                    else if (!IsMenu && Left != null && Top != null)
                    {
                        OpenEventArgs = null!;
                        await Task.Yield();
                        await InvokeMethod(CMENUPOS, dataId, Left, Top, EnableRtl || SyncfusionService.options.EnableRtl, false, true, scrollHeight, IsDevice).ConfigureAwait(true);
                        if (Delegates?.Opened.HasDelegate == true)
                            await Delegates.Opened.InvokeAsync(eventArgs).ConfigureAwait(true);
                        StateHasChanged();
                    }
                }
            }
        }

        internal override async Task OnAfterScriptRendered()
        {
            await Task.Yield();
            if (IsDisposed || DotnetObjectReference == null) { return; }
            await InvokeMethod(INITIALIZE, dataId, Element, Target, Filter, OpenActionEvents, CloseActionEvents, EnableScrolling, DotnetObjectReference, AnimationSettingsObj).ConfigureAwait(true);
        }

        private void OnBreakPointChanged(BreakpointChangedEventArgs args)
        {
            if (activeBreakpoint != args.ActiveBreakpoint)
            {
                if (args.ActiveBreakpoint == "Small" && !IsMenu)
                {
                    IsDevice = true;
                }
                else
                {
                    IsDevice = false;
                }
            }
            activeBreakpoint = args.ActiveBreakpoint;
        }

        internal override void ComponentDispose()
        {
            ariaLabelCount--;
            if (IsRendered)
            {
                InvokeMethod(DESTROY, dataId, refElement).ContinueWith(t => { }, TaskScheduler.Current);
                WindowInstanceDispose(dataId).ConfigureAwait(false);
            }
        }
    }
}
