using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// A class that represents steps of <see cref="SfStepper"/> component.
    /// </summary>
    public partial class StepperStep : SfOwningComponentBase
    {
        #region Private Variables

        private string? iconCss;
        private string? cssClass;
        private bool disabled;
        private bool optional;
        private bool? isValid;
        private string? text;
        private string? label;
        private StepperStatus status;
        private bool isInitialRender = true;

        #endregion

        #region Properties
        /// <summary>
        /// Indicates the StepperSteps component.
        /// </summary>
        [CascadingParameter]
        internal StepperSteps Parent { get; set; }

        /// <summary>
        /// Indicates the SfStepper component.
        /// </summary>
        [CascadingParameter]
        internal SfStepper BaseParent { get; set; }

        /// <summary>
        /// Gets or sets the CSS class for the icon associated with the <see cref="StepperStep" />.
        /// </summary>
        /// <value>
        /// A string representing the CSS class for the icon. The default value is an empty string.
        /// </value>
        /// <remarks>
        /// The icon CSS class is used to style the icon displayed for the step. If specified, it determines the visual representation of the step.
        /// </remarks>
        [Parameter]
        public string IconCss { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the custom CSS class for customize the appearance of step.
        /// </summary>
        /// <value>
        /// A string representing the CSS class to be applied to the step. The default value is an empty string.
        /// </value>
        /// <remarks>
        /// You can use this property to apply custom styles to individual steps within the Stepper component by specifying a CSS class.
        /// </remarks>
        [Parameter]
        public string CssClass { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the step is disabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if the step is disabled; otherwise, <c>false</c>. The default value is <c>false</c>.
        /// </value>
        /// <remarks>
        /// Disabled steps are not interactive and cannot be progressed to.
        /// </remarks>
        [Parameter]
        public bool Disabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the step is optional.
        /// </summary>
        /// <value>
        /// <c>true</c> if the step is optional; otherwise, <c>false</c>. The default value is <c>false</c>.
        /// </value>
        /// <remarks>
        /// If a step is optional, users may choose to skip it without affecting the overall process.
        /// </remarks>
        [Parameter]
        public bool Optional { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the step is valid.
        /// </summary>
        /// <value>
        /// <c>true</c> if the step is valid completion; otherwise, <c>false</c>. The default value is <c>false</c>.
        /// </value>
        /// <remarks>
        /// Indicate whether a step's required criteria have been met.
        /// </remarks>
        [Parameter]
        public bool? IsValid { get; set; } = null;

        /// <summary>
        /// Gets or sets the text content for the <see cref="StepperStep" />.
        /// </summary>
        /// <value>
        /// A string representing the text content of the step. The default value is an empty string.
        /// </value>
        /// <remarks>
        /// The text content provides descriptive information for the step.
        /// </remarks>
        [Parameter]
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the label for the <see cref="StepperStep" />.
        /// </summary>
        /// <value>
        /// A string representing the label of the step. The default value is an empty string.
        /// </value>
        /// <remarks>
        /// The label can provide additional information or context for the step. If both the <see cref="Label" /> and <see cref="Text" /> properties are defined,
        /// this property will be prioritized for display.
        /// </remarks>
        [Parameter]
        public string Label { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the status of the step.
        /// </summary>
        /// <value>
        /// A <see cref="StepperStatus"/> value representing the status of the step. The default value is <see cref="StepperStatus.NotStarted"/>.
        /// </value>
        /// <remarks>
        /// The status indicates the progress or state of the step, which can be one of the following:
        /// - <see cref="StepperStatus.NotStarted"/>
        /// - <see cref="StepperStatus.InProgress"/>
        /// - <see cref="StepperStatus.Completed"/>
        /// </remarks>
        [Parameter]
        public StepperStatus Status { get; set; }

        #endregion

        #region LifeCycle Methods
        protected override async Task OnInitializedAsync()
        {
            try
            {
                await base.OnInitializedAsync().ConfigureAwait(true);
                UpdateStepProperties();
                Parent.UpdateChildProperty(this);
                await BaseParent.RefreshComponent().ConfigureAwait(true);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"Invalid operation error occurred: {e.Message}");
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                await base.OnParametersSetAsync().ConfigureAwait(true);
                if (!isInitialRender && Status != status)
                {
                    BaseParent.stepStatus = true;
                }
                if (IconCss != iconCss || CssClass != cssClass || Disabled != disabled || Optional != optional || IsValid != isValid ||
                    Text != text || Label != label || Status != status)
                {
                    UpdateStepProperties();
                    await BaseParent.RefreshComponent().ConfigureAwait(true);
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"Invalid operation error occurred: {e.Message}");
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"Null reference error occurred: {ex.Message}");
            }
            catch (TimeoutException te)
            {
                Console.WriteLine($"TimeoutException occurred: {te.Message}");
            }
        }

        /// <inheritdoc/>
        protected override async void OnAfterRender(bool firstRender)
        {
            try
            {
                base.OnAfterRender(firstRender);
                if (firstRender)
                {
                    if (BaseParent.StepRendered.HasDelegate && BaseParent.Steps != null)
                    {
                        await BaseParent.StepRendered.InvokeAsync(new StepperRenderedEventArgs()
                        {
                            Step = this,
                            Index = BaseParent.Steps.IndexOf(this)
                        }).ConfigureAwait(true);
                    }
                }
                else
                {
                    isInitialRender = false;
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"Invalid operation error occurred: {e.Message}");
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"Null reference error occurred: {ex.Message}");
            }
            catch (TimeoutException te)
            {
                Console.WriteLine($"TimeoutException occurred: {te.Message}");
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                Parent?.RemoveChildProperty(this);
                Parent = null;
            }
        }
        #endregion

        #region Internal Methods
        internal void UpdateStepProperties()
        {
            iconCss = IconCss;
            cssClass = CssClass;
            disabled = Disabled;
            optional = Optional;
            isValid = IsValid;
            text = Text;
            label = Label;
            status = Status;
        }

        #endregion
    }

    #region Enums

    /// <summary>
    /// Specifies the status of a step within the <see cref="SfStepper"/> component.
    /// </summary>
    /// <list type="bullet">
    ///     <item>
    ///         <term>NotStarted</term>
    ///         <description>The step has not been started or initiated.</description>
    ///     </item>
    ///     <item>
    ///         <term>InProgress</term>
    ///         <description>The step is currently in progress.</description>
    ///     </item>
    ///     <item>
    ///         <term>Completed</term>
    ///         <description>The step has been successfully completed.</description>
    ///     </item>
    /// </list>
    public enum StepperStatus
    {
        /// <summary>
        /// Represents a step that has not yet been started.
        /// </summary>
        NotStarted,

        /// <summary>
        /// Represents a step that is currently in progress.
        /// </summary>
        InProgress,

        /// <summary>
        /// Represents a step that has been completed.
        /// </summary>
        Completed
    }

    #endregion
}
