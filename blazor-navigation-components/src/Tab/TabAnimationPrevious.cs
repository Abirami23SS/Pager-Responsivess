using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Specifies the animation to appear when moving to the previous Tab content.
    /// </summary>
    /// <summary>
    /// A class that represents the animation to appear while moving to the previous tab content.
    /// </summary>
    /// <remarks>
    /// You can apply the animation effect and transform duration while switching to previous <see cref="TabItem"/> by specifying value to corresponding property.
    /// </remarks>
    /// <example>
    /// <code><![CDATA[
    /// <SfTab>
    ///     <TabAnimationSettings>
    ///         <TabAnimationPrevious Effect="AnimationEffect.FadeIn" Duration="500"></TabAnimationPrevious>
    ///     </TabAnimationSettings>
    /// </SfTab>
    /// ]]></code>
    /// </example>
    public partial class TabAnimationPrevious : SfOwningComponentBase
    {
        private int duration;
        private string? easing;
        private AnimationEffect effect;

        [CascadingParameter]
        internal TabAnimationSettings Parent { get; set; }

        [CascadingParameter]
        internal SfTab BaseParent { get; set; }

        /// <summary> 
        /// Gets or sets the time duration to transform content while switching to previous tab item. 
        /// </summary> 
        /// <value> 
        /// If we set the duration value, then the content transforms with in specified duration otherwise the default duration value `600` is set.
        /// </value>
        /// <remarks> 
        /// The interval value accepts in milliseconds. 
        /// </remarks>
        [Parameter]
        public int Duration { get; set; } = 600;

        /// <summary>
        /// Gets or sets the easing effect applied when transforming the content.
        /// </summary>
        /// <value>
        /// If we set the easing value, then the specified easing effect is applied when switching to previous tab otherwise the default easing value <c>ease</c> is set.
        /// </value>
        /// <remarks>
        /// This property will accepts the `animation-timing-function` css values to apply content transition accordingly.   
        /// </remarks>
        [Parameter]
        public string Easing { get; set; } = "ease";

        /// <summary> 
        /// Gets or sets the animation effect for displaying the previous tab content. 
        /// </summary> 
        /// <value> 
        /// One of the <see cref="AnimationEffect"/> enumeration. The default value is <see cref="AnimationEffect.SlideLeftIn"/> 
        /// </value> 
        /// <remarks>
        /// Animation effect were disabled by setting <see cref="AnimationEffect.None"/> to <c>Effect</c> property.
        /// </remarks>
        [Parameter]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AnimationEffect Effect { get; set; }

        /// <summary>
        /// Method invoked when the component is ready to start.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(true);
            duration = Duration;
            easing = Easing;
            effect = Effect;
            Parent.UpdatePreviousProperties(this);
        }

        /// <summary>
        /// Method invoked when the component has received parameters from its parent in the render tree,
        /// and the incoming values have been assigned to properties.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync().ConfigureAwait(true);
            if (Duration != duration || Easing != easing || Effect != effect)
            {
                duration = Duration;
                easing = Easing;
                effect = Effect;
                BaseParent.IsTabItemChanged = true;
            }
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
                BaseParent = null;
            }
        }
    }
}