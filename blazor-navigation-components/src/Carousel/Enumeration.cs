using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Specifies how to display the navigation buttons in <see cref="SfCarousel"/> component.
    /// </summary>
    public enum CarouselButtonVisibility
    {
        /// <summary>
        /// Hides the navigation buttons and play button.
        /// </summary>
        Hidden,

        /// <summary>
        /// Shows the navigation buttons and play button always.
        /// </summary>
        Visible,

        /// <summary>
        /// Shows the navigation buttons and play button only when mouse enters the carousel and hides when mouse leaves.
        /// </summary>
        VisibleOnHover
    }

    /// <summary>
    /// Specifies the animation effect which need to be applied on transition of <see cref="SfCarousel"/> component.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CarouselAnimationEffect
    {
        /// <summary>
        /// Applies no animation effect on slide transitions.
        /// </summary>
        None,

        /// <summary>
        /// Applies slide animation effect on slide transitions.
        /// </summary>
        Slide,

        /// <summary>
        /// Applies fade animation effect on slide transitions.
        /// </summary>
        Fade,

        /// <summary>
        /// Applies custom animation effect on slide transitions.
        /// </summary>
        Custom
    }

    /// <summary>
    /// Specifies the slide direction in which transition of <see cref="SfCarousel"/> component occurs.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CarouselSlideDirection
    {
        /// <summary>
        /// Applies when slide transition towards next direction.
        /// </summary>
        Next,

        /// <summary>
        /// Applies when slide transition towards previous direction.
        /// </summary>
        Previous
    }

    /// <summary> 
    /// Enables or disables the slide swiping action through Touch and Mouse.
    /// </summary>
    /// <remarks>
    /// The slide swiping is enabled or disabled using bitwise operators. The swiping is disabled using ‘~’ bitwise operator.
    /// </remarks> 
    /// <example> 
    /// <code lang="Razor"> 
    /// <![CDATA[ 
    /// <SfCarousel SwipeMode="CarouselSwipeMode.Touch & CarouselSwipeMode.Mouse"> 
    /// </SfCarousel> 
    /// ]]>
    /// </code> 
    /// </example>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [Flags]
    public enum CarouselSwipeMode
    {
        /// <summary>
        /// Enables or disables the Touch swiping.
        /// </summary>
        Touch = 1 << 0,
        
        /// <summary> 
        /// Enables or disables the slide swiping through Mouse.
        /// </summary>
        Mouse = 1 << 1,
    }
    /// <summary>
    /// Specifies the types of the indicators of <see cref="SfCarousel"/> component.
    /// </summary>
    public enum CarouselIndicatorsType
    {
        /// <summary> 
        /// Indicates the indicators with a bullet design. 
        /// </summary> 
        Default,

        /// <summary> 
        /// Applies a dynamic animation design to the indicators. 
        /// </summary> 
        Dynamic,

        /// <summary> 
        /// Indicates the slides numerically as indicators. 
        /// </summary> 
        Fraction,

        /// <summary> 
        /// Indicates the indicators using a progress bar design. 
        /// </summary> 
        Progress
    }
}
