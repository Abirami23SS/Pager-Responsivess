using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Internal;
using Syncfusion.Blazor.Navigations.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// The TreeView component is used to represent hierarchical data in a tree like structure with editing, drag and drop, checkboxes, expand and collapse, and more.
    /// </summary>
    public partial class SfTreeView<TValue>
    {
        /// <summary>
        /// Adds a collection of TreeView nodes at the specified target and index position. If the target node is not specified,
        /// then the nodes are added as children of the given parentID or at the root level of the TreeView.
        /// </summary>
        /// <param name="nodes">A list of nodes to be added to the TreeView.</param>
        /// <param name="target">Specifies the target to which the nodes will be added as children of the given parentID or at the root level of the TreeView.</param>
        public void AddNodes(List<TValue> nodes, string target = null)
        {
            try
            {
                ListReference?.AddNodeData(nodes, target);
            }
            catch
            {
                if (!IsDisposed)
                    throw;
            }
        }

        /// <summary>
        /// Enables editing of a TreeView node without clicking on it.  
        /// Passing the node ID or element through this method will create an edit text box for the specified node, allowing it to be edited.
        /// </summary>
        /// <param name="node">Specifies the ID of the TreeView node to be edited.</param>
        /// <returns>A System.Threading.Tasks.Task that represents any asynchronous action.</returns>
        public async Task BeginEditAsync(string node)
        {
            await InvokeMethod(BEGINEDIT, new object[] { dataId, node }).ConfigureAwait(true);
        }

        /// <summary>
        /// Checks all the unchecked nodes in the TreeView. Specific nodes can be checked by passing an array of unchecked node IDs
        /// as an argument to this method.
        /// </summary>
        /// <param name="nodesId">Specifies the IDs of the nodes to be checked. If not provided, all unchecked nodes will be checked.</param>
        /// <returns>A System.Threading.Tasks.Task that represents any asynchronous action.</returns>
        public async Task CheckAllAsync(string[] nodesId = null)
        {
            if (isCheckingInProgress) return;
            isCheckingInProgress = true;
            try
            {
                if (ShowCheckBox)
                {
                    NodeCheckEventArgs nodeArgs = new NodeCheckEventArgs
                    {
                        NodeData = new NodeData()
                    };
                    if (nodesId == null)
                    {
                        ListReference?.GetAllNodeId(ListReference.DataSource);
                        AllCheckedNodes?.Clear();
                    }
                    string[]? checkNodeIds = nodesId ?? ListReference?.CheckNodeId.ToArray();
                    Dictionary<string, TValue>? dictData = ListReference?.DataType == TreeViewDataType.SelfReferential ? ListReference?.ItemsData?.ToDictionary(data => GetValue(TreeViewFields?.Id, data).ToString()) : null;
                    if (checkNodeIds != null)
                    {
                        foreach (string nodeId in checkNodeIds)
                        {
                            if (AllCheckedNodes != null && AllCheckedNodes.TryGetValue(nodeId, out var checkedState) && checkedState?.ToString() == "true")
                            {
                                continue;
                            }
                            nodeArgs.NodeData.Id = nodeId;
                            nodeArgs.Action = CHECK;
                            if (nodeArgs.NodeData.Id != null)
                            {
                                await TriggerNodeCheckingEvent(nodeArgs, dictData).ConfigureAwait(true);
                            }
                        }
                    }
                }
            }
            finally
            {
                isCheckingInProgress = false;
            }
        }

        /// <summary>
        /// This method clears the expanded, selected, and checked interaction states in the TreeView. This method is useful when dynamically changing the data source.
        /// </summary>
        public async Task ClearStateAsync()
        {
            AllSelectedNodes?.Clear();
            AllCheckedNodes?.Clear();
            AllExpandedNodes = new HashSet<string>();
            InternalExpandedNodes = new List<string>();
            SelectedNodes = treeSelectedNodes = await SfBaseUtils.UpdateProperty<string[]>(null!, treeSelectedNodes!, SelectedNodesChanged).ConfigureAwait(true);
            CheckedNodes = treeCheckedNodes = await SfBaseUtils.UpdateProperty<string[]>(null!, treeCheckedNodes!, CheckedNodesChanged).ConfigureAwait(true);
            ExpandedNodes = treeExpandedNodes = await SfBaseUtils.UpdateProperty<string[]>(null!, treeExpandedNodes!, ExpandedNodesChanged).ConfigureAwait(true);
            ListReference?.ListUpdated();
            IsClearStateCall = true;
        }

        /// <summary>
        /// Collapses all the expanded nodes in the TreeView. Specific nodes can also be collapsed by passing an array of node IDs as an argument to this method.
        /// </summary>
        /// <param name="nodesId">Specifies the NodeID to be collapsed. If not provided, all expanded nodes will be collapsed.</param>
        /// <returns>A System.Threading.Tasks.Task that represents any asynchronous action.</returns>
        public async Task CollapseAllAsync(string[] nodesId = null)
        {
            List<string>? expandedNodes = nodesId == null ? InternalExpandedNodes : nodesId.ToList();
            NodeExpandEventArgs nodeArgs = new NodeExpandEventArgs
            {
                NodeData = new NodeData()
            };
            if (expandedNodes != null)
            {
                for (int i = expandedNodes.Count - 1; i >= 0; i--)
                {
                    nodeArgs.NodeData.Id = expandedNodes[i];
                    await TriggerNodeCollapsingEvent(nodeArgs).ConfigureAwait(true);
                }
            }
        }

        /// <summary>
        /// Disables a collection of nodes by passing the ID of nodes or node elements in the array.
        /// </summary>
        /// <param name="nodes">Specifies the array of TreeView nodes ID to be disabled.</param>
        /// <returns>A System.Threading.Tasks.Task that represents any asynchronous action.</returns>
        public async Task DisableNodesAsync(string[] nodes)
        {
            if (nodes != null && nodes.Length > 0)
            {
                foreach (var nodeId in nodes)
                {
                    if (AllDisabledNodes != null && !AllDisabledNodes.Contains(nodeId))
                    {
                        AllDisabledNodes.Add(nodeId);
                    }
                }

                ListReference?.ListUpdated();
            }
            await Task.CompletedTask.ConfigureAwait(true);
        }

        /// <summary>
        /// Enables a collection of disabled nodes by passing the ID of nodes or node elements in the array.
        /// </summary>
        /// <param name="nodes">Specifies the array of TreeView nodes ID to be enabled.</param>
        /// <returns>A System.Threading.Tasks.Task that represents any asynchronous action.</returns>
        public async Task EnableNodesAsync(string[] nodes)
        {
            if (nodes != null && nodes.Length > 0)
            {
                AllDisabledNodes?.RemoveAll(nodeId => nodes.Contains(nodeId));

                ListReference?.ListUpdated();
            }
            await Task.CompletedTask.ConfigureAwait(true);
        }

        /// <summary>
        /// Ensures visibility of the TreeView node by using the node ID or node element.
        /// When many TreeView nodes are present and a particular node has to be found, `EnsureVisibleAsync` method
        /// brings the node to visibility by expanding the TreeView and scrolling to the specific node.
        /// </summary>
        /// <param name="node">Specifies ID of TreeView node.</param>
        /// <returns>A System.Threading.Tasks.Task that represents any asynchronous action.</returns>
        public async Task EnsureVisibleAsync(string node)
        {
            try
            {
                if (node != null)
                {
                    List<string>? idList = new List<string>();
                    if (ListReference?.DataType == TreeViewDataType.SelfReferential)
                    {
                        string parentId = ListReference.GetNodeDetails(node).ParentID;
                        while (parentId != null)
                        {
                            idList.Add(parentId);
                            parentId = ListReference.GetNodeDetails(parentId).ParentID;
                        }
                    }
                    else
                    {
                        if (ListReference != null)
                        {
                            idList = await ListReference.GetHierarchicalAndRemoteParent(node, ListReference.DataSource).ConfigureAwait(true);
                        }
                    }
                    await UpdateExpandedNode(idList.ToArray()).ConfigureAwait(true);
                    await InvokeMethod(ENSUREVISIBLE, new object[] { dataId, node }).ConfigureAwait(true);
                }
            }
            catch
            {
                if (!IsDisposed)
                    throw;
            }
        }

        /// <summary>
        /// Expands all the collapsed TreeView nodes. Specific nodes can be expanded by passing the array of collapsed nodes ID.
        /// </summary>
        /// <param name="nodesId">Specifies the NodeId.</param>
        /// <returns>A System.Threading.Tasks.Task that represents any asynchronous action.</returns>
        public async Task ExpandAllAsync(string[] nodesId = null)
        {
            Internal.ExpandEventArgs nodeArgs = new Internal.ExpandEventArgs
            {
                NodeData = new NodeData()
            };
            List<string>? expandingNodes = new List<string>();
            if (nodesId == null)
            {
                ListReference?.AllParentNodeId.Clear();
                ListReference?.GetAllNodeId(ListReference.DataSource);
                expandingNodes = ListReference?.AllParentNodeId?.ToList();
            }
            else
            {
                expandingNodes = nodesId.ToList();
            }
            IEnumerable<string>? expandedNodes = expandingNodes?.Where(node => InternalExpandedNodes != null && !InternalExpandedNodes.Contains(node));
            if (expandedNodes != null)
            {
                foreach (string nodeId in expandedNodes)
                {
                    nodeArgs.NodeData.Id = nodeId;
                    nodeArgs.IsLoaded = false;
                    if (nodeArgs.NodeData.Id != null)
                    {
                        treeExpandAll = true;
                        await TriggerNodeExpandingEvent(nodeArgs).ConfigureAwait(true);
                    }
                }
            }
        }

        /// <summary>
        /// Gets all the disabled nodes including child, whether it is loaded or not.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task that represents any asynchronous action.</returns>
        public async Task<List<string>> GetDisabledNodesAsync()
        {
            return await Task.FromResult(AllDisabledNodes!).ConfigureAwait(true);
        }

        /// <summary>
        /// Get the node's data such as id, text, parentID, selected, isChecked, and expanded by passing the node element or it's ID.
        /// </summary>
        /// <param name="node">Specifies ID of TreeView node.</param>
        /// <returns>Return TreeData.</returns>
        public NodeData GetNode(string node)
        {
            return node != null ? ListReference?.GetNodeDetails(node) : new NodeData();
        }

        /// <summary>
        ///  Gets the updated data source of TreeView after performing some operation like drag and drop, node editing,
        /// node selecting/unselecting, node expanding/collapsing, node checking/unchecking, adding, and removing node.
        ///  When the ID of TreeView node is passed as arguments for this method then it will return the updated data source
        /// of the corresponding node otherwise it will return the entire updated data source of TreeView.
        ///  The updated data source also contains the custom attributes if specified in data source.
        /// </summary>
        /// <param name="node">Specifies ID of TreeView node.</param>
        /// <returns>Return TreeData.</returns>
        public List<TValue> GetTreeData(string node = null)
        {
            try
            {
                List<TValue>? treeData = ListReference?.GetTreeViewData(node);
                return treeData!;
            }
            catch
            {
                if (!IsDisposed)
                    throw;
                return null!;
            }
        }

        /// <summary>
        /// Removes a collection of TreeView nodes by passing an array of node details as an argument to this method.
        /// </summary>
        /// <param name="nodes">Specifies the array of TreeView nodes ID.</param>
        public void RemoveNodes(string[] nodes)
        {
            try
            {
                ListReference?.RemoveNodes(nodes);
            }
            catch
            {
                if (!IsDisposed)
                    throw;
            }
        }

        /// <summary>
        /// Unchecks all the checked nodes. Specific nodes can also be unchecked by passing array of checked nodes
        /// as an argument to this method.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task that represents any asynchronous action.</returns>
        /// <param name="nodesId">Specifies the Id of the node to be unchecked.</param>
        public async Task UncheckAllAsync(string[] nodesId = null)
        {
            if (ShowCheckBox)
            {
                List<string>? checkedNodes = AllCheckedNodes?.Keys?.ToList();
                NodeCheckEventArgs nodeArgs = new NodeCheckEventArgs
                {
                    NodeData = new NodeData()
                };
                Dictionary<string, TValue>? dictData = ListReference?.DataType == TreeViewDataType.SelfReferential ? ListReference?.ItemsData?.ToDictionary(data => GetValue(TreeViewFields?.Id, data).ToString()) : null;
                if (checkedNodes != null)
                {
                    foreach (string nodeId in checkedNodes)
                    {
                        if (nodesId != null && nodesId.Contains(nodeId))
                        {
                            nodeArgs.NodeData.Id = nodeId;
                            nodeArgs.Action = "uncheck";
                        }
                        else if (nodesId == null && AllCheckedNodes.Count > 0)
                        {
                            nodeArgs.NodeData.Id = nodeId;
                            nodeArgs.Action = "uncheckall";
                        }

                        if (nodeArgs.NodeData.Id != null)
                        {
                            await TriggerNodeCheckingEvent(nodeArgs, dictData).ConfigureAwait(true);
                            nodeArgs.NodeData.Id = null!;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Replaces the text of the TreeView node with the given text.
        /// </summary>
        /// <param name="target">Specifies the ID of TreeView node to be refreshed.</param>
        /// <param name="newData">Specifies the new Data of TreeView node.</param>
        /// <returns>A System.Threading.Tasks.Task that represents any asynchronous action.</returns>
        public async Task RefreshNodeAsync(string target, List<TValue> newData)
        {
            if (target != null && newData != null && ListReference != null)
            {
                ListReference.IsRefreshNode = true;
                ListReference.RefreshTreeNodes(target, newData);
                await TriggerDataSourceChangedEvent().ConfigureAwait(true);
                ListReference.IsRefreshNode = false;
            }
        }

        /// <summary>
        /// Gets all the checked nodes including intermediate nodes.
        /// </summary>
        /// <param name = "includeInterMediate"> Set to <c>true</c> or <c>false</c> to include intermediate node details.</param>
        /// <returns>Return all checked nodes.</returns>
        public List<TValue> GetAllCheckedNodes(bool includeInterMediate)
        {
            return includeInterMediate ? AllCheckedNodes?.Keys?.SelectMany(id => ListReference.GetTreeViewData(id))?.ToList() : AllCheckedNodes?.Where(item => item.Value?.ToString() == "true")?.SelectMany(id => ListReference.GetTreeViewData(id.Key))?.ToList();
        }
    }
}
