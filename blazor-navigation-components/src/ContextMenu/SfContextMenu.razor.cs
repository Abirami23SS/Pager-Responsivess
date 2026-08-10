using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Navigations.Internal;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Syncfusion.Blazor.Internal;
using System.Runtime.CompilerServices;
using System.Linq;
using System;

[assembly: InternalsVisibleTo("Syncfusion.Blazor.FileManager, PublicKey=00240000048000009400000006020000002400005253413100040000010001002382fcb1069523ce72d849497a557a445c151eaf4007aa79adef551a8204ca7f728e5378607d85695b16f129ec35bf4af15dcf6d3581deb8bb0debb239c33e7f1271a37c7f60f1044ae417730f5082abee5f9ec568a8a4cef04074394755706376e982dc6f9d15430faaad385ae8f00a77ef1c97517f1a1517004ce78028b9ce")]

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// ContextMenu is a graphical user interface that appears on the user right click/touch hold operation.
    /// </summary>
    /// <example>
    /// In the below code example, showcases a basic context menu.
    /// <code><![CDATA[
    /// <SfContextMenu Target="#contextmenutarget" TValue="MenuItem">
    /// <MenuItems>
    /// <MenuItem Text="Cut" IconCss="e-cm-icons e-cut"></MenuItem>
    /// <MenuItem Text="Copy" IconCss="e-cm-icons e-copy"></MenuItem>
    /// <MenuItem Text="Paste" IconCss="e-cm-icons e-paste"></MenuItem>
    /// </MenuItems>
    /// </SfContextMenu>
    /// ]]></code>
    /// </example>
    public partial class SfContextMenu<TValue> : SfMenuBase<TValue>
    {
        [CascadingParameter]
        private MenuOptions? Parent { get; set; }
        private string id = SfBaseUtils.GenerateID(SFCONTEXTMENU);
        private ElementReference refElement;
        private string containerClass = string.Empty;
        private bool manualOpen;
        private bool isCollision;
        internal string dataId = "sfContextMenu-" + Guid.NewGuid().ToString();
        internal double scrollHeight;
        internal string activeBreakpoint = string.Empty;
        private static int ariaLabelCount;
        private string ariaLabel = "contextmenu-" + ariaLabelCount++;
        private string cmenuHidden = string.Empty;
        private bool isReposition;
        private bool isSubMenuDevice;
        
        internal void Initialize()
        {
            containerClass = Initialize(IsMenu ? CONTAINER + MENUCONTAINER : CONTAINER, Parent?.dataId ?? dataId);
            if (HtmlAttributes != null && HtmlAttributes.TryGetValue("id", out var idValue))
            {
                id = idValue.ToString()!;
            }
            else if (HtmlAttributes != null && HtmlAttributes.TryGetValue("aria-label", out var idValue1))
            {
                ariaLabel = (string)idValue1!;
            }
        }

        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task OpenContextMenuAsync(double clientX, double clientY, string id = null)
        {
            var eventArgs = await TriggerBeforeOpenCloseEvent(default, Items, ONOPEN, true, clientX, clientY, id).ConfigureAwait(true);
            if (!eventArgs.Cancel)
            {
                Top = eventArgs.Top;
                Left = eventArgs.Left;
                OpenEventArgs = new OpenCloseMenuEventArgs<TValue>() { Name = OPENED, Element = Element, Items = Items, ParentItem = default, NavigationIndex = this.NavIdx.Count - 1, TargetId = id };
                cmenuHidden = "visibility: hidden;";
                ClsCollection = new List<ClassCollection>();
                NavIdx = new List<int> { 0 };
                scrollHeight = eventArgs.ScrollHeight;
                await InvokeAsync(() => StateHasChanged()).ConfigureAwait(true);
            }
        }

        private async Task ItemClickHandler(TValue item, MouseEventArgs e, bool isEnterKey = false, bool isUl = false, bool header = false)
        {
            await ClickHandler(Items, item, e, isEnterKey, isUl, header).ConfigureAwait(true);
            if (NavIdx.Count == 0 && EnableScrolling)
            {
                StateHasChanged();
            }
            if (CloseActionEvents != "mousedown touchstart")
            {
                await InvokeMethod(CLICK, dataId, true).ConfigureAwait(true);
            }
            else if ((!ShowItemOnClick || (ShowItemOnClick && Utils.GetItemProperties<List<TValue>, TValue>(item!, Fields?.Children) == null)) && EnableScrolling)
            {
                await InvokeMethod(CLICK, dataId, false).ConfigureAwait(true);
            }
        }

        private async Task MouseOverHandler(TValue item)
        {
            if (!IsDevice)
            {
                await OpenCloseSubMenu(item, false, false, false, this, false, true).ConfigureAwait(true);
            }
        }

        private async Task KeyDownHandler(TValue item, KeyboardEventArgs e, bool isUl = false)
        {
            await KeyActionHandler(Items, item, e, isUl).ConfigureAwait(true);
        }

        private static Dictionary<string, object> GetAttributes(Dictionary<string, object>? htmlAttributes, string type)
        {
            var attr = new Dictionary<string, object>();
            if (htmlAttributes != null)
            {
                switch (type)
                {
                    case "anchor":
                        {
                            if (htmlAttributes.TryGetValue("anchor", out var anchorValue))
                            {
                                attr = (Dictionary<string, object>)anchorValue!;
                            }
                            break;
                        }
                    default:
                        {
                            attr = htmlAttributes.ToDictionary(entry => entry.Key, entry => entry.Value);
                            if (htmlAttributes.TryGetValue("anchor", out var value))
                            {
                                var anchorAttr = (Dictionary<string, object>)value!;
                                attr.Remove("anchor");
                                htmlAttributes["anchor"] = anchorAttr;
                            }
                            break;
                        }
                }
            }
            return attr;
        }
    }
}