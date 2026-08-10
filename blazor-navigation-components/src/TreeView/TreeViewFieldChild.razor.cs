using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    ///  A class used for configuring the TreeView child element fields properties.
    /// </summary>
    /// <typeparam name="TValue">"Specifies the TValue parameter".</typeparam>
    public partial class TreeViewFieldChild<TValue> : TreeViewFieldOptions<TValue>
    {
        [CascadingParameter]
        private TreeViewFieldsSettings<TValue>? Parent { get; set; }

        /// <summary>
        /// Specifies the Treeview child content.
        /// </summary>
        /// <exclude/>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Method invoked when the component is ready to start.
        /// </summary>
        /// <returns>"Task".</returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(true);
            Parent?.UpdateChildProperties("child", this);
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                Parent = null;
                ChildContent = null!;
            }
        }
    }
}