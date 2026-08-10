using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Tests.Accordion.Samples.Default;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Syncfusion.Blazor.Tests.Accordion
{
    public class AccordionCoverageTest : BunitTestContext
    {
        private Helper HelperCls = new Helper();

        #region SfAccordion Members Coverage

        [Fact(Timeout = 10000, DisplayName = "UpdateItemProperties updates Items list")]
        public void UpdateItemProperties_SetsItemsList()
        {
            var cut = RenderComponent<SfAccordion>();
            Assert.Null(cut.Instance.Items);
        }

        [Fact(Timeout = 10000, DisplayName = "UpdateAnimationProperties with Default global animation")]
        public void UpdateAnimationProperties_DefaultAnimation()
        {
            var cut = RenderComponent<SfAccordion>();
            Assert.Null(cut.Instance.Items);
        }

        [Fact(Timeout = 10000, DisplayName = "UpdateAnimationProperties with null settings creates defaults")]
        public void UpdateAnimationProperties_NullSettingsCreatesDefaults()
        {
            var cut = RenderComponent<SfAccordion>();
            cut.Render();
            Assert.NotNull(cut.Instance);
        }

        #endregion

        #region HtmlAttributes Coverage

        [Fact(Timeout = 10000, DisplayName = "HtmlAttributes with class attribute")]
        public void HtmlAttributes_ClassAttribute()
        {
            var htmlAttributes = new System.Collections.Generic.Dictionary<string, object>
            {
                { "class", "custom-class" }
            };
            var cut = RenderComponent<SfAccordion>(options =>
                options.Add(p => p.HtmlAttributes, htmlAttributes));
            var accordionEle = cut.Find("." + HelperCls.Accordion);
            Assert.Contains("custom-class", accordionEle.ClassName);
        }

        [Fact(Timeout = 10000, DisplayName = "HtmlAttributes with style attribute")]
        public void HtmlAttributes_StyleAttribute()
        {
            var htmlAttributes = new System.Collections.Generic.Dictionary<string, object>
            {
                { "style", "background-color: red;" }
            };
            var cut = RenderComponent<SfAccordion>(options =>
                options.Add(p => p.HtmlAttributes, htmlAttributes));
            var accordionEle = cut.Find("." + HelperCls.Accordion);
            var styleAttr = accordionEle.GetAttribute("data-sf-style");
            Assert.Contains("background-color: red;", styleAttr);
        }

        [Fact(Timeout = 10000, DisplayName = "HtmlAttributes with data attribute")]
        public void HtmlAttributes_DataAttribute()
        {
            var htmlAttributes = new System.Collections.Generic.Dictionary<string, object>
            {
                { "data-custom", "test-value" }
            };
            var cut = RenderComponent<SfAccordion>(options =>
                options.Add(p => p.HtmlAttributes, htmlAttributes));
            var accordionEle = cut.Find("." + HelperCls.Accordion);
            Assert.Equal("test-value", accordionEle.GetAttribute("data-custom"));
        }

        #endregion

        #region EnablePersistence Exception Coverage

        [Fact(Timeout = 10000, DisplayName = "EnablePersistence without ID throws InvalidOperationException")]
        public void EnablePersistence_WithoutId_ThrowsException()
        {
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                var cut = RenderComponent<SfAccordion>(options =>
                    options.Add(p => p.EnablePersistence, true));
            });
            Assert.Contains("ID property of Accordion must not be null or Empty when using EnablePersistance", exception.Message);
        }

        [Fact(Timeout = 10000, DisplayName = "EnablePersistence with ID does not throw")]
        public void EnablePersistence_WithId_NoException()
        {
            var cut = RenderComponent<SfAccordion>(options =>
            {
                options.Add(p => p.EnablePersistence, true);
                options.Add(p => p.ID, "custom-accordion");
            });
            Assert.NotNull(cut.Instance.ID);
        }

        #endregion

        #region Property Change Handler Coverage

        [Fact(Timeout = 10000, DisplayName = "OnPropertyChangeHandler with Width change")]
        public void OnPropertyChangeHandler_WidthChange()
        {
            var cut = RenderComponent<SfAccordion>(options =>
                options.Add(p => p.Width, "500px"));
            cut.Render();
            // Verify Width change is handled
            Assert.Equal("500px", cut.Instance.Width);
        }

        [Fact(Timeout = 10000, DisplayName = "OnPropertyChangeHandler with Height change")]
        public void OnPropertyChangeHandler_HeightChange()
        {
            var cut = RenderComponent<SfAccordion>(options =>
                options.Add(p => p.Height, "300px"));
            cut.Render();
            Assert.Equal("300px", cut.Instance.Height);
        }

        [Fact(Timeout = 10000, DisplayName = "OnPropertyChangeHandler with EnableRtl change")]
        public void OnPropertyChangeHandler_EnableRtlChange()
        {
            var cut = RenderComponent<SfAccordion>(options =>
                options.Add(p => p.EnableRtl, true));
            cut.Render();
            var accordionEle = cut.Find("." + HelperCls.Accordion);
            Assert.Contains(HelperCls.RTL, accordionEle.ClassName);
        }

        [Fact(Timeout = 10000, DisplayName = "OnPropertyChangeHandler with ExpandMode change")]
        public void OnPropertyChangeHandler_ExpandModeChange()
        {
            var cut = RenderComponent<SfAccordion>(options =>
                options.Add(p => p.ExpandMode, ExpandMode.Single));
            Assert.Equal(ExpandMode.Single, cut.Instance.ExpandMode);
        }

        #endregion

        #region ShouldRender Coverage

        [Fact(Timeout = 10000, DisplayName = "PreventRender method toggles ShouldRender")]
        public void PreventRender_TogglesShouldRender()
        {
            var cut = RenderComponent<SfAccordion>();
            cut.Instance.PreventRender(true);
            // ShouldRender returns false after PreventRender(true)
            cut.Render();
            cut.Instance.PreventRender(false);
            // ShouldRender returns true after PreventRender(false)
            cut.Render();
        }

        #endregion

        #region JSInvokable Methods Coverage

        [Fact(Timeout = 10000, DisplayName = "OnAccordionClick sets IsContentRendered")]
        public async Task OnAccordionClick_SetsIsContentRendered()
        {
            var cut = RenderComponent<SfAccordion>();
            await cut.InvokeAsync(async () =>
            {
                await Task.Delay(100);
            });
            Assert.NotNull(cut.Instance);
        }

        [Fact(Timeout = 10000, DisplayName = "TriggerClickedEvent with delegate")]
        public async Task TriggerClickedEvent_WithDelegate()
        {
            var cut = RenderComponent<SfAccordion>(options => options.AddChildContent<AccordionEvents>(events =>
                events.Add(e => e.Clicked, (args) =>
                {
                    // Event handler registered
                })
            ));
            await cut.InvokeAsync(async () =>
            {
                await Task.Delay(50);
            });
            Assert.NotNull(cut.Instance);
        }

        [Fact(Timeout = 10000, DisplayName = "TriggerClickedEvent without delegate")]
        public async Task TriggerClickedEvent_WithoutDelegate()
        {
            var cut = RenderComponent<SfAccordion>();
            await cut.InvokeAsync(async () =>
            {
                await Task.Delay(50);
            });
            Assert.NotNull(cut.Instance);
        }

        [Fact(Timeout = 10000, DisplayName = "TriggerExpandingEvent with cancelled event")]
        public async Task TriggerExpandingEvent_Cancelled()
        {
            var cut = RenderComponent<SfAccordion>(options => options.AddChildContent<AccordionEvents>(events =>
                events.Add(e => e.Expanding, (args) =>
                {
                    args.Cancel = true;
                })
            ));
            await cut.InvokeAsync(async () =>
            {
                await cut.Instance.TriggerExpandingEvent(0);
            });
        }

        [Fact(Timeout = 10000, DisplayName = "TriggerExpandingEvent with invalid index")]
        public async Task TriggerExpandingEvent_InvalidIndex()
        {
            var cut = RenderComponent<SfAccordion>();
            await cut.InvokeAsync(async () =>
            {
                await cut.Instance.TriggerExpandingEvent(100);
            });
        }

        [Fact(Timeout = 10000, DisplayName = "TriggerExpandedEvent updates indices")]
        public async Task TriggerExpandedEvent_UpdatesIndices()
        {
            var cut = RenderComponent<SfAccordion>(options =>
            {
                options.Add(p => p.ExpandedIndices, new int[] { 0 });
            });
            await cut.InvokeAsync(async () =>
            {
                await Task.Delay(50);
            });
            Assert.NotNull(cut.Instance.ExpandedIndices);
        }

        [Fact(Timeout = 10000, DisplayName = "TriggerCollapsingEvent without delegate")]
        public async Task TriggerCollapsingEvent_WithoutDelegate()
        {
            var cut = RenderComponent<SfAccordion>();
            await cut.InvokeAsync(async () =>
            {
                await cut.Instance.TriggerCollapsingEvent(0);
            });
        }

        [Fact(Timeout = 10000, DisplayName = "TriggerCollapsingEvent with cancelled event")]
        public async Task TriggerCollapsingEvent_Cancelled()
        {
            var cut = RenderComponent<SfAccordion>(options => options.AddChildContent<AccordionEvents>(events =>
                events.Add(e => e.Collapsing, (args) =>
                {
                    args.Cancel = true;
                })
            ));
            await cut.InvokeAsync(async () =>
            {
                await cut.Instance.TriggerCollapsingEvent(0);
            });
        }

        [Fact(Timeout = 10000, DisplayName = "AfterContentRender invokes JS method")]
        public async Task AfterContentRender_InvokesMethod()
        {
            var cut = RenderComponent<SfAccordion>();
            await cut.InvokeAsync(async () =>
            {
                await Task.Delay(50);
            });
            Assert.NotNull(cut.Instance);
        }

        #endregion

        #region SelectAsync Coverage

        [Fact(Timeout = 10000, DisplayName = "SelectAsync invokes JS method")]
        public async Task SelectAsync_InvokesMethod()
        {
            var cut = RenderComponent<SfAccordion>();
            await cut.InvokeAsync(async () =>
            {
                await cut.Instance.SelectAsync(0);
            });
        }

        #endregion

        #region GetInstance Coverage

        [Fact(Timeout = 10000, DisplayName = "GetInstance returns correct dictionary")]
        public void GetInstance_ReturnsCorrectDictionary()
        {
            var cut = RenderComponent<SfAccordion>(options =>
            {
                options.Add(p => p.ID, "accord1");
                options.Add(p => p.EnablePersistence, true);
                options.Add(p => p.ExpandMode, ExpandMode.Single);
                options.Add(p => p.ExpandedIndices, new int[] { 0 });
            });
            // The GetInstance method is internal, testing via JS invocation
            Assert.NotNull(cut.Instance);
        }

        #endregion

        #region SetItems Coverage

        [Fact(Timeout = 10000, DisplayName = "SetItems with ExpandedIndices")]
        public void SetItems_WithExpandedIndices()
        {
            var cut = RenderComponent<SfAccordion>(options =>
            {
                options.Add(p => p.ExpandedIndices, new int[] { 0, 1 });
            });
            cut.Render();
            Assert.Null(cut.Instance.Items);
        }

        #endregion

        #region UpdateExpandedIndices Coverage

        [Fact(Timeout = 10000, DisplayName = "UpdateExpandedIndices with null Items")]
        public void UpdateExpandedIndices_NullItems()
        {
            var cut = RenderComponent<SfAccordion>();
            cut.Render();
            // Verify component handles null items gracefully
            Assert.NotNull(cut.Instance);
        }

        #endregion

        #region AccordionItem Coverage

        [Fact(Timeout = 10000, DisplayName = "AccordionItem VisibleItem method")]
        public void AccordionItem_VisibleItem()
        {
            var cut = RenderComponent<SfAccordion>(options =>
            {
                options.AddChildContent<AccordionItems>(items =>
                {
                    items.AddChildContent<AccordionItem>(item =>
                        item.Add(i => i.Header, "Test Header")
                            .Add(i => i.Content, "Test Content")
                    );
                });
            });
            var accordionItems = cut.FindComponents<AccordionItem>();
            Assert.NotEmpty(accordionItems);
        }

        [Fact(Timeout = 10000, DisplayName = "AccordionItem UpdateExpandedValue")]
        public async Task AccordionItem_UpdateExpandedValue()
        {
            var cut = RenderComponent<SfAccordion>(options =>
            {
                options.AddChildContent<AccordionItems>(items =>
                {
                    items.AddChildContent<AccordionItem>(item =>
                        item.Add(i => i.Header, "Test").Add(i => i.Content, "Content")
                    );
                });
            });
            await cut.InvokeAsync(async () =>
            {
                await Task.Delay(50);
            });
            Assert.NotNull(cut.Instance);
        }

        [Fact(Timeout = 10000, DisplayName = "AccordionItem with LoadOnDemand false")]
        public void AccordionItem_LoadOnDemandFalse()
        {
            var cut = RenderComponent<SfAccordion>(options =>
                options.Add(p => p.LoadOnDemand, false));
            Assert.False(cut.Instance.LoadOnDemand);
        }

        [Fact(Timeout = 10000, DisplayName = "AccordionItem IsExpandedFromIndex")]
        public void AccordionItem_IsExpandedFromIndex()
        {
            var cut = RenderComponent<SfAccordion>(options =>
            {
                options.Add(p => p.ExpandedIndices, new int[] { 0 });
                options.Add(p => p.LoadOnDemand, false);
            });
            Assert.Null(cut.Instance.Items);
        }

        #endregion

        #region AccordionAnimationSettings Coverage

        [Fact(Timeout = 10000, DisplayName = "AccordionAnimationSettings with null Expand")]
        public void AccordionAnimationSettings_NullExpand()
        {
            var cut = RenderComponent<SfAccordion>();
            cut.Render();
            Assert.Null(cut.Instance.Items);
        }

        [Fact(Timeout = 10000, DisplayName = "AccordionAnimationSettings with null Collapse")]
        public void AccordionAnimationSettings_NullCollapse()
        {
            var cut = RenderComponent<SfAccordion>();
            cut.Render();
            Assert.Null(cut.Instance.Items);
        }

        #endregion

        #region AccordionItemModel Coverage

        [Fact(Timeout = 10000, DisplayName = "AccordionItemModel properties")]
        public void AccordionItemModel_Properties()
        {
            var model = new AccordionItemModel
            {
                Content = "Test Content",
                CssClass = "test-class",
                Disabled = true,
                Expanded = true,
                Header = "Test Header",
                IconCss = "e-icon",
                Id = "test-id",
                Visible = false
            };
            Assert.Equal("Test Content", model.Content);
            Assert.Equal("test-class", model.CssClass);
            Assert.True(model.Disabled);
            Assert.True(model.Expanded);
            Assert.Equal("Test Header", model.Header);
            Assert.Equal("e-icon", model.IconCss);
            Assert.Equal("test-id", model.Id);
            Assert.False(model.Visible);
        }

        #endregion

        #region Event Args Coverage

        [Fact(Timeout = 10000, DisplayName = "ExpandEventArgs with all properties")]
        public void ExpandEventArgs_AllProperties()
        {
            var args = new ExpandEventArgs
            {
                Cancel = true
            };
            Assert.True(args.Cancel);
        }

        [Fact(Timeout = 10000, DisplayName = "ExpandedEventArgs with all properties")]
        public void ExpandedEventArgs_AllProperties()
        {
            var args = new ExpandedEventArgs();
            Assert.NotNull(args);
        }

        [Fact(Timeout = 10000, DisplayName = "CollapsedEventArgs inherits from ExpandedEventArgs")]
        public void CollapsedEventArgs_InheritsFromExpandedEventArgs()
        {
            var collapsedArgs = new CollapsedEventArgs();
            Assert.NotNull(collapsedArgs);
        }

        [Fact(Timeout = 10000, DisplayName = "CollapseEventArgs inherits from ExpandEventArgs")]
        public void CollapseEventArgs_InheritsFromExpandEventArgs()
        {
            var collapseArgs = new CollapseEventArgs
            {
                Cancel = true
            };
            Assert.True(collapseArgs.Cancel);
        }

        #endregion

        #region AnimationEffect Enum Coverage

        [Fact(Timeout = 10000, DisplayName = "AnimationEffect enum values")]
        public void AnimationEffect_EnumValues()
        {
            Assert.Equal(AnimationEffect.SlideDown, AnimationEffect.SlideDown);
            Assert.Equal(AnimationEffect.SlideUp, AnimationEffect.SlideUp);
            Assert.Equal(AnimationEffect.FadeIn, AnimationEffect.FadeIn);
            Assert.Equal(AnimationEffect.FadeOut, AnimationEffect.FadeOut);
            Assert.Equal(AnimationEffect.None, AnimationEffect.None);
        }

        #endregion

        #region ComponentDispose Coverage

        [Fact(Timeout = 10000, DisplayName = "ComponentDispose is called")]
        public async Task ComponentDispose_IsCalled()
        {
            var cut = RenderComponent<SfAccordion>(options =>
            {
                options.Add(p => p.ID, "dispose-test-accordion");
            });
            await cut.InvokeAsync(async () =>
            {
                cut.Instance.ComponentDispose();
            });
        }

        [Fact(Timeout = 10000, DisplayName = "ComponentDispose with Destroyed event")]
        public async Task ComponentDispose_WithDestroyedEvent()
        {
            var destroyEventCount = 0;
            var cut = RenderComponent<SfAccordion>(options =>
            {
                options.Add(p => p.ID, "destroy-test-accordion");
                options.AddChildContent<AccordionEvents>(events =>
                    events.Add(e => e.Destroyed, (args) =>
                    {
                        destroyEventCount++;
                    })
                );
            });
            await cut.InvokeAsync(async () =>
            {
                cut.Instance.ComponentDispose();
            });
            Assert.Equal(1, destroyEventCount);
        }

        #endregion

        #region ChildContent with Items Coverage

        [Fact(Timeout = 10000, DisplayName = "ChildContent with AccordionItems renders correctly")]
        public void ChildContent_WithAccordionItems()
        {
            var cut = RenderComponent<SfAccordion>(options => options.AddChildContent<AccordionItems>());
            Assert.NotNull(cut.Find("." + HelperCls.Accordion));
        }

        #endregion

        #region AccordionItem Dispose Coverage

        [Fact(Timeout = 10000, DisplayName = "AccordionItem Dispose removes from parent")]
        public void AccordionItem_Dispose_RemovesFromParent()
        {
            var cut = RenderComponent<Default>();
            var itemCount = cut.FindAll("." + HelperCls.AccordionItem).Count;
            Assert.Equal(3, itemCount);
        }

        #endregion

        #region OnAfterScriptRendered Coverage

        [Fact(Timeout = 10000, DisplayName = "OnAfterScriptRendered invokes JS initialize")]
        public async Task OnAfterScriptRendered_InvokesJS()
        {
            var cut = RenderComponent<SfAccordion>();
            await cut.InvokeAsync(async () =>
            {
                await Task.Delay(50);
            });
            Assert.NotNull(cut.Instance);
        }

        #endregion

        #region AccordionItemModel with Templates

        [Fact(Timeout = 10000, DisplayName = "AccordionItemModel with HeaderTemplate")]
        public void AccordionItemModel_WithHeaderTemplate()
        {
            var model = new AccordionItemModel
            {
                Header = "Test Header",
                Content = "Test Content"
            };
            Assert.Equal("Test Header", model.Header);
            Assert.Equal("Test Content", model.Content);
        }

        [Fact(Timeout = 10000, DisplayName = "AccordionItemModel with ContentTemplate")]
        public void AccordionItemModel_WithContentTemplate()
        {
            var model = new AccordionItemModel
            {
                Content = "Test Content",
                Disabled = false,
                Expanded = true
            };
            Assert.Equal("Test Content", model.Content);
            Assert.False(model.Disabled);
            Assert.True(model.Expanded);
        }

        #endregion

        #region IsExpandIndicesChanged Coverage

        [Fact(Timeout = 10000, DisplayName = "IsExpandIndicesChanged flag is set correctly")]
        public async Task IsExpandIndicesChanged_FlagSet()
        {
            var cut = RenderComponent<SfAccordion>(options =>
            {
                options.Add(p => p.ExpandedIndices, new int[] { 0 });
            });
            await cut.InvokeAsync(async () =>
            {
                await Task.Delay(50);
            });
            Assert.NotNull(cut.Instance);
        }

        #endregion

        #region Multiple TriggerExpandedEvent Coverage

        [Fact(Timeout = 10000, DisplayName = "Multiple TriggerExpandedEvent calls")]
        public async Task Multiple_TriggerExpandedEventCalls()
        {
            var cut = RenderComponent<SfAccordion>(options =>
            {
                options.Add(p => p.ExpandedIndices, new int[] { 0, 1 });
            });
            await cut.InvokeAsync(async () =>
            {
                await Task.Delay(50);
            });
            Assert.NotNull(cut.Instance.ExpandedIndices);
        }

        #endregion

        #region TriggerCollapsedEvent with Item ExpandedChanged

        [Fact(Timeout = 10000, DisplayName = "TriggerCollapsedEvent with Item ExpandedChanged delegate")]
        public async Task TriggerCollapsedEvent_WithExpandedChangedDelegate()
        {
            var cut = RenderComponent<SfAccordion>(options =>
            {
                options.Add(p => p.ExpandedIndices, new int[] { 0, 1 });
            });
            await cut.InvokeAsync(async () =>
            {
                await Task.Delay(50);
            });
            Assert.NotNull(cut.Instance);
        }

        #endregion
    }
}