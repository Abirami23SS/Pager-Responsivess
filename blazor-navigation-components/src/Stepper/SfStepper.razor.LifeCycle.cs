using Syncfusion.Blazor.Internal;
using System;
using System.Threading.Tasks;

namespace Syncfusion.Blazor.Navigations
{
    public partial class SfStepper
    {
        #region LifeCycle Methods
        protected override async Task OnInitializedAsync()
        {
            try
            {
                dataId = SfBaseUtils.GenerateID("stepper");
                UpdateProperties();
                await base.OnInitializedAsync().ConfigureAwait(true);
                ScriptModules = SfScriptModules.SfStepper;
                ID = string.IsNullOrEmpty(ID) ? dataId + "_container" : ID;
                IsInitialRender = true;
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine($"Null reference error occurred: {e.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Invalid operation error occurred: {ex.Message}");
            }
        }

        /// <inheritdoc/>
        protected override async Task OnParametersSetAsync()
        {
            try
            {
                await base.OnParametersSetAsync().ConfigureAwait(true);
                stepperClass = SPACE + "e-" + GetOrientationClassName(Orientation);
                if (!string.IsNullOrEmpty(CssClass))
                {
                    stepperClass += SPACE + CssClass;
                }
                if (SyncfusionService.options.EnableRtl)
                {
                    stepperClass += SPACE + "e-rtl";
                }
                if (ReadOnly)
                {
                    stepperClass += SPACE + "e-stepper-readonly";
                }
                if (StepType != StepperType.Default)
                {
                    stepperClass += SPACE + "e-step-type-" + GetStepTypeClassName(StepType);
                }
                stepperClass += SPACE + (StepType != StepperType.Indicator ? "e-label-" + @UpdateLabelClass() : "");                
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Invalid operation error occurred: {ex.Message}");
            }
        }

        /// <inheritdoc/>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                await base.OnAfterRenderAsync(firstRender).ConfigureAwait(true);
                if (firstRender)
                {
                    stepLength = Steps?.Count ?? 0;
                    await InvokeMethod("sfBlazor.Stepper.initialize", this.GetInstance()).ConfigureAwait(true);
                    if (Created.HasDelegate)
                    {
                        await Created.InvokeAsync(null).ConfigureAwait(true);
                    }
                    IsInitialRender = false;
                }
                else
                {
                    if ((stepLength != Steps.Count) && IsInitialRender == false)
                    {
                        bool isAdd = stepLength < Steps.Count;
                        int countDiff = isAdd ? 0 : Math.Abs(stepLength - Steps.Count);
                        stepLength = Steps.Count;
                        await RefreshComponent().ConfigureAwait(true);
                        await InvokeMethod("sfBlazor.Stepper.updateStepLength", dataId, isAdd, countDiff).ConfigureAwait(true);
                    }
                }
                if (ActiveStep != initialVal)
                {
                    activeStep = initialVal = (isUpdateProp || StepChanged.HasDelegate) ? NotifyPropertyChanges(nameof(ActiveStep), ActiveStep, activeStep) : activeStep;
                }
                else
                {
                    ActiveStep = activeStep;
                }
                readOnly = NotifyPropertyChanges(nameof(ReadOnly), ReadOnly, readOnly);
                linear = NotifyPropertyChanges(nameof(Linear), Linear, linear);
                showTooltip = NotifyPropertyChanges(nameof(ShowTooltip), ShowTooltip, showTooltip);
                labelPosition = NotifyPropertyChanges(nameof(LabelPosition), LabelPosition, labelPosition);
                orientation = NotifyPropertyChanges(nameof(Orientation), Orientation, orientation);
                stepType = NotifyPropertyChanges(nameof(StepType), StepType, stepType);
                if (!isCancel)
                {
                    if (PropertyChanges.Count > 0)
                    {
                        await OnPropertyChangeHandler().ConfigureAwait(true);
                    }
                    await Task.Delay(50).ConfigureAwait(true); // Added delay for status change in step click.
                    if (statusIndex != 0 && stepStatus)
                    {
                        var stepperObj = GetInstance();
                        stepperObj["stepperStatus"] = status;
                        await InvokeMethod("sfBlazor.Stepper.updateStepperProps", stepperObj).ConfigureAwait(true);
                        stepStatus = false;
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Invalid operation error occurred: {ex.Message}");
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine($"Null reference error occurred: {e.Message}");
            }
            catch (TimeoutException te)
            {
                Console.WriteLine($"TimeoutException occurred: {te.Message}");
            }
        }

        #endregion
    }
}
