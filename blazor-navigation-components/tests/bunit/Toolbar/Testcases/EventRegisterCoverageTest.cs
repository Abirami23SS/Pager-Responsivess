using System;
using Bunit;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Navigations.Internal;
using Syncfusion.Blazor.Tests.Toolbar.Samples.EventRegisterCoverage;
using Xunit;

namespace Syncfusion.Blazor.Tests.Toolbar
{
    public class EventRegisterCoverageTest : BunitTestContext
    {
        [Fact]
        public void EventRegisterAsync_WithValidNameAndHandler_RendersSuccessfully()
        {
            var cut = RenderComponent<ToolbarEventRegisterAsyncDefault>();
            Assert.NotNull(cut);
        }

        [Fact]
        public void EventRegisterAsync_NameNull_ThrowsMissingMemberException()
        {
            var exception = Assert.Throws<MissingMemberException>(() =>
            {
                var cut = RenderComponent<EventRegisterAsyncNullName>();
            });
            Assert.Contains("Name must be provided to add event", exception.Message);
        }

        [Fact]
        public void EventRegisterAsync_ParentNull_DoesNotThrow()
        {
            // When Parent is null, OnInitialized returns early without throwing or adding handler
            var cut = RenderComponent<EventRegisterAsyncParentNull>();
            Assert.NotNull(cut);
        }

        [Fact]
        public void EventRegisterAsync_WithHandlerRegistered_AccessesHandlerProperty()
        {
            var cut = RenderComponent<ToolbarEventRegisterAsyncDefault>();
            Assert.NotNull(cut);
        }
    }
}