using AngleSharp.Css.Dom;
using AngleSharp.Dom;
using Bunit;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Tests.Tabs.Samples.HeaderPositionRightWithDemandContent.TabItem;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Syncfusion.Blazor.Tests.Tabs
{
    public class HeaderPositionRightWithDemandContentTabItem : BunitTestContext
    {
        public Helper HelperCls = new();

        [Fact(Timeout = 10000, DisplayName = "Header string testing")]
        public async Task HeaderString()
        {
            var tab = RenderComponent<HeaderContentString>();
            await Task.Delay(100);
            var tabHtml = tab.Find("." + HelperCls.Tab);
            Assert.NotNull(tabHtml);
            Assert.Contains(HelperCls.VerticalTab, tabHtml.ClassName);
            Assert.Contains(HelperCls.VerticalRight, tabHtml.ClassName);
            var tabHeaderHtml = tab.Find("." + HelperCls.Toolbar);
            Assert.NotNull(tabHeaderHtml);
            Assert.Contains(HelperCls.Vertical, tabHeaderHtml.ClassName);
            Assert.Contains(HelperCls.VerticalRight, tabHeaderHtml.ClassName);
            Assert.True(tab.Find("." + HelperCls.Tab).LastElementChild.ClassList.Contains(HelperCls.Content));
            Assert.Equal("Twitter", tab.Find("." + HelperCls.ToolbarItem).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Facebook", tab.FindAll("." + HelperCls.ToolbarItem)[1].TextContent.Replace("\n", string.Empty).Trim());
        }

        [Fact(Timeout = 10000, DisplayName = "Content string testing")]
        public async Task ContentString()
        {
            var tab = RenderComponent<HeaderContentString>();
            await Task.Delay(100);
            var tabHtml = tab.Find("." + HelperCls.Tab);
            Assert.NotNull(tabHtml);
            Assert.Contains(HelperCls.VerticalTab, tabHtml.ClassName);
            Assert.Contains(HelperCls.VerticalRight, tabHtml.ClassName);
            var tabHeaderHtml = tab.Find("." + HelperCls.Toolbar);
            Assert.NotNull(tabHeaderHtml);
            Assert.Contains(HelperCls.Vertical, tabHeaderHtml.ClassName);
            Assert.Contains(HelperCls.VerticalRight, tabHeaderHtml.ClassName);
            Assert.True(tab.Find("." + HelperCls.Tab).LastElementChild.ClassList.Contains(HelperCls.Content));
            Assert.True(tab.Find("." + HelperCls.Item).TextContent.Replace("\n", string.Empty).Trim() == "Twitter is an online social networking service that enables users to send and read short 140-charactermessages called tweets.Registered users can read and post tweets, but those who are unregistered can only readthem.Users access Twitter through the website interface, SMS or mobile device app Twitter Inc. is based in SanFrancisco and has more than 25 offices around the world.Twitter was created in March 2006 by Jack Dorsey,Evan Williams, Biz Stone, and Noah Glass and launched in July 2006. The service rapidly gained worldwide popularity,with more than 100 million users posting 340 million tweets a day in 2012.The service also handled 1.6 billionsearch queries per day.");
        }

        [Fact(Timeout = 10000, DisplayName = "Header template testing")]
        public async Task HeaderTemplate()
        {
            var tab = RenderComponent<HeaderContentTemplate>();
            await Task.Delay(100);
            var tabHtml = tab.Find("." + HelperCls.Tab);
            Assert.NotNull(tabHtml);
            Assert.Contains(HelperCls.VerticalTab, tabHtml.ClassName);
            Assert.Contains(HelperCls.VerticalRight, tabHtml.ClassName);
            var tabHeaderHtml = tab.Find("." + HelperCls.Toolbar);
            Assert.NotNull(tabHeaderHtml);
            Assert.Contains(HelperCls.Vertical, tabHeaderHtml.ClassName);
            Assert.Contains(HelperCls.VerticalRight, tabHeaderHtml.ClassName);
            Assert.True(tab.Find("." + HelperCls.Tab).LastElementChild.ClassList.Contains(HelperCls.Content));
            Assert.Equal("Twitter", tab.Find("." + HelperCls.TabText).GetElementsByTagName("div").First().InnerHtml);
            Assert.Equal("Facebook", tab.FindAll("." + HelperCls.TabText)[1].GetElementsByTagName("div").First().InnerHtml);
        }

        [Fact(Timeout = 10000, DisplayName = "Content template testing")]
        public async Task ContentTemplate()
        {
            var tab = RenderComponent<HeaderContentTemplate>();
            await Task.Delay(100);
            var tabHtml = tab.Find("." + HelperCls.Tab);
            Assert.NotNull(tabHtml);
            Assert.Contains(HelperCls.VerticalTab, tabHtml.ClassName);
            Assert.Contains(HelperCls.VerticalRight, tabHtml.ClassName);
            var tabHeaderHtml = tab.Find("." + HelperCls.Toolbar);
            Assert.NotNull(tabHeaderHtml);
            Assert.Contains(HelperCls.Vertical, tabHeaderHtml.ClassName);
            Assert.Contains(HelperCls.VerticalRight, tabHeaderHtml.ClassName);
            Assert.True(tab.Find("." + HelperCls.Tab).LastElementChild.ClassList.Contains(HelperCls.Content));
            Assert.Equal("Twitter is an online social networking service that enables users to send and read short 140-charactermessages called tweets.Registered users can read and post tweets, but those who are unregistered can only readthem.Users access Twitter through the website interface, SMS or mobile device app Twitter Inc. is based in SanFrancisco and has more than 25 offices around the world.Twitter was created in March 2006 by Jack Dorsey,Evan Williams, Biz Stone, and Noah Glass and launched in July 2006. The service rapidly gained worldwide popularity,with more than 100 million users posting 340 million tweets a day in 2012.The service also handled 1.6 billionsearch queries per day.", tab.Find("." + HelperCls.Item).GetElementsByTagName("div").First().InnerHtml);
        }

        [Fact(Timeout = 10000, DisplayName = "CssClass property testing")]
        public async Task CssClass()
        {
            var tab = RenderComponent<CssClass>();
            await Task.Delay(100);
            var tabHtml = tab.Find("." + HelperCls.Tab);
            Assert.NotNull(tabHtml);
            Assert.Contains(HelperCls.VerticalTab, tabHtml.ClassName);
            Assert.Contains(HelperCls.VerticalRight, tabHtml.ClassName);
            var tabHeaderHtml = tab.Find("." + HelperCls.Toolbar);
            Assert.NotNull(tabHeaderHtml);
            Assert.Contains(HelperCls.Vertical, tabHeaderHtml.ClassName);
            Assert.Contains(HelperCls.VerticalRight, tabHeaderHtml.ClassName);
            var twitterHtml = tab.Find("." + HelperCls.ToolbarItem);
            Assert.NotNull(twitterHtml);
            Assert.Contains(HelperCls.Twitter, twitterHtml.ClassName);
            var facebookHtml = tab.FindAll("." + HelperCls.ToolbarItem)[1];
            Assert.NotNull(facebookHtml);
            Assert.Contains(HelperCls.Facebook, facebookHtml.ClassName);
            Assert.True(tab.Find("." + HelperCls.Tab).LastElementChild.ClassList.Contains(HelperCls.Content));
        }

        [Fact(Timeout = 10000, DisplayName = "Disabled property testing")]
        public async Task Disabled()
        {
            var tab = RenderComponent<Disabled>();
            await Task.Delay(100);
            var tabHtml = tab.Find("." + HelperCls.Tab);
            Assert.NotNull(tabHtml);
            Assert.Contains(HelperCls.VerticalTab, tabHtml.ClassName);
            Assert.Contains(HelperCls.VerticalRight, tabHtml.ClassName);
            var tabHeaderHtml = tab.Find("." + HelperCls.Toolbar);
            Assert.NotNull(tabHeaderHtml);
            Assert.Contains(HelperCls.Vertical, tabHeaderHtml.ClassName);
            Assert.Contains(HelperCls.VerticalRight, tabHeaderHtml.ClassName);
            Assert.DoesNotContain(HelperCls.Disabled, tab.FindAll("." + HelperCls.ToolbarItem)[0].ClassName);
            Assert.DoesNotContain(HelperCls.Overlay, tab.FindAll("." + HelperCls.ToolbarItem)[0].ClassName);
            Assert.DoesNotContain(HelperCls.Disabled, tab.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            Assert.DoesNotContain(HelperCls.Overlay, tab.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            Assert.Contains(HelperCls.Disabled, tab.FindAll("." + HelperCls.ToolbarItem)[2].ClassName);
            Assert.Contains(HelperCls.Overlay, tab.FindAll("." + HelperCls.ToolbarItem)[2].ClassName);
            Assert.True(tab.Find("." + HelperCls.Tab).LastElementChild.ClassList.Contains(HelperCls.Content));
        }

        [Fact(Timeout = 10000, DisplayName = "Visible property testing")]
        public async Task Visible()
        {
            var tab = RenderComponent<Visible>();
            await Task.Delay(100);
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
            //Assert.Equal(2, tab.FindAll("." + HelperCls.ToolbarItem).Count);
            //Assert.Equal(2, tab.FindAll("." + HelperCls.TabWrap).Count);
            //Assert.Equal(2, tab.FindAll("." + HelperCls.TextWrap).Count);
            //Assert.Equal(2, tab.FindAll("." + HelperCls.TabText).Count);
            //Assert.Equal(2, tab.FindAll("." + HelperCls.CloseIcon).Count);
            //Assert.Equal(1, tab.FindAll("." + HelperCls.Item).Count);
            //Assert.NotNull(tab.Find("." + HelperCls.Content));
            //Assert.Equal("Twitter", tab.Find("." + HelperCls.ToolbarItem).TextContent.Replace("\n", string.Empty).Trim());
            //Assert.Equal("Whatsapp", tab.FindAll("." + HelperCls.ToolbarItem)[1].TextContent.Replace("\n", string.Empty).Trim());
            //Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[0].ClassName);
            //Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            //Assert.True(tab.Find("." + HelperCls.Tab).LastElementChild.ClassList.Contains(HelperCls.Content));
            //Assert.True(tab.Find("." + HelperCls.Item).TextContent.Replace("\n", string.Empty).Trim() == "Twitter is an online social networking service that enables users to send and read short 140-charactermessages called tweets.Registered users can read and post tweets, but those who are unregistered can only readthem.Users access Twitter through the website interface, SMS or mobile device app Twitter Inc. is based in SanFrancisco and has more than 25 offices around the world.Twitter was created in March 2006 by Jack Dorsey,Evan Williams, Biz Stone, and Noah Glass and launched in July 2006. The service rapidly gained worldwide popularity,with more than 100 million users posting 340 million tweets a day in 2012.The service also handled 1.6 billionsearch queries per day.");
        }

        [Fact(Timeout = 10000, DisplayName = "Header IconCss property testing")]
        public async Task HeaderIconCss()
        {
            var tab = RenderComponent<IconCss>();
            await Task.Delay(100);
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
            Assert.Equal(1, tab.FindAll("." + HelperCls.Item).Count);
            Assert.NotNull(tab.Find("." + HelperCls.Content));
            Assert.Equal("Twitter", tab.Find("." + HelperCls.ToolbarItem).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Facebook", tab.FindAll("." + HelperCls.ToolbarItem)[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Whatsapp", tab.FindAll("." + HelperCls.ToolbarItem)[2].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[0].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[2].ClassName);
            Assert.Contains(HelperCls.ILeft, tab.FindAll("." + HelperCls.ToolbarItem)[0].ClassName);
            Assert.Contains(HelperCls.ILeft, tab.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            Assert.Contains(HelperCls.ILeft, tab.FindAll("." + HelperCls.ToolbarItem)[2].ClassName);
            Assert.Contains(HelperCls.IconLeft, tab.FindAll("." + HelperCls.TextWrap)[0].GetElementsByTagName("span").First().ClassName);
            Assert.Contains(HelperCls.TwitterIcon, tab.FindAll("." + HelperCls.TextWrap)[0].GetElementsByTagName("span").First().ClassName);
            Assert.Contains(HelperCls.IconLeft, tab.FindAll("." + HelperCls.TextWrap)[1].GetElementsByTagName("span").First().ClassName);
            Assert.Contains(HelperCls.FacebookIcon, tab.FindAll("." + HelperCls.TextWrap)[1].GetElementsByTagName("span").First().ClassName);
            Assert.Contains(HelperCls.IconLeft, tab.FindAll("." + HelperCls.TextWrap)[2].GetElementsByTagName("span").First().ClassName);
            Assert.Contains(HelperCls.WhatsappIcon, tab.FindAll("." + HelperCls.TextWrap)[2].GetElementsByTagName("span").First().ClassName);
            Assert.True(tab.Find("." + HelperCls.Tab).LastElementChild.ClassList.Contains(HelperCls.Content));
            Assert.True(tab.Find("." + HelperCls.Item).TextContent.Replace("\n", string.Empty).Trim() == "Twitter is an online social networking service that enables users to send and read short 140-charactermessages called tweets.Registered users can read and post tweets, but those who are unregistered can only readthem.Users access Twitter through the website interface, SMS or mobile device app Twitter Inc. is based in SanFrancisco and has more than 25 offices around the world.Twitter was created in March 2006 by Jack Dorsey,Evan Williams, Biz Stone, and Noah Glass and launched in July 2006. The service rapidly gained worldwide popularity,with more than 100 million users posting 340 million tweets a day in 2012.The service also handled 1.6 billionsearch queries per day.");
        }

        [Fact(Timeout = 10000, DisplayName = "Header Icon Right Position testing")]
        public async Task RightIconPosition()
        {
            var tab = RenderComponent<IconPositionRight>();
            await Task.Delay(100);
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
            Assert.Equal(1, tab.FindAll("." + HelperCls.Item).Count);
            Assert.NotNull(tab.Find("." + HelperCls.Content));
            Assert.Equal("Twitter", tab.Find("." + HelperCls.ToolbarItem).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Facebook", tab.FindAll("." + HelperCls.ToolbarItem)[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Whatsapp", tab.FindAll("." + HelperCls.ToolbarItem)[2].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[0].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[2].ClassName);
            Assert.Contains(HelperCls.IRight, tab.FindAll("." + HelperCls.ToolbarItem)[0].ClassName);
            Assert.Contains(HelperCls.IRight, tab.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            Assert.Contains(HelperCls.IRight, tab.FindAll("." + HelperCls.ToolbarItem)[2].ClassName);
            Assert.Contains(HelperCls.IconRight, tab.FindAll("." + HelperCls.TextWrap)[0].GetElementsByTagName("span").First().ClassName);
            Assert.Contains(HelperCls.TwitterIcon, tab.FindAll("." + HelperCls.TextWrap)[0].GetElementsByTagName("span").First().ClassName);
            Assert.Contains(HelperCls.IconRight, tab.FindAll("." + HelperCls.TextWrap)[1].GetElementsByTagName("span").First().ClassName);
            Assert.Contains(HelperCls.FacebookIcon, tab.FindAll("." + HelperCls.TextWrap)[1].GetElementsByTagName("span").First().ClassName);
            Assert.Contains(HelperCls.IconRight, tab.FindAll("." + HelperCls.TextWrap)[2].GetElementsByTagName("span").First().ClassName);
            Assert.Contains(HelperCls.WhatsappIcon, tab.FindAll("." + HelperCls.TextWrap)[2].GetElementsByTagName("span").First().ClassName);
            Assert.True(tab.Find("." + HelperCls.Tab).LastElementChild.ClassList.Contains(HelperCls.Content));
            Assert.True(tab.Find("." + HelperCls.Item).TextContent.Replace("\n", string.Empty).Trim() == "Twitter is an online social networking service that enables users to send and read short 140-charactermessages called tweets.Registered users can read and post tweets, but those who are unregistered can only readthem.Users access Twitter through the website interface, SMS or mobile device app Twitter Inc. is based in SanFrancisco and has more than 25 offices around the world.Twitter was created in March 2006 by Jack Dorsey,Evan Williams, Biz Stone, and Noah Glass and launched in July 2006. The service rapidly gained worldwide popularity,with more than 100 million users posting 340 million tweets a day in 2012.The service also handled 1.6 billionsearch queries per day.");
        }

        [Fact(Timeout = 10000, DisplayName = "Header Icon Top Position testing")]
        public async Task TopIconPosition()
        {
            var tab = RenderComponent<IconPositionTop>();
            await Task.Delay(100);
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
            Assert.Equal(1, tab.FindAll("." + HelperCls.Item).Count);
            Assert.NotNull(tab.Find("." + HelperCls.Content));
            Assert.Equal("Twitter", tab.Find("." + HelperCls.ToolbarItem).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Facebook", tab.FindAll("." + HelperCls.ToolbarItem)[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Whatsapp", tab.FindAll("." + HelperCls.ToolbarItem)[2].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[0].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[2].ClassName);
            Assert.Contains(HelperCls.ITop, tab.FindAll("." + HelperCls.ToolbarItem)[0].ClassName);
            Assert.Contains(HelperCls.ITop, tab.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            Assert.Contains(HelperCls.ITop, tab.FindAll("." + HelperCls.ToolbarItem)[2].ClassName);
            Assert.Contains(HelperCls.IconTop, tab.FindAll("." + HelperCls.TextWrap)[0].GetElementsByTagName("span").First().ClassName);
            Assert.Contains(HelperCls.TwitterIcon, tab.FindAll("." + HelperCls.TextWrap)[0].GetElementsByTagName("span").First().ClassName);
            Assert.Contains(HelperCls.IconTop, tab.FindAll("." + HelperCls.TextWrap)[1].GetElementsByTagName("span").First().ClassName);
            Assert.Contains(HelperCls.FacebookIcon, tab.FindAll("." + HelperCls.TextWrap)[1].GetElementsByTagName("span").First().ClassName);
            Assert.Contains(HelperCls.IconTop, tab.FindAll("." + HelperCls.TextWrap)[2].GetElementsByTagName("span").First().ClassName);
            Assert.Contains(HelperCls.WhatsappIcon, tab.FindAll("." + HelperCls.TextWrap)[2].GetElementsByTagName("span").First().ClassName);
            Assert.True(tab.Find("." + HelperCls.Tab).LastElementChild.ClassList.Contains(HelperCls.Content));
            Assert.True(tab.Find("." + HelperCls.Item).TextContent.Replace("\n", string.Empty).Trim() == "Twitter is an online social networking service that enables users to send and read short 140-charactermessages called tweets.Registered users can read and post tweets, but those who are unregistered can only readthem.Users access Twitter through the website interface, SMS or mobile device app Twitter Inc. is based in SanFrancisco and has more than 25 offices around the world.Twitter was created in March 2006 by Jack Dorsey,Evan Williams, Biz Stone, and Noah Glass and launched in July 2006. The service rapidly gained worldwide popularity,with more than 100 million users posting 340 million tweets a day in 2012.The service also handled 1.6 billionsearch queries per day.");
        }

        [Fact(Timeout = 10000, DisplayName = "Header Icon Bottom Position testing")]
        public async Task BottomIconPosition()
        {
            var tab = RenderComponent<IconPositionBottom>();
            await Task.Delay(100);
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
            Assert.Equal(1, tab.FindAll("." + HelperCls.Item).Count);
            Assert.NotNull(tab.Find("." + HelperCls.Content));
            Assert.Equal("Twitter", tab.Find("." + HelperCls.ToolbarItem).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Facebook", tab.FindAll("." + HelperCls.ToolbarItem)[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Whatsapp", tab.FindAll("." + HelperCls.ToolbarItem)[2].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[0].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[2].ClassName);
            Assert.Contains(HelperCls.IBottom, tab.FindAll("." + HelperCls.ToolbarItem)[0].ClassName);
            Assert.Contains(HelperCls.IBottom, tab.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            Assert.Contains(HelperCls.IBottom, tab.FindAll("." + HelperCls.ToolbarItem)[2].ClassName);
            Assert.Contains(HelperCls.IconBottom, tab.FindAll("." + HelperCls.TextWrap)[0].GetElementsByTagName("span").First().ClassName);
            Assert.Contains(HelperCls.TwitterIcon, tab.FindAll("." + HelperCls.TextWrap)[0].GetElementsByTagName("span").First().ClassName);
            Assert.Contains(HelperCls.IconBottom, tab.FindAll("." + HelperCls.TextWrap)[1].GetElementsByTagName("span").First().ClassName);
            Assert.Contains(HelperCls.FacebookIcon, tab.FindAll("." + HelperCls.TextWrap)[1].GetElementsByTagName("span").First().ClassName);
            Assert.Contains(HelperCls.IconBottom, tab.FindAll("." + HelperCls.TextWrap)[2].GetElementsByTagName("span").First().ClassName);
            Assert.Contains(HelperCls.WhatsappIcon, tab.FindAll("." + HelperCls.TextWrap)[2].GetElementsByTagName("span").First().ClassName);
            Assert.True(tab.Find("." + HelperCls.Tab).LastElementChild.ClassList.Contains(HelperCls.Content));
            Assert.True(tab.Find("." + HelperCls.Item).TextContent.Replace("\n", string.Empty).Trim() == "Twitter is an online social networking service that enables users to send and read short 140-charactermessages called tweets.Registered users can read and post tweets, but those who are unregistered can only readthem.Users access Twitter through the website interface, SMS or mobile device app Twitter Inc. is based in SanFrancisco and has more than 25 offices around the world.Twitter was created in March 2006 by Jack Dorsey,Evan Williams, Biz Stone, and Noah Glass and launched in July 2006. The service rapidly gained worldwide popularity,with more than 100 million users posting 340 million tweets a day in 2012.The service also handled 1.6 billionsearch queries per day.");
        }

        [Fact(Timeout = 10000, DisplayName = "Conditional rendering foreach loop testing")]

        public async Task ForeachLoop()
        {
            var tab = RenderComponent<ForLoop>();
            await Task.Delay(100);
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
            Assert.Equal(1, tab.FindAll("." + HelperCls.Item).Count);
            Assert.NotNull(tab.Find("." + HelperCls.Content));
            Assert.Equal("ASP.NET", tab.Find("." + HelperCls.TabText).GetElementsByTagName("div").First().InnerHtml);
            Assert.Equal("ASP.NET MVC", tab.FindAll("." + HelperCls.TabText)[1].GetElementsByTagName("div").First().InnerHtml);
            Assert.Equal("ASP.NET Razor", tab.FindAll("." + HelperCls.TabText)[2].GetElementsByTagName("div").First().InnerHtml);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[0].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[2].ClassName);
            Assert.Contains(HelperCls.ILeft, tab.FindAll("." + HelperCls.ToolbarItem)[0].ClassName);
            Assert.Contains(HelperCls.ILeft, tab.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            Assert.Contains(HelperCls.ILeft, tab.FindAll("." + HelperCls.ToolbarItem)[2].ClassName);
            Assert.True(tab.Find("." + HelperCls.Tab).LastElementChild.ClassList.Contains(HelperCls.Content));
            Assert.Equal("Microsoft ASP.NET is a set of technologies in the Microsoft .NET Framework for building Web applications and XML Web services. ASP.NET pages execute on the server and generate markup such as HTML, WML, or XML that is sent to a desktop or mobile browser. ASP.NET pages use a compiled,event-driven programming model that improves performance and enables the separation of application logic and user interface.", tab.Find("." + HelperCls.Item).GetElementsByTagName("div").First().InnerHtml);
            tab.Find("button").Click();
            await Task.Delay(100);
            Assert.Equal(4, tab.FindAll("." + HelperCls.ToolbarItem).Count);
            Assert.Equal(4, tab.FindAll("." + HelperCls.TabWrap).Count);
            Assert.Equal(4, tab.FindAll("." + HelperCls.TextWrap).Count);
            Assert.Equal(4, tab.FindAll("." + HelperCls.TabText).Count);
            Assert.Equal(4, tab.FindAll("." + HelperCls.CloseIcon).Count);
            Assert.Equal(1, tab.FindAll("." + HelperCls.Item).Count);
            Assert.Equal("ASP.NET", tab.Find("." + HelperCls.TabText).GetElementsByTagName("div").First().InnerHtml);
            Assert.Equal("ASP.NET MVC", tab.FindAll("." + HelperCls.TabText)[1].GetElementsByTagName("div").First().InnerHtml);
            Assert.Equal("ASP.NET Razor", tab.FindAll("." + HelperCls.TabText)[2].GetElementsByTagName("div").First().InnerHtml);
            Assert.Equal("JavaScript", tab.FindAll("." + HelperCls.TabText)[3].GetElementsByTagName("div").First().InnerHtml);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[0].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[2].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[3].ClassName);
            Assert.Contains(HelperCls.ILeft, tab.FindAll("." + HelperCls.ToolbarItem)[0].ClassName);
            Assert.Contains(HelperCls.ILeft, tab.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            Assert.Contains(HelperCls.ILeft, tab.FindAll("." + HelperCls.ToolbarItem)[2].ClassName);
            Assert.Equal("3", tab.FindAll("." + HelperCls.ToolbarItem)[3].GetAttribute("data-index"));
            Assert.True(tab.Find("." + HelperCls.Tab).LastElementChild.ClassList.Contains(HelperCls.Content));
            Assert.Equal("Microsoft ASP.NET is a set of technologies in the Microsoft .NET Framework for building Web applications and XML Web services. ASP.NET pages execute on the server and generate markup such as HTML, WML, or XML that is sent to a desktop or mobile browser. ASP.NET pages use a compiled,event-driven programming model that improves performance and enables the separation of application logic and user interface.", tab.Find("." + HelperCls.Item).GetElementsByTagName("div").First().InnerHtml);
            tab.FindAll("button")[1].Click();
            await Task.Delay(100);
            Assert.Equal(3, tab.FindAll("." + HelperCls.ToolbarItem).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.TabWrap).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.TextWrap).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.TabText).Count);
            Assert.Equal(3, tab.FindAll("." + HelperCls.CloseIcon).Count);
            Assert.Equal(1, tab.FindAll("." + HelperCls.Item).Count);
            Assert.Equal("ASP.NET MVC", tab.FindAll("." + HelperCls.TabText)[0].GetElementsByTagName("div").First().InnerHtml);
            Assert.Equal("ASP.NET Razor", tab.FindAll("." + HelperCls.TabText)[1].GetElementsByTagName("div").First().InnerHtml);
            Assert.Equal("JavaScript", tab.FindAll("." + HelperCls.TabText)[2].GetElementsByTagName("div").First().InnerHtml);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[0].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[2].ClassName);
            Assert.Contains(HelperCls.ILeft, tab.FindAll("." + HelperCls.ToolbarItem)[0].ClassName);
            Assert.Contains(HelperCls.ILeft, tab.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            Assert.Contains(HelperCls.ILeft, tab.FindAll("." + HelperCls.ToolbarItem)[2].ClassName);
            Assert.True(tab.Find("." + HelperCls.Tab).LastElementChild.ClassList.Contains(HelperCls.Content));
            Assert.Equal("The Model-View-Controller (MVC) architectural pattern separates an application into three main components: the model, the view, and the controller. The ASP.NET MVC framework provides an alternative to the ASP.NET Web Forms pattern for creating Web applications. The ASP.NET MVC framework is a lightweight, highly testable presentation framework that (as with Web Forms-based applications) is integrated with existing ASP.NET features, such as master pages and membership-based authentication.", tab.Find("." + HelperCls.Item).GetElementsByTagName("div").First().InnerHtml);
        }

        [Fact(Timeout = 10000, DisplayName = "Conditional rendering if statement testing")]
        public async Task IfStatement()
        {
            var tab = RenderComponent<IfStatement>();
            await Task.Delay(100);
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
            Assert.Equal(1, tab.FindAll("." + HelperCls.Item).Count);
            Assert.NotNull(tab.Find("." + HelperCls.Content));
            Assert.Equal("ASP.NET", tab.Find("." + HelperCls.TabText).GetElementsByTagName("div").First().InnerHtml);
            Assert.Equal("ASP.NET Razor", tab.FindAll("." + HelperCls.TabText)[1].GetElementsByTagName("div").First().InnerHtml);
            Assert.Equal("JavaScript", tab.FindAll("." + HelperCls.TabText)[2].GetElementsByTagName("div").First().InnerHtml);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[0].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            Assert.Contains(HelperCls.Template, tab.FindAll("." + HelperCls.ToolbarItem)[2].ClassName);
            Assert.Contains(HelperCls.ILeft, tab.FindAll("." + HelperCls.ToolbarItem)[0].ClassName);
            Assert.Contains(HelperCls.ILeft, tab.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            Assert.Contains(HelperCls.ILeft, tab.FindAll("." + HelperCls.ToolbarItem)[2].ClassName);
            Assert.True(tab.Find("." + HelperCls.Tab).LastElementChild.ClassList.Contains(HelperCls.Content));
            Assert.Equal("Microsoft ASP.NET is a set of technologies in the Microsoft .NET Framework for building Web applications and XML Web services. ASP.NET pages execute on the server and generate markup such as HTML, WML, or XML that is sent to a desktop or mobile browser. ASP.NET pages use a compiled,event-driven programming model that improves performance and enables the separation of application logic and user interface.", tab.Find("." + HelperCls.Item).GetElementsByTagName("div").First().InnerHtml);
        }
    }
}