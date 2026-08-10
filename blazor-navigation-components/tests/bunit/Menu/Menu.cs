using System.Collections.Generic;
using Bunit;
using Xunit;
using System.Threading.Tasks;
using AngleSharp.Dom;
using System.Linq;
using Syncfusion.Blazor.Navigations;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using System;
using Syncfusion.Blazor.Navigations.Internal;

namespace Syncfusion.Blazor.Tests.Navigations.TestCase
{
    public partial class Menu : BunitTestContext
    {


        [Trait("Menu", "Basic Menu")]
        [Fact(Timeout = 10000, DisplayName = "Basic Menu  Vertical")]
        public async Task Menu_Vertical()
        {
            var cut = RenderComponent<Vertical>();
            var ul = cut.Find("ul");
            Assert.True(ul.ClassList.Contains("e-vertical"));
            ul.FirstElementChild.MouseOver();
            ul.FirstElementChild.Click();
            await Task.Delay(200);
            ul.KeyDown(new KeyboardEventArgs { Code = "ArrowLeft" });
            await Task.Delay(200);
            ul.KeyDown(new KeyboardEventArgs { Code = "ArrowRight" });
            await Task.Delay(200);
            ul.KeyDown(new KeyboardEventArgs { Code = "ArrowDown" });
            await Task.Delay(200);
            ul.KeyDown(new KeyboardEventArgs { Code = "ArrowUp" });
            await Task.Delay(200);
            ul.KeyDown(new KeyboardEventArgs { Code = "Enter" });
            await Task.Delay(200);
            ul.KeyDown(new KeyboardEventArgs { Code = "Home" });
            await Task.Delay(200);
            ul.KeyDown(new KeyboardEventArgs { Code = "End" });
            await Task.Delay(200);
            ul.KeyDown(new KeyboardEventArgs { Code = "Escape" });
            await Task.Delay(200);
            ul.KeyDown(new KeyboardEventArgs { Code = "Tab" });
        }

        [Trait("Menu", "Seperator")]
        [Fact(Timeout = 10000, DisplayName = "Seperator - No Seperator")]
        public void Menu_No_Seperator()
        {
            var cut = RenderComponent<DefaultMenu>();
            var ul = cut.FindAll("e-separator");
            Assert.True(ul.Count==0);
        }

         [Trait("Menu", "Seperator")]
        [Fact(Timeout = 10000, DisplayName = "Seperator - MultipleSeperatorItem")]
        public void Menu_MultipleSeperatorItem()
        {
            var cut = RenderComponent<DefaultMenu>();
            var ul = cut.FindAll("ul");
            ul[0].Children[1].MouseOver();
            var classname = cut.FindAll("li.e-separator");
            Assert.Equal(0, classname.Count);
        }

        [Trait("Menu", "Icon")]
        [Fact(Timeout = 10000, DisplayName = "Icon")]
        public void Menu_Icon()
        {
            var cut = RenderComponent<DefaultMenu>();
            var ul = cut.Find("ul");
            Assert.False(ul.Children[0].ClassList.Contains("e-blankicon"));
            Assert.Equal("e-menu-icon em-icons e-file", ul.Children[0].Children[0].ClassName);
        }

        [Trait("Menu", "DataBinding")]
        [Fact(Timeout = 10000, DisplayName = "DataBinding - Empty Datasource")]
        public void Menu_EmptyDataSource()
        {
            var cut = RenderComponent<DataBinding>();
            var ul = cut.FindAll("ul");
            Assert.Equal(0, ul[0].ChildElementCount);
        }


        [Trait("Menu", "DataBinding")]
        [Fact(Timeout = 10000, DisplayName = "DataBinding - With Datasource")]
        public void Menu_WithDataSource()
        {
            var cut = RenderComponent<DataBinding>();
            var ul = cut.FindAll("ul");
            Assert.Equal(5, ul[1].ChildElementCount);
            Assert.Equal("Appliances", ul[1].Children[0].TextContent.Trim());
            Assert.Equal("Home & Living", ul[1].Children[3].TextContent.Trim());
        }

        [Trait("Menu", "DataBinding")]
        [Fact(Timeout = 10000, DisplayName = "DataBinding - SelfReferential Databinding")]
        public void Menu_SelfRerential()
        {
            var cut = RenderComponent<DataBinding>();
            var ul = cut.FindAll("ul");
            Assert.Equal(3, ul[2].ChildElementCount);
            //validating the 1st item text
            Assert.Equal("Events", ul[2].Children[0].TextContent.Trim());
            //validating the 3rd item text
            Assert.Equal("Directory", ul[2].Children[2].TextContent.Trim());
        }

        [Trait("Menu", "KeyboardFunctions")]
        [Fact(Timeout = 10000, DisplayName = "KeyboardFunction - Basic")]
        public void Menu_KeyboardFunctions_Basic()
        {
            var cut = RenderComponent<DefaultMenu>();
            var ul = cut.Find("ul");

            //check for tab index 0 for ul element
            Assert.Equal("0", ul.GetAttribute("tabindex"));

            //validating the tab index for li items
            for (var i=0; i<5;i++)
            Assert.Equal("-1", ul.Children[i].GetAttribute("tabindex"));

           
        }


        [Trait("Menu", "Events")]
        [Fact(Timeout = 10000, DisplayName = "Events - Created")]
        public async Task Menu_Events_Created()
        {
            var cut = RenderComponent<EventsMenu>();
            await Task.Delay(200);
            var eventElems = cut.FindAll("div.event-trace", true);
            //checking for Created event text in log
            Assert.Contains("Created event", eventElems[0].TextContent.Trim());
        }

        [Trait("Menu", "Events")]
        [Fact(Timeout = 10000, DisplayName = "Events - OnRender")]
        public async Task Menu_Events_OnRender()
        {
            var cut = RenderComponent<EventsMenu>();
            var ul = cut.Find("ul");
            var btnelem = cut.Find("button.e-btn");            
            btnelem.Click();
            await Task.Delay(100);
            //mousehover on file menu
            ul.Children[0].MouseOver();
            await Task.Delay(100);
            var eventElems = cut.FindAll("div.event-trace", true);
            Assert.Contains("OnItemRender", eventElems[0].TextContent.Trim());
        }

        [Trait("Menu", "Events")]
        [Fact(Timeout = 10000, DisplayName = "Events - OnOpen")]
        public async Task Menu_Events_OnOpen()
        {
            var cut = RenderComponent<EventsMenu>();
            var ul = cut.Find("ul");
            var btnelem = cut.Find("button.e-btn");            
            //Clear logs
            btnelem.Click();
            await Task.Delay(100);
            //mousehover on file menu
             ul.Children[0].MouseOver();
            await Task.Delay(100);
            var eventElems = cut.FindAll("div.event-trace", true);
            Assert.Contains("OnOpen", eventElems[0].TextContent.Trim());
        }

        [Trait("Menu", "Events")]
        [Fact(Timeout = 10000, DisplayName = "Events - Opened")]
        public async Task Menu_Events_Opened()
        {
            var cut = RenderComponent<EventsMenu>();
            var ul = cut.Find("ul");
            var btnelem = cut.Find("button.e-btn");            
            //Clear logs
            btnelem.Click();
            await Task.Delay(100);
            var divelems = cut.FindAll("div", true);
            //mousehover on file menu
             ul.Children[0].MouseOver();
            await Task.Delay(100);
            var eventElems = cut.FindAll("div.event-trace", true);
            Assert.Contains("Opened", eventElems[0].TextContent.Trim());
        }

        [Trait("Menu", "Events")]
        [Fact(Timeout = 10000, DisplayName = "Events - OnClose")]
        public async Task Menu_Events_OnClose()
        {
            var cut = RenderComponent<EventsMenu>();
            var ul = cut.Find("ul");
            var btnelem = cut.Find("button.e-btn");            
            //Clear logs
            btnelem.Click();
            await Task.Delay(100);
            var divelems = cut.FindAll("div", true);
            //mousehover on file menu
            ul.Children[0].MouseOver();
            var divelemss = cut.FindAll("div", true);
            await Task.Delay(100);
            var divelemsss = cut.FindAll("div", true);
            await Task.Delay(100);
            ul.Children[1].MouseOver();
            await Task.Delay(100);
            var eventElems = cut.FindAll("div.event-trace", true);
            Assert.Contains("OnClose", eventElems[0].TextContent.Trim());
        }

        [Trait("Menu", "Events")]
        [Fact(Timeout = 10000, DisplayName = "Events - Closed")]
        public async Task Menu_Events_Closed()
        {
            var cut = RenderComponent<EventsMenu>();
            var ul = cut.Find("ul");
            var btnelem = cut.Find("button.e-btn");            
            //Clear logs
            btnelem.Click();
            await Task.Delay(100);
            var divelems = cut.FindAll("div", true);
            //mousehover on file menu
            ul.Children[0].MouseOver();
            var divelemss = cut.FindAll("div", true);
            await Task.Delay(100);
            var divelemsss = cut.FindAll("div", true);
            await Task.Delay(100);
            ul.Children[1].MouseOver();
            await Task.Delay(100);
            var eventElems = cut.FindAll("div.event-trace", true);
            Assert.Contains("Closed", eventElems[0].TextContent.Trim());
        }


        [Trait("Menu", "Events")]
        [Fact(Timeout = 10000, DisplayName = "Events - Select")]
        public async Task Menu_Events_Select()
        {
            var cut = RenderComponent<EventsMenu>();
            var ul = cut.Find("ul");
            var btnelem = cut.Find("button.e-btn");            
            //Clear logs
            btnelem.Click();
            await Task.Delay(100);
            var divelems = cut.FindAll("div", true);
            //clicking help item
             ul.Children[4].Click();

            //checking for select event text in log
            Assert.Contains("ItemSelected", divelems[1].Children[0].TextContent);
        }

        [Trait("Menu", "Aria")]
        [Fact(Timeout = 10000, DisplayName = "Aria - Basic")]
        public void Menu_Aria_Basic()
        {
            var cut = RenderComponent<DefaultMenu>();
            var ul = cut.Find("ul");
         
            //checking for role in ul
            Assert.Equal("menubar", ul.GetAttribute("role"));

            //checking for role in li
            for (var i = 0; i < 5; i++)
                Assert.Equal("menuitem", ul.Children[i].GetAttribute("role"));

            //checking for aria label for all li's
            Assert.Equal("File", ul.Children[0].GetAttribute("aria-label"));
            Assert.Equal("Edit", ul.Children[1].GetAttribute("aria-label"));
            Assert.Equal("View", ul.Children[2].GetAttribute("aria-label"));
            Assert.Equal("Tools", ul.Children[3].GetAttribute("aria-label"));
            Assert.Equal("Help", ul.Children[4].GetAttribute("aria-label"));

            //checking for aria-pop is boolean
            for (var i = 0; i < 5; i++)
            {
                if (i<4)
                {
                    Assert.Equal("true", ul.Children[i].GetAttribute("aria-haspopup"));
                }
                else
                {
                    Assert.Null(ul.Children[i].GetAttribute("aria-haspopup"));
                }
            }
                

            //checking for aria-expanded
            for (var i = 0; i < 4; i++)
            {
                if (i<4)
                {
                    Assert.Equal("false", ul.Children[i].GetAttribute("aria-expanded"));
                }
                else
                {
                    Assert.Null(ul.Children[i].GetAttribute("aria-expanded"));
                }
            }
              


        }

        [Trait("Menu", "PropertyChanges")]
        [Fact(Timeout = 10000, DisplayName = "PropertyChanges - WithDatasource")]
        public void Menu_PropertyChanges_WithDatasource()
        {
            var cut = RenderComponent<DataBinding>((nameof(DataBinding.EmptyDataSource), GetDataItems())) ;
            var ul = cut.FindAll("ul");
            Assert.Equal(5, ul[0].ChildElementCount);
        }

        [Trait("Menu", "PropertyChanges")]
        [Fact(Timeout = 10000, DisplayName = "PropertyChanges - EmptyDatasource")]
        public void Menu_PropertyChanges_EmptyDatasource()
        {
            var cut = RenderComponent<DataBinding>((nameof(DataBinding.menuItems), new List<object> { }));
            var ul = cut.FindAll("ul");
            Assert.Equal(0, ul[1].ChildElementCount);
        }

        [Trait("Menu", "PropertyChanges")]
        [Fact(Timeout = 10000, DisplayName = "PropertyChanges - SelfReferential")]
        public void Menu_PropertyChanges_SelfRerential()
        {
           // var cut = RenderComponent<DataBinding>((nameof(DataBinding.MenuSelfMenuItems), SelfMenuItems()));
           //var ul = cut.FindAll("ul");
           // check mouse over selected item element
           //Assert.Equal(3, ul[2].ChildElementCount);
        }

        [Trait("Menu", "PropertyChanges")]
        [Fact(Timeout = 10000, DisplayName = "PropertyChanges - Item on click")]
        public void Menu_PropertyChanges_ItemOnClick()
        {
            var cut = RenderComponent<OpenOnClick>((nameof(OpenOnClick.ItemOnClick), false));
            var ul = cut.Find("ul");
            ul.FirstElementChild.MouseOver();
            Assert.True(ul.FirstElementChild.ClassList.Contains("e-selected"));
        }

        [Trait("Menu", "PropertyChanges")]
        [Fact(Timeout = 10000, DisplayName = "PropertyChanges - Orientation")]
        public void Menu_PropertyChanges_Orientation()
        {
            var cut = RenderComponent<Vertical>((nameof(Vertical.Orientation),Orientation.Horizontal));
            var ul = cut.Find("ul");
            Assert.False(ul.ClassList.Contains("e-vertical"));
        }


        [Trait("Menu", "RTL")]
        [Fact(Timeout = 10000, DisplayName = "RTL")]
        public void Menu_RTL()
        {
            var cut = RenderComponent<RtlMenu>();
            var ul = cut.Find("ul");
            Assert.True(ul.ParentElement.ClassList.Contains("e-rtl"));
        }

        [Trait("Menu", "RTL")]
        [Fact(Timeout = 10000, DisplayName = "Vertical RTL")]
        public void Menu_VerticalRTL()
        {
            var cut = RenderComponent<RtlMenu>();
            var ul = cut.FindAll("ul")[1];
            Assert.True(ul.ClassList.Contains("e-vertical"));
            Assert.True(ul.ParentElement.ClassList.Contains("e-rtl"));
        }

        [Trait("Menu", "HamburgerMode")]
        [Fact(Timeout = 10000, DisplayName = "Hamburger - Basic")]
        public async Task Menu_Hamburger_Basic()
        {
            var cut = RenderComponent<HamburgerMode>();
            var div = cut.FindAll("div.e-menu-container");
            Assert.True(div[0].ClassList.Contains("e-hamburger"));
            Assert.Equal("Menu",div[0].Children[0].Children[0].TextContent.Trim());
            Assert.True(div[0].Children[0].Children[1].ClassList.Contains("e-menu-icon"));
            div[0].Children[0].Children[1].Click();
            var ul=cut.Find("ul");
            await Task.Delay(200);
            Assert.Equal("e-lib e-menu e-control e-menu-parent",ul.ClassName);
            for (var i = 0; i < 5; i++)
                Assert.True(ul.Children[i].ClassList.Contains("e-menu-item"));
            ul.FirstElementChild.Click();
            await Task.Delay(200);
            var li=cut.Find("ul.e-menu-parent.e-ul");
            Assert.Equal("e-menu-item e-menu-caret-icon",li.FirstElementChild.ClassName);
            Assert.Equal("e-icons e-caret",li.FirstElementChild.Children[0].ClassName);
            var btn = cut.FindAll(".e-hamb-btn", true);
            btn[0].Click();
        }
        
        [Trait("Menu", "HamburgerMode")]
        [Fact(Timeout = 10000, DisplayName = "Hamburger - UIIntercation In Parent Menu")]
        public async Task Menu_Hamburger_UIIntercation_parent()
        {
            var cut = RenderComponent<HamburgerMode>();
            var div = cut.FindAll("div.e-menu-container");
            div[0].Children[0].Children[1].Click();
            var ul=cut.Find("ul");
            ul.FirstElementChild.Click();
            await Task.Delay(200);
            var li=cut.Find("ul.e-menu-parent.e-ul");
            Assert.Equal("true",li.FirstElementChild.GetAttribute("aria-haspopup"));
            Assert.Equal("false",li.FirstElementChild.GetAttribute("aria-expanded"));
            li.FirstElementChild.MouseOver();
            Assert.True(li.FirstElementChild.ClassList.Contains("e-focused"));
            li.FirstElementChild.Click();
            await Task.Delay(200);
            Assert.True(li.FirstElementChild.ClassList.Contains("e-selected"));
            Assert.Equal("true",li.FirstElementChild.GetAttribute("aria-expanded"));
        }

        [Trait("Menu", "HamburgerMode")]
        [Fact(Timeout = 10000, DisplayName = "Hamburger - UIIntercation In Parent and Child Menu")]
        public async Task Menu_Hamburger_UIIntercation_parent_Child()
        {
            var cut = RenderComponent<HamburgerMode>();
            var div = cut.FindAll("div.e-menu-container");
            div[0].Children[0].Children[1].Click();
            var parent=cut.Find("ul.e-menu-parent");
            parent.FirstElementChild.Click();
            await Task.Delay(100);
            Assert.True(parent.FirstElementChild.ClassList.Contains("e-selected"));
            Assert.Equal("true", parent.FirstElementChild.GetAttribute("aria-expanded"));
            await Task.Delay(100);
            var li=cut.FindAll("ul.e-menu-parent",true);
            li[1].FirstElementChild.Click();
            await Task.Delay(100);
            Assert.True(li[1].FirstElementChild.ClassList.Contains("e-selected"));
            Assert.Equal("true",li[1].FirstElementChild.GetAttribute("aria-expanded"));
            Assert.Equal("true", parent.FirstElementChild.GetAttribute("aria-expanded"));
            Assert.True(parent.FirstElementChild.ClassList.Contains("e-selected"));
            await Task.Delay(100);
            var li_popup=cut.FindAll("ul.e-menu-parent",true);
            li_popup[2].FirstElementChild.Click();
            await Task.Delay(100);
            Assert.Equal("false", parent.FirstElementChild.GetAttribute("aria-expanded"));
            Assert.False(parent.ClassList.Contains("e-selected"));
            var parent_count=cut.FindAll("ul.e-menu-parent");
            Assert.Equal(1,parent_count.Count);
        }

        [Trait("Menu", "HamburgerMode")]
        [Fact(Timeout = 10000, DisplayName = "Hamburger - CustomizedRendering")]
        public async Task Menu_Hamburger_CustomizedRendering()
        {
            var cut = RenderComponent<HamburgerMode>((nameof(HamburgerMode.title),"syncfusion"));
            var div = cut.FindAll("div.e-menu-container");
            Assert.Equal("e-menu-container e-hamburger",div[0].ClassName);
            Assert.Equal("syncfusion",div[0].Children[0].Children[0].TextContent.Trim());
            Assert.True(div[0].Children[0].Children[1].ClassList.Contains("e-menu-icon"));
            div[0].Children[0].Children[1].Click();
            var ul=cut.Find("ul");
            Assert.Equal("e-lib e-menu e-control e-menu-parent",ul.ClassName);
            ul.KeyDown(new KeyboardEventArgs { Code = "ArrowLeft" });
            await Task.Delay(200);
            ul.KeyDown(new KeyboardEventArgs { Code = "ArrowRight" });
            await Task.Delay(200);
            ul.KeyDown(new KeyboardEventArgs { Code = "ArrowDown" });
            await Task.Delay(200);
            ul.KeyDown(new KeyboardEventArgs { Code = "ArrowUp" });
            await Task.Delay(200);
            ul.KeyDown(new KeyboardEventArgs { Code = "Enter" });
            await Task.Delay(200);
            ul.KeyDown(new KeyboardEventArgs { Code = "Home" });
            await Task.Delay(200);
            ul.KeyDown(new KeyboardEventArgs { Code = "End" });
            await Task.Delay(200);
            ul.KeyDown(new KeyboardEventArgs { Code = "Escape" });
            await Task.Delay(200);
            ul.KeyDown(new KeyboardEventArgs { Code = "Tab" });
        }


        //datasource
        private List<object> GetDataItems()
        {
            return new List<object>
            {
                new { Id= "parent1", Text= "Appliances" },
                new { Id= "parent2", Text= "Accessories" },
                new { Id= "parent3", Text= "Fashion" },
                new { Id= "parent4", Text= "Home & Living" },
                new { Id= "parent5", Text= "Entertainment" }
            };
        }

        [Trait("Menu", "CR_Issues")]
        [Fact(Timeout = 10000, DisplayName = "BLAZ-12036: Script error thrown while opening another menu before closing the menu popup")]
        public async Task BLAZ_12036()
        {
            var cut = RenderComponent<IndexMenu>();
            await Task.Delay(500);
            var menuElems = cut.FindAll("ul",true);
            menuElems[0].FirstElementChild.MouseOver();
            await Task.Delay(200);
            var ulElems = cut.FindAll("ul.e-menu-parent.e-ul",true);
            Assert.Equal(3,ulElems[0].ChildElementCount);
            menuElems[1].FirstElementChild.MouseOver();
            await Task.Delay(200);
            ulElems = cut.FindAll("ul.e-menu-parent.e-ul",true);
            Assert.Equal(3,ulElems[0].ChildElementCount);
        }
        [Trait("Menu", "CR_Issues")]
        [Fact(Timeout = 10000, DisplayName = "BLAZ-11929: SFMenu UG samples not working")]
        public async Task BLAZ_11929()
        {
            var cut = RenderComponent<ShowHide>();
            var ul = cut.Find("ul");
            Assert.True(ul.Children[1].ClassList.Contains("e-menu-hide"));
            ul.FirstElementChild.MouseOver();
            await Task.Delay(200);
            var popup_ul = cut.Find("ul.e-menu-parent.e-ul");
            Assert.True(popup_ul.Children[1].ClassList.Contains("e-menu-hide"));
            Assert.True(popup_ul.Children[2].ClassList.Contains("e-menu-hide"));
        }

        [Trait("Menu", "Keydown_Action")]
        [Fact(Timeout = 10000, DisplayName = "Keydown_Action with default sample")]
        public async Task KeydownAction()
        {
            var cut = RenderComponent<OpenOnClick>();
            var ul = cut.Find("ul");
            ul.FirstElementChild.MouseOver();
            ul.FirstElementChild.Click();
            await Task.Delay(200);
            ul.KeyDown(new KeyboardEventArgs { Code = "ArrowLeft" });
            await Task.Delay(200);
            ul.KeyDown(new KeyboardEventArgs { Code = "ArrowRight" });
            await Task.Delay(200);
            ul.KeyDown(new KeyboardEventArgs { Code = "ArrowDown" });
            await Task.Delay(200);
            ul.KeyDown(new KeyboardEventArgs { Code = "ArrowUp" });
            await Task.Delay(200);
            ul.KeyDown(new KeyboardEventArgs { Code = "Enter" });
            await Task.Delay(200);
            ul.KeyDown(new KeyboardEventArgs { Code = "Home" });
            await Task.Delay(200);
            ul.KeyDown(new KeyboardEventArgs { Code = "End" });
            await Task.Delay(200);
            ul.KeyDown(new KeyboardEventArgs { Code = "Escape" });
            await Task.Delay(200);
            ul.KeyDown(new KeyboardEventArgs { Code = "Tab" });
        }
        [Fact(DisplayName = "Test DocumentMouseDownAsync - Default Parameters")]
        public async Task DocumentMouseDownAsync_DefaultParameters()
        {
            var menu = new SfMenu<string>();
            await menu.DocumentMouseDownAsync();
        }

        //[Fact(DisplayName = "Test DocumentMouseDownAsync - Skip Navigation Index")]
        //public async Task DocumentMouseDownAsync_SkipNavIndex()
        //{
        //    var menu = new SfMenu<string>
        //    {
        //        NavIdx = new List<int> { 0 }
        //    };

        //    await menu.DocumentMouseDownAsync(skipNavIndex: true);
        //}

        [Fact(DisplayName = "Test DocumentMouseDownAsync - Close SubMenu")]
        public async Task DocumentMouseDownAsync_CloseSubMenu()
        {
            var menu = new SfMenu<string>();
            await menu.DocumentMouseDownAsync(closeSubMenu: true);
        }

        [Fact(DisplayName = "Test DocumentMouseDownAsync - Is Focus")]
        public async Task DocumentMouseDownAsync_IsFocus()
        {
            var menu = new SfMenu<string>();
            await menu.DocumentMouseDownAsync(isFocus: true);
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
        [Fact(DisplayName = "ElementReference_Property_SetAndGet")]
        public void ElementReferencePropertySetAndGet()
        {
            var elementReference = new ElementReference(Guid.NewGuid().ToString());
            var args = new BeforeOpenCloseMenuEventArgs<object>();
            args.Element = elementReference;
            Assert.Equal(elementReference, args.Element);
        }

        [Fact(DisplayName = "Items_Property_SetAndGet")]
        public void ItemsPropertySetAndGet()
        {
            var items = new List<object> { "Item1", "Item2" };
            var args = new BeforeOpenCloseMenuEventArgs<object>();
            args.Items = items;
            Assert.Equal(items, args.Items);
        }

        [Fact(DisplayName = "TargetId_Property_SetAndGet")]
        public void TargetIdPropertySetAndGet()
        {
            var targetId = "target-element-id";
            var args = new BeforeOpenCloseMenuEventArgs<object>();
            args.TargetId = targetId;
            Assert.Equal(targetId, args.TargetId);
        }
        [Fact(DisplayName = "Test Children Property Default and Set/Get")]
        public void TestChildrenProperty()
        {
            var settings = new MenuFieldSettings();
            string expected = "NewItems";
            settings.Children = expected;
            Assert.Equal("Items", new MenuFieldSettings().Children);
            Assert.Equal(expected, settings.Children);
        }

        [Fact(DisplayName = "Test IconCss Property Default and Set/Get")]
        public void TestIconCssProperty()
        {
            var settings = new MenuFieldSettings();
            string expected = "NewIconCss";
            settings.IconCss = expected;
            Assert.Equal("IconCss", new MenuFieldSettings().IconCss);
            Assert.Equal(expected, settings.IconCss);
        }

        [Fact(DisplayName = "Test Separator Property Default and Set/Get")]
        public void TestSeparatorProperty()
        {
            var settings = new MenuFieldSettings();
            string expected = "NewSeparator";
            settings.Separator = expected;
            Assert.Equal("Separator", new MenuFieldSettings().Separator);
            Assert.Equal(expected, settings.Separator);
        }

        [Fact(DisplayName = "Test Disabled Property Default and Set/Get")]
        public void TestDisabledProperty()
        {
            var settings = new MenuFieldSettings();
            string expected = "NewDisabled";
            settings.Disabled = expected;
            Assert.Equal("Disabled", new MenuFieldSettings().Disabled);
            Assert.Equal(expected, settings.Disabled);
        }

        [Fact(DisplayName = "Test Hidden Property Default and Set/Get")]
        public void TestHiddenProperty()
        {
            var settings = new MenuFieldSettings();
            string expected = "NewHidden";
            settings.Hidden = expected;
            Assert.Equal("Hidden", new MenuFieldSettings().Hidden);
            Assert.Equal(expected, settings.Hidden);
        }


        [Fact(DisplayName = "Test Url Property Default and Set/Get")]
        public void TestUrlProperty()
        {
            var settings = new MenuFieldSettings();
            string expected = "NewUrl";
            settings.Url = expected;
            Assert.Equal("Url", new MenuFieldSettings().Url);
            Assert.Equal(expected, settings.Url);
        }

        [Fact(DisplayName = "Test HtmlAttributes Property Default and Set/Get")]
        public void TestHtmlAttributesProperty()
        {
            var settings = new MenuFieldSettings();
            string expected = "NewHtmlAttributes";
            settings.HtmlAttributes = expected;
            Assert.Equal("HtmlAttributes", new MenuFieldSettings().HtmlAttributes);
            Assert.Equal(expected, settings.HtmlAttributes);
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
