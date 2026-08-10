using System;
using System.Collections.Generic;
using Syncfusion.Blazor.Navigations.Internal;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Internal;
using System.ComponentModel;
using System.Globalization;
using Microsoft.AspNetCore.Components.Web;
using System.Linq;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Tab is a content panel to show multiple contents in a compact space. Also, only one tab is active at a time. Each Tab item has an associated content, that will be displayed based on the active Tab.
    /// </summary>
    /// <remarks>
    /// Tab items can be populated by specifying <see cref="TabItem"/> within <see cref="TabItems"/> tag directive.
    /// </remarks>
    /// <example>
    /// In the below code example, a basic tab component initialized with <see cref="TabItems"/> tag directive.
    /// <code><![CDATA[
    /// <SfTab>
    ///     <TabItems>
    ///         <TabItem>
    ///             <ChildContent>
    ///                 <TabHeader Text="Tab 1"></TabHeader>
    ///             </ChildContent>
    ///             <ContentTemplate>
    ///                 <div>Content of tab 1</div>
    ///             </ContentTemplate>
    ///         </TabItem>
    ///         <TabItem>
    ///             <ChildContent>
    ///                 <TabHeader Text="Tab 2"></TabHeader>
    ///             </ChildContent>
    ///             <ContentTemplate>
    ///                 <div>Content of tab 2</div>
    ///             </ContentTemplate>
    ///         </TabItem>
    ///         <TabItem>
    ///             <ChildContent>
    ///                 <TabHeader Text="Tab 3"></TabHeader>
    ///             </ChildContent>
    ///             <ContentTemplate>
    ///                 <div>Content of tab 3</div>
    ///             </ContentTemplate>
    ///         </TabItem>
    ///     </TabItems>
    /// </SfTab>
    /// ]]></code>
    /// </example> 
    public partial class SfTab : SfBaseComponent
    {
        private const string SPACE = " ";
        private const string RTL = "e-rtl";
        private const string TAB_HEADER = "e-tab-header";
        private const string VERTICAL_CLASS = "e-vertical";
        private const string VERTICAL_TAB = "e-vertical-tab";
        private const string VERTICAL_LEFT = "e-vertical-left";
        private const string VERTICAL_RIGHT = "e-vertical-right";
        private const string HORIZONTAL_BOTTOM = "e-horizontal-bottom";
        private const string DISABLE = "e-disable";
        private const string OVERLAY = "e-overlay";
        private const string HIDDEN = "e-hidden";
        private const string AUTO = "auto";
        private const string HUNDRED_PERCENT = "100%";
        private const string CLOSE_SHOW = "e-close-show";
        private const string VERTICAL = "vertical";
        private const string HORIZONTAL = "horizontal";
        private const string ADDING = "Adding";
        private const string ADDED = "Added";
        private const string REMOVING = "Removing";
        private const string REMOVED = "Removed";
        private const string ANIMATION = "Animation";
        private const string CSSCLASS = "CssClass";
        private const string ENABLE_RTL = "EnableRtl";
        private const string HEADER_PLACEMENT = "HeaderPlacement";
        private const string HEIGHT = "Height";
        private const string ITEMS = "Items";
        private const string OVERFLOWMODE = "OverflowMode";
        private const string SCROLLSTEP = "ScrollStep";
        private const string SHOWCLOSEBUTTON = "ShowCloseButton";
        private const string REORDERACTIVETAB = "reorderActiveTab";
        private const string WIDTH = "Width";
        private const string SELECTED_ITEM = "SelectedItem";
        private const string CHILD_ANIMATION = "animation";
        private const string CSSCLASS_NAME = "cssClass";
        private const string ENABLEPERSISTENCE = "enablePersistence";
        private const string RTL_ENABLE = "enableRtl";
        private const string HEADERPLACEMENT = "headerPlacement";
        private const string WIDTHNAME = "width";
        private const string LOAD_ON = "loadOn";
        private const string HEIGHTNAME = "height";
        private const string LOCALENAME = "locale";
        private const string OVERFLOW = "overflowMode";
        private const string SCROLL = "scrollStep";
        private const string SELECTEDITEM = "selectedItem";
        private const string SHOWCLOSE = "showCloseButton";
        private const string TABPREFIX = "tab-";
        private const string TABITEMPREFIX = "tabitem_";
        private const string CLASS = "class";
        private const string ITEM = "e-item";
        private const string ACTIVE = "e-active";
        private const string ITEM_SUFFIX = "e-item-";
        private const string CONTENT_SUFFIX = "e-content-";
        private const string UNDERSCO = "_";
        private const string VERTICAL_ICON = "e-vertical-icon";
        private const string TOP = "top";
        private const string BOTTOM = "bottom";
        private const string ALLOWDRAGANDDROP = "allowDragAndDrop";
        private const string DRAGAREA = "dragArea";
        private const string SWIPEMODE = "swipeMode";
        private const string STYLE = "data-sf-style";
        private List<ToolbarItem> toolbarItems = new List<ToolbarItem>();
        private List<TabItem> visibleItems = new List<TabItem>();
        private int previousIndex;
        private Dictionary<string, object> containerAttributes = new Dictionary<string, object>();
        private bool shouldRender = true;

        #region Private properties
        private string TabClass { get; set; } = "e-control e-tab e-lib";

        private string ToolbarHeight { get; set; } = "auto";

        private string ToolbarWidth { get; set; } = "100%";

        private string? ToolbarCssClass { get; set; }

        private bool IsVerticalIcon { get; set; }

        internal bool IsCreatedEvent { get; set; }

        private bool IsTabScriptLoaded { get; set; }

        private TabItem? activeItem { get; set; }
        #endregion

        #region Internal variables

        internal bool IsTabItemChanged { get; set; }
        internal bool IsSelectedItemChanged { get; set; }

        internal TabEvents? Delegates { get; set; }

        internal string Orientation { get; set; } = HORIZONTAL;

        internal string dataId = "sfTab-" + Guid.NewGuid().ToString();

        internal bool IsPreventFocus { get; set; } = true;
        internal bool isSwitchTabClick;
        internal bool isSwitchTabUpdate;

        internal int tabTargetIndex;
        internal SelectingEventArgs tabSelectingEventArgs;
        internal ToolbarEventArgs toolbarArgs;
        #endregion

        internal static void SetUniqueID(TabItem item)
        {
            if (item != null && item.UniqueID == null)
            {
                item.UniqueID = TABITEMPREFIX + Guid.NewGuid().ToString();
            }
        }

        private static TabItemModel GetTabItemModel(TabItem tabItem)
        {
            TabItemModel item = new TabItemModel();
            item.ID = tabItem.ID;
            item.Content = tabItem.Content;
            item.CssClass = tabItem.CssClass;
            item.Disabled = tabItem.Disabled;
            if (tabItem.Header != null)
            {
                if (item.Header == null)
                {
                    item.Header = new HeaderModel();
                }

                item.Header.IconCss = tabItem.Header.IconCss;
                item.Header.IconPosition = tabItem.Header.IconPosition;
                item.Header.Text = tabItem.Header.Text;
            }

            item.HeaderTemplate = tabItem.HeaderTemplate;
            item.Visible = tabItem.Visible;
            item.TabIndex = tabItem.TabIndex;
            return item;
        }

        #region Private Methods
        private async Task SetToolbarItems()
        {
            IsVerticalIcon = false;
            visibleItems.Clear();
            toolbarItems = new List<ToolbarItem>();
            if (Items == null)
            {
                return;
            }
            bool isAllNotVisible = Items != null && Items.Count > 0 && !Items.Any(x => x.Visible);
            if (isAllNotVisible)
            {
                return;
            }
            
            for (int i = 0; i < Items.Count; i++)
            {
                TabItem item = Items[i];
                if (LoadOn == ContentLoad.Init)
                {
                    item.IsContentRendered = true;
                }
                visibleItems.Add(item);
                SetUniqueID(item);
            }

            if (!PropertyChanges.ContainsKey(SELECTED_ITEM) && IsTabItemChanged && visibleItems.Count > 0 && activeItem != null)
            {
                int itemIndex = visibleItems.IndexOf(activeItem);
                if (itemIndex > -1)
                {
                    SelectedItem = selectedItem = await SfBaseUtils.UpdateProperty(itemIndex, selectedItem, SelectedItemChanged).ConfigureAwait(true);
                }
            }

            if (SelectedItem <= 0)
            {
                if (Items.Count == 0)
                {
                    SelectedItem = selectedItem = await SfBaseUtils.UpdateProperty(-1, selectedItem, SelectedItemChanged).ConfigureAwait(true);
                }
                else
                {
                    SelectedItem = selectedItem = await SfBaseUtils.UpdateProperty(0, selectedItem, SelectedItemChanged).ConfigureAwait(true);
                }
            }

            if (visibleItems.Count > 0 && visibleItems.Count - 1 < SelectedItem)
            {
                SelectedItem = selectedItem = visibleItems.Count - 1;
                SelectedItem = selectedItem = await SfBaseUtils.UpdateProperty(SelectedItem, selectedItem, SelectedItemChanged).ConfigureAwait(true);
            }

            SetActiveItem();

            for (int i = 0; i < Items.Count; i++)
            {
                TabItem item = Items[i];
                List<string> classList = new List<string>();
                if (item.Header != null && (item.Header.IconPosition == TOP || item.Header.IconPosition == BOTTOM))
                {
                    IsVerticalIcon = true;
                }

                if (!string.IsNullOrEmpty(item.CssClass))
                {
                    classList.Add(item.CssClass);
                }

                if (!item.Visible)
                {
                    classList.Add(HIDDEN);
                }

                if (item.Disabled)
                {
                    classList.Add(DISABLE + SPACE + OVERLAY);
                }

                if (item.Header != null)
                {
                    classList.Add("e-i" + item.Header.IconPosition);
                }
#pragma warning disable BL0005 // Component parameter should not be set outside of its component.
                toolbarItems.Add(new ToolbarItem()
                {
                    Id = item.ID ?? "e-item-" + ID + "_" + i,
                    CssClass = string.Join(SPACE, classList.ToArray()),
                    Template = getTabHeader(item)
                });
#pragma warning restore BL0005 // Component parameter should not be set outside of its component.
            }

            if (IsVerticalIcon)
            {
                TabClass = SfBaseUtils.AddClass(TabClass, VERTICAL_ICON);
            }

            if (LoadOn == ContentLoad.Demand && visibleItems.Count > 0)
            {
                if (visibleItems.Count > SelectedItem && visibleItems[SelectedItem] != null && !visibleItems[SelectedItem].IsContentRendered)
                {
                    visibleItems[SelectedItem].IsContentRendered = true;
                }
            }

            if (SelectedItem >= 0 && SelectedItem < visibleItems.Count &&
                (visibleItems[SelectedItem].Disabled || !visibleItems[SelectedItem].Visible))
            {
                var next = GetNextEnabledIndex(SelectedItem);
                if (next != -1 && next != SelectedItem)
                {
                    SelectedItem = selectedItem = await SfBaseUtils.UpdateProperty(next, selectedItem, SelectedItemChanged).ConfigureAwait(true);
                }
                else
                {
                    var previousEnabledIndex = GetPreviousEnabledIndex(SelectedItem);
                    if (previousEnabledIndex != -1)
                    {
                        SelectedItem = selectedItem = await SfBaseUtils.UpdateProperty(previousEnabledIndex, selectedItem, SelectedItemChanged);
                    }
                }                
            }
        }

        private void UpdateHtmlAttributes()
        {
            if (HtmlAttributes != null)
            {
                foreach (var item in HtmlAttributes)
                {
                    if (item.Key == CLASS)
                    {
                        TabClass += SPACE + item.Value;
                    }
                    else if (item.Key == STYLE || item.Key == "style")
                    {
                        if (containerAttributes.ContainsKey(STYLE))
                        {
                            containerAttributes[STYLE] += item.Value.ToString();
                        }
                        else
                        {
                            SfBaseUtils.UpdateDictionary(STYLE, item.Value, containerAttributes);
                        }
                    }
                    else
                    {
                        SfBaseUtils.UpdateDictionary(item.Key, item.Value, containerAttributes);
                    }
                }
            }
        }

        private void UpdateLocalProperties()
        {
            Orientation = HORIZONTAL;
            ToolbarCssClass = TAB_HEADER;
            if (HeaderPlacement == HeaderPosition.Left || HeaderPlacement == HeaderPosition.Right)
            {
                Orientation = VERTICAL;
                TabClass += SPACE + VERTICAL_TAB;
                if (HeaderPlacement == HeaderPosition.Left)
                {
                    TabClass += SPACE + VERTICAL_LEFT;
                }
                else if (HeaderPlacement == HeaderPosition.Right)
                {
                    TabClass += SPACE + VERTICAL_RIGHT;
                }

                ToolbarWidth = AUTO;
                ToolbarHeight = HUNDRED_PERCENT;
            }
            if (HeaderPlacement == HeaderPosition.Bottom)
            {
                TabClass += SPACE + HORIZONTAL_BOTTOM;
            }

            if (EnableRtl || SyncfusionService.options.EnableRtl)
            {
                TabClass += SPACE + RTL;
            }

            if (!string.IsNullOrEmpty(CssClass))
            {
                TabClass += SPACE + CssClass;
            }

            if (ShowCloseButton)
            {
                ToolbarCssClass += SPACE + CLOSE_SHOW;
            }

            if (HeaderPlacement == HeaderPosition.Left)
            {
                ToolbarCssClass += SPACE + VERTICAL_CLASS + SPACE + VERTICAL_LEFT;
            }
            else if (HeaderPlacement == HeaderPosition.Right)
            {
                ToolbarCssClass += SPACE + VERTICAL_CLASS + SPACE + VERTICAL_RIGHT;
            }
            else if (HeaderPlacement == HeaderPosition.Bottom)
            {
                ToolbarCssClass += SPACE + HORIZONTAL_BOTTOM;
            }

            UpdateHtmlAttributes();
        }

        private void ToolbarInitialLoad(ToolbarEventArgs args)
        {
            Task.Yield().GetAwaiter().OnCompleted(async () =>
            {
                await InvokeAsync(StateHasChanged).ConfigureAwait(true);
                if (IsTabScriptLoaded)
                {
                    await InvokeMethod("sfBlazor.Tab.headerReady", new object[] { dataId, true });
                    if (!IsCreatedEvent && !IsDisposed)
                    {
                        IsCreatedEvent = true;
                        await InvokeAsync(CreatedEvent).ConfigureAwait(true);
                    }
                }
            });
        }

        private void ToolbarClickedHandler(ToolbarEventArgs args)
        {
            if (args.TargetParentDataIndex == null || visibleItems[(int)args.TargetParentDataIndex].Disabled)
            {
                return;
            }

            if (args.TargetParentDataIndex.HasValue && args.TargetParentDataIndex != SelectedItem)
            {
                _ = ServerSelect(args.TargetParentDataIndex.Value);
            }
        }

        private void OnKeyDown(KeyboardEventArgs args)
        {
            if(args.Key == "Delete")
            {
                IsPreventFocus = false;
            }
        }
        private void ItemChangeHandler(ToolbarEventArgs args)
        {
            Task.Yield().GetAwaiter().OnCompleted(() =>
            {
                IsTabItemChanged = false;
                _ = InvokeMethod("sfBlazor.Tab.serverItemsChanged", new object[] { dataId, SelectedItem, Animation, IsVerticalIcon, IsPreventFocus });
                IsPreventFocus = true;
            });
        }

        private void OverflowModeChangeHandler(ToolbarEventArgs args)
        {
            _ = InvokeMethod("sfBlazor.Tab.overflowMode", new object[] { dataId, OverflowMode });
        }

        private void DraggedPopupItem(int droppingIndex, int draggingIndex)
        {
            if (Items != null)
            {
                TabItem item = Items[draggingIndex];
                Items.RemoveAt(draggingIndex);
                Items.Insert(droppingIndex, item);
            }
        }

        private void SelectContent(int? targetIndex = null)
        {
            int index = targetIndex ?? SelectedItem;
            if (LoadOn == ContentLoad.Demand)
            {
                if (index >= 0 && visibleItems.Count > index && visibleItems[index] != null && !visibleItems[index].IsContentRendered)
                {
                    visibleItems[index].IsContentRendered = true;
                }
            }
        }

        #endregion

        #region Internal methods

        internal async Task OnPropertyChangeHandler()
        {
            if (PropertyChanges.ContainsKey(CSSCLASS))
            {
                await InvokeMethod("sfBlazor.Tab.setCssClass", new object[] { dataId, CssClass }).ConfigureAwait(true);
            }

            if (PropertyChanges.ContainsKey(SHOWCLOSEBUTTON) && ToolbarCssClass != null)
            {
                if (ShowCloseButton && !ToolbarCssClass.Contains(CLOSE_SHOW, StringComparison.CurrentCulture))
                {
                    ToolbarCssClass += SPACE + CLOSE_SHOW;
                }
                else if (ToolbarCssClass.Contains(CLOSE_SHOW, StringComparison.CurrentCulture))
                {
                    ToolbarCssClass = SfBaseUtils.RemoveClass(ToolbarCssClass, SPACE + CLOSE_SHOW);
                }

                await Toolbar.RefreshOverflowAsync().ConfigureAwait(true);
                await InvokeMethod("sfBlazor.Tab.showCloseButton", new object[] { dataId, ShowCloseButton }).ConfigureAwait(true);
            }

            if (PropertyChanges.ContainsKey(HEADER_PLACEMENT))
            {
                ToolbarCssClass = TAB_HEADER;
                if (ShowCloseButton)
                {
                    ToolbarCssClass += SPACE + CLOSE_SHOW;
                }

                bool previousOrientation = Toolbar.IsVertical;
                if (HeaderPlacement == HeaderPosition.Left || HeaderPlacement == HeaderPosition.Right)
                {
                    Orientation = VERTICAL;
                    ToolbarCssClass += SPACE + VERTICAL_CLASS + SPACE + (HeaderPlacement == HeaderPosition.Right ? VERTICAL_RIGHT : VERTICAL_LEFT);
                    ToolbarHeight = HUNDRED_PERCENT;
                    ToolbarWidth = AUTO;
                    Toolbar.IsVertical = true;
                }
                else if (HeaderPlacement == HeaderPosition.Bottom || HeaderPlacement == HeaderPosition.Top)
                {
                    Orientation = HORIZONTAL;
                    if (HeaderPlacement == HeaderPosition.Bottom)
                    {
                        ToolbarCssClass += SPACE + HORIZONTAL_BOTTOM;
                    }

                    ToolbarHeight = AUTO;
                    ToolbarWidth = HUNDRED_PERCENT;
                    Toolbar.IsVertical = false;
                }

                Toolbar.PreventPropChange = Toolbar.IsVertical != previousOrientation;
                await InvokeMethod("sfBlazor.Tab.headerPlacement", new object[] { dataId, HeaderPlacement, SelectedItem, Toolbar.dataId, ToolbarCssClass, Toolbar.IsVertical, Toolbar.PreventPropChange }).ConfigureAwait(true);
            }

            if (PropertyChanges.ContainsKey("EnableRtl"))
            {
                Toolbar.SetRtl(EnableRtl);
                await InvokeMethod("sfBlazor.Tab.enableRtl", new object[] { dataId, EnableRtl }).ConfigureAwait(true);
            }

            if (PropertyChanges.ContainsKey(ALLOWDRAGANDDROP))
            {
                await InvokeMethod("sfBlazor.Tab.allowDragAndDrop", new object[] { dataId, AllowDragAndDrop }).ConfigureAwait(true);
            }
        }

        internal Dictionary<string, object> GetInstance()
        {
            Dictionary<string, object> tabObj = new Dictionary<string, object>();
            tabObj.Add(CHILD_ANIMATION, Animation);
            tabObj.Add(CSSCLASS_NAME, CssClass);
            tabObj.Add(ENABLEPERSISTENCE, EnablePersistence);
            tabObj.Add(RTL_ENABLE, EnableRtl || SyncfusionService.options.EnableRtl);
            tabObj.Add(HEADERPLACEMENT, HeaderPlacement);
            tabObj.Add(HEIGHTNAME, Height);
            tabObj.Add(LOAD_ON, LoadOn);
            tabObj.Add(LOCALENAME, Locale);
            tabObj.Add(OVERFLOW, OverflowMode);
            tabObj.Add(SCROLL, ScrollStep);
            tabObj.Add(SELECTEDITEM, SelectedItem);
            tabObj.Add(SHOWCLOSE, ShowCloseButton);
            tabObj.Add(REORDERACTIVETAB, ReorderActiveTab);
            tabObj.Add(WIDTHNAME, Width);
            tabObj.Add(ALLOWDRAGANDDROP, AllowDragAndDrop);
            tabObj.Add(DRAGAREA, DragArea);
            tabObj.Add(SWIPEMODE, SwipeMode);
            return tabObj;
        }

        internal async Task ServerSelect(int targetIndex)
        {
            previousIndex = SelectedItem;
            SelectingEventArgs eventArgs = new SelectingEventArgs()
            {
                PreviousIndex = previousIndex,
                SelectedIndex = SelectedItem,
                SelectingIndex = targetIndex,
                IsSwiped = false,
                IsInteracted = true,
                Cancel = false
            };
            await SfBaseUtils.InvokeEvent<SelectingEventArgs>(Delegates?.Selecting, eventArgs).ConfigureAwait(true);
            if (!eventArgs.Cancel)
            {

                isSwitchTabClick = true;
                tabTargetIndex = targetIndex;
                tabSelectingEventArgs = eventArgs;
                await OnClientChanged(targetIndex).ConfigureAwait(true);
                if (LoadOn == ContentLoad.Dynamic)
                {
                    toolbarArgs = await InvokeMethod<ToolbarEventArgs>("sfBlazor.Tab.contentReady", true, new object[] { dataId, targetIndex }).ConfigureAwait(true);
                }
            }
            if (LoadOn == ContentLoad.Init && !eventArgs.Cancel)
            {
                await SwitchTabClick(targetIndex, eventArgs).ConfigureAwait(true);
            }
        }

        internal async Task SwitchTabClick(int targetIndex, SelectingEventArgs eventArgs)
        {
            isSwitchTabClick = false;
            if (LoadOn != ContentLoad.Dynamic)
            {
                toolbarArgs = await InvokeMethod<ToolbarEventArgs>("sfBlazor.Tab.contentReady", true, new object[] { dataId, targetIndex }).ConfigureAwait(true);
            }
            if (AllowDragAndDrop && toolbarArgs != null && toolbarArgs.IsPopupElement)
            {
                DraggedPopupItem((int)toolbarArgs.ToolbarItemIndex, targetIndex);
            }

            if (Delegates?.Selected.HasDelegate == true)
            {
                bool isSwiped = eventArgs?.IsSwiped ?? false;
                SelectEventArgs selectArgs = new SelectEventArgs()
                {
                    PreviousIndex = previousIndex,
                    SelectedIndex = targetIndex,
                    IsSwiped = isSwiped,
                    IsInteracted = true
                };
                await Delegates.Selected.InvokeAsync(selectArgs).ConfigureAwait(true);
            }
        }

        internal async Task OnClientChanged(int selectedValue)
        {
            IsSelectedItemChanged = SelectedItemChanged.HasDelegate;
            SelectContent(selectedValue);
            SelectedItem = selectedItem = await SfBaseUtils.UpdateProperty(selectedValue, selectedItem, SelectedItemChanged).ConfigureAwait(true);
            SetActiveItem();
            if (LoadOn != ContentLoad.Init)
            {
                await InvokeAsync(StateHasChanged).ConfigureAwait(true);
            }
        }

        internal async Task AddItems(List<TabItem> items, int index)
        {
            var temp = index;
            if (Items == null)
            {
                Items = tabitems = new List<TabItem>();
            }

            for (var i = 0; i < items.Count; i++)
            {
                var tabItemToAdd = items[i];
                TabItem itemToInsert;

                bool isExisting = Items.Contains(tabItemToAdd);
                if (isExisting || !tabItemToAdd.Visible)
                {
                    itemToInsert = CloneForInsert(tabItemToAdd);
                    itemToInsert.Visible = isExisting ? true : tabItemToAdd.Visible;
                }
                else
                {
                    itemToInsert = tabItemToAdd;
                }

                Items.Insert(index, itemToInsert);
                SetUniqueID(itemToInsert);

                var visibleIndex = index > visibleItems.Count ? visibleItems.Count : index;
                if (itemToInsert.Visible)
                {
                    visibleItems.Insert(visibleIndex, itemToInsert);
                }

                index = index + 1;
            }

            SfBaseUtils.UpdateDictionary(ITEMS, Items, PropertyChanges);
            if (temp < SelectedItem)
            {
                SelectedItem = selectedItem = await SfBaseUtils.UpdateProperty(SelectedItem + items.Count, selectedItem, SelectedItemChanged).ConfigureAwait(true);
                SetActiveItem();
            }
        }

        private static TabItem CloneForInsert(TabItem tabItemToClone)
        {
            return new TabItem
            {
                ID = null,
                Content = tabItemToClone.Content,
                CssClass = tabItemToClone.CssClass,
                Disabled = tabItemToClone.Disabled,
                Visible = tabItemToClone.Visible,
                TabIndex = tabItemToClone.TabIndex,
                Header = tabItemToClone.Header != null ? new TabHeader
                {
                    Text = tabItemToClone.Header.Text,
                    IconCss = tabItemToClone.Header.IconCss,
                    IconPosition = tabItemToClone.Header.IconPosition
                } : null,
                HeaderTemplate = tabItemToClone.HeaderTemplate,
                ContentTemplate = tabItemToClone.ContentTemplate
            };
        }

        internal async Task RemoveItem(int index)
        {
            if (Items != null && index < Items.Count)
            {
                if (visibleItems.Contains(Items[index]))
                {
                    visibleItems.Remove(Items[index]);
                }

                Items.RemoveAt(index);
                if (index <= SelectedItem)
                {
                    SelectedItem = selectedItem = await SfBaseUtils.UpdateProperty(SelectedItem - 1, selectedItem, SelectedItemChanged).ConfigureAwait(true);
                }
            }

            await SetToolbarItems().ConfigureAwait(true);
            if (Toolbar != null)
            {
                Toolbar.IsItemChanged = true;
            }

            PropertyChanges?.Clear();
            StateHasChanged();
            
            if(Delegates?.Removed.HasDelegate == true)
            {
                RemoveEventArgs removedEventArgs = new RemoveEventArgs()
                {
                    Name = REMOVED,
                    RemovedIndex = index
                };
                await Delegates.Removed.InvokeAsync(removedEventArgs).ConfigureAwait(true);
            }
        }

        private void SetActiveItem()
        {
            if (SelectedItem > -1 && SelectedItem < visibleItems.Count)
            {
                activeItem = visibleItems[SelectedItem];
            }
            else
            {
                activeItem = null;
            }
        }

        #endregion

        #region JSInterop methods

        private async Task CreatedEvent()
        {
            if (Delegates?.Created.HasDelegate == true)
                await Delegates.Created.InvokeAsync(null).ConfigureAwait(true);
        }

        /// <exclude />
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task<bool> OnDragStart(int draggingIndex)
        {
            TabItem draggedItem = null;
            if (Items.Count > 0 && draggingIndex >= 0 && draggingIndex < Items.Count)
            {
                draggedItem = Items[draggingIndex];
            }

            bool isCancel = false;
            if (Delegates?.OnDragStart.HasDelegate == true)
            {
                DragEventArgs dragArgs = new DragEventArgs()
                {
                    Index = draggingIndex,
                    DraggedItem = draggedItem
                };
                await Delegates.OnDragStart.InvokeAsync(dragArgs).ConfigureAwait(true);
                isCancel = dragArgs.Cancel;
            }
            return isCancel;
        }

        /// <exclude />
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task<bool> Dragged(int droppingIndex, int draggingIndex, int left, int top)
        {
            TabItem draggedItem = null;
            TabItem droppedItem = null;
            if (Items.Count > 0 && draggingIndex >= 0 && droppingIndex >= 0 && draggingIndex < Items.Count && droppingIndex < Items.Count)
            {
                draggedItem = Items[draggingIndex];
                droppedItem = Items[droppingIndex];
            }

            bool isCancel = false;
            if (Delegates?.Dragged.HasDelegate == true)
            {
                DragEventArgs dropArgs = new DragEventArgs()
                {
                    Index = droppingIndex,
                    DraggedItem = draggedItem,
                    DroppedItem = droppedItem,
                    Left = left,
                    Top = top
                };
                await Delegates.Dragged.InvokeAsync(dropArgs).ConfigureAwait(true);
                isCancel = dropArgs.Cancel;
            }
            if (!isCancel)
            {
                if (Items != null)
                {
                    TabItem item = Items[draggingIndex];
                    Items.RemoveAt(draggingIndex);
                    Items.Insert(droppingIndex, item);
                }

                SelectedItem = selectedItem = await SfBaseUtils.UpdateProperty(droppingIndex, selectedItem, SelectedItemChanged).ConfigureAwait(true);
                if (EnablePersistence)
                {
                    await InvokeMethod("window.localStorage.setItem", new object[] { $"tab{ID}", SelectedItem.ToString(CultureInfo.InvariantCulture) }).ConfigureAwait(true);
                }
                await UpdateToolbarItems().ConfigureAwait(true);
            }
            else
            {
                await UpdateToolbarItems().ConfigureAwait(true);
            }

            return isCancel;
        }

        internal async Task UpdateToolbarItems()
        {
            await SetToolbarItems().ConfigureAwait(true);
            if (Toolbar != null)
            {
                Toolbar.IsItemChanged = true;
            }

            PropertyChanges?.Clear();
            await InvokeAsync(StateHasChanged).ConfigureAwait(true);
        }

        /// <exclude />
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task SelectingEvent(SelectingEventArgs args, int? dataIndex)
        {
            previousIndex = SelectedItem;
            var selectedItem = Items[(int)dataIndex];
            int selectingIndex = visibleItems.Where(x => x.Visible).ToList().IndexOf(selectedItem);
            if (args != null)
            {
                args.SelectingIndex = selectingIndex;
                if (Delegates?.Selecting.HasDelegate == true)
                    await Delegates.Selecting.InvokeAsync(args).ConfigureAwait(true);
                if (!args.Cancel && dataIndex != null)
                {
                    isSwitchTabUpdate = true;
                    if (dataIndex.HasValue)
                    {
                        tabTargetIndex = dataIndex.Value;
                    }
                    tabSelectingEventArgs = args;
                    await OnClientChanged(dataIndex.Value).ConfigureAwait(true);
                }
                if (LoadOn == ContentLoad.Init && !args.Cancel)
                {
                    await SwitchTabUpdate(dataIndex, args).ConfigureAwait(true);
                }
            }
        }
        internal async Task SwitchTabUpdate(int? dataIndex, SelectingEventArgs args)
        {
			isSwitchTabUpdate = false;
            await InvokeMethod("sfBlazor.Tab.selectingContent", new object[] { dataId, dataIndex.Value }).ConfigureAwait(true);

            bool preventFocus = args.IsInteracted;
            if (Delegates?.Selected.HasDelegate == true)
            {
                SelectEventArgs selectArgs = new SelectEventArgs()
                {
                    PreviousIndex = previousIndex,
                    SelectedIndex = dataIndex.Value,
                    IsSwiped = args.IsSwiped,
                    IsInteracted = args.IsSwiped || args.IsInteracted,
                    PreventFocus = preventFocus
                };
                await Delegates.Selected.InvokeAsync(selectArgs).ConfigureAwait(true);
                preventFocus = selectArgs.PreventFocus;
            }
            if (!preventFocus)
            {
                await InvokeMethod("sfBlazor.Tab.focusSelectedTab", new object[] { dataId, preventFocus }).ConfigureAwait(true);
            }
        }
        
        private int GetNextEnabledIndex(int start)
        {
            if (visibleItems == null || visibleItems.Count == 0)
                return -1;

            for (int i = start + 1; i < visibleItems.Count; i++)
            {
                var it = visibleItems[i];
                if (it != null && it.Visible && !it.Disabled)
                    return i;
            }
            return -1;
        }

        private int GetPreviousEnabledIndex(int startIndex)
        {
            if (visibleItems == null || visibleItems.Count == 0)
                return -1;

            for (int i = startIndex - 1; i >= 0; i--)
            {
                var previousEnabledItem = visibleItems[i];
                if (previousEnabledItem != null && previousEnabledItem.Visible && !previousEnabledItem.Disabled)
                    return i;
            }
            return -1;
        }
        #endregion
    }
}
