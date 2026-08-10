using Bunit;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Tests.Toolbar.Samples.ExtendedMode.Events;
using System.Threading.Tasks;
using Xunit;

namespace Syncfusion.Blazor.Tests.Toolbar
{
    public class ExtendedModeEvent : BunitTestContext
    {
        public Helper HelperCls = new();

        [Fact(Timeout = 10000, DisplayName = "Created event testing")]
        public async Task CreatedEvent()
        {
            var toolbar = RenderComponent<CreatedEvent>();
            await Task.Delay(100);
            toolbar.FindAll("br")[0].NextElementSibling.MarkupMatches("<span>Toolbar created event testing</span>");
        }
    }
}