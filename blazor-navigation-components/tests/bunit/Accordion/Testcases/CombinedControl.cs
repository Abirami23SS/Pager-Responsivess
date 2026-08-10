using Xunit;
using Bunit;
using AngleSharp.Dom;
using AngleSharp.Css.Dom;
using System.Threading.Tasks;
using Syncfusion.Blazor.Tests.Accordion.Samples;

namespace Syncfusion.Blazor.Tests.Accordion
{
    public class AccordionCombinedControls : BunitTestContext
    {
        public Helper HelperCls = new Helper();

        [Fact(Timeout = 10000)]
        public async Task GridInAccordion()
        {
            var cut = RenderComponent<CombinedControl>();
            await Task.Delay(100);
            var acrdn = cut.Find("." + HelperCls.Accordion);
            Assert.Equal(3, acrdn.QuerySelectorAll("." + HelperCls.AccordionItem).Length);
            Assert.True("Grid" == acrdn.QuerySelector("." + HelperCls.AccordionHeaderContent).TextContent.Replace("\n", string.Empty).Trim());
            Assert.True("Tab" == acrdn.QuerySelectorAll("." + HelperCls.AccordionHeaderContent)[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.True("Toolbar" == acrdn.QuerySelectorAll("." + HelperCls.AccordionHeaderContent)[2].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Null(acrdn.QuerySelector("." + HelperCls.AccordionPanel));
            //To check for Grid in Accordion
            cut.FindAll("." + HelperCls.AccordionHeader)[0].Click(); await Task.Delay(200);
            Assert.Equal(1, cut.FindAll("." + HelperCls.AccordionPanel).Count);
            Assert.Contains("e-grid", cut.Find("." + HelperCls.AccordionContent).FirstElementChild.ClassList);
            var grid = cut.Find(".e-grid");
            Assert.Equal(4, grid.QuerySelectorAll(".e-headercell").Length);
            Assert.Equal(75, grid.QuerySelectorAll(".e-gridcontent .e-row").Length);
            Assert.True("Order ID" == grid.QuerySelector(".e-headertext").TextContent.Replace("\n", string.Empty).Trim());
            Assert.True("Customer Name" == grid.QuerySelectorAll(".e-headertext")[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.True("Order Date" == grid.QuerySelectorAll(".e-headertext")[2].TextContent.Replace("\n", string.Empty).Trim());
            Assert.True("Freight" == grid.QuerySelectorAll(".e-headertext")[3].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal(300, grid.QuerySelectorAll(".e-rowcell").Length);
            Assert.Equal("1001", grid.QuerySelector(".e-rowcell").TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("$157.50", grid.QuerySelectorAll(".e-rowcell")[299].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("$2.10", grid.QuerySelectorAll(".e-rowcell")[3].TextContent.Replace("\n", string.Empty).Trim());
        }
        [Fact(Timeout = 10000)]
        public async Task TabInAccordion()
        {
            var cut = RenderComponent<CombinedControl>();
            await Task.Delay(100);
            //To check for Tab in Accordion
            cut.FindAll(".e-acrdn-header")[1].Click(); await Task.Delay(100);
            var tab = cut.Find(".e-tab");
            Assert.Contains("width:100%", tab.GetAttribute("data-sf-style"));
            Assert.Contains("height:auto", tab.GetAttribute("data-sf-style"));
            Assert.Equal(3, tab.QuerySelectorAll(".e-toolbar-item").Length);
            Assert.Equal(3, tab.QuerySelectorAll(".e-tab-wrap").Length);
            Assert.Equal(3, tab.QuerySelectorAll(".e-text-wrap").Length);
            Assert.Equal(3, tab.QuerySelectorAll(".e-tab-text").Length);
            Assert.Equal(3, tab.QuerySelectorAll(".e-close-icon").Length); await Task.Delay(50);
            Assert.Equal(1, tab.QuerySelectorAll(".e-item").Length);
            Assert.NotNull(tab.QuerySelector(".e-content"));
            Assert.Equal("Twitter", tab.QuerySelector(".e-toolbar-item").TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Facebook", tab.QuerySelectorAll(".e-toolbar-item")[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Whatsapp", tab.QuerySelectorAll(".e-toolbar-item")[2].TextContent.Replace("\n", string.Empty).Trim());
            Assert.True(tab.QuerySelector(".e-content").FirstElementChild.ClassList.Contains("e-active"));
            Assert.True(tab.QuerySelector(".e-item").TextContent.Replace("\n", string.Empty).Trim() == "Twitter is an online social networking service that enables users to send and read short 140-charactermessages called tweets.Registered users can read and post tweets, but those who are unregistered can only readthem.Users access Twitter through the website interface, SMS or mobile device app Twitter Inc. is based in SanFrancisco and has more than 25 offices around the world.Twitter was created in March 2006 by Jack Dorsey,Evan Williams, Biz Stone, and Noah Glass and launched in July 2006. The service rapidly gained worldwide popularity,with more than 100 million users posting 340 million tweets a day in 2012.The service also handled 1.6 billionsearch queries per day.");
        }
        [Fact(Timeout = 10000)]
        public async Task ToolbarInAccordion()
        {
            var cut = RenderComponent<CombinedControl>();
            await Task.Delay(100);
            //To check for Toolbar in Accordion
            cut.FindAll(".e-acrdn-header")[2].Click(); await Task.Delay(100);
            var toolbar = cut.FindAll(".e-toolbar")[0];
            Assert.Contains("width:500px", toolbar.GetAttribute("data-sf-style"));
            Assert.Contains("height:auto", toolbar.GetAttribute("data-sf-style"));
            Assert.Equal(8, toolbar.QuerySelectorAll(".e-toolbar-item").Length);
            Assert.Equal(7, toolbar.QuerySelectorAll(".e-tbar-btn").Length);
            Assert.Equal(7, toolbar.QuerySelectorAll(".e-tbtn-txt").Length);
            Assert.Equal(7, toolbar.QuerySelectorAll(".e-tbar-btn-text").Length);
            Assert.Equal("Cut", toolbar.QuerySelector(".e-toolbar-item").GetInnerText().Trim());
            Assert.Equal("Copy", toolbar.QuerySelectorAll(".e-toolbar-item")[1].GetInnerText().Trim());
            Assert.Equal("Paste", toolbar.QuerySelectorAll(".e-toolbar-item")[2].GetInnerText().Trim());
            Assert.Contains("e-separator", toolbar.QuerySelectorAll(".e-toolbar-item")[3].ClassList);
            Assert.Equal("Bold", toolbar.QuerySelectorAll(".e-toolbar-item")[4].GetInnerText().Trim());
            Assert.Equal("Underline", toolbar.QuerySelectorAll(".e-toolbar-item")[5].GetInnerText().Trim());
            Assert.Equal("Italic", toolbar.QuerySelectorAll(".e-toolbar-item")[6].GetInnerText().Trim());
            Assert.Equal("Color-Picker", toolbar.QuerySelectorAll(".e-toolbar-item")[7].GetInnerText().Trim());
        }
    }
}