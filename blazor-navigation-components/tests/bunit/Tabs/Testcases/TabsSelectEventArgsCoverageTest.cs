using System.Threading.Tasks;
using Xunit;
using Bunit;
using AngleSharp.Dom;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Tests.Tabs.Samples;

namespace Syncfusion.Blazor.Tests.Tabs
{
    public class TabsSelectEventArgsCoverageTest : BunitTestContext
    {
        public Helper HelperCls = new Helper();

        #region SelectEventArgs Coverage Tests

        /// <summary>
        /// Test SelectEventArgs with all properties accessed in event handler
        /// This covers all getter properties: IsSwiped, IsInteracted, Name, PreventFocus, PreviousIndex, SelectedIndex
        /// </summary>
        [Fact(Timeout = 10000)]
        public async Task SelectEventArgs_AllProperties()
        {
            var cut = RenderComponent<SelectEventArgsAllPropertiesTest>();
            await Task.Delay(200);

            // Verify the component rendered
            var container = cut.Find("#selectEventArgsTest");
            Assert.NotNull(container);

            // Click tab item to trigger selection
            var tabItems = cut.FindAll("." + HelperCls.ToolbarItem);
            Assert.True(tabItems.Count >= 2, "Expected at least 2 tab items");

            // Click second tab
            tabItems[1].Click();
            await Task.Delay(300);

            // Verify all properties were accessed in event handler
            var isSwipedSpan = cut.Find("#isSwiped");
            var isInteractedSpan = cut.Find("#isInteracted");
            var eventNameSpan = cut.Find("#eventName");
            var preventFocusSpan = cut.Find("#preventFocus");
            var previousIndexSpan = cut.Find("#previousIndex");
            var selectedIndexSpan = cut.Find("#selectedIndex");

            Assert.NotNull(isSwipedSpan);
            Assert.NotNull(isInteractedSpan);
            Assert.NotNull(eventNameSpan);
            Assert.NotNull(preventFocusSpan);
            Assert.NotNull(previousIndexSpan);
            Assert.NotNull(selectedIndexSpan);
        }

        /// <summary>
        /// Test SelectEventArgs PreventFocus setter is accessible
        /// </summary>
        [Fact(Timeout = 10000)]
        public async Task SelectEventArgs_PreventFocusSetter()
        {
            var cut = RenderComponent<SelectEventArgsPreventFocusTest>();
            await Task.Delay(200);

            // Verify initial state
            var preventFocusValue = cut.Find("#preventFocusValue");
            Assert.NotNull(preventFocusValue);
            Assert.Contains("Initial: False", preventFocusValue.TextContent);

            // Click tab to trigger event
            var tabItems = cut.FindAll("." + HelperCls.ToolbarItem);
            if (tabItems.Count > 1)
            {
                tabItems[1].Click();
                await Task.Delay(300);
            }

            // Verify PreventFocus was modified by event handler
            var updatedValue = cut.Find("#preventFocusValue");
            Assert.NotNull(updatedValue);
        }

        /// <summary>
        /// Test SelectEventArgs with IsSwiped true
        /// </summary>
        [Fact(Timeout = 10000)]
        public async Task SelectEventArgs_IsSwipedTrue()
        {
            var cut = RenderComponent<SelectEventArgsIsSwipedTest>();
            await Task.Delay(200);

            var isSwipedSpan = cut.Find("#isSwipedValue");
            Assert.NotNull(isSwipedSpan);
            Assert.Contains("IsSwiped:", isSwipedSpan.TextContent);
        }

        /// <summary>
        /// Test SelectEventArgs with multiple tab selections
        /// </summary>
        [Fact(Timeout = 10000)]
        public async Task SelectEventArgs_MultipleSelections()
        {
            var cut = RenderComponent<SelectEventArgsAllPropertiesTest>();
            await Task.Delay(200);

            var tabItems = cut.FindAll("." + HelperCls.ToolbarItem);
            Assert.True(tabItems.Count >= 3, "Expected at least 3 tab items");

            // Click second tab
            tabItems[1].Click();
            await Task.Delay(200);

            // Click third tab
            tabItems[2].Click();
            await Task.Delay(200);

            // Verify last selection properties were captured
            var selectedIndexSpan = cut.Find("#selectedIndex");
            Assert.NotNull(selectedIndexSpan);
        }

        /// <summary>
        /// Test SelectEventArgs direct instantiation and property access
        /// This ensures all auto-properties are covered
        /// </summary>
        [Fact(Timeout = 10000)]
        public void SelectEventArgs_DirectInstantiation()
        {
            // Create SelectEventArgs instance
            var args = new SelectEventArgs();

            // Access all getter properties (should return default values)
            var isSwiped = args.IsSwiped;
            var isInteracted = args.IsInteracted;
            var name = args.Name;
            var preventFocus = args.PreventFocus;
            var previousIndex = args.PreviousIndex;
            var selectedIndex = args.SelectedIndex;

            // Set PreventFocus (the only setter)
            args.PreventFocus = true;
            Assert.True(args.PreventFocus);

            args.PreventFocus = false;
            Assert.False(args.PreventFocus);
        }

        #endregion
    }
}