using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Syncfusion.Blazor.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syncfusion.Blazor.Navigations.Internal
{
    public partial class CreateMenuItem<TValue, TItem>
    {
        [CascadingParameter]
        private SfMenu<TValue>? Parent { get; set; }

        [Parameter]
        public List<TItem> Items { get; set; }

        private List<TValue>? SelfItem { get; set; }

        private async Task MenuHoverHandler(TItem item)
        {
            if (Parent?.NavIdx == null)
            {
                return;
            }

            if (Parent.HamburgerMode)
            {
                await Parent.OpenCloseSubMenu(item).ConfigureAwait(true);
                return;
            }

            var index = Items.IndexOf(item);
            if (Parent.NavIdx.Count == 0)
            {
                if (Parent.ShowItemOnClick)
                {
                    if (!Parent.ClsCollection[index].ItemClass.Contains(SELECTED, StringComparison.Ordinal))
                    {
                        await ClearAndUpdate(index, FOCUSED).ConfigureAwait(true);
                    }
                }
                else if (index > -1)
                {
                    await UpdateState(item, index, FOCUSED).ConfigureAwait(true);
                }
            }
            else
            {
                if (Parent.NavIdx[0] != index)
                {
                    if (Parent.ShowItemOnClick)
                    {
                        await ClearAndUpdate(index, FOCUSED, true).ConfigureAwait(true);
                    }
                    else
                    {
                        await UpdateState(item, index, FOCUSED).ConfigureAwait(true);
                    }
                }
                else if (Parent.ShowItemOnClick)
                {
                    await Parent.DocumentMouseDownAsync(false, true).ConfigureAwait(true);
                }
            }
        }

        private async Task ClearAndUpdate(int index, string stateCls, bool skipNavIndex = false)
        {
            if (Parent != null && !Parent.ClsCollection[index].ItemClass.Contains(stateCls, StringComparison.Ordinal))
            {
                await Parent.DocumentMouseDownAsync(false, skipNavIndex, false, stateCls == FOCUSED).ConfigureAwait(true);
                Parent.ClsCollection[index].ItemClass = SfBaseUtils.AddClass(Parent.ClsCollection[index].ItemClass, stateCls);
            }
        }

        private async Task ItemClickHandler(TItem item, System.EventArgs e, bool isEnterKey = false)
        {
            if (Parent != null && Parent.HamburgerMode)
            {
                if (Parent.NavIdx.Count == 1)
                {
                    await Parent.DocumentMouseDownAsync().ConfigureAwait(true);
                }

                if (Parent.MenuItems == null)
                {
                    await Parent.ClickHandler(Parent.Items as List<TItem>, item, e, false, false, false, true).ConfigureAwait(true);
                }
                else
                {
                    await Parent.ClickHandler(Parent.MenuItems!, item as MenuItemModel, e, false, false, false, true).ConfigureAwait(true);
                }

                Parent.ComponentRefresh();
                return;
            }

            if (Parent != null && Parent.Delegates == null)
            {
                await SfBaseUtils.InvokeEvent(Parent.SelfRefDelegates?.ItemSelected, new MenuEventArgs<TItem>() { Name = SELECT, Item = item, Event = e }).ConfigureAwait(true);
            }
            else if (Parent != null)
            {
                await SfBaseUtils.InvokeEvent(Parent.Delegates.ItemSelected, new MenuEventArgs<TItem>() { Name = SELECT, Item = item, Event = e }).ConfigureAwait(true);
            }

            var index = Items.IndexOf(item);
            if (Parent != null && Parent.ShowItemOnClick || isEnterKey)
            {
                if (Parent != null && Parent.ClsCollection[index].ItemClass.Contains(SELECTED, StringComparison.Ordinal))
                {
                    bool cancel = false;
                    if (Parent.NavIdx.Count > 0)
                    {
                        if (Parent.SelfDataSubMenu == null)
                        {
                            cancel = await Parent.SubMenu.CloseMenuAsync().ConfigureAwait(true);
                        }
                        else
                        {
                            cancel = await Parent.SelfDataSubMenu.CloseMenuAsync().ConfigureAwait(true);
                        }
                    }

                    if (!cancel)
                    {
                        Parent.NavIdx.Clear();
                        Parent.ClsCollection[index].ItemClass = SfBaseUtils.RemoveClass(Parent.ClsCollection[index].ItemClass, SELECTED);
                    }
                }
                else
                {
                    await UpdateState(item, index, SELECTED).ConfigureAwait(true);
                }
            }
            else
            {
                if (Parent != null && Parent.ClsCollection[index].ItemClass.Contains(FOCUSED, StringComparison.Ordinal))
                {
                    await Parent.DocumentMouseDownAsync().ConfigureAwait(true);
                    Parent.ClsCollection[index].ItemClass = SfBaseUtils.AddClass(Parent.ClsCollection[index].ItemClass, SELECTED);
                }
            }
        }

        internal async Task KeyDownHandler(TItem item, KeyboardEventArgs e, bool isUl = false)
        {
            if (Parent != null && Parent.HamburgerMode)
            {
                if (!isUl && e.Code == ENTER && Parent.NavIdx.Count == 1)
                {
                    await Parent.DocumentMouseDownAsync().ConfigureAwait(true);
                }

                if (Parent.MenuItems == null)
                {
                    await Parent.KeyActionHandler(Parent.Items as List<TItem>, item, e, isUl, false, true).ConfigureAwait(true);
                }
                else
                {
                    await Parent.KeyActionHandler(Parent.MenuItems!, item as MenuItemModel, e, isUl, false, true).ConfigureAwait(true);
                }

                Parent.ComponentRefresh();
            }
            else
            {
                if (Parent != null && Parent.Orientation == Orientation.Vertical)
                {
                    if (e.Code == ARROWUP || e.Code == ARROWDOWN)
                    {
                        await Parent.KeyActionHandler(Items, item, e, isUl, true).ConfigureAwait(true);
                    }
                    else if (!isUl && (Parent.EnableRtl ? e.Code == ARROWLEFT : e.Code == ARROWRIGHT))
                    {
                        await ItemClickHandler(item, e, true).ConfigureAwait(true);
                    }
                }
                else if (Parent != null && (e.Code == ARROWLEFT || e.Code == ARROWRIGHT))
                {
                    e.Code = e.Code == ARROWLEFT ? ARROWUP : ARROWDOWN;
                    await Parent.KeyActionHandler(Items, item, e, isUl, true).ConfigureAwait(true);
                }

                if (!isUl && Parent != null && (e.Code == ENTER || (e.Key == ARROWDOWN && Parent.Orientation == Orientation.Horizontal)))
                {
                    Parent.EnterKey = true;
                    await ItemClickHandler(item, e, true).ConfigureAwait(true);
                }
                if (Parent != null && (e.Code == HOME || e.Code == END || e.Code == "Tab"))
                {
                    await Parent.KeyActionHandler(Items, item, e, isUl, true).ConfigureAwait(true);
                }
            }
        }

        private async Task UpdateState(TItem item, int index, string stateCls)
        {
            if (Parent == null) return;
            var subItems = Utils.GetItemProperties<List<TItem>, TItem>(item, Parent.Fields?.Children);
            bool cancel = false;
            if (Parent.NavIdx.Count > 0)
            {
                if (Parent.SelfDataSubMenu == null && Parent.SubMenu != null)
                {
                    cancel = await Parent.SubMenu.CloseMenuAsync().ConfigureAwait(true);
                }
                else if (Parent.SelfDataSubMenu != null)
                {
                    cancel = await Parent.SelfDataSubMenu.CloseMenuAsync().ConfigureAwait(true);
                }
            }

            if (!cancel)
            {
                if (subItems == null)
                {
                    Parent.NavIdx.Clear();
                    await ClearAndUpdate(index, stateCls).ConfigureAwait(true);
                }
                else
                {
                    Parent.NavIdx = new List<int>() { index };
                    if (Parent.MenuItems == null)
                    {
                        Parent.SubMenuItems = subItems as List<TValue>;
                    }
                    else
                    {
                        Parent.SubMenuItemsModel = subItems as List<MenuItemModel>;
                    }

                    await Parent.DocumentMouseDownAsync().ConfigureAwait(true);
                    Parent.ClsCollection[Parent.NavIdx[0]].ItemClass = SfBaseUtils.AddClass(Parent.ClsCollection[Parent.NavIdx[0]].ItemClass, SELECTED);
                    if (Parent.SubMenuOpen)
                    {
                        Parent.ComponentRefresh();
                        OpenSubMenus();
                    }
                    else
                    {
                        Parent.SubMenuOpen = true;
                        Parent.ComponentRefresh();
                    }
                }
            }
        }

        private void OpenSubMenus()
        {
            Parent.SubMenu?.Open();
            Parent.SelfDataSubMenu?.Open();
        }

        private CurrentNavProps GetCurrentNavProps()
        {
            List<TItem>? items;
            if (Parent?.MenuItems == null)
            {
                items = Parent?.Items as List<TItem>;
            }
            else
            {
                if ((SelfItem != null && Parent.Items != SelfItem) || (SelfItem?.Count == 0 && Parent.Items.Count == 0 && Parent.MenuItems.Count != 0))
                {
                    Parent.SelfReferentialData();
                }
                SelfItem = Parent.Items;
                items = Parent.MenuItems as List<TItem>;
            }

            List<ClassCollection>? itemClasses = Parent?.ClsCollection;
            int index = -1;
            int ulIndex = -1;
            for (var i = 1; i < Parent?.NavIdx.Count; i++)
            {
                ulIndex++;
                if (items == Items)
                {
                    index = Parent.NavIdx[i];
                    break;
                }

                itemClasses = itemClasses?[Parent.NavIdx[i]].ClassList;
                if (items != null)
                {
                    items = Utils.GetItemProperties<List<TItem>, TItem>(items[Parent.NavIdx[i]], Parent.Fields.Children);
                }
            }

            return new CurrentNavProps { ItemIndex = index, ItemClasses = itemClasses!, UlIndex = ulIndex };
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender).ConfigureAwait(true);
            if (Parent != null && !Parent.isMenuRendered)
            {
                Parent.isMenuRendered = true;
                if (Parent.EnableScrolling)
                {
                    var args = new MenuOptions() { dataId = Parent.dataId, Element = Parent.Element, EnableScrolling = Parent.EnableScrolling, IsRtl = Parent.EnableRtl || Parent.SyncfusionService.options.EnableRtl };
                    await Parent.InvokeMethod(UPDATESCROLL, args.dataId, args.EnableScrolling, args.IsRtl).ConfigureAwait(true);
                }
            }
        }

        private static Dictionary<string, object> GetAttributes(Dictionary<string, object> htmlAttributes, string type)
        {
            var attr = new Dictionary<string, object>();
            switch (type)
            {
                case "anchor":
                    {
                        if (htmlAttributes.TryGetValue("anchor", out var anchorValue))
                        {
                            attr = (Dictionary<string, object>)anchorValue;
                        }
                        break;
                    }
                default:
                    {
                        attr = htmlAttributes.ToDictionary(entry => entry.Key, entry => entry.Value);
                        if (htmlAttributes.TryGetValue("anchor", out var value))
                        {
                            var anchorAttr = new Dictionary<string, object>();
                            anchorAttr = (Dictionary<string, object>)value;
                            attr.Remove("anchor");
                            htmlAttributes["anchor"] = anchorAttr;
                        }
                        break;
                    }
            }
            return attr;
        }
    }
}
