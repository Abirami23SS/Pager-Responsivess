using System;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using Syncfusion.Blazor.Internal;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Partial Class SfSidebar.
    /// </summary>
    public partial class SfSidebar 
    {
        /// <summary>
        /// Hide the Sidebar component, if it is in an open state.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task that represents any asynchronous action</returns>
        [JSInvokable]
        internal async Task SidebarHide()
        {
            if (!sidebarClass.Contains(CLOSE, StringComparison.Ordinal))
            {
                bool isCancelled = false;
                if (OnClose.HasDelegate)
                {
                    EventArgs eventArgs = SidebarEvent("OnClose");
                    await OnClose.InvokeAsync(eventArgs).ConfigureAwait(true);
                    isCancelled = eventArgs.Cancel;
                }
                if (!isCancelled)
                {
                    openState = false;
                    IsOpen = SidebarIsOpen = await SfBaseUtils.UpdateProperty<bool>(false, SidebarIsOpen, IsOpenChanged).ConfigureAwait(true);
                    UpdateClass();
                    if (SfSidebarContainer == null)
                    {
                        await InvokeMethod("sfBlazor.Sidebar.hide", new object[] { dataId, GetProperties() }).ConfigureAwait(true);
                    }
                    else
                    {
                        if (Changed.HasDelegate)
                        {
                            ChangeEventArgs eventArgs = new ChangeEventArgs
                            {
                                Element = element,
                                Name = "Changed",
                                IsInteracted = isInteracted,
                            };
                            await Changed.InvokeAsync(eventArgs).ConfigureAwait(true);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Shows the Sidebar component, if it is in closed state.
        /// </summary>
        /// <returns>"Task".</returns>
        [JSInvokable]
        internal async Task SidebarShow()
        {
            if (!sidebarClass.Contains(OPEN, StringComparison.Ordinal))
            {
                bool isCancelled = false;
                if (OnOpen.HasDelegate)
                {
                    EventArgs eventArgs = SidebarEvent("OnOpen");
                    await OnOpen.InvokeAsync(eventArgs).ConfigureAwait(true);
                    isCancelled = eventArgs.Cancel;
                }
                if (!isCancelled)
                {
                    openState = true;
                    IsOpen = SidebarIsOpen = await SfBaseUtils.UpdateProperty<bool>(true, SidebarIsOpen, IsOpenChanged).ConfigureAwait(true);
                    UpdateClass();
                    if (SfSidebarContainer == null)
                    {
                        await InvokeMethod("sfBlazor.Sidebar.show", new object[] { dataId, GetProperties(), openState }).ConfigureAwait(true);
                    }
                    else
                    {
                        if (Changed.HasDelegate)
                        {
                            ChangeEventArgs eventArgs = new ChangeEventArgs
                            {
                                Element = element,
                                Name = "Changed",
                                IsInteracted = isInteracted,
                            };
                            await Changed.InvokeAsync(eventArgs).ConfigureAwait(true);
                        }
                    }
                }
            }
        }
    }
}
