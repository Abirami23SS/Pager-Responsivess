using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Syncfusion.Blazor.Navigations
{
    public partial class SfStepper
    {
        #region Non Browsable Public Methods

        /// <summary>
        /// Update the current value of the stepper on click.
        /// </summary>
        /// <param name="prevStep">previuos value of the stepper.</param>
        /// <param name="currStep">current value of the stepper.</param>
        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async void StepClickHandler(int prevStep, int currStep)
        {
            try
            {
                if (StepClicked.HasDelegate)
                {
                    isUpdateProp = false;
                    await StepClicked.InvokeAsync(new StepperClickedEventArgs()
                    {
                        PreviousStep = prevStep,
                        ActiveStep = currStep
                    }).ConfigureAwait(true);
                }
                else
                {
                    isUpdateProp = true;
                }
                stepStatus = false;
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"Invalid operation error occurred: {e.Message}");
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine($"TimeoutException occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Update the value of the stepper on step change.
        /// </summary>
        /// <param name="isInteracted">value indicating whether the step change was initiated by user interaction.</param>
        /// <param name="prevStep">previuos value of the stepper.</param>
        /// <param name="currentStep">current value of the stepper.</param>
        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async void StepChangedHandler(bool isInteracted, int prevStep, int currentStep)
        {
            try
            {
                prevStep = activeStep;
                ActiveStep = activeStep = currentStep;
                if (prevStep != currentStep)
                {
                    await UpdateStepperValue(dataId, currentStep, false).ConfigureAwait(true);
                }
                if (StepChanged.HasDelegate)
                {
                    isUpdateProp = false;
                    await StepChanged.InvokeAsync(new StepperChangedEventArgs()
                    {
                        IsInteracted = isInteracted,
                        PreviousStep = prevStep,
                        ActiveStep = currentStep
                    }).ConfigureAwait(true);
                }
                else
                {
                    isUpdateProp = true;
                }
                if (currentStep != ActiveStep)
                {
                    ActiveStep = activeStep = currentStep;
                }
                foreach (StepperStep step in Steps)
                {
                    UpdateStepperStatus(Steps.IndexOf(step), step, true, currentStep);
                }
                stepStatus = false;
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"Invalid operation error occurred: {e.Message}");
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine($"TimeoutException occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Update the value of the stepper on step changing.
        /// </summary>
        /// <param name="args">Specifies the step changing event args.</param>
        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task<StepperChangeEventArgs> StepChangingHandler(StepperChangeEventArgs args)
        {
            var eventArgs = new StepperChangeEventArgs()
            {
                Cancel = args != null ? args.Cancel : false,
                IsInteracted = args != null ? args.IsInteracted : false,
                PreviousStep = args != null ? args.PreviousStep : 0,
                ActiveStep = args != null ? args.ActiveStep : 0
            };
            if (StepChanging.HasDelegate)
            {
                isUpdateProp = false;
                try
                {
                    await StepChanging.InvokeAsync(eventArgs).ConfigureAwait(true);
                }
                catch (TimeoutException ex)
                {
                    Console.WriteLine($"TimeoutException occurred: {ex.Message}");
                }
            }
            else
            {
                isUpdateProp = true;
            }
            isCancel = eventArgs.Cancel;
            stepStatus = false;
            return eventArgs;
        }

        #endregion

        #region Internal Methods

        internal async Task UpdateStepperValue(string dataId, int currentStep, bool isInteraction = true)
        {
            try
            {
                await InvokeMethod("sfBlazor.Stepper.updateStepperValue", dataId, currentStep, isInteraction).ConfigureAwait(true);
                isUpdateProp = false;
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine($"TimeoutException occurred: {ex.Message}");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"Invalid operation error occurred: {e.Message}");
            }
        }

        internal void UpdateProperties()
        {
            initialVal = activeStep = ActiveStep;
            readOnly = ReadOnly;
            linear = Linear;
            showTooltip = ShowTooltip;
            tooltipTemplate = TooltipTemplate != null;
            labelPosition = LabelPosition;
            orientation = Orientation;
            stepType = StepType;
        }

        private void UpdateStepperStatus(int index, StepperStep step, bool isClicked, int val = 0)
        {
            int comparisonValue = isClicked ? val : activeStep;
            #pragma warning disable BL0005 // Component parameter should not be set outside of its component.
            if ((!isClicked && step.Status == StepperStatus.NotStarted) || isClicked)
            {
                if (index < comparisonValue)
                {
                    step.Status = StepperStatus.Completed;
                }
                else if (index == comparisonValue)
                {
                    step.Status = StepperStatus.InProgress;
                }
                else
                {
                    step.Status = StepperStatus.NotStarted;
                }
            }

            #pragma warning restore BL0005
        }

        internal bool RenderDefault(int index)
        {
            return String.IsNullOrEmpty(this.Steps[index].IconCss) && String.IsNullOrEmpty(this.Steps[index].Text) && String.IsNullOrEmpty(this.Steps[index].Label);
        }

        private static string GetLabelPositionString(StepperLabelPosition position)
        {
            return position switch
            {
                StepperLabelPosition.Top => "top",
                StepperLabelPosition.Bottom => "bottom",
                StepperLabelPosition.Start => "start",
                StepperLabelPosition.End => "end",
                _ => throw new ArgumentOutOfRangeException(nameof(position))
            };
        }

        private static string GetOrientationClassName(StepperOrientation orientation)
        {
            return orientation switch
            {
                StepperOrientation.Horizontal => "horizontal",
                StepperOrientation.Vertical => "vertical",
                _ => throw new ArgumentOutOfRangeException(nameof(orientation))
            };
        }

        private static string GetStepTypeClassName(StepperType stepType)
        {
            return stepType switch
            {
                StepperType.Default => "default",
                StepperType.Indicator => "indicator",
                StepperType.Label => "label",
                _ => throw new ArgumentOutOfRangeException(nameof(stepType))
            };
        }

        private static string GetStepperStatusString(StepperStatus status) => status switch
        {
            StepperStatus.NotStarted => "notstarted",
            StepperStatus.InProgress => "inprogress",
            StepperStatus.Completed => "completed",
            _ => throw new ArgumentOutOfRangeException(nameof(status))
        };

        internal string UpdateLabelClass()
        {
            string position = GetLabelPositionString(LabelPosition);
            if (Orientation == StepperOrientation.Horizontal)
            {
                return position == "top" ? "before" : position == "bottom" ? "after" : position;
            }
            else
            {
                return position == "start" ? "before" : position == "end" ? "after" : position;
            }
        }

        internal string UpdateLiClass(StepperStep step, int index, bool isDefault)
        {
            string stepClassLists = "";
            bool isPreviousStep = (index == activeStep - 1);
            bool isNextStep = (index == activeStep + 1);
            if (index == activeStep || activeStep == Steps.Count)
            {
                stepClassLists += SPACE + "e-step-selected";
            }
            if (index == activeStep) {
                stepClassLists += SPACE + "e-step-inprogress";
            } else if (activeStep > 0 && index < activeStep) {
                stepClassLists += SPACE + "e-step-completed";
            } else {
                stepClassLists += SPACE + "e-step-notstarted";
            }
            stepClassLists = ToggleClass(stepClassLists, PREVSTEP, isPreviousStep);
            stepClassLists = ToggleClass(stepClassLists, NEXTSTEP, isNextStep);
            if (!isDefault)
            {
                if (Template != null)
                {
                    stepClassLists += SPACE + "e-step-template";
                }
                if (!(String.IsNullOrEmpty(step.IconCss)) || !(String.IsNullOrEmpty(step.Text)))
                {
                    stepClassLists += (((String.IsNullOrEmpty(step.Label) && StepType == StepperType.Label) || StepType != StepperType.Label)) ? SPACE + STEPICON : "";
                }
                if (!(String.IsNullOrEmpty(step.Text)))
                {
                    if (String.IsNullOrEmpty(step.Label) && StepType != StepperType.Indicator && !String.IsNullOrEmpty(step.IconCss))
                    {
                        stepClassLists += SPACE + STEPTEXT;
                    }
                }
                if (((String.IsNullOrEmpty(step.IconCss) && String.IsNullOrEmpty(step.Label) && StepType != StepperType.Indicator) || String.IsNullOrEmpty(step.Label) && StepType == StepperType.Label))
                {
                    stepClassLists += !(stepClassLists.Contains(STEPICON, StringComparison.Ordinal)) ? SPACE + "e-step-text-only" : "";
                }
                if (!(String.IsNullOrEmpty(step.Label)))
                {
                    if ((!String.IsNullOrEmpty(step.IconCss) || !String.IsNullOrEmpty(step.Text)) && StepType != StepperType.Label && ((GetOrientationClassName(Orientation) == "horizontal" && (GetLabelPositionString(LabelPosition) == "start" || GetLabelPositionString(LabelPosition) == "end")) ||
                        (GetOrientationClassName(Orientation) == "vertical" && (GetLabelPositionString(LabelPosition) == "top" || GetLabelPositionString(LabelPosition) == "bottom"))))
                    {
                        stepClassLists += StepType != StepperType.Indicator ? SPACE + STEPTEXT : "";
                    } else {
                        stepClassLists += StepType != StepperType.Indicator ? SPACE + STEPLABEL : "";
                    }
                    showLabelClass = StepType != StepperType.Indicator ? "e-label-" + @UpdateLabelClass() : "";
                    if ((String.IsNullOrEmpty(step.IconCss) && String.IsNullOrEmpty(step.Text) && StepType != StepperType.Indicator) || StepType == StepperType.Label)
                    {
                        stepClassLists += SPACE + "e-step-label-only";
                    }
                }
            }
            if (!(String.IsNullOrEmpty(step.CssClass)))
            {
                stepClassLists += SPACE + step.CssClass;
            }
            if (step.IsValid != null)
            {
                stepClassLists += SPACE + (step.IsValid == true ? "e-step-valid" : "e-step-error");
            }
            if (step.Disabled == true)
            {
                stepClassLists += SPACE + "e-step-disabled";
            }
            return stepClassLists;
        }
        private static string ToggleClass(string existingClasses, string className, bool condition)
        {
            if (condition)
            {
                if (!existingClasses.Contains(SPACE + className, StringComparison.Ordinal))
                {
                    existingClasses += SPACE + className;
                }
            }
            else
            {
                if (existingClasses.Contains(SPACE + className, StringComparison.Ordinal))
                {
                    existingClasses = existingClasses.Replace(SPACE + className, "", StringComparison.Ordinal);
                }
            }
            return existingClasses;
        }
        internal async Task OnPropertyChangeHandler()
        {
            try
            {
                var stepperObj = GetInstance();
                if (PropertyChanges.ContainsKey(nameof(LabelPosition)) || PropertyChanges.ContainsKey(nameof(Orientation)) || PropertyChanges.ContainsKey(nameof(StepType)))
                {
                    await InvokeMethod("sfBlazor.Stepper.updateDynamicStepperProps", stepperObj).ConfigureAwait(true);
                }
                else if (PropertyChanges.ContainsKey(nameof(Linear)))
                {
                    stepperObj.Remove("activeStep");
                    await InvokeMethod("sfBlazor.Stepper.updateLinear", stepperObj).ConfigureAwait(true);
                }
                else
                {
                    await InvokeMethod("sfBlazor.Stepper.updateStepperProps", stepperObj).ConfigureAwait(true);
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

        internal Dictionary<string, object> GetInstance()
        {
            bool isStepNavigated = !(PropertyChanges.ContainsKey(nameof(Linear)) || PropertyChanges.ContainsKey(nameof(ReadOnly)) || PropertyChanges.ContainsKey(nameof(ShowTooltip)));
            var stepperObj = new Dictionary<string, object>
            {
                { "dataId", dataId },
                { "element", stepperElement },
                { "dotNetRef", DotnetObjectReference },
                { "activeStep", activeStep },
                { "readOnly", readOnly },
                { "showLabelClass", showLabelClass },
                { "linear", linear },
                { "enableRtl", SyncfusionService.options.EnableRtl },
                { "showTooltip", showTooltip },
                { "tooltipContent", tooltipContent },
                { "tooltipTemplate", tooltipTemplate },
                { "isDefaultStep", isDefaultStep },
                { "stepperStatus", status },
                { "statusIndex", statusIndex },
                { "stepNavigation", isStepNavigated },
                { "duration", Animation.Duration }
            };
            return stepperObj;
        }

        /// <summary>
        /// Updates the Steps list with the list provided from the StepperSteps tag directive.
        /// </summary>
        internal void UpdateSteps(List<StepperStep> steps)
        {
            Steps = steps;
        }

        /// <summary>
        /// Updates the Animation with the value provided from the Animation tag directive.
        /// </summary>
        internal void UpdateAnimationSettings(StepperAnimationSettings animation)
        {
            Animation = animation;
        }

        internal async Task RefreshComponent()
        {
            await InvokeAsync(StateHasChanged).ConfigureAwait(true);
        }

        internal override async void ComponentDispose()
        {
            try
            {
                base.ComponentDispose();
                if (IsRendered) await InvokeMethod("sfBlazor.Stepper.destroy", dataId).ConfigureAwait(true);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"Null reference error occurred: {ex.Message}");
            }
        }

        #endregion
    }
}
