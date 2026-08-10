using System.Threading.Tasks;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Internal;
using System;

namespace Syncfusion.Blazor.Navigations
{
    public partial class SfCarousel : SfBaseComponent
    {

        /// <summary>
        /// Method invoked when the component is ready to start.
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();
            CarouselClass = "e-carousel e-blazor-carousel e-control e-lib";
            if (PartialVisible)
            {
                CarouselClass = "e-carousel e-blazor-carousel e-partial e-control e-lib";
            }
            if (EnableRtl || SyncfusionService.options.EnableRtl)
            {
                CarouselClass = SfBaseUtils.AddClass(CarouselClass, CLS_RTL);
            }
            if(Loop)
            {
                CarouselClass = SfBaseUtils.AddClass(CarouselClass, CLS_LOOP);
            }
            carouselLabel = Localizer.GetText("Carousel_SlideShow");
            UpdateHtmlAttributes();
            isPlayButtonClicked = AutoPlay;
            enableRtl = EnableRtl;
            autoPlay = AutoPlay;
            loop = Loop;
            buttonsVisibility = ButtonsVisibility;
            partialVisible = PartialVisible;
            enableTouchSwipe = EnableTouchSwipe;
            swipeMode = SwipeMode;
            pauseOnHover = PauseOnHover;
            allowKeyboardInteraction = AllowKeyboardInteraction;
        }

        /// <summary>
        /// Method invoked when the component has received parameters from its parent in the render tree,
        /// and the incoming values have been assigned to properties.
        /// </summary>
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync().ConfigureAwait(true);
            if (!isSlideChanged && Items.Count > 0)
            {
                if(PartialVisible != partialVisible)
                {
                    partialVisible = PartialVisible;
                    CarouselClass = partialVisible ? SfBaseUtils.AddClass(CarouselClass, "e-partial") : SfBaseUtils.RemoveClass(CarouselClass, "e-partial");
                }
                if (EnableRtl != enableRtl || SyncfusionService.options.EnableRtl != enableRtl)
                {
                    enableRtl = EnableRtl || SyncfusionService.options.EnableRtl;
                    CarouselClass = enableRtl ? SfBaseUtils.AddClass(CarouselClass, CLS_RTL) : SfBaseUtils.RemoveClass(CarouselClass, CLS_RTL);
                }
                if (Loop != loop)
                {
                    loop = Loop;
                    CarouselClass = loop ? SfBaseUtils.AddClass(CarouselClass, CLS_LOOP) : SfBaseUtils.RemoveClass(CarouselClass, CLS_LOOP);
                }
                if (AutoPlay != autoPlay)
                {
                    autoPlay = AutoPlay;
                    isPlayButtonClicked = autoPlay;
                    AutoSlide();
                }
                if (ButtonsVisibility != buttonsVisibility)
                {
                    hoverClass = ButtonsVisibility == CarouselButtonVisibility.VisibleOnHover ? CLS_HOVER_ARROWS : string.Empty;
                    buttonsVisibility = ButtonsVisibility;
                    BindMouseCallbackEvents();
                }
                if (SelectedIndex != selectedIndex)
                {
                    PreviousIndex = selectedIndex;
                    CarouselSlideDirection direction;
                    if (SelectedIndex > Items.Count - 1)
                    {
                        SelectedIndex = Items.Count - 1;
                        direction = CarouselSlideDirection.Next;
                    }
                    else if (SelectedIndex < 0)
                    {
                        SelectedIndex = 0;
                        direction = CarouselSlideDirection.Previous;
                    }
                    else
                    {
                        direction = SelectedIndex > selectedIndex ? CarouselSlideDirection.Next : CarouselSlideDirection.Previous;
                    }
                    await SetActiveSlide(SelectedIndex, direction).ConfigureAwait(true);
                }
                if(PauseOnHover != pauseOnHover)
                {
                    pauseOnHover = PauseOnHover;
                    BindMouseCallbackEvents();
                }
                if(allowKeyboardInteraction != AllowKeyboardInteraction)
                {
                    allowKeyboardInteraction = AllowKeyboardInteraction;
                    BindMouseCallbackEvents();
                }
                bool isSwipeUpdated = swipeMode != SwipeMode;
                if (isSwipeUpdated)
                {
                    enableTouchSwipe = EnableTouchSwipe;
                    if (swipeMode != SwipeMode)
                    {
                        swipeMode = SwipeMode;
                    }
                    BindMouseCallbackEvents();
                    if (isInitialized)
                        await InvokeMethod("sfBlazor.Carousel.updateTouch", new object[] { dataId, EnableTouchSwipe, SwipeMode }).ConfigureAwait(true);
                }
            }
        }

        /// <summary>
        /// Method invoked after each time the component has been rendered.
        /// </summary>
        /// <param name="firstRender">Set to true for the first time component rendering; otherwise gets false.</param>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                isItemChanged = false;
                int index = SelectedIndex;
                if (EnablePersistence)
                {
                    if (string.IsNullOrEmpty(ID))
                    {
                        throw new InvalidOperationException("The ID property of Carousel must not be null or Empty when using EnablePersistance.");
                    }
                    string? localStorageValue = await InvokeMethod<string>("window.localStorage.getItem", false, new object[] { ID }).ConfigureAwait(true);
                    localStorageValue = string.IsNullOrEmpty(localStorageValue) ? null : localStorageValue;
                    if (localStorageValue != null && localStorageValue != "null")
                    {
                        int persistValue = (int)SfBaseUtils.ChangeType(localStorageValue, typeof(int));
                        if (persistValue >= 0)
                        {
                            index = persistValue;
                        }
                    }
                }
                if (Items?.Count > 0)
                {
                    if (SelectedIndex > Items.Count - 1)
                    {
                        index = Items.Count - 1;
                    }
                    else if (SelectedIndex < 0)
                    {
                        index = 0;
                    }
                }
                hoverClass = ButtonsVisibility == CarouselButtonVisibility.VisibleOnHover ? CLS_HOVER_ARROWS : string.Empty;
                await UpdateActiveSlide(index).ConfigureAwait(true);
                ApplySlideInterval();
            }
            else if (isItemChanged)
            {
                await UpdateActiveSlide(selectedIndex).ConfigureAwait(true);
                isItemChanged = false;
            }
            if (!isInitialized && isScriptRendered && Items != null && Items.Count > 0 && !isDestroyed)
            {
                await InvokeMethod("sfBlazor.Carousel.initialize", new object[] { dataId, Element, SwipeMode, DotnetObjectReference });
                isInitialized = true;
            }
            await base.OnAfterRenderAsync(firstRender).ConfigureAwait(true);
        }

        protected override bool ShouldRender()
        {
            bool isPreventRender = shouldRender;
            shouldRender = true;
            return isPreventRender;
        }

        internal override void ComponentDispose()
        {
            isDestroyed = true;
            if (IsRendered)
            {
                if (isInitialized && !string.IsNullOrEmpty(dataId))
                {
                    _ = InvokeMethod("sfBlazor.Carousel.destroy", new object[] { dataId });
                }
            }
            if (slideTimer != null)
            {
                slideTimer.Stop();
                slideTimer.Elapsed -= OnSlideTimerEvent;
                slideTimer.Dispose();
                slideTimer = null;
            }

            if (transitionTimer != null)
            {
                transitionTimer.Stop();
                transitionTimer.Elapsed -= OnTransitionTimerEvent;
                transitionTimer.Dispose();
                transitionTimer = null;
            }
            WindowInstanceDispose(dataId).ConfigureAwait(false);
        }
        internal override async Task OnAfterScriptRendered()
        {
            if (JSRuntime is IJSInProcessRuntime)
            {
                await Task.Yield();
            }
            isScriptRendered = true;
        }
    }
}
