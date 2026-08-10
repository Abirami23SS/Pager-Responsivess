using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// A class that represents the animations to appear while activating the <see cref="TabItem"/>.
    /// </summary>
    /// <remarks>
    /// <see cref="TabAnimationPrevious"/> and <see cref="TabAnimationNext"/> can be used to set previous and next animation for tab item respectively.
    /// </remarks>
    /// <example>
    /// <code><![CDATA[
    /// <SfTab>
    ///     <TabAnimationSettings>
    ///         <TabAnimationPrevious Effect="AnimationEffect.FadeIn" Duration="500"></TabAnimationPrevious>
    ///         <TabAnimationNext Effect="AnimationEffect.FadeOut" Duration="500"></TabAnimationNext>
    ///     </TabAnimationSettings>
    /// </SfTab>
    /// ]]></code>
    /// </example>
    public partial class TabAnimationSettings : SfOwningComponentBase
    {
        [CascadingParameter]
        internal SfTab Parent { get; set; }

        /// <summary>
        /// Gets or sets the Child Content for Tab Animation Settings.
        /// </summary>
        [Parameter]
        [JsonIgnore]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Gets or sets the animation to appear while moving to the next tab content.
        /// </summary>
        /// <value>
        /// If we set the next animation, then the provided <see cref="TabAnimationNext"/> value is applied for next action, otherwise the default `null` value is set.
        /// </value>
        [Parameter]
        public TabAnimationNext Next { get; set; }

        /// <summary>
        /// Gets or sets the animation to appear while moving to the previous tab content.
        /// </summary>
        /// <value>
        /// If we set the previous animation, then the provided <see cref="TabAnimationPrevious"/> value is applied for previous action, otherwise the default `null` value is set.
        /// </value>
        [Parameter]
        public TabAnimationPrevious Previous { get; set; }

        internal void UpdatePreviousProperties(TabAnimationPrevious animation)
        {
            Previous = animation ?? new TabAnimationPrevious();
        }

        internal void UpdateNextProperties(TabAnimationNext animation)
        {
            Next = animation ?? new TabAnimationNext();
        }

        /// <summary>
        /// Method invoked when the component is ready to start.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(true);
            Parent.UpdateAnimationProperties(this);
            UpdateNextProperties(Next);
            UpdatePreviousProperties(Previous);
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
                ChildContent = null;
            }
        }
    }
}