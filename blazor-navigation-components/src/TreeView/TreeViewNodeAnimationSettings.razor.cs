using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Navigations.Internal;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// A class used for configuring the TreeView animation properties.
    /// </summary>
    public partial class TreeViewNodeAnimationSettings : SfOwningComponentBase
    {
        [CascadingParameter]
        private ITreeView? TreeParent { get; set; }

        [Inject]
        private SyncfusionBlazorService? SyncfusionService { get; set; }

        /// <exclude/>
        /// <summary>
        /// Child Content for the Treeview Animation Settings.
        /// </summary>
        [Parameter]
        [JsonIgnore]
        public RenderFragment ChildContent { get; set; }

        internal TreeViewAnimationCollapse? NodeAnimationCollapse { get; set; }

        internal TreeViewAnimationExpand? NodeAnimationExpand { get; set; }

        internal void UpdateExpandProperties(TreeViewAnimationExpand? animation, SyncfusionBlazorService? service = null)
        {
            service ??= SyncfusionService;
            NodeAnimationExpand = animation ?? new TreeViewAnimationExpand();
            if (service?.options?.Animation == GlobalAnimationMode.Disable)
            {
                NodeAnimationExpand.SetDuration(0);
            }
        }

        internal void UpdateCollapseProperties(TreeViewAnimationCollapse? animation, SyncfusionBlazorService? service = null)
        {
            service ??= SyncfusionService;
            NodeAnimationCollapse = animation ?? new TreeViewAnimationCollapse();
            if(service?.options?.Animation == GlobalAnimationMode.Disable)
            {
                NodeAnimationCollapse.SetDuration(0);
            }
        }

        /// <summary>
        /// Method invoked when the component is ready to start.
        /// </summary>
        /// <returns>"Task".</returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(true);
            UpdateExpandProperties(NodeAnimationExpand, SyncfusionService);
            UpdateCollapseProperties(NodeAnimationCollapse, SyncfusionService);
            TreeParent?.UpdateAnimationProperties(this);
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                TreeParent = null!;
                ChildContent = null!;
            }
        }
    }
}