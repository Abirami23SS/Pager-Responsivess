using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Represents a collection of <see cref="ToolbarItem"/>.
    /// </summary>
    /// <remarks>
    /// To generate dynamic <see cref="ToolbarItem"/> based on collection, use <c>@foreach</c> within <see cref="ToolbarItems"/> tag directive.
    /// </remarks>
    /// <example>
    /// In the below code example, a basic toolbar has been rendered using <see cref="ToolbarItems"/> tag directive.
    /// <code><![CDATA[
    /// <SfToolbar>
    ///     <ToolbarItems>
    ///         <ToolbarItem Text="Cut"></ToolbarItem>
    ///         <ToolbarItem Text="Copy"></ToolbarItem>
    ///         <ToolbarItem Text="Paste"></ToolbarItem>
    ///     </ToolbarItems>
    /// </SfToolbar>
    /// ]]></code>
    /// </example>
    public partial class ToolbarItems : SfOwningComponentBase
    {
        [CascadingParameter]
        internal SfToolbar Parent { get; set; }

        /// <summary>
        /// Gets or sets the child content for the toolbar items.
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Gets or sets the list of toolbar items used to configure toolbar commands.
        /// </summary>
        public List<ToolbarItem> Items { get; set; } = new List<ToolbarItem>();

        internal int UpdateChildProperty(ToolbarItem item)
        {
            if (item != null)
            {
                Items.Add(item);
                Parent.UpdateChildProperties(Items);
            }

            return Items.Count - 1;
        }

        /// <summary>
        /// Method invoked when the component is ready to start.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(true);
            Parent.UpdateChildProperties(Items);
        }

        /// <summary>
        /// Dispose unmanaged resources in the Syncfusion Blazor component.
        /// </summary>
        /// <param name="disposing">Boolean value to dispose the object.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                Parent = null;
                ChildContent = null;
            }
        }
    }
}