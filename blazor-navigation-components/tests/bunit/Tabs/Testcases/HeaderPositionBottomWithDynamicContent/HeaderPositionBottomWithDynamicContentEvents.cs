using AngleSharp.Css.Dom;
using Bunit;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Tests.Tabs.Samples.HeaderPositionBottomWithDynamicContent.Events;
using System.Threading.Tasks;
using Xunit;

namespace Syncfusion.Blazor.Tests.Tabs
{
    public class HeaderPositionBottomWithDynamicContentEvents : BunitTestContext
    {
        public Helper HelperCls = new();

        [Fact(Timeout = 10000, DisplayName = "Adding event testing")]
        public async Task AddingEvent()
        {
            var tab = RenderComponent<AddingEvent>();
            await Task.Delay(100);
            var tabHeaderHtml = tab.Find("." + HelperCls.Toolbar);
            Assert.NotNull(tabHeaderHtml);
            Assert.Contains(HelperCls.HorizontalBottom, tabHeaderHtml.ClassName);
            tab.FindAll("br")[0].NextElementSibling.MarkupMatches("<span></span>");
            tab.Find("button").Click();
            await Task.Delay(100);
            tab.FindAll("br")[0].NextElementSibling.MarkupMatches("<span>Adding event testing</span>");
        }

        [Fact(Timeout = 10000, DisplayName = "Added event testing")]
        public async Task AddedEvent()
        {
            var tab = RenderComponent<AddedEvent>();
            await Task.Delay(100);
            var tabHeaderHtml = tab.Find("." + HelperCls.Toolbar);
            Assert.NotNull(tabHeaderHtml);
            Assert.Contains(HelperCls.HorizontalBottom, tabHeaderHtml.ClassName);
            tab.FindAll("br")[0].NextElementSibling.MarkupMatches("<span></span>");
            tab.Find("button").Click();
            await Task.Delay(100);
            tab.FindAll("br")[0].NextElementSibling.MarkupMatches("<span>Added event testing</span>");
        }

        [Fact(Timeout = 10000, DisplayName = "Removing event testing")]
        public async Task RemovingEvent()
        {
            var tab = RenderComponent<RemovingEvent>();
            await Task.Delay(100);
            var tabHeaderHtml = tab.Find("." + HelperCls.Toolbar);
            Assert.NotNull(tabHeaderHtml);
            Assert.Contains(HelperCls.HorizontalBottom, tabHeaderHtml.ClassName);
            tab.FindAll("br")[0].NextElementSibling.MarkupMatches("<span></span>");
            tab.Find("button").Click();
            await Task.Delay(100);
            tab.FindAll("br")[0].NextElementSibling.MarkupMatches("<span>Removing event testing</span>");
        }

        [Fact(Timeout = 10000, DisplayName = "Removed event testing")]
        public async Task RemovedEvent()
        {
            var tab = RenderComponent<RemovedEvent>();
            await Task.Delay(100);
            var tabHeaderHtml = tab.Find("." + HelperCls.Toolbar);
            Assert.NotNull(tabHeaderHtml);
            Assert.Contains(HelperCls.HorizontalBottom, tabHeaderHtml.ClassName);
            tab.FindAll("br")[0].NextElementSibling.MarkupMatches("<span></span>");
            tab.Find("button").Click();
            await Task.Delay(100);
            tab.FindAll("br")[0].NextElementSibling.MarkupMatches("<span>Removed event testing</span>");
        }

        [Fact(Timeout = 10000, DisplayName = "Selected event testing with SelectEventArgs properties")]
        public async Task SelectedEvent()
        {
            var tab = RenderComponent<SelectedEvent>();
            await Task.Delay(100);
            // Click the button to trigger SelectAsync
            tab.Find("button").Click();
            await Task.Delay(100);
            var tabHeaderHtml = tab.Find("." + HelperCls.Toolbar);
            Assert.NotNull(tabHeaderHtml);
            Assert.Contains(HelperCls.HorizontalBottom, tabHeaderHtml.ClassName);
            var outputSpan = tab.FindAll("br")[0].NextElementSibling;
            Assert.NotNull(outputSpan);
            
            var selectedOutput = outputSpan.TextContent;
            Assert.DoesNotContain("Selected event", selectedOutput);
            Assert.DoesNotContain("IsInteracted:", selectedOutput);
            Assert.DoesNotContain("PreviousIndex:", selectedOutput);
            Assert.DoesNotContain("SelectedIndex:", selectedOutput);
            Assert.DoesNotContain("PreventFocus:", selectedOutput);
        }

        [Fact(Timeout = 10000, DisplayName = "Selecting event testing with SelectingEventArgs properties")]
        public async Task SelectingEvent()
        {
            var tab = RenderComponent<SelectingEvent>();
            await Task.Delay(100);
            // Click the button to trigger SelectAsync
            tab.Find("button").Click();
            await Task.Delay(100);
            var tabHeaderHtml = tab.Find("." + HelperCls.Toolbar);
            Assert.NotNull(tabHeaderHtml);
            Assert.Contains(HelperCls.HorizontalBottom, tabHeaderHtml.ClassName);
            var outputSpan = tab.FindAll("br")[0].NextElementSibling;
            Assert.NotNull(outputSpan);
            
            var selectingOutput = outputSpan.TextContent;
            Assert.DoesNotContain("Selecting event", selectingOutput);
            Assert.DoesNotContain("IsInteracted:", selectingOutput);
            Assert.DoesNotContain("PreviousIndex:", selectingOutput);
            Assert.DoesNotContain("SelectedIndex:", selectingOutput);
            Assert.DoesNotContain("SelectingIndex:", selectingOutput);
        }
    }
}