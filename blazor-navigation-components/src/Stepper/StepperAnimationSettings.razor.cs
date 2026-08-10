using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components;
using System;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Represents animation settings for the <see cref="SfStepper" /> component.
    /// </summary>
    /// <remarks>
    /// The <see cref="StepperAnimationSettings"/> class provides properties to control the animation behavior of the <see cref="SfStepper" /> component.
    /// </remarks>
    /// <example> 
    /// A simple Stepper with animation settings.
    /// <code><![CDATA[
    /// <SfStepper>
    ///     <StepperSteps>
    ///         <StepperStep></StepperStep>
    ///         <StepperStep></StepperStep>
    ///         <StepperStep></StepperStep>
    ///         <StepperStep></StepperStep>
    ///         <StepperStep></StepperStep>
    ///     </StepperSteps>
    ///     <StepperAnimationSettings Enable=true Delay="500" Duration="2000"></StepperAnimationSettings>
    /// </SfStepper>
    /// ]]></code> 
    /// </example>
    public partial class StepperAnimationSettings : SfOwningComponentBase
    {
        #region Properties

        [CascadingParameter]
        [JsonIgnore]
        internal SfStepper Parent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether animation is enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if animation is enabled; otherwise, <c>false</c>. The default value is <c>true</c>.
        /// </value>
        /// <remarks>
        /// Enabling animations enhances the visual experience when transitioning between steps in the Stepper.
        /// </remarks>
        [Parameter]
        public bool Enable { get; set; } = true;

        /// <summary>
        /// Gets or sets the duration of animation in milliseconds.
        /// </summary>
        /// <value>
        /// The duration of animation in milliseconds. The default value is 2000 milliseconds.
        /// </value>
        /// <remarks>
        /// This property defines the time it takes for the animations to complete their transition effect.
        /// </remarks>
        [Parameter]
        public double Duration { get; set; } = 1000;

        /// <summary>
        /// Gets or sets the delay before animation start in milliseconds.
        /// </summary>
        /// <value>
        /// The delay before animation start in milliseconds. The default value is 0 milliseconds.
        /// </value>
        /// <remarks>
        /// A delay can be applied before animations start, providing control over the timing of the animation sequence.
        /// </remarks>
        [Parameter]
        public double Delay { get; set; } = 0;

        #endregion

        #region LifeCycle Methods

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await base.OnInitializedAsync().ConfigureAwait(true);
                Parent.UpdateAnimationSettings(this);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"Invalid operation error occurred: {e.Message}");
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            
            Parent?.UpdateAnimationSettings(new StepperAnimationSettings());
            if (disposing)
            {
                Parent = null;
            }
        }
        #endregion
    }
}
