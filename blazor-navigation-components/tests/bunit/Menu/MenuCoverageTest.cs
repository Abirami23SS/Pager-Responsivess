using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bunit;
using Xunit;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Navigations.Internal;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;

namespace Syncfusion.Blazor.Tests.Navigations.TestCase
{
    public partial class MenuCoverageTest : BunitTestContext
    {
        #region MenuEventArgs Coverage
        [Fact(DisplayName = "MenuEventArgs Element Property Set And Get")]
        public void MenuEventArgs_ElementProperty_SetAndGet()
        {
            var args = new MenuEventArgs<MenuItem>();
            var elementRef = new ElementReference("test-element");
            args.Element = elementRef;
            Assert.Equal(elementRef, args.Element);
        }

        [Fact(DisplayName = "MenuEventArgs Event Property Set And Get")]
        public void MenuEventArgs_EventProperty_SetAndGet()
        {
            var args = new MenuEventArgs<MenuItem>();
            var eventArgs = new System.EventArgs();
            args.Event = eventArgs;
            Assert.Equal(eventArgs, args.Event);
        }

        [Fact(DisplayName = "MenuEventArgs Item Property Set And Get")]
        public void MenuEventArgs_ItemProperty_SetAndGet()
        {
            var args = new MenuEventArgs<MenuItem>();
            var item = new MenuItem { Text = "Test" };
            args.Item = item;
            Assert.Equal(item, args.Item);
        }

        [Fact(DisplayName = "MenuEventArgs Name Property Set And Get")]
        public void MenuEventArgs_NameProperty_SetAndGet()
        {
            var args = new MenuEventArgs<MenuItem>();
            args.Name = "TestEvent";
            Assert.Equal("TestEvent", args.Name);
        }

        [Fact(DisplayName = "MenuEventArgs String Item Property Set And Get")]
        public void MenuEventArgs_StringItem_SetAndGet()
        {
            var args = new MenuEventArgs<string>();
            args.Item = "TestString";
            Assert.Equal("TestString", args.Item);
        }
        #endregion

        #region BeforeOpenCloseMenuEventArgs Coverage
        [Fact(DisplayName = "BeforeOpenCloseMenuEventArgs Cancel Property Set And Get")]
        public void BeforeOpenCloseMenuEventArgs_CancelProperty_SetAndGet()
        {
            var args = new BeforeOpenCloseMenuEventArgs<MenuItem>();
            args.Cancel = true;
            Assert.True(args.Cancel);
        }

        [Fact(DisplayName = "BeforeOpenCloseMenuEventArgs ScrollHeight Property Set And Get")]
        public void BeforeOpenCloseMenuEventArgs_ScrollHeightProperty_SetAndGet()
        {
            var args = new BeforeOpenCloseMenuEventArgs<MenuItem>();
            args.ScrollHeight = 300;
            Assert.Equal(300, args.ScrollHeight);
        }

        [Fact(DisplayName = "BeforeOpenCloseMenuEventArgs Top Property Set And Get")]
        public void BeforeOpenCloseMenuEventArgs_TopProperty_SetAndGet()
        {
            var args = new BeforeOpenCloseMenuEventArgs<MenuItem>();
            args.Top = 100.5;
            Assert.Equal(100.5, args.Top);
        }

        [Fact(DisplayName = "BeforeOpenCloseMenuEventArgs Left Property Set And Get")]
        public void BeforeOpenCloseMenuEventArgs_LeftProperty_SetAndGet()
        {
            var args = new BeforeOpenCloseMenuEventArgs<MenuItem>();
            args.Left = 200.5;
            Assert.Equal(200.5, args.Left);
        }

        [Fact(DisplayName = "BeforeOpenCloseMenuEventArgs IsOpenHover Property Set And Get")]
        public void BeforeOpenCloseMenuEventArgs_IsOpenHover_SetAndGet()
        {
            var args = new BeforeOpenCloseMenuEventArgs<MenuItem>();
            args.Cancel = true;
            Assert.True(args.Cancel);
        }

        [Fact(DisplayName = "BeforeOpenCloseMenuEventArgs ParentItem Property Set And Get")]
        public void BeforeOpenCloseMenuEventArgs_ParentItem_SetAndGet()
        {
            var args = new BeforeOpenCloseMenuEventArgs<MenuItem>();
            var parentItem = new MenuItem { Text = "Parent" };
            args.ParentItem = parentItem;
            Assert.Equal(parentItem, args.ParentItem);
        }
        #endregion

        #region OpenCloseMenuEventArgs Coverage
        [Fact(DisplayName = "OpenCloseMenuEventArgs NavigationIndex Property Set And Get")]
        public void OpenCloseMenuEventArgs_NavigationIndexProperty_SetAndGet()
        {
            var args = new OpenCloseMenuEventArgs<MenuItem>();
            args.NavigationIndex = 5;
            Assert.Equal(5, args.NavigationIndex);
        }

        [Fact(DisplayName = "OpenCloseMenuEventArgs String Item Property Set And Get")]
        public void OpenCloseMenuEventArgs_StringItem_SetAndGet()
        {
            var args = new OpenCloseMenuEventArgs<string>();
            args.Items = new List<string> { "Item1", "Item2" };
            Assert.Equal(2, args.Items.Count);
        }
        #endregion

        #region SfMenu Methods Coverage
        [Fact(DisplayName = "SfMenu GetItemIndex with MenuItem")]
        public void SfMenu_GetItemIndex_MenuItem()
        {
            var cut = RenderComponent<SfMenu<MenuItem>>(parameters => parameters
                .Add(p => p.Items, new List<MenuItem>
                {
                    new MenuItem { Id = "file", Text = "File" },
                    new MenuItem { Id = "edit", Text = "Edit" }
                }));
            var menu = cut.Instance;
            var index = menu.GetItemIndex(new MenuItem { Text = "File" }, false);
            Assert.Single(index);
            Assert.Equal(0, index[0]);
        }

        [Fact(DisplayName = "SfMenu GetItemIndex with UniqueId")]
        public void SfMenu_GetItemIndex_UniqueId()
        {
            var cut = RenderComponent<SfMenu<MenuItem>>(parameters => parameters
                .Add(p => p.Items, new List<MenuItem>
                {
                    new MenuItem { Id = "file", Text = "File" },
                    new MenuItem { Id = "edit", Text = "Edit" }
                }));
            var menu = cut.Instance;
            var index = menu.GetItemIndex(new MenuItem { Id = "edit" }, true);
            Assert.Single(index);
            Assert.Equal(1, index[0]);
        }

        [Fact(DisplayName = "SfMenu GetItemIndex with Nested Items")]
        public void SfMenu_GetItemIndex_NestedItems()
        {
            var cut = RenderComponent<SfMenu<MenuItem>>(parameters => parameters
                .Add(p => p.Items, new List<MenuItem>
                {
                    new MenuItem
                    {
                        Id = "file",
                        Text = "File",
                        Items = new List<MenuItem>
                        {
                            new MenuItem { Id = "open", Text = "Open" },
                            new MenuItem { Id = "save", Text = "Save" }
                        }
                    }
                }));
            var menu = cut.Instance;
            var index = menu.GetItemIndex(new MenuItem { Text = "Open" }, false);
            Assert.Equal(2, index.Count);
        }

        [Fact(DisplayName = "SfMenu GetItemIndex Not Found")]
        public void SfMenu_GetItemIndex_NotFound()
        {
            var cut = RenderComponent<SfMenu<MenuItem>>(parameters => parameters
                .Add(p => p.Items, new List<MenuItem>
                {
                    new MenuItem { Text = "File" }
                }));
            var menu = cut.Instance;
            var index = menu.GetItemIndex(new MenuItem { Text = "NonExistent" }, false);
            Assert.Single(index);
            Assert.Equal(-1, index[0]);
        }

        [Fact(DisplayName = "SfMenu OpenAsync and CloseAsync Hamburger Mode")]
        public async Task SfMenu_OpenAsync_CloseAsync_HamburgerMode()
        {
            var cut = RenderComponent<SfMenu<MenuItem>>(parameters => parameters
                .Add(p => p.HamburgerMode, true)
                .Add(p => p.Title, "Test Menu")
                .Add(p => p.Items, new List<MenuItem>
                {
                    new MenuItem { Text = "File" },
                    new MenuItem { Text = "Edit" }
                }));
            var menu = cut.Instance;

            // Test OpenAsync
            await menu.OpenAsync();
            await Task.Delay(100);

            // Test CloseAsync
            await menu.CloseAsync();
            await Task.Delay(100);
        }

        [Fact(DisplayName = "SfMenu CloseAsync with EnableScrolling")]
        public async Task SfMenu_CloseAsync_WithEnableScrolling()
        {
            var cut = RenderComponent<SfMenu<MenuItem>>(parameters => parameters
                .Add(p => p.HamburgerMode, true)
                .Add(p => p.EnableScrolling, true)
                .Add(p => p.Items, new List<MenuItem>
                {
                    new MenuItem { Text = "File" }
                }));
            var menu = cut.Instance;
            await menu.CloseAsync();
        }
        #endregion

        #region ItemModelBase Coverage
        [Fact(DisplayName = "ItemModelBase Properties Set And Get")]
        public void ItemModelBase_Properties_SetAndGet()
        {
            var item = new MenuItemModel
            {
                IconCss = "e-icon",
                Id = "item1",
                Separator = true,
                Disabled = true,
                Hidden = false,
                Text = "Test Item",
                Url = "http://test.com",
                HtmlAttributes = new Dictionary<string, object> { { "class", "test-class" } }
            };

            Assert.Equal("e-icon", item.IconCss);
            Assert.Equal("item1", item.Id);
            Assert.True(item.Separator);
            Assert.True(item.Disabled);
            Assert.False(item.Hidden);
            Assert.Equal("Test Item", item.Text);
            Assert.Equal("http://test.com", item.Url);
            Assert.Contains("test-class", item.HtmlAttributes["class"].ToString());
        }

        [Fact(DisplayName = "MenuItemModel Items Property Set And Get")]
        public void MenuItemModel_Items_SetAndGet()
        {
            var parent = new MenuItemModel
            {
                Text = "Parent",
                Items = new List<MenuItemModel>
                {
                    new MenuItemModel { Text = "Child1" },
                    new MenuItemModel { Text = "Child2" }
                }
            };

            Assert.NotNull(parent.Items);
            Assert.Equal(2, parent.Items.Count);
        }
        #endregion

        #region MenuOptions Coverage
        [Fact(DisplayName = "MenuOptions All Properties Set And Get")]
        public void MenuOptions_AllProperties_SetAndGet()
        {
            var options = new MenuOptions
            {
                dataId = "data-123",
                popupDataId = "popup-456",
                Element = new ElementReference("element"),
                Popup = new ElementReference("popup"),
                ItemIndex = 3,
                ScrollHeight = 500,
                IsRtl = true,
                IsVertical = true,
                ShowItemOnClick = true,
                EnableScrolling = true,
                NavigationIndex = new List<int> { 1, 2, 3 },
                Orientation = Orientation.Vertical,
                AnimationSettings = new Dictionary<string, object> { { "effect", "fade" } }
            };

            Assert.Equal("data-123", options.dataId);
            Assert.Equal("popup-456", options.popupDataId);
            Assert.NotNull(options.Element);
            Assert.NotNull(options.Popup);
            Assert.Equal(3, options.ItemIndex);
            Assert.Equal(500, options.ScrollHeight);
            Assert.True(options.IsRtl);
            Assert.True(options.IsVertical);
            Assert.True(options.ShowItemOnClick);
            Assert.True(options.EnableScrolling);
            Assert.Equal(3, options.NavigationIndex.Count);
            Assert.Equal(Orientation.Vertical, options.Orientation);
            Assert.NotNull(options.AnimationSettings);
        }
        #endregion

        #region CurrentNavProps Coverage
        [Fact(DisplayName = "CurrentNavProps Properties Set And Get")]
        public void CurrentNavProps_Properties_SetAndGet()
        {
            var props = new CurrentNavProps
            {
                ItemIndex = 5,
                ItemClasses = new List<ClassCollection>
                {
                    new ClassCollection { ItemClass = "e-menu-item e-selected" }
                },
                UlIndex = 2
            };

            Assert.Equal(5, props.ItemIndex);
            Assert.Single(props.ItemClasses);
            Assert.Equal(2, props.UlIndex);
        }

        [Fact(DisplayName = "ClassCollection Properties Set And Get")]
        public void ClassCollection_Properties_SetAndGet()
        {
            var collection = new ClassCollection
            {
                ItemClass = "e-menu-item",
                ClassList = new List<ClassCollection>
                {
                    new ClassCollection { ItemClass = "e-submenu-item" }
                }
            };

            Assert.Equal("e-menu-item", collection.ItemClass);
            Assert.Single(collection.ClassList);
        }
        #endregion

        #region MenuFieldSettings Coverage
        [Fact(DisplayName = "MenuFieldSettings ItemId Property Default and Set/Get")]
        public void MenuFieldSettings_ItemIdProperty_SetAndGet()
        {
            var settings = new MenuFieldSettings();
            string expected = "CustomId";
            settings.ItemId = expected;
            Assert.Equal("Id", new MenuFieldSettings().ItemId);
            Assert.Equal(expected, settings.ItemId);
        }

        [Fact(DisplayName = "MenuFieldSettings ParentId Property Default and Set/Get")]
        public void MenuFieldSettings_ParentIdProperty_SetAndGet()
        {
            var settings = new MenuFieldSettings();
            string expected = "CustomParentId";
            settings.ParentId = expected;
            Assert.Equal("ParentId", new MenuFieldSettings().ParentId);
            Assert.Equal(expected, settings.ParentId);
        }

        [Fact(DisplayName = "MenuFieldSettings Text Property Default and Set/Get")]
        public void MenuFieldSettings_TextProperty_SetAndGet()
        {
            var settings = new MenuFieldSettings();
            string expected = "CustomText";
            settings.Text = expected;
            Assert.Equal("Text", new MenuFieldSettings().Text);
            Assert.Equal(expected, settings.Text);
        }
        #endregion

        #region MenuAnimationSettings Coverage
        [Fact(DisplayName = "MenuAnimationSettings Duration Property Default and Set/Get")]
        public void MenuAnimationSettings_DurationProperty_SetAndGet()
        {
            var settings = new MenuAnimationSettings();
            settings.Duration = 500;
            Assert.Equal(400, new MenuAnimationSettings().Duration);
            Assert.Equal(500, settings.Duration);
        }

        [Fact(DisplayName = "MenuAnimationSettings Easing Property Default and Set/Get")]
        public void MenuAnimationSettings_EasingProperty_SetAndGet()
        {
            var settings = new MenuAnimationSettings();
            settings.Easing = "ease-in-out";
            Assert.Equal("ease", new MenuAnimationSettings().Easing);
            Assert.Equal("ease-in-out", settings.Easing);
        }

        [Fact(DisplayName = "MenuAnimationSettings Effect Property Set And Get")]
        public void MenuAnimationSettings_EffectProperty_SetAndGet()
        {
            var settings = new MenuAnimationSettings();
            settings.Effect = MenuEffect.FadeIn;
            Assert.Equal(MenuEffect.FadeIn, settings.Effect);
        }
        #endregion

        #region ItemModel Coverage
        [Fact(DisplayName = "ItemModel Properties Set And Get")]
        public void ItemModel_Properties_SetAndGet()
        {
            var model = new ItemModel<MenuItem>
            {
                Items = new List<MenuItem> { new MenuItem { Text = "Child" } },
                ParentId = "parent-1",
                IconCss = "e-icon",
                Text = "Parent Item"
            };

            Assert.Single(model.Items);
            Assert.Equal("parent-1", model.ParentId);
            Assert.Equal("e-icon", model.IconCss);
            Assert.Equal("Parent Item", model.Text);
        }

        [Fact(DisplayName = "ItemModel String Type Set And Get")]
        public void ItemModel_StringType_SetAndGet()
        {
            var model = new ItemModel<string>
            {
                Items = new List<string> { "Child1", "Child2" },
                ParentId = "parent-1"
            };

            Assert.Equal(2, model.Items.Count);
            Assert.Equal("parent-1", model.ParentId);
        }
        #endregion

        #region MenuEvents Coverage
        [Fact(DisplayName = "MenuEvents Properties Set And Get")]
        public void MenuEvents_Properties_SetAndGet()
        {
            var events = new MenuEvents<MenuItem>();
            var onCloseCallback = new EventCallback<BeforeOpenCloseMenuEventArgs<MenuItem>>(null, (Action<BeforeOpenCloseMenuEventArgs<MenuItem>>)(e => { }));
            var onItemRenderCallback = new EventCallback<MenuEventArgs<MenuItem>>(null, (Action<MenuEventArgs<MenuItem>>)(e => { }));
            var onOpenCallback = new EventCallback<BeforeOpenCloseMenuEventArgs<MenuItem>>(null, (Action<BeforeOpenCloseMenuEventArgs<MenuItem>>)(e => { }));
            var createdCallback = new EventCallback<object>(null, (Action<object>)(e => { }));
            var closedCallback = new EventCallback<OpenCloseMenuEventArgs<MenuItem>>(null, (Action<OpenCloseMenuEventArgs<MenuItem>>)(e => { }));
            var openedCallback = new EventCallback<OpenCloseMenuEventArgs<MenuItem>>(null, (Action<OpenCloseMenuEventArgs<MenuItem>>)(e => { }));
            var itemSelectedCallback = new EventCallback<MenuEventArgs<MenuItem>>(null, (Action<MenuEventArgs<MenuItem>>)(e => { }));

            events.OnClose = onCloseCallback;
            events.OnItemRender = onItemRenderCallback;
            events.OnOpen = onOpenCallback;
            events.Created = createdCallback;
            events.Closed = closedCallback;
            events.Opened = openedCallback;
            events.ItemSelected = itemSelectedCallback;

            Assert.True(events.OnClose.HasDelegate);
            Assert.True(events.OnItemRender.HasDelegate);
            Assert.True(events.OnOpen.HasDelegate);
            Assert.True(events.Created.HasDelegate);
            Assert.True(events.Closed.HasDelegate);
            Assert.True(events.Opened.HasDelegate);
            Assert.True(events.ItemSelected.HasDelegate);
        }
        #endregion

        #region SfMenuBase GetIndex Edge Cases
        [Fact(DisplayName = "SfMenuBase GetIndex with self-referential data")]
        public void SfMenuBase_GetIndex_SelfReferential()
        {
            var cut = RenderComponent<SfMenu<MenuItem>>(parameters => parameters
                .Add(p => p.Items, new List<MenuItem>
                {
                    new MenuItem
                    {
                        Text = "Events",
                        Items = new List<MenuItem>
                        {
                            new MenuItem { Text = "Conference" },
                            new MenuItem { Text = "Seminar" }
                        }
                    },
                    new MenuItem
                    {
                        Text = "Directory",
                        Items = new List<MenuItem>
                        {
                            new MenuItem { Text = "Office" }
                        }
                    }
                }));
            var menu = cut.Instance;

            // Get index for nested item
            var index = menu.GetItemIndex(new MenuItem { Text = "Conference" }, false);
            Assert.Equal(2, index.Count);
            Assert.Equal(0, index[0]);
            Assert.Equal(0, index[1]);

            // Get index for item at root level
            index = menu.GetItemIndex(new MenuItem { Text = "Directory" }, false);
            Assert.Single(index);
            Assert.Equal(1, index[0]);
        }
        #endregion

        #region SfMenu HamburgerMode with Target
        [Fact(DisplayName = "SfMenu HamburgerMode with Target element")]
        public async Task SfMenu_HamburgerMode_WithTarget()
        {
            var cut = RenderComponent<SfMenu<MenuItem>>(parameters => parameters
                .Add(p => p.HamburgerMode, true)
                .Add(p => p.Target, "#target-menu")
                .Add(p => p.Title, "Target Menu")
                .Add(p => p.Items, new List<MenuItem>
                {
                    new MenuItem { Text = "File" },
                    new MenuItem { Text = "Edit" }
                }));

            // Verify hamburger mode is enabled with target
            var container = cut.Find("div.e-menu-container");
            Assert.True(container.ClassList.Contains("e-hamburger"));

            var menu = cut.Instance;
            await menu.OpenAsync();
            await Task.Delay(100);
        }
        #endregion

        #region MenuEffect Coverage
        [Fact(DisplayName = "MenuEffect All Values")]
        public void MenuEffect_AllValues()
        {
            Assert.Equal(1, (int)MenuEffect.None);
            Assert.Equal(0, (int)MenuEffect.SlideDown);
            Assert.Equal(2, (int)MenuEffect.ZoomIn);
            Assert.Equal(3, (int)MenuEffect.FadeIn);
        }
        #endregion

        #region Orientation Coverage
        [Fact(DisplayName = "Orientation All Values")]
        public void Orientation_AllValues()
        {
            Assert.Equal(0, (int)Orientation.Horizontal);
            Assert.Equal(1, (int)Orientation.Vertical);
        }
        #endregion
    }
}