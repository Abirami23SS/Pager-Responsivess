using AngleSharp.Css.Dom;
using Bunit;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Tests.Tabs.Samples.HeaderPositionBottomWithDemandContent.Events;
using System.Threading.Tasks;
using Xunit;

namespace Syncfusion.Blazor.Tests.Tabs
{
    public class HeaderPositionBottomWithDemandContentEvents : BunitTestContext
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

        [Fact(Timeout = 10000, DisplayName = "Created event testing")]
        public async Task CreatedEvent()
        {
            var tab = RenderComponent<CreatedEvent>();
            await Task.Delay(100);
            Assert.Contains("", tab.Instance.output);
            tab.Instance.onCreated();
            await Task.Delay(500);
            Assert.Contains("Created event testing", tab.Instance.output);
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
        [Fact(Timeout = 10000, DisplayName = "Drag and Drop event testing")]
        public async Task DragAndDrop()
        {
            var tab = RenderComponent<DragAndDrop>();
            await Task.Delay(100);
            var tabHtml = tab.Find("." + HelperCls.Tab);
            Assert.NotNull(tabHtml);
            var tabHeaderHtml = tab.Find("." + HelperCls.Toolbar);
            Assert.NotNull(tabHeaderHtml);
            Assert.Equal(3, tab.FindAll("." + HelperCls.ToolbarItem).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.TabWrap).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.TextWrap).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.TabText).Count);
            Assert.Equal("Twitter", tab.Find("." + HelperCls.ToolbarItem).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Facebook", tab.FindAll("." + HelperCls.ToolbarItem)[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Whatsapp", tab.FindAll("." + HelperCls.ToolbarItem)[2].TextContent.Replace("\n", string.Empty).Trim());
            await tab.Instance.Drag();
            await Task.Delay(1000);
            tab.Render();
            Assert.Equal("Facebook", tab.Find("." + HelperCls.ToolbarItem).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Twitter", tab.FindAll("." + HelperCls.ToolbarItem)[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Whatsapp", tab.FindAll("." + HelperCls.ToolbarItem)[2].TextContent.Replace("\n", string.Empty).Trim());
            tab.Instance.EnablePersistance = true;
            tab.Render();
            await tab.Instance.Drag();
            await Task.Delay(1000);
            Assert.Equal("Twitter", tab.Find("." + HelperCls.ToolbarItem).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Facebook", tab.FindAll("." + HelperCls.ToolbarItem)[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Whatsapp", tab.FindAll("." + HelperCls.ToolbarItem)[2].TextContent.Replace("\n", string.Empty).Trim());

        }
    }
}