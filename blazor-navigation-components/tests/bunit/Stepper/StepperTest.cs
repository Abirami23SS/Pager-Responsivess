using Xunit;
using Bunit;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Tests.Navigations;
using AngleSharp.Dom;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Syncfusion.Blazor.Tests.Stepper
{
    public class Stepper : BunitTestContext
    {
        [Fact(DisplayName = "Initial DOM Rendering")]
        public void ComponentRendering()
        {
            var stepper = RenderComponent<SfStepper>();
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>().AddChildContent<StepperStep>().AddChildContent<StepperStep>()));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-step-content\">1</span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-step-content\">2</span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-step-content\">3</span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Generic ID generation")]
        public void StepperWithGenericID()
        {
            var stepper = RenderComponent<SfStepper>();
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>().AddChildContent<StepperStep>().AddChildContent<StepperStep>()));
            stepper.Render();
            var stepperElement = stepper.Find(".e-stepper");
            Assert.True(stepperElement.GetAttribute("id") != null);
        }
        [Fact(DisplayName = "CssClass property")]
        public void CssClassProperty()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.CssClass), "testClass"));
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>().AddChildContent<StepperStep>().AddChildContent<StepperStep>()));
            stepper.Render();
            var stepperElement = stepper.Find(".e-stepper");
            Assert.Contains("testClass", stepperElement.ClassName);
        }        
        [Fact(DisplayName = "ReadOnly property")]
        public void ReadOnlyProperty()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.ReadOnly), true));
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>().AddChildContent<StepperStep>().AddChildContent<StepperStep>()));
            stepper.Render();
            var stepperElement = stepper.Find(".e-stepper");
            Assert.Contains("e-stepper-readonly", stepperElement.ClassName);
        }
        [Fact(DisplayName = "Step with CssClass property")]
        public void StepWithCssClassProperty()
        {
            var stepper = RenderComponent<SfStepper>();
            var stepperElement = stepper.Find(".e-stepper");            
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder").Add(p => p.CssClass, "testClass")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder"))));
            stepper.Render();
            Assert.Contains("testClass", stepperElement.Children[1].Children[0].ClassName);
        }
        [Fact(DisplayName = "Template property")]
        public void Template()
        {
            var stepper = RenderComponent<StepperTemplate>();
            var stepperElement = stepper.Find(".e-stepper");
            Assert.Contains("e-step-template", stepperElement.Children[1].Children[0].ClassName);
        }
        [Fact(DisplayName = "TooltipTemplate property")]
        public void TooltipTemplate()
        {
            var stepper = RenderComponent<TooltipTemplate>();
            var stepperListElement = stepper.Find(".e-stepper-steps");
            var expectedOutput = "Step2";
            stepperListElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Created Event Testing")]
        public void StepperCreated()
        {
            var isCreated = false;
            var stepper = RenderComponent<SfStepper>(parameters => parameters.Add(s => s.Created, () =>
            {
                isCreated = true;
            }));
            Assert.True(isCreated);
        }
        [Fact(DisplayName = "Stepper Changing Event Testing")]
        public void StepperChanging()
        {
            var isChanging = false;
            var stepper = RenderComponent<SfStepper>(parameters => parameters.Add(s => s.StepChanging, (StepperChangeEventArgs args) =>
            {
                Assert.Equal("1", args.ActiveStep.ToString());
                Assert.Equal("0", args.PreviousStep.ToString());
                Assert.True(args.IsInteracted);
                Assert.False(args.Cancel);
                isChanging = true;
            }));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>().AddChildContent<StepperStep>().AddChildContent<StepperStep>()));
            var eventArgs = new StepperChangeEventArgs()
            {
                Cancel = false,
                IsInteracted = true,
                PreviousStep = 0,
                ActiveStep = 1
            };
            stepper.Instance.StepChangingHandler(eventArgs);
            Assert.True(isChanging);
        }
        [Fact(DisplayName = "Stepper Changed Event Testing")]
        public void StepperChanged()
        {
            var isChanged = false;
            var stepper = RenderComponent<SfStepper>(parameters => parameters.Add(s => s.StepChanged, (StepperChangedEventArgs args) =>
            {
                Assert.Equal("1", args.ActiveStep.ToString());
                Assert.Equal("0", args.PreviousStep.ToString());
                Assert.True(args.IsInteracted);
                isChanged = true;
            }));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>().AddChildContent<StepperStep>().AddChildContent<StepperStep>()));
            stepper.Instance.StepChangedHandler(true, 0, 1);
            Assert.True(isChanged);
        }
        [Fact(DisplayName = "Stepper Click Event Testing")]
        public void StepperClick()
        {
            var isClicked = false;
            var stepper = RenderComponent<SfStepper>(parameters => parameters.Add(s => s.StepClicked, (StepperClickedEventArgs args) =>
            {
                Assert.Equal("1", args.ActiveStep.ToString());
                Assert.Equal("0", args.PreviousStep.ToString());
                isClicked = true;
            }));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>().AddChildContent<StepperStep>().AddChildContent<StepperStep>()));
            stepper.Instance.StepClickHandler(0, 1);
            var eventArgs = new StepperChangeEventArgs()
            {
                Cancel = false,
                IsInteracted = true,
                PreviousStep = 0,
                ActiveStep = 1
            };
            stepper.Instance.StepChangingHandler(eventArgs);
            Assert.True(isClicked);
        }
        [Fact(DisplayName = "Stepper Rendered Event Testing")]
        public void StepperRendered()
        {
            var isRendered = false;
            var stepper = RenderComponent<SfStepper>(parameters => parameters.Add(s => s.StepRendered, (StepperRenderedEventArgs args) =>
            {
                Assert.Equal("", args.Step.IconCss.ToString());
                Assert.Equal("0", args.Index.ToString());
                isRendered = true;
            }));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>().AddChildContent<StepperStep>().AddChildContent<StepperStep>()));
            Assert.True(isRendered);
        }
        [Fact(DisplayName = "ActiveStep Property")]
        public void ActiveStep()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.ActiveStep), 1));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>().AddChildContent<StepperStep>().AddChildContent<StepperStep>()));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-completed", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-step-content\">1</span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-step-content\">2</span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-step-content\">3</span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Disabled property as True")]
        public void DisabledProperty()
        {
            var stepper = RenderComponent<SfStepper>();
            var stepperElement = stepper.Find(".e-stepper");            
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder").Add(p => p.Disabled, true)).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder"))));
            stepper.Render();
            Assert.Contains("e-step-disabled", stepperElement.Children[1].Children[0].ClassName);
        }
        [Fact(DisplayName = "Disabled property as False")]
        public void DisabledPropertyAsFalse()
        {
            var stepper = RenderComponent<SfStepper>();
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder").Add(p => p.Disabled, false)).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder"))));
            stepper.Render();
            var stepperElement = stepper.Find(".e-stepper");
            Assert.DoesNotContain("e-step-disabled", stepperElement.Children[1].Children[0].ClassName);
        }
        [Fact(DisplayName = "AriaDisabled Attribute Rendering")]
        public void AriaDisabledAttributeRendering()
        {
            var stepper = RenderComponent<SfStepper>();
            stepper.SetParametersAndRender(parameters => parameters
                .AddChildContent<StepperSteps>(p => p
                    .AddChildContent<StepperStep>(parameters => parameters
                        .Add(step => step.Label, "Payment")
                        .Add(step => step.Disabled, true))
                    .AddChildContent<StepperStep>(parameters => parameters
                        .Add(step => step.Label, "Review")
                        .Add(step => step.Disabled, false))
                    .AddChildContent<StepperStep>(parameters => parameters
                        .Add(step => step.Label, "Complete"))));
            stepper.Render();
            var labels = stepper.FindAll(".e-label");
            Assert.Equal("true", labels[0].GetAttribute("aria-disabled"));
            Assert.Null(labels[1].GetAttribute("aria-disabled"));
            Assert.Null(labels[2].GetAttribute("aria-disabled"));
        }
        [Fact(DisplayName = "Animation Property")]
        public void Animation()
        {
            var stepper = RenderComponent<SfStepper>();
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>().AddChildContent<StepperStep>().AddChildContent<StepperStep>()));
            stepper.Render();            
            Assert.Contains("e-control e-stepper e-lib", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var progressStyle = stepperElement.Children[0].Children[0].GetAttribute("data-sf-style");
            Assert.Contains("--progress-value:0%", progressStyle);
            Assert.Contains("transition-delay:0ms", progressStyle);
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperAnimationSettings>(parameters => parameters.Add(p => p.Enable, true).Add(p => p.Duration, 2000).Add(p => p.Delay, 500)));
            stepper.Render();
            progressStyle = stepperElement.Children[0].Children[0].GetAttribute("data-sf-style");
            Assert.Contains("--progress-value:0%", progressStyle);
            Assert.Contains("transition-delay:500ms", progressStyle);
        }
        [Fact(DisplayName = "ShowTooltip Property")]
        public void ShowTooltip()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.ShowTooltip), true));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Text, "Step1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Text, "Step2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Text, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-item", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-item", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-item", stepperElement.Children[1].Children[2].GetAttribute("class"));
            Assert.Contains("e-control e-tooltip", stepperElement.Children[1].Children[0].Children[1].GetAttribute("class"));
        }
        [Fact(DisplayName = "Step Tooltip With Text Only As Indicator")]
        public void TooltipTextOnlyIndicator()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.StepType), StepperType.Indicator), (nameof(SfStepper.ShowTooltip), true));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Text, "Step1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Text, "Step2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Text, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal e-step-type-indicator", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[2].GetAttribute("class"));
            Assert.Contains("e-control e-tooltip e-lib", stepperElement.Children[1].Children[0].Children[1].GetAttribute("class"));
            var tooltipElement = stepper.FindAll("div.e-tooltip");
            Assert.Equal(3, tooltipElement.Count);
        }
        [Fact(DisplayName = "Step Tooltip With Label Only As Indicator")]
        public void TooltipLabelOnlyIndicator()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.StepType), StepperType.Indicator), (nameof(SfStepper.ShowTooltip), true));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Label, "Step1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Label, "Step2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Label, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal e-step-type-indicator", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[2].GetAttribute("class"));
            Assert.Contains("e-control e-tooltip e-lib", stepperElement.Children[1].Children[0].Children[1].GetAttribute("class"));
            var tooltipElement = stepper.FindAll("div.e-tooltip");
            Assert.Equal(3, tooltipElement.Count);
        }
        [Fact(DisplayName = "Step Tooltip With Icon and Text As Indicator")]
        public void TooltipIconAndTextIndicator()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.StepType), StepperType.Indicator), (nameof(SfStepper.ShowTooltip), true));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder").Add(p => p.Text, "Step1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-cut").Add(p => p.Text, "Step2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-copy").Add(p => p.Text, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal e-step-type-indicator", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-item", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-item", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-item", stepperElement.Children[1].Children[2].GetAttribute("class"));
            Assert.Contains("e-control e-tooltip e-lib", stepperElement.Children[1].Children[0].Children[1].GetAttribute("class"));
            var tooltipElement = stepper.FindAll("div.e-tooltip");
            Assert.Equal(3, tooltipElement.Count);
        }
        [Fact(DisplayName = "Step Tooltip With Icon and Label As Indicator")]
        public void TooltipIconAndLabelIndicator()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.StepType), StepperType.Indicator), (nameof(SfStepper.ShowTooltip), true));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder").Add(p => p.Label, "Step1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-cut").Add(p => p.Label, "Step2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-copy").Add(p => p.Label, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal e-step-type-indicator", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-item", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-item", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-item", stepperElement.Children[1].Children[2].GetAttribute("class"));
            Assert.Contains("e-control e-tooltip e-lib", stepperElement.Children[1].Children[0].Children[1].GetAttribute("class"));
            var tooltipElement = stepper.FindAll("div.e-tooltip");
            Assert.Equal(3, tooltipElement.Count);
        }
        [Fact(DisplayName = "Stepper With Status Properties")]
        public void StepStatus()
        {
            var stepper = RenderComponent<StepperCompleteStatus>();
            #pragma warning disable BL0005 // Component parameter should not be set outside of its component.
            stepper.Instance.stepperStatus.Status = StepperStatus.Completed;
            stepper.Render();
            Assert.Equal(StepperStatus.Completed, stepper.Instance.stepperStatus.Status);
            stepper.Instance.stepperStatus.Status = StepperStatus.InProgress;
            stepper.Render();
            Assert.Equal(StepperStatus.InProgress, stepper.Instance.stepperStatus.Status);
            stepper.Instance.stepperStatus.Status = StepperStatus.NotStarted;
            #pragma warning restore BL0005
            stepper.Render();
            Assert.Equal(StepperStatus.NotStarted, stepper.Instance.stepperStatus.Status);
        }
        [Fact(DisplayName = "Linear Property")]
        public void LinearMode()
        {
            JSInterop.Setup<string>("sfBlazor.Stepper.initialize", _ => true).SetResult("{\"linear\":\"true\",\"activeStep\":\"0\"}");
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.Linear), true));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>().AddChildContent<StepperStep>().AddChildContent<StepperStep>()));
            stepper.Render();
            stepper.Instance.StepClickHandler(0, 2);
            Assert.Equal(0, stepper.Instance.ActiveStep);
            #pragma warning disable BL0005
            stepper.Instance.Linear = false;
            #pragma warning restore BL0005
            stepper.Render();
            stepper.Instance.StepClickHandler(0, 2);
            stepper.Instance.StepChangedHandler(true, 0, 2);
            Assert.Equal(2, stepper.Instance.ActiveStep);
        }
        [Fact(DisplayName = "Next Step method")]
        public async void NextStep()
        {
            var stepper = RenderComponent<SfStepper>();
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>().AddChildContent<StepperStep>().AddChildContent<StepperStep>()));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[2].GetAttribute("class"));
            Assert.Equal(0, stepper.Instance.ActiveStep);
            await stepper.Instance.NextStepAsync();
            stepper.Instance.StepChangedHandler(true, 0, 1);
            Assert.Equal(1, stepper.Instance.ActiveStep);
        }
        [Fact(DisplayName = "Previous Step method")]
        public async void PrevStep()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.ActiveStep), 1));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>().AddChildContent<StepperStep>().AddChildContent<StepperStep>()));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-completed", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[2].GetAttribute("class"));
            Assert.Equal(1, stepper.Instance.ActiveStep);
            await stepper.Instance.PreviousStepAsync();
            stepper.Instance.StepChangedHandler(true, 1, 0);
            Assert.Equal(0, stepper.Instance.ActiveStep);
        }
        [Fact(DisplayName = "Reset Step method")]
        public async void ResetStep()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.ActiveStep), 1));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>().AddChildContent<StepperStep>().AddChildContent<StepperStep>()));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-completed", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[2].GetAttribute("class"));
            Assert.Equal(1, stepper.Instance.ActiveStep);
            await stepper.Instance.ResetAsync();
            stepper.Instance.StepChangedHandler(true, 1, 0);
            Assert.Equal(0, stepper.Instance.ActiveStep);
        }
        [Fact(DisplayName = "Stepper with Html Attribute")]
        public void StepperWithHtmlAttribute()
        {
            Dictionary<string, object> htmlAttribute = new Dictionary<string, object>() {
                {"style", "background-color: darkgrey" },
                {"data-myattribute", "customvalue" }
            };
            var stepper = RenderComponent<SfStepper>(("htmlAttributes", htmlAttribute));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-cut")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-copy"))));
            stepper.Render();
            Assert.Contains("customvalue", stepperElement.GetAttribute("data-myattribute"));
            var stepperStyle = stepperElement.GetAttribute("data-sf-style");
            Assert.Contains("background-color", stepperStyle);
            Assert.Contains("darkgrey", stepperStyle);
        }
        [Fact(DisplayName = "Step With Icon Only")]
        public void IconOnly()
        {
            var stepper = RenderComponent<SfStepper>();
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-cut")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-copy"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-item", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-item", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-item", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-indicator e-icons e-folder\"></span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-cut\"></span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-copy\"></span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
            stepper.SetParametersAndRender(parameters => parameters.Add(p => p.ActiveStep, 1));
            stepper.Render();
            Assert.Contains("e-step-container  e-step-completed e-previous e-step-item", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-item", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-item", stepperElement.Children[1].Children[2].GetAttribute("class"));
        }
        [Fact(DisplayName = "Step With Text Only")]
        public void TextOnly()
        {
            var stepper = RenderComponent<SfStepper>();
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Text, "Step1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Text, "Step2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Text, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-item", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-item", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-item", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-indicator\">Step1</span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator\">Step2</span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator\">Step3</span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Step With Label Only")]
        public void LabelOnly()
        {
            var stepper = RenderComponent<SfStepper>();
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Label, "Step1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Label, "Step2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Label, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-label e-step-label-only", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-label e-step-label-only", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-label e-step-label-only", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step-label-container e-label-after\"><span class=\"e-label\">Step1</span></span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step-label-container e-label-after\"><span class=\"e-label\">Step2</span></span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step-label-container e-label-after\"><span class=\"e-label\">Step3</span></span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Step With Default Indicator")]
        public void Indicator()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.StepType), StepperType.Indicator));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>().AddChildContent<StepperStep>().AddChildContent<StepperStep>()));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal e-step-type-indicator", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-icons e-step-indicator\"></span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Step With Icon and Text")]
        public void IconAndText()
        {
            var stepper = RenderComponent<SfStepper>();
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder").Add(p => p.Text, "Step1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-cut").Add(p => p.Text, "Step2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-copy").Add(p => p.Text, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-item e-step-text", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-item e-step-text", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-item e-step-text", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-indicator e-icons e-folder\"></span><span class=\"e-step-text-container e-text\">Step1</span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-cut\"></span><span class=\"e-step-text-container e-text\">Step2</span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-copy\"></span><span class=\"e-step-text-container e-text\">Step3</span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Step With Icon and Label")]
        public void IconAndLabel()
        {
            var stepper = RenderComponent<SfStepper>();
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder").Add(p => p.Label, "Step1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-cut").Add(p => p.Label, "Step2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-copy").Add(p => p.Label, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-item e-step-label", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-item e-step-label", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-item e-step-label", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-indicator e-icons e-folder\"></span><span class=\"e-step-label-container e-label-after\"><span class=\"e-label\">Step1</span></span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-cut\"></span><span class=\"e-step-label-container e-label-after\"><span class=\"e-label\">Step2</span></span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-copy\"></span><span class=\"e-step-label-container e-label-after\"><span class=\"e-label\">Step3</span></span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Step With Icon, Text and Label")]
        public void IconTextAndLabel()
        {
            var stepper = RenderComponent<SfStepper>();
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder").Add(p => p.Label, "Step1").Add(p => p.Text, "Text1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-cut").Add(p => p.Label, "Step2").Add(p => p.Text, "Text2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-copy").Add(p => p.Label, "Step3").Add(p => p.Text, "Text3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-item e-step-label", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-item e-step-label", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-item e-step-label", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-indicator e-icons e-folder\"></span><span class=\"e-step-label-container e-label-after\"><span class=\"e-label\">Step1</span></span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-cut\"></span><span class=\"e-step-label-container e-label-after\"><span class=\"e-label\">Step2</span></span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-copy\"></span><span class=\"e-step-label-container e-label-after\"><span class=\"e-label\">Step3</span></span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Step With Icon and Before Label Position")]
        public void IconAndBeforeLabel()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.LabelPosition), StepperLabelPosition.Top));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder").Add(p => p.Label, "Step1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-cut").Add(p => p.Label, "Step2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-copy").Add(p => p.Label, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-item e-step-label", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-item e-step-label", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-item e-step-label", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-indicator e-icons e-folder\"></span><span class=\"e-step-label-container e-label-before\"><span class=\"e-label\">Step1</span></span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-cut\"></span><span class=\"e-step-label-container e-label-before\"><span class=\"e-label\">Step2</span></span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-copy\"></span><span class=\"e-step-label-container e-label-before\"><span class=\"e-label\">Step3</span></span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Step With Icon and Start Label Position")]
        public void IconAndStartLabel()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.LabelPosition), StepperLabelPosition.Start));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder").Add(p => p.Label, "Step1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-cut").Add(p => p.Label, "Step2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-copy").Add(p => p.Label, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-item e-step-text", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-item e-step-text", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-item e-step-text", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-indicator e-icons e-folder\"></span><span class=\"e-step-text-container e-text\">Step1</span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-cut\"></span><span class=\"e-step-text-container e-text\">Step2</span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-copy\"></span><span class=\"e-step-text-container e-text\">Step3</span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Step With Icon and End Label Position")]
        public void IconAndEndLabel()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.LabelPosition), StepperLabelPosition.End));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder").Add(p => p.Label, "Step1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-cut").Add(p => p.Label, "Step2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-copy").Add(p => p.Label, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-item e-step-text", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-item e-step-text", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-item e-step-text", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-indicator e-icons e-folder\"></span><span class=\"e-step-text-container e-text\">Step1</span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-cut\"></span><span class=\"e-step-text-container e-text\">Step2</span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-copy\"></span><span class=\"e-step-text-container e-text\">Step3</span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Step With Icon, Text And Label")]
        public void IconTextLabel()
        {
            var stepper = RenderComponent<SfStepper>();
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder").Add(p => p.Label, "Step1").Add(p => p.Text, "Text1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-cut").Add(p => p.Label, "Step2").Add(p => p.Text, "Text2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-copy").Add(p => p.Label, "Step3").Add(p => p.Text, "Text3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-item e-step-label", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-item e-step-label", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-item e-step-label", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-indicator e-icons e-folder\"></span><span class=\"e-step-label-container e-label-after\"><span class=\"e-label\">Step1</span></span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-cut\"></span><span class=\"e-step-label-container e-label-after\"><span class=\"e-label\">Step2</span></span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-copy\"></span><span class=\"e-step-label-container e-label-after\"><span class=\"e-label\">Step3</span></span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Step With Icon Only In Vertical Orientation")]
        public void IconOnlyVertOrient()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.Orientation), StepperOrientation.Vertical));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-cut")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-copy"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-vertical", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-item", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-item", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-item", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-indicator e-icons e-folder\"></span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-cut\"></span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-copy\"></span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
            stepper.SetParametersAndRender(parameters => parameters.Add(p => p.ActiveStep, 1));
            stepper.Render();
            Assert.Contains("e-step-container  e-step-completed e-previous e-step-item", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-item", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-item", stepperElement.Children[1].Children[2].GetAttribute("class"));
        }
        [Fact(DisplayName = "Step With Text Only In Vertical Orientation")]
        public void TextOnlyVertOrient()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.Orientation), StepperOrientation.Vertical));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Text, "Step1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Text, "Step2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Text, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-vertical", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-item", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-item", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-item", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-indicator\">Step1</span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator\">Step2</span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator\">Step3</span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Step With Label Only In Vertical Orientation")]
        public void LabelOnlyVertOrient()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.Orientation), StepperOrientation.Vertical));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Label, "Step1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Label, "Step2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Label, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-vertical", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-label e-step-label-only", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-label e-step-label-only", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-label e-step-label-only", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step-label-container e-label-bottom\"><span class=\"e-label\">Step1</span></span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step-label-container e-label-bottom\"><span class=\"e-label\">Step2</span></span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step-label-container e-label-bottom\"><span class=\"e-label\">Step3</span></span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Step With Icon and Text In Vertical Orientation")]
        public void IconAndTextVertOrient()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.Orientation), StepperOrientation.Vertical));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder").Add(p => p.Text, "Step1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-cut").Add(p => p.Text, "Step2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-copy").Add(p => p.Text, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-vertical", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-item e-step-text", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-item e-step-text", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-item e-step-text", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-indicator e-icons e-folder\"></span><span class=\"e-step-text-container e-text\">Step1</span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-cut\"></span><span class=\"e-step-text-container e-text\">Step2</span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-copy\"></span><span class=\"e-step-text-container e-text\">Step3</span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Step With Icon and Label In Vertical Orientation")]
        public void IconAndLabelVertOrient()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.Orientation), StepperOrientation.Vertical));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder").Add(p => p.Label, "Step1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-cut").Add(p => p.Label, "Step2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-copy").Add(p => p.Label, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-vertical", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-item e-step-text", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-item e-step-text", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-item e-step-text", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-indicator e-icons e-folder\"></span><span class=\"e-step-text-container e-text\">Step1</span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-cut\"></span><span class=\"e-step-text-container e-text\">Step2</span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-copy\"></span><span class=\"e-step-text-container e-text\">Step3</span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Step With Icon and Top Label Position In Vertical Orientation")]
        public void IconAndBeforeLabelVertOrient()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.LabelPosition), StepperLabelPosition.Top), (nameof(SfStepper.Orientation), StepperOrientation.Vertical));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder").Add(p => p.Label, "Step1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-cut").Add(p => p.Label, "Step2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-copy").Add(p => p.Label, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-vertical", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-item e-step-text", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-item e-step-text", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-item e-step-text", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-indicator e-icons e-folder\"></span><span class=\"e-step-text-container e-text\">Step1</span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-cut\"></span><span class=\"e-step-text-container e-text\">Step2</span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-copy\"></span><span class=\"e-step-text-container e-text\">Step3</span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Step With Icon and Start Label Position In Vertical Orientation")]
        public void IconAndStartLabelVertOrient()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.LabelPosition), StepperLabelPosition.Start), (nameof(SfStepper.Orientation), StepperOrientation.Vertical));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder").Add(p => p.Label, "Step1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-cut").Add(p => p.Label, "Step2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-copy").Add(p => p.Label, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-vertical", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-item e-step-label", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-item e-step-label", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-item e-step-label", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step-label-container e-label-before\"><span class=\"e-label\">Step1</span></span><span class=\"e-step e-indicator e-icons e-folder\"></span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step-label-container e-label-before\"><span class=\"e-label\">Step2</span></span><span class=\"e-step e-indicator e-icons e-cut\"></span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step-label-container e-label-before\"><span class=\"e-label\">Step3</span></span><span class=\"e-step e-indicator e-icons e-copy\"></span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Step With Icon and End Label Position In Vertical Orientation")]
        public void IconAndEndLabelVertOrient()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.LabelPosition), StepperLabelPosition.End), (nameof(SfStepper.Orientation), StepperOrientation.Vertical));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder").Add(p => p.Label, "Step1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-cut").Add(p => p.Label, "Step2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-copy").Add(p => p.Label, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-vertical", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-item e-step-label", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-item e-step-label", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-item e-step-label", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-indicator e-icons e-folder\"></span><span class=\"e-step-label-container e-label-after\"><span class=\"e-label\">Step1</span></span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-cut\"></span><span class=\"e-step-label-container e-label-after\"><span class=\"e-label\">Step2</span></span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-copy\"></span><span class=\"e-step-label-container e-label-after\"><span class=\"e-label\">Step3</span></span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Default stepper with IsValid Property")]
        public void IsValid()
        {
            var stepper = RenderComponent<SfStepper>();
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IsValid, true)).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IsValid, false)).AddChildContent<StepperStep>()));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-valid", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-error", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-indicator e-icons e-check\"></span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-circle-info\"></span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-step-content\">3</span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Default Indicator stepper with IsValid Property")]
        public void IndicatorIsValid()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.StepType), StepperType.Indicator));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IsValid, true)).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IsValid, false)).AddChildContent<StepperStep>()));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal e-step-type-indicator", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-valid", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-error", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-indicator e-icons e-check\"></span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-circle-info\"></span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-step-indicator e-icons\"></span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Step With Icon Only with IsValid Property")]
        public void IconOnlyIsValid()
        {
            var stepper = RenderComponent<SfStepper>();
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder").Add(p => p.IsValid, true)).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-cut").Add(p => p.IsValid, false)).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-copy"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-item e-step-valid", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-item e-step-error", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-item", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-indicator e-icons e-check\"></span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-circle-info\"></span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-copy\"></span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Step With Text Only with IsValid Property")]
        public void TextOnlyIsValid()
        {
            var stepper = RenderComponent<SfStepper>();
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Text, "Step1").Add(p => p.IsValid, true)).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Text, "Step2").Add(p => p.IsValid, false)).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Text, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-item e-step-valid", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-item e-step-error", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-item", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-indicator e-icons e-check\"></span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-circle-info\"></span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator\">Step3</span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Step With Label Only with IsValid Property")]
        public void LabelOnlyIsValid()
        {
            var stepper = RenderComponent<SfStepper>();
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Label, "Step1").Add(p => p.IsValid, true)).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Label, "Step2").Add(p => p.IsValid, false)).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Label, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-label e-step-label-only e-step-valid", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-label e-step-label-only e-step-error", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-label e-step-label-only", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step-label-container e-label-after\"><span class=\"e-label\">Step1</span><span class=\"e-step-validation-icon e-icons e-circle-check\"></span></span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step-label-container e-label-after\"><span class=\"e-label\">Step2</span><span class=\"e-step-validation-icon e-icons e-circle-info\"></span></span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step-label-container e-label-after\"><span class=\"e-label\">Step3</span></span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Step With Icon and Text with IsValid Property")]
        public void IconAndTextIsValid()
        {
            var stepper = RenderComponent<SfStepper>();
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder").Add(p => p.Text, "Step1").Add(p => p.IsValid, true)).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-cut").Add(p => p.Text, "Step2").Add(p => p.IsValid, false)).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-copy").Add(p => p.Text, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-item e-step-text e-step-valid", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-item e-step-text e-step-error", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-item e-step-text", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-indicator e-icons e-check\"></span><span class=\"e-step-text-container e-text\">Step1</span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-circle-info\"></span><span class=\"e-step-text-container e-text\">Step2</span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-copy\"></span><span class=\"e-step-text-container e-text\">Step3</span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Step With Icon and Label with IsValid Property")]
        public void IconAndLabelIsValid()
        {
            var stepper = RenderComponent<SfStepper>();
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder").Add(p => p.Label, "Step1").Add(p => p.IsValid, true)).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-cut").Add(p => p.Label, "Step2").Add(p => p.IsValid, false)).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-copy").Add(p => p.Label, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-item e-step-label e-step-valid", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-item e-step-label e-step-error", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-item e-step-label", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-indicator e-icons e-check\"></span><span class=\"e-step-label-container e-label-after\"><span class=\"e-label\">Step1</span></span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-circle-info\"></span><span class=\"e-step-label-container e-label-after\"><span class=\"e-label\">Step2</span></span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-copy\"></span><span class=\"e-step-label-container e-label-after\"><span class=\"e-label\">Step3</span></span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Step With Icon Only As Indicator")]
        public void IconOnlyIndicator()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.StepType), StepperType.Indicator));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-cut")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-copy"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal e-step-type-indicator", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-item", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-item", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-item", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-indicator e-icons e-folder\"></span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-cut\"></span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-copy\"></span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Step With Text Only As Indicator")]
        public void TextOnlyIndicator()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.StepType), StepperType.Indicator));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Text, "Step1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Text, "Step2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Text, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal e-step-type-indicator", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-indicator\">Step1</span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator\">Step2</span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator\">Step3</span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Step With Label Only As Indicator")]
        public void LabelOnlyIndicator()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.StepType), StepperType.Indicator));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Label, "Step1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Label, "Step2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Label, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal e-step-type-indicator", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-step-content\">Step1</span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-step-content\">Step2</span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-step-content\">Step3</span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Step With Icon and Text As Indicator")]
        public void IconAndTextIndicator()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.StepType), StepperType.Indicator));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder").Add(p => p.Text, "Step1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-cut").Add(p => p.Text, "Step2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-copy").Add(p => p.Text, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal e-step-type-indicator", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-item", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-item", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-item", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-indicator e-icons e-folder\"></span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-cut\"></span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-copy\"></span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Step With Icon and Label As Indicator")]
        public void IconAndLabelIndicator()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.StepType), StepperType.Indicator));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder").Add(p => p.Label, "Step1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-cut").Add(p => p.Label, "Step2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-copy").Add(p => p.Label, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal e-step-type-indicator", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-item", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-item", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-item", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-indicator e-icons e-folder\"></span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-cut\"></span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-copy\"></span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Step With Text Only As StepType Label")]
        public void TextOnlyLabel()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.StepType), StepperType.Label));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Text, "Step1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Text, "Step2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Text, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal e-step-type-label", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-step-content\">Step1</span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-step-content\">Step2</span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-step-content\">Step3</span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Step With Label Only As StepType Label")]
        public void LabelOnlyLabel()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.StepType), StepperType.Label));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Label, "Step1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Label, "Step2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Label, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal e-step-type-label", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step-label-container e-label-after\">\r\n  <span class=\"e-label\">Step1</span>\r\n</span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step-label-container e-label-after\">\r\n  <span class=\"e-label\">Step2</span>\r\n</span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step-label-container e-label-after\">\r\n  <span class=\"e-label\">Step3</span>\r\n</span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Step With Icon and Text As StepType Label")]
        public void IconAndTextLabel()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.StepType), StepperType.Label));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder").Add(p => p.Text, "Step1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-cut").Add(p => p.Text, "Step2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-copy").Add(p => p.Text, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal e-step-type-label", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-item e-step-text", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-item e-step-text", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-item e-step-text", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-indicator\">Step1</span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator\">Step2</span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator\">Step3</span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Step With Icon and Label As StepType Label")]
        public void IconAndLabelStepTypeLabel()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.StepType), StepperType.Label));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder").Add(p => p.Label, "Step1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-cut").Add(p => p.Label, "Step2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-copy").Add(p => p.Label, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal e-step-type-label", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-label e-step-label-only", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-label e-step-label-only", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-step-label e-step-label-only", stepperElement.Children[1].Children[2].GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step-label-container e-label-after\"><span class=\"e-label\">Step1</span></span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step-label-container e-label-after\"><span class=\"e-label\">Step2</span></span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step-label-container e-label-after\"><span class=\"e-label\">Step3</span></span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }
        [Fact(DisplayName = "Dynamically update CssClass property")]
        public void DynamicCssClassProperty()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.CssClass), "testClass"));
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>().AddChildContent<StepperStep>().AddChildContent<StepperStep>()));
            stepper.Render();
            var stepperElement = stepper.Find(".e-stepper");
            Assert.Contains("testClass", stepperElement.ClassName);
            stepper.SetParametersAndRender((nameof(SfStepper.CssClass), "updatedClass"));
            Assert.Contains("updatedClass", stepperElement.ClassName);
        }        
        [Fact(DisplayName = "Dynamically update ReadOnly property")]
        public void DynamicReadOnlyProperty()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.ReadOnly), true));
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>().AddChildContent<StepperStep>().AddChildContent<StepperStep>()));
            stepper.Render();
            var stepperElement = stepper.Find(".e-stepper");
            Assert.Contains("e-stepper-readonly", stepperElement.ClassName);
            stepper.SetParametersAndRender((nameof(SfStepper.ReadOnly), false));
            Assert.DoesNotContain("e-stepper-readonly", stepperElement.ClassName);
        }
        [Fact(DisplayName = "Dynamically update ActiveStep Property")]
        public void DynamicActiveStep()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.ActiveStep), 1));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>().AddChildContent<StepperStep>().AddChildContent<StepperStep>()));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-completed", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[2].GetAttribute("class"));
            stepper.SetParametersAndRender((nameof(SfStepper.ActiveStep), 0));
            stepper.Render();
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[2].GetAttribute("class"));
        }
        [Fact(DisplayName = "Dynamically change ShowTooltip Property")]
        public void DynamicShowTooltip()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.ShowTooltip), true));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Text, "Step1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Text, "Step2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.Text, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal", stepperElement.GetAttribute("class"));
            Assert.Contains("e-control e-tooltip", stepperElement.Children[1].Children[0].Children[1].GetAttribute("class"));
            var elementCount = stepperElement.Children[1].Children[0].ChildElementCount;
            Assert.Equal(2, elementCount);
            stepper.SetParametersAndRender((nameof(SfStepper.ShowTooltip), false));
            elementCount = stepperElement.Children[1].Children[0].ChildElementCount;
            Assert.Equal(1, elementCount);
        }
        [Fact(DisplayName = "Dynamically change orientation")]
        public void ChangeOrientation()
        {
            var stepper = RenderComponent<SfStepper>();
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>().AddChildContent<StepperStep>().AddChildContent<StepperStep>()));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[2].GetAttribute("class"));
            stepper.SetParametersAndRender((nameof(SfStepper.Orientation), StepperOrientation.Vertical));
            Assert.Contains("e-control e-stepper e-lib  e-vertical", stepperElement.GetAttribute("class"));
            Assert.DoesNotContain("e-horizontal", stepperElement.ClassName);
        }
        [Fact(DisplayName = "Step With Dynamic label position in horizontal orientation")]
        public void DynamicPostionInHorizontal()
        {
            var stepper = RenderComponent<SfStepper>();
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder").Add(p => p.Label, "Step1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-cut").Add(p => p.Label, "Step2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-copy").Add(p => p.Label, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal", stepperElement.GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-indicator e-icons e-folder\"></span><span class=\"e-step-label-container e-label-after\"><span class=\"e-label\">Step1</span></span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-cut\"></span><span class=\"e-step-label-container e-label-after\"><span class=\"e-label\">Step2</span></span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-copy\"></span><span class=\"e-step-label-container e-label-after\"><span class=\"e-label\">Step3</span></span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
            stepper.SetParametersAndRender((nameof(SfStepper.LabelPosition), StepperLabelPosition.Top));
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-folder\"></span><span class=\"e-step-label-container e-label-before\"><span class=\"e-label\">Step1</span></span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-cut\"></span><span class=\"e-step-label-container e-label-before\"><span class=\"e-label\">Step2</span></span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-copy\"></span><span class=\"e-step-label-container e-label-before\"><span class=\"e-label\">Step3</span></span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
            stepper.SetParametersAndRender((nameof(SfStepper.LabelPosition), StepperLabelPosition.Start));
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-folder\"></span><span class=\"e-step-text-container e-text\">Step1</span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-cut\"></span><span class=\"e-step-text-container e-text\">Step2</span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-copy\"></span><span class=\"e-step-text-container e-text\">Step3</span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
            stepper.SetParametersAndRender((nameof(SfStepper.LabelPosition), StepperLabelPosition.End));
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-folder\"></span><span class=\"e-step-text-container e-text\">Step1</span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-cut\"></span><span class=\"e-step-text-container e-text\">Step2</span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-copy\"></span><span class=\"e-step-text-container e-text\">Step3</span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }

        [Fact(DisplayName = "Step With Dynamic label position in vertical orientation")]
        public void DynamicPostionInVertical()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.Orientation), StepperOrientation.Vertical));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder").Add(p => p.Label, "Step1")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-cut").Add(p => p.Label, "Step2")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-copy").Add(p => p.Label, "Step3"))));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-vertical", stepperElement.GetAttribute("class"));
            var expectedOutput = "<span class=\"e-step e-indicator e-icons e-folder\"></span><span class=\"e-step-text-container e-text\">Step1</span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-cut\"></span><span class=\"e-step-text-container e-text\">Step2</span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-copy\"></span><span class=\"e-step-text-container e-text\">Step3</span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
            stepper.SetParametersAndRender((nameof(SfStepper.LabelPosition), StepperLabelPosition.Top));
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-folder\"></span><span class=\"e-step-text-container e-text\">Step1</span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-cut\"></span><span class=\"e-step-text-container e-text\">Step2</span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-copy\"></span><span class=\"e-step-text-container e-text\">Step3</span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
            stepper.SetParametersAndRender((nameof(SfStepper.LabelPosition), StepperLabelPosition.Start));
            expectedOutput = "<span class=\"e-step-label-container e-label-before\"><span class=\"e-label\">Step1</span></span><span class=\"e-step e-indicator e-icons e-folder\"></span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step-label-container e-label-before\"><span class=\"e-label\">Step2</span></span><span class=\"e-step e-indicator e-icons e-cut\"></span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step-label-container e-label-before\"><span class=\"e-label\">Step3</span></span><span class=\"e-step e-indicator e-icons e-copy\"></span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
            stepper.SetParametersAndRender((nameof(SfStepper.LabelPosition), StepperLabelPosition.End));
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-folder\"></span><span class=\"e-step-label-container e-label-after\"><span class=\"e-label\">Step1</span></span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-cut\"></span><span class=\"e-step-label-container e-label-after\"><span class=\"e-label\">Step2</span></span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-copy\"></span><span class=\"e-step-label-container e-label-after\"><span class=\"e-label\">Step3</span></span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
        }

        // Stepper CR Issues

        [Fact(DisplayName = "Fix the IsValid not working for default stepper configuration")]
        public void IsValidTrue()
        {
            var stepper = RenderComponent<SfStepper>();
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IsValid, true)).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IsValid, false)).AddChildContent<StepperStep>()));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-valid", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted e-next e-step-error", stepperElement.Children[1].Children[1].GetAttribute("class"));    
            var expectedOutput = "<span class=\"e-step e-indicator e-icons e-check\"></span>";
            stepperElement.Children[1].Children[0].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-indicator e-icons e-circle-info\"></span>";
            stepperElement.Children[1].Children[1].InnerHtml.MarkupMatches(expectedOutput);
            expectedOutput = "<span class=\"e-step e-step-content\">3</span>";
            stepperElement.Children[1].Children[2].InnerHtml.MarkupMatches(expectedOutput);
            var isCount = 0;
            var stepper1 = RenderComponent<SfStepper>(parameters => parameters.Add(s => s.StepChanging, (StepperChangeEventArgs args) =>
            {
                Assert.Equal("1", args.ActiveStep.ToString());
                Assert.Equal("0", args.PreviousStep.ToString());
                Assert.True(args.IsInteracted);
                Assert.False(args.Cancel);
                isCount++;
            }));
            var stepperElement1 = stepper.Find(".e-stepper");
            stepper1.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>().AddChildContent<StepperStep>().AddChildContent<StepperStep>()));
            var eventArgs = new StepperChangeEventArgs()
            {
                Cancel = false,
                IsInteracted = true,
                PreviousStep = 0,
                ActiveStep = 1
            };
            stepper1.Instance.StepChangingHandler(eventArgs);
            Assert.True(isCount > 0);
        }


        [Fact(DisplayName = "Fix the issue setting an active step higher then 1 at initialization of the Stepper with a list of steps")]
        public void ActiveStepHigherThanOne()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.ActiveStep), 2));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>().AddChildContent<StepperStep>().AddChildContent<StepperStep>()));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-completed", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-completed", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress", stepperElement.Children[1].Children[2].GetAttribute("class"));
        }

        [Fact(DisplayName = "Custom Next Button Step method")]
        public async void CustomNextButton()
        {
            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.ActiveStep), 1));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>().AddChildContent<StepperStep>().AddChildContent<StepperStep>()));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-completed", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-notstarted", stepperElement.Children[1].Children[2].GetAttribute("class"));
            Assert.Equal(1, stepper.Instance.ActiveStep);
            await stepper.Instance.NextStepAsync();
            stepper.Instance.StepChangedHandler(true, 0, 1);
            Assert.Equal(1, stepper.Instance.ActiveStep);
            Assert.Equal(1, stepper.Instance.ActiveStep);
            await stepper.Instance.PreviousStepAsync();
            stepper.Instance.StepChangedHandler(true, 1, 0);
            Assert.Equal(0, stepper.Instance.ActiveStep);
        }

        [Fact(DisplayName = "Stepper disabled state update when being navigated while using methods")]
        public async void StepperDisabledState()
        {
            var isClicked = true;
            var stepper = RenderComponent<SfStepper>();
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder").Add(p => p.Disabled, true)).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder")).AddChildContent<StepperStep>(parameters => parameters.Add(p => p.IconCss, "e-icons e-folder"))));
            stepper.Render();
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress e-step-item e-step-disabled", stepperElement.Children[1].Children[0].ClassName);
            Assert.Equal(0, stepper.Instance.ActiveStep);
            await stepper.Instance.NextStepAsync();
            stepper.Instance.StepChangedHandler(true, 0, 1);
            Assert.Equal(1, stepper.Instance.ActiveStep);
            stepper.Instance.StepClickHandler(1, 2);
            stepper.Instance.StepChangedHandler(true, 1, 2);
            Assert.Equal(2, stepper.Instance.ActiveStep);
            await stepper.Instance.PreviousStepAsync();
            stepper.Instance.StepChangedHandler(true, 2, 1);
            Assert.Equal(1, stepper.Instance.ActiveStep);
        }
        // Task - 876064
        [Fact(DisplayName = "Two Stepper ActiveStep Event Testing")]
        public void TwoStepperActiveStep()
        {

            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.ActiveStep), 2));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>().AddChildContent<StepperStep>().AddChildContent<StepperStep>()));
            stepper.Render();
            Assert.Contains("e-control e-stepper e-lib  e-horizontal", stepperElement.GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-completed", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-completed", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress", stepperElement.Children[1].Children[2].GetAttribute("class"));
        }

        [Fact(DisplayName = "Three Stepper ActiveStep Event Testing")]
        public void ThreeStepperActiveStep()
        {

            var stepper = RenderComponent<SfStepper>((nameof(SfStepper.ActiveStep), 3));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>().AddChildContent<StepperStep>().AddChildContent<StepperStep>().AddChildContent<StepperStep>()));
            stepper.Render();
            Assert.Contains("e-step-container  e-step-completed", stepperElement.Children[1].Children[0].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-completed", stepperElement.Children[1].Children[1].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-completed", stepperElement.Children[1].Children[2].GetAttribute("class"));
            Assert.Contains("e-step-container  e-step-selected e-step-inprogress", stepperElement.Children[1].Children[3].GetAttribute("class"));
        }

        // Task - 875402 

        [Fact(DisplayName = "Dynamically AddingNewSteps")]
        public void TestStepperAddEvent()
        {

            // Arrange
            var stepperComponent = RenderComponent<DynamicallyAddingNewSteps>();
            var stepperSteps = stepperComponent.FindAll("stepper-step");
            var initialStepCount = stepperSteps.Count;
            var addButton = stepperComponent.Find("button");

            // Act
            addButton.Click();

            // Assert
            var updatedStepperSteps = stepperComponent.FindAll("stepper-step");
            Assert.False(updatedStepperSteps.Count == initialStepCount + 1);
        }


        [Fact(DisplayName = "Dynamically AddingNewSteps Should Trigger StepChanging Event")]
        public void TestStepChangingEvent_True()
        {

            var isChanged = false;
            var stepper = RenderComponent<SfStepper>(parameters => parameters.Add(s => s.StepChanged, (StepperChangedEventArgs args) =>
            {
                Assert.Equal("1", args.ActiveStep.ToString());
                Assert.Equal("0", args.PreviousStep.ToString());
                Assert.True(args.IsInteracted);
                isChanged = true;
            }));
            var stepperElement = stepper.Find(".e-stepper");
            stepper.SetParametersAndRender(parameters => parameters.AddChildContent<StepperSteps>(p => p.AddChildContent<StepperStep>().AddChildContent<StepperStep>().AddChildContent<StepperStep>()));
            stepper.Instance.StepChangedHandler(true, 0, 1);
            Assert.True(isChanged);
        }
        [Fact(DisplayName = "Test RefreshProgressbarAsync Execution")]
        public async Task TestRefreshProgressbarAsync()
        {
            var cut = RenderComponent<SfStepper>();

            await cut.InvokeAsync(() => cut.Instance.RefreshProgressbarAsync());
            var progressBar = cut.FindAll(".e-stepper-progressbar");
            Assert.NotNull(progressBar);
        }
        [Fact(DisplayName = "Test HtmlAttributes Property Set and Get")]
        public void TestHtmlAttributes()
        {
            var attributes = new Dictionary<string, object>
        {
            { "data-sf-style", "width:300px" },
            { "class", "custom-stepper-class" }
        };
            var cut = RenderComponent<SfStepper>(parameters => parameters
                .Add(p => p.HtmlAttributes, attributes)
            );
            var stepperComponent = cut.Instance;
            Assert.Equal(attributes, stepperComponent.HtmlAttributes);
            var stepperContainer = cut.Find(".custom-stepper-class");
            var stepperStyle = stepperContainer.GetAttribute("data-sf-style");
            Assert.Contains("width", stepperStyle);
            Assert.Contains("300px", stepperStyle);
        }
    }
}
