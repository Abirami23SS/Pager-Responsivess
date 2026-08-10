using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Syncfusion.Blazor.Navigations
{
    public partial class SfTab : SfBaseComponent
    {
        /// <summary>
        /// Adds new items to the Tab that accepts a list of Tab items.
        /// </summary>
        /// <param name="items">A list of items that are added to the Tab.</param>
        /// <param name="index">Specifies an index value that determines where the items to be added.</param>
        /// <returns><see cref="System.Threading.Tasks.Task"/>.</returns>
        public async Task AddTab(List<TabItem> items, int index)
        {
            if (Items != null && (index <= Items.Count || index <= 0))
            {
                List<TabItemModel> addedTabItems = new List<TabItemModel>();
                if (items != null)
                {
                    for (var i = 0; i < items.Count; i++)
                    {
                        TabItemModel item = GetTabItemModel(items[i]);
                        addedTabItems.Add(item);
                    }
                }

                bool isCancelled = false;
                if(Delegates?.Adding.HasDelegate == true)
                {
                    AddEventArgs addingEventArgs = new AddEventArgs()
                    {
                        Name = ADDING,
                        Cancel = false,
                        AddedItems = addedTabItems
                    };
                    await Delegates.Adding.InvokeAsync(addingEventArgs).ConfigureAwait(true);
                    isCancelled = addingEventArgs.Cancel;
                }
                if (items != null && !isCancelled)
                {
                    await AddItems(items, index).ConfigureAwait(true);
                    if(Delegates?.Added.HasDelegate == true)
                    {
                        AddEventArgs addEventArgs = new AddEventArgs()
                        {
                            Name = ADDED,
                            AddedItems = addedTabItems
                        };
                        await Delegates.Added.InvokeAsync(addEventArgs).ConfigureAwait(true);
                    }
                }
            }
        }

        /// <summary>
        /// Removes a particular Tab based on index from the Tabs.
        /// </summary>
        /// <param name="index">Index of tab item that is going to be removed.</param>
        /// <returns><see cref="System.Threading.Tasks.Task"/>.</returns>
        [JSInvokable]
        public async Task RemoveTab(int index)
        {
            if (Items != null && index < Items.Count)
            {
                if (Items[index].Disabled)
                {
                    return;
                }
                bool isCancelled = false;
                if(Delegates?.Removing.HasDelegate == true)
                {
                    RemoveEventArgs removingEventArgs = new RemoveEventArgs()
                    {
                        Name = REMOVING,
                        Cancel = false,
                        RemovedIndex = index
                    };
                    await Delegates.Removing.InvokeAsync(removingEventArgs).ConfigureAwait(true);
                    isCancelled = removingEventArgs.Cancel;
                }
                if (!isCancelled)
                {
                    await RemoveItem(index).ConfigureAwait(true);
                }
            }
        }

        /// <summary>
        /// Enables or disables a particular tab item. On passing the value as `false`, the tab will be disabled.
        /// </summary>
        /// <param name="index">Index value of target Tab item.</param>
        /// <param name="isEnable">Specify a Boolean value that determines whether the command should be enabled or disabled. By default, isEnable has true.</param>
        /// <returns><see cref="System.Threading.Tasks.Task"/>.</returns>
        public async Task EnableTabAsync(int index, bool isEnable)
        {
            if (Items[index] != null)
            {
                Items[index].EnableItem(!isEnable);
            }

            await InvokeMethod("sfBlazor.Tab.enableTab", new object[] { dataId, index, isEnable }).ConfigureAwait(true);
        }

        /// <summary>
        /// Shows or hides a particular Tab based on the specified index.
        /// </summary>
        /// <param name="index">Index value of target item.</param>
        /// <param name="isHide">Based on this Boolean value, item will be hide (false) or show (true).</param>
        /// <returns><see cref="System.Threading.Tasks.Task"/>.</returns>
        public async Task HideTabAsync(int index, bool? isHide = null)
        {
            if (Items != null && index >= 0 && index < Items.Count)
            {
                var item = Items[index];
                bool newVisible = isHide.HasValue ? !isHide.Value : !item.Visible;
                item.SetVisible(newVisible);
                IsTabItemChanged = true;
            }
            await InvokeMethod("sfBlazor.Tab.hideTab", new object[] { dataId, index, isHide }).ConfigureAwait(true);
            if (OverflowMode == OverflowMode.Popup)
            {
                await Toolbar.RefreshOverflowAsync().ConfigureAwait(true);
            }
        }

        /// <summary>
        /// Select (activate) a particular tab based on the specified index.
        /// </summary>
        /// <param name="index">Index is used for selecting an item from the Tab.</param>
        /// <returns><see cref="System.Threading.Tasks.Task"/>.</returns>
        public async Task SelectAsync(int index)
        {
            await InvokeMethod("sfBlazor.Tab.select", new object[] { dataId, index }).ConfigureAwait(true);
        }

        /// <summary>
        /// Specifies the value to disable or enable the Tabs component. When set to `true`, the component will be disabled.
        /// </summary>
        /// <param name="disable">Based on this Boolean value, Tab will be enabled (false) or disabled (true).</param>
        /// <returns><see cref="System.Threading.Tasks.Task"/>.</returns>
        public async Task DisableAsync(bool disable)
        {
            await InvokeMethod("sfBlazor.Tab.disable", new object[] { dataId, disable }).ConfigureAwait(true);
        }

        /// <summary>
        /// Return a tab item element based on the specified index.
        /// </summary>
        /// <param name="index">Index is used for accessing tab header item element from the Tab.</param>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        public async Task<DOM> GetTabItem(int index)
        {
            var dom = await InvokeMethod<DOM>("sfBlazor.Tab.getTabItem", true, new object[] { Toolbar.Element, index }).ConfigureAwait(true);
            dom.JsRuntime = JSRuntime;
            return dom;
        }

        /// <summary>
        /// Returns the tab content element based on the specified index.
        /// </summary>
        /// <param name="index">Index is used for accessing tab content element from the Tab.</param>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        public async Task<DOM> GetTabContent(int index)
        {
            var dom = await InvokeMethod<DOM>("sfBlazor.Tab.getTabContent", true, new object[] { dataId, index }).ConfigureAwait(true);
            dom.JsRuntime = JSRuntime;
            return dom;
        }

        /// <summary>
        /// Refresh the entire tabs component.
        /// </summary>
        /// <returns><see cref="System.Threading.Tasks.Task"/>.</returns>
        public async Task RefreshAsync()
        {
            await InvokeAsync(StateHasChanged).ConfigureAwait(true);
            if (Toolbar != null)
            {
                await Toolbar.RefreshOverflowAsync().ConfigureAwait(true);
            }

            await InvokeMethod("sfBlazor.Tab.refresh", new object[] { dataId }).ConfigureAwait(true);
        }

        /// <summary>
        /// Retrieves a tab item based on the specified index. 
        /// </summary>
        /// <param name="index">Index is used for accessing tab item from the Tab</param> - Index -> index
        /// <returns>Returns the tab item</returns>
        public TabItem GetTabItemByIndex(int index)
        {
            if(index >= 0 && Items != null && Items.Count > 0 && Items.Count > index)
            {
                return Items[index];
            }
            return null;
        }

        /// <summary> 
        /// Retrieves a tab item based on the specified id. 
        /// </summary> 
        /// <param name="id"> id is used for accessing tab item from the Tab </param> 
        /// <returns>Returns the tab item</returns> 
        public TabItem GetTabItemById(string id)
        {
           return Items?.Find(x => x.ID == id);
        }
    }
}
