using AngleSharp.Css.Dom;
using Bunit;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Tests.Tabs.Samples.HeaderPositionBottomWithDemandContent.PublicMethods;
using System.Threading.Tasks;
using Xunit;

namespace Syncfusion.Blazor.Tests.Tabs
{
    public class HeaderPositionBottomWithDemandContentPublicMethods : BunitTestContext
    {
        public Helper HelperCls = new();

        [Fact(Timeout = 10000, DisplayName = "AddTab public method testing")]
        public async Task AddTab()
        {
            var tab = RenderComponent<AddTab>();
            await Task.Delay(100);
            var tabHtml = tab.Find("." + HelperCls.Tab);
            Assert.NotNull(tabHtml);
            Assert.Contains(HelperCls.Control, tabHtml.ClassName);
            Assert.Contains(HelperCls.Tab, tabHtml.ClassName);
            Assert.Contains(HelperCls.Library, tabHtml.ClassName);
            var style = tabHtml.GetAttribute("data-sf-style").Replace(" ", string.Empty);
            Assert.Contains("width:100%", style);
            Assert.Contains("height:auto", style);
            var tabHeaderHtml = tab.Find("." + HelperCls.Toolbar);
            Assert.NotNull(tabHeaderHtml);
            Assert.Contains(HelperCls.TabHeader, tabHeaderHtml.ClassName);
            Assert.Contains(HelperCls.HorizontalBottom, tabHeaderHtml.ClassName);
            Assert.Equal(3, tab.FindAll("." + HelperCls.ToolbarItem).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.TabWrap).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.TextWrap).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.TabText).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.CloseIcon).Count);
            Assert.Equal(1, tab.FindAll("." + HelperCls.Item).Count);
            Assert.NotNull(tab.Find("." + HelperCls.Content));
            Assert.Equal("New York", tab.Find("." + HelperCls.ToolbarItem).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Los Angeles", tab.FindAll("." + HelperCls.ToolbarItem)[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Chicago", tab.FindAll("." + HelperCls.ToolbarItem)[2].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[0].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[2].ClassName);
            Assert.True(tab.Find("." + HelperCls.Tab).LastElementChild.ClassList.Contains(HelperCls.Content));
            //Assert.True(tab.Find("." + HelperCls.Item).TextContent.Replace("\n", string.Empty).Trim() == "Los Angeles is a sprawling Southern California city and the center of the nation’s film and television industry. Near its iconic Hollywood sign, studios such as Paramount Pictures, Universal and Warner Brothers offer behind-the-scenes tours. On Hollywood Boulevard, TCL Chinese Theatre displays celebrities’ hand- and footprints, the Walk of Fame honors thousands of luminaries and vendors sell maps to stars’ homes.");
            tab.Find(".e-btn").Click();
            await Task.Delay(100);
            Assert.Equal(4, tab.FindAll("." + HelperCls.ToolbarItem).Count);
            Assert.Equal(4, tab.FindAll("." + HelperCls.TabWrap).Count);
            Assert.Equal(4, tab.FindAll("." + HelperCls.TextWrap).Count);
            Assert.Equal(4, tab.FindAll("." + HelperCls.TabText).Count);
            Assert.Equal(4, tab.FindAll("." + HelperCls.CloseIcon).Count);
            Assert.Equal("Sydney", tab.Find("." + HelperCls.ToolbarItem).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("New York", tab.FindAll("." + HelperCls.ToolbarItem)[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Los Angeles", tab.FindAll("." + HelperCls.ToolbarItem)[2].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Chicago", tab.FindAll("." + HelperCls.ToolbarItem)[3].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[0].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[2].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[3].ClassName);
            Assert.True(tab.Find("." + HelperCls.Tab).LastElementChild.ClassList.Contains(HelperCls.Content));
        }

        [Fact(Timeout = 10000, DisplayName = "HideTab public method testing")]
        public async Task HideTab()
        {
            var tab = RenderComponent<HideTab>();
            await Task.Delay(100);
            var tabHtml = tab.Find("." + HelperCls.Tab);
            Assert.NotNull(tabHtml);
            Assert.Contains(HelperCls.Control, tabHtml.ClassName);
            Assert.Contains(HelperCls.Tab, tabHtml.ClassName);
            Assert.Contains(HelperCls.Library, tabHtml.ClassName);
            var style = tabHtml.GetAttribute("data-sf-style").Replace(" ", string.Empty);
            Assert.Contains("width:100%", style);
            Assert.Contains("height:auto", style);
            var tabHeaderHtml = tab.Find("." + HelperCls.Toolbar);
            Assert.NotNull(tabHeaderHtml);
            Assert.Contains(HelperCls.TabHeader, tabHeaderHtml.ClassName);
            Assert.Contains(HelperCls.HorizontalBottom, tabHeaderHtml.ClassName);
            Assert.Equal(3, tab.FindAll("." + HelperCls.ToolbarItem).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.TabWrap).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.TextWrap).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.TabText).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.CloseIcon).Count);
            Assert.Equal(1, tab.FindAll("." + HelperCls.Item).Count);
            Assert.NotNull(tab.Find("." + HelperCls.Content));
            Assert.Equal("New York", tab.Find("." + HelperCls.ToolbarItem).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Los Angeles", tab.FindAll("." + HelperCls.ToolbarItem)[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Chicago", tab.FindAll("." + HelperCls.ToolbarItem)[2].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[0].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[2].ClassName);
            Assert.True(tab.Find("." + HelperCls.Tab).LastElementChild.ClassList.Contains(HelperCls.Content));
            //Assert.True(tab.Find("." + HelperCls.Item).TextContent.Replace("\n", string.Empty).Trim() == "New York City comprises 5 boroughs sitting where the Hudson River meets the Atlantic Ocean. At its core is Manhattan, a densely populated borough that’s among the world’s major commercial, financial and cultural centers. Its iconic sites include skyscrapers such as the Empire State Building and sprawling Central Park. Broadway theater is staged in neon-lit Times Square.");
            tab.Find(".e-btn").Click();
            await Task.Delay(1000);
            Assert.True(true, tab.FindAll(".e-tab-wrap")[0].GetAttribute("aria-hidden"));
        }

        [Fact(Timeout = 10000, DisplayName = "EnableTab public method testing")]
        public async Task EnableTab()
        {
            var tab = RenderComponent<EnableTab>();
            await Task.Delay(100);
            var tabHtml = tab.Find("." + HelperCls.Tab);
            Assert.NotNull(tabHtml);
            Assert.Contains(HelperCls.Control, tabHtml.ClassName);
            Assert.Contains(HelperCls.Tab, tabHtml.ClassName);
            Assert.Contains(HelperCls.Library, tabHtml.ClassName);
            var style = tabHtml.GetAttribute("data-sf-style").Replace(" ", string.Empty);
            Assert.Contains("width:100%", style);
            Assert.Contains("height:auto", style);
            var tabHeaderHtml = tab.Find("." + HelperCls.Toolbar);
            Assert.NotNull(tabHeaderHtml);
            Assert.Contains(HelperCls.TabHeader, tabHeaderHtml.ClassName);
            Assert.Contains(HelperCls.HorizontalBottom, tabHeaderHtml.ClassName);
            Assert.Equal(3, tab.FindAll("." + HelperCls.ToolbarItem).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.TabWrap).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.TextWrap).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.TabText).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.CloseIcon).Count);
            Assert.Equal(1, tab.FindAll("." + HelperCls.Item).Count);
            Assert.NotNull(tab.Find("." + HelperCls.Content));
            Assert.Equal("New York", tab.Find("." + HelperCls.ToolbarItem).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Los Angeles", tab.FindAll("." + HelperCls.ToolbarItem)[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Chicago", tab.FindAll("." + HelperCls.ToolbarItem)[2].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[0].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[2].ClassName);
            Assert.True(tab.Find("." + HelperCls.Tab).LastElementChild.ClassList.Contains(HelperCls.Content));
            //Assert.True(tab.Find("." + HelperCls.Item).TextContent.Replace("\n", string.Empty).Trim() == "New York City comprises 5 boroughs sitting where the Hudson River meets the Atlantic Ocean. At its core is Manhattan, a densely populated borough that’s among the world’s major commercial, financial and cultural centers. Its iconic sites include skyscrapers such as the Empire State Building and sprawling Central Park. Broadway theater is staged in neon-lit Times Square.");
            Assert.True(tab.FindAll(".e-tab-wrap")[0].GetAttribute("aria-disabled") == "false");
            tab.FindAll(".e-btn")[1].Click();
            await Task.Delay(1000);
            Assert.True(tab.FindAll(".e-tab-wrap")[0].GetAttribute("aria-disabled") == "true");
            tab.FindAll(".e-btn")[0].Click();
            await Task.Delay(1000);
            Assert.True(tab.FindAll(".e-tab-wrap")[0].GetAttribute("aria-disabled") == "false");
            tab.FindAll(".e-btn")[2].Click();
            await Task.Delay(2000);
            Assert.True(true,tab.Find(".e-tab").GetAttribute("aria-disabled"));
        }

        [Fact(Timeout = 10000, DisplayName = "RemoveTab public method testing")]
        public async Task RemoveTab()
        {
            var tab = RenderComponent<RemoveTab>();
            await Task.Delay(100);
            var tabHtml = tab.Find("." + HelperCls.Tab);
            Assert.NotNull(tabHtml);
            Assert.Contains(HelperCls.Control, tabHtml.ClassName);
            Assert.Contains(HelperCls.Tab, tabHtml.ClassName);
            Assert.Contains(HelperCls.Library, tabHtml.ClassName);
            var style = tabHtml.GetAttribute("data-sf-style").Replace(" ", string.Empty);
            Assert.Contains("width:100%", style);
            Assert.Contains("height:auto", style);
            var tabHeaderHtml = tab.Find("." + HelperCls.Toolbar);
            Assert.NotNull(tabHeaderHtml);
            Assert.Contains(HelperCls.TabHeader, tabHeaderHtml.ClassName);
            Assert.Contains(HelperCls.HorizontalBottom, tabHeaderHtml.ClassName);
            Assert.Equal(3, tab.FindAll("." + HelperCls.ToolbarItem).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.TabWrap).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.TextWrap).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.TabText).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.CloseIcon).Count);
            Assert.Equal(1, tab.FindAll("." + HelperCls.Item).Count);
            Assert.NotNull(tab.Find("." + HelperCls.Content));
            Assert.Equal("New York", tab.Find("." + HelperCls.ToolbarItem).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Los Angeles", tab.FindAll("." + HelperCls.ToolbarItem)[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Chicago", tab.FindAll("." + HelperCls.ToolbarItem)[2].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[0].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[2].ClassName);
            Assert.True(tab.Find("." + HelperCls.Tab).LastElementChild.ClassList.Contains(HelperCls.Content));
            //Assert.True(tab.Find("." + HelperCls.Item).TextContent.Replace("\n", string.Empty).Trim() == "Los Angeles is a sprawling Southern California city and the center of the nation’s film and television industry. Near its iconic Hollywood sign, studios such as Paramount Pictures, Universal and Warner Brothers offer behind-the-scenes tours. On Hollywood Boulevard, TCL Chinese Theatre displays celebrities’ hand- and footprints, the Walk of Fame honors thousands of luminaries and vendors sell maps to stars’ homes.");
            tab.Find("button").Click();
            await Task.Delay(100);
            Assert.Equal(2, tab.FindAll("." + HelperCls.ToolbarItem).Count);
            Assert.Equal(2, tab.FindAll("." + HelperCls.ToolbarItem).Count);
            Assert.Equal(2, tab.FindAll("." + HelperCls.TabWrap).Count);
            Assert.Equal(2, tab.FindAll("." + HelperCls.TextWrap).Count);
            Assert.Equal(2, tab.FindAll("." + HelperCls.TabText).Count);
            Assert.Equal(2, tab.FindAll("." + HelperCls.CloseIcon).Count);
            Assert.Equal(1, tab.FindAll("." + HelperCls.Item).Count);
            Assert.Equal("New York", tab.Find("." + HelperCls.ToolbarItem).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Los Angeles", tab.FindAll("." + HelperCls.ToolbarItem)[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[0].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            Assert.True(tab.Find("." + HelperCls.Tab).LastElementChild.ClassList.Contains(HelperCls.Content));
            //Assert.True(tab.Find("." + HelperCls.Item).TextContent.Replace("\n", string.Empty).Trim() == "Los Angeles is a sprawling Southern California city and the center of the nation’s film and television industry. Near its iconic Hollywood sign, studios such as Paramount Pictures, Universal and Warner Brothers offer behind-the-scenes tours. On Hollywood Boulevard, TCL Chinese Theatre displays celebrities’ hand- and footprints, the Walk of Fame honors thousands of luminaries and vendors sell maps to stars’ homes.");
            tab.Instance.index = 0;
            tab.Render();
            tab.Find("button").Click();
            await Task.Delay(100);
            Assert.Equal(2, tab.FindAll("." + HelperCls.ToolbarItem).Count);
            tab.Instance.isDisable = false;
            tab.Render();
            tab.Find("button").Click();
            await Task.Delay(100);
            Assert.Equal(1, tab.FindAll("." + HelperCls.ToolbarItem).Count);
        }

        [Fact(Timeout = 10000, DisplayName = "Select tab public method testing")]
        public async Task SelectMethod()
        {
            var tab = RenderComponent<SelectMethod>();
            var tabHtml = tab.Find("." + HelperCls.Tab);
            Assert.NotNull(tabHtml);
            Assert.Contains(HelperCls.Control, tabHtml.ClassName);
            Assert.Contains(HelperCls.Tab, tabHtml.ClassName);
            Assert.Contains(HelperCls.Library, tabHtml.ClassName);
            var style = tabHtml.GetAttribute("data-sf-style").Replace(" ", string.Empty);
            Assert.Contains("width:100%", style);
            Assert.Contains("height:auto", style);
            var tabHeaderHtml = tab.FindAll("." + HelperCls.ToolbarItem);
            Assert.NotNull(tabHeaderHtml);
            Assert.Equal("New York", tab.Find("." + HelperCls.ToolbarItem).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Los Angeles", tab.FindAll("." + HelperCls.ToolbarItem)[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Chicago", tab.FindAll("." + HelperCls.ToolbarItem)[2].TextContent.Replace("\n", string.Empty).Trim());
            Assert.True(tab.Instance.isSelected == false);
            tab.Find("button").Click();
            await Task.Delay(100);
            Assert.True(tab.Instance.isSelected == true);
            TabItem tabItem1 = tab.Instance.Tab.GetTabItemByIndex(1);
            await Task.Delay(500);
            Assert.Contains("Los Angeles", tabItem1.Header.Text);
            TabItem tabItem2 = tab.Instance.Tab.GetTabItemById(tabHeaderHtml[2].Id);
            await Task.Delay(500);
            Assert.Contains("Chicago", tabItem2.Header.Text);
            TabItem tabItem3 = tab.Instance.Tab.GetTabItemByIndex(-1);
            await Task.Delay(500);
            Assert.Null(tabItem3);
            await tab.Instance.Tab.RefreshAsync();
            await Task.Delay(500);
        }
    }
}