using System.ComponentModel;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Syncfusion.Blazor.Navigations
{
    public partial class SfCarousel : SfBaseComponent
    {
        /// <exclude />
        /// <summary>
        /// Gets or sets the child content of Carousel component.
        /// </summary>
        [Parameter]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public RenderFragment ChildContent { get; set; }

        /// <summary> 
        /// Gets or sets whether the slide transition is automatic or manual. 
        /// </summary> 
        /// <value> 
        /// false, the slide transition has been stopped otherwise the slide transition plays. The default value is `true`. 
        /// </value> 
        [Parameter]
        public bool AutoPlay { get; set; } = true;

        /// <summary> 
        /// Gets or sets the custom classes to customize the Carousel component.  
        /// </summary> 
        /// <value> 
        /// If we set the css class, then the custom class is applied for carousel. The default value is `null`. 
        /// </value> 
        /// <example> 
        /// <code><![CDATA[ 
        /// <SfCarousel CssClass="custom-carousel e-custom-animation"></SfCarousel> 
        /// ]]></code> 
        /// </example>
        [Parameter]
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets whether to persist component's state between page reloads.
        /// </summary>
        /// <value> 
        /// true, the <see cref="SelectedIndex" /> property is persisted. The default value is `false`. 
        /// </value>
        /// <remarks> 
        /// To persist the <see cref="SelectedIndex" /> property, it is mandatory to provide the <see cref="ID" /> property.
        /// </remarks> 
        /// <example>
        /// <code><![CDATA[ 
        /// <SfCarousel Id="CarouselSlide" EnablePersistence="true"></SfCarousel> 
        /// ]]></code> 
        /// </example> 
        [Parameter]
        public bool EnablePersistence { get; set; }

        /// <summary>
        /// Gets or sets whether the right to left direction is enabled for Carousel component.
        /// </summary>
        /// <value> 
        /// true, the right to left direction is enabled for carousel component. The default value is `false`. 
        /// </value> 
        [Parameter]
        public bool EnableRtl { get; set; }

        /// <summary> 
        /// Gets or sets whether the slide transition should occur or not when performing swipe action. 
        /// </summary> 
        /// <value> 
        /// true, the slide transition with swipe action in touch screen works otherwise touch swipe action does not work. The default value is `true`. 
        /// </value>
        [Parameter]
        public bool EnableTouchSwipe { get; set; } = true;

        /// <summary> 
        /// Gets or sets the ability to use keyboard input in the Carousel. 
        /// </summary> 
        /// <value> 
        /// A boolean value indicating whether keyboard interaction is enabled. The default value is `true`. 
        /// </value> 
        /// <remarks> 
        /// If any form input component is placed on the carousel slide, interacting with it may cause  
        /// the left/right arrow keys to navigate to other slides. Disabling keyboard interaction helps  
        /// prevent this unintended navigation, leading to a smoother user experience. 
        /// </remarks> 
        [Parameter]
        public bool AllowKeyboardInteraction { get; set; } = true;

        /// <summary> 
        /// Gets or sets the height of the Carousel in pixels/number/percentage. Number value is considered as pixels. 
        /// </summary> 
        /// <value> 
        /// If we set the height value, then the carousel will render based on specified height otherwise the default height value `auto` is set.  
        /// </value> 
        /// <example> 
        /// <code><![CDATA[ 
        /// <SfCarousel Height="500px"></SfCarousel> 
        /// ]]></code> 
        /// </example> 
        [Parameter]
        public string Height { get; set; } = "100%";

        /// <summary> 
        /// Gets or sets a collection of additional attributes that will applied to the carousel element. 
        /// </summary> 
        /// <remarks> 
        /// Additional attributes can be added by specifying <c>HtmlAttributes</c> directive. 
        /// </remarks> 
        /// <value> 
        /// It allows the carousel component to render non-declared attributes. The default value is `null`. 
        /// </value>
        /// <example> 
        /// <code><![CDATA[ 
        /// <SfCarousel HtmlAttributes="customAttribute"></SfCarousel> 
        /// @code{ 
        ///    Dictionary<string, object> customAttribute = new Dictionary<string, object>() 
        ///    { 
        ///        { "aria-label", "Slide show of current News" } 
        ///    }; 
        /// } 
        /// ]]></code> 
        /// </example> 
        [Parameter(CaptureUnmatchedValues = true)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Dictionary<string, object> HtmlAttributes { get { return htmlAttributes; } set { htmlAttributes = value; } }

        /// <summary>
        /// Gets or sets the unique Id value for carousel component.
        /// </summary>
        /// <value>
        /// If we set the id, then the id value set for carousel element. The default value is `null`.
        /// </value>
        /// <example>
        /// <code><![CDATA[ 
        /// <SfCarousel Id="CarouselForNews"></SfCarousel> 
        /// ]]></code> 
        /// </example> 
        [Parameter]
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the animation effects applies to the slide transition. 
        /// </summary>
        /// <value>
        /// One of the <see cref="CarouselAnimationEffect" /> enumeration. The default value is <see cref="CarouselAnimationEffect.Slide"/>
        /// </value>
        /// <example> 
        /// <code><![CDATA[ 
        /// <SfCarousel AnimationEffect="CarouselAnimationEffect.Fade">
        ///     <CarouselItem><div>Slide 1</div></CarouselItem>
        ///     <CarouselItem><div>Slide 2</div></CarouselItem>
        ///     <CarouselItem><div>Slide 3</div></CarouselItem>
        /// </SfCarousel> 
        /// ]]></code> 
        /// </example> 
        [Parameter]
        public CarouselAnimationEffect AnimationEffect { get; set; } = CarouselAnimationEffect.Slide;

        /// <summary> 
        /// Gets or sets whether the slide transition should loop or end when reaches the last slide of the carousel. 
        /// </summary> 
        /// <value> 
        /// false, the slide transition has been stopped at end of the last slide otherwise the slide transition plays continuously. The default value is `true`. 
        /// </value> 
        [Parameter]
        public bool Loop { get; set; } = true;

        /// <summary> 
        /// Gets or sets the amount of delay time to transition slides automatically. 
        /// </summary> 
        /// <value> 
        /// If we set the interval value, then the slide transition begins after the specified time interval otherwise the default interval value 5000 is set.  
        /// </value> 
        /// <remarks> 
        /// The interval value accepts in milliseconds. 
        /// </remarks> 
        /// <example> 
        /// <code><![CDATA[ 
        /// <SfCarousel Interval="4000"></SfCarousel> 
        /// ]]></code>
        /// </example> 
        [Parameter]
        public int Interval { get; set; } = 5000;

        /// <summary>
        /// Gets or sets whether the partial slides are rendered or not. 
        /// </summary>
        /// <value>
        /// true, the partial slides are rendered. The default value is `false`.
        /// </value>
        /// <remarks>Shows the next and previous slides partially. So, user can identify that more slides are yet to display. Slide animation only applicable if the PartialVisible is enabled.
        /// </remarks>
        /// In the below example, Previous/next slides visible range can be customized using the css class.
        /// <example> 
        /// <code><![CDATA[ 
        /// <SfCarousel PartialVisible="true"></SfCarousel> 
        /// <style 
        ///  .e-partial .e-carousel-slide-container {
        ///    padding: 0 150px;
        ///  }
        /// </style>
        /// ]]></code> 
        /// </example>
        [Parameter]
        public bool PartialVisible { get; set; } = false;

        /// <summary> 
        /// Gets or sets whether the slide transition should occur or not when performing Touch/Mouse swipe action. 
        /// </summary> 
        /// <value> 
        /// One of the <see cref="CarouselSwipeMode" /> enumeration values that represents the swiping action for the Carousel slides. The default value is <see cref="CarouselSwipeMode.Touch" />. 
        /// </value> 
        [Parameter]
        [DefaultValue(CarouselSwipeMode.Touch)]
        public CarouselSwipeMode SwipeMode { get; set; } = CarouselSwipeMode.Touch;

        /// <summary> 
        /// Gets or sets the types of the carousel indicators.  
        /// </summary> 
        /// <value> 
        /// One of the <see cref="CarouselIndicatorsType" /> enumeration. The default value is <see cref="CarouselIndicatorsType.Default"/> 
        /// </value> 
        /// <example>  
        /// <code><![CDATA[  
        /// <SfCarousel IndicatorsType="CarouselIndicatorsType.Fraction"> 
        ///     <CarouselItem><div>Slide 1</div></CarouselItem> 
        ///     <CarouselItem><div>Slide 2</div></CarouselItem> 
        ///     <CarouselItem><div>Slide 3</div></CarouselItem> 
        /// </SfCarousel>  
        /// ]]></code>  
        /// </example>  
        [Parameter]
        [DefaultValue(CarouselIndicatorsType.Default)]
        public CarouselIndicatorsType IndicatorsType { get; set; } = CarouselIndicatorsType.Default;

        /// <summary> 
        /// Gets or sets the index of the current carousel item. 
        /// </summary> 
        /// <value> 
        /// If we set the index value, then the slides begin from specified index otherwise the default index value 0 is set.  
        /// </value> 
        /// <example> 
        /// <code><![CDATA[ 
        /// <SfCarousel SelectedIndex="1"></SfCarousel> 
        /// ]]></code> 
        /// </example> 
        [Parameter]
        public int SelectedIndex { get; set; }

        /// <summary>
        /// Invokes when index of the active slide changed.
        /// </summary>
        /// <value> 
        /// Fired when SelectedSlideIndex changes. 
        /// </value>
        [Parameter]
        public EventCallback<int> SelectedIndexChanged { get; set; }

        /// <summary> 
        /// Gets or sets whether to show previous/next navigation buttons or not. 
        /// </summary> 
        /// <value> 
        /// One of the <see cref="CarouselButtonVisibility"/> enumeration. The default value is <see cref="CarouselButtonVisibility.Visible"/> 
        /// </value> 
        /// <example>
        /// <code><![CDATA[ 
        /// <SfCarousel ButtonsVisibility="CarouselButtonVisibility.VisibleOnHover"></SfCarousel> 
        /// ]]></code> 
        /// </example> 
        [Parameter]
        public CarouselButtonVisibility ButtonsVisibility { get; set; } = CarouselButtonVisibility.Visible;

        /// <summary> 
        /// Gets or sets whether to show indicators or not. 
        /// </summary> 
        /// <value> 
        /// false, the slide transition indicators will not be shown otherwise the indicators is shown. The default value is `true`. 
        /// </value> 
        [Parameter]
        public bool ShowIndicators { get; set; } = true;

        /// <summary> 
        /// Gets or sets whether to show play button or not to play or pause the transitions. 
        /// </summary> 
        /// <value> 
        /// true, the slide transition play/pause button will be shown otherwise the button is not shown. The default value is `false`. 
        /// </value> 
        [Parameter]
        public bool ShowPlayButton { get; set; }

        /// <summary> 
        /// Gets or sets the width of the Carousel in pixels/number/percentage. Number value is considered as pixels. 
        /// </summary> 
        /// <value> 
        /// If we set the width value, then the carousel will render based on specified width otherwise the default width value `auto` is set.  
        /// </value> 
        /// <example> 
        /// <code><![CDATA[ 
        /// <SfCarousel Width="500px"></SfCarousel> 
        /// ]]></code> 
        /// </example> 
        [Parameter]
        public string Width { get; set; } = "100%";

        /// <summary>
        /// Gets or sets template as <see cref="RenderFragment"/>, that defines custom appearance of previous navigation button.
        /// </summary>
        /// <value>
        /// A template content that specifies the visualization of previous navigation button. The default value is <c>null</c>.
        /// </value>        
        /// <example>
        /// In the below code example, previous navigation button are customized with chevron double icons.
        /// <code><![CDATA[
        /// <SfCarousel>
        ///     <CarouselItem><div>Slide 1</div></CarouselItem>
        ///     <CarouselItem><div>Slide 2</div></CarouselItem>
        ///     <CarouselItem><div>Slide 3</div></CarouselItem>
        ///     <PreviousButtonTemplate>
        ///         <SfButton CssClass="e-flat e-round" IconCss="e-icons e-chevron-left-double"></SfButton >
        ///     </PreviousButtonTemplate>
        /// </SfCarousel>
        /// ]]></code>
        /// </example>
        [Parameter]
        public RenderFragment PreviousButtonTemplate { get; set; }

        /// <summary>
        /// Gets or sets template as <see cref="RenderFragment"/>, that defines custom appearance of next navigation button.
        /// </summary>
        /// <value>
        /// A template content that specifies the visualization of next navigation button. The default value is <c>null</c>.
        /// </value>        
        /// <example>
        /// In the below code example, next navigation button are customized with chevron double icons.
        /// <code><![CDATA[
        /// <SfCarousel>
        ///     <CarouselItem><div>Slide 1</div></CarouselItem>
        ///     <CarouselItem><div>Slide 2</div></CarouselItem>
        ///     <CarouselItem><div>Slide 3</div></CarouselItem>
        ///     <NextButtonTemplate>
        ///         <SfButton CssClass="e-flat e-round" IconCss="e-icons e-chevron-right-double"></SfButton >
        ///     </NextButtonTemplate>
        /// </SfCarousel>
        /// ]]></code>
        /// </example>
        [Parameter]
        public RenderFragment NextButtonTemplate { get; set; }

        /// <summary>
        /// Gets or sets template as <see cref="RenderFragment"/>, that defines custom appearance of indicators.
        /// Here, context refers to the Index and SelectedIndex value.
        /// </summary>
        /// <value>
        /// A template content that specifies the visualization of indicators. The default value is <c>null</c>.
        /// </value>
        /// <remarks>
        /// Use the template's context parameter <c>Index</c> which returns current index of the slide and <c>SelectedIndex</c> which returns the index of active slide.
        /// With the <c>SelectedIndex</c> parameter, you can differentiate the active indicator.
        /// </remarks>        
        /// <example>
        /// <code><![CDATA[
        /// <SfCarousel>
        ///     <CarouselItem><div>Slide 1</div></CarouselItem>
        ///     <CarouselItem><div>Slide 2</div></CarouselItem>
        ///     <CarouselItem><div>Slide 3</div></CarouselItem>
        ///     <IndicatorsTemplate>
        ///         @if(context.SelectedIndex == context.Index)
        ///         {
        ///             <div class="indicator active" indicator-index="@context.Index"></div>
        ///         }
        ///         else
        ///         {
        ///             <div class="indicator" indicator-index="@context.Index"></div>
        ///         }
        ///     </IndicatorsTemplate>
        /// </SfCarousel>
        /// ]]></code>
        /// </example>
        [Parameter]
        public RenderFragment<IndicatorsTemplateContext> IndicatorsTemplate { get; set; }

        /// <summary>
        /// Gets or sets template as <see cref="RenderFragment"/>, that defines custom appearance of play/pause button.
        /// </summary>
        /// <value>
        /// A template content that specifies the visualization of play/pause button. The default value is <c>null</c>.
        /// </value>
        /// <remarks>
        /// Based on AutoPlay value or click action of the play/pause button, you can decide whether the play or pause button to be rendered.
        /// </remarks>        
        /// <example>
        /// <code><![CDATA[
        /// <SfCarousel @ref="CarouselRef">
        ///     <CarouselItem><div>Slide 1</div></CarouselItem>
        ///     <CarouselItem><div>Slide 2</div></CarouselItem>
        ///     <CarouselItem><div>Slide 3</div></CarouselItem>
        ///     <PlayButtonTemplate>
        ///         <SfButton CssClass="e-flat e-round" IconCss="@playPauseIcon" @onclick="@OnPlayClick" IsToggle="true"></SfButton>
        ///     </PlayButtonTemplate>
        /// </SfCarousel>
        /// @code{
        ///    SfCarousel CarouselRef;
        ///    private string playPauseIcon = "e-icons e-pause";
        ///    private void OnPlayClick()
        ///     {
        ///         if (!CarouselRef.AutoPlay)
        ///         {
        ///             playPauseIcon = "e-icons e-pause";
        ///             CarouselRef.Play();
        ///         }
        ///         else
        ///         {
        ///             playPauseIcon = "e-icons e-play";
        ///             CarouselRef.Pause();
        ///         }
        ///     }
        /// }
        /// ]]></code>
        /// </example>
        [Parameter]
        public RenderFragment PlayButtonTemplate { get; set; }

        /// <summary>
        /// Gets or sets whether the auto play slides pause on mouse hover or not.
        /// </summary>
        /// <value>
        /// false, the slide transition has been played on mouse enters to carousel otherwise the slide transition paused. The default value is `true`.
        /// </value>
        /// <remarks>
        /// This property applicable when <see cref="AutoPlay"/> value is true.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// <SfCarousel PauseOnHover="false"></SfCarousel>
        /// ]]></code>
        /// </example>
        [Parameter]
        public bool PauseOnHover { get; set; } = true;
    }

    /// <summary>
    /// A class that holds the options for the IndictorsTemplate.
    /// </summary>
    public class IndicatorsTemplateContext
    {
        /// <summary>
        /// Returns the current slide index.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Returns the active slide index.
        /// </summary>
        public int SelectedIndex { get; set; }
    }

}
