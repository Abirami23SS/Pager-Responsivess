using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Internal;
using Syncfusion.Blazor.Navigations.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Menu is a graphical user interface that serve as navigation headers for your application.
    /// </summary>
    public partial class SfMenu<TValue> : SfMenuBase<TValue>
    {
        internal bool SubMenuOpen;
        internal SfContextMenu<TValue>? SubMenu;
        internal List<TValue>? SubMenuItems;
        internal SfContextMenu<MenuItemModel>? SelfDataSubMenu;
        internal List<MenuItemModel>? SubMenuItemsModel;
        internal bool isMenuRendered;
        private readonly string id = SfBaseUtils.GenerateID(SFMENU);
        private string? containerClass;
        private bool closeMenu;
        private bool enableScrolling;
        internal double scrollHeight;
        private MenuFieldSettings? fields;
        private bool hamburgerMode;
        internal string dataId = "sfMenu-" + Guid.NewGuid().ToString();
        private double? customLeft;
        private double? customTop;
        private Orientation orientation;
        private bool isOrientationScroll;

        private void Initialize()
        {
            var container = CONTAINER;
            if (HamburgerMode)
            {
                container += HAMBURGER;
                if (string.IsNullOrEmpty(Target) && Orientation == Orientation.Horizontal)
                {
                    closeMenu = true;
                }
                else
                {
                    NavIdx = new List<int> { 0 };
                }
            }

            containerClass = Initialize(container, dataId);
        }

        private async Task HeaderClickHandler(bool open = false)
        {
            if (!open)
            {
                open = closeMenu;
            }

            if (open)
            {
                var cancel = await BeforeOpenCloseEvent(ONOPEN, true).ConfigureAwait(true);
                if (!cancel)
                {
                    NavIdx = new List<int> { 0 };
                    closeMenu = false;
                    SetOpenEventArgs<TValue>(default!, default!);
                }
            }
            else if (hamburgerMode)
            {
                var cancel = await CloseMenuAsync().ConfigureAwait(true);
                if (!cancel)
                {
                    NavIdx.Clear();
                    closeMenu = true;
                }
            }
            else
            {
                await DocumentMouseDownAsync(true, false).ConfigureAwait(true);
            }
        }

        private async Task<bool> BeforeOpenCloseEvent(string eventName, bool isParent = false)
        {
            bool cancel;
            if (MenuItems == null)
            {
                List<TValue>? subItems = isParent ? Items : SubMenuItems;
                var eventArgs = await TriggerBeforeOpenCloseEvent(
                    Items[NavIdx.Count > 0 ? NavIdx[0] : 0],
                    subItems!,
                    eventName,
                    isParent
                ).ConfigureAwait(true);
                cancel = eventArgs.Cancel;
                scrollHeight = eventArgs.ScrollHeight;
                this.customLeft = eventArgs.Left;
                this.customTop = eventArgs.Top;
            }
            else
            {
                List<MenuItemModel>? subItems = isParent ? MenuItems : SubMenuItemsModel;
                var eventArgs = await TriggerBeforeOpenCloseEvent(
                    MenuItems[NavIdx.Count > 0 ? NavIdx[0] : 0],
                    subItems!,
                    eventName,
                    isParent
                ).ConfigureAwait(true);
                cancel = eventArgs.Cancel;
                scrollHeight = eventArgs.ScrollHeight;
                this.customLeft = eventArgs.Left;
                this.customTop = eventArgs.Top;
            }
            return cancel;
        }

        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task DocumentMouseDownAsync(bool refresh = false, bool skipNavIndex = false, bool closeSubMenu = false, bool isFocus = false, bool focusRefresh = false)
        {
            bool cancel = false;
            if (!HamburgerMode)
            {
                for (var index = 0; index < ClsCollection?.Count; index++)
                {
                    if (skipNavIndex && NavIdx.Count > 0 && NavIdx[0] == index)
                    {
                        continue;
                    }
                    ClsCollection[index].ItemClass = SfBaseUtils.RemoveClass(ClsCollection[index].ItemClass, FOCUSED);
                    if (!isFocus)
                    {
                        ClsCollection[index].ItemClass = SfBaseUtils.RemoveClass(ClsCollection[index].ItemClass, SELECTED);
                    }
                }
            }

            if (refresh && ShowItemOnClick && NavIdx.Count > 0)
            {
                if (HamburgerMode)
                {
                    cancel = await CloseMenuAsync(1, false, false, HamburgerMode).ConfigureAwait(true);
                }
                else
                {
                    var index = closeSubMenu ? 1 : 0;
                    if (SelfDataSubMenu == null && SubMenu != null)
                    {
                        cancel = await SubMenu.CloseMenuAsync(index).ConfigureAwait(true);
                    }
                    else if (SelfDataSubMenu != null)
                    {
                        cancel = await SelfDataSubMenu.CloseMenuAsync(index).ConfigureAwait(true);
                    }

                    if (!cancel)
                    {
                        NavIdx.Clear();
                    }
                }
            }

            if (!cancel && HamburgerMode)
            {
                for (var index = 0; index < ClsCollection?.Count; index++)
                {
                    if (skipNavIndex && NavIdx.Count > 0 && NavIdx[0] == index)
                    {
                        continue;
                    }

                    ClsCollection[index].ItemClass = SfBaseUtils.RemoveClass(ClsCollection[index].ItemClass, FOCUSED);
                    if (!isFocus)
                    {
                        ClsCollection[index].ItemClass = SfBaseUtils.RemoveClass(ClsCollection[index].ItemClass, SELECTED);
                    }
                }
            }

            if (refresh || focusRefresh)
            {
                StateHasChanged();
            }
        }

        private async Task BeforeOpenHandler<T>(BeforeOpenCloseMenuEventArgs<T> e)
        {
            scrollHeight = e.ScrollHeight = 0;
            if (e.ParentItem == null)
            {
                bool cancel = await BeforeOpenCloseEvent(ONOPEN).ConfigureAwait(true);
                if (cancel)
                {
                    if (NavIdx.Count > 0)
                    {
                        ClsCollection[NavIdx[0]].ItemClass = SfBaseUtils.RemoveClass(ClsCollection[NavIdx[0]].ItemClass, SELECTED);
                        ClsCollection[NavIdx[0]].ItemClass = SfBaseUtils.AddClass(ClsCollection[NavIdx[0]].ItemClass, FOCUSED);
                        NavIdx.Clear();
                    }

                    SubMenu?.Close();
                    SelfDataSubMenu?.Close();
                    e.Cancel = true;
                }
                else
                {
                    await SetPosition(CALCULATEPOS).ConfigureAwait(true);
                }
            }
            else
            {
                if (Delegates == null)
                {
                    await SfBaseUtils.InvokeEvent(SelfRefDelegates?.OnOpen, e).ConfigureAwait(true);
                }
                else
                {
                    await SfBaseUtils.InvokeEvent(Delegates.OnOpen, e).ConfigureAwait(true);
                }

                scrollHeight = e.ScrollHeight;
            }
        }

        private async Task SetPosition(string name)
        {
            if (NavIdx == null || NavIdx.Count == 0)
            {
                SubMenu?.Close();
                SelfDataSubMenu?.Close();
                return;
            }

            var args = new MenuOptions()
            {
                dataId = dataId,
                Element = Element,
                ItemIndex = NavIdx[0],
                ShowItemOnClick = ShowItemOnClick,
                EnableScrolling = EnableScrolling,
                IsVertical = Orientation == Orientation.Vertical,
                IsRtl = EnableRtl || SyncfusionService.options.EnableRtl,
                ScrollHeight = scrollHeight
            };

            if (MenuItems == null && SubMenu != null)
            {
                args.Popup = SubMenu.Element;
                args.popupDataId = SubMenu.dataId;
            }
            else if (SelfDataSubMenu != null)
            {
                args.Popup = SelfDataSubMenu.Element;
                args.popupDataId = SelfDataSubMenu.dataId;
            }

            if (customLeft != null || customTop != null)
            {
                await InvokeMethod(name, args, EnterKey, customLeft, customTop).ConfigureAwait(true);
            }
            else
            {
                await InvokeMethod(name, args, EnterKey).ConfigureAwait(true);
            }

            EnterKey = false;
            customLeft = null;
            customTop = null;
        }

        private async Task BeforeCloseHandler<T>(BeforeOpenCloseMenuEventArgs<T> e)
        {
            if (e.ParentItem == null)
            {
                bool cancel = await BeforeOpenCloseEvent(ONCLOSE).ConfigureAwait(true);
                if (cancel)
                {
                    ClsCollection[NavIdx[0]].ItemClass = SfBaseUtils.RemoveClass(ClsCollection[NavIdx[0]].ItemClass, FOCUSED);
                    ClsCollection[NavIdx[0]].ItemClass = SfBaseUtils.AddClass(ClsCollection[NavIdx[0]].ItemClass, SELECTED);
                    e.Cancel = true;
                }
                else
                {
                    if (NavIdx.Count > 0)
                    {
                        ClsCollection[NavIdx[0]].ItemClass = SfBaseUtils.RemoveClass(ClsCollection[NavIdx[0]].ItemClass, SELECTED);
                        ClsCollection[NavIdx[0]].ItemClass = SfBaseUtils.AddClass(ClsCollection[NavIdx[0]].ItemClass, FOCUSED);
                    }
                    NavIdx.Clear();
                }
            }
            else
            {
                if (Delegates == null)
                {
                    await SfBaseUtils.InvokeEvent(SelfRefDelegates?.OnClose, e).ConfigureAwait(true);
                }
                else
                {
                    await SfBaseUtils.InvokeEvent(Delegates.OnClose, e).ConfigureAwait(true);
                }
            }
        }

        private async Task OpenedHandler<T>(OpenCloseMenuEventArgs<T> e)
        {
            if (Delegates == null)
            {
                await SfBaseUtils.InvokeEvent(SelfRefDelegates?.Opened, e).ConfigureAwait(true);
            }
            else
            {
                await SfBaseUtils.InvokeEvent(Delegates.Opened, e).ConfigureAwait(true);
            }
        }

        internal void SelfReferentialData()
        {
            if (fields == null)
            {
                fields = Fields;
            }
            else
            {
                Fields = fields;
            }

            MenuItems = new List<MenuItemModel>();
            foreach (var item in Items)
            {
                var itemModel = GetMenuItem(item);
                if (string.IsNullOrEmpty(itemModel.ParentId))
                {
                    MenuItems.Add(new MenuItemModel { Text = itemModel.Text, Disabled = itemModel.Disabled, Hidden = itemModel.Hidden, IconCss = itemModel.IconCss, Id = itemModel.Id, Separator = itemModel.Separator, Url = itemModel.Url });
                }
                else
                {
                    List<int> navIdxes = new List<int>();
                    var SubMenuItems = MenuItems;
                    GetIndex(itemModel.ParentId, MenuItems, navIdxes, true, true);
                    for (var i = 0; i < navIdxes.Count; i++)
                    {
                        if (navIdxes[i] == -1)
                        {
                            break;
                        }

                        if (SubMenuItems[navIdxes[i]].Items == null)
                        {
                            SubMenuItems[navIdxes[i]].Items = new List<MenuItemModel>();
                        }

                        SubMenuItems = SubMenuItems[navIdxes[i]].Items;
                        if (i == navIdxes.Count - 1)
                        {
                            SubMenuItems.Add(new MenuItemModel { Text = itemModel.Text, Disabled = itemModel.Disabled, Hidden = itemModel.Hidden, IconCss = itemModel.IconCss, Id = itemModel.Id, Separator = itemModel.Separator, Url = itemModel.Url });
                        }
                    }
                }
            }

            Fields = new MenuFieldSettings();
            ClsCollection = new List<ClassCollection>();
            StateHasChanged();
        }

        private async Task TriggerOpenCloseEvent<T>(OpenCloseMenuEventArgs<T> e, bool open, bool focus)
        {
            MenuEvents<T>? delegates = SelfRefDelegates == null ? Delegates as MenuEvents<T> : SelfRefDelegates as MenuEvents<T>;
            await SfBaseUtils.InvokeEvent(open ? delegates?.Opened : delegates?.Closed, e).ConfigureAwait(true);
            if (focus)
            {
                await InvokeMethod(FOCUSMENU, dataId, false).ConfigureAwait(true);
            }
        }

        internal void ComponentRefresh()
        {
            StateHasChanged();
        }
    }
}
