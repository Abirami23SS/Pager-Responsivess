using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel;
using Syncfusion.Blazor.Navigations.Internal;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// ContextMenu is a graphical user interface that appears on the user right click/touch hold operation.
    /// </summary>
    public partial class SfContextMenu<TValue> : SfMenuBase<TValue>
    {
        /// <summary>
        /// Closes the context menu if it is opened.
        /// </summary>
        public void Close()
        {
            NavIdx = new List<int>();
            ClsCollection = new List<ClassCollection>();
            StateHasChanged();
        }

        /// <summary>
        /// Opens the  context menu in specified position. If the positions are not specified, the context menu
        /// will open at its rendered position.
        /// </summary>
        /// <param name = "clientX">Specifies the horizontal position of the context menu.</param>
        /// <param name = "clientY">Specifies the vertical position of the context menu.</param>
        /// <param name = "enableCollision">Specifies the collision detection of the context menu.</param>
        /// <exclude/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [ObsoleteAttribute("This method is obsolete. Use OpenAsync instead.", false)]
        public void Open(double? clientX = null, double? clientY = null, bool enableCollision = false)
        {
            if (Fields == null)
            {
                return;
            }

            Left = clientX;
            Top = clientY;
            if ((Left != null && Top != null) || IsMenu)
            {
                manualOpen = true;
                isCollision = enableCollision;
                OpenEventArgs = new OpenCloseMenuEventArgs<TValue>() { Name = OPENED, Element = Element, Items = Items, ParentItem = default };
                cmenuHidden = "visibility: hidden;";
            }

            ClsCollection = new List<ClassCollection>();
            NavIdx = new List<int> { 0 };
            StateHasChanged();
        }

        /// <summary>
        /// Opens the context menu at a particular location on the screen, determined by the specified x and y coordinates.
        /// If coordinates are not specified, the context menu will appear at its default position.
        /// </summary>
        /// <param name = "clientX">Specifies the client x position of the context menu.</param>
        /// <param name = "clientY">Specifies the client y position of the context menu.</param>
        /// <param name = "enableCollision"> Set `true`/`false` to enable/disable the collision detection of the context menu.</param>
        /// <returns>A Task that represents the asynchronous operation of opening a context menu. It completes when the component has finished the action.</returns>
        public async Task OpenAsync(double? clientX = null, double? clientY = null, bool enableCollision = false)
        {
            if (Fields == null)
            {
                return;
            }

            Left = clientX;
            Top = clientY;
            if ((Left != null && Top != null) || IsMenu)
            {
                manualOpen = true;
                isCollision = enableCollision;
                await this.OpenContextMenuAsync((double)clientX, (double)clientY).ConfigureAwait(true);
            }

            ClsCollection = new List<ClassCollection>();
            NavIdx = new List<int> { 0 };
            StateHasChanged();
        }
    }
}
