using AngleSharp.Css.Dom;
using AngleSharp.Dom;
using Bunit;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Tests.Accordion.Samples.ContentRenderSingleExpandMode.Default;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Syncfusion.Blazor.Tests.Accordion
{
    public class ContentRenderSingleExpandModeDefault : BunitTestContext
    {
        public Helper HelperCls = new();

        [Fact(Timeout = 10000, DisplayName = "Initial loading testing with header and content")]
        public void Default()
        {
            var cut = RenderComponent<Default>();
            var accordionHtml = cut.Find("." + HelperCls.Accordion);
            Assert.NotNull(accordionHtml);
            Assert.Contains(HelperCls.Control, accordionHtml.ClassName);
            Assert.Contains(HelperCls.Accordion, accordionHtml.ClassName);
            var accordionStyle = accordionHtml.GetAttribute("data-sf-style");
            Assert.Contains("100%", accordionStyle);
            Assert.Contains("auto", accordionStyle);
            Assert.Equal(3, cut.FindAll("." + HelperCls.AccordionItem).Count);
            Assert.Equal(3, cut.FindAll("." + HelperCls.AccordionHeader).Count);
            Assert.Equal(3, cut.FindAll("." + HelperCls.AccordionHeaderContent).Count);
            Assert.Equal(3, cut.FindAll("." + HelperCls.HeaderToggleIcon).Count);
            Assert.Equal(3, cut.FindAll("." + HelperCls.HeaderToggleCollapseIcon).Count);
            Assert.Equal(1, cut.FindAll("." + HelperCls.HeaderToggleExpandIcon).Count);
            Assert.Equal(3, cut.FindAll("." + HelperCls.AccordionPanel).Count);
            Assert.Equal(3, cut.FindAll("." + HelperCls.AccordionContent).Count);
            Assert.Equal("ASP.NET", cut.Find("." + HelperCls.AccordionHeaderContent).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("ASP.NET MVC", cut.FindAll("." + HelperCls.AccordionHeaderContent)[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("JavaScript", cut.FindAll("." + HelperCls.AccordionHeaderContent)[2].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Microsoft ASP.NET is a set of technologies in the Microsoft .NET Framework for building Web applications and XML Web services. ASP.NET pages execute on the server and generate markup such as HTML, WML, or XML that is sent to a desktop or mobile browser. ASP.NET pages use a compiled,event-driven programming model that improves performance and enables the separation of application logic and user interface.", cut.Find("." + HelperCls.AccordionContent).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("The Model-View-Controller (MVC) architectural pattern separates an application into three main components: the model, the view, and the controller. The ASP.NET MVC framework provides an alternative to the ASP.NET Web Forms pattern for creating Web applications. The ASP.NET MVC framework is a lightweight, highly testable presentation framework that (as with Web Forms-based applications) is integrated with existing ASP.NET features, such as master pages and membership-based authentication.", cut.FindAll("." + HelperCls.AccordionContent)[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("JavaScript (JS) is an interpreted computer programming language.It was originally implemented as part of web browsers so that client-side scripts could interact with the user, control the browser, communicate asynchronously, and alter the document content that was displayed.More recently, however, it has become common in both game development and the creation of desktop applications.", cut.FindAll("." + HelperCls.AccordionContent)[2].TextContent.Replace("\n", string.Empty).Trim());
            var expandItemHtml = cut.Find("." + HelperCls.AccordionItem);
            Assert.Contains(HelperCls.ExpandState, expandItemHtml.ClassName);
            Assert.Contains(HelperCls.Selected, expandItemHtml.ClassName);
            Assert.Contains(HelperCls.Active, expandItemHtml.ClassName);
            var secondItemHtml = cut.FindAll("." + HelperCls.AccordionPanel)[1];
            Assert.Contains(HelperCls.ContentHide, secondItemHtml.ClassName);
            var thirdItemHtml = cut.FindAll("." + HelperCls.AccordionPanel)[2];
            Assert.Contains(HelperCls.ContentHide, thirdItemHtml.ClassName);
            Assert.Equal("false", cut.FindAll("." + HelperCls.AccordionPanel)[0].GetAttribute("aria-hidden"));
            Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionPanel)[1].GetAttribute("aria-hidden"));
            Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionPanel)[1].GetAttribute("aria-hidden"));
        }

        [Fact(Timeout = 10000, DisplayName = "Properties default value testing- Accordion")]
        public void DefaultValueAccordion()
        {
            var cut = RenderComponent<SfAccordion>(options => options.Add(mode => mode.ExpandMode, ExpandMode.Single).Add(content => content.LoadOnDemand, false));
            Assert.False(cut.Instance.EnablePersistence);
            Assert.False(cut.Instance.LoadOnDemand);
            Assert.False(cut.Instance.EnableRtl);
            Assert.Equal(ExpandMode.Single, cut.Instance.ExpandMode);
            Assert.Null(cut.Instance.ExpandedIndices);
            Assert.Equal("auto", cut.Instance.Height);
            Assert.Equal("100%", cut.Instance.Width);
        }

        [Fact(Timeout = 10000, DisplayName = "Interactions testing")]
        public void Interactions()
        {
            var cut = RenderComponent<Default>();
            Assert.Equal(3, cut.FindAll("." + HelperCls.AccordionPanel).Count);
            Assert.Equal(3, cut.FindAll("." + HelperCls.AccordionContent).Count);
            cut.FindAll("." + HelperCls.AccordionHeader)[1].Click();
            Assert.Equal("The Model-View-Controller (MVC) architectural pattern separates an application into three main components: the model, the view, and the controller. The ASP.NET MVC framework provides an alternative to the ASP.NET Web Forms pattern for creating Web applications. The ASP.NET MVC framework is a lightweight, highly testable presentation framework that (as with Web Forms-based applications) is integrated with existing ASP.NET features, such as master pages and membership-based authentication.", cut.FindAll("." + HelperCls.AccordionContent)[1].TextContent.Replace("\n", string.Empty).Trim());
            cut.FindAll("." + HelperCls.AccordionHeader)[2].Click();
            Assert.Equal("JavaScript (JS) is an interpreted computer programming language.It was originally implemented as part of web browsers so that client-side scripts could interact with the user, control the browser, communicate asynchronously, and alter the document content that was displayed.More recently, however, it has become common in both game development and the creation of desktop applications.", cut.FindAll("." + HelperCls.AccordionContent)[2].TextContent.Replace("\n", string.Empty).Trim());
        }

        [Fact(Timeout = 10000, DisplayName = "Height testing")]
        public void Height()
        {
            var cut = RenderComponent<WidthAndHeight>();
            var accordionEle = cut.Find("." + HelperCls.Accordion);
            var accordionHeight = accordionEle.GetAttribute("data-sf-style");
            var expectedValue = "width: 600px;height: 260px;";
            expectedValue.MarkupMatches(accordionHeight);
            Assert.Contains("260px", accordionHeight);
        }

        [Fact(Timeout = 10000, DisplayName = "Width testing")]
        public void Width()
        {
            var cut = RenderComponent<WidthAndHeight>();
            var accordionEle = cut.Find("." + HelperCls.Accordion);
            var accordionWidth = accordionEle.GetAttribute("data-sf-style");
            var expectedValue = "width: 600px;height: 260px;";
            expectedValue.MarkupMatches(accordionWidth);
            Assert.Contains("600px", accordionWidth);
        }

        [Fact(Timeout = 10000, DisplayName = "Expanded indices testing")]
        public void ExpandedIndices()
        {
            var cut = RenderComponent<ExpandedIndices>();
            Assert.Equal(3, cut.FindAll("." + HelperCls.AccordionItem).Count);
            Assert.Equal(3, cut.FindAll("." + HelperCls.AccordionHeader).Count);
            Assert.Equal(3, cut.FindAll("." + HelperCls.AccordionHeaderContent).Count);
            Assert.Equal(3, cut.FindAll("." + HelperCls.HeaderToggleIcon).Count);
            Assert.Equal(3, cut.FindAll("." + HelperCls.HeaderToggleCollapseIcon).Count);
            Assert.Equal(1, cut.FindAll("." + HelperCls.HeaderToggleExpandIcon).Count);
            Assert.Equal(3, cut.FindAll("." + HelperCls.AccordionPanel).Count);
            Assert.Equal(3, cut.FindAll("." + HelperCls.AccordionContent).Count);
            Assert.Equal("ASP.NET", cut.Find("." + HelperCls.AccordionHeaderContent).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("ASP.NET MVC", cut.FindAll("." + HelperCls.AccordionHeaderContent)[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("JavaScript", cut.FindAll("." + HelperCls.AccordionHeaderContent)[2].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Microsoft ASP.NET is a set of technologies in the Microsoft .NET Framework for building Web applications and XML Web services.", cut.Find("." + HelperCls.AccordionContent).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("The Model-View-Controller (MVC) architectural pattern separates an application into three main components: the model, the view, and the controller.", cut.FindAll("." + HelperCls.AccordionContent)[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("JavaScript (JS) is an interpreted computer programming language.It was originally implemented as part of web browsers so that client-side scripts could interact with the user, control the browser, communicate asynchronously, and alter the document content that was displayed.", cut.FindAll("." + HelperCls.AccordionContent)[2].TextContent.Replace("\n", string.Empty).Trim());
            var expandItemHtml = cut.Find("." + HelperCls.AccordionItem);
            Assert.Contains(HelperCls.ExpandState, expandItemHtml.ClassName);
            Assert.Contains(HelperCls.Selected, expandItemHtml.ClassName);
            Assert.Contains(HelperCls.Active, expandItemHtml.ClassName);
        }

        [Fact(Timeout = 10000, DisplayName = "Expanded indices two way binding testing")]
        public async Task ExpandedIndicesTwoWayBinding()
        {
            var cut = RenderComponent<ExpandedIndices>();
            cut.FindAll("br")[0].NextElementSibling.MarkupMatches("<span>ExpandItemsLength: 1</span>");
            cut.FindAll("br")[1].NextElementSibling.MarkupMatches("<span>ExpandItemsValue: 0</span>");
            var firstItemHtml = cut.FindAll("." + HelperCls.AccordionItem)[0];
            Assert.Contains(HelperCls.Selected, firstItemHtml.ClassName);
            Assert.Contains(HelperCls.Active, firstItemHtml.ClassName);
            Assert.Contains(HelperCls.ExpandState, firstItemHtml.ClassName);
            Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[0].GetAttribute("aria-expanded"));
            //Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[0].GetAttribute("aria-selected"));
            cut.Instance.ExpandItems = new int[] { 1 };
            cut.Render();
            await Task.Delay(500);
            cut.FindAll("br")[0].NextElementSibling.MarkupMatches("<span>ExpandItemsLength: 1</span>");
            cut.FindAll("br")[1].NextElementSibling.MarkupMatches("<span>ExpandItemsValue: 1</span>");
            var secondItemHtml = cut.FindAll("." + HelperCls.AccordionItem)[1];
            Assert.Contains(HelperCls.Selected, secondItemHtml.ClassName);
            Assert.Contains(HelperCls.Active, secondItemHtml.ClassName);
            Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[1].GetAttribute("aria-expanded"));
            //Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[1].GetAttribute("aria-selected"));
            cut.Instance.ExpandItems = new int[] { 2 };
            cut.Render();
            await Task.Delay(500);
            cut.FindAll("br")[0].NextElementSibling.MarkupMatches("<span>ExpandItemsLength: 1</span>");
            cut.FindAll("br")[1].NextElementSibling.MarkupMatches("<span>ExpandItemsValue: 2</span>");
            var thirdItemHtml = cut.FindAll("." + HelperCls.AccordionItem)[2];
            Assert.Contains(HelperCls.Selected, thirdItemHtml.ClassName);
            Assert.Contains(HelperCls.Active, thirdItemHtml.ClassName);
            Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[2].GetAttribute("aria-expanded"));
            //Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[2].GetAttribute("aria-selected"));
            await cut.Instance.AccordionClick(); await Task.Delay(500);
            await cut.InvokeAsync(async () =>
            {
                await cut.Instance.Acc.TriggerExpandingEvent(1);
                await Task.Delay(500);
                await cut.Instance.Acc.TriggerExpandedEvent(new ExpandEventArgs
                { IsExpanded = true, Index = 1, Cancel = false, Name = "expanding" });
                await Task.Delay(500);
                cut.Render();
            });
            Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[1].GetAttribute("aria-expanded"));
            await cut.InvokeAsync(async () =>
            {
                await cut.Instance.Acc.TriggerCollapsingEvent(1);
                await Task.Delay(500);
                await cut.Instance.Acc.TriggerCollapsedEvent(new ExpandEventArgs
                { IsExpanded = false, Index = 1, Cancel = false, Name = "collapsing" });
                await Task.Delay(500);
                cut.Render();
            });
            Assert.Equal("false", cut.FindAll("." + HelperCls.AccordionHeader)[1].GetAttribute("aria-expanded"));
        }

        [Fact(Timeout = 10000, DisplayName = "ARIA attributes testing")]
        public void ARIAAttributes()
        {
            var cut = RenderComponent<Default>();
            Assert.Equal("button", cut.FindAll("." + HelperCls.AccordionHeader)[0].GetAttribute("role"));
            Assert.Equal("button", cut.FindAll("." + HelperCls.AccordionHeader)[1].GetAttribute("role"));
            Assert.Equal("button", cut.FindAll("." + HelperCls.AccordionHeader)[2].GetAttribute("role"));
            Assert.Equal("region", cut.Find("." + HelperCls.AccordionPanel).GetAttribute("role"));
            //Assert.Equal("false", cut.Find("." + HelperCls.Accordion).GetAttribute("aria-multiselectable"));
            Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[0].GetAttribute("aria-expanded"));
            Assert.Equal("false", cut.FindAll("." + HelperCls.AccordionHeader)[1].GetAttribute("aria-expanded"));
            Assert.Equal("false", cut.FindAll("." + HelperCls.AccordionHeader)[2].GetAttribute("aria-expanded"));
            //Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionHeader)[0].GetAttribute("aria-selected"));
            //Assert.Equal("false", cut.FindAll("." + HelperCls.AccordionHeader)[1].GetAttribute("aria-selected"));
            //Assert.Equal("false", cut.FindAll("." + HelperCls.AccordionHeader)[2].GetAttribute("aria-selected"));
            Assert.Equal("false", cut.FindAll("." + HelperCls.AccordionHeader)[0].GetAttribute("aria-disabled"));
            Assert.Equal("false", cut.FindAll("." + HelperCls.AccordionHeader)[1].GetAttribute("aria-disabled"));
            Assert.Equal("false", cut.FindAll("." + HelperCls.AccordionHeader)[2].GetAttribute("aria-disabled"));
            Assert.Equal("false", cut.FindAll("." + HelperCls.AccordionPanel)[0].GetAttribute("aria-hidden"));
            Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionPanel)[1].GetAttribute("aria-hidden"));
            Assert.Equal("true", cut.FindAll("." + HelperCls.AccordionPanel)[1].GetAttribute("aria-hidden"));
        }

        [Fact(Timeout = 10000, DisplayName = "EnableRtl as 'true' testing")]
        public void EnableRtlAsTrue()
        {
            var accordion = RenderComponent<SfAccordion>(options => options.Add(mode => mode.ExpandMode, ExpandMode.Single).Add(p => p.EnableRtl, true).Add(content => content.LoadOnDemand, false));
            var rootEle = accordion.Find("." + HelperCls.Accordion);
            Assert.Contains(HelperCls.RTL, rootEle.ClassName);
        }

        [Fact(Timeout = 10000, DisplayName = "EnableRtl as 'false' testing")]
        public void EnableRtlAsFalse()
        {
            var accordion = RenderComponent<SfAccordion>(options => options.Add(mode => mode.ExpandMode, ExpandMode.Single).Add(p => p.EnableRtl, false).Add(content => content.LoadOnDemand, false));
            var rootEle = accordion.Find("." + HelperCls.Accordion);
            Assert.DoesNotContain(HelperCls.RTL, rootEle.ClassName);
        }
    }
}