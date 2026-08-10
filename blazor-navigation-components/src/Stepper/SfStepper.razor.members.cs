using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Internal;
using System.Collections.Generic;
using System.ComponentModel;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary> 
    /// The Blazor Stepper component visualizes several steps and indicates the current progress by highlighting already completed steps.
    /// </summary>
    /// <remarks>
    /// Stepper items can be populated by specifying <see cref="StepperStep"/> within <see cref="SfStepper"/> tag directive.
    /// </remarks>
    /// <example>
    /// In the below code example, a basic Stepper component is initialized with <see cref="StepperStep"/> tag directive.
    /// <code><![CDATA[ 
    /// <SfStepper>
    ///     <StepperSteps>
    ///         <StepperStep></StepperStep>
    ///         <StepperStep></StepperStep>
    ///         <StepperStep></StepperStep>
    ///         <StepperStep></StepperStep>
    ///         <StepperStep></StepperStep>
    ///     </StepperSteps>
    /// </SfStepper>
    /// ]]></code>
    /// </example>
    public partial class SfStepper
    {
        #region Constants
        private const string ROOT = "e-control e-stepper e-lib";
        private const string SPACE = " ";
        private const string ITEMCONTAINER = "e-step-container";
        private const string ICONCSS = "e-indicator";
        private const string STEPICON = "e-step-item";
        private const string STEPTEXT = "e-step-text";
        private const string STEPLABEL = "e-step-label";
        private const string STEPPERSTEP = "e-step";
        private const string TEXTCSS = "e-step-text-container";
        private const string LABELCSS = "e-step-label-container";
        private const string OPTIONAL = "e-step-label-optional";
        private const string PREVSTEP = "e-previous";
        private const string NEXTSTEP = "e-next";

        #endregion

        #region Private Variables

        private string? stepperClass;
        private string showLabelClass = "";
        private bool isDefaultStep;
        internal Dictionary<string, object>? htmlAttr;
        private int statusIndex;
        private List<string> tooltipContent = new List<string>();
        private bool isUpdateProp = true;
        private bool isCancel;
        private bool IsInitialRender = true;
        private int stepLength;
        private int initialVal;

        //Unique ID of the stepper element.
        private string? dataId;

        // To store the current focused item details.
        private ElementReference stepperElement;

        //Private variables for storing the values of public property to check whether the public property is changed.
        private int activeStep;
        private bool readOnly;
        private bool linear;
        private bool showTooltip;
        private bool tooltipTemplate;
        private StepperLabelPosition labelPosition;
        private StepperOrientation orientation;
        private StepperType stepType;
        private StepperAnimationSettings? animation;
        private string? status;

        #endregion

        internal bool stepStatus;

        #region Members
        /// <summary>
        /// Gets or sets the current step index of the <see cref="SfStepper"/> component.
        /// </summary>
        /// <remarks>
        /// Changing this property on demand will update the active step, but it will respect the <see cref="Linear" /> flow if enabled.
        /// </remarks>
        /// <example>
        /// <para>Examples of how it works:</para>
        /// <code>
        /// ActiveStep = -1
        /// 1 - Not Started
        /// 2 - Not Started
        /// 3 - Not Started
        /// 4 - Not Started
        /// </code>
        /// <code>
        /// ActiveStep = 0
        /// 1 - In Progress
        /// 2 - Not Started
        /// 3 - Not Started
        /// 4 - Not Started
        /// </code>
        /// <code>
        /// ActiveStep = 1
        /// 1 - Completed
        /// 2 - In Progress
        /// 3 - Not Started
        /// 4 - Not Started
        /// </code>
        /// <code>
        /// ActiveStep = 3
        /// 1 - Completed
        /// 2 - Completed
        /// 3 - Completed
        /// 4 - In Progress
        /// </code>
        /// <code>
        /// ActiveStep = 4
        /// 1 - Completed
        /// 2 - Completed
        /// 3 - Completed
        /// 4 - Completed
        /// </code>
        /// </example> 
        [Parameter]
        public int ActiveStep { get; set; } = 0;

        /// <summary>
        /// Gets or sets the position of step labels in relation to the <see cref="SfStepper"/> component.
        /// </summary>
        /// <value>
        /// A value indicating the position of step labels. The default value is <see cref="StepperLabelPosition.Bottom"/>.
        /// </value>
        /// <remarks>
        /// Use this property to control whether step labels appear before or after each step in the <see cref="SfStepper"/> component.
        /// </remarks>
        [Parameter]
        public StepperLabelPosition LabelPosition { get; set; } = StepperLabelPosition.Bottom;

        /// <summary>
        /// Gets or sets the custom CSS class to customize the <see cref="SfStepper"/> component.
        /// </summary>
        /// <value>
        /// A string representing the CSS class to be applied. The default value is an empty string.
        /// </value>
        /// <remarks>
        /// You can use this property to apply custom styles to the <see cref="SfStepper"/> component by specifying a CSS class.
        /// </remarks>
        [Parameter]
        public string CssClass { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="SfStepper"/> component is in read-only mode.
        /// </summary>
        /// <value>
        /// <c>true</c> if the <see cref="SfStepper"/> is in read-only mode; otherwise, <c>false</c>. The default value is <c>false</c>.
        /// </value>
        /// <remarks>
        /// When set to <c>true</c>, the <see cref="SfStepper"/> component becomes read-only, preventing user interaction.
        /// </remarks>
        [Parameter]
        public bool ReadOnly { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether tooltips should be displayed in the <see cref="SfStepper"/> component.
        /// </summary>
        /// <value>
        /// <c>true</c> if tooltips should be displayed; otherwise, <c>false</c>. The default value is <c>false</c>.
        /// </value>
        /// <remarks>
        /// When set to <c>true</c>, tooltips will appear to provide additional information for steps in the <see cref="SfStepper"/>.
        /// </remarks>
        [Parameter]
        public bool ShowTooltip { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="SfStepper"/> component should behave in linear mode.
        /// </summary>
        /// <remarks>
        /// When set to <c>true</c>, the Stepper will restrict navigation to a linear path, allowing users to proceed to the next step only after completing the current one.
        /// In non-linear mode (default), users can navigate freely between steps.
        /// </remarks>
        /// <value> 
        /// <c>true</c> if the <see cref="SfStepper"/> follows a linear flow; otherwise, <c>false</c>. The default value is <c>false</c>.
        /// </value>
        [Parameter]
        public bool Linear { get; set; }

        /// <summary>
        /// Sets id attribute for the stepper element.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        [Parameter]
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the custom template for rendering individual steps in the <see cref="SfStepper"/> component.
        /// </summary>
        /// <value>
        /// The template content. The default value is <c>null</c>.
        /// </value>
        /// <remarks>
        /// The <see cref="StepperStep"/> allows you to define a custom template for rendering each step.
        /// You can use this template to fully customize the appearance and content of individual steps within the <see cref="SfStepper"/>.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// <SfStepper>
        /// </SfStepper>
        /// ]]></code>
        /// </example>
        [Parameter]
        public RenderFragment<StepperStep> Template { get; set; }

        /// <summary>
        /// Gets or sets a custom template for rendering tooltips in the <see cref="SfStepper"/> component.
        /// </summary>
        /// <value>
        /// A <see cref="StepperStep"/> representing the custom tooltip template.
        /// The default value is <c>null</c>.
        /// </value>
        /// <remarks>
        /// You can use this property to define a custom template for rendering tooltips associated with individual steps in the <see cref="SfStepper"/> component.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// <SfStepper>
        /// </SfStepper>
        /// ]]></code>
        /// </example>
        [Parameter]
        public RenderFragment<StepperStep> TooltipTemplate { get; set; }

        /// <summary>
        /// Gets or sets the orientation of the <see cref="SfStepper"/>  component.
        /// </summary>
        /// <remarks>
        /// The <see cref="StepperOrientation"/> enumeration defines the possible orientations for the <see cref="SfStepper"/> component.
        /// You can set this property to control whether the stepper is displayed horizontally or vertically.
        /// </remarks>
        /// <value>
        /// A <see cref="StepperOrientation"/> value representing the orientation of the <see cref="SfStepper"/>.
        /// The default value is <see cref="StepperOrientation.Horizontal"/>.
        /// </value>
        [Parameter]
        public StepperOrientation Orientation { get; set; }

        /// <summary>
        /// Gets or sets the display style of steps in the <see cref="SfStepper"/> component.
        /// </summary>
        /// <remarks>
        /// The <see cref="StepperType"/> enumeration defines the available styles for displaying steps:
        /// - <see cref="StepperType.Indicator"/>: Display only step indicators.
        /// - <see cref="StepperType.Label"/>: Display only step labels.
        /// - <see cref="StepperType.Default"/>: Display a combination of both step indicators and labels.
        /// </remarks>
        /// <value>
        /// A value from the <see cref="StepperType"/> enumeration representing the display style of steps.
        /// The default value is <see cref="StepperType.Default"/>.
        /// </value>
        [Parameter]
        public StepperType StepType { get; set; } = StepperType.Default;

        /// <exclude/>
        /// <summary> 
        /// Gets or sets a a value that indicates the collection of additional attributes that will applied to the stepper container element. 
        /// </summary> 
        /// <remarks> 
        /// Additional attributes can be added by specifying as inline attributes or by specifying <c>@attributes</c> directive. 
        /// </remarks>
        [Parameter(CaptureUnmatchedValues = true)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Dictionary<string, object> HtmlAttributes
        {
            get { return htmlAttr; }
            set { htmlAttr = SfBaseUtils.SanitizeHtmlAttributes(value); }
        }

        #endregion

        #region Tag directive members

        /// <summary>
        /// Gets or sets the list of steps in stepper.
        /// </summary>
        internal List<StepperStep>? Steps { get; set; }

        /// <summary>
        /// Gets or sets the animation settings for the Stepper component.
        /// </summary>
        /// <value>
        /// An <see cref="StepperAnimationSettings"/> object that defines the animation behavior.
        /// The default value is an animation with <see cref="StepperAnimationSettings.Enable"/> set to <c>true</c>,
        /// <see cref="StepperAnimationSettings.Duration"/> set to 1000 milliseconds, and <see cref="StepperAnimationSettings.Delay"/> set to 0 milliseconds.
        /// </value>
        internal StepperAnimationSettings Animation
        {
            get { return animation ?? new StepperAnimationSettings(); }
            set { animation = value; }
        }

        #endregion

        #region Non Browsable Members

        /// <summary>
        /// Gets or sets a value that indicates the child content for the Stepper including HTML element.
        /// </summary>
        [Parameter]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public RenderFragment ChildContent { get; set; }

        #endregion

        #region Events

        /// <summary>
        /// Gets or sets an event callback that is raised when the <see cref="SfStepper"/> rendering is completed.
        /// </summary>
        /// <value> 
        /// An event call back function. 
        /// </value>
        [Parameter]
        public EventCallback<object> Created { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised when the current step is changed.
        /// </summary>
        /// <remarks>
        /// You can subscribe to this event to be notified when the Stepper's current step changes.
        /// </remarks>
        /// <value>
        /// An event callback function.
        /// </value>
        [Parameter]
        public EventCallback<StepperChangedEventArgs> StepChanged { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised before a step change occurs in the <see cref="SfStepper" /> component.
        /// </summary>
        /// <remarks>
        /// Subscribe to this event to perform custom actions or validations before a step change is finalized.
        /// The event provides information about the step change being initiated, allowing you to intervene and control the process.
        /// </remarks>
        /// <value>
        /// An event callback function that is triggered before transitioning between steps in the Stepper.
        /// </value>
        [Parameter]
        public EventCallback<StepperChangeEventArgs> StepChanging { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised when a step in the <see cref="SfStepper"/> component is clicked.
        /// </summary>
        /// <remarks>
        /// Subscribe to this event to respond to user interactions with individual steps. It provides an opportunity to handle
        /// custom actions or navigation logic based on the step that was clicked.
        /// </remarks>
        /// <value>
        /// An event callback function that is triggered when a user clicks on a step within the Stepper.
        /// </value>
        [Parameter]
        public EventCallback<StepperClickedEventArgs> StepClicked { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised when a step in the <see cref="SfStepper"/> component is rendered.
        /// </summary>
        /// <value>
        /// An event callback function that is triggered when a step is rendered within the Stepper.
        /// </value>
        /// <remarks>
        /// Subscribe to this event to perform additional actions or apply custom styling after a step has been rendered.
        /// It provides an opportunity to dynamically modify the appearance or behavior of individual steps during the rendering process.
        /// </remarks>
        [Parameter]
        public EventCallback<StepperRenderedEventArgs> StepRendered { get; set; }

        #endregion
    }

    #region Enums

    /// <summary>
    /// Specifies the position of step labels in relation to the Stepper component.
    /// </summary>
    /// <list type="bullet">
    ///     <item>
    ///         <term>Top</term>
    ///         <description>Displays step labels on top when the Stepper is in a horizontal orientation, or on the left when the Stepper is in a vertical orientation.</description>
    ///     </item>
    ///     <item>
    ///         <term>Bottom</term>
    ///         <description>Displays step labels on the bottom when the Stepper is in a horizontal orientation, or on the right when the Stepper is in a vertical orientation.</description>
    ///     </item>
    ///     <item>
    ///         <term>Start</term>
    ///         <description>Displays step labels on the left side, regardless of the Stepper's orientation.</description>
    ///     </item>
    ///     <item>
    ///         <term>End</term>
    ///         <description>Displays step labels on the right side, regardless of the Stepper's orientation.</description>
    ///     </item>
    /// </list>
    public enum StepperLabelPosition
    {
        /// <summary>
        /// Displays step labels on top position regardless of the Stepper's orientation.
        /// </summary>
        Top,

        /// <summary>
        /// Displays step labels on the bottom position regardless of the Stepper's orientation.
        /// </summary>
        Bottom,

        /// <summary>
        /// Displays step labels on the left side regardless of the Stepper's orientation.
        /// </summary>
        Start,

        /// <summary>
        /// Displays step labels on the right side regardless of the Stepper's orientation.
        /// </summary>
        End
    }

    /// <summary>
    /// Represents the orientation options for the <see cref="SfStepper"/> component.
    /// </summary>
    /// <list type="bullet">
    ///     <item>
    ///         <term>Horizontal</term>
    ///         <description>The steps are arranged horizontally.</description>
    ///     </item>
    ///     <item>
    ///         <term>Vertical</term>
    ///         <description>The steps are arranged vertically.</description>
    ///     </item>
    /// </list>
    public enum StepperOrientation
    {
        /// <summary>
        /// Represents a horizontal orientation for the <see cref="SfStepper"/> component.
        /// </summary>
        Horizontal,

        /// <summary>
        /// Represents a vertical orientation for the <see cref="SfStepper"/> component.
        /// </summary>
        Vertical
    }

    /// <summary>
    /// Specifies the type of display for steps in the <see cref="SfStepper"/> component.
    /// </summary>
    /// <list type="bullet">
    ///     <item>
    ///         <term>Default</term>
    ///         <description>Display a combination of both step indicators and labels.</description>
    ///     </item>
    ///     <item>
    ///         <term>Label</term>
    ///         <description>Display only step labels.</description>
    ///     </item>
    ///     <item>
    ///         <term>Indicator</term>
    ///         <description>Display only step indicators.</description>
    ///     </item>
    /// </list>
    public enum StepperType
    {
        /// <summary>
        /// Displays both step indicators and labels.
        /// </summary>
        Default,

        /// <summary>
        /// Displays only step labels.
        /// </summary>
        Label,

        /// <summary>
        /// Displays only step indicators.
        /// </summary>
        Indicator
    }

    #endregion

    #region Event Args

    /// <summary>
    /// Represents the event arguments for the Stepper's step change event.
    /// </summary>
    public class StepperChangedEventArgs
    {
        /// <summary>
        /// Gets or sets a value indicating whether the step change was initiated by user interaction.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the event was triggered by user interaction. Otherwise, <c>false</c>.
        /// </value>
        public bool IsInteracted { get; set; }

        /// <summary>
        /// Gets the index of the previous step before the change.
        /// </summary>
        /// <value>
        /// It represents the previous step index, otherwise the default index value 0.
        /// </value>
        public int PreviousStep { get; set; }

        /// <summary>
        /// Gets the index of the active step after the change.
        /// </summary>
        /// <value>
        /// It represents the active step index, otherwise the default index value 0.
        /// </value>
        public int ActiveStep { get; set; }
    }

    /// <summary>
    /// Represents the event arguments for the Stepper's step changing event.
    /// </summary>
    public class StepperChangeEventArgs : StepperChangedEventArgs
    {
        /// <summary>
        /// Gets or sets a value indicating whether to cancel the action of step changing.
        /// </summary>
        /// <value>
        /// <c>true</c> if the action of step changing should be canceled; otherwise, <c>false</c>.
        /// </value>
        public bool Cancel { get; set; }
    }

    /// <summary>
    /// Represents the event arguments for the Stepper's step click event.
    /// </summary>
    public class StepperClickedEventArgs
    {
        /// <summary>
        /// Gets the index of the previous step.
        /// </summary>
        /// <value>
        /// It represents the previous step index, otherwise the default index value 0.
        /// </value>
        public int PreviousStep { get; set; }

        /// <summary>
        /// Gets the index of the active step.
        /// </summary>
        /// <value>
        /// It represents the active step index, otherwise the default index value 0.
        /// </value>
        public int ActiveStep { get; set; }
    }

    /// <summary>
    /// Represents event arguments for Stepper rendered event.
    /// </summary>

    public class StepperRenderedEventArgs
    {
        /// <summary>
        /// Gets the index of the rendered current step.
        /// </summary>
        /// <value>
        /// It represents the index of the rendered step.
        /// </value>
        public int Index { get; set; }

        /// <summary>
        /// Gets the step data associated with the rendered step.
        /// </summary>
        /// <value>
        /// An <see cref="SfStepper"/> to the current step element that is being rendered.
        /// </value>

        public StepperStep Step { get; set; }

    }

    #endregion
}
