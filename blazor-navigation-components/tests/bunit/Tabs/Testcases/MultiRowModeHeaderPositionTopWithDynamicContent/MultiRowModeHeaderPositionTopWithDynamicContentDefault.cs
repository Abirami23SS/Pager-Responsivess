using AngleSharp.Css.Dom;
using AngleSharp.Dom;
using Bunit;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Tests.Tabs.Samples.MultiRowModeHeaderPositionTopWithDynamicContent.Default;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Syncfusion.Blazor.Tests.Tabs
{
    public class MultiRowModeHeaderPositionTopWithDynamicContentDefault : BunitTestContext
    {
        public Helper HelperCls = new();

        [Fact(Timeout = 10000, DisplayName = "Properties default value testing- Tabs")]
        public async Task DefaultValueTabs()
        {
            var tab = RenderComponent<SfTab>(options => options.Add(content => content.OverflowMode, Syncfusion.Blazor.Navigations.OverflowMode.MultiRow));
            await Task.Delay(100);
            Assert.Equal(string.Empty, tab.Instance.CssClass);
            Assert.False(tab.Instance.EnablePersistence);
            Assert.False(tab.Instance.AllowDragAndDrop);
            Assert.Null(tab.Instance.DragArea);
            Assert.False(tab.Instance.EnableRtl);
            Assert.Equal(HeaderPosition.Top, tab.Instance.HeaderPlacement);
            Assert.Equal(Syncfusion.Blazor.Navigations.OverflowMode.MultiRow, tab.Instance.OverflowMode);
            Assert.Equal("auto", tab.Instance.Height);
            Assert.Equal(ContentLoad.Dynamic, tab.Instance.LoadOn);
            Assert.Equal(string.Empty, tab.Instance.Locale);
            Assert.Equal(0, tab.Instance.ScrollStep);
            Assert.Equal(-1, tab.Instance.SelectedItem);
            Assert.False(tab.Instance.ShowCloseButton);
            Assert.Equal("100%", tab.Instance.Width);
        }

        [Fact(Timeout = 10000, DisplayName = "Initial loading testing with header and content")]
        public async Task Default()
        {
            var tab = RenderComponent<Default>();
            await Task.Delay(100);
            var toolbarItemsHtml = tab.Find("." + HelperCls.ToolbarItems);
            Assert.NotNull(toolbarItemsHtml);
            Assert.Contains(HelperCls.ToolbarMultiRow, toolbarItemsHtml.ClassName);
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
            Assert.Equal(3, tab.FindAll("." + HelperCls.ToolbarItem).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.TabWrap).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.TextWrap).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.TabText).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.CloseIcon).Count);
            Assert.Equal(1, tab.FindAll("." + HelperCls.Item).Count);
            Assert.NotNull(tab.Find("." + HelperCls.Content));
            Assert.Equal("Twitter", tab.Find("." + HelperCls.ToolbarItem).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Facebook", tab.FindAll("." + HelperCls.ToolbarItem)[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Whatsapp", tab.FindAll("." + HelperCls.ToolbarItem)[2].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[0].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[2].ClassName);
            Assert.True(tab.Find("." + HelperCls.Tab).LastElementChild.ClassList.Contains(HelperCls.Content));
            Assert.True(tab.Find("." + HelperCls.Content).FirstElementChild.ClassList.Contains(HelperCls.Active));
            Assert.True(tab.Find("." + HelperCls.Item).TextContent.Replace("\n", string.Empty).Trim()== "Twitter is an online social networking service that enables users to send and read short 140-charactermessages called tweets.Registered users can read and post tweets, but those who are unregistered can only readthem.Users access Twitter through the website interface, SMS or mobile device app Twitter Inc. is based in SanFrancisco and has more than 25 offices around the world.Twitter was created in March 2006 by Jack Dorsey,Evan Williams, Biz Stone, and Noah Glass and launched in July 2006. The service rapidly gained worldwide popularity,with more than 100 million users posting 340 million tweets a day in 2012.The service also handled 1.6 billionsearch queries per day.");
        }

        [Fact(Timeout = 10000, DisplayName = "SelectedItem property two way binding testing")]
        public async Task SelectedItemTwoWayBinding()
        {
            var tab = RenderComponent<SelectedItem>();
            await Task.Delay(100);
            var toolbarItemsHtml = tab.Find("." + HelperCls.ToolbarItems);
            Assert.NotNull(toolbarItemsHtml);
            Assert.Contains(HelperCls.ToolbarMultiRow, toolbarItemsHtml.ClassName);
            tab.FindAll("br")[0].NextElementSibling.MarkupMatches("<span>SelectedItemValue: 1</span>");
            Assert.NotNull(tab.Find("." + HelperCls.Content));
            Assert.True(tab.Find("." + HelperCls.Tab).LastElementChild.ClassList.Contains(HelperCls.Content));
            Assert.True(tab.Find("." + HelperCls.Content).FirstElementChild.ClassList.Contains(HelperCls.Active));
            Assert.True(tab.Find("." + HelperCls.Item).TextContent.Replace("\n", string.Empty).Trim()== "Facebook is an online social networking service headquartered in Menlo Park, California. Its website waslaunched on February 4, 2004, by Mark Zuckerberg with his Harvard College roommates and fellow students EduardoSaverin, Andrew McCollum, Dustin Moskovitz and Chris Hughes.");
            tab.Instance.SelectedTab = 2;
            tab.Render();
            await Task.Delay(500);
            tab.FindAll("br")[0].NextElementSibling.MarkupMatches("<span>SelectedItemValue: 2</span>");
            Assert.NotNull(tab.Find("." + HelperCls.Content));
            Assert.True(tab.Find("." + HelperCls.Tab).LastElementChild.ClassList.Contains(HelperCls.Content));
            Assert.True(tab.Find("." + HelperCls.Content).FirstElementChild.ClassList.Contains(HelperCls.Active));
            Assert.True(tab.Find("." + HelperCls.Item).TextContent.Replace("\n", string.Empty).Trim()== "WhatsApp Messenger is a proprietary cross-platform instant messaging client for smartphones that operatesunder a subscription business model.It uses the Internet to send text messages, images, video, user location andaudio media messages to other users using standard cellular mobile numbers. As of February 2016, WhatsApp had a userbase of up to one billion,[10] making it the most globally popular messaging application.WhatsApp Inc., based inMountain View, California, was acquired by Facebook Inc.on February 19, 2014, for approximately US1. 9.3 billion.");
            tab.Instance.SelectedTab = 0;
            tab.Render();
            await Task.Delay(500);
            tab.FindAll("br")[0].NextElementSibling.MarkupMatches("<span>SelectedItemValue: 0</span>");
            Assert.NotNull(tab.Find("." + HelperCls.Content));
            Assert.True(tab.Find("." + HelperCls.Tab).LastElementChild.ClassList.Contains(HelperCls.Content));
            Assert.True(tab.Find("." + HelperCls.Content).FirstElementChild.ClassList.Contains(HelperCls.Active));
            Assert.True(tab.Find("." + HelperCls.Item).TextContent.Replace("\n", string.Empty).Trim()== "Twitter is an online social networking service that enables users to send and read short 140-charactermessages called tweets.Registered users can read and post tweets, but those who are unregistered can only readthem.Users access Twitter through the website interface, SMS or mobile device app Twitter Inc. is based in SanFrancisco and has more than 25 offices around the world.Twitter was created in March 2006 by Jack Dorsey,Evan Williams, Biz Stone, and Noah Glass and launched in July 2006. The service rapidly gained worldwide popularity,with more than 100 million users posting 340 million tweets a day in 2012.The service also handled 1.6 billionsearch queries per day.");
        }

        [Fact(Timeout = 10000, DisplayName = "CssClass property testing")]
        public async Task CssClass()
        {
            var tab = RenderComponent<CssClass>();
            await Task.Delay(100);
            var toolbarItemsHtml = tab.Find("." + HelperCls.ToolbarItems);
            Assert.NotNull(toolbarItemsHtml);
            Assert.Contains(HelperCls.ToolbarMultiRow, toolbarItemsHtml.ClassName);
            var tabHtml = tab.Find("." + HelperCls.Tab);
            Assert.NotNull(tabHtml);
            Assert.Contains(HelperCls.CustomClass, tabHtml.ClassName);
        }

        [Fact(Timeout = 10000, DisplayName = "Height property testing")]
        public async Task Height()
        {
            var tab = RenderComponent<SfTab>(options => options.Add(content => content.OverflowMode, Syncfusion.Blazor.Navigations.OverflowMode.MultiRow).Add(p => p.Height, "260px"));
            await Task.Delay(100);
            var tabEle = tab.Find("." + HelperCls.Tab);
            Assert.NotNull(tabEle);
            var tabHeight = tabEle.GetAttribute("data-sf-style");
            var expectedValue = "height:260px; width:100%;";
            expectedValue.MarkupMatches(tabHeight);
            Assert.Contains("height:260px", tabHeight.Replace(" ", string.Empty));
            tab.SetParametersAndRender((HelperCls.Height, "300px"));
            await Task.Delay(100);
            tabEle = tab.Find("." + HelperCls.Tab);
            tabHeight = tabEle.GetAttribute("data-sf-style");
            Assert.Contains("height:300px", tabHeight.Replace(" ", string.Empty));
        }

        [Fact(Timeout = 10000, DisplayName = "Width property testing")]
        public async Task Width()
        {
            var tab = RenderComponent<SfTab>(options => options.Add(content => content.OverflowMode, Syncfusion.Blazor.Navigations.OverflowMode.MultiRow).Add(p => p.Width, "600px"));
            await Task.Delay(100);
            var tabEle = tab.Find("." + HelperCls.Tab);
            Assert.NotNull(tabEle);
            var tabWidth = tabEle.GetAttribute("data-sf-style");
            var expectedValue = "height:auto; width:600px;";
            expectedValue.MarkupMatches(tabWidth);
            Assert.Contains("width:600px", tabWidth.Replace(" ", string.Empty));
            tab.SetParametersAndRender((HelperCls.Width, "700px"));
            await Task.Delay(100);
            tabEle = tab.Find("." + HelperCls.Tab);
            tabWidth = tabEle.GetAttribute("data-sf-style");
            Assert.Contains("width:700px", tabWidth.Replace(" ", string.Empty));
        }

        [Fact(Timeout = 10000, DisplayName = "ARIA attributes testing")]
        public async Task ARIAAttributes()
        {
            var tab = RenderComponent<Default>();
            await Task.Delay(100);
            var toolbarItemsHtml = tab.Find("." + HelperCls.ToolbarItems);
            Assert.NotNull(toolbarItemsHtml);
            Assert.Contains(HelperCls.ToolbarMultiRow, toolbarItemsHtml.ClassName);
            Assert.Equal("tab", tab.Find(".e-tab-wrap").GetAttribute("role"));
            Assert.Equal("toolbar", tab.Find("." + HelperCls.Toolbar).GetAttribute("role"));
            Assert.Equal("horizontal", tab.Find("." + HelperCls.Toolbar).GetAttribute("aria-orientation"));
            Assert.Equal("false", tab.FindAll("." + HelperCls.ToolbarItem)[0].QuerySelector(".e-tab-wrap").GetAttribute("aria-disabled"));
            Assert.Equal("false", tab.FindAll("." + HelperCls.ToolbarItem)[1].QuerySelector(".e-tab-wrap").GetAttribute("aria-disabled"));
            Assert.Equal("false", tab.FindAll("." + HelperCls.ToolbarItem)[2].QuerySelector(".e-tab-wrap").GetAttribute("aria-disabled"));
            Assert.Equal("tabpanel", tab.Find("." + HelperCls.Item).GetAttribute("role"));
        }

        [Fact(Timeout = 10000, DisplayName = "ShowCloseButton property testing")]
        public async Task ShowCloseButton()
        {
            var tab = RenderComponent<ShowCloseButton>();
            await Task.Delay(100);
            var toolbarItemsHtml = tab.Find("." + HelperCls.ToolbarItems);
            Assert.NotNull(toolbarItemsHtml);
            Assert.Contains(HelperCls.ToolbarMultiRow, toolbarItemsHtml.ClassName);
            var toolbarHtml = tab.Find("." + HelperCls.Toolbar);
            Assert.NotNull(toolbarHtml);
            Assert.True(tab.Find("." + HelperCls.Tab).LastElementChild.ClassList.Contains(HelperCls.Content));
            Assert.Contains(HelperCls.CloseShow, toolbarHtml.ClassName);
        }
    }
}