using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// A class that represents the animation to appear while expanding the <see cref="AccordionItem"/>.
    /// </summary>
    /// <remarks>
    /// You can apply the animation effect and transform duration for accordion expand action by specifying value to corresponding property.
    /// </remarks>
    /// <example>
    /// <code><![CDATA[
    /// <SfAccordion>
    ///     <AccordionAnimationSettings>
    ///         <AccordionAnimationExpand Effect="AnimationEffect.FadeIn" Duration="300"></AccordionAnimationExpand>
    ///     </AccordionAnimationSettings>
    /// </SfAccordion> 
    /// ]]></code>
    /// </example>
    public partial class AccordionAnimationExpand : SfOwningComponentBase
    {
        [CascadingParameter]
        private AccordionAnimationSettings Parent { get; set; }

        /// <summary> 
        /// Gets or sets the time duration to transform content on expand action. 
        /// </summary> 
        /// <value> 
        /// If we set the duration value, then the content transforms with in specified duration otherwise the default duration value `400` is set.
        /// </value> 
        /// <remarks> 
        /// The interval value accepts in milliseconds. 
        /// </remarks>
        /// <example> 
        /// <code><![CDATA[ 
        /// <SfAccordion>
        ///     <AccordionAnimationSettings>
        ///         <AccordionAnimationExpand Duration="500"></AccordionAnimationExpand>
        ///     </AccordionAnimationSettings>
        /// </SfAccordion> 
        /// ]]></code>
        /// </example>
        [Parameter]
        public int Duration { get; set; } = 400;

        /// <summary>
        /// Gets or sets the easing effect applied when transforming the content.
        /// </summary>
        /// <value>
        /// If we set the easing value, then the specified easing effect is applied for expand action otherwise the default easing value <c>linear</c> is set.
        /// </value>
        /// <remarks>
        /// This property will accepts the `animation-timing-function` css values to apply content transition accordingly.   
        /// </remarks>
        [Parameter]
        public string Easing { get; set; } = "linear";

        /// <summary>
        /// Gets or sets the animation effect for accordion item expand action. 
        /// </summary> 
        /// <value> 
        /// One of the <see cref="AnimationEffect"/> enumeration. The default value is <see cref="AnimationEffect.SlideDown"/> 
        /// </value> 
        /// <remarks>
        /// Animation effect were disabled by setting <see cref="AnimationEffect.None"/> to <c>Effect</c> property.
        /// </remarks>
        /// <example> 
        /// <code><![CDATA[ 
        /// <SfAccordion>
        ///     <AccordionAnimationSettings>
        ///         <AccordionAnimationExpand Effect="AnimationEffect.SlideUp"></AccordionAnimationExpand>
        ///     </AccordionAnimationSettings>
        /// </SfAccordion> 
        /// ]]></code>
        /// </example>
        [Parameter]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AnimationEffect Effect { get; set; } = AnimationEffect.SlideDown;

        /// <summary>
        /// Method invoked when the component is ready to start.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(true);
            Parent.UpdateExpandProperties(this);
        }

        /// <summary>
        /// Dispose unmanaged resources in the Syncfusion Blazor component.
        /// </summary>
        /// <param name="disposing">Boolean value to dispose the object.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                Parent = null;
            }
        }
    }
}