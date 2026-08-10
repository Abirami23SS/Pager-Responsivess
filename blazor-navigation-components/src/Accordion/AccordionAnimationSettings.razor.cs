using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// A class that represents the animations to appear on expand and collapse action of <see cref="AccordionItem"/>.
    /// </summary>
    /// <remarks>
    /// <see cref="AccordionAnimationExpand"/> and <see cref="AccordionAnimationCollapse"/> can be used to set expand and collapse animation for accordion item respectively.
    /// </remarks>
    /// <example>
    /// <code><![CDATA[
    /// <SfAccordion>
    ///     <AccordionAnimationSettings>
    ///         <AccordionAnimationExpand Effect="AnimationEffect.FadeIn" Duration="500"></AccordionAnimationExpand>
    ///         <AccordionAnimationCollapse Effect="AnimationEffect.FadeOut" Duration="500"></AccordionAnimationCollapse>
    ///     </AccordionAnimationSettings>
    /// </SfAccordion> 
    /// ]]></code>
    /// </example>
    public partial class AccordionAnimationSettings : SfOwningComponentBase
    {
        [CascadingParameter]
        private SfAccordion Parent { get; set; }

        /// <summary>
        /// Gets or sets the Child Content for the Accordion Animation Settings.
        /// </summary>
        [Parameter]
        [JsonIgnore]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Gets or sets the animation to appear while collapsing the <see cref="AccordionItem"/>.
        /// </summary>
        /// <value>
        /// If we set the collapse animation, then the provided <see cref="AccordionAnimationCollapse"/> value is applied for collapse action, otherwise the default `null` value is set.
        /// </value>
        public AccordionAnimationCollapse Collapse { get; set; }

        /// <summary>
        /// Gets or sets the animation to appear while expanding the <see cref="AccordionItem"/>.
        /// </summary>
        /// <value>
        /// If we set the expand animation, then the provided <see cref="AccordionAnimationExpand"/> value is applied for expand action, otherwise the default `null` value is set.
        /// </value>
        public AccordionAnimationExpand Expand { get; set; }

        internal void UpdateExpandProperties(AccordionAnimationExpand animation)
        {
            Expand = animation ?? new AccordionAnimationExpand();
        }

        internal void UpdateCollapseProperties(AccordionAnimationCollapse animation)
        {
            Collapse = animation ?? new AccordionAnimationCollapse();
        }

        /// <summary>
        /// Method invoked when the component is ready to start.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(true);
            Parent.UpdateAnimationProperties(this);
            UpdateExpandProperties(Expand);
            UpdateCollapseProperties(Collapse);
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