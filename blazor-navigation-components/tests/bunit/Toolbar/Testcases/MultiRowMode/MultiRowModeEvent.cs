using Bunit;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Tests.Toolbar.Samples.MultiRowMode.Events;
using System.Threading.Tasks;
using Xunit;

namespace Syncfusion.Blazor.Tests.Toolbar
{
    public class MultiRowModeEvent : BunitTestContext
    {
        public Helper HelperCls = new();

        [Fact(Timeout = 10000, DisplayName = "Created event testing")]
        public async Task CreatedEvent()
        {
            var toolbar = RenderComponent<CreatedEvent>();
            await Task.Delay(100);
            Assert.True(toolbar.Find("." + HelperCls.Toolbar).FirstElementChild.ClassList.Contains(HelperCls.ToolbarItems));
            Assert.True(toolbar.Find("." + HelperCls.Toolbar).FirstElementChild.ClassList.Contains(HelperCls.ToolbarMultirow));
            toolbar.FindAll("br")[0].NextElementSibling.MarkupMatches("<span>Toolbar created event testing</span>");
        }
    }
}