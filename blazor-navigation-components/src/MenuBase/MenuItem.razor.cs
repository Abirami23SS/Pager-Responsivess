using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using System;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Used to configure the items which is going to render as menu.
    /// </summary>
    public partial class MenuItem: ComponentBase, IDisposable
    {
        [CascadingParameter]
        private MenuItems Parent { get; set; }

        /// <exclude/>
        [Parameter]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Defines class/multiple classes separated by a space for the menu Item that is used to include an icon.
        /// Menu Item can include font icon and sprite image.
        /// </summary>
        [Parameter]
        public string IconCss { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value that indicates the id for menu item.
        /// </summary>
        [Parameter]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value that indicates the sub menu items that is the array of MenuItem.
        /// </summary>
        [Parameter]
        public List<MenuItem> Items { get; set; }

        /// <summary>
        /// Gets or sets whether to enable/disable separator between the menu items.
        /// </summary>
        /// <remarks>
        /// Separator are either horizontal or vertical lines used to group menu items.
        /// </remarks>
        [Parameter]
        public bool Separator { get; set; }

        /// <summary>
        /// Gets or sets whether to enable or disable the menu item.
        /// </summary>
        [Parameter]
        public bool Disabled { get; set; }

        /// <summary>
        /// Gets or sets whether to hide or show the menu item.
        /// </summary>
        [Parameter]
        public bool Hidden { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the text for menu item.
        /// </summary>
        [Parameter]
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value that indicates the URL for menu item that creates the anchor link to navigate to the url provided.
        /// </summary>
        [Parameter]
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value that indicates the additional HTML attributes such as style, title etc., to the menu item.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> HtmlAttributes { get; set; }

        private bool isDisposed;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(true);
            Parent.UpdateChildProperty(this);
        }

        internal void UpdateChildProperty(List<MenuItem> items)
        {
            Items = items;
        }

        /// <summary>
        /// Disposes unmanaged resources in the component.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed)
            {
                return;
            }

            if (disposing)
            {
                Parent?.RemoveChildProperty(this);
                Parent = null;
                Items = null;
                ChildContent = null;
            }
            isDisposed = true;
        }
    }
}