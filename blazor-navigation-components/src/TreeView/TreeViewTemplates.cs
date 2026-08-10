using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// The TreeView component allows you to customize the look of TreeView nodes using Templates.
    /// </summary>
    /// <typeparam name="TValue">"Specifies the TValue".</typeparam>
    public class TreeViewTemplates<TValue> : SfOwningComponentBase
    {
        [CascadingParameter]
        private SfTreeView<TValue>? Parent { get; set; }

        /// <summary>
        /// Specifies the NodeTemplate.
        /// </summary>
        [Parameter]
        public RenderFragment<TValue> NodeTemplate { get; set; }

        /// <summary>
        /// Method invoked when the component is ready to start.
        /// </summary>
        /// <returns>"Task".</returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(true);
            Parent?.UpdateChildProperties("TreeViewTemplates", this);
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                Parent = null;
            }
        }
    }
}
