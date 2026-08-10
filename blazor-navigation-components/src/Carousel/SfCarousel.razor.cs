using System;
using System.Timers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Web;
using Syncfusion.Blazor.Internal;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// The carousel component is a slide show of images, contents or images with contents.
    /// </summary>
    /// <remarks>
    /// Carousel items can be populated by specifying <see cref="CarouselItem"/> within <see cref="SfCarousel"/> tag directive.
    /// </remarks>
    /// <example>
    /// In the below code example, a basic Carousel component initialized with <see cref="CarouselItem"/> tag directive.
    /// <code><![CDATA[
    /// <SfCarousel>
    ///     <CarouselItem><div>Slide 1</div></CarouselItem>
    ///     <CarouselItem><div>Slide 2</div></CarouselItem>
    ///     <CarouselItem><div>Slide 3</div></CarouselItem>
    /// </SfCarousel>
    /// ]]></code>
    /// </example>
    public partial class SfCarousel : SfBaseComponent
    {
        //Class variables
        private const string CLS_NAVIGATORS = "e-carousel-navigators";
        private const string CLS_PREVIOUS = "e-previous";
        private const string CLS_PREV_BUTTON = "e-previous-button e-round e-flat";
        private const string CLS_PREV_ICON = "e-previous-icon e-icons";
        private const string CLS_NEXT = "e-next";
        private const string CLS_NEXT_BUTTON = "e-next-button e-round e-flat";
        private const string CLS_NEXT_ICON = "e-next-icon e-icons";
        private const string CLS_INDICATORS = "e-carousel-indicators";
        private const string CLS_INDICATOR_BARS = "e-indicator-bars";
        private const string CLS_INDICATOR_BAR = "e-indicator-bar";
        private const string CLS_CAROUSEL_ITEMS = "e-carousel-items";
        private const string CLS_CAROUSEL_ITEM = "e-carousel-item";
        private const string CLS_SLIDE_CONTAINER = "e-carousel-slide-container";
        private const string CLS_CLONE = "e-clone";
        private const string CLS_PLAYPAUSE = "e-play-pause";
        private const string CLS_PLAYBUTTON = "e-play-button e-round e-flat";
        private const string CLS_PLAYICON = "e-play-icon e-icons";
        private const string CLS_PAUSEICON = "e-pause-icon e-icons";
        private const string CLS_RTL = "e-rtl";
        private const string CLS_LOOP = "e-loop";
        private const string CLS_TEMPLATE = "e-template";
        private const string CLS_HOVER_ARROWS = "e-hover-arrows";
        private const string CLS_CAROUSEL_HOVER = "e-carousel-hover";
        private const string CLS_SLIDE_ANIMATION = "e-carousel-slide-animation";
        private const string CLS_FADE_ANIMATION = "e-carousel-fade-animation";
        private const string CLS_CUSTOM_ANIMATION = "e-carousel-custom-animation";
        private const string CLS_ANIMATION_NONE = "e-carousel-animation-none";
        private const string CLS_PREVIOUS_SLIDE = "e-prev";
        private const string CLS_NEXT_SLIDE = "e-next";
        private const string CLS_ACTIVE = "e-active";
        private const string CLS_KEYPRESS = "e-key-press";
        private const string CLS_DEFAULT_INDICATOR = "e-default";
        private const string CLS_DYNAMIC_INDICTOR = "e-dynamic";
        private const string CLS_FRACTION_INDICATOR = "e-fraction";
        private const string CLS_PROGRESS_INDICATOR = "e-progress";
        private bool shouldRender = true;
        private bool isScriptRendered;
        private bool isInitialized;

        private List<CarouselItem> Items { get; set; } = new List<CarouselItem>();
        private int PreviousIndex { get; set; } = -1;
        private string CarouselClass { get; set; } = string.Empty;
        private Timer? slideTimer { get; set; }
        private Timer? transitionTimer { get; set; }
        internal string dataId = "sfCarousel-" + Guid.NewGuid().ToString();
        private bool autoPlay;
        private bool loop;
        private bool enableRtl;
        private bool partialVisible;
        private CarouselButtonVisibility buttonsVisibility;
        private Dictionary<string, object>? htmlAttributes;
        private string hoverClass = string.Empty;
        private string? carouselLabel;
        private bool isPlayButtonClicked;
        private int selectedIndex;
        private bool isSlideChanged;
        private bool isItemChanged;
        private bool isDestroyed;
        private bool enableTouchSwipe;
        private CarouselSwipeMode swipeMode;
        private bool pauseOnHover;
        private bool allowKeyboardInteraction;

        internal void UpdateItemProperties(CarouselItem item, bool isRemove)
        {
            if (isRemove)
            {
                if (Items != null && Items.Contains(item))
                {
                    int currentIndex = Items.IndexOf(item);
                    Items.Remove(item);
                    if (currentIndex == SelectedIndex && currentIndex > 0)
                    {
                        selectedIndex = currentIndex - 1;
                    }
                    else if (SelectedIndex > Items.Count)
                    {
                        selectedIndex = Items.Count - 1;
                    }
                    isItemChanged = true;
                }
            }
            else
            {
                Items.Add(item);
                isItemChanged = true;
            }
        }

        private int GetSlideIndex(CarouselSlideDirection direction)
        {
            int currentIndex = SelectedIndex;
            if (direction == CarouselSlideDirection.Previous)
            {
                currentIndex--;
                if (currentIndex < 0)
                {
                    currentIndex = Items.Count - 1;
                }
            }
            else
            {
                currentIndex++;
                if (currentIndex > Items.Count - 1)
                {
                    currentIndex = 0;
                }
            }
            return currentIndex;
        }

        private async Task OnNavigationClick(CarouselSlideDirection direction)
        {
            if (IsSuspendSlideTransition(direction))
            {
                return;
            }
            await SetActiveSlide(GetSlideIndex(direction), direction).ConfigureAwait(true);
        }

        private async Task OnIndicatorClick(int index)
        {
            CarouselSlideDirection direction;
            if (index > selectedIndex)
            {
                direction = CarouselSlideDirection.Next;
            }
            else
            {
                direction = CarouselSlideDirection.Previous;
            }
            await SetActiveSlide(index, direction).ConfigureAwait(true);
        }

        private void OnCaruselButtonFocus()
        {
            CarouselClass = SfBaseUtils.AddClass(CarouselClass, CLS_KEYPRESS);
        }

        private async Task OnPlayButtonClick()
        {
            if (!ShowPlayButton)
            {
                return;
            }
            isPlayButtonClicked = !isPlayButtonClicked;
            if (!Loop && SelectedIndex == Items.Count - 1)
            {
                AutoPlay = true;
                await SetActiveSlide(0, CarouselSlideDirection.Next).ConfigureAwait(true);
            }
            else
            {
                AutoPlay = !AutoPlay;
            }
            AutoSlide();
        }

        private bool IsSuspendSlideTransition(CarouselSlideDirection direction)
        {
            return !Loop && ((direction == CarouselSlideDirection.Previous && SelectedIndex == 0) || (direction == CarouselSlideDirection.Next && SelectedIndex == Items.Count - 1));
        }

        private void OnFocusActions(FocusEventArgs e)
        {
            if (e.Type == "focusin")
            {
                CarouselClass = SfBaseUtils.AddClass(CarouselClass, CLS_CAROUSEL_HOVER);
                ResetSlideInterval();
            }
            else if (e.Type == "focusout" && CarouselClass.Contains(CLS_CAROUSEL_HOVER, StringComparison.Ordinal))
            {
                CarouselClass = SfBaseUtils.RemoveClass(CarouselClass, CLS_CAROUSEL_HOVER);
                CarouselClass = SfBaseUtils.RemoveClass(CarouselClass, CLS_KEYPRESS);
                ApplySlideInterval();
            }
        }

        private void OnHoverActions(string type)
        {
            if (ButtonsVisibility == CarouselButtonVisibility.VisibleOnHover)
            {
                if (type == "mouseenter" && hoverClass == CLS_HOVER_ARROWS)
                {
                    hoverClass = string.Empty;
                }
                else if (type == "mouseout" && string.IsNullOrEmpty(hoverClass))
                {
                    hoverClass = CLS_HOVER_ARROWS;
                }
            }
            if (type == "mouseenter")
            {
                if (PauseOnHover)
                {
                    CarouselClass = SfBaseUtils.AddClass(CarouselClass, CLS_CAROUSEL_HOVER);
                    ResetSlideInterval();
                }
            }
            else if (type == "mouseout" && CarouselClass.Contains(CLS_CAROUSEL_HOVER, StringComparison.Ordinal))
            {
                CarouselClass = SfBaseUtils.RemoveClass(CarouselClass, CLS_CAROUSEL_HOVER);
                AutoSlide();
            }
        }

        private async Task OnKeyDown(KeyboardEventArgs e)
        {
            if(!AllowKeyboardInteraction) { return; }
            int index;
            switch (e.Code)
            {
                case "ArrowLeft":
                case "ArrowUp":
                    if (!Loop && SelectedIndex == 0)
                    {
                        return;
                    }
                    index = SelectedIndex == 0 || EnableRtl ? Items.Count - 1 : SelectedIndex - 1;
                    await SetActiveSlide(index, CarouselSlideDirection.Previous).ConfigureAwait(true);
                    break;
                case "ArrowRight":
                case "ArrowDown":
                    if (!Loop && SelectedIndex == Items.Count - 1)
                    {
                        return;
                    }
                    index = SelectedIndex == Items.Count - 1 ? 0 : SelectedIndex + 1;
                    await SetActiveSlide(index, CarouselSlideDirection.Next).ConfigureAwait(true);
                    break;
                case "Home":
                    await SetActiveSlide(0, CarouselSlideDirection.Previous).ConfigureAwait(true);
                    break;
                case "End":
                    await SetActiveSlide(Items.Count - 1, CarouselSlideDirection.Next).ConfigureAwait(true);
                    break;
                case "Space":
                    if (!CarouselClass.Contains(CLS_KEYPRESS, StringComparison.Ordinal))
                    {
                        await OnPlayButtonClick().ConfigureAwait(true);
                    }
                    break;
                default:
                    return;
            }
        }

        private void OnTouchSwipeActions(TouchEventArgs args)
        {
            if (args.Type == "touchstart")
            {
                ResetSlideInterval();
            }
            else if (args.Type == "touchend")
            {
                AutoSlide();
            }
        }

        /// <exclude />
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task ChangeSlide(string direction)
        {
            isSlideChanged = true;
            int index = direction == "Previous" ? GetSlideIndex(CarouselSlideDirection.Previous) : GetSlideIndex(CarouselSlideDirection.Next);
            SelectedIndex = selectedIndex = index = await SfBaseUtils.UpdateProperty(index, SelectedIndex, SelectedIndexChanged).ConfigureAwait(true);
            if (EnablePersistence)
            {
                await InvokeMethod("window.localStorage.setItem", new object[] { ID, SelectedIndex }).ConfigureAwait(true);
            }
            await InvokeAsync(StateHasChanged).ConfigureAwait(true);
            isSlideChanged = false;
        }

        private bool IsProgressSlideTransition()
        {
            List<CarouselItem> items = Items.FindAll(item => !string.IsNullOrEmpty(item.SlideClass) && (item.SlideClass.Contains(CLS_PREVIOUS_SLIDE, StringComparison.OrdinalIgnoreCase) || item.SlideClass.Contains(CLS_NEXT_SLIDE, StringComparison.OrdinalIgnoreCase)));
            return items.Count > 0;
        }

        private async Task SetActiveSlide(int index, CarouselSlideDirection slideDirection = CarouselSlideDirection.Next)
        {
            if (index == selectedIndex || IsProgressSlideTransition() || isDestroyed)
            {
                return;
            }
            PreviousIndex = selectedIndex;
            isSlideChanged = true;
            try
            {
                await InvokeMethod("sfBlazor.Carousel.swipeHandler", new object[] { dataId, slideDirection, index }).ConfigureAwait(true);
                SelectedIndex = selectedIndex = await SfBaseUtils.UpdateProperty(index, SelectedIndex, SelectedIndexChanged).ConfigureAwait(true);
                if (EnablePersistence)
                {
                    await InvokeMethod("window.localStorage.setItem", new object[] { ID, SelectedIndex }).ConfigureAwait(true);
                }
                await ActiveSlideTransition().ConfigureAwait(true);
                isSlideChanged = false;
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine($"The operation was canceled: {ex.Message}");
            }
        }

        private async Task ActiveSlideTransition()
        {
            await InvokeAsync(StateHasChanged).ConfigureAwait(true);
            transitionTimer?.Stop();
            transitionTimer = new Timer { Interval = 700, AutoReset = false };
            transitionTimer.Elapsed += OnTransitionTimerEvent;
            transitionTimer.Start();
        }

        private void AutoSlide()
        {
            ResetSlideInterval();
            ApplySlideInterval();
        }

        private void ResetSlideInterval()
        {
            slideTimer?.Stop();
            slideTimer = null;
        }

        private void ApplySlideInterval()
        {
            if (!AutoPlay || CarouselClass.Contains(CLS_CAROUSEL_HOVER, StringComparison.Ordinal))
            {
                return;
            }
            int slideInterval = Interval;
            if (Items.Count > 0 && Items[SelectedIndex].Interval != 5000)
            {
                slideInterval = Items[SelectedIndex].Interval;
            }
            slideTimer = new Timer { Interval = slideInterval, AutoReset = true };
            slideTimer.Elapsed += OnSlideTimerEvent;
            slideTimer.Start();
        }

        private async void OnSlideTimerEvent(object? source, ElapsedEventArgs e)
        {
            if (CarouselClass.Contains(CLS_CAROUSEL_HOVER, StringComparison.Ordinal) || !Loop && SelectedIndex == Items.Count - 1)
            {
                return;
            }
            if (Items != null && Items.Count > 0)
            {
                int index = (SelectedIndex + 1) % Items.Count;
                await InvokeAsync(async () => { await SetActiveSlide(index, CarouselSlideDirection.Next).ConfigureAwait(true); }).ConfigureAwait(true);
            }
        }

        private async void OnTransitionTimerEvent(object? source, ElapsedEventArgs e)
        {
            if (isDestroyed) return;
            if(PreviousIndex >= 0 && PreviousIndex < Items.Count)
            {
                Items[PreviousIndex].SlideClass = null;
            }
            if (SelectedIndex >= 0 && SelectedIndex < Items.Count)
            {
                Items[SelectedIndex].SlideClass = null;
                Items[SelectedIndex].SlideClass = CLS_ACTIVE;
            }
            isSlideChanged = false;
            ResetAutoPlay();
            await InvokeAsync(StateHasChanged).ConfigureAwait(true);
        }

        private void ResetAutoPlay()
        {
            if (!Loop && SelectedIndex == Items.Count - 1)
            {
                AutoPlay = false;
            }
            else if (!Loop && SelectedIndex != Items.Count - 1 && isPlayButtonClicked)
            {
                AutoPlay = true;
            }
            if (AutoPlay && !(!Loop && SelectedIndex == Items.Count - 1))
            {
                AutoSlide();
            }
        }

        private string GetAnimationClass()
        {
            string animationClass = CLS_ANIMATION_NONE;
            if (SyncfusionService.options.Animation == GlobalAnimationMode.Default || SyncfusionService.options.Animation == GlobalAnimationMode.Enable)
            {
                if (AnimationEffect == CarouselAnimationEffect.Slide || (AnimationEffect == CarouselAnimationEffect.None && SyncfusionService.options.Animation == GlobalAnimationMode.Enable))
                {
                    animationClass = CLS_SLIDE_ANIMATION;
                }
                else if (AnimationEffect == CarouselAnimationEffect.Fade)
                {
                    animationClass = CLS_FADE_ANIMATION;
                }
                else if (AnimationEffect == CarouselAnimationEffect.None)
                {
                    animationClass = CLS_ANIMATION_NONE;
                }
                else if (AnimationEffect == CarouselAnimationEffect.Custom)
                {
                    animationClass = CLS_CUSTOM_ANIMATION;
                }
            }
            return animationClass;
        }

        private string GetIndicatorClass()
        {
            string indicatorClass = CLS_DEFAULT_INDICATOR;
            if (IndicatorsTemplate != null)
            {
                return indicatorClass;
            }
            if (IndicatorsType == CarouselIndicatorsType.Default)
            {
                indicatorClass = CLS_DEFAULT_INDICATOR;
            }
            else if (IndicatorsType == CarouselIndicatorsType.Dynamic)
            {
                indicatorClass = CLS_DYNAMIC_INDICTOR;
            }
            else if (IndicatorsType == CarouselIndicatorsType.Fraction)
            {
                indicatorClass = CLS_FRACTION_INDICATOR;
            }
            else if (IndicatorsType == CarouselIndicatorsType.Progress)
            {
                indicatorClass = CLS_PROGRESS_INDICATOR;
            }
            return indicatorClass;
        }

        private string GetIndicatorActiveClass(int index)
        {
            string activeClass = string.Empty;
            if (index == SelectedIndex)
            {
                activeClass = CLS_ACTIVE;
            }
            return activeClass;
        }

        private void UpdateHtmlAttributes()
        {
            if (htmlAttributes != null)
            {
                if (htmlAttributes.TryGetValue("class", out object? clsValue))
                {
                    CarouselClass = SfBaseUtils.AddClass(CarouselClass, clsValue as string);
                    htmlAttributes.Remove("class");
                }
                if (htmlAttributes.TryGetValue("id", out object? id))
                {
                    ID = id as string ?? string.Empty;
                }
                if (htmlAttributes.TryGetValue("aria-label", out object? label))
                {
                    carouselLabel = label as string;
                }
            }
            BindMouseCallbackEvents();
        }

        private void AddOrUpdateItem(string key, object propertyValue)
        {
            if (htmlAttributes != null && !htmlAttributes.TryAdd(key, propertyValue))
            {
                htmlAttributes[key] = propertyValue;
            }
        }

        private void BindMouseCallbackEvents()
        {
            if (htmlAttributes == null)
            {
                htmlAttributes = new Dictionary<string, object>();
            }
            if (PauseOnHover || ButtonsVisibility == CarouselButtonVisibility.VisibleOnHover)
            {
                AddOrUpdateItem("onmouseover", EventCallback.Factory.Create<MouseEventArgs>(this, () => OnHoverActions("mouseenter")));
                AddOrUpdateItem("onmouseout", EventCallback.Factory.Create<MouseEventArgs>(this, () => OnHoverActions("mouseout")));
            }
            else
            {
                htmlAttributes.Remove("onmouseover");
                htmlAttributes.Remove("onmouseout");
            }
            if (AllowKeyboardInteraction)
            {
                AddOrUpdateItem("onkeydown", EventCallback.Factory.Create<KeyboardEventArgs>(this, (e) => OnKeyDown(e)));
            }
            else
            {
                htmlAttributes.Remove("onkeydown");
            }
            if (EnableTouchSwipe && SwipeMode != (~CarouselSwipeMode.Mouse & ~CarouselSwipeMode.Touch))
            {
                AddOrUpdateItem("ontouchstart", EventCallback.Factory.Create<TouchEventArgs>(this, (e) => OnTouchSwipeActions(e)));
                AddOrUpdateItem("ontouchend", EventCallback.Factory.Create<TouchEventArgs>(this, (e) => OnTouchSwipeActions(e)));
            }
            else
            {
                htmlAttributes.Remove("ontouchstart");
                htmlAttributes.Remove("ontouchend");
            }
        }

        private static string UpdateItemClass(Dictionary<string, object> itemAttributes)
        {
            string? attributeCls = null;
            if (itemAttributes != null)
            {
                if (itemAttributes.TryGetValue("class", out object? clsValue))
                {
                    attributeCls = clsValue as string;
                    itemAttributes.Remove("class");
                }
            }
            return attributeCls ?? string.Empty;
        }

        private async Task UpdateActiveSlide(int index)
        {
            if (Items?.Count > index)
            {
                Items[index].SlideClass = CLS_ACTIVE;
            }
            if (SelectedIndexChanged.HasDelegate && SelectedIndex != index)
            {
                SelectedIndex = selectedIndex = index;
                await SelectedIndexChanged.InvokeAsync(index).ConfigureAwait(true);
            }
            else
            {
                SelectedIndex = selectedIndex = index;
                StateHasChanged();
            }
        }

        internal void NotifyItemsInitialized()
        {
            StateHasChanged();
        }
    }
}
