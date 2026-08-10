using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// The TreeView allows us to control the component by using events.
    /// </summary>
    /// <typeparam name="TValue">"TValue parameter".</typeparam>
    public partial class TreeViewEvents<TValue> : SfOwningComponentBase
    {
        [CascadingParameter]
        private SfTreeView<TValue>? Parent { get; set; }
        /// <summary>
        /// Gets or sets an event callback that is raised while any TreeView action failed to fetch the desired results.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <remarks>
        /// You can capture this failure and throw error message for users in required cases.
        /// </remarks>
        [Parameter]
        public EventCallback<FailureEventArgs> OnActionFailure { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised when the TreeView component is created successfully.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <remarks>
        /// You can perform required actions during this component creation.
        /// </remarks>
        [Parameter]
        public EventCallback<ActionEventArgs> Created { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised when data source is populated in the TreeView.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <remarks>
        /// The data can be modified or updated at this time when the component is updated with data source.
        /// </remarks>
        [Parameter]
        public EventCallback<DataBoundEventArgs<TValue>> DataBound { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised when data source is changed in the TreeView. The data source will be changed after performing some operation like
        /// drag and drop, node editing, adding, and removing node.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <remarks>
        /// The data source change can be detected and the updated data can be fetched or stored in required cases.
        /// </remarks>
        [Parameter]
        public EventCallback<DataSourceChangedEventArgs<TValue>> DataSourceChanged { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised when the TreeView component is destroyed successfully.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <remarks>
        /// Required actions can be performed once the component is destroyed.
        /// </remarks>
        [Parameter]
        public EventCallback<ActionEventArgs> Destroyed { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised before the TreeView node is appended to the TreeView element.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <remarks>
        /// A specific node can be customized at the time of rendering using this event.
        /// </remarks>
        [Parameter]
        public EventCallback<NodeRenderEventArgs<TValue>> OnNodeRender { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised when key press is successful. 
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <remarks>
        /// Helps to customize the operations at key press.
        /// </remarks>
        [Parameter]
        public EventCallback<NodeKeyPressEventArgs> OnKeyPress { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised when the TreeView node is checked/unchecked successfully.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <remarks>
        /// The details of checked/unchecked node can be fetched and required actions can be performed using this event.
        /// </remarks>
        [Parameter]
        public EventCallback<NodeCheckEventArgs> NodeChecked { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised before the TreeView node is to be checked/unchecked.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <remarks>
        /// Check/uncheck action can be prevented for specific nodes in required cases.
        /// </remarks>
        [Parameter]
        public EventCallback<NodeCheckEventArgs> NodeChecking { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised when the TreeView node is clicked successfully.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <remarks>
        /// Required actions can be performed on node click using this event.
        /// </remarks>
        [Parameter]
        public EventCallback<NodeClickEventArgs> NodeClicked { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised when the TreeView node collapses successfully.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <remarks>
        /// Helps to identify that a node is collapsed and the collapsed node details can be fetched in this event.
        /// </remarks>
        [Parameter]
        public EventCallback<NodeExpandEventArgs> NodeCollapsed { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised before the TreeView node collapses.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <remarks>
        /// Collapse action can be prevented for specific nodes in required cases.
        /// </remarks>
        [Parameter]
        public EventCallback<NodeExpandEventArgs> NodeCollapsing { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised when the TreeView node drag (move) starts.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <remarks>
        /// Helps to identify the dragged node details. Dragging can be prevented for specific node in required cases.
        /// </remarks>
        [Parameter]
        public EventCallback<DragAndDropEventArgs> OnNodeDragStart { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised when the TreeView node dragging (move) stops.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <remarks>
        /// Helps to identify the node drag stop action. Dropped target can be fetched and used to perform required actions.
        /// </remarks>
        [Parameter]
        public EventCallback<DragAndDropEventArgs> OnNodeDragStop { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised when the TreeView node is dragged.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <remarks>
        /// Helps to identify the node drag action.
        /// </remarks>
        [Parameter]
        public EventCallback<DragAndDropEventArgs> OnNodeDragged { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised when the TreeView node is dropped on target element successfully.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <remarks>
        /// Helps to identify the node drop action and the dropped index/target can be fetched to perform required actions.
        /// </remarks>
        [Parameter]
        public EventCallback<DragAndDropEventArgs> NodeDropped { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised when the TreeView node is renamed successfully.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <remarks>
        /// Helps to identify that the node is edited successfully and the new edited text can be obtained here.
        /// </remarks>
        [Parameter]
        public EventCallback<NodeEditEventArgs> NodeEdited { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised before the TreeView node is renamed.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <remarks>
        /// Node editing can be prevented for specific nodes using this event.
        /// </remarks>
        [Parameter]
        public EventCallback<NodeEditEventArgs> NodeEditing { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised when the TreeView node expands successfully.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <remarks>
        /// Helps to identify that a node is expanded and the expanded node details can be fetched using this event.
        /// </remarks>
        [Parameter]
        public EventCallback<NodeExpandEventArgs> NodeExpanded { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised before the TreeView node is to be expanded.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <remarks>
        /// Expand action can be prevented for specific nodes in required cases using this event.
        /// </remarks>
        [Parameter]
        public EventCallback<NodeExpandEventArgs> NodeExpanding { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised when the TreeView node is selected/unselected successfully.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <remarks>
        /// Selected node details can be obtained here and updated to required places using this event.
        /// </remarks>
        [Parameter]
        public EventCallback<NodeSelectEventArgs> NodeSelected { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised before the TreeView node is selected/unselected.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <remarks>
        /// A specific node selection can be prevented using this event.
        /// </remarks>
        [Parameter]
        public EventCallback<NodeSelectEventArgs> NodeSelecting { get; set; }

        /// <summary>
        /// Method invoked when the component is ready to start.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(true);
            if (Parent != null)
            {
                Parent.TreeViewEvents = this;
            }
        }
    }
}
