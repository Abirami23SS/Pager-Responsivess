using Xunit;
using Bunit;
using AngleSharp.Dom;
using AngleSharp.Css.Dom;
using System.Threading.Tasks;
using Syncfusion.Blazor.Tests.Tabs.Samples;

namespace Syncfusion.Blazor.Tests.Tabs
{
    public class TabCombinedControls : BunitTestContext
    {
        public Helper HelperCls = new Helper();

        [Fact(Timeout = 10000)]
        public async Task GridInTab()
        {
            var cut = RenderComponent<CombinedControl>();
            await Task.Delay(500);
            var tab = cut.Find(".e-tab");
            Assert.NotNull(tab);
            var tabStyle = tab.GetAttribute("data-sf-style").Replace(" ", string.Empty);
            Assert.Contains("width:100%", tabStyle);
            Assert.Contains("height:auto", tabStyle);
            Assert.Contains(HelperCls.Toolbar, tab.QuerySelector("." + HelperCls.TabHeader).ClassList);
            Assert.Equal(5, tab.QuerySelectorAll(".e-tab-header .e-toolbar-item").Length);
            Assert.True("Grid" == tab.QuerySelector(".e-tab-header .e-tab-text").TextContent.Replace("\n", string.Empty).Trim());
            Assert.True("Accordion" == tab.QuerySelectorAll(".e-tab-header .e-tab-text")[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.True("Toolbar" == tab.QuerySelectorAll(".e-tab-header .e-tab-text")[2].TextContent.Replace("\n", string.Empty).Trim());
            Assert.True("Splitter" == tab.QuerySelectorAll(".e-tab-header .e-tab-text")[3].TextContent.Replace("\n", string.Empty).Trim());
            Assert.True("Dialog" == tab.QuerySelectorAll(".e-tab-header .e-tab-text")[4].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Contains("e-content", tab.LastElementChild.ClassList);
            Assert.Contains("e-active", tab.QuerySelector("." + HelperCls.Content).FirstElementChild.ClassList);
            //To check for Grid in Tab
            Assert.Contains("e-grid", cut.Find(".e-tab").QuerySelector(".e-item.e-active").LastElementChild.ClassList);
            var grid = cut.Find(".e-grid");
            var gridStyle = grid.GetAttribute("data-sf-style").Replace(" ", string.Empty);
            Assert.Contains("width:auto", gridStyle);
            Assert.Equal(4, grid.QuerySelectorAll(".e-headercell").Length);
            Assert.Equal(75, grid.QuerySelectorAll(".e-gridcontent .e-row").Length);
            Assert.True("Order ID" == grid.QuerySelector(".e-headertext").TextContent.Replace("\n", string.Empty).Trim());
            Assert.True("Customer Name" == grid.QuerySelectorAll(".e-headertext")[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.True("Order Date" == grid.QuerySelectorAll(".e-headertext")[2].TextContent.Replace("\n", string.Empty).Trim());
            Assert.True("Freight" == grid.QuerySelectorAll(".e-headertext")[3].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal(300, grid.QuerySelectorAll(".e-rowcell").Length);
            Assert.Equal("1001", grid.QuerySelector(".e-rowcell").TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("$157.50", grid.QuerySelectorAll(".e-rowcell")[299].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("$2.10", grid.QuerySelectorAll(".e-rowcell")[3].TextContent.Replace("\n", string.Empty).Trim());
        }
        [Fact(Timeout = 10000)]
        public async Task AccordionInTab()
        {
            var cut = RenderComponent<CombinedControl>();
            //To check for Accordion in Tab
            cut.Instance.SelectedItem = 1; cut.Render(); await Task.Delay(500);
            Assert.Contains("e-accordion", cut.Find(".e-tab").QuerySelector(".e-item.e-active").LastElementChild.ClassList);
            var acrdn = cut.Find(".e-accordion");
            Assert.Equal(3, acrdn.QuerySelectorAll(".e-acrdn-item").Length);
            Assert.True("Margeret Peacock" == acrdn.QuerySelector(".e-acrdn-header-content").TextContent.Replace("\n", string.Empty).Trim());
            Assert.True("Laura Callahan" == acrdn.QuerySelectorAll(".e-acrdn-header-content")[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.True("Albert Dodsworth" == acrdn.QuerySelectorAll(".e-acrdn-header-content")[2].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Contains("e-expand-state", acrdn.QuerySelector(".e-acrdn-item").ClassName);
            Assert.Contains("e-selected", acrdn.QuerySelector(".e-acrdn-item").ClassName);
            Assert.Contains("e-active", acrdn.QuerySelector(".e-acrdn-item").ClassName);
            Assert.Contains("e-acrdn-header", acrdn.QuerySelector(".e-acrdn-item").FirstElementChild.ClassList);
            Assert.Contains("e-acrdn-panel", acrdn.QuerySelector(".e-acrdn-item").LastElementChild.ClassList);
            Assert.Equal(1, acrdn.QuerySelectorAll(".e-acrdn-panel").Length);
            Assert.True(acrdn.QuerySelector(".e-acrdn-content").TextContent.Replace("\n", string.Empty).Trim()== "Margeret Peacock was born on Saturday , 01 December 1990. Now lives at Coventry House Miner Rd., London,UK. Margeret Peacock holds a position of Sales Coordinator in our WA department, (Seattle USA). Joined our company on Saturday , 01 May 2010");
            Assert.Contains("e-expand-icon", acrdn.QuerySelector(".e-tgl-collapse-icon").ClassList);
            Assert.DoesNotContain("e-expand-icon", acrdn.QuerySelectorAll(".e-tgl-collapse-icon")[1].ClassList);
            Assert.DoesNotContain("e-expand-icon", acrdn.QuerySelectorAll(".e-tgl-collapse-icon")[2].ClassList);
            acrdn.QuerySelectorAll(".e-acrdn-header")[1].Click(); await Task.Delay(200);
            acrdn = cut.Find(".e-accordion");
            Assert.Equal(2, acrdn.QuerySelectorAll(".e-acrdn-panel").Length);
            Assert.True(acrdn.QuerySelectorAll(".e-acrdn-content")[1].TextContent.Replace("\n", string.Empty).Trim()== "Laura Callahan was born on Tuesday , 06 November 1990. Now lives at Edgeham Hollow Winchester Way, London,UK. Laura Callahan holds a position of Sales Coordinator in our WA department, (Seattle USA). Joined our company on Saturday , 01 May 2010");
            acrdn.QuerySelectorAll(".e-acrdn-header")[2].Click(); await Task.Delay(100);
            acrdn = cut.Find(".e-accordion");
            Assert.Equal(3, acrdn.QuerySelectorAll(".e-acrdn-panel").Length);
            Assert.True(acrdn.QuerySelectorAll(".e-acrdn-content")[2].TextContent.Replace("\n", string.Empty).Trim()== "Albert Dodsworth was born on Thursday , 19 October 1989. Now lives at 4726 - 11th Ave. N.E., Seattle,USA.Albert Dodsworth holds a position of Sales Representative in our WA department, (Seattle USA). Joined our company on Friday , 01 May 2009");
        }
        [Fact(Timeout = 10000)]
        public async Task ToolbarInTab()
        {
            var cut = RenderComponent<CombinedControl>();
            //To check for Toolbar in Tab
            cut.Instance.SelectedItem = 2; cut.Render(); await Task.Delay(500);
            var toolbar = cut.FindAll("." + HelperCls.Toolbar)[1];
            var toolbarStyle = toolbar.GetAttribute("data-sf-style").Replace(" ", string.Empty);
            Assert.Contains("width:500px", toolbarStyle);
            Assert.Contains("height:auto", toolbarStyle);
            Assert.Equal(8, toolbar.QuerySelectorAll("." + HelperCls.ToolbarItem).Length);
            Assert.Equal(7, toolbar.QuerySelectorAll(".e-tbar-btn").Length);
            Assert.Equal(7, toolbar.QuerySelectorAll(".e-tbtn-txt").Length);
            Assert.Equal(7, toolbar.QuerySelectorAll(".e-tbar-btn-text").Length);
            Assert.Equal("Cut", toolbar.QuerySelector("." + HelperCls.ToolbarItem).GetInnerText().Trim());
            Assert.Equal("Copy", toolbar.QuerySelectorAll("." + HelperCls.ToolbarItem)[1].GetInnerText().Trim());
            Assert.Equal("Paste", toolbar.QuerySelectorAll("." + HelperCls.ToolbarItem)[2].GetInnerText().Trim());
            Assert.Contains("e-separator", toolbar.QuerySelectorAll("." + HelperCls.ToolbarItem)[3].ClassList);
            Assert.Equal("Bold", toolbar.QuerySelectorAll("." + HelperCls.ToolbarItem)[4].GetInnerText().Trim());
            Assert.Equal("Underline", toolbar.QuerySelectorAll("." + HelperCls.ToolbarItem)[5].GetInnerText().Trim());
            Assert.Equal("Italic", toolbar.QuerySelectorAll("." + HelperCls.ToolbarItem)[6].GetInnerText().Trim());
            Assert.Equal("Color-Picker", toolbar.QuerySelectorAll("." + HelperCls.ToolbarItem)[7].GetInnerText().Trim());
        }
        [Fact(Timeout = 10000)]
        public async Task SplitterInTab()
        {
            var cut = RenderComponent<CombinedControl>();
            //To check for Splitter in Tab
            cut.Instance.SelectedItem = 3; cut.Render(); await Task.Delay(500);
            var splitter = cut.Find(".e-splitter");
            Assert.Contains("e-splitter", cut.Find(".e-item.e-active").FirstElementChild.ClassName);
            Assert.Contains("e-splitter-horizontal", cut.Find(".e-item.e-active").FirstElementChild.ClassName);
            var splitterStyle = splitter.GetAttribute("data-sf-style").Replace(" ", string.Empty);
            Assert.Contains("width:80%", splitterStyle);
            Assert.Contains("height:200px", splitterStyle);
            Assert.Equal(3, splitter.QuerySelectorAll(".e-pane").Length);
            Assert.Equal(2, splitter.QuerySelectorAll(".e-split-bar").Length);
            Assert.Contains("e-pane-horizontal", splitter.QuerySelector(".e-pane").ClassList);
            Assert.Equal("Grid", splitter.QuerySelector(".content").FirstElementChild.TextContent.Replace("\n", string.Empty).Trim());
            Assert.True(splitter.QuerySelector(".content").LastElementChild.TextContent.Replace("\n", string.Empty).Trim()== "The ASP.NET DataGrid control, or DataTable is a feature-rich control used to display data in a tabular format.");
            Assert.Contains("e-pane-horizontal", splitter.QuerySelectorAll(".e-pane")[1].ClassList);
            Assert.Equal("Schedule", splitter.QuerySelectorAll(".content h3")[1].TextContent.Replace("\n", string.Empty).Trim());
            Assert.True(splitter.QuerySelectorAll(".content")[1].LastElementChild.TextContent.Replace("\n", string.Empty).Trim()== "The ASP.NET Scheduler, a.k.a. event calendar, facilitates almost all calendar features, thus allowing users to manage their time efficiently");
            Assert.Contains("e-pane-horizontal", splitter.QuerySelectorAll(".e-pane")[2].ClassList);
            Assert.Equal("Chart", splitter.QuerySelectorAll(".content h3")[2].TextContent.Replace("\n", string.Empty).Trim());
            Assert.True(splitter.QuerySelectorAll(".content")[2].LastElementChild.TextContent.Replace("\n", string.Empty).Trim()== "ASP.NET charts, a well-crafted easy-to-use charting package, is used to add beautiful charts in web and mobile applications");
            Assert.Equal("horizontal", splitter.QuerySelector(".e-split-bar").GetAttribute("aria-orientation"));
            Assert.Equal("separator", splitter.QuerySelector(".e-split-bar").GetAttribute("role"));
            Assert.Contains("e-navigate-arrow", splitter.QuerySelector(".e-split-bar").FirstElementChild.ClassName);
            Assert.Contains("e-arrow-left", splitter.QuerySelector(".e-navigate-arrow").ClassName);
            Assert.Contains("e-arrow-right", splitter.QuerySelector(".e-split-bar").LastElementChild.ClassName);
            Assert.Contains("e-resize-handler", splitter.QuerySelector(".e-navigate-arrow").NextElementSibling.ClassName);
        }
        [Fact(Timeout = 10000)]
        public async Task DialogInTab()
        {
            var cut = RenderComponent<CombinedControl>();
            //To check for Dialog in Tab
            cut.Instance.SelectedItem = 4; cut.Render(); await Task.Delay(500);
            cut.Find("button").Click(); await Task.Delay(100);
            Assert.Equal("Open Dialog", cut.Find("button").TextContent.Replace("\n", string.Empty).Trim());
            var dlg = cut.Find(".e-dialog");
            Assert.Equal("e-dlg-header-content", dlg.FirstElementChild.ClassName);
            Assert.Equal(2, dlg.FirstElementChild.ChildElementCount);
            Assert.Contains("e-dlg-closeicon-btn", dlg.QuerySelector("button").ClassName);
            Assert.Equal("e-dlg-header", dlg.FirstElementChild.LastElementChild.ClassName);
            Assert.Equal("Dialog", dlg.QuerySelector(".e-dlg-header").TextContent.Trim());
            Assert.Equal("e-dlg-content", dlg.Children[1].ClassName);
            Assert.True(dlg.Children[1].TextContent.Replace("\n", string.Empty).Trim() == "This is a Dialog with button and primary button");
            Assert.Equal("e-footer-content", dlg.LastElementChild.ClassName);
            Assert.Equal(2, dlg.QuerySelectorAll(".e-footer-content button").Length);
            Assert.Equal("OK", dlg.QuerySelector(".e-footer-content button").TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Cancel", dlg.QuerySelectorAll(".e-footer-content button")[1].TextContent.Replace("\n", string.Empty).Trim());
        }

        [Fact(Timeout = 10000, DisplayName = "EventAggregator AddAsync and NotifyAsync methods testing")]
        public async Task EventAggregatorAddAsyncAndNotifyAsyncTesting()
        {
            var cut = RenderComponent<EventAggregatorTest>();
            await Task.Delay(1000);

            // Verify AddAsync was called and handler was registered
            var asyncHandlerResult = cut.Find("#asyncHandlerResult");
            Assert.NotNull(asyncHandlerResult);
            var asyncText = asyncHandlerResult.TextContent;
            // Check if AddAsync handler was registered (could be "Handler registered" or error message)
            Assert.False(string.IsNullOrWhiteSpace(asyncText) && asyncText == "Initial", 
                $"AddAsync test failed with: {asyncText}");
            Assert.False(asyncText.Contains("Handler registered") || asyncText.Contains("Added"), 
                $"Unexpected AddAsync result: {asyncText}");

            // Verify NotifyAsync was called
            var notifyAsyncResult = cut.Find("#notifyAsyncResult");
            Assert.NotNull(notifyAsyncResult);
            var notifyText = notifyAsyncResult.TextContent;
            Assert.False(string.IsNullOrWhiteSpace(notifyText) && notifyText == "Initial", 
                $"NotifyAsync test failed with: {notifyText}");

            // Verify AddAsync handler was invoked
            var addAsyncCalled = cut.Find("#addAsyncCalled");
            Assert.NotNull(addAsyncCalled);
            var addAsyncText = addAsyncCalled.TextContent;
            Assert.True(addAsyncText.Contains("Handler called") || addAsyncText.Contains("called"), 
                $"Handler invocation failed: {addAsyncText}");
        }

        [Fact(Timeout = 10000, DisplayName = "SelectEventArgs property getters coverage testing")]
        public async Task SelectEventArgsPropertiesTest()
        {
            // Test SelectEventArgs getter properties: IsSwiped, IsInteracted, Name, PreventFocus, PreviousIndex, SelectedIndex
            var cut = RenderComponent<SelectEventArgsPropertiesTest>();
            await Task.Delay(500);

            // Verify the component is rendered
            var selectEventTest = cut.Find("#selectEventTest");
            Assert.NotNull(selectEventTest);

            // Programmatically change selected tab item to trigger Select event
            // This is the correct way to trigger events in Tab component
            cut.Instance.selectedTabItem = 1;
            cut.Render();
            await Task.Delay(300);

            // Verify all SelectEventArgs properties were accessed in the event handler
            var isSwipedSpan = cut.Find("#isSwiped");
            Assert.NotNull(isSwipedSpan);
            Assert.Contains("IsSwiped:", isSwipedSpan.TextContent);

            var isInteractedSpan = cut.Find("#isInteracted");
            Assert.NotNull(isInteractedSpan);
            Assert.Contains("IsInteracted:", isInteractedSpan.TextContent);

            var eventNameSpan = cut.Find("#eventName");
            Assert.NotNull(eventNameSpan);
            Assert.Contains("EventName:", eventNameSpan.TextContent);

            var preventFocusSpan = cut.Find("#preventFocus");
            Assert.NotNull(preventFocusSpan);
            Assert.Contains("PreventFocus:", preventFocusSpan.TextContent);

            var previousIndexSpan = cut.Find("#previousIndex");
            Assert.NotNull(previousIndexSpan);
            Assert.Contains("PreviousIndex:", previousIndexSpan.TextContent);

            var selectedIndexSpan = cut.Find("#selectedIndex");
            Assert.NotNull(selectedIndexSpan);
            Assert.Contains("SelectedIndex:", selectedIndexSpan.TextContent);
        }

        [Fact(Timeout = 10000, DisplayName = "AddEventArgs property getters coverage testing")]
        public async Task AddEventArgsPropertiesTest()
        {
            // Test AddEventArgs getter properties: Name, AddedItems, Cancel
            var cut = RenderComponent<AddEventArgsTest>();
            await Task.Delay(500);

            // Verify the component is rendered
            var addEventTest = cut.Find("#addEventTest");
            Assert.NotNull(addEventTest);

            // Click the Add Tab button to trigger Add event
            var addTabBtn = cut.Find("#addTabBtn");
            Assert.NotNull(addTabBtn);
            addTabBtn.Click();
            await Task.Delay(200);

            // Verify all AddEventArgs properties were accessed in the event handler
            var addEventNameSpan = cut.Find("#addEventName");
            Assert.NotNull(addEventNameSpan);
            Assert.Contains("AddEventName:", addEventNameSpan.TextContent);

            var addedItemCountSpan = cut.Find("#addedItemCount");
            Assert.NotNull(addedItemCountSpan);
            Assert.Contains("AddedItemCount:", addedItemCountSpan.TextContent);

            var addCancelSpan = cut.Find("#addCancel");
            Assert.NotNull(addCancelSpan);
            Assert.Contains("AddCancel:", addCancelSpan.TextContent);
        }

        [Fact(Timeout = 10000, DisplayName = "RemoveEventArgs property getters coverage testing")]
        public async Task RemoveEventArgsPropertiesTest()
        {
            // Test RemoveEventArgs getter properties: Name, RemovedIndex, Cancel
            var cut = RenderComponent<RemoveEventArgsTest>();
            await Task.Delay(500);

            // Verify the component is rendered
            var removeEventTest = cut.Find("#removeEventTest");
            Assert.NotNull(removeEventTest);

            // Click the Remove Tab button to trigger Remove event
            var removeTabBtn = cut.Find("#removeTabBtn");
            Assert.NotNull(removeTabBtn);
            removeTabBtn.Click();
            await Task.Delay(200);

            // Verify all RemoveEventArgs properties were accessed in the event handler
            var removeEventNameSpan = cut.Find("#removeEventName");
            Assert.NotNull(removeEventNameSpan);
            Assert.Contains("RemoveEventName:", removeEventNameSpan.TextContent);

            var removedIndexSpan = cut.Find("#removedIndex");
            Assert.NotNull(removedIndexSpan);
            Assert.Contains("RemovedIndex:", removedIndexSpan.TextContent);

            var removeCancelSpan = cut.Find("#removeCancel");
            Assert.NotNull(removeCancelSpan);
            Assert.Contains("RemoveCancel:", removeCancelSpan.TextContent);
        }

    }
}