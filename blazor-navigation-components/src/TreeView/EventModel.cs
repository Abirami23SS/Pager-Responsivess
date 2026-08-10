using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Provides information about the <see cref="TreeViewEvents{TValue}.DataSourceChanged"/> event callback.
    /// </summary>
    /// <typeparam name="T">Specifies the TValue of TreeView events.</typeparam>
    public class DataSourceChangedEventArgs<T>
    {
        /// <summary>
        /// Gets or sets the 'DataSourceChanged' Event name.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        public string Name { get; set; }
    }

    /// <summary>
    /// Provides information about the <see cref="TreeViewEvents{TValue}.DataBound"/> event callback.
    /// </summary>
    /// <typeparam name="T">Specifies the TValue of TreeView events.</typeparam>
    public class DataBoundEventArgs<T>
    {
        /// <summary>
        /// Gets or sets the 'DataBound' Event name . 
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        public string Name { get; set; }
    }

    /// <summary>
    /// Provides information about the drag and drop events callback.
    /// </summary>
    public class DragAndDropEventArgs
    {
        /// <summary>
        /// Gets or sets whether the drag and drop action should be cancelled or not.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the drag and drop action should be canceled. Otherwise, <c>false</c>.
        /// </value>
        public bool Cancel { get; set; }

        /// <summary>
        /// Gets or sets the currently dragged node as array of JSON object from data source.
        /// </summary>
        /// <value>
        /// A <see cref="NodeData"/> object that represents the currently dragged node from the data source.
        /// </value>
        public NodeData DraggedNodeData { get; set; }

        /// <summary>
        /// Gets or sets the dragged/dropped element's target index position.
        /// </summary>
        /// <value>
        /// Accepts an integer value.
        /// </value>
        public int? DropIndex { get; set; }

        /// <summary>
        /// Gets or sets the cloned element's drop status icon while dragging.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        public string DropIndicator { get; set; }

        /// <summary>
        /// Gets or sets the dragged/dropped element's target level.
        /// </summary>
        /// <value>
        /// Accepts an integer value.
        /// </value>
        public int? DropLevel { get; set; }

        /// <summary>
        /// Gets or sets the dropped node as array of JSON object from data source.
        /// </summary>
        /// <value>
        /// A <see cref="NodeData"/> object that represents the dropped node from the data source.
        /// </value>
        public NodeData DroppedNodeData { get; set; }

        /// <summary>
        /// Gets the actual drag/drop event.
        /// </summary>
        /// <value>
        /// An object that represents the event object associated with the drag/drop operation.
        /// </value>
        public object Event { get; internal set; }

        /// <summary>
        /// Gets or sets the boolean value for preventing auto-expanding of parent node.
        /// </summary>
        /// <value>
        /// <c>true</c>, to prevent auto-expanding of the parent node. Otherwise, <c>false</c>.
        /// </value>
        public bool PreventTargetExpand { get; set; }

        /// <summary>
        /// Gets or sets the actual drag/drop Event name.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets the Client X value of target element.
        /// </summary>
        /// <value>
        /// Accepts the double value.
        /// </value>
        public double Left { get; internal set; }

        /// <summary>
        /// Gets the Client Y value of target element.
        /// </summary>
        /// <value>
        /// Accepts the double value.
        /// </value>
        public double Top { get; internal set; }
    }

    /// <summary>
    /// Provides information about the <see cref="TreeViewEvents{TValue}.OnNodeRender"/> event callback.
    /// </summary>
    /// <typeparam name="T">Specifies the TValue of TreeView events.</typeparam>
    public class NodeRenderEventArgs<T>
    {
        /// <summary>
        /// Gets or sets the current rendering node.
        /// </summary>
        /// <value>
        /// The current rendering node as an <see cref="ElementReference" /> object.
        /// </value>
        public ElementReference Node { get; set; }

        /// <summary>
        /// Gets or sets the current rendering node as JSON object.
        /// </summary>
        /// <value>
        /// The JSON object representing the current rendering node.
        /// </value>
        public T NodeData { get; set; }

        /// <summary>
        /// Gets or sets the current rendering node text.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the 'NodeRender' Event name.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        public string Name { get; set; }
    }

    /// <summary>
    /// Provides information about the <see cref="TreeViewEvents{TValue}.OnActionFailure"/> event callback.
    /// </summary>
    public class FailureEventArgs : ActionEventArgs
    {
        /// <summary>
        /// Gets the error information.
        /// </summary>
        /// <value>
        /// An <see cref="Exception"/> object that represents the error information.
        /// </value>
        public Exception Error { get; internal set; }
    }

    /// <summary>
    /// Provides information about the <see cref="TreeViewEvents{TValue}.NodeChecked"/> event callback.
    /// </summary>
    public class NodeCheckEventArgs
    {
        /// <summary>
        /// Gets or sets the name of action like check or un-check.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        public string Action { get; set; }

        /// <summary>
        /// Gets or sets whether the check/un-check action should be cancelled or not.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the check/un-check action should be canceled. Otherwise, <c>false</c>.
        /// </value>
        public bool Cancel { get; set; }

        /// <summary>
        /// Gets or sets the currently checked node as JSON object from data source.
        /// </summary>
        /// <value>
        /// A JSON object that represents the currently checked node.
        /// </value>
        public NodeData NodeData { get; set; }

        /// <summary>
        /// If the event is triggered by interaction, it returns true. Otherwise, it returns false.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the event was triggered by user interaction. Otherwise, <c>false</c>.
        /// </value>
        public bool IsInteracted { get; set; }

        /// <summary>
        /// Gets or sets the 'Node check/uncheck' Event name.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        public string Name { get; set; }
    }

    /// <summary>
    /// Provides information about the <see cref="TreeViewEvents{TValue}.NodeClicked"/> event callback.
    /// </summary>
    public class NodeClickEventArgs
    {
        /// <summary>
        /// Gets the actual event.
        /// </summary>
        /// <value>
        /// The <see cref="ClickEventArgs" /> object that represents the click event.
        /// </value>
        public ClickEventArgs Event { get; internal set; }

        /// <summary>
        /// Gets or sets the current clicked TreeView node data.
        /// </summary>
        /// <value>
        /// A <see cref="NodeData"/> object that represents the data associated with the currently clicked node.
        /// </value>
        public NodeData NodeData { get; set; }

        /// <summary>
        /// Gets or sets the 'Node click' Event name.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets the Client X value of target element.
        /// </summary>
        /// <value>
        /// Accepts the double value.
        /// </value>
        public double Left { get; internal set; }

        /// <summary>
        /// Gets the Client Y value of target element.
        /// </summary>
        /// <value>
        /// Accepts the double value.
        /// </value>
        public double Top { get; internal set; }
    }

    /// <summary>
    /// Specifies class that holds the node details.
    /// </summary>
    public class NodeData
    {
        /// <summary>
        /// Gets or sets the mapping field for expand state of the TreeView node.
        /// </summary>
        public bool Expanded { get; set; }

        /// <summary>
        ///  Gets the hasChildren value to check whether a node has child nodes or not.
        /// </summary>
        public bool HasChildren { get; internal set; }

        /// <summary>
        /// Gets or sets the ID field mapped in the dataSource.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the checked state of the TreeView node.
        /// </summary>
        public string IsChecked { get; set; }

        /// <summary>
        /// Gets or sets the parent ID field mapped in dataSource.
        /// </summary>
        public string ParentID { get; set; }

        /// <summary>
        /// Gets or sets the selected state of the TreeView node.
        /// </summary>
        public bool Selected { get; set; }

        /// <summary>
        /// Gets or sets the text displayed as TreeView node's text.
        /// </summary>
        public string Text { get; set; }
    }

    /// <summary>
    /// Provides information about the node edit events callback.
    /// </summary>
    public class NodeEditEventArgs
    {
        /// <summary>
        /// Gets or sets whether the edit action should be cancelled or not.
        /// </summary>
        /// <value>
        /// <c>true</c>, if an edit action should be canceled. Otherwise, <c>false</c>.
        /// </value>
        public bool Cancel { get; set; }

        /// <summary>
        /// Gets or sets the inner HTML of TreeView node while editing.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        public string InnerHtml { get; set; }

        /// <summary>
        /// Gets or sets the new text of current TreeView node.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        public string NewText { get; set; }

        /// <summary>
        /// Gets or sets the current node details as JSON object from data source.
        /// </summary>
        /// <value>
        /// A <see cref="NodeData"/> object that represents the details of the current node from the data source.
        /// </value>
        public NodeData NodeData { get; set; }

        /// <summary>
        /// Gets the old text of current TreeView node.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        public string OldText { get; internal set; }

        /// <summary>
        /// Gets or sets the 'Node Edit' Event name.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        public string Name { get; set; }
    }

    /// <summary>
    /// Provides information about the node expand events callback.
    /// </summary>
    public class NodeExpandEventArgs
    {
        /// <summary>
        /// Gets or sets whether the expand action should be cancelled or not.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the expand action should be canceled. Otherwise, <c>false</c>.
        /// </value>
        public bool Cancel { get; set; }

        /// <summary>
        /// Gets the event argument.
        /// </summary>
        /// <value>
        /// A <see cref="ClickEventArgs"/> object that represents the event argument associated with the click event.
        /// </value>
        public ClickEventArgs Event { get; set; }

        /// <summary>
        /// If the event is triggered by interaction, it returns <c>true</c>. Otherwise, it returns <c>false</c>.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the event is triggered by user interaction. Otherwise, <c>false</c>.
        /// </value>
        public bool IsInteracted { get; set; }

        /// <summary>
        /// Gets or sets the expanded/collapsed node as JSON object from data source.
        /// </summary>
        /// <value>
        /// A <see cref="NodeData"/> object that represents the expanded/collapsed node from the data source.
        /// </value>
        public NodeData NodeData { get; set; }

        /// <summary>
        /// Gets or sets the Node expand Event name.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        public string Name { get; set; }
    }

    /// <summary>
    /// Provides information about the <see cref="TreeViewEvents{TValue}.OnKeyPress"/> event callback.
    /// </summary>
    public class NodeKeyPressEventArgs
    {
        /// <summary>
        /// Gets or sets whether the key press action should be cancelled or not.
        /// </summary>
        /// <value>
        /// <c>true</c>, if key press action should be canceled. Otherwise, <c>false</c>.
        /// </value>
        public bool Cancel { get; set; }

        /// <summary>
        /// Gets the actual event.
        /// </summary>
        /// <value>
        /// A <see cref="KeyboardEventArgs"/> object representing the keyboard event associated with the current action.
        /// </value>
        public KeyboardEventArgs Event { get; internal set; }

        /// <summary>
        /// Gets or sets the current active node as JSON object from data source.
        /// </summary>
        /// <value>
        /// The JSON object representing the current active node in the TreeView's data source.
        /// </value>
        public NodeData NodeData { get; set; }

        /// <summary>
        /// Gets or sets the 'NodeKeyPress' Event name.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Key Action of Event.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        public string Action { get; set; }

        /// <summary>
        /// Gets the Key value of Event.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        public string Key { get; internal set; }
    }

    /// <summary>
    /// Provides information about the node selection event callback.
    /// </summary>
    public class NodeSelectEventArgs
    {
        /// <summary>
        /// Gets or sets the name of action like select or un-select.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        public string Action { get; set; }

        /// <summary>
        /// Gets or sets whether the node selection should be cancelled or not.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the node selection should be canceled. Otherwise, <c>false</c>.
        /// </value>
        public bool Cancel { get; set; }

        /// <summary>
        /// If the event is triggered by interaction, it returns <c>true</c>. Otherwise, it returns <c>false</c>.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the event is triggered by user interaction. Otherwise, <c>false</c>.
        /// </value>
        public bool IsInteracted { get; set; }

        /// <summary>
        /// Gets or sets the currently selected node as JSON object from data source.
        /// </summary>
        /// <value>
        /// A <see cref="NodeData"/> object that represents the currently selected node from the data source.
        /// </value>
        public NodeData NodeData { get; set; }

        /// <summary>
        /// Gets or sets the Node selection Event name.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        public string Name { get; set; }
    }

    /// <summary>
    /// Specified the class that denotes the creation and destroy action of TreeView component.
    /// </summary>
    public class ActionEventArgs
    {
        /// <summary>
        /// Gets or sets the Event name.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        public string Name { get; set; }
    }

    /// <summary>
    /// class for persistence values.
    /// </summary>
    internal class TreePersistenceValues
    {
        /// <summary>
        /// Gets or sets the SelectedNodes of the TreeView component.
        /// </summary>
        public List<string> SelectedNodes { get; set; }

        /// <summary>
        /// Gets or sets the CheckedNodes of the TreeView component.
        /// </summary>
        public Dictionary<string, object> CheckedNodes { get; set; }

        /// <summary>
        /// Gets or sets the ExpandedNodes of the TreeView component.
        /// </summary>
        public List<string> ExpandedNodes { get; set; }
    }
}
