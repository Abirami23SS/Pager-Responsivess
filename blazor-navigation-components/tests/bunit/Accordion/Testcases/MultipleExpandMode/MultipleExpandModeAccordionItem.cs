using AngleSharp.Css.Dom;
using AngleSharp.Dom;
using Bunit;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Tests.Accordion.Samples.MultipleExpandMode.AccordionItem;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Syncfusion.Blazor.Tests.Accordion
{
    public class MultipleExpandModeAccordionItem : BunitTestContext
    {
        public Helper HelperCls = new();

        [Fact(Timeout = 10000, DisplayName = "Header string testing")]
        public void HeaderString()
        {
            var cut = RenderComponent<HeaderContentString>();
            Assert.Equal("ASP.NET", cut.Find("." + HelperCls.AccordionHeaderContent).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("ASP.NET MVC", cut.FindAll("." + HelperCls.AccordionHeaderContent)[1].TextContent.Replace("\n", string.Empty).Trim());
        }

        [Fact(Timeout = 10000, DisplayName = "Content string testing")]
        public void ContentString()
        {
            var cut = RenderComponent<HeaderContentString>();
            Assert.Equal("Microsoft ASP.NET is a set of technologies in the Microsoft .NET Framework for building Web applications and XML Web services. ASP.NET pages execute on the server and generate markup such as HTML, WML, or XML that is sent to a desktop or mobile browser. ASP.NET pages use a compiled,event-driven programming model that improves performance and enables the separation of application logic and user interface.", cut.Find("." + HelperCls.AccordionContent).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("The Model-View-Controller (MVC) architectural pattern separates an application into three main components: the model, the view, and the controller. The ASP.NET MVC framework provides an alternative to the ASP.NET Web Forms pattern for creating Web applications. The ASP.NET MVC framework is a lightweight, highly testable presentation framework that (as with Web Forms-based applications) is integrated with existing ASP.NET features, such as master pages and membership-based authentication.", cut.FindAll("." + HelperCls.AccordionContent)[1].TextContent.Replace("\n", string.Empty).Trim());
        }

        [Fact(Timeout = 10000, DisplayName = "Header template testing")]
        public void HeaderTemplate()
        {
            var cut = RenderComponent<HeaderContentTemplate>();
            Assert.Equal("Blazor Header template 1", cut.Find("." + HelperCls.AccordionHeaderContent).GetElementsByTagName("div").First().InnerHtml);
            Assert.Equal("Blazor Header template 2", cut.FindAll("." + HelperCls.AccordionHeaderContent)[1].GetElementsByTagName("div").First().InnerHtml);
        }

        [Fact(Timeout = 10000, DisplayName = "Content template testing")]
        public void ContentTemplate()
        {
            var cut = RenderComponent<HeaderContentTemplate>();
            Assert.Equal("Blazor Content template 1", cut.Find("." + HelperCls.AccordionContent).GetElementsByTagName("div").First().InnerHtml);
            Assert.Equal("Blazor Content template 2", cut.FindAll("." + HelperCls.AccordionContent)[1].GetElementsByTagName("div").First().InnerHtml);
        }

        [Fact(Timeout = 10000, DisplayName = "Expanded property testing")]
        public void Expanded()
        {
            var cut = RenderComponent<HeaderContentString>();
            Assert.Equal(2, cut.FindAll("." + HelperCls.AccordionItem).Count);
            Assert.Equal(2, cut.FindAll("." + HelperCls.AccordionHeader).Count);
            Assert.Equal(2, cut.FindAll("." + HelperCls.AccordionHeaderContent).Count);
            Assert.Equal(2, cut.FindAll("." + HelperCls.HeaderToggleIcon).Count);
            Assert.Equal(2, cut.FindAll("." + HelperCls.HeaderToggleCollapseIcon).Count);
            Assert.Equal(2, cut.FindAll("." + HelperCls.HeaderToggleExpandIcon).Count);
            Assert.Equal(2, cut.FindAll("." + HelperCls.AccordionPanel).Count);
            Assert.Equal(2, cut.FindAll("." + HelperCls.AccordionContent).Count);
            Assert.Equal("ASP.NET", cut.Find("." + HelperCls.AccordionHeaderContent).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("ASP.NET MVC", cut.FindAll("." + HelperCls.AccordionHeaderContent)[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Microsoft ASP.NET is a set of technologies in the Microsoft .NET Framework for building Web applications and XML Web services. ASP.NET pages execute on the server and generate markup such as HTML, WML, or XML that is sent to a desktop or mobile browser. ASP.NET pages use a compiled,event-driven programming model that improves performance and enables the separation of application logic and user interface.", cut.Find("." + HelperCls.AccordionContent).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("The Model-View-Controller (MVC) architectural pattern separates an application into three main components: the model, the view, and the controller. The ASP.NET MVC framework provides an alternative to the ASP.NET Web Forms pattern for creating Web applications. The ASP.NET MVC framework is a lightweight, highly testable presentation framework that (as with Web Forms-based applications) is integrated with existing ASP.NET features, such as master pages and membership-based authentication.", cut.FindAll("." + HelperCls.AccordionContent)[1].TextContent.Replace("\n", string.Empty).Trim());
            var firstItemHtml = cut.Find("." + HelperCls.AccordionItem);
            Assert.Contains(HelperCls.Selected, firstItemHtml.ClassName);
            Assert.Contains(HelperCls.Active, firstItemHtml.ClassName);
            var secondItemHtml = cut.FindAll("." + HelperCls.AccordionItem)[1];
            Assert.Contains(HelperCls.Selected, secondItemHtml.ClassName);
            Assert.Contains(HelperCls.Active, secondItemHtml.ClassName);
            //Assert.Contains(HelperCls.ExpandState, secondItemHtml.ClassName);
            Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[0].GetAttribute("aria-expanded"));
            //Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[0].GetAttribute("aria-selected"));
            Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[1].GetAttribute("aria-expanded"));
            //Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[1].GetAttribute("aria-selected"));
        }

        [Fact(Timeout = 10000, DisplayName = "Expanded property two way binding testing")]
        public async Task ExpandedTwoWayBinding()
        {
            var cut = RenderComponent<HeaderContentString>();
            cut.FindAll("br")[0].NextElementSibling.MarkupMatches("<span>ExpandFirstItemValue: True</span>");
            cut.FindAll("br")[1].NextElementSibling.MarkupMatches("<span>ExpandSecondItemValue: True</span>");
            var firstItemHtml = cut.Find("." + HelperCls.AccordionItem);
            Assert.Contains(HelperCls.Selected, firstItemHtml.ClassName);
            Assert.Contains(HelperCls.Active, firstItemHtml.ClassName);
            var secondItemHtml = cut.FindAll("." + HelperCls.AccordionItem)[1];
            //Assert.Contains(HelperCls.ExpandState, secondItemHtml.ClassName);
            Assert.Contains(HelperCls.Selected, secondItemHtml.ClassName);
            Assert.Contains(HelperCls.Active, secondItemHtml.ClassName);
            Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[0].GetAttribute("aria-expanded"));
            //Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[0].GetAttribute("aria-selected"));
            Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[1].GetAttribute("aria-expanded"));
            //Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[1].GetAttribute("aria-selected"));
            cut.Instance.ExpandFirstItem = false;
            cut.Instance.ExpandSecondItem = false;
            cut.Render();
            await Task.Delay(500);
            cut.FindAll("br")[0].NextElementSibling.MarkupMatches("<span>ExpandFirstItemValue: False</span>");
            cut.FindAll("br")[1].NextElementSibling.MarkupMatches("<span>ExpandSecondItemValue: False</span>");
            Assert.Equal("false", cut.FindAll("." + HelperCls.AccordionHeader)[0].GetAttribute("aria-expanded"));
            //Assert.Equal("false", cut.FindAll("." + HelperCls.AccordionHeader)[0].GetAttribute("aria-selected"));
            Assert.Equal("false", cut.FindAll("." + HelperCls.AccordionHeader)[1].GetAttribute("aria-expanded"));
            //Assert.Equal("false", cut.FindAll("." + HelperCls.AccordionHeader)[1].GetAttribute("aria-selected"));
        }

        [Fact(Timeout = 10000, DisplayName = "Id property testing")]
        public void ItemId()
        {
            var cut = RenderComponent<ItemId>();
            Assert.Equal("ASPNET", cut.Find("." + HelperCls.AccordionItem).GetAttribute("Id"));
            Assert.Equal("ASPNETMVC", cut.FindAll("." + HelperCls.AccordionItem)[1].GetAttribute("Id"));
        }

        [Fact(Timeout = 10000, DisplayName = "IconCss property testing")]
        public void IconCss()
        {
            var cut = RenderComponent<IconCss>();
            Assert.Equal(4, cut.FindAll("." + HelperCls.AccordionHeaderIcon).Count);
            var athleticsIconHtml = cut.FindAll("." + HelperCls.AccordionHeaderIcon)[0].FirstElementChild;
            Assert.NotNull(athleticsIconHtml);
            Assert.Contains(HelperCls.Athletics, athleticsIconHtml.ClassName);
            Assert.Contains(HelperCls.AccordionIcons, athleticsIconHtml.ClassName);
            var waterIconHtml = cut.FindAll("." + HelperCls.AccordionHeaderIcon)[1].FirstElementChild;
            Assert.NotNull(waterIconHtml);
            Assert.Contains(HelperCls.WaterGames, waterIconHtml.ClassName);
            Assert.Contains(HelperCls.AccordionIcons, waterIconHtml.ClassName);
            var racingIconHtml = cut.FindAll("." + HelperCls.AccordionHeaderIcon)[2].FirstElementChild;
            Assert.NotNull(racingIconHtml);
            Assert.Contains(HelperCls.RacingGames, racingIconHtml.ClassName);
            Assert.Contains(HelperCls.AccordionIcons, racingIconHtml.ClassName);
            var indoorIconHtml = cut.FindAll("." + HelperCls.AccordionHeaderIcon)[3].FirstElementChild;
            Assert.NotNull(indoorIconHtml);
            Assert.Contains(HelperCls.IndoorGames, indoorIconHtml.ClassName);
            Assert.Contains(HelperCls.AccordionIcons, indoorIconHtml.ClassName);
        }

        [Fact(Timeout = 10000, DisplayName = "CssClass property testing")]
        public void CssClass()
        {
            var cut = RenderComponent<CssClass>();
            var itemHtml = cut.Find("." + HelperCls.AccordionItem);
            Assert.NotNull(itemHtml);
            Assert.Contains(HelperCls.ASP, itemHtml.ClassName);
            var secondItemHtml = cut.FindAll("." + HelperCls.AccordionItem)[1];
            Assert.NotNull(secondItemHtml);
            Assert.Contains(HelperCls.MVC, secondItemHtml.ClassName);
        }

        [Fact(Timeout = 10000, DisplayName = "Disabled property testing")]
        public void Disabled()
        {
            var cut = RenderComponent<Disabled>();
            var itemHtml = cut.Find("." + HelperCls.AccordionItem);
            Assert.NotNull(itemHtml);
            Assert.Contains(HelperCls.Overlay, itemHtml.ClassName);
            Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[0].GetAttribute("aria-disabled"));
            Assert.Equal("false", cut.FindAll("." + HelperCls.AccordionHeader)[1].GetAttribute("aria-disabled"));
            Assert.Equal("false", cut.FindAll("." + HelperCls.AccordionHeader)[2].GetAttribute("aria-disabled"));
        }

        [Fact(Timeout = 10000, DisplayName = "Visible property testing")]
        public void Visible()
        {
            var cut = RenderComponent<Visible>();
            Assert.Equal(4, cut.FindAll("." + HelperCls.AccordionItem).Count);
            Assert.Equal(4, cut.FindAll("." + HelperCls.AccordionHeader).Count);
            Assert.Equal(4, cut.FindAll("." + HelperCls.AccordionHeaderContent).Count);
            Assert.Equal(4, cut.FindAll("." + HelperCls.HeaderToggleIcon).Count);
            Assert.Equal(4, cut.FindAll("." + HelperCls.HeaderToggleCollapseIcon).Count);
            Assert.Equal(1, cut.FindAll("." + HelperCls.HeaderToggleExpandIcon).Count);
            Assert.Equal(1, cut.FindAll("." + HelperCls.AccordionPanel).Count);
            Assert.Equal(1, cut.FindAll("." + HelperCls.AccordionContent).Count);
            var itemHtml = cut.FindAll("." + HelperCls.AccordionItem)[1];
            Assert.NotNull(itemHtml);
            Assert.Contains(HelperCls.Hide, itemHtml.ClassName);
        }

        [Fact(Timeout = 10000, DisplayName = "Conditional rendering foreach loop testing")]
        public void ForeachLoop()
        {
            var cut = RenderComponent<ForLoop>();
            Assert.Equal(3, cut.FindAll("." + HelperCls.AccordionItem).Count);
            Assert.Equal(3, cut.FindAll("." + HelperCls.AccordionHeader).Count);
            Assert.Equal(3, cut.FindAll("." + HelperCls.AccordionHeaderContent).Count);
            Assert.Equal(3, cut.FindAll("." + HelperCls.HeaderToggleIcon).Count);
            Assert.Equal(3, cut.FindAll("." + HelperCls.HeaderToggleCollapseIcon).Count);
            Assert.Equal(3, cut.FindAll("." + HelperCls.HeaderToggleExpandIcon).Count);
            Assert.Equal(3, cut.FindAll("." + HelperCls.AccordionPanel).Count);
            Assert.Equal(3, cut.FindAll("." + HelperCls.AccordionContent).Count);
            Assert.Equal("ASP.NET", cut.FindAll("." + HelperCls.AccordionHeaderContent)[0].GetElementsByTagName("div").First().InnerHtml);
            Assert.Equal("ASP.NET MVC", cut.FindAll("." + HelperCls.AccordionHeaderContent)[1].GetElementsByTagName("div").First().InnerHtml);
            Assert.Equal("ASP.NET Razor", cut.FindAll("." + HelperCls.AccordionHeaderContent)[2].GetElementsByTagName("div").First().InnerHtml);
            Assert.Equal("Microsoft ASP.NET is a set of technologies in the Microsoft .NET Framework for building Web applications and XML Web services. ASP.NET pages execute on the server and generate markup such as HTML, WML, or XML that is sent to a desktop or mobile browser. ASP.NET pages use a compiled,event-driven programming model that improves performance and enables the separation of application logic and user interface.", cut.Find("." + HelperCls.AccordionContent).GetElementsByTagName("div").First().InnerHtml);
            Assert.Equal("The Model-View-Controller (MVC) architectural pattern separates an application into three main components: the model, the view, and the controller. The ASP.NET MVC framework provides an alternative to the ASP.NET Web Forms pattern for creating Web applications. The ASP.NET MVC framework is a lightweight, highly testable presentation framework that (as with Web Forms-based applications) is integrated with existing ASP.NET features, such as master pages and membership-based authentication.", cut.FindAll("." + HelperCls.AccordionContent)[1].GetElementsByTagName("div").First().InnerHtml);
            Assert.Equal("Razor is an ASP.NET programming syntax used to create dynamic web pages with the C# or Visual Basic .NET programming languages. Razor was in development in June 2010 and was released for Microsoft Visual Studio 2010 in January 2011. Razor is a simple-syntax view engine and was released as part of MVC 3 and the WebMatrix tool set. Side Code content", cut.FindAll("." + HelperCls.AccordionContent)[2].GetElementsByTagName("div").First().InnerHtml);
            var firstItemHtml = cut.Find("." + HelperCls.AccordionItem);
            Assert.Contains(HelperCls.Selected, firstItemHtml.ClassName);
            Assert.Contains(HelperCls.Active, firstItemHtml.ClassName);
            var secondItemHtml = cut.FindAll("." + HelperCls.AccordionItem)[1];
            Assert.Contains(HelperCls.Selected, secondItemHtml.ClassName);
            Assert.Contains(HelperCls.Active, secondItemHtml.ClassName);
            var thirdItemHtml = cut.FindAll("." + HelperCls.AccordionItem)[2];
           // Assert.Contains(HelperCls.ExpandState, thirdItemHtml.ClassName);
            Assert.Contains(HelperCls.Selected, thirdItemHtml.ClassName);
            Assert.Contains(HelperCls.Active, thirdItemHtml.ClassName);
            Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[0].GetAttribute("aria-expanded"));
            Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[1].GetAttribute("aria-expanded"));
            Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[2].GetAttribute("aria-expanded"));
            //Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[0].GetAttribute("aria-selected"));
            //Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[1].GetAttribute("aria-selected"));
            //Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[2].GetAttribute("aria-selected"));
        }

        [Fact(Timeout = 10000, DisplayName = "Conditional rendering if statement testing")]
        public void IfStatement()
        {
            var cut = RenderComponent<IfStatement>();
            Assert.Equal(3, cut.FindAll("." + HelperCls.AccordionItem).Count);
            Assert.Equal(3, cut.FindAll("." + HelperCls.AccordionHeader).Count);
            Assert.Equal(3, cut.FindAll("." + HelperCls.AccordionHeaderContent).Count);
            Assert.Equal(3, cut.FindAll("." + HelperCls.HeaderToggleIcon).Count);
            Assert.Equal(3, cut.FindAll("." + HelperCls.HeaderToggleCollapseIcon).Count);
            Assert.Equal(1, cut.FindAll("." + HelperCls.HeaderToggleExpandIcon).Count);
            Assert.Equal(1, cut.FindAll("." + HelperCls.AccordionPanel).Count);
            Assert.Equal(1, cut.FindAll("." + HelperCls.AccordionContent).Count);
            Assert.Equal("ASP.NET", cut.FindAll("." + HelperCls.AccordionHeaderContent)[0].GetElementsByTagName("div").First().InnerHtml);
            Assert.Equal("ASP.NET Razor", cut.FindAll("." + HelperCls.AccordionHeaderContent)[1].GetElementsByTagName("div").First().InnerHtml);
            Assert.Equal("JavaScript", cut.FindAll("." + HelperCls.AccordionHeaderContent)[2].GetElementsByTagName("div").First().InnerHtml);
            Assert.Equal("Microsoft ASP.NET is a set of technologies in the Microsoft .NET Framework for building Web applications and XML Web services. ASP.NET pages execute on the server and generate markup such as HTML, WML, or XML that is sent to a desktop or mobile browser. ASP.NET pages use a compiled,event-driven programming model that improves performance and enables the separation of application logic and user interface.", cut.Find("." + HelperCls.AccordionContent).GetElementsByTagName("div").First().InnerHtml);
            var expandItemHtml = cut.Find("." + HelperCls.AccordionItem);
            Assert.Contains(HelperCls.ExpandState, expandItemHtml.ClassName);
            Assert.Contains(HelperCls.Selected, expandItemHtml.ClassName);
            Assert.Contains(HelperCls.Active, expandItemHtml.ClassName);
            Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[0].GetAttribute("aria-expanded"));
            Assert.Equal("false", cut.FindAll("." + HelperCls.AccordionHeader)[1].GetAttribute("aria-expanded"));
            Assert.Equal("false", cut.FindAll("." + HelperCls.AccordionHeader)[2].GetAttribute("aria-expanded"));
            //Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[0].GetAttribute("aria-selected"));
            //Assert.Equal("false", cut.FindAll("." + HelperCls.AccordionHeader)[1].GetAttribute("aria-selected"));
            //Assert.Equal("false", cut.FindAll("." + HelperCls.AccordionHeader)[2].GetAttribute("aria-selected"));
        }
    }
}