using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syncfusion.Blazor.Navigations
{
    public partial class SfDropDownTree<TValue, TItem>
    {
        /// <summary>
        /// Opens the DropDownTree popup that displays the list of tree items.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents any asynchronous action.</returns>
        public async Task ShowPopupAsync()
        {
            if (Disabled)
            {
                return;
            }
            if (!isPopupOpen)
            {
                showPopupTree = true;
                await ShowPopup().ConfigureAwait(true);
            }
        }

        /// <summary>
        /// Close the DropDownTree popup that displays the list of tree items.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents any asynchronous action.</returns> 
        public async Task HidePopupAsync()
        {
            if (Disabled)
            {
                return;
            }
            if (isPopupOpen)
            {
                await InvokeMethod("sfBlazor.DropDownTree.invokePopupEvent", new object[] { dataId, currentValue, null! }).ConfigureAwait(true);
            }
        }

        /// <summary>
        /// Clear out the selected values from the <see cref="SfDropDownTree{TValue,TItem}"/> component and set the <see cref="Value"/> property as <c>null</c>.
        /// </summary>
        /// <returns> A <see cref="Task"/> that represents any asynchronous action. </returns>
        public async Task ClearAsync()
        {
            await ClearAll(true).ConfigureAwait(true);
            isClearButtonClick = false;
        }

        /// <summary>
        /// Selects or deselects the entire collection of items of the <see cref="SfDropDownTree{TValue,TItem}"/> based on the state parameter.
        /// </summary>
        /// <param name = "state"> Set <c>true</c> or <c>false</c> to select or unselect the entire list items.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation of selecting all the items. </returns>
        public async Task SelectAllAsync(bool state = true)
        {
            try
            {
                if (ShowCheckBox)
                {
                    if (state)
                    {
                        isSelectAllChecked = true;
                        checkedNodes = AllData.Keys.ToArray();
                    }
                    else
                    {
                        await ResetValue().ConfigureAwait(true);
                    }
                }
                else if (AllowMultiSelection)
                {
                    if (!state)
                    {
                        await ResetValue().ConfigureAwait(true);
                    }
                    else
                    {
                        selectedNodes = AllData.Keys.ToArray();
                    }
                }
                await SetMultiSelect(false, true).ConfigureAwait(true);
            }
            catch
            {
                if (!IsDisposed)
                    throw;
            }
        }

        /// <summary>
        /// Gets the updated data source of Dropdown Tree after performing some operation like
        /// node selecting/unselecting, node expanding/collapsing, node checking/unchecking.
        /// When the ID of tree node is passed as arguments for this method then it will return the updated data source of the corresponding node otherwise, it will return the entire updated data source of the Dropdown Tree.
        /// </summary>
        /// <param name="nodeID">Specifies ID of TreeView node. </param>
        /// <returns>"Return Data". </returns>
        public List<TItem> GetTreeViewData(string nodeID = null)
        {
            try
            {
                return GetTreeData(nodeID);
            }
            catch
            {
                if (!IsDisposed)
                    throw;
                return null!;
            }
        }

        /// <summary>
        /// This method is used to apply the pending changes and render the Dropdown Tree component again.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous action.</returns>
        public async Task RefreshAsync()
        {
            await InvokeAsync(() => StateHasChanged()).ConfigureAwait(true);
            if (showPopupTree)
                await RefreshPopup().ConfigureAwait(true);
        }
    }
}
