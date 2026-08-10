using Bunit;
using Syncfusion.Blazor.Navigations;
using System.Threading.Tasks;
using Xunit;

namespace Syncfusion.Blazor.Tests.Tabs
{
    public class HeaderPositionTopWithInitContentAnimation : BunitTestContext
    {
        public Helper HelperCls = new();

        [Fact(Timeout = 10000, DisplayName = "TabAnimationPrevious - Default API value testing")]
        public async Task TabAnimationPrevious()
        {
            var tab = RenderComponent<SfTab>(options => options.Add(content => content.LoadOn, ContentLoad.Init).AddChildContent<TabAnimationSettings>(p => p.AddChildContent<TabAnimationPrevious>()));
            await Task.Delay(100);
            var previous = tab.FindComponent<TabAnimationPrevious>();
            Assert.Equal(600, previous.Instance.Duration);
            Assert.Equal("ease", previous.Instance.Easing);
            Assert.Equal(AnimationEffect.SlideLeftIn, previous.Instance.Effect);
        }

        [Fact(Timeout = 10000, DisplayName = "TabAnimationNext - Default API value testing")]
        public async Task TabAnimationNext()
        {
            var tab = RenderComponent<SfTab>(options => options.Add(content => content.LoadOn, ContentLoad.Init).AddChildContent<TabAnimationSettings>(p => p.AddChildContent<TabAnimationNext>()));
            await Task.Delay(100);
            var next = tab.FindComponent<TabAnimationNext>();
            Assert.Equal(600, next.Instance.Duration);
            Assert.Equal("ease", next.Instance.Easing);
            Assert.Equal(AnimationEffect.SlideRightIn, next.Instance.Effect);
        }
    }
}