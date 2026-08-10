using AngleSharp.Css.Dom;
using Bunit;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Tests.Tabs.Samples.MultiRowModeHeaderPositionRightWithInitContent.Events;
using System.Threading.Tasks;
using Xunit;

namespace Syncfusion.Blazor.Tests.Tabs
{
    public class MultiRowModeHeaderPositionRightWithInitContentEvents : BunitTestContext
    {
        public Helper HelperCls = new();

        [Fact(Timeout = 10000, DisplayName = "Adding event testing")]
        public async Task AddingEvent()
        {
            var tab = RenderComponent<AddingEvent>();
            await Task.Delay(100);
            var toolbarItemsHtml = tab.Find("." + HelperCls.ToolbarItems);
            Assert.NotNull(toolbarItemsHtml);
            Assert.Contains(HelperCls.ToolbarMultiRow, toolbarItemsHtml.ClassName);
            var tabHtml = tab.Find("." + HelperCls.Tab);
            Assert.NotNull(tabHtml);
            Assert.Contains(HelperCls.VerticalTab, tabHtml.ClassName);
            Assert.Contains(HelperCls.VerticalRight, tabHtml.ClassName);
            var tabHeaderHtml = tab.Find("." + HelperCls.Toolbar);
            Assert.NotNull(tabHeaderHtml);
            Assert.Contains(HelperCls.Vertical, tabHeaderHtml.ClassName);
            Assert.Contains(HelperCls.VerticalRight, tabHeaderHtml.ClassName);
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
            var toolbarItemsHtml = tab.Find("." + HelperCls.ToolbarItems);
            Assert.NotNull(toolbarItemsHtml);
            Assert.Contains(HelperCls.ToolbarMultiRow, toolbarItemsHtml.ClassName);
            var tabHtml = tab.Find("." + HelperCls.Tab);
            Assert.NotNull(tabHtml);
            Assert.Contains(HelperCls.VerticalTab, tabHtml.ClassName);
            Assert.Contains(HelperCls.VerticalRight, tabHtml.ClassName);
            var tabHeaderHtml = tab.Find("." + HelperCls.Toolbar);
            Assert.NotNull(tabHeaderHtml);
            Assert.Contains(HelperCls.Vertical, tabHeaderHtml.ClassName);
            Assert.Contains(HelperCls.VerticalRight, tabHeaderHtml.ClassName);
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
            var toolbarItemsHtml = tab.Find("." + HelperCls.ToolbarItems);
            Assert.NotNull(toolbarItemsHtml);
            Assert.Contains(HelperCls.ToolbarMultiRow, toolbarItemsHtml.ClassName);
            var tabHtml = tab.Find("." + HelperCls.Tab);
            Assert.NotNull(tabHtml);
            Assert.Contains(HelperCls.VerticalTab, tabHtml.ClassName);
            Assert.Contains(HelperCls.VerticalRight, tabHtml.ClassName);
            var tabHeaderHtml = tab.Find("." + HelperCls.Toolbar);
            Assert.NotNull(tabHeaderHtml);
            Assert.Contains(HelperCls.Vertical, tabHeaderHtml.ClassName);
            Assert.Contains(HelperCls.VerticalRight, tabHeaderHtml.ClassName);
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
            var toolbarItemsHtml = tab.Find("." + HelperCls.ToolbarItems);
            Assert.NotNull(toolbarItemsHtml);
            Assert.Contains(HelperCls.ToolbarMultiRow, toolbarItemsHtml.ClassName);
            var tabHtml = tab.Find("." + HelperCls.Tab);
            Assert.NotNull(tabHtml);
            Assert.Contains(HelperCls.VerticalTab, tabHtml.ClassName);
            Assert.Contains(HelperCls.VerticalRight, tabHtml.ClassName);
            var tabHeaderHtml = tab.Find("." + HelperCls.Toolbar);
            Assert.NotNull(tabHeaderHtml);
            Assert.Contains(HelperCls.Vertical, tabHeaderHtml.ClassName);
            Assert.Contains(HelperCls.VerticalRight, tabHeaderHtml.ClassName);
            tab.FindAll("br")[0].NextElementSibling.MarkupMatches("<span></span>");
            tab.Find("button").Click();
            await Task.Delay(100);
            tab.FindAll("br")[0].NextElementSibling.MarkupMatches("<span>Removed event testing</span>");
        }
    }
}