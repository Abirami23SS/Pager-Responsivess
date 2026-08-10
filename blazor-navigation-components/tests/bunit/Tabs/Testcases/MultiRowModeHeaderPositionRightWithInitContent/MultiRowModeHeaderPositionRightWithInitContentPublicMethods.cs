using AngleSharp.Css.Dom;
using Bunit;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Tests.Tabs.Samples.MultiRowModeHeaderPositionRightWithInitContent.PublicMethods;
using System.Threading.Tasks;
using Xunit;

namespace Syncfusion.Blazor.Tests.Tabs
{
    public class MultiRowModeHeaderPositionRightWithInitContentPublicMethods : BunitTestContext
    {
        public Helper HelperCls = new();

        [Fact(Timeout = 10000, DisplayName = "AddTab public method testing")]
        public async Task AddTab()
        {
            var tab = RenderComponent<AddTab>();
            await Task.Delay(100);
            var toolbarItemsHtml = tab.Find("." + HelperCls.ToolbarItems);
            Assert.NotNull(toolbarItemsHtml);
            Assert.Contains(HelperCls.ToolbarMultiRow, toolbarItemsHtml.ClassName);
            var tabHtml = tab.Find("." + HelperCls.Tab);
            Assert.NotNull(tabHtml);
            Assert.Contains(HelperCls.Control, tabHtml.ClassName);
            Assert.Contains(HelperCls.Tab, tabHtml.ClassName);
            Assert.Contains(HelperCls.Library, tabHtml.ClassName);
            Assert.Contains(HelperCls.VerticalTab, tabHtml.ClassName);
            Assert.Contains(HelperCls.VerticalRight, tabHtml.ClassName);
            var style = tabHtml.GetAttribute("data-sf-style").Replace(" ", string.Empty);
            Assert.Contains("width:100%", style);
            Assert.Contains("height:auto", style);
            var tabHeaderHtml = tab.Find("." + HelperCls.Toolbar);
            Assert.NotNull(tabHeaderHtml);
            Assert.Contains(HelperCls.TabHeader, tabHeaderHtml.ClassName);
            Assert.Contains(HelperCls.Vertical, tabHeaderHtml.ClassName);
            Assert.Contains(HelperCls.VerticalRight, tabHeaderHtml.ClassName);
            Assert.Equal(3, tab.FindAll("." + HelperCls.ToolbarItem).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.TabWrap).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.TextWrap).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.TabText).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.CloseIcon).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.Item).Count);
            Assert.NotNull(tab.Find("." + HelperCls.Content));
            Assert.Equal("New York", tab.Find("." + HelperCls.ToolbarItem).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Los Angeles", tab.FindAll("." + HelperCls.ToolbarItem)[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Chicago", tab.FindAll("." + HelperCls.ToolbarItem)[2].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[0].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[2].ClassName);
            Assert.True(tab.Find("." + HelperCls.Tab).LastElementChild.ClassList.Contains(HelperCls.Content));
            Assert.True(tab.Find("." + HelperCls.Item).TextContent.Replace("\n", string.Empty).Trim()== "New York City comprises 5 boroughs sitting where the Hudson River meets the Atlantic Ocean. At its core is Manhattan, a densely populated borough that’s among the world’s major commercial, financial and cultural centers. Its iconic sites include skyscrapers such as the Empire State Building and sprawling Central Park. Broadway theater is staged in neon-lit Times Square.");
            Assert.True(tab.FindAll("." + HelperCls.Item)[1].TextContent.Replace("\n", string.Empty).Trim()== "Los Angeles is a sprawling Southern California city and the center of the nation’s film and television industry. Near its iconic Hollywood sign, studios such as Paramount Pictures, Universal and Warner Brothers offer behind-the-scenes tours. On Hollywood Boulevard, TCL Chinese Theatre displays celebrities’ hand- and footprints, the Walk of Fame honors thousands of luminaries and vendors sell maps to stars’ homes.");
            Assert.True(tab.FindAll("." + HelperCls.Item)[2].TextContent.Replace("\n", string.Empty).Trim()== "Chicago, on Lake Michigan in Illinois, is among the largest cities in the U.S. Famed for its bold architecture, it has a skyline punctuated by skyscrapers such as the iconic John Hancock Center, 1,451-ft. Willis Tower (formerly the Sears Tower) and the neo-Gothic Tribune Tower. The city is also renowned for its museums, including the Art Institute of Chicago with its noted Impressionist and Post-Impressionist works.");
            tab.Find("button").Click();
            await Task.Delay(100);
            Assert.Equal(4, tab.FindAll("." + HelperCls.ToolbarItem).Count);
            Assert.Equal(4, tab.FindAll("." + HelperCls.TabWrap).Count);
            Assert.Equal(4, tab.FindAll("." + HelperCls.TextWrap).Count);
            Assert.Equal(4, tab.FindAll("." + HelperCls.TabText).Count);
            Assert.Equal(4, tab.FindAll("." + HelperCls.CloseIcon).Count);
            Assert.Equal(4, tab.FindAll("." + HelperCls.Item).Count);
            Assert.Equal("Sydney", tab.Find("." + HelperCls.ToolbarItem).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("New York", tab.FindAll("." + HelperCls.ToolbarItem)[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Los Angeles", tab.FindAll("." + HelperCls.ToolbarItem)[2].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Chicago", tab.FindAll("." + HelperCls.ToolbarItem)[3].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[0].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[2].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[3].ClassName);
            Assert.True(tab.Find("." + HelperCls.Tab).LastElementChild.ClassList.Contains(HelperCls.Content));
            Assert.True(tab.Find("." + HelperCls.Item).TextContent.Replace("\n", string.Empty).Trim()== "Sydney, capital of New South Wales and one of Australia largest cities, is best known for its harbourfront Sydney Opera House, with a distinctive sail-like design. Massive Darling Harbour and the smaller Circular Quay port are hubs of waterside life, with the arched Harbour Bridge and esteemed Royal Botanic Garden nearby. Sydney Tower’s outdoor platform, the Skywalk, offers 360-degree views of the city and suburbs.");
            Assert.True(tab.FindAll("." + HelperCls.Item)[1].TextContent.Replace("\n", string.Empty).Trim()== "New York City comprises 5 boroughs sitting where the Hudson River meets the Atlantic Ocean. At its core is Manhattan, a densely populated borough that’s among the world’s major commercial, financial and cultural centers. Its iconic sites include skyscrapers such as the Empire State Building and sprawling Central Park. Broadway theater is staged in neon-lit Times Square.");
            Assert.True(tab.FindAll("." + HelperCls.Item)[2].TextContent.Replace("\n", string.Empty).Trim()== "Los Angeles is a sprawling Southern California city and the center of the nation’s film and television industry. Near its iconic Hollywood sign, studios such as Paramount Pictures, Universal and Warner Brothers offer behind-the-scenes tours. On Hollywood Boulevard, TCL Chinese Theatre displays celebrities’ hand- and footprints, the Walk of Fame honors thousands of luminaries and vendors sell maps to stars’ homes.");
            Assert.True(tab.FindAll("." + HelperCls.Item)[3].TextContent.Replace("\n", string.Empty).Trim()== "Chicago, on Lake Michigan in Illinois, is among the largest cities in the U.S. Famed for its bold architecture, it has a skyline punctuated by skyscrapers such as the iconic John Hancock Center, 1,451-ft. Willis Tower (formerly the Sears Tower) and the neo-Gothic Tribune Tower. The city is also renowned for its museums, including the Art Institute of Chicago with its noted Impressionist and Post-Impressionist works.");
        }

        [Fact(Timeout = 10000, DisplayName = "RemoveTab public method testing")]
        public async Task RemoveTab()
        {
            var tab = RenderComponent<RemoveTab>();
            await Task.Delay(100);
            var toolbarItemsHtml = tab.Find("." + HelperCls.ToolbarItems);
            Assert.NotNull(toolbarItemsHtml);
            Assert.Contains(HelperCls.ToolbarMultiRow, toolbarItemsHtml.ClassName);
            var tabHtml = tab.Find("." + HelperCls.Tab);
            Assert.NotNull(tabHtml);
            Assert.Contains(HelperCls.Control, tabHtml.ClassName);
            Assert.Contains(HelperCls.Tab, tabHtml.ClassName);
            Assert.Contains(HelperCls.Library, tabHtml.ClassName);
            Assert.Contains(HelperCls.VerticalTab, tabHtml.ClassName);
            Assert.Contains(HelperCls.VerticalRight, tabHtml.ClassName);
            var style = tabHtml.GetAttribute("data-sf-style").Replace(" ", string.Empty);
            Assert.Contains("width:100%", style);
            Assert.Contains("height:auto", style);
            var tabHeaderHtml = tab.Find("." + HelperCls.Toolbar);
            Assert.NotNull(tabHeaderHtml);
            Assert.Contains(HelperCls.TabHeader, tabHeaderHtml.ClassName);
            Assert.Contains(HelperCls.Vertical, tabHeaderHtml.ClassName);
            Assert.Contains(HelperCls.VerticalRight, tabHeaderHtml.ClassName);
            Assert.Equal(3, tab.FindAll("." + HelperCls.ToolbarItem).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.TabWrap).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.TextWrap).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.TabText).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.CloseIcon).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.Item).Count);
            Assert.NotNull(tab.Find("." + HelperCls.Content));
            Assert.Equal("New York", tab.Find("." + HelperCls.ToolbarItem).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Los Angeles", tab.FindAll("." + HelperCls.ToolbarItem)[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Chicago", tab.FindAll("." + HelperCls.ToolbarItem)[2].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[0].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[2].ClassName);
            Assert.True(tab.Find("." + HelperCls.Tab).LastElementChild.ClassList.Contains(HelperCls.Content));
            Assert.True(tab.Find("." + HelperCls.Item).TextContent.Replace("\n", string.Empty).Trim()== "New York City comprises 5 boroughs sitting where the Hudson River meets the Atlantic Ocean. At its core is Manhattan, a densely populated borough that’s among the world’s major commercial, financial and cultural centers. Its iconic sites include skyscrapers such as the Empire State Building and sprawling Central Park. Broadway theater is staged in neon-lit Times Square.");
            Assert.True(tab.FindAll("." + HelperCls.Item)[1].TextContent.Replace("\n", string.Empty).Trim()== "Los Angeles is a sprawling Southern California city and the center of the nation’s film and television industry. Near its iconic Hollywood sign, studios such as Paramount Pictures, Universal and Warner Brothers offer behind-the-scenes tours. On Hollywood Boulevard, TCL Chinese Theatre displays celebrities’ hand- and footprints, the Walk of Fame honors thousands of luminaries and vendors sell maps to stars’ homes.");
            Assert.True(tab.FindAll("." + HelperCls.Item)[2].TextContent.Replace("\n", string.Empty).Trim()== "Chicago, on Lake Michigan in Illinois, is among the largest cities in the U.S. Famed for its bold architecture, it has a skyline punctuated by skyscrapers such as the iconic John Hancock Center, 1,451-ft. Willis Tower (formerly the Sears Tower) and the neo-Gothic Tribune Tower. The city is also renowned for its museums, including the Art Institute of Chicago with its noted Impressionist and Post-Impressionist works.");
            tab.Find("button").Click();
            await Task.Delay(100);
            Assert.Equal(2, tab.FindAll("." + HelperCls.ToolbarItem).Count);
            Assert.Equal(2, tab.FindAll("." + HelperCls.TabWrap).Count);
            Assert.Equal(2, tab.FindAll("." + HelperCls.TextWrap).Count);
            Assert.Equal(2, tab.FindAll("." + HelperCls.TabText).Count);
            Assert.Equal(2, tab.FindAll("." + HelperCls.CloseIcon).Count);
            Assert.Equal(2, tab.FindAll("." + HelperCls.Item).Count);
            Assert.Equal("New York", tab.Find("." + HelperCls.ToolbarItem).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Los Angeles", tab.FindAll("." + HelperCls.ToolbarItem)[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[0].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            Assert.True(tab.Find("." + HelperCls.Tab).LastElementChild.ClassList.Contains(HelperCls.Content));
            Assert.True(tab.Find("." + HelperCls.Item).TextContent.Replace("\n", string.Empty).Trim()== "New York City comprises 5 boroughs sitting where the Hudson River meets the Atlantic Ocean. At its core is Manhattan, a densely populated borough that’s among the world’s major commercial, financial and cultural centers. Its iconic sites include skyscrapers such as the Empire State Building and sprawling Central Park. Broadway theater is staged in neon-lit Times Square.");
            Assert.True(tab.FindAll("." + HelperCls.Item)[1].TextContent.Replace("\n", string.Empty).Trim()== "Los Angeles is a sprawling Southern California city and the center of the nation’s film and television industry. Near its iconic Hollywood sign, studios such as Paramount Pictures, Universal and Warner Brothers offer behind-the-scenes tours. On Hollywood Boulevard, TCL Chinese Theatre displays celebrities’ hand- and footprints, the Walk of Fame honors thousands of luminaries and vendors sell maps to stars’ homes.");
        }
    }
}