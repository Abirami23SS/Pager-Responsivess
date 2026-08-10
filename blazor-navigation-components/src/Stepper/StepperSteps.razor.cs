using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.ComponentModel;
using System;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Represents a collection of <see cref="StepperStep"/>.
    /// </summary>
    public partial class StepperSteps
    {
        #region Properties

        [CascadingParameter]
        internal SfStepper Stepper { get; set; }

        /// <exclude/>
        /// <summary>
        /// Gets or sets the child content for the StepperStep (Child) from the Stepper(Parent) .
        /// </summary>
        [Parameter]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public RenderFragment ChildContent { get; set; }

        private List<StepperStep> steps { get; set; } = new List<StepperStep>();
        #endregion

        #region Methods

        /// <summary>
        /// Updates the Stepper with list of steps from the StepperStep tag directive when they are rendered.
        /// </summary>
        internal void UpdateChildProperty(StepperStep step)
        {
            steps.Add(step);
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await base.OnInitializedAsync().ConfigureAwait(true);
                Stepper.UpdateSteps(steps);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"Invalid operation error occurred: {e.Message}");
            }
        }

        /// <summary>
        /// Updates the Stepper with list of steps from the StepperStep tag directive when they are removed.
        /// </summary>
        internal void RemoveChildProperty(StepperStep step)
        {
            steps.Remove(step);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                Stepper = null;
                ChildContent = null;
            }
        }
        #endregion
    }
}
