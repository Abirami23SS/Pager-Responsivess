using Xunit;
using Bunit;
using AngleSharp.Dom;
using AngleSharp.Css.Dom;
using System.Threading.Tasks;
using Syncfusion.Blazor.Tests.Carousel.Samples;
using Syncfusion.Blazor.Navigations;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;

namespace Syncfusion.Blazor.Tests.Carousel
{
    public class Carousel : BunitTestContext
    {
        public Helper HelperCls = new Helper();

        [Fact(Timeout = 10000)]
        public void CarouselInAccordion()
        {
            var cut = RenderComponent<CarouselInAccordion>();
            Assert.Equal("Carousel Sample-1", cut.Find("." + HelperCls.AccordionHeaderContent).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Carousel Sample-2", cut.FindAll("." + HelperCls.AccordionHeaderContent)[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal(2, cut.FindAll("." + HelperCls.AccordionItem).Count);
            Assert.Equal(2, cut.FindAll("." + HelperCls.AccordionHeaderContent).Count);
            Assert.NotNull(cut.Find(".e-carousel-indicators"));
            var firstItemHtml = cut.Find("." + HelperCls.AccordionItem);
            Assert.Contains(HelperCls.Selected, firstItemHtml.ClassName);
            var secondItemHtml = cut.FindAll("." + HelperCls.AccordionItem)[1];
            Assert.Contains(HelperCls.Selected, secondItemHtml.ClassName);
            Assert.Equal("true", cut.FindAll("." + HelperCls.ActiveItem)[0].GetAttribute("aria-expanded"));
            Assert.True(3 == cut.FindAll("." + HelperCls.Slideindicator).Count);
            Assert.Contains(HelperCls.Control, cut.Find("." + HelperCls.Carousel).ClassName);
            Assert.Contains("e-previous-button", cut.Find("." + HelperCls.PreviousButton).QuerySelector("button").ClassName);
            Assert.Contains("e-next-button", cut.Find("." + HelperCls.NextButton).QuerySelector("button").ClassName);
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
            Assert.Equal("false", cut.FindAll("." + HelperCls.CarouselItem)[1].GetAttribute("aria-hidden"));
            Assert.Equal("Slide 1", cut.FindAll("." + HelperCls.CarouselSlide)[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Slide 2", cut.FindAll("." + HelperCls.CarouselSlide)[2].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Slide 3", cut.FindAll("." + HelperCls.CarouselSlide)[3].TextContent.Replace("\n", string.Empty).Trim());
            cut.Instance.ExpandedIndices = new int[] { 1 };
            cut.Render();
            Assert.Equal("true", cut.FindAll("." + HelperCls.ActiveItem)[1].GetAttribute("aria-expanded"));
            Assert.Contains("e-previous-button", cut.Find("." + HelperCls.PreviousButton).QuerySelector("button").ClassName);
            Assert.Contains("e-next-button", cut.Find("." + HelperCls.NextButton).QuerySelector("button").ClassName);
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
            Assert.Equal("group", cut.Find("." + HelperCls.CarouselItem).GetAttribute("role"));
            Assert.True(6 == cut.FindAll("." + HelperCls.Slideindicator).Count);
            Assert.Equal("Slide 4", cut.FindAll("." + HelperCls.CarouselSlide)[6].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Slide 5", cut.FindAll("." + HelperCls.CarouselSlide)[7].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Slide 6", cut.FindAll("." + HelperCls.CarouselSlide)[8].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("slide", cut.Find("." + HelperCls.CarouselItem).GetAttribute("aria-roledescription"));
            Assert.Contains("slide-content", cut.FindAll("." + HelperCls.CarouselItem)[1].FirstElementChild.ClassName);
        }
        [Fact(Timeout = 10000)]
        public async Task CarouselInTab()
        {
            var cut = RenderComponent<CarouselInTab>();
            await Task.Delay(300);
            Assert.Equal("Carousel Sample-1", cut.Find("." + HelperCls.ToolbarHeaderContent).TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Carousel Sample-2", cut.FindAll("." + HelperCls.ToolbarHeaderContent)[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Contains(HelperCls.Control, cut.Find("." + HelperCls.Carousel).ClassName);
            Assert.Equal(1, cut.FindAll("." + HelperCls.Carousel).Count);
            Assert.NotNull(cut.Find(".e-carousel-indicators"));
            Assert.Contains("e-previous-button", cut.Find("." + HelperCls.PreviousButton).QuerySelector("button").ClassName);
            Assert.Contains("e-next-button", cut.Find("." + HelperCls.NextButton).QuerySelector("button").ClassName);
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
            Assert.Equal("Slide 1", cut.FindAll("." + HelperCls.CarouselSlide)[1].GetInnerText());
            Assert.Equal("Slide 2", cut.FindAll("." + HelperCls.CarouselSlide)[2].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Slide 3", cut.FindAll("." + HelperCls.CarouselSlide)[3].TextContent.Replace("\n", string.Empty).Trim());
            Assert.True(3 == cut.FindAll("." + HelperCls.Slideindicator).Count);
            Assert.Contains("e-previous-button", cut.Find("." + HelperCls.PreviousButton).QuerySelector("button").ClassName);
            Assert.Contains("e-next-button", cut.Find("." + HelperCls.NextButton).QuerySelector("button").ClassName);
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
            cut.Instance.SelectedTab = 1;
            cut.Render();
            await Task.Delay(300);
            Assert.Contains("e-previous-button", cut.Find("." + HelperCls.PreviousButton).QuerySelector("button").ClassName);
            Assert.Contains("e-next-button", cut.Find("." + HelperCls.NextButton).QuerySelector("button").ClassName);
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
            Assert.Equal("Slide 4", cut.FindAll("." + HelperCls.CarouselSlide)[1].GetInnerText());
            Assert.Equal("Slide 5", cut.FindAll("." + HelperCls.CarouselSlide)[2].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Slide 6", cut.FindAll("." + HelperCls.CarouselSlide)[3].TextContent.Replace("\n", string.Empty).Trim());
            Assert.True(3 == cut.FindAll("." + HelperCls.Slideindicator).Count);
            Assert.Contains("e-previous-button", cut.Find("." + HelperCls.PreviousButton).QuerySelector("button").ClassName);
            Assert.Contains("e-next-button", cut.Find("." + HelperCls.NextButton).QuerySelector("button").ClassName);
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
        }
        [Fact(Timeout = 10000)]
        public async Task AutoPlay()
        {
            var cut = RenderComponent<CarouselPropertyChanges>();
            Assert.True(7 == cut.FindAll("." + HelperCls.checkbox).Count);
            Assert.True(7 == cut.FindAll("." + HelperCls.Input).Count);
            Assert.Contains("e-previous-button", cut.Find("." + HelperCls.PreviousButton).QuerySelector("button").ClassName);
            Assert.Contains("e-next-button", cut.Find("." + HelperCls.NextButton).QuerySelector("button").ClassName);
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
            Assert.True(5 == cut.FindAll("." + HelperCls.Slideindicator).Count);
            Assert.Equal("Slide 1 of 5", cut.FindAll(".e-indicator")[0].GetAttribute("aria-label"));
            Assert.Equal("Slide 2 of 5", cut.FindAll(".e-indicator")[1].GetAttribute("aria-label"));
            Assert.Equal("Slide 3 of 5", cut.FindAll(".e-indicator")[2].GetAttribute("aria-label"));
            Assert.Equal("Slide 4 of 5", cut.FindAll(".e-indicator")[3].GetAttribute("aria-label"));
            Assert.Equal("Slide 5 of 5", cut.FindAll(".e-indicator")[4].GetAttribute("aria-label"));
            Assert.NotNull(cut.Find(".e-carousel-indicators"));
            Assert.Equal("polite", cut.Find(".e-carousel-items").GetAttribute("aria-live"));
            cut.FindAll(".e-checkbox")[0].Click(); await Task.Delay(50);
            Assert.Equal("off", cut.Find(".e-carousel-items").GetAttribute("aria-live"));
            cut.FindAll(".e-checkbox")[0].Click(); await Task.Delay(50);
            Assert.Equal("polite", cut.Find(".e-carousel-items").GetAttribute("aria-live"));
        }
        [Fact(Timeout = 10000)]
        public async Task ShowIndicator()
        {
            var cut = RenderComponent<CarouselPropertyChanges>();
            Assert.Contains("e-previous-button", cut.Find("." + HelperCls.PreviousButton).QuerySelector("button").ClassName);
            Assert.Contains("e-next-button", cut.Find("." + HelperCls.NextButton).QuerySelector("button").ClassName);
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
            Assert.NotNull(cut.Find(".e-carousel-indicators"));
            Assert.Equal(5, cut.FindAll("." + HelperCls.Slideindicator).Count);
            cut.FindAll(".e-checkbox")[3].Click();
            await Task.Delay(200);
            Assert.Null(cut.Find(".e-carousel").QuerySelector(".e-carousel-indicators"));
        }
        [Fact(Timeout = 10000)]
        public async Task ShowPlayButton()
        {
            var cut = RenderComponent<CarouselPropertyChanges>();
            Assert.Contains("e-previous-button", cut.Find("." + HelperCls.PreviousButton).QuerySelector("button").ClassName);
            Assert.Contains("e-next-button", cut.Find("." + HelperCls.NextButton).QuerySelector("button").ClassName);
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
            Assert.NotNull(cut.Find(".e-carousel-indicators"));
            Assert.Equal(5, cut.FindAll("." + HelperCls.Slideindicator).Count);
            Assert.Null(cut.Find(".e-carousel-navigators").QuerySelector(".e-play-pause"));
            cut.FindAll(".e-checkbox")[6].Click();
            await Task.Delay(200);
            Assert.NotNull(cut.Find(".e-carousel-navigators").QuerySelector(".e-play-pause"));
        }
        [Fact(Timeout = 10000)]
        public async Task Loop()
        {
            var cut = RenderComponent<CarouselPropertyChanges>();
            Assert.Contains("e-previous-button", cut.Find("." + HelperCls.PreviousButton).QuerySelector("button").ClassName);
            Assert.Contains("e-next-button", cut.Find("." + HelperCls.NextButton).QuerySelector("button").ClassName);
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
            cut.FindAll(".e-checkbox")[2].Click(); await Task.Delay(50);
            Assert.Equal("Slide 1 of 5", cut.FindAll(".e-indicator")[0].GetAttribute("aria-label"));
            Assert.Equal("0", cut.Find(".e-numerictextbox").GetAttribute("aria-valuenow"));
            cut.FindAll(".e-indicator-bar")[4].Click(); await Task.Delay(50);
            Assert.Equal("Slide 5 of 5", cut.FindAll(".e-indicator")[4].GetAttribute("aria-label"));
            Assert.Equal("4", cut.Find(".e-numerictextbox").GetAttribute("aria-valuenow"));
        }
        [Fact(Timeout = 10000)]
        public async Task PauseonHover()
        {
            var cut = RenderComponent<CarouselPropertyChanges>();
            cut.FindAll(".e-checkbox")[0].Click(); await Task.Delay(100);
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
            cut.Find("." + HelperCls.Carousel).MouseOver(); await Task.Delay(5000);
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
            Assert.Contains("e-carousel-hover", cut.Find("." + HelperCls.Carousel).ClassName);
            cut.Find("." + HelperCls.Carousel).MouseOut();
            Assert.DoesNotContain("e-carousel-hover", cut.Find("." + HelperCls.Carousel).ClassName);
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
        }
        [Fact(Timeout = 10000)]
        public async Task EnableRtl()
        {
            var cut = RenderComponent<CarouselPropertyChanges>();
            Assert.Contains("e-carousel-slide-animation", cut.Find("." + HelperCls.Carousel).ClassName);
            Assert.Equal("Slide 1 of 5", cut.FindAll(".e-indicator")[0].GetAttribute("aria-label"));
            cut.FindAll(".e-checkbox")[5].Click(); await Task.Delay(100);
            Assert.Contains("e-rtl", cut.Find("." + HelperCls.Carousel).ClassName);
            Assert.Equal("Slide 1 of 5", cut.FindAll(".e-indicator")[0].GetAttribute("aria-label"));
        }
        [Fact(Timeout = 10000)]
        public async Task AnimationEffect()
        {
            var cut = RenderComponent<CarouselPropertyChanges>();
            Assert.Contains("e-carousel-slide-animation", cut.Find("." + HelperCls.Carousel).ClassName);
            cut.Instance.AnimationEffect = CarouselAnimationEffect.Fade;
            cut.Render(); await Task.Delay(100);
            Assert.Contains("e-carousel-fade-animation", cut.Find("." + HelperCls.Carousel).ClassName);
            cut.Instance.AnimationEffect = CarouselAnimationEffect.None;
            cut.Render(); await Task.Delay(500);
            Assert.Contains("e-carousel-animation-none", cut.Find("." + HelperCls.Carousel).ClassName);
            cut.Instance.AnimationEffect = CarouselAnimationEffect.Custom;
            cut.Render(); await Task.Delay(500);
            Assert.Contains("e-carousel-custom-animation", cut.Find("." + HelperCls.Carousel).ClassName);
        }
        [Fact(Timeout = 10000)]
        public async Task ButtonVisibility()
        {
            var cut = RenderComponent<CarouselPropertyChanges>();
            Assert.Contains("e-previous-button", cut.Find("." + HelperCls.PreviousButton).QuerySelector("button").ClassName);
            Assert.Contains("e-next-button", cut.Find("." + HelperCls.NextButton).QuerySelector("button").ClassName);
            cut.Instance.ButtonVisibility = CarouselButtonVisibility.VisibleOnHover;
            cut.Render();
            cut.Find("." + HelperCls.Carousel).MouseOver();
            Assert.NotEqual("e-previous e-hover-arrows", cut.Find("." + HelperCls.navigators).FirstElementChild.ClassName); await Task.Delay(50);
            Assert.DoesNotContain("e-next e-hover-arrows ", cut.Find("." + HelperCls.navigators).LastElementChild.ClassName);
            Assert.Contains("e-previous-button", cut.Find("." + HelperCls.PreviousButton).QuerySelector("button").ClassName);
            Assert.Contains("e-next-button", cut.Find("." + HelperCls.NextButton).QuerySelector("button").ClassName);
            cut.Find("." + HelperCls.Carousel).MouseOut();
            Assert.Contains("e-previous e-hover-arrows", cut.Find("." + HelperCls.navigators).FirstElementChild.ClassName);
            Assert.Contains("e-next e-hover-arrows", cut.Find("." + HelperCls.navigators).LastElementChild.ClassName);
            cut.Instance.ButtonVisibility = CarouselButtonVisibility.Hidden;
            cut.Render(); await Task.Delay(100);
            Assert.Null(cut.Find(".e-carousel").QuerySelector(".e-carousel-navigators"));
        }
        [Fact(Timeout = 10000)]
        public async Task Height()
        {
            var cut = RenderComponent<CarouselPropertyChanges>();
            var carouselStyle = cut.Find("." + HelperCls.Carousel).GetAttribute("data-sf-style");
            Assert.Contains("100%", carouselStyle);
            cut.Instance.Height = "auto";
            cut.Render(); await Task.Delay(50);
            carouselStyle = cut.Find("." + HelperCls.Carousel).GetAttribute("data-sf-style");
            Assert.Contains("auto", carouselStyle);
            cut.Instance.Height = "350px";
            cut.Render(); await Task.Delay(100);
            carouselStyle = cut.Find("." + HelperCls.Carousel).GetAttribute("data-sf-style");
            Assert.Contains("350px", carouselStyle);
        }
        [Fact(Timeout = 10000)]
        public async Task Width()
        {
            var cut = RenderComponent<CarouselPropertyChanges>();
            var carouselStyle = cut.Find("." + HelperCls.Carousel).GetAttribute("data-sf-style");
            Assert.Contains("100%", carouselStyle);
            cut.Instance.Width = "auto";
            cut.Render(); await Task.Delay(50);
            carouselStyle = cut.Find("." + HelperCls.Carousel).GetAttribute("data-sf-style");
            Assert.Contains("auto", carouselStyle);
            cut.Instance.Width = "520px";
            cut.Render(); await Task.Delay(100);
            carouselStyle = cut.Find("." + HelperCls.Carousel).GetAttribute("data-sf-style");
            Assert.Contains("520px", carouselStyle);
        }
        [Fact(Timeout = 10000)]
        public async Task HtmlAttributes()
        {
            var cut = RenderComponent<HtmlAttributes>();
            var carousel = cut.Find("." + HelperCls.Carousel);
            Assert.NotNull(carousel);
            Assert.Contains("carousel-custom-css", carousel.GetAttribute("class"));
            Assert.Equal("CarouselAttributes", carousel.GetAttribute("id"));
            Assert.Equal("Slide show of slides", carousel.GetAttribute("aria-label"));

        }
        [Fact(Timeout = 30000)]
        public async Task Methods()
        {
            var cut = RenderComponent<Methods>();
            var carousel = cut.Find("." + HelperCls.Carousel);
            cut.FindAll(".e-btn")[0].Click();
            await Task.Delay(8000);
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[2].ClassName);
            await Task.Delay(5000);
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[3].ClassName);
            cut.FindAll(".e-btn")[1].Click();
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[3].ClassName);
            cut.FindAll(".e-btn")[2].Click();
            await Task.Delay(1000);
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[2].ClassName);
            cut.FindAll(".e-btn")[3].Click();
            await Task.Delay(1000);
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[3].ClassName);
            cut.FindAll(".e-btn")[4].Click();
            await Task.Delay(1000);
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
        }
        [Fact(Timeout = 10000)]
        public async Task KeyBoardInteraction()
        {
            var cut = RenderComponent<CarouselPropertyChanges>();
            Assert.Equal("polite", cut.Find(".e-carousel-items").GetAttribute("aria-live"));
            cut.FindAll(".e-checkbox")[0].Click(); await Task.Delay(50);
            Assert.Equal("off", cut.Find(".e-carousel-items").GetAttribute("aria-live"));
            cut.FindAll(".e-checkbox")[6].Click(); await Task.Delay(50);
            cut.Find("." + HelperCls.Carousel).KeyDown(new KeyboardEventArgs
            { Key = " ", Code = "Space", AltKey = true, ShiftKey = true, Type = "keydown" });
            Assert.Equal("polite", cut.Find(".e-carousel-items").GetAttribute("aria-live"));
            cut.Find("." + HelperCls.Carousel).KeyDown(new KeyboardEventArgs
            { Key = " ", Code = "ArrowLeft", AltKey = true, ShiftKey = true, Type = "keydown" });
            await Task.Delay(1000);
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[5].ClassName);
            cut.Find("." + HelperCls.Carousel).KeyDown(new KeyboardEventArgs
            { Key = " ", Code = "ArrowRight", AltKey = true, ShiftKey = true, Type = "keydown" });
            await Task.Delay(1000);
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
            cut.Find("." + HelperCls.Carousel).KeyDown(new KeyboardEventArgs
            { Key = "End", Code = "End", AltKey = true, ShiftKey = true, Type = "keydown" });
            await Task.Delay(1000);
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[5].ClassName);
            cut.Find("." + HelperCls.Carousel).KeyDown(new KeyboardEventArgs
            { Key = "Home", Code = "Home", AltKey = true, ShiftKey = true, Type = "keydown" });
            await Task.Delay(1000);
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
            cut.Find("." + HelperCls.Carousel).KeyDown(new KeyboardEventArgs
            { Key = "", Code = "", AltKey = true, ShiftKey = true, Type = "keydown" });
            await Task.Delay(1000);
        }
        [Fact(Timeout = 10000)]
        public async Task Navigations()
        {
            var cut = RenderComponent<CarouselPropertyChanges>();
            var nextButton = cut.Find("." + HelperCls.NextButton).QuerySelector("button");
            Assert.Contains("e-next-button", nextButton.ClassName);
            nextButton.Click(); await Task.Delay(1000);
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[2].ClassName);
            var prevButton = cut.Find("." + HelperCls.PreviousButton).QuerySelector("button");
            Assert.Contains("e-previous-button", prevButton.ClassName);
            prevButton.Click(); await Task.Delay(1000);
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
        }
        [Fact(Timeout = 10000)]
        public async Task CssClass()
        {
            var cut = RenderComponent<CarouselPropertyChanges>();
            Assert.Contains("e-carousel-transparent", cut.Find("." + HelperCls.Carousel).ClassName);
            cut.Instance.CssClass = "dark";
            cut.Render(); await Task.Delay(50);
            Assert.Contains("e-carousel-dark", cut.Find("." + HelperCls.Carousel).ClassName);
            cut.Instance.CssClass = "light"; 
            cut.Render(); await Task.Delay(200);
            Assert.Contains("e-carousel-light", cut.Find("." + HelperCls.Carousel).ClassName);
        }
        [Fact(Timeout = 10000)]
        public void ComponentsInsideCarousel()
        {
            var cut = RenderComponent<ComponentsInsideCarousel>();
            Assert.Contains("e-previous-button", cut.Find("." + HelperCls.PreviousButton).QuerySelector("button").ClassName);
            Assert.Contains("e-next-button", cut.Find("." + HelperCls.NextButton).QuerySelector("button").ClassName);
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
            Assert.NotNull(cut.Find(".e-carousel-indicators"));
            Assert.Contains("e-accordion", cut.FindAll("." + HelperCls.CarouselItem)[2].LastElementChild.FirstElementChild.ClassName);
            cut.FindAll(".e-indicator-bar")[1].Click();
            Assert.Contains("e-tab", cut.FindAll("." + HelperCls.CarouselItem)[3].LastElementChild.FirstElementChild.ClassName);
            cut.FindAll(".e-indicator-bar")[2].Click();
            Assert.Contains("e-toolbar", cut.FindAll("." + HelperCls.CarouselItem)[4].LastElementChild.FirstElementChild.ClassName);
            cut.FindAll(".e-indicator-bar")[3].Click();
            Assert.Contains("e-treeview", cut.FindAll("." + HelperCls.CarouselItem)[5].LastElementChild.FirstElementChild.ClassName);
            cut.FindAll(".e-indicator-bar")[4].Click();
            Assert.Contains("e-breadcrumb", cut.FindAll("." + HelperCls.CarouselItem)[6].LastElementChild.FirstElementChild.ClassName);
        }
        [Fact(Timeout = 10000)]
        public void DataBinding()
        {
            var cut = RenderComponent<DataBinding>();
            Assert.Equal(4, cut.FindAll("." + HelperCls.Slideindicator).Count);
            //Add Item
            cut.FindAll(".e-btn")[0].Click();
            Assert.Equal(5, cut.FindAll("." + HelperCls.Slideindicator).Count);
            //Add range
            cut.FindAll(".e-btn")[1].Click();
            Assert.Equal(7, cut.FindAll("." + HelperCls.Slideindicator).Count);
            //Add item in specified index
            cut.FindAll(".e-btn")[2].Click();
            Assert.Equal(8, cut.FindAll("." + HelperCls.Slideindicator).Count);
            //Remove item
            cut.FindAll(".e-btn")[3].Click();
            Assert.Equal(7, cut.FindAll("." + HelperCls.Slideindicator).Count);
            //Remove range
            cut.FindAll(".e-btn")[4].Click();
            Assert.Equal(5, cut.FindAll("." + HelperCls.Slideindicator).Count);
            //Remove item in specified index
            cut.FindAll(".e-btn")[5].Click();
            Assert.Equal(4, cut.FindAll("." + HelperCls.Slideindicator).Count);
            //show/hide
            Assert.Equal("San Francisco", cut.FindAll("." + HelperCls.CarouselItem)[1].FirstElementChild.LastElementChild.FirstElementChild.TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("0", cut.Find(".e-numerictextbox").GetAttribute("aria-valuenow"));
            cut.Find(".e-checkbox").Click();
            Assert.NotEqual("San Francisco", cut.FindAll("." + HelperCls.CarouselItem)[1].FirstElementChild.LastElementChild.FirstElementChild.TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("0", cut.Find(".e-numerictextbox").GetAttribute("aria-valuenow"));
            Assert.Equal(3, cut.FindAll("." + HelperCls.Slideindicator).Count);
            cut.Find(".e-checkbox").Click();
            Assert.Equal("London", cut.FindAll("." + HelperCls.CarouselItem)[2].FirstElementChild.LastElementChild.FirstElementChild.TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal(4, cut.FindAll("." + HelperCls.Slideindicator).Count);
            cut.Instance.CarouselIndex = 1;
            cut.Render();
            cut.Find(".e-checkbox").Click();
            Assert.NotEqual("London", cut.FindAll("." + HelperCls.CarouselItem)[2].FirstElementChild.LastElementChild.FirstElementChild.TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal(3, cut.FindAll("." + HelperCls.Slideindicator).Count);
            
        }
        [Fact]
        public async Task ItemPropertyChanges()
        {
            var cut = RenderComponent<ItemPropertyChanges> ();
            Assert.Contains("e-previous-button", cut.Find("." + HelperCls.PreviousButton).QuerySelector("button").ClassName);
            Assert.Contains("e-next-button", cut.Find("." + HelperCls.NextButton).QuerySelector("button").ClassName);
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
            Assert.NotNull(cut.Find(".e-carousel-indicators"));
            Assert.Contains("e-carousel-item-transparent", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
            Assert.Contains("e-carousel-item-transparent", cut.FindAll("." + HelperCls.CarouselItem)[2].ClassName);
            Assert.Contains("e-carousel-item-transparent", cut.FindAll("." + HelperCls.CarouselItem)[3].ClassName);
            Assert.Contains("e-carousel-item-transparent", cut.FindAll("." + HelperCls.CarouselItem)[4].ClassName);
            cut.Instance.OddItemCSS = "light";
            cut.Render();
            cut.FindAll(".e-indicator-bar")[0].Click(); await Task.Delay(200);
            Assert.Contains("e-carousel-item-light", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
            Assert.Contains("e-carousel-item-transparent", cut.FindAll("." + HelperCls.CarouselItem)[2].ClassName);
            Assert.Contains("e-carousel-item-light", cut.FindAll("." + HelperCls.CarouselItem)[3].ClassName);
            Assert.Contains("e-carousel-item-transparent", cut.FindAll("." + HelperCls.CarouselItem)[4].ClassName);
            cut.Instance.OddItemCSS = "dark";
            cut.Render(); cut.FindAll(".e-indicator-bar")[0].Click(); await Task.Delay(200);
            Assert.Contains("e-carousel-item-dark", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
            Assert.Contains("e-carousel-item-transparent", cut.FindAll("." + HelperCls.CarouselItem)[2].ClassName);
            Assert.Contains("e-carousel-item-dark", cut.FindAll("." + HelperCls.CarouselItem)[3].ClassName);
            Assert.Contains("e-carousel-item-transparent", cut.FindAll("." + HelperCls.CarouselItem)[4].ClassName);
            cut.Instance.EvenItemCSS = "light";
            cut.Render(); cut.FindAll(".e-indicator-bar")[0].Click(); await Task.Delay(200);
            Assert.Contains("e-carousel-item-dark", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
            Assert.Contains("e-carousel-item-light", cut.FindAll("." + HelperCls.CarouselItem)[2].ClassName);
            Assert.Contains("e-carousel-item-dark", cut.FindAll("." + HelperCls.CarouselItem)[3].ClassName);
            Assert.Contains("e-carousel-item-light", cut.FindAll("." + HelperCls.CarouselItem)[4].ClassName);
            cut.Instance.EvenItemCSS = "dark";
            cut.Render(); cut.FindAll(".e-indicator-bar")[0].Click(); await Task.Delay(200);
            Assert.Contains("e-carousel-item-dark", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
            Assert.Contains("e-carousel-item-dark", cut.FindAll("." + HelperCls.CarouselItem)[2].ClassName);
            Assert.Contains("e-carousel-item-dark", cut.FindAll("." + HelperCls.CarouselItem)[3].ClassName);
            Assert.Contains("e-carousel-item-dark", cut.FindAll("." + HelperCls.CarouselItem)[4].ClassName);

        }
        [Fact(Timeout = 10000)]
        public async Task Template()
        {
            var cut = RenderComponent<Template>();
            Assert.Contains("e-previous", cut.Find("." + HelperCls.navigators).FirstElementChild.ClassName);
            Assert.Contains("e-next", cut.Find("." + HelperCls.navigators).LastElementChild.ClassName);
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
            Assert.NotNull(cut.Find(".e-carousel-indicators"));
            Assert.NotNull(cut.Find(".e-play-pause"));
            Assert.Equal("Showing 1 of 5", cut.FindAll("." + HelperCls.CarouselItem)[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Showing 2 of 5", cut.FindAll("." + HelperCls.CarouselItem)[2].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Showing 3 of 5", cut.FindAll("." + HelperCls.CarouselItem)[3].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Showing 4 of 5", cut.FindAll("." + HelperCls.CarouselItem)[4].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Showing 5 of 5", cut.FindAll("." + HelperCls.CarouselItem)[5].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Contains("e-carousel-slide-animation", cut.Find("." + HelperCls.Carousel).ClassName);
            await Task.Delay(50);
            cut.Instance.AnimationEffect = CarouselAnimationEffect.Fade;
            cut.Render(); await Task.Delay(50);
            Assert.Contains("e-carousel-fade-animation", cut.Find("." + HelperCls.Carousel).ClassName);
            cut.Instance.AnimationEffect = CarouselAnimationEffect.None;
            cut.Render(); await Task.Delay(100);
            Assert.Contains("e-carousel-animation-none", cut.Find("." + HelperCls.Carousel).ClassName);
        }
        [Fact(Timeout = 10000)]
        public async Task ChangeSlide()
        {
            var cut = RenderComponent<ChangeSlide>();
            var carousel = cut.Find("." + HelperCls.Carousel);
            Assert.NotNull(carousel);
            Assert.Equal("false", cut.FindAll("." + HelperCls.CarouselItem)[1].GetAttribute("aria-hidden"));
            Assert.Equal("true", cut.FindAll("." + HelperCls.CarouselItem)[2].GetAttribute("aria-hidden"));
            Assert.Equal("true", cut.FindAll("." + HelperCls.CarouselItem)[3].GetAttribute("aria-hidden"));
            Assert.Equal("true", cut.FindAll("." + HelperCls.CarouselItem)[4].GetAttribute("aria-hidden"));
            Assert.Equal("true", cut.FindAll("." + HelperCls.CarouselItem)[5].GetAttribute("aria-hidden"));
            await cut.Instance.nextSlideChange();
            await Task.Delay(500);
            Assert.Equal("true", cut.FindAll("." + HelperCls.CarouselItem)[1].GetAttribute("aria-hidden"));
            Assert.Equal("false", cut.FindAll("." + HelperCls.CarouselItem)[2].GetAttribute("aria-hidden"));
            Assert.Equal("true", cut.FindAll("." + HelperCls.CarouselItem)[3].GetAttribute("aria-hidden"));
            Assert.Equal("true", cut.FindAll("." + HelperCls.CarouselItem)[4].GetAttribute("aria-hidden"));
            Assert.Equal("true", cut.FindAll("." + HelperCls.CarouselItem)[5].GetAttribute("aria-hidden"));
            await cut.Instance.previousSlideChange();
            await Task.Delay(500);
            Assert.Equal("false", cut.FindAll("." + HelperCls.CarouselItem)[1].GetAttribute("aria-hidden"));
            Assert.Equal("true", cut.FindAll("." + HelperCls.CarouselItem)[2].GetAttribute("aria-hidden"));
            Assert.Equal("true", cut.FindAll("." + HelperCls.CarouselItem)[3].GetAttribute("aria-hidden"));
            Assert.Equal("true", cut.FindAll("." + HelperCls.CarouselItem)[4].GetAttribute("aria-hidden"));
            Assert.Equal("true", cut.FindAll("." + HelperCls.CarouselItem)[5].GetAttribute("aria-hidden"));
            cut.Render();
            await cut.Instance.carousel.MoveToAsync(-1);
            await Task.Delay(1000);
            Assert.Contains("e-carousel-item", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
            Assert.Contains("e-carousel-item", cut.FindAll("." + HelperCls.CarouselItem)[2].ClassName);
            Assert.Contains("e-carousel-item", cut.FindAll("." + HelperCls.CarouselItem)[3].ClassName);
            Assert.Contains("e-carousel-item", cut.FindAll("." + HelperCls.CarouselItem)[4].ClassName);
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[5].ClassName);
            await cut.Instance.carousel.MoveToAsync(8);
            await Task.Delay(1000);
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
            Assert.Contains("e-carousel-item", cut.FindAll("." + HelperCls.CarouselItem)[2].ClassName);
            Assert.Contains("e-carousel-item", cut.FindAll("." + HelperCls.CarouselItem)[3].ClassName);
            Assert.Contains("e-carousel-item", cut.FindAll("." + HelperCls.CarouselItem)[4].ClassName);
            Assert.Contains("e-carousel-item", cut.FindAll("." + HelperCls.CarouselItem)[5].ClassName);

        }

        [Fact(Timeout = 10000)]
        public async Task Indicators()
        {
            var cut = RenderComponent<Indicators>();
            var carousel = cut.Find("." + HelperCls.Carousel);
            Assert.NotNull(carousel);
            var indicator = cut.Find(".e-carousel-indicators");
            Assert.Contains("e-default", indicator.ClassName);
            cut.Instance.indicatorsType = CarouselIndicatorsType.Dynamic; cut.Render();
            indicator = cut.Find(".e-carousel-indicators");
            Assert.Contains("e-dynamic", indicator.ClassName);
            cut.Instance.indicatorsType = CarouselIndicatorsType.Progress; cut.Render();
            indicator = cut.Find(".e-carousel-indicators");
            Assert.Contains("e-progress", indicator.ClassName);
            cut.Instance.indicatorsType = CarouselIndicatorsType.Fraction; cut.Render();
            indicator = cut.Find(".e-carousel-indicators");
            Assert.Contains("e-fraction", indicator.ClassName);

        }
        [Fact(Timeout = 10000)]
        public async Task PartialVisible()
        {
            var cut = RenderComponent<PartialVisible>();
            var carousel = cut.Find("." + HelperCls.Carousel);
            Assert.NotNull(carousel);
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
            Assert.Contains("e-carousel-item", cut.FindAll("." + HelperCls.CarouselItem)[2].ClassName);
            Assert.Contains("e-carousel-item", cut.FindAll("." + HelperCls.CarouselItem)[3].ClassName);
            Assert.Contains("e-carousel-item", cut.FindAll("." + HelperCls.CarouselItem)[4].ClassName);
            Assert.Contains("e-carousel-item", cut.FindAll("." + HelperCls.CarouselItem)[5].ClassName);

            cut.Instance.index = 8; cut.Render();
            Assert.Contains("e-carousel-item", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
            Assert.Contains("e-carousel-item", cut.FindAll("." + HelperCls.CarouselItem)[2].ClassName);
            Assert.Contains("e-carousel-item", cut.FindAll("." + HelperCls.CarouselItem)[3].ClassName);
            Assert.Contains("e-carousel-item", cut.FindAll("." + HelperCls.CarouselItem)[4].ClassName);
            Assert.Contains("e-carousel-item e-clone e-active", cut.FindAll("." + HelperCls.CarouselItem)[6].ClassName);
            cut.Instance.index = -1; cut.Render();
            Assert.Contains("e-carousel-item e-active", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
            Assert.Contains("e-carousel-item", cut.FindAll("." + HelperCls.CarouselItem)[2].ClassName);
            Assert.Contains("e-carousel-item", cut.FindAll("." + HelperCls.CarouselItem)[3].ClassName);
            Assert.Contains("e-carousel-item", cut.FindAll("." + HelperCls.CarouselItem)[4].ClassName);
            Assert.Contains("e-carousel-item", cut.FindAll("." + HelperCls.CarouselItem)[5].ClassName);

            Assert.DoesNotContain("e-partial", carousel.ClassName);
            cut.Instance.visible = true;
            cut.Render();
            await Task.Delay(1000);
            Assert.Contains("e-partial", carousel.ClassName);
            cut.Instance.CarouselRef.PreventRender(true);
            await Task.Delay(100);
        }

        [Fact(Timeout = 10000)]
        public async Task SelectIndex()
        {
            var cut = RenderComponent<PartialVisible>();
            var carousel = cut.Find("." + HelperCls.Carousel);
            Assert.NotNull(carousel);
            cut.Instance.index = 2; cut.Render();
            Assert.Contains("e-carousel-item", cut.FindAll("." + HelperCls.CarouselItem)[1].ClassName);
            Assert.Contains("e-carousel-item", cut.FindAll("." + HelperCls.CarouselItem)[2].ClassName);
            Assert.Contains("e-carousel-item", cut.FindAll("." + HelperCls.CarouselItem)[3].ClassName);
            Assert.Contains("e-carousel-item", cut.FindAll("." + HelperCls.CarouselItem)[4].ClassName);
            Assert.Contains("e-carousel-item", cut.FindAll("." + HelperCls.CarouselItem)[5].ClassName);
        }
        [Fact(DisplayName = "Test SelectedIndex Property Getter and Setter")]
        public void TestSelectedIndexProperty()
        {
            var indicatorsContext = new IndicatorsTemplateContext();
            int expectedSelectedIndex = 2;
            indicatorsContext.SelectedIndex = expectedSelectedIndex;
            Assert.Equal(expectedSelectedIndex, indicatorsContext.SelectedIndex);
        }
        [Fact(DisplayName = "Test AllowKeyboardInteraction Default State")]
        public void TestAllowKeyboardInteractionDefault()
        {
            var cut = RenderComponent<SfCarousel>();
            Assert.True(cut.Instance.AllowKeyboardInteraction);
        }

        [Fact(DisplayName = "Test AllowKeyboardInteraction Set and Get")]
        public void TestAllowKeyboardInteractionSet()
        {
            var cut = RenderComponent<SfCarousel>(parameters => parameters
                .Add(p => p.AllowKeyboardInteraction, false));
            Assert.False(cut.Instance.AllowKeyboardInteraction);
        }

        [Fact(DisplayName = "Test SwipeMode Default State")]
        public void TestSwipeModeDefault()
        {
            var cut = RenderComponent<SfCarousel>();
            Assert.Equal(CarouselSwipeMode.Touch, cut.Instance.SwipeMode);
        }

        [Fact(DisplayName = "Test SwipeMode Set and Get")]
        public void TestSwipeModeSet()
        {
            var cut = RenderComponent<SfCarousel>(parameters => parameters
                .Add(p => p.SwipeMode, CarouselSwipeMode.Mouse));
            Assert.Equal(CarouselSwipeMode.Mouse, cut.Instance.SwipeMode);
        }

        [Fact(DisplayName = "Test HtmlAttributes Property Set and Get")]
        public void TestHtmlAttributes()
        {
            var customAttributes = new Dictionary<string, object>
        {
            { "aria-label", "Slide show of current News" },
            { "role", "region" }
        };
            var cut = RenderComponent<SfCarousel>(parameters => parameters
                .Add(p => p.HtmlAttributes, customAttributes));
            Assert.Equal(customAttributes, cut.Instance.HtmlAttributes);
            var carouselElement = cut.Find("div[aria-label=\"Slide show of current News\"][role=\"region\"]");
            Assert.NotNull(carouselElement);
        }
    }

}