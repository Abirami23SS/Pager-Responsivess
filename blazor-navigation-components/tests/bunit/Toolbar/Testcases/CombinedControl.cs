using Xunit;
using Bunit;
using AngleSharp.Dom;
using AngleSharp.Css.Dom;
using System.Threading.Tasks;
using Syncfusion.Blazor.Tests.Toolbar.Samples;

namespace Syncfusion.Blazor.Tests.Toolbar
{
    public class ToolbarCombinedControls : BunitTestContext
    {
        public Helper HelperCls = new Helper();

        [Fact(Timeout = 10000)]
        public async Task CheckboxInToolbar()
        {
            var cut = RenderComponent<CombinedControl>();
            await Task.Delay(100);
            var toolbar = cut.Find("." + HelperCls.Toolbar);
            Assert.NotNull(toolbar);
            var toolbarStyle = toolbar.GetAttribute("data-sf-style").Replace(" ", string.Empty);
            Assert.Contains("width:650px", toolbarStyle);
            Assert.Contains("height:auto", toolbarStyle);
            Assert.Equal(4, cut.FindAll("." + HelperCls.ToolbarItem).Count);
            //To check for checkbox in toolbar
            Assert.False(cut.Find("." + HelperCls.ToolbarItem).IsDisabled());
            Assert.Contains(HelperCls.Template, cut.Find("." + HelperCls.ToolbarItem).ClassName);
            Assert.Equal("0", cut.Find("." + HelperCls.ToolbarItem).GetAttribute("data-index"));
            Assert.Contains("e-checkbox-wrapper", cut.Find("." + HelperCls.ToolbarItem).FirstElementChild.ClassName);
            var checkbox = cut.Find(".e-checkbox-wrapper");
            Assert.False(checkbox.IsDisabled());
            Assert.False(checkbox.IsChecked());
            Assert.Equal("Checkbox", checkbox.QuerySelector(".e-label").TextContent);
            Assert.Contains("e-icon", checkbox.QuerySelector("span").ClassName);
            checkbox.QuerySelector("input").Click(); await Task.Delay(50);
            checkbox = cut.Find(".e-checkbox-wrapper");
            Assert.Contains("e-check", checkbox.QuerySelector("span").ClassName);
        }
        [Fact(Timeout = 10000)]
        public async Task DropDownInToolbar()
        {
            var cut = RenderComponent<CombinedControl>();
            await Task.Delay(100);
            //To check for drop down in toolbar
            Assert.False(cut.FindAll("." + HelperCls.ToolbarItem)[1].IsDisabled());
            Assert.Contains(HelperCls.Template, cut.FindAll("." + HelperCls.ToolbarItem)[1].ClassName);
            Assert.Equal("1", cut.FindAll("." + HelperCls.ToolbarItem)[1].GetAttribute("data-index"));
            Assert.Contains("e-ddl", cut.FindAll("." + HelperCls.ToolbarItem)[1].FirstElementChild.ClassName);
            var dropdwn = cut.Find(".e-ddl");
            Assert.False(dropdwn.IsDisabled());
            Assert.False(dropdwn.IsFocused);
            Assert.Contains("e-dropdownlist", dropdwn.QuerySelector("input").ClassName);
            Assert.Equal("Select a game", dropdwn.QuerySelector("input").GetAttribute("placeholder"));
            Assert.Contains("e-ddl-icon", dropdwn.QuerySelector("span").ClassName);
            var buttonElement = cut.Find("#ButtonId");
            Assert.NotNull(buttonElement);
            buttonElement.Click();
            await Task.Delay(500);
            Assert.Equal(10, cut.FindAll(".e-dropdownbase li").Count);
            cut.Find(".e-dropdownbase li[data-value='" + "Game5" + "']").Click();
            Assert.Equal("Football", cut.Find(".e-dropdownlist").GetAttribute("value"));
        }
        [Fact(Timeout = 10000)]
        public async Task RadioButtonInToolbar()
        {
            var cut = RenderComponent<CombinedControl>();
            await Task.Delay(100);
            //To check for radio button in toolbar
            Assert.False(cut.FindAll("." + HelperCls.ToolbarItem)[2].IsDisabled());
            Assert.Contains(HelperCls.Template, cut.FindAll("." + HelperCls.ToolbarItem)[2].ClassName);
            Assert.Equal("2", cut.FindAll("." + HelperCls.ToolbarItem)[2].GetAttribute("data-index"));
            Assert.Contains("e-radio-wrapper", cut.FindAll("." + HelperCls.ToolbarItem)[2].FirstElementChild.ClassName);
            var radiobtn = cut.Find(".e-radio-wrapper");
            Assert.False(radiobtn.IsDisabled());
            Assert.False(radiobtn.IsChecked());
            Assert.Contains("e-radio", radiobtn.QuerySelector("input").ClassName);
            Assert.True("radio" == cut.Find(".e-radio").GetAttribute("type"));
            Assert.Equal("Radio button", radiobtn.QuerySelector(".e-label").TextContent);
            Assert.Null(radiobtn.GetAttribute("tabindex"));
        }
        [Fact(Timeout = 10000)]
        public async Task TextboxInToolbar()
        {
            var cut = RenderComponent<CombinedControl>();
            await Task.Delay(100);
            //To check for textbox in toolbar
            Assert.False(cut.FindAll("." + HelperCls.ToolbarItem)[3].IsDisabled());
            Assert.Contains(HelperCls.Template, cut.FindAll("." + HelperCls.ToolbarItem)[3].ClassName);
            Assert.Equal("3", cut.FindAll("." + HelperCls.ToolbarItem)[3].GetAttribute("data-index"));
            Assert.Contains("e-input-group", cut.FindAll("." + HelperCls.ToolbarItem)[3].FirstElementChild.ClassName);
            var txtbox = cut.FindAll("." + HelperCls.ToolbarItem)[3].FirstElementChild;
            Assert.False(txtbox.IsFocused);
            Assert.False(txtbox.IsDisabled());
            Assert.Contains("e-textbox", txtbox.QuerySelector("input").ClassName);
            Assert.True("text" == txtbox.FirstElementChild.GetAttribute("type"));
            Assert.Null(txtbox.FirstElementChild.GetAttribute("value"));
            Assert.True("Type something" == txtbox.FirstElementChild.GetAttribute("placeholder"));
        }

        [Fact(Timeout = 10000, DisplayName = "Toolbar Clicked event testing with ClickEventArgs properties")]
        public async Task ToolbarClickedEvent()
        {
            var cut = RenderComponent<ToolbarClickedEvent>();
            await Task.Delay(100);
            var outputSpan = cut.Find("span");
            Assert.NotNull(outputSpan);
            // Click the button to trigger the toolbar item click
            cut.Find("button").Click();
            await Task.Delay(200);
            var toolbarItems = cut.FindAll("." + HelperCls.ToolbarItem);
            Assert.NotEmpty(toolbarItems);
            // Click the first toolbar item to trigger Clicked event
            toolbarItems[0].Click();
            await Task.Delay(200);
            var clickOutput = outputSpan.TextContent;
            Assert.Contains("Clicked event", clickOutput);
            Assert.Contains("Item=", clickOutput);
            Assert.Contains("Name=", clickOutput);
        }

        [Fact(Timeout = 10000, DisplayName = "ToolbarEventArgs property getters coverage testing")]
        public async Task ToolbarEventArgsPropertiesTest()
        {
            // Test ToolbarEventArgs getter properties: TargetParentDataIndex, ToolbarItemIndex, IsPopupElement
            var cut = RenderComponent<ToolbarEventArgsPropertiesTest>();
            await Task.Delay(500);

            // Verify the component is rendered
            var toolbarEventTest = cut.Find("#toolbarEventTest");
            Assert.NotNull(toolbarEventTest);

            // Verify all ToolbarEventArgs properties are accessible
            var targetParentDataIndexSpan = cut.Find("#targetParentDataIndex");
            Assert.NotNull(targetParentDataIndexSpan);
            Assert.Contains("TargetParentDataIndex:", targetParentDataIndexSpan.TextContent);

            var toolbarItemIndexSpan = cut.Find("#toolbarItemIndex");
            Assert.NotNull(toolbarItemIndexSpan);
            Assert.Contains("ToolbarItemIndex:", toolbarItemIndexSpan.TextContent);

            var isPopupElementSpan = cut.Find("#isPopupElement");
            Assert.NotNull(isPopupElementSpan);
            Assert.Contains("IsPopupElement:", isPopupElementSpan.TextContent);
        }

        [Fact(Timeout = 10000, DisplayName = "EventRegisterAsync properties and OnInitialized coverage testing")]
        public async Task EventRegisterAsyncPropertiesTest()
        {
            // Test EventRegisterAsync properties: Name, Handler, Parent and OnInitialized method
            var cut = RenderComponent<EventRegisterAsyncTest>();
            await Task.Delay(500);

            // Verify the component is rendered
            var triggerBtn = cut.Find("#triggerClickBtn");
            Assert.NotNull(triggerBtn);

            // Verify ClickEventArgs properties are accessible through the event handler
            var clickEventNameSpan = cut.Find("#clickEventName");
            Assert.NotNull(clickEventNameSpan);
            Assert.Contains("ClickEventName:", clickEventNameSpan.TextContent);

            var clickEventItemSpan = cut.Find("#clickEventItem");
            Assert.NotNull(clickEventItemSpan);
            Assert.Contains("ClickEventItem:", clickEventItemSpan.TextContent);

            // Click the trigger button to fire the event and access all properties
            triggerBtn.Click();
            await Task.Delay(300);

            // Verify event was handled and properties were accessed
            var handledSpan = cut.Find("#clickEventHandled");
            Assert.NotNull(handledSpan);
            Assert.Contains("ClickEventHandled: True", handledSpan.TextContent);

            // Verify event name was captured
            Assert.Contains("ClickEventName: click", clickEventNameSpan.TextContent);

            // Verify item text was captured
            Assert.Contains("ClickEventItem: Cut", clickEventItemSpan.TextContent);
        }

        [Fact(Timeout = 10000, DisplayName = "ToolbarEventArgs property getters direct coverage testing")]
        public async Task ToolbarEventArgsPropertyGettersTest()
        {
            // Test ToolbarEventArgs property getters: TargetParentDataIndex, ToolbarItemIndex, IsPopupElement
            var cut = RenderComponent<ToolbarEventArgsAccessTest>();
            await Task.Delay(500);

            // Verify the component is rendered
            var testButton = cut.Find("#testButton");
            Assert.NotNull(testButton);

            // Click the test button to trigger property getter access
            testButton.Click();
            await Task.Delay(300);

            // Verify TargetParentDataIndex property getter was accessed and returned expected value
            var targetParentResult = cut.Find("#targetParentDataIndexResult");
            Assert.NotNull(targetParentResult);
            Assert.Contains("TargetParentDataIndex:", targetParentResult.TextContent);

            // Verify ToolbarItemIndex property getter was accessed and returned expected value
            var toolbarItemIdxResult = cut.Find("#toolbarItemIndexResult");
            Assert.NotNull(toolbarItemIdxResult);
            Assert.Contains("ToolbarItemIndex:", toolbarItemIdxResult.TextContent);

            // Verify IsPopupElement property getter was accessed and returned expected value
            var isPopupResult = cut.Find("#isPopupElementResult");
            Assert.NotNull(isPopupResult);
            Assert.Contains("IsPopupElement:", isPopupResult.TextContent);

            // Verify that specific property values are accessible
            Assert.Contains("TargetParentDataIndex: 2", targetParentResult.TextContent);
            Assert.Contains("ToolbarItemIndex: 3", toolbarItemIdxResult.TextContent);
            Assert.Contains("IsPopupElement: True", isPopupResult.TextContent);
        }
    }
}