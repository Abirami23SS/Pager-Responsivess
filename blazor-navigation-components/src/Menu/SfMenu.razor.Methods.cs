using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Syncfusion.Blazor.Navigations.Internal;
using System.ComponentModel;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Menu is a graphical user interface that serve as navigation headers for your application.
    /// </summary>
    public partial class SfMenu<TValue> : SfMenuBase<TValue>
    {
        /// <summary>
        /// Gets the index of the menu item from the menu based on the given argument.
        /// </summary>
        /// <param name = "item">Item to be passed to get the index.</param>
        /// <param name = "isUniqueId">Set `true` if it is a unique id.</param>
        public List<int> GetItemIndex(TValue item, bool isUniqueId = false)
        {
            var itemtext = Utils.GetItemProperties<string, TValue>(item, isUniqueId ? Fields.ItemId : Fields.Text);
            return GetIndex(itemtext, Items, new List<int>(), isUniqueId);
        }

        /// <summary>
        /// Opens the menu in the hamburger mode.
        /// </summary>
        public async Task OpenAsync()
        {
            await HeaderClickHandler(true).ConfigureAwait(true);
        }

        /// <summary>
        /// Closes the Menu if it is opened in hamburger mode.
        /// </summary>
        public async Task CloseAsync()
        {
            await HeaderClickHandler().ConfigureAwait(true);
            if (EnableScrolling)
                await InvokeMethod("sfBlazor.ContextMenu.destroyScrollElement", dataId).ConfigureAwait(true);
        }
    }
}
