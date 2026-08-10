using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Navigations.Internal;
using System.Threading.Tasks;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Used to configure the menu events.
    /// </summary>
    public class MenuEvents<TValue> : SfOwningComponentBase
    {
        [CascadingParameter]
        private IMenu Parent { get; set; }

        [CascadingParameter]
        private SfContextMenu<TValue> ContextMenu { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised before closing the sub menu.
        /// </summary>
        [Parameter]
        public EventCallback<BeforeOpenCloseMenuEventArgs<TValue>> OnClose { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised while rendering each menu item.
        /// </summary>
        [Parameter]
        public EventCallback<MenuEventArgs<TValue>> OnItemRender { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised before  opening the menu item.
        /// </summary>
        [Parameter]
        public EventCallback<BeforeOpenCloseMenuEventArgs<TValue>> OnOpen { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised when rendering is completed.
        /// </summary>
        [Parameter]
        public EventCallback<object> Created { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised after closing the menu.
        /// </summary>
        [Parameter]
        public EventCallback<OpenCloseMenuEventArgs<TValue>> Closed { get; set; }

        /// <summary>
        ///  Gets or sets an event callback that is raised after opening the menu item.
        /// </summary>
        [Parameter]
        public EventCallback<OpenCloseMenuEventArgs<TValue>> Opened { get; set; }

        /// <summary>
        ///  Gets or sets an event callback that is raised after selecting menu item.
        /// </summary>
        [Parameter]
        public EventCallback<MenuEventArgs<TValue>> ItemSelected { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(true);
            if (ContextMenu == null)
            {
                Parent?.UpdateChildProperties(typeof(TValue) == typeof(MenuItemModel) ? "SelfRefMenuEvents" : "MenuEvents", this);
            }
            else
            {
                ContextMenu.Delegates = this;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                Parent = null;
                ContextMenu = null;
            }
        }
    }
}