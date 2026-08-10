using Bunit;
using Syncfusion.Blazor.Navigations;
using Xunit;

namespace Syncfusion.Blazor.Tests.Accordion
{
    public class SingleExpandModeAnimation : BunitTestContext
    {
        public Helper HelperCls = new();

        [Fact(Timeout = 10000, DisplayName = "AccordionAnimationCollapse - Default API value testing")]
        public void AccordionAnimationCollapse()
        {
            var accordion = RenderComponent<SfAccordion>(options => options.Add(mode => mode.ExpandMode, ExpandMode.Single).AddChildContent<AccordionAnimationSettings>(p => p.AddChildContent<AccordionAnimationCollapse>()));
            var collapse = accordion.FindComponent<AccordionAnimationCollapse>();
            Assert.Equal(400, collapse.Instance.Duration);
            Assert.Equal("linear", collapse.Instance.Easing);
            Assert.Equal(AnimationEffect.SlideUp, collapse.Instance.Effect);
        }

        [Fact(Timeout = 10000, DisplayName = "AccordionAnimationExpand - Default API value testing")]
        public void AccordionAnimationExpand()
        {
            var accordion = RenderComponent<SfAccordion>(options => options.Add(mode => mode.ExpandMode, ExpandMode.Single).AddChildContent<AccordionAnimationSettings>(p => p.AddChildContent<AccordionAnimationExpand>()));
            var expand = accordion.FindComponent<AccordionAnimationExpand>();
            Assert.Equal(400, expand.Instance.Duration);
            Assert.Equal("linear", expand.Instance.Easing);
            Assert.Equal(AnimationEffect.SlideDown, expand.Instance.Effect);
        }
    }
}