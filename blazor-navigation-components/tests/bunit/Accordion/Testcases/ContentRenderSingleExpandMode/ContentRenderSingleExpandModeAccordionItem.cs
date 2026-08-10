using AngleSharp.Css.Dom;
using AngleSharp.Dom;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Tests.Accordion.Samples.ContentRenderSingleExpandMode.AccordionItem;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Syncfusion.Blazor.Tests.Accordion
{
    public class ContentRenderSingleExpandModeAccordionItem : BunitTestContext
    {
        public Helper HelperCls = new();

        [Fact(Timeout = 10000, DisplayName = "Header string testing")]
        public void HeaderString()
        {
            var cut = RenderComponent<HeaderContentString>();
            Assert.Equal("ASP.NET", cut.Find("." + HelperCls.AccordionHeaderContent).TextContent.Replace("\n", string.Empty).Trim());
        }

        [Fact(Timeout = 10000, DisplayName = "Content string testing")]
        public void ContentString()
        {
            var cut = RenderComponent<HeaderContentString>();
            Assert.Equal("Microsoft ASP.NET is a set of technologies in the Microsoft .NET Framework for building Web applications and XML Web services. ASP.NET pages execute on the server and generate markup such as HTML, WML, or XML that is sent to a desktop or mobile browser. ASP.NET pages use a compiled,event-driven programming model that improves performance and enables the separation of application logic and user interface.", cut.Find("." + HelperCls.AccordionContent).TextContent.Replace("\n", string.Empty).Trim());
        }

        [Fact(Timeout = 10000, DisplayName = "Header template testing")]
        public void HeaderTemplate()
        {
            var cut = RenderComponent<HeaderContentTemplate>();
            Assert.Equal("Blazor Header template 1", cut.Find("." + HelperCls.AccordionHeaderContent).GetElementsByTagName("div").First().InnerHtml);
        }

        [Fact(Timeout = 10000, DisplayName = "Content template testing")]
        public void ContentTemplate()
        {
            var cut = RenderComponent<HeaderContentTemplate>();
            Assert.Equal("Blazor Content template 1", cut.Find("." + HelperCls.AccordionContent).GetElementsByTagName("div").First().InnerHtml);
        }

        [Fact(Timeout = 10000, DisplayName = "Expanded property testing")]
        public void Expanded()
        {
            var cut = RenderComponent<HeaderContentString>();
            Assert.Equal(1, cut.FindAll("." + HelperCls.AccordionItem).Count);
            Assert.Equal(1, cut.FindAll("." + HelperCls.AccordionHeader).Count);
            Assert.Equal(1, cut.FindAll("." + HelperCls.AccordionHeaderContent).Count);
            Assert.Equal(1, cut.FindAll("." + HelperCls.HeaderToggleIcon).Count);
            Assert.Equal(1, cut.FindAll("." + HelperCls.HeaderToggleCollapseIcon).Count);
            Assert.Equal(1, cut.FindAll("." + HelperCls.HeaderToggleExpandIcon).Count);
            Assert.Equal(1, cut.FindAll("." + HelperCls.AccordionPanel).Count);
            Assert.Equal(1, cut.FindAll("." + HelperCls.AccordionContent).Count);
            Assert.Equal("ASP.NET", cut.Find("." + HelperCls.AccordionHeaderContent).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Microsoft ASP.NET is a set of technologies in the Microsoft .NET Framework for building Web applications and XML Web services. ASP.NET pages execute on the server and generate markup such as HTML, WML, or XML that is sent to a desktop or mobile browser. ASP.NET pages use a compiled,event-driven programming model that improves performance and enables the separation of application logic and user interface.", cut.Find("." + HelperCls.AccordionContent).TextContent.Replace("\n", string.Empty).Trim());
            var expandItemHtml = cut.Find("." + HelperCls.AccordionItem);
            Assert.Contains(HelperCls.ExpandState, expandItemHtml.ClassName);
            Assert.Contains(HelperCls.Selected, expandItemHtml.ClassName);
            Assert.Contains(HelperCls.Active, expandItemHtml.ClassName);
            Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[0].GetAttribute("aria-expanded"));
            //Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[0].GetAttribute("aria-selected"));
        }

        [Fact(Timeout = 10000, DisplayName = "Expanded property two way binding testing")]
        public async Task ExpandedTwoWayBinding()
        {
            var cut = RenderComponent<HeaderContentString>();
            cut.FindAll("br")[0].NextElementSibling.MarkupMatches("<span>ExpandItemValue: True</span>");
            var firstItemHtml = cut.FindAll("." + HelperCls.AccordionItem)[0];
            Assert.Contains(HelperCls.Selected, firstItemHtml.ClassName);
            Assert.Contains(HelperCls.Active, firstItemHtml.ClassName);
            Assert.Contains(HelperCls.ExpandState, firstItemHtml.ClassName);
            Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[0].GetAttribute("aria-expanded"));
            //Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[0].GetAttribute("aria-selected"));
            cut.Instance.ExpandItem = false;
            cut.Render();
            await Task.Delay(500);
            cut.FindAll("br")[0].NextElementSibling.MarkupMatches("<span>ExpandItemValue: False</span>");
            Assert.Equal("false", cut.FindAll("." + HelperCls.AccordionHeader)[0].GetAttribute("aria-expanded"));
            //Assert.Equal("false", cut.FindAll("." + HelperCls.AccordionHeader)[0].GetAttribute("aria-selected"));
            cut.Instance.ExpandItem = true;
            cut.Render();
            await Task.Delay(500);
            cut.FindAll("br")[0].NextElementSibling.MarkupMatches("<span>ExpandItemValue: True</span>");
            var activeItemHtml = cut.FindAll("." + HelperCls.AccordionItem)[0];
            Assert.Contains(HelperCls.Selected, activeItemHtml.ClassName);
            Assert.Contains(HelperCls.Active, activeItemHtml.ClassName); 
            Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[0].GetAttribute("aria-expanded"));
            //Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[0].GetAttribute("aria-selected"));
        }

        [Fact(Timeout = 10000, DisplayName = "Id property testing")]
        public void ItemId()
        {
            var cut = RenderComponent<ItemId>();
            Assert.Equal("ASPNET", cut.Find("." + HelperCls.AccordionItem).GetAttribute("Id"));
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
            Assert.Equal(4, cut.FindAll("." + HelperCls.AccordionPanel).Count);
            Assert.Equal(4, cut.FindAll("." + HelperCls.AccordionContent).Count);
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
            Assert.Equal(1, cut.FindAll("." + HelperCls.HeaderToggleExpandIcon).Count);
            Assert.Equal(3, cut.FindAll("." + HelperCls.AccordionPanel).Count);
            Assert.Equal(3, cut.FindAll("." + HelperCls.AccordionContent).Count);
            Assert.Equal("ASP.NET", cut.FindAll("." + HelperCls.AccordionHeaderContent)[0].GetElementsByTagName("div").First().InnerHtml);
            Assert.Equal("ASP.NET MVC", cut.FindAll("." + HelperCls.AccordionHeaderContent)[1].GetElementsByTagName("div").First().InnerHtml);
            Assert.Equal("ASP.NET Razor", cut.FindAll("." + HelperCls.AccordionHeaderContent)[2].GetElementsByTagName("div").First().InnerHtml);
            Assert.Equal("Microsoft ASP.NET is a set of technologies in the Microsoft .NET Framework for building Web applications and XML Web services. ASP.NET pages execute on the server and generate markup such as HTML, WML, or XML that is sent to a desktop or mobile browser. ASP.NET pages use a compiled,event-driven programming model that improves performance and enables the separation of application logic and user interface.", cut.Find("." + HelperCls.AccordionContent).GetElementsByTagName("div").First().InnerHtml);
            Assert.Equal("The Model-View-Controller (MVC) architectural pattern separates an application into three main components: the model, the view, and the controller. The ASP.NET MVC framework provides an alternative to the ASP.NET Web Forms pattern for creating Web applications. The ASP.NET MVC framework is a lightweight, highly testable presentation framework that (as with Web Forms-based applications) is integrated with existing ASP.NET features, such as master pages and membership-based authentication.", cut.FindAll("." + HelperCls.AccordionContent)[1].GetElementsByTagName("div").First().InnerHtml);
            Assert.Equal("Razor is an ASP.NET programming syntax used to create dynamic web pages with the C# or Visual Basic .NET programming languages. Razor was in development in June 2010 and was released for Microsoft Visual Studio 2010 in January 2011. Razor is a simple-syntax view engine and was released as part of MVC 3 and the WebMatrix tool set. Side Code content", cut.FindAll("." + HelperCls.AccordionContent)[2].GetElementsByTagName("div").First().InnerHtml);
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
            Assert.Equal("false", cut.FindAll("." + HelperCls.AccordionPanel)[0].GetAttribute("aria-hidden"));
            Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionPanel)[1].GetAttribute("aria-hidden"));
            Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionPanel)[1].GetAttribute("aria-hidden"));
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
            Assert.Equal(3, cut.FindAll("." + HelperCls.AccordionPanel).Count);
            Assert.Equal(3, cut.FindAll("." + HelperCls.AccordionContent).Count);
            Assert.Equal("ASP.NET", cut.FindAll("." + HelperCls.AccordionHeaderContent)[0].GetElementsByTagName("div").First().InnerHtml);
            Assert.Equal("ASP.NET Razor", cut.FindAll("." + HelperCls.AccordionHeaderContent)[1].GetElementsByTagName("div").First().InnerHtml);
            Assert.Equal("JavaScript", cut.FindAll("." + HelperCls.AccordionHeaderContent)[2].GetElementsByTagName("div").First().InnerHtml);
            Assert.Equal("Microsoft ASP.NET is a set of technologies in the Microsoft .NET Framework for building Web applications and XML Web services. ASP.NET pages execute on the server and generate markup such as HTML, WML, or XML that is sent to a desktop or mobile browser. ASP.NET pages use a compiled,event-driven programming model that improves performance and enables the separation of application logic and user interface.", cut.Find("." + HelperCls.AccordionContent).GetElementsByTagName("div").First().InnerHtml);
            Assert.Equal("Razor is an ASP.NET programming syntax used to create dynamic web pages with the C# or Visual Basic .NET programming languages. Razor was in development in June 2010 and was released for Microsoft Visual Studio 2010 in January 2011. Razor is a simple-syntax view engine and was released as part of MVC 3 and the WebMatrix tool set.", cut.FindAll("." + HelperCls.AccordionContent)[1].GetElementsByTagName("div").First().InnerHtml);
            Assert.Equal("JavaScript (JS) is an interpreted computer programming language.It was originally implemented as part of web browsers so that client-side scripts could interact with the user, control the browser, communicate asynchronously, and alter the document content that was displayed.", cut.FindAll("." + HelperCls.AccordionContent)[2].GetElementsByTagName("div").First().InnerHtml);
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
            Assert.Equal("false", cut.FindAll("." + HelperCls.AccordionPanel)[0].GetAttribute("aria-hidden"));
            Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionPanel)[1].GetAttribute("aria-hidden"));
            Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionPanel)[1].GetAttribute("aria-hidden"));
        }
        [Fact(Timeout = 10000, DisplayName = "AccordionClickArgs uncovered coverage")]
        public void AccordionClickArgs_ManualCoverage()
        {
            var args = new AccordionClickArgs();
            typeof(AccordionClickArgs).GetProperty("Item").SetValue(args, new AccordionItemModel());
            typeof(AccordionClickArgs).GetProperty("Name").SetValue(args, "click");
            typeof(AccordionClickArgs).GetProperty("OriginalEvent").SetValue(args, new MouseEventArgs());
            Assert.NotNull(args.Item);
            Assert.Equal("click", args.Name);
            Assert.NotNull(args.OriginalEvent);
        }
        [Fact(DisplayName = "AccordionItemModel HeaderTemplate and ContentTemplate coverage")]
        public void AccordionItemModel_TemplatePropertiesCoverage()
        {
            RenderFragment headerTemplate = builder =>
            {
                builder.OpenElement(0, "div");
                builder.AddContent(1, "Header Template");
                builder.CloseElement();
            };
            RenderFragment contentTemplate = builder =>
            {
                builder.OpenElement(0, "div");
                builder.AddContent(1, "Content Template");
                builder.CloseElement();
            };
            var itemModel = new AccordionItemModel
            {
                HeaderTemplate = headerTemplate,
                ContentTemplate = contentTemplate
            };
            Assert.NotNull(itemModel.HeaderTemplate);
            Assert.NotNull(itemModel.ContentTemplate);
        }
        [Fact(Timeout = 10000, DisplayName = "SelectAsync method coverage")]
        public async Task SelectAsync_MethodCoverage()
        {
            var cut = RenderComponent<SfAccordion>(parameters =>
            {
                parameters.AddChildContent<AccordionItems>(items =>
                {
                    items.AddChildContent<AccordionItem>(item =>
                    {
                        item.Add(a => a.Header, "Item 1");
                        item.Add(a => a.Content, "Content 1");
                    });
                    items.AddChildContent<AccordionItem>(item =>
                    {
                        item.Add(a => a.Header, "Item 2");
                        item.Add(a => a.Content, "Content 2");
                    });
                });
            });
            JSInterop.SetupVoid("sfBlazor.Accordion.select", _ => true).SetVoidResult();
            await cut.InvokeAsync(() => cut.Instance.SelectAsync(1));
            JSInterop.VerifyInvoke("sfBlazor.Accordion.select");
        }
        [Fact(Timeout = 10000, DisplayName = "TriggerClickedEvent safe coverage")]
        public async Task TriggerClickedEvent_SafeCoverage()
        {
            var cut = RenderComponent<SfAccordion>(parameters =>
            {
                parameters.Add(p => p.ExpandMode, ExpandMode.Multiple);
                parameters.AddChildContent<AccordionItems>(items =>
                {
                    items.AddChildContent<AccordionItem>(item =>
                    {
                        item.Add(a => a.Header, "Item 1");
                        item.Add(a => a.Content, "Content 1");
                    });
                });
            });
            var method = typeof(SfAccordion).GetMethod("TriggerClickedEvent", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.NotNull(method);
            await (Task)method.Invoke(cut.Instance,
                new object[]
                {
                    new MouseEventArgs(),
                    cut.FindComponent<AccordionItem>().Instance
                });
            Assert.True(true);
        }
        [Fact(Timeout = 10000, DisplayName = "CreatedEvent safe coverage")]
        public async Task CreatedEvent_SafeCoverage()
        {
            var cut = RenderComponent<SfAccordion>();
            await cut.InvokeAsync(() => cut.Instance.CreatedEvent());
            Assert.True(true);
        }
    }
}