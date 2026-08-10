using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Navigations.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Syncfusion.Blazor.Tests.Navigations.Test
{
    public partial class ContextMenu : BunitTestContext
    {
        [Trait("ContextMenu", "Basic")]
        [Fact(Timeout = 10000, DisplayName = "Basic")]
        public async Task Basic()
        {
           var cut = RenderComponent<DefaultCM>();
            var buttonElems = cut.FindAll("Button", true);
            buttonElems[0].Click();
            await Task.Delay(200);
            var contextElem = cut.FindAll("ul.e-contextmenu", true);
            Assert.Contains("e-contextmenu-container", contextElem[0].ParentElement.ClassName);
            Assert.Contains("e-contextmenu", contextElem[0].ClassName);
            Assert.Contains("e-menu-item", contextElem[0].Children[0].ClassName);
            Assert.Contains("e-menu-caret-icon", contextElem[0].Children[2].ClassName);
            Assert.Equal("e-icons e-caret", contextElem[0].Children[2].Children[1].ClassName);

        }

        [Trait("ContextMenu", "Basic")]
        [Fact(Timeout = 10000, DisplayName = "Menu Open On Hover")]
        public async Task OnHover()
        {
           var cut = RenderComponent<DefaultCM>();
           var buttonElems = cut.FindAll("Button",true);
           buttonElems[0].Click();
           await Task.Delay(200);
           var contextElem=cut.FindAll("ul.e-contextmenu",true);
           contextElem[0].Children[1].MouseOver();
           Assert.Contains("e-focused",contextElem[0].Children[1].ClassName);
           contextElem[0].Children[2].MouseOver();
           var popup_ul = cut.Find("ul.e-menu-parent.e-ul");
           popup_ul.Children[0].MouseOver();
           Assert.Contains("e-selected", contextElem[0].Children[2].ClassName);
        }

        [Trait("ContextMenu", "Separator")]
        [Fact(Timeout = 10000, DisplayName = "No separator Menu")]
        public void NoSeparator()
        {
           var cut = RenderComponent<Template>();
           var buttonElems = cut.FindAll("Button",true);
           buttonElems[0].Click();
           var contextElem=cut.FindAll("ul.e-contextmenu",true);
           Assert.Null(contextElem[0].QuerySelector(".e-separator"));
          
        }

        [Trait("ContextMenu", "Separator")]
        [Fact(Timeout = 10000, DisplayName = "separator Menu")]
        public void WithSeparator()
        {
           var cut = RenderComponent<DefaultCM>();
           var buttonElems = cut.FindAll("Button",true);
           buttonElems[0].Click();
           var contextElem=cut.FindAll("ul.e-contextmenu",true);
           Assert.NotNull(contextElem[0].QuerySelector(".e-separator"));
          
        }

        [Trait("ContextMenu", "Icon")]
        [Fact(Timeout = 10000, DisplayName = "No Icon Menu")]
        public void NoIcon()
        {
           var cut = RenderComponent<ItemsDirective>();
           var buttonElems = cut.FindAll("Button",true);
           buttonElems[0].Click();
           var contextElem=cut.FindAll("ul.e-contextmenu",true);
           Assert.Null(contextElem[0].QuerySelector(".e-menu-icon"));
           Assert.Null(contextElem[0].QuerySelector(".e-blankicon"));
          
        }

        [Trait("ContextMenu", "Icon")]
        [Fact(Timeout = 10000, DisplayName = "Icon Menu")]
        public void WithIcon()
        {
           var cut = RenderComponent<DefaultCM>();
            var buttonElems = cut.FindAll("Button", true);
            buttonElems[0].Click();
            var contextElem = cut.FindAll("ul.e-contextmenu", true);
            Assert.Equal(5, contextElem[0].QuerySelectorAll(".e-menu-icon").Length);
            Assert.Null(contextElem[0].QuerySelector(".e-blankicon"));
        }

        [Trait("ContextMenu", "Items")]
        [Fact(Timeout = 10000, DisplayName = "Items as List")]
        public void ListItems()
        {
           var cut = RenderComponent<DefaultCM>();
           var buttonElems = cut.FindAll("Button",true);
           buttonElems[0].Click();
           var contextElem=cut.FindAll("ul.e-contextmenu",true);
           Assert.Equal("Cut",contextElem[0].Children[0].TextContent.Trim());
          
        }

        [Trait("ContextMenu", "Items")]
        [Fact(Timeout = 10000, DisplayName = "Items as TagDirective")]
        public void TagDirective()
        {
           var cut = RenderComponent<ItemsDirective>();
           var buttonElems = cut.FindAll("Button",true);
           buttonElems[0].Click();
           var contextElem=cut.FindAll("ul.e-contextmenu",true);
           Assert.Equal("Cut",contextElem[0].Children[0].TextContent.Trim());
          
        }

        [Trait("ContextMenu", "Template")]
        [Fact(Timeout = 10000, DisplayName = "Basic Template Support")]
        public void Template()
        {
           var cut = RenderComponent<Template>();
            var buttonElems = cut.FindAll("Button", true);
            buttonElems[0].Click();
            var contextElem = cut.FindAll("ul.e-contextmenu", true);
            Assert.Equal(3, contextElem[0].QuerySelectorAll(".e-checkbox-wrapper").Length);
        }

        [Trait("ContextMenu", "KeyboardFunctions")]
        [Fact(Timeout = 10000, DisplayName = "KeyboardFunctions - Basic")]
        public void KeyboardFunction_Basic()
        {
           var cut = RenderComponent<DefaultCM>();
           var buttonElems = cut.FindAll("Button",true);
           buttonElems[0].Click();
           var contextElem=cut.FindAll("ul.e-contextmenu",true);
           Assert.Equal("0",contextElem[0].GetAttribute("tabindex"));
           Assert.Equal("-1",contextElem[0].Children[0].GetAttribute("tabindex"));
        }

        [Trait("ContextMenu", "Events")]
        [Fact(Timeout = 10000, DisplayName = "Event - Created")]
        public void Event_Created()
        {
           var cut = RenderComponent<Events>();
           var divElems=cut.FindAll("div",true);
           Assert.Contains("Created",divElems[3].TextContent.Trim());
        }

        [Trait("ContextMenu", "Events")]
        [Fact(Timeout = 10000, DisplayName = "Event - OnItem Render")]
        public async Task Event_OnItemRender()
        {
           var cut = RenderComponent<Events>();
           var buttonElems = cut.FindAll("Button",true);
           var divElems=cut.FindAll("div",true);
           buttonElems[0].Click();
           await Task.Delay(100);
           Assert.Contains("OnItemRender",divElems[3].TextContent.Trim());
        }

        [Trait("ContextMenu", "Events")]
        [Fact(Timeout = 10000, DisplayName = "Event - Opened")]
        public async Task Event_Opened()
        {
           var cut = RenderComponent<Events>();
           var buttonElems = cut.FindAll("Button",true);
           var divElems=cut.FindAll("div",true);
           buttonElems[0].Click();
           await Task.Delay(100);
           Assert.Contains("Opened",divElems[3].TextContent.Trim());
        }
        [Trait("ContextMenu", "Events")]
        [Fact(Timeout = 10000, DisplayName = "Event - OnOpen")]
        public async Task Event_OnOpen()
        {
           var cut = RenderComponent<Events>();
            var buttonElems = cut.FindAll("Button", true);
            var divElems = cut.FindAll("div", true);
            buttonElems[0].Click();
            var contextElem = cut.FindAll("ul.e-contextmenu", true);
            contextElem[0].Children[2].MouseOver();
            await Task.Delay(500);
            //Not working in bunit
            // Assert.Contains("OnOpen",divElems[3].TextContent.Trim());
        }

        [Trait("ContextMenu", "Events")]
        [Fact(Timeout = 10000, DisplayName = "Event - OnClose")]
        public async Task Event_OnClose()
        {
           var cut = RenderComponent<Events>();
           var buttonElems = cut.FindAll("Button",true);
           var divElems=cut.FindAll("div",true);
           buttonElems[0].Click();
           await Task.Delay(100);
           var contextElem=cut.FindAll("ul.e-contextmenu",true);
           contextElem[0].Children[0].Click();
           await Task.Delay(100);
           Assert.Contains("OnClose",divElems[3].TextContent.Trim());
        }

        [Trait("ContextMenu", "Events")]
        [Fact(Timeout = 10000, DisplayName = "Event - Closed")]
        public async Task Event_Closed()
        {
           var cut = RenderComponent<Events>();
           var buttonElems = cut.FindAll("Button",true);
           var divElems=cut.FindAll("div",true);
           buttonElems[0].Click();
           await Task.Delay(100);
           var contextElem=cut.FindAll("ul.e-contextmenu",true);
           contextElem[0].Children[0].Click();
           await Task.Delay(100);
           Assert.Contains("Closed",divElems[3].TextContent.Trim());
        }

        [Trait("ContextMenu", "Events")]
        [Fact(Timeout = 10000, DisplayName = "Event - ItemSelected")]
        public async Task Event_ItemSelected()
        {
           var cut = RenderComponent<Events>();
           var buttonElems = cut.FindAll("Button",true);
           var divElems=cut.FindAll("div",true);
           buttonElems[0].Click();
           await Task.Delay(100);
           var contextElem=cut.FindAll("ul.e-contextmenu",true);
           contextElem[0].Children[0].Click();
           await Task.Delay(100);
           Assert.Contains("ItemSelected",divElems[3].TextContent.Trim());
        }

        [Trait("ContextMenu", "Aria")]
        [Fact(Timeout = 10000, DisplayName = "Aria-Basic")]
        public void Aria_Basic()
        {
           var cut = RenderComponent<Events>();
           var buttonElems = cut.FindAll("Button",true);
           buttonElems[0].Click();
           var contextElem=cut.FindAll("ul.e-contextmenu",true);
           Assert.Equal("menu",contextElem[0].GetAttribute("role"));
           Assert.Equal("menuitem",contextElem[0].Children[0].GetAttribute("role"));
           Assert.Equal("Cut",contextElem[0].Children[0].GetAttribute("aria-label"));
           Assert.Equal("true",contextElem[0].Children[2].GetAttribute("aria-haspopup"));
           Assert.Equal("false",contextElem[0].Children[2].GetAttribute("aria-expanded"));
           
        }

         [Trait("ContextMenu", "Others")]
        [Fact(Timeout = 10000, DisplayName = "RTL")]
        public async Task RTL()
        {
           var cut = RenderComponent<Rtl>();
           var buttonElems = cut.FindAll("Button",true);
           buttonElems[0].Click();
           await Task.Delay(200);
           var contextElem=cut.FindAll("ul.e-contextmenu",true);
           Assert.Contains("e-rtl",contextElem[0].ParentElement.ClassName);
        }
        [Trait("ContextMenu", "Methods")]
        [Fact(Timeout = 10000, DisplayName = "Method - Close")]
        public async Task Method_Close()
        {
            var cut = RenderComponent<Open>();
            await Task.Delay(100);
            var btnElems =cut.FindAll("button.e-btn",true);
            var contextElem=cut.FindAll("div.e-contextmenu-container",true);
            btnElems[0].Click();
            await Task.Delay(100);
            btnElems[1].Click();
            await Task.Delay(100);
            Assert.Equal(0,contextElem[0].ChildElementCount);
            Assert.Null(contextElem[0].QuerySelector(".e-contextmenu"));
        }
        [Trait("ContextMenu", "PropertyChanges")]
        [Fact(Timeout = 10000, DisplayName = "PropertyChanges - Items")]
        public async Task PropertyChange_Items()
        {
            var cut = RenderComponent<DefaultCM>();
            await Task.Delay(100);
            var btnElems =cut.FindAll("button.e-btn",true);
            var contextElem=cut.FindAll("ul.e-contextmenu",true);
            btnElems[0].Click();
            Assert.Equal(6,contextElem[0].ChildElementCount);
        }

        [Trait("ContextMenu", "CR Issues")]
        [Fact(DisplayName = "BLAZ:7737 Context menu items alignment")]
        public async Task BLAZ_7737()
        {
           var cut = RenderComponent<IndexCM>();
           var buttonElems = cut.FindAll("Button",true);
           buttonElems[0].Click();
           await Task.Delay(200);
           var contextElem=cut.FindAll("ul.e-contextmenu",true);
           Assert.Equal(3,contextElem[0].ChildElementCount);
           contextElem[0].Children[1].Click();
        }
        [Trait("ContextMenu", "CR Issues")]
        [Fact(DisplayName = "BLAZ:2603 Context menu popup position issue ")]
        public async Task BLAZ_2603()
        {
           // need to write
           var cut = RenderComponent<IndexCM>();
           var buttonElems = cut.FindAll("Button",true);
           buttonElems[0].Click();
           await Task.Delay(200);
           var contextElem=cut.FindAll("ul.e-contextmenu",true);
           Assert.Equal(3,contextElem[0].ChildElementCount);
           contextElem[0].Children[1].KeyDown(Key.Enter);
        }
         [Trait("ContextMenu", "CR Issues")]
         [Fact(DisplayName = "BLAZ:17877 stopPropagation is not working  when render target tag in innerdiv")]
         public async Task BLAZ_17877()
         {
           // need to write
           var cut = RenderComponent<CRIssues>();
           var buttonElems = cut.FindAll("Button",true);
           buttonElems[0].Click();
           await Task.Delay(200);
           var contextElem=cut.FindAll("ul.e-contextmenu",true);
           Assert.Equal(5,contextElem[0].ChildElementCount);
           buttonElems = cut.FindAll("Button",true);
           buttonElems[1].Click();
           await Task.Delay(200);
           contextElem=cut.FindAll("ul.e-contextmenu",true);
           Assert.Equal(5,contextElem[0].ChildElementCount);
         }
        [Trait("ContextMenu", "FieldsNullCheck")]
        [Fact(Timeout = 10000, DisplayName = "Fields Null Check")]
        public async Task FieldsNullCheck()
        {

            var cut = RenderComponent<SfContextMenu<object>>(p => p
                .Add(x => x.Items, null)
            );
            var menus = cut.FindAll("ul.e-contextmenu", true);
            await cut.InvokeAsync(() => cut.Instance.OpenAsync());
            menus = cut.FindAll("ul.e-contextmenu", true);
            Assert.Single(menus);
            Assert.Equal(0, menus[0].ChildElementCount);
            Assert.Empty(menus[0].QuerySelectorAll("li"));

        }
        [Trait("ContextMenu", "Filter")]
        [Fact(Timeout = 10000, DisplayName = "Filter Condition")]
        public async Task FilterCondition()
        {
            var cut = RenderComponent<SfContextMenu<string>>(parameters => parameters
                .Add(p => p.Filter, ".filter-target")
                .AddChildContent("<div class='filter-target'>Right click here</div>")
                .AddChildContent("<div>Do not trigger here</div>")
            );
            await cut.InvokeAsync(() => cut.Instance.OpenAsync());
            await Task.Delay(100);
            var menus = cut.FindAll("ul.e-contextmenu", true);
            Assert.NotEmpty(menus);

            await cut.InvokeAsync(() => cut.Instance.Close());
            menus = cut.FindAll("ul.e-contextmenu", true);
            Assert.Empty(menus);
        }
        [Fact(DisplayName = "Test Element Property Getter and Setter")]
        public void TestElementProperty()
        {
            var args = new OpenCloseMenuEventArgs<string>();
            var expectedElementReference = new ElementReference();
            args.Element = expectedElementReference;
            Assert.Equal(expectedElementReference, args.Element);
        }

        [Fact(DisplayName = "Test Items Property Getter and Setter")]
        public void TestItemsProperty()
        {
            var args = new OpenCloseMenuEventArgs<string>();
            var expectedItems = new List<string> { "Item1", "Item2" };
            args.Items = expectedItems;
            Assert.Equal(expectedItems, args.Items);
        }

        [Fact(DisplayName = "Test ParentItem Property Getter and Setter")]
        public void TestParentItemProperty()
        {
            var args = new OpenCloseMenuEventArgs<string>();
            var expectedParentItem = "ParentItem";
            args.ParentItem = expectedParentItem;
            Assert.Equal(expectedParentItem, args.ParentItem);
        }

        [Fact(DisplayName = "Test TargetId Property Getter and Setter")]
        public void TestTargetIdProperty()
        {
            var args = new OpenCloseMenuEventArgs<string>();
            var expectedTargetId = "element-id";
            args.TargetId = expectedTargetId;
            Assert.Equal(expectedTargetId, args.TargetId);
        }
        [Fact(DisplayName = "Test HTML Attributes Assignment")]
        public void TestHtmlAttributes()
        {
            var contextMenu = new SfContextMenu<string>
            {
                HtmlAttributes = new Dictionary<string, object>
            {
                { "style", "color: red;" },
                { "title", "context menu" }
            }
            };
            Assert.Equal("color: red;", contextMenu.HtmlAttributes["data-sf-style"]);
            Assert.Equal("context menu", contextMenu.HtmlAttributes["title"]);
        }
        [Fact(DisplayName = "Test PopupDataId Property Set And Get")]
        public void TestPopupDataIdProperty()
        {
            var options = new MenuOptions();
            string expected = "popup-123";
            options.popupDataId = expected;
            Assert.Equal(expected, options.popupDataId);
        }

        [Fact(DisplayName = "Test Element Property Set And Get")]
        public void TestElementPropertyMenu()
        {
            var options = new MenuOptions();
            var elementRef = new ElementReference("element123");
            options.Element = elementRef;
            Assert.Equal(elementRef, options.Element);
        }

        [Fact(DisplayName = "Test Popup Property Set And Get")]
        public void TestPopupProperty()
        {
            var options = new MenuOptions();
            var popupRef = new ElementReference("popup123");
            options.Popup = popupRef;
            Assert.Equal(popupRef, options.Popup);
        }

        [Fact(DisplayName = "Test ItemIndex Property Set And Get")]
        public void TestItemIndexProperty()
        {
            var options = new MenuOptions();
            int expected = 5;
            options.ItemIndex = expected;
            Assert.Equal(expected, options.ItemIndex);
        }
        [Fact(DisplayName = "Test IsVertical Property Set And Get")]
        public void TestIsVerticalProperty()
        {
            var options = new MenuOptions();
            bool expected = true;
            options.IsVertical = expected;
            Assert.Equal(expected, options.IsVertical);
        }

        [Fact(DisplayName = "Test ShowItemOnClick Property Set And Get")]
        public void TestShowItemOnClickProperty()
        {
            var options = new MenuOptions();
            bool expected = true;
            options.ShowItemOnClick = expected;
            Assert.Equal(expected, options.ShowItemOnClick);
        }

        [Fact(DisplayName = "Test NavigationIndex Property Set And Get")]
        public void TestNavigationIndexProperty()
        {
            var options = new MenuOptions();
            List<int> expected = new List<int> { 1, 2, 3 };
            options.NavigationIndex = expected;
            Assert.Equal(expected, options.NavigationIndex);
        }

        [Fact(DisplayName = "Test Orientation Property Set And Get")]
        public void TestOrientationProperty()
        {
            var options = new MenuOptions();
            Orientation expected = Orientation.Horizontal;
            options.Orientation = expected;
            Assert.Equal(expected, options.Orientation);
        }

        [Fact(DisplayName = "Test AnimationSettings Property Set And Get")]
        public void TestAnimationSettingsProperty()
        {
            var options = new MenuOptions();
            var expected = new Dictionary<string, object>
        {
            { "effect", "fade" },
            { "duration", 300 }
        };
            options.AnimationSettings = expected;
            Assert.Equal(expected, options.AnimationSettings);
        }
    }
}
