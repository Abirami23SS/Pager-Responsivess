using System;
using System.Threading.Tasks;


namespace Syncfusion.Blazor.Navigations
{
    public partial class SfStepper
    {

        #region Public Methods

        /// <summary>
        /// Moves the Stepper to the next step from the current step.
        /// </summary>
        /// <returns><see cref="System.Threading.Tasks.Task"/>.</returns>
        public async Task NextStepAsync()
        {            
            try
            {
                if (activeStep != this.Steps.Count)
                {
                    await UpdateStepperValue(dataId, activeStep + 1).ConfigureAwait(true);
                }
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine($"TimeoutException occurred: {ex.Message}");
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine($"Null reference error occurred: {e.Message}");
            }
        }

        /// <summary>
        /// Moves the Stepper to the previous step from the current step.
        /// </summary>
        /// <returns><see cref="System.Threading.Tasks.Task"/>.</returns>
        public async Task PreviousStepAsync()
        {
            try
            {
                if (activeStep > 0)
                {
                    await UpdateStepperValue(dataId, activeStep - 1).ConfigureAwait(true);
                }
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine($"TimeoutException occurred: {ex.Message}");
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine($"Null reference error occurred: {e.Message}");
            }
        }

        /// <summary>
        /// Resets the state of the Stepper and navigates to the first step.
        /// </summary>
        /// <returns><see cref="System.Threading.Tasks.Task"/>.</returns>
        public async Task ResetAsync()
        {
            try
            {
                if (activeStep != 0)
                {
                    await UpdateStepperValue(dataId, 0).ConfigureAwait(true);
                }
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine($"TimeoutException occurred: {ex.Message}");
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine($"Null reference error occurred: {e.Message}");
            }
        }

        /// <summary>
        /// Refreshes the position of the progress bar programmatically when the dimensions of the parent container are changed.
        /// </summary>
        /// <returns><see cref="System.Threading.Tasks.Task"/>.</returns>
        public async Task RefreshProgressbarAsync()
        {
            try
            {
                await InvokeMethod("sfBlazor.Stepper.refreshProgressbar", dataId, activeStep).ConfigureAwait(true);
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine($"TimeoutException occurred: {ex.Message}");
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine($"Null reference error occurred: {e.Message}");
            }
        }

        #endregion
    }
}
