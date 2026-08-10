using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace Syncfusion.Blazor.Navigations.Internal
{
    /// <summary>
    /// An enum type that denotes the Treeview data source types.
    /// </summary>
    /// <exclude/>
    internal enum TreeViewDataType
    {
        /// <summary>
        /// Specifies 'SelfReferential' Data type.
        /// </summary>
        SelfReferential,

        /// <summary>
        /// Specifies 'Hierarchical' Data type.
        /// </summary>
        Hierarchical,

        /// <summary>
        /// Specifies 'RemoteData' Data type.
        /// </summary>
        RemoteData
    }

    /// <summary>
    /// List generation of TreeView component.
    /// </summary>
    /// <typeparam name="TValue">"TValue parameter".</typeparam>
    public partial class ListGeneration<TValue> : SfOwningComponentBase
    {
        private const string TRUE = "true";
        private const string ISTRUE = "True";
        private const string FALSE = "false";
        private const string INTERMEDIATE = "intermediate";
        private const string EXPANDED = "Expanded";
        private const string SELECTED = "Selected";
        private const string CHECK = "check";
        private const string UNCHECK = "uncheck";
        private const string LISTGENERATION_DATASOURCE = "DataSource";
        private const string ICONEXPANDCLASS = "e-icon-expandable";
        private const string SETITEM = "window.localStorage.setItem";

        [CascadingParameter]
        internal SfTreeView<TValue>? Parent { get; set; }
        internal List<TValue>? ChildItems { get; set; } = new List<TValue>();
        internal IEnumerable<TValue>? DataSource { get; set; } = new List<TValue>();
        internal List<string> AllParentNodeId { get; set; } = new List<string>();
        internal List<string> CheckNodeId { get; set; } = new List<string>();
        internal IEnumerable<TValue>? ListData { get; set; }
        internal List<string> RemoteExpandedValues { get; set; } = new List<string>() { };
        internal IEnumerable<TValue>? ItemsData { get; set; }
        internal TreeViewDataType DataType { get; set; }
        internal bool IsRefreshNode { get; set; }
        internal Query? Query { get; set; }
        internal bool isFiltering;
        internal Dictionary<string, List<TValue>> SelfChilds = new Dictionary<string, List<TValue>>();
        internal Dictionary<string, List<TValue>> HierarchicalChilds = new Dictionary<string, List<TValue>>();
        internal bool IsSelfChildsUpdate = true;

        private string? iconClass;
        private bool isRemoteChild;
        private bool listGenerationIsExpanded { get; set; }
        private bool listGenerationIsTextUpdated { get; set; }
        private IEnumerable<TValue>? listGenerationChild { get; set; }
        private DataManager? listGenerationDataManager { get; set; }
        internal List<RemoteFieldsData> listGenerationRemoteData { get; set; } = new List<RemoteFieldsData>();
        private TreeFieldsMapping? listGenerationFieldsMapper { get; set; }
        private bool isParentLevel { get; set; } = true;
        private bool multiSelectFlag { get; set; } = true;

        /// <summary>
        /// Method invoked when the component is ready to start.
        /// </summary>
        /// <returns>"Task".</returns>
        protected override async Task OnInitializedAsync()
        {
            try
            {
                await base.OnInitializedAsync().ConfigureAwait(true);
                DataSource = (IEnumerable<TValue>)Parent.GetValue(LISTGENERATION_DATASOURCE, Parent.TreeViewFields);
                DataSource = DataSource?.ToList();
                DataSource = DataSource?.ToList();
                multiSelectFlag = true;
                List<TValue>? data = DataSource?.ToList();
                if (data?.Count > 0 && data[0].GetType()?.GetProperty(Parent?.TreeViewFields?.Id)?.GetValue(data[0])?.GetType() == typeof(int))
                {
                    Parent.IsNumberTypeId = true;
                }
                ItemsData = GetSortedData((Parent.TreeViewFields).DataSource?.ToList(), Parent.SortOrder.ToString(), Parent.TreeViewFields.Text, Parent.SortComparer);
                if (Parent.ExpandedNodesChanged.HasDelegate && (Parent.ExpandedNodes == null || Parent.ExpandedNodes.Length == 0))
                    Parent.PreventRender();
                Parent.tempSelectedNodes = Parent.SelectedNodes?.ToHashSet() ?? new();
                Parent.tempDataSource = DataSource?.ToList();
                await IdentifyDataSource().ConfigureAwait(true);
            }
            catch
            {
                if (!IsDisposed)
                    throw;
            }
        }

        // Sorting operations for provided data source.
        internal static List<TValue> GetSortedData(List<TValue>? dataSource, string sortOrder, string fieldValue, object? sortComparer = null)
        {
            return (sortOrder != "None" && dataSource != null && sortComparer == null)
                ? DataOperations.PerformSorting<TValue>(dataSource, new List<Sort> { new Sort { Direction = sortOrder, Name = fieldValue } }).ToList() : dataSource;
        }

        private async Task UpdateExpandedNodesState()
        {
            if (Parent.IsDataSourceChanged)
            {
                Parent.AllExpandedNodes = Parent.ExpandedNodes.ToHashSet();
                UpdateHierarchicalAndSelfProps(DataSource);
                Parent.InternalExpandedNodes = Parent.AllExpandedNodes.ToList();
                await Parent.UpdateExpandedNodes().ConfigureAwait(true);
            }
            else
            {
                Parent.InternalExpandedNodes = Parent.ExpandedNodes.ToList();
                if (!Parent.IsCompletelyRendered)
                {
                    Parent.AllExpandedNodes = Parent.ExpandedNodes.ToHashSet();
                }

            }
        }

        // Identify the Bounded data source type and update the selected and checked options values.
        internal async Task IdentifyDataSource(bool isUpdateChecked = false)
        {
            if (Parent?.TreeViewFields?.DataManager != null)
                return;
            bool isPropsUpdated = false;
            if (!string.IsNullOrEmpty(Parent?.TreeViewFields?.ParentID) || (!string.IsNullOrEmpty(Parent?.TreeViewFields?.HasChildren) && string.IsNullOrEmpty(Parent.TreeViewFields.Child)))
            {
                DataType = TreeViewDataType.SelfReferential;
                if (DataSource != null && DataSource.Any() && !DataSource.Any(item => Parent.GetValue(Parent?.TreeViewFields?.ParentID, item)?.ToString() == null))
                {
                    throw new InvalidOperationException("Invalid DataSource, at least one item in DataSource should have ParentID as null");
                }
                if (ItemsData != null)
                {
                    if (Parent.ExpandedNodes != null)
                    {
                        await UpdateExpandedNodesState().ConfigureAwait(true);
                    }
                    else
                    {
                        UpdateHierarchicalAndSelfProps(DataSource);
                        isPropsUpdated = true;
                        Parent.InternalExpandedNodes = (Parent.ExpandedNodes != null) ? Parent.ExpandedNodes.ToList() : Parent.AllExpandedNodes.ToList();
                        await Parent.UpdateExpandedNodes().ConfigureAwait(true);
                    }
                    if(IsSelfChildsUpdate)
                        UpdateSelfChilds();
                    await TypeCheck(GroupingData(null, null), isUpdateChecked, isPropsUpdated).ConfigureAwait(true);
                }
            }
            else
            {
                DataType = TreeViewDataType.Hierarchical;
                if (Parent != null && Parent.ExpandedNodes != null)
                {
                    await UpdateExpandedNodesState().ConfigureAwait(true);
                }
                else if (DataSource != null && DataSource.Any())
                {
                    UpdateHierarchicalAndSelfProps(DataSource);
                    if (Parent.ShowCheckBox && Parent.AllCheckedNodes.Count > 0)
                        await Parent.UpdateCheckedNodes().ConfigureAwait(true);
                    isPropsUpdated = true;
                    Parent.InternalExpandedNodes = Parent.AllExpandedNodes?.ToList();
                    await Parent.UpdateExpandedNodes().ConfigureAwait(true);
                }

                await TypeCheck(ItemsData, isUpdateChecked, isPropsUpdated).ConfigureAwait(true);
            }
        }
        private async Task TypeCheck(IEnumerable<TValue> itemData, bool isUpdateChecked = false, bool isPropsUpdated = false)
        {
            if (ItemsData != null)
            {
                await UpdateSelectedNodes(isPropsUpdated).ConfigureAwait(true);
                if (Parent.ShowCheckBox) await UpdateCheckedNodes(isUpdateChecked, isPropsUpdated).ConfigureAwait(true);
            }
            ListData = itemData;
        }

        internal void RefreshTreeNodes(string target, List<TValue> newData)
        {
            if (DataType == TreeViewDataType.SelfReferential)
            {
                TValue item = GetRemovedSelfData(target, true);
                List<TValue> dataSource = DataSource.ToList();
                int pos = Parent.TreeViewFields.DataSource.IndexOf(item);
                dataSource.Insert(pos, newData[0]);
                DataSource = ItemsData = (IEnumerable<TValue>)dataSource;
            }
            else if (DataType == TreeViewDataType.Hierarchical)
            {
                string? childProp = Parent?.TreeViewFields.Child?.ToString();
                List<TValue> childs = (List<TValue>)(IEnumerable<TValue>)DataUtil.GetObject(childProp, newData[0]);
                bool refreshChild = childs?.Count > 0;

                GetHierarchicalData(target, DataSource);
                if (!refreshChild)
                {
                    newData[0].GetType().GetProperty(childProp)?.SetValue(newData[0], (IEnumerable<TValue>)DataUtil.GetObject(childProp, Parent.TreeViewRemovedData));
                }

                GetRemovedHierData(target, DataSource.ToList(), true, default, newData[0], false);
            }

            ListUpdated();
        }

        // Update the TreeNode Text for Self Referential data source.
        internal void UpdateSelfNodeText(string nodeId, string newText)
        {
            bool isOfflineHierarchicalData = false;
            bool isOfflineData = false;
            if (DataType == TreeViewDataType.RemoteData)
            {
                isOfflineData = Parent.TreeViewFields.DataManager?.Offline ?? false;
                if (isOfflineData && Parent.TreeViewFields.Child != null)
                {
                    isOfflineHierarchicalData = true;
                }
            }

            if (DataType == TreeViewDataType.SelfReferential || (DataType == TreeViewDataType.RemoteData && !isOfflineData) || (isOfflineData && !isOfflineHierarchicalData))
            {
                List<TValue> updatedData = ItemsData?.ToList();
                foreach (TValue item in updatedData)
                {
                    UpdateFields(0);
                    if (UpdateNodeText(nodeId, item, listGenerationFieldsMapper, newText))
                    {
                        break;
                    }

                    if (DataType == TreeViewDataType.RemoteData && !listGenerationIsTextUpdated)
                    {
                        UpdateRemoteNodeText(listGenerationFieldsMapper, item, nodeId, newText, 1);
                    }
                }

                Parent?.TreeViewFields?.GetType().GetProperty(LISTGENERATION_DATASOURCE)?.SetValue(Parent.TreeViewFields, updatedData);
                DataSource = ItemsData = updatedData;
            }
            else if (DataType == TreeViewDataType.Hierarchical || (isOfflineData && isOfflineHierarchicalData))
            {
                UpdateFields(0);
                UpdatedHierarchicalText(nodeId, newText, DataSource?.ToList());
            }

            listGenerationIsTextUpdated = false;
        }

        private void UpdateRemoteNodeText(TreeFieldsMapping fields, TValue updatedData, string nodeId, string newText, int level)
        {
            UpdateFields(level);
            string idAttrValue = GetAttrValue(fields?.Id, updatedData);
            IEnumerable<TValue> childData = (IEnumerable<TValue>)GetChildRemoteData(idAttrValue);
            if (childData != null)
            {
                foreach (TValue child in childData)
                {
                    if (UpdateNodeText(nodeId, child, listGenerationFieldsMapper, newText))
                    {
                        break;
                    }
                    UpdateRemoteNodeText(listGenerationFieldsMapper, child, nodeId, newText, level + 1);
                }
            }
        }

        internal void DropNodeAsSiblingNodeHier(string dragLi, string dropLi, bool? pre, TValue removedData, IEnumerable<TValue>? listData, TValue? parentNode, bool isExternalDrop = false)
        {
            List<TValue> dataList = listData?.ToList() ?? new List<TValue>();
            if (isExternalDrop)
            {
                dataList.Add(removedData);
            }
            else
            {
                bool IsDropped = false;
                for (int i = 0; i < dataList.Count; i++)
                {
                    string idAttrValue = GetAttrValue(Parent.TreeViewFields?.Id, dataList[i]);
                    IEnumerable<TValue> childs = (IEnumerable<TValue>)DataUtil.GetObject(Parent.TreeViewFields.Child.ToString(), dataList[i]);
                    if (idAttrValue == dropLi)
                    {
                        if (pre == true)
                        {
                            dataList.Insert(i, removedData);
                        }
                        else
                        {
                            dataList.Insert(i + 1, removedData);
                        }

                        if (parentNode != null)
                        {
                            PropertyInfo property = parentNode.GetType().GetProperty(Parent.TreeViewFields?.Child?.ToString());
                            FieldInfo field = parentNode.GetType().GetField(Parent.TreeViewFields?.Child?.ToString());
                            property?.SetValue(parentNode, dataList);
                            field?.SetValue(parentNode, dataList);
                        }
                        IsDropped = true;
                        break;
                    }

                    if (childs != null)
                    {
                        DropNodeAsSiblingNodeHier(dragLi, dropLi, pre, removedData, childs, dataList[i]);
                    }
                }
                if (!IsDropped && isExternalDrop)
                {
                    dataList.Add(removedData);
                }
            }
            ItemsData = ListData = dataList;
        }

        internal void DropNodeAsSiblingNode(string dropLi, bool? pre, TValue removedData, bool isExternalDrop = false)
        {
            List<TValue> dataList = DataSource?.ToList();
            if (isExternalDrop)
            {
                dataList.Add(removedData);
            }
            else
            {
                bool IsDropped = false;
                for (int i = 0; i < dataList.Count; i++)
                {
                    string idAttrValue = GetAttrValue(Parent.TreeViewFields?.Id, dataList[i]);
                    if (idAttrValue == dropLi)
                    {
                        if (pre == true)
                        {
                            dataList.Insert(i, removedData);
                        }
                        else
                        {
                            dataList.Insert(i + 1, removedData);
                        }
                        Parent.TreeViewFields.GetType().GetProperty("DataSource").SetValue(Parent.TreeViewFields, dataList);
                        IsDropped = true;
                        break;
                    }
                }
                if (!IsDropped)
                {
                    dataList.Add(removedData);
                }
            }
            DataSource = dataList;
        }

        internal TValue GetRemovedSelfData(string? id, bool isRemoveCall = false, bool isHasChildUpdate = false)
        {
            TValue? removedData = default;
            List<TValue>? dataList = DataSource?.ToList();
            int index = dataList?.FindIndex(d => GetAttrValue(Parent.TreeViewFields?.Id, d) == id) ?? -1;
            if (index >= 0)
            {
                removedData = dataList[index];
                if (isRemoveCall) dataList.RemoveAt(index);
                if (isHasChildUpdate) removedData?.GetType().GetProperty(Parent.TreeViewFields?.HasChildren)?.SetValue(removedData, false);
                DataSource = dataList;
            }
            return removedData;
        }

        internal void GetAndRemovedHierData(string id, List<TValue>? dataSource, object? parentNode)
        {
            int dataLength = dataSource.Count;
            TreeViewFieldsSettings<TValue>? fields = Parent?.TreeViewFields;
            if (dataSource == null)
            {
                Parent.TreeViewRemovedData = default;
            }

            for (int i = 0; i < dataLength; i++)
            {
                string idAttrValue = GetAttrValue(fields.Id, dataSource[i]);
                if (dataSource[i] != null && idAttrValue != null && idAttrValue == id)
                {
                    Parent.TreeViewRemovedData = dataSource[i];
                    dataSource.RemoveAt(i);
                    if (parentNode != null)
                    {
                        PropertyInfo property = parentNode.GetType().GetProperty(fields?.Child?.ToString());
                        FieldInfo field = parentNode.GetType().GetField(fields?.Child?.ToString());
                        if (property != null)
                        {
                            property.SetValue(parentNode, dataSource);
                        }
                        else if (field != null)
                        {
                            field.SetValue(parentNode, dataSource);
                        }
                    }
                    break;
                }
                else
                {
                    IEnumerable<TValue> childs = (IEnumerable<TValue>)DataUtil.GetObject(fields.Child.ToString(), dataSource[i]);
                    if (childs != null) GetAndRemovedHierData(id, childs.ToList(), dataSource[i]);
                }
            }

            ItemsData = DataSource = dataSource;
            Parent.InternalData = DataSource.ToList();
        }

        internal void GetRemovedHierData(string id, List<TValue> dataSource, bool isChild = false, TValue? parent = default, TValue? newData = default, bool isRemove = false)
        {
            TreeViewFieldsSettings<TValue> fields = Parent.TreeViewFields;
            if (dataSource == null)
            {
                Parent.TreeViewRemovedData = default;
            }

            for (int i = 0; i < dataSource.Count; i++)
            {
                string idAttrValue = GetAttrValue(fields.Id, dataSource[i]);
                if (dataSource[i] != null && idAttrValue != null && idAttrValue == id)
                {
                    Parent.TreeViewRemovedData = dataSource[i];
                    dataSource.RemoveAt(i);
                    if (!isRemove)
                    {
                        dataSource.Insert(i, newData);
                    }

                    if (isChild && parent != null)
                    {
                        PropertyInfo? prop = parent.GetType().GetProperty(fields?.Child);
                        if (prop != null)
                        {
                            prop.SetValue(parent, dataSource);
                        }
                        else
                        {
                            FieldInfo? field = parent.GetType().GetField(fields?.Child);
                            field?.SetValue(parent, dataSource);
                        }
                    }
                }
                else
                {
                    IEnumerable<TValue> childs = (IEnumerable<TValue>)DataUtil.GetObject(fields.Child.ToString(), dataSource[i]);
                    if (childs != null) GetRemovedHierData(id, childs.ToList(), true, dataSource[i], newData, isRemove);
                }
            }

            DataSource = dataSource;
        }

        private bool UpdateNodeText(string nodeId, TValue updatedData, TreeFieldsMapping fields, string newText)
        {
            if (GetAttrValue(fields.Id, updatedData) == nodeId)
            {
                updatedData.GetType().GetProperty(fields?.Text.ToString())?.SetValue(updatedData, newText);
                listGenerationIsTextUpdated = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        // Update the TreeNode Text for Hierarchical data source.
        private void UpdatedHierarchicalText(string nodeId, string newText, List<TValue> dataSource)
        {
            TreeViewFieldsSettings<TValue> fields = Parent.TreeViewFields;
            List<TValue> updatedData = dataSource?.ToList();
            foreach (TValue data in updatedData)
            {
                if (UpdateNodeText(nodeId, data, listGenerationFieldsMapper, newText))
                {
                    break;
                }
                IEnumerable<TValue> childs = (IEnumerable<TValue>)DataUtil.GetObject(fields.Child.ToString(), data);
                if (childs != null)
                {
                    UpdatedHierarchicalText(nodeId, newText, (List<TValue>)childs);
                }
            }

            dataSource?.Concat(updatedData);
            Parent?.TreeViewFields?.GetType().GetProperty(LISTGENERATION_DATASOURCE)?.SetValue(fields, dataSource);
            ItemsData = (IEnumerable<TValue>)dataSource;
        }

        internal async Task UpdateCheckedNodes(bool isUpdateChecked = false, bool isPropsUpdated = false)
        {
            if (Parent != null && Parent.ShowCheckBox)
            {
                if (Parent.CheckedNodes != null && Parent.CheckedNodes.Length > 0)
                {
                    Parent.AllCheckedNodes = Parent.CheckedNodes.ToDictionary(id => id, id => (object)TRUE).Concat(Parent.AllCheckedNodes).GroupBy(i => i.Key).ToDictionary(i => i.Key, i => i.First().Value);
                    if (!isUpdateChecked)
                    {
                        await Parent.UpdateCheckedNodes().ConfigureAwait(true);
                    }
                }

                if (DataType == TreeViewDataType.SelfReferential || DataType == TreeViewDataType.Hierarchical)
                {
                    if ((Parent.CheckedNodes == null || Parent.CheckedNodes.Length == 0) && !isPropsUpdated)
                    {
                        UpdateHierarchicalAndSelfProps(DataSource);
                        if (Parent.ShowCheckBox && Parent.AllCheckedNodes.Count > 0)
                            await Parent.UpdateCheckedNodes().ConfigureAwait(true);
                    }

                    if (Parent.AutoCheck)
                    {
                        List<KeyValuePair<string, object>> intermediateNodes = Parent.AllCheckedNodes?.Where(y => y.Value?.ToString() == INTERMEDIATE)?.ToList();
                        foreach (var item in intermediateNodes)
                        {
                            Parent.AllCheckedNodes.Remove(item.Key);
                        }
                        List<string> checkedNodes = Parent.AllCheckedNodes.Where(x => x.Value == TRUE as object).Select(x => x.Key).ToList();
                        UpdateChildCheckedNodes(checkedNodes, CHECK, isPropsUpdated);
                        await Parent.UpdateCheckedNodes().ConfigureAwait(true);
                        if (DataType == TreeViewDataType.SelfReferential)
                        {
                            UpdateSelfIntermediateState(checkedNodes, ItemsData.ToDictionary(data => GetAttrValue(Parent.TreeViewFields?.Id, data)), new());
                        }
                        else
                        {
                            UpdateCheckedDataFromDS(checkedNodes, CHECK);
                        }
                    }
                }
            }
        }

        // Update child checked state for Hierarchical data source.
        private void UpdateChildCheckedState(List<TValue> childItems, TValue treeData, List<string> checkedNodes, string action)
        {
            TreeViewFieldsSettings<TValue> fields = Parent.TreeViewFields;
            string checkedParent = Parent.GetValue(fields?.Id, treeData)?.ToString();
            int checkCount = 0;
            int intermediateCount = 0;
            foreach (TValue childItem in childItems)
            {
                string checkedChild = Parent.GetValue(fields?.Id, childItem)?.ToString();
                IEnumerable<TValue> childs = (IEnumerable<TValue>)DataUtil.GetObject(fields.Child.ToString(), childItem);
                List<TValue> childData = childs?.ToList();
                if (childData != null && childData.Count > 0)
                {
                    UpdateChildCheckedState(childData, childItem, checkedNodes, action);
                }

                if (Parent.AllCheckedNodes.TryGetValue(checkedChild, out object nodeId))
                {
                    if (nodeId.ToString() == TRUE)
                    {
                        checkCount++;
                    }
                    else if (nodeId.ToString() == INTERMEDIATE)
                    {
                        intermediateCount++;
                    }
                }
            }

            UpdateIntermediateState(checkCount, childItems.Count, checkedParent, intermediateCount);
        }

        private void UpdateIntermediateState(int checkCount, int? childLength, string checkedParent, int intermediateCount)
        {
            if (childLength != null && checkCount == childLength)
            {
                SfBaseUtils.UpdateDictionary(checkedParent, TRUE, Parent.AllCheckedNodes);
            }
            else if (checkCount == 0)
            {
                if (intermediateCount > 0)
                {
                    Parent.AllCheckedNodes[checkedParent] = INTERMEDIATE;
                }
                else
                {
                    Parent.AllCheckedNodes.Remove(checkedParent);
                }
            }
            else
            {
                SfBaseUtils.UpdateDictionary(checkedParent, INTERMEDIATE, Parent.AllCheckedNodes);
            }
        }

        // Based on checked nodes values to updated the checked tree node's list.
        internal void UpdateCheckedDataFromDS(List<string> checkedNodes, string action)
        {
            List<TValue> itemsData = DataSource != null ? DataSource.ToList() : new List<TValue>();
            foreach (TValue itemData in itemsData)
            {
                IEnumerable<TValue> childItems = Parent.TreeViewFields?.Child != null ? (IEnumerable<TValue>)DataUtil.GetObject(Parent.TreeViewFields.Child.ToString(), itemData) : null;
                if (childItems?.Any() == true)
                {
                    UpdateChildCheckedState(childItems.ToList(), itemData, checkedNodes, action);
                }
            }
        }

        // Update intermediate state for self referential data source.
        internal void UpdateSelfIntermediateState(List<string> checkedNodes, Dictionary<string, TValue>? dictData, HashSet<string> addedNodes)
        {
            foreach (string checkedNode in checkedNodes)
            {
                string parentID = GetParentId(checkedNode, dictData);
                if (parentID != null)
                {
                    if (addedNodes.Contains(parentID))
                        continue;
                    addedNodes.Add(parentID);
                    List<TValue> siblingNodes = Parent.isDdtFiltering ? Parent.tempDataSource.Where(item => GetAttrValue(Parent.TreeViewFields.ParentID, item) == parentID).ToList() : SelfChilds.GetValueOrDefault(parentID) ?? new();
                    int checkCount = 0;
                    int intermediateCount = 0;
                    foreach (TValue siblingNode in siblingNodes)
                    {
                        string idAttrValue = GetAttrValue(Parent.TreeViewFields?.Id, siblingNode);
                        if (Parent.AllCheckedNodes.TryGetValue(idAttrValue, out object nodeId))
                        {
                            if (nodeId.ToString() == TRUE)
                            {
                                ++checkCount;
                            }
                            else if (nodeId.ToString() == INTERMEDIATE)
                            {
                                ++intermediateCount;
                            }
                        }
                    }
                    UpdateIntermediateState(checkCount, siblingNodes.Count, parentID, intermediateCount);
                    if (GetParentId(parentID, dictData) != null)
                        UpdateSelfIntermediateState(new() { parentID }, dictData, addedNodes);
                }
            }
        }

        // Get Parent node id for self referential data source.
        private string GetParentId(string id, Dictionary<string, TValue> dictData) => dictData.TryGetValue(id, out TValue data) ? GetAttrValue(Parent.TreeViewFields?.ParentID, data) : null;

        // Update child checked state for Hierarchical data source.
        internal void UpdateHierarchicalChildCheckedNodes(List<string> checkedNodes, string action)
        {
            foreach (string checkedNode in checkedNodes)
            {
                GetHierarchicalChild(checkedNode, DataSource);
                if (ChildItems != null && ChildItems.Count > 0)
                {
                    UpdateChildCheckedValues(ChildItems, action);
                }
            }
        }

        // Update child checked state values for tree nodes.
        private void UpdateChildCheckedValues(List<TValue> dataSource, string action)
        {
            foreach (TValue item in dataSource)
            {
                string idAttrValue = GetAttrValue(Parent.TreeViewFields?.Id, item);
                if (Parent.AllCheckedNodes.ContainsKey(idAttrValue))
                {
                    if (action == UNCHECK)
                    {
                        Parent.AllCheckedNodes.Remove(idAttrValue);
                    }
                    else
                    {
                        Parent.AllCheckedNodes[idAttrValue] = TRUE;
                    }
                }
                else if (action == CHECK)
                {
                    SfBaseUtils.UpdateDictionary(idAttrValue, TRUE, Parent.AllCheckedNodes);
                }

                IEnumerable<TValue> childs = (IEnumerable<TValue>)DataUtil.GetObject(Parent.TreeViewFields?.Child.ToString(), item);
                if (childs != null)
                {
                    UpdateChildCheckedValues((List<TValue>)childs, action);
                }
            }
        }

        // Update child checked state values for tree nodes.
        internal void UpdateChildCheckedNodes(List<string> checkedNodes, string action, bool isPropsUpdated = false)
        {
            List<string> childNodes = new List<string>();
            foreach (string checkedNode in checkedNodes)
            {
                if (DataType == TreeViewDataType.SelfReferential)
                {
                    ChildItems = null;
                    GetSelfChild(checkedNode, ItemsData);
                }
                else if (DataType == TreeViewDataType.Hierarchical)
                {
                    GetHierarchicalChild(checkedNode, DataSource, isPropsUpdated);
                }

                if (ChildItems != null && ChildItems.Count > 0)
                {
                    foreach (TValue childItem in ChildItems)
                    {
                        if (ChildItems == null)
                        {
                            return;
                        }

                        string idAttrValue = GetAttrValue(Parent.TreeViewFields?.Id, childItem);
                        childNodes.Add(idAttrValue);
                        if (Parent.AllCheckedNodes.ContainsKey(idAttrValue))
                        {
                            if (action == UNCHECK)
                            {
                                Parent.AllCheckedNodes.Remove(idAttrValue);
                            }
                            else
                            {
                                Parent.AllCheckedNodes[idAttrValue] = TRUE;
                            }
                        }
                        else if (action == CHECK)
                        {
                            SfBaseUtils.UpdateDictionary(idAttrValue, TRUE, Parent.AllCheckedNodes);
                            if (DataType == TreeViewDataType.Hierarchical && Parent.CheckedNodesChanged.HasDelegate)
                            {
                                List<TValue> tempChild = new List<TValue>(ChildItems);
                                UpdateChildCheckedNodes(childNodes, action);
                                ChildItems = new List<TValue>(tempChild);
                            }
                        }
                    }

                    ChildItems = null;
                }
            }
        }

        // Update selected nodes for tree view.
        private async Task UpdateSelectedNodes(bool isPropsUpdated = false)
        {
            if (Parent != null && Parent.SelectedNodes != null && Parent.SelectedNodes.Length > 0)
            {
                if (Parent.AllowMultiSelection)
                {
                    Parent.AllSelectedNodes = Parent.SelectedNodes.ToHashSet();
                }
                else
                {
                    Parent.AllSelectedNodes.Clear();
                    Parent.AllSelectedNodes.Add(Parent.SelectedNodes[0]);
                    await Parent.UpdateSelectedNodes().ConfigureAwait(true);
                }
            }
            else
            {
                if (DataType == TreeViewDataType.Hierarchical || DataType == TreeViewDataType.SelfReferential)
                {
                    if (!isPropsUpdated)
                        UpdateHierarchicalAndSelfProps(DataSource, Parent?.CheckedNodes?.Length == 0);
                    if (Parent.AllSelectedNodes.Count > 0)
                    {
                        if (!Parent.AllowMultiSelection && Parent.AllSelectedNodes.Count > 1)
                        {
                            List<string> selectedNodes = Parent.AllSelectedNodes.ToList();
                            selectedNodes.RemoveRange(0, Parent.AllSelectedNodes.Count - 1);
                            Parent.AllSelectedNodes = selectedNodes.ToHashSet();
                        }
                        Parent.UpdateTreeSelectedNodes();
                        await Parent.UpdateSelectedNodes().ConfigureAwait(true);
                    }
                }
            }
        }

        // Get the attribute value based on current data.
        internal string GetAttrValue(string propertyName, TValue currentData)
        {
            return Parent.GetValue(propertyName, currentData)?.ToString();
        }

        private void UpdateSelfChilds()
        {
            if (DataSource == null || !IsSelfChildsUpdate)
                return;

            foreach (var itemData in DataSource)
            {
                string parentId = Parent.GetValue(Parent.TreeViewFields?.ParentID, itemData)?.ToString();
                if (!string.IsNullOrEmpty(parentId))
                {
                    if (!SelfChilds.TryGetValue(parentId, out List<TValue> value))
                    {
                        SelfChilds[parentId] = new List<TValue>() { itemData };
                    }
                    else
                    {
                        value.Add(itemData);
                    }
                }
            }
            IsSelfChildsUpdate = false;
        }

        private void UpdatePropertyValues(List<string> idValue, HashSet<string> collections, string action)
        {
            IEnumerable<string> differedId = idValue.Except(collections);
            if (differedId.Any())
            {
                if (DataType == TreeViewDataType.Hierarchical && !Parent.AllowMultiSelection && collections.Count > 0 && action == SELECTED)
                {
                    collections.Clear();
                }
                collections.UnionWith(differedId);
                if (action == EXPANDED && Parent != null && Parent.ExpandedNodes != null && !idValue.Except(Parent.ExpandedNodes).Any() && idValue.Except(Parent.InternalExpandedNodes).Any())
                {
                    IEnumerable<string> expandedDifferedId = idValue.Except(Parent.InternalExpandedNodes);
                    if (expandedDifferedId.Any())
                        Parent.InternalExpandedNodes.AddRange(expandedDifferedId);
                }
            }
        }

        private string CheckConditions(string id)
        {
            if ((Parent.CheckAction == "uncheckall") || (!Parent.IsNodeClicked && Parent.CheckAction == UNCHECK && (id == Parent.InteractedNodeId)) || (Parent.CheckAction == null && id == Parent.InteractedNodeId && Parent.IsInteractedNodeChecked))
            {
                return FALSE;
            }
            else if ((Parent.CheckAction == null) || (Parent.CheckAction != UNCHECK) || Parent.isInteracted)
            {
                return TRUE;
            }
            return FALSE;
        }

        private async void UpdateHierarchicalAndSelfProps(IEnumerable<TValue> dataSource, bool needCheckBoxUpdate = true)
        {
            List<TValue> itemsData = dataSource?.ToList();
            TreeViewFieldsSettings<TValue> fields = Parent?.TreeViewFields;
            if (itemsData != null && (fields?.Expanded != null || fields?.Selected != null || fields?.IsChecked != null || fields?.Child != null))
            {
                if (DataType == TreeViewDataType.SelfReferential)
                {
                    if (!string.IsNullOrEmpty(fields?.Expanded))
                    {
                        List<string> eNodes = itemsData.Where(item => GetAttrValue(fields.Expanded, item) == ISTRUE).Select(item => GetAttrValue(fields?.Id, item)).ToList();
                        UpdatePropertyValues(eNodes, Parent.AllExpandedNodes, EXPANDED);
                    }
                    if (!string.IsNullOrEmpty(fields?.Selected))
                    {
                        List<string> sNodes = itemsData.Where(item => GetAttrValue(fields.Selected, item) == ISTRUE).Select(item => GetAttrValue(fields?.Id, item)).ToList();
                        UpdatePropertyValues(sNodes, Parent.AllSelectedNodes, SELECTED);
                    }
                    if (needCheckBoxUpdate && !string.IsNullOrEmpty(fields?.IsChecked) && !(Parent.CheckedNodes == null && (Parent.CheckAction == UNCHECK || Parent.CheckAction == CHECK)))
                    {
                        List<string> cNodes = itemsData.Where(item => GetAttrValue(fields.IsChecked, item) == ISTRUE).Select(item => GetAttrValue(fields?.Id, item)).ToList();
                        List<string> differedID = cNodes.Except(Parent.AllCheckedNodes.Select(node => node.Key))?.ToList();
                        if (differedID?.Count > 0)
                        {
                            Parent.AllCheckedNodes = Parent.AllCheckedNodes.Concat(differedID.ToDictionary(id => id, id => (object)CheckConditions(id))).ToDictionary(x => x.Key, x => x.Value);
                            await Parent.UpdateCheckedNodes().ConfigureAwait(true);
                        }
                    }
                }

                foreach (TValue itemData in itemsData)
                {
                    if (DataType == TreeViewDataType.SelfReferential && IsSelfChildsUpdate)
                    {
                        string parentId = Parent.GetValue(fields?.ParentID, itemData)?.ToString();
                        if (!string.IsNullOrEmpty(parentId))
                        {
                            if (!SelfChilds.TryGetValue(parentId, out List<TValue> value))
                            {
                                SelfChilds.Add(parentId, new List<TValue>() { itemData });
                            }
                            else
                            {
                                value.Add(itemData);
                            }
                        }
                    }
                    if (DataType == TreeViewDataType.Hierarchical)
                    {
                        string idAttrValue = GetAttrValue(fields?.Id, itemData);
                        string attrValue;
                        if (fields?.Expanded != null)
                        {
                            attrValue = GetAttrValue(fields.Expanded, itemData);
                            if (attrValue != null && attrValue == ISTRUE)
                                UpdatePropertyValues(new() { idAttrValue }, Parent.AllExpandedNodes, EXPANDED);
                        }
                        if (fields?.Selected != null)
                        {
                            attrValue = GetAttrValue(fields.Selected, itemData);
                            if (attrValue != null && attrValue == ISTRUE)
                                UpdatePropertyValues(new() { idAttrValue }, Parent.AllSelectedNodes, SELECTED);
                        }
                        if (fields?.IsChecked != null && !(Parent.CheckedNodes == null && Parent.CheckAction == UNCHECK))
                        {
                            attrValue = GetAttrValue(fields.IsChecked, itemData);
                            UpdateCheckedProperty(attrValue, idAttrValue, Parent.AllCheckedNodes, Parent);
                        }
                        else if (fields?.IsChecked != null && !(Parent.CheckedNodes == null && Parent.CheckAction == CHECK))
                        {
                            attrValue = GetAttrValue(fields.IsChecked, itemData);
                            UpdateCheckedProperty(attrValue, idAttrValue, Parent.AllCheckedNodes, Parent);
                        }
                        if (fields?.Child != null)
                        {
                            IEnumerable<TValue> childs = (IEnumerable<TValue>)DataUtil.GetObject(fields?.Child, itemData);
                            if (childs?.Any() == true)
                            {
                                List<TValue> childItems = childs?.ToList();
                                if (!string.IsNullOrEmpty(idAttrValue))
                                {
                                    if (!HierarchicalChilds.TryGetValue(idAttrValue, out List<TValue> value))
                                    {
                                        HierarchicalChilds.Add(idAttrValue, childItems);
                                    }
                                    else
                                    {
                                        value.Add(itemData);
                                    }
                                }
                                UpdateHierarchicalAndSelfProps(childItems);
                            }
                        }
                    }
                }
                 if (fields?.IsChecked != null && Parent.ShowCheckBox && DataType == TreeViewDataType.Hierarchical)
                {
                    await Parent.UpdateCheckedNodes().ConfigureAwait(true);
                }
                IsSelfChildsUpdate = false;
            }
        }

        // Update Checked node values based on checked attribute value.
        private static void UpdateCheckedProperty(string attrValue, string idValue, Dictionary<string, object> collections, SfTreeView<TValue> parent)
        {
            if (attrValue == ISTRUE && !collections.TryGetValue(idValue, out _))
            {
                if ((parent.CheckAction == "uncheckall") || (!parent.IsNodeClicked && parent.CheckAction == UNCHECK && (idValue == parent.InteractedNodeId)) || (parent.CheckAction == null && idValue == parent.InteractedNodeId && parent.IsInteractedNodeChecked))
                {
                    collections.Add(idValue, FALSE);
                }
                else if ((parent.CheckAction == null) || (parent.CheckAction != UNCHECK) || parent.isInteracted)
                {
                    collections.Add(idValue, TRUE);
                }
            }
        }

        internal async Task<List<string>> GetHierarchicalAndRemoteParent(string id, IEnumerable<TValue> dataSource, List<string>? parentsId = null)
        {
            parentsId ??= new List<string>();
            List<TValue> data = dataSource.ToList();
            TreeViewFieldsSettings<TValue> fields = Parent.TreeViewFields;
            bool isRemote = DataType.ToString() == "RemoteData";
            var index = data.FindIndex((item) => GetAttrValue(isRemote ? listGenerationFieldsMapper.Id : fields.Id, item) == id);

            if (index == -1)
            {
                List<TValue> childs;
                string idAttrValue;
                foreach (var node in data)
                {
                    if (isRemote)
                    {
                        if (isParentLevel)
                        {
                            UpdateFields(0);
                        }
                        idAttrValue = GetAttrValue(listGenerationFieldsMapper.Id, node);
                        UpdateFields(1);
                        Query = GettingQuery(listGenerationFieldsMapper, idAttrValue);
                        object itemsData = (listGenerationDataManager != null) ? await listGenerationDataManager.ExecuteQuery<TValue>(Query).ConfigureAwait(true) : null;
                        childs = GetDataSource(itemsData);
                        UpdateFields(1);
                    }
                    else
                    {
                        idAttrValue = GetAttrValue(fields.Id, node);
                        childs = fields.Child != null ? (DataUtil.GetObject(fields.Child.ToString(), node) as IEnumerable<TValue>)?.ToList() : null;
                    }
                    if (childs?.Count > 0)
                    {
                        isParentLevel = false;
                        index = childs.FindIndex((item) => GetAttrValue(isRemote ? listGenerationFieldsMapper.Id : fields.Id, item) == id);
                        if (index == -1)
                        {
                            await GetHierarchicalAndRemoteParent(id, childs, parentsId).ConfigureAwait(true);
                        }
                        else
                        {
                            parentsId.Add(idAttrValue);
                            isParentLevel = true;
                            UpdateFields(0);
                            await GetHierarchicalAndRemoteParent(idAttrValue, DataSource, parentsId).ConfigureAwait(true);
                            return parentsId;
                        }
                        isParentLevel = true;
                    }
                }
            }
            return parentsId;
        }

        // Get child nodes for Hierarchical data source.
        internal void GetHierarchicalChild(string id, IEnumerable<TValue> dataSource, bool isPropsUpdated = false)
        {
            if (HierarchicalChilds?.Count > 0 || isPropsUpdated)
            {
                ChildItems = HierarchicalChilds?.GetValueOrDefault(id);
                return;
            }
            IEnumerable<TValue> data = dataSource;
            IEnumerable<TValue> childs;
            TreeViewFieldsSettings<TValue> fields = Parent.TreeViewFields;
            foreach (TValue currentData in data)
            {
                string idAttrValue = GetAttrValue(fields.Id, currentData);
                childs = fields.Child != null ? (IEnumerable<TValue>)DataUtil.GetObject(fields.Child.ToString(), currentData) : null;
                if (idAttrValue == id)
                {
                    ChildItems = childs?.ToList();
                    break;
                }

                if (childs != null)
                {
                    GetHierarchicalChild(id, childs);
                }
            }
        }

        // Get child nodes for Self Referential data source.
        private void GetSelfChild(string id, IEnumerable<TValue> dataSource)
        {
            ChildItems ??= new List<TValue>();
            IEnumerable<TValue> itemsData = dataSource;
            TreeViewFieldsSettings<TValue> fields = Parent.TreeViewFields;
            List<TValue> childs = SelfChilds.GetValueOrDefault(id) ?? new List<TValue>();
            ChildItems.AddRange(childs);
            foreach (TValue child in childs)
            {
                string parentID = Parent.GetValue(fields.Id, child)?.ToString();
                bool? hasChild = (bool?)Parent.GetValue(fields.HasChildren?.ToString(), child);
                if (parentID != null && hasChild == true)
                {
                    GetSelfChild(parentID, itemsData);
                }
            }
        }

        internal async Task TriggerDataBoundEvent()
        {
            DataBoundEventArgs<TValue> args = new DataBoundEventArgs<TValue>()
            {
                Name = "DataBound"
            };
            if (Parent.TreeViewEventAggregator != null)
            {
                await Parent.TreeViewEventAggregator.NotifyAsync("DataBound", args).ConfigureAwait(true);
            }
            if (Parent.TreeViewEvents?.DataBound.HasDelegate == true)
            {
                await Parent.TreeViewEvents.DataBound.InvokeAsync(args).ConfigureAwait(true);
            }
        }

        /// <summary>
        /// Method invoked after each time the component has been rendered.
        /// </summary>
        /// <param name="firstRender">"First render".</param>
        /// <returns>"Task".</returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (Parent.TreeViewFields?.DataManager != null)
                {
                    DataType = TreeViewDataType.RemoteData;
                    Parent.AllExpandedNodes = Parent != null && Parent.ExpandedNodes != null ? Parent.ExpandedNodes.ToHashSet() : new HashSet<string>() { };
                    listGenerationDataManager = Parent.TreeViewFields.DataManager;
                    UpdateFields(0);
                    Query = GettingQuery(listGenerationFieldsMapper);
                    await GetDataManagerData().ConfigureAwait(true);
                    if (IsDisposed)
                        return;
                    if (Parent != null && (Parent.TreeViewFields?.HasChildren != null || Parent.TreeViewFields?.ParentID != null) && Parent.TreeViewFields?.Query == null)
                    {
                        Parent.InternalData = ListData.ToList();
                        DataSource = Parent.InternalData;
                        ItemsData = Parent.InternalData;
                        ListData = GroupingData(null, null);
                    }
                    StateHasChanged();
                }

                ChildItems = null;
                Parent.IsNodeRendered = true;
                if (Parent != null && Parent.EnablePersistence)
                {
                    TreePersistenceValues localStorageValue = await Parent.InvokeMethod<TreePersistenceValues>("window.localStorage.getItem", true, new object[] { Parent.ID }).ConfigureAwait(true);
                    if (localStorageValue == null)
                    {
                        await SetLocalStorage(Parent.ID, SerializeModel()).ConfigureAwait(true);
                    }
                    else
                    {
                        Parent.AllSelectedNodes = localStorageValue.SelectedNodes?.ToHashSet();
                        Parent.tempSelectedNodes = Parent.AllSelectedNodes?.ToHashSet();
                        Parent.AllCheckedNodes = localStorageValue.CheckedNodes;
                        Parent.AllExpandedNodes = localStorageValue.ExpandedNodes?.ToHashSet();
                        Parent.CurrentExpandedNodes = Parent.AllExpandedNodes?.ToList();
                        if (!SfBaseUtils.Equals(Parent.CurrentExpandedNodes, Parent.ExpandedNodes?.ToList()))
                        {
                            Parent.InternalExpandedNodes = Parent.CurrentExpandedNodes;
                            await Parent.UpdateExpandedNodes().ConfigureAwait(true);
                        }
                    }

                    await InvokeAsync(StateHasChanged).ConfigureAwait(true);
                }

                await TriggerDataBoundEvent().ConfigureAwait(true);
            }
            if (isFiltering)
            {
                Parent.InternalData = ListData?.ToList();
                DataSource = Parent.InternalData;
                ItemsData = Parent.InternalData;
                ListData = GroupingData(null, null);
            }
            if (DataType == TreeViewDataType.RemoteData && Parent.IsCompletelyRendered && !SfBaseUtils.Equals(RemoteExpandedValues, Parent.ExpandedNodes?.ToList()))
            {
                Parent.InternalExpandedNodes = RemoteExpandedValues;
                await Parent.UpdateExpandedNodes().ConfigureAwait(true);
            }
        }

        internal async Task GetDataManagerData()
        {
            if (!isFiltering)
            {
                try
                {
                    object itemsData = (listGenerationDataManager != null) ? await listGenerationDataManager.ExecuteQuery<TValue>(Query).ConfigureAwait(true) : null;
                    if (IsDisposed || itemsData == null)
                        return;
                    if (listGenerationDataManager != null)
                    {
                        List<TValue> dataSource = GetDataSource(itemsData);
                        if (dataSource?.Count > 0)
                        {
                            dataSource = GetSortedData(dataSource.ToList(), Parent.SortOrder.ToString(), Parent.TreeViewFields.Text);
                            ListData = dataSource;
                            Parent.InternalData = dataSource;
                            DataSource = Parent.InternalData;
                            ItemsData = Parent.InternalData;
                            if (listGenerationDataManager.Adaptor.ToString() == "CustomAdaptor")
                            {
                                ListData = GroupingData(null, null);
                            }
                            if (!Parent.LoadOnDemand)
                            {
                                await GetChildRemoteData(dataSource, 1).ConfigureAwait(true);
                            }
                            if (IsDisposed) { return; }
                            await EnsureExpandNodes(dataSource).ConfigureAwait(true);
                        }
                    }
                }
                catch (Exception exception)
                {
                    await ThrowException(exception).ConfigureAwait(true);
                    throw new InvalidOperationException("Data operation failed", exception);
                }
            }
        }

        private async Task ThrowException(Exception e)
        {
            FailureEventArgs args = new FailureEventArgs() { Error = e, Name = "OnActionFailure" };
            if (Parent.TreeViewEventAggregator != null)
            {
                await Parent.TreeViewEventAggregator.NotifyAsync("OnActionFailure", args).ConfigureAwait(true);
            }
            if (Parent.TreeViewEvents?.OnActionFailure.HasDelegate == true)
                await Parent.TreeViewEvents.OnActionFailure.InvokeAsync(args).ConfigureAwait(true);
        }

        // Get child nodes data values for Remote data binding.
        private async Task<IEnumerable<TValue>> GetRemoteDataChild()
        {
            try
            {
                object itemsData = (listGenerationDataManager != null) ? await listGenerationDataManager.ExecuteQuery<TValue>(Query).ConfigureAwait(true) : null;
                List<TValue> dataSource = (listGenerationDataManager != null && itemsData != null) ? GetDataSource(itemsData) : new List<TValue>();

                return (itemsData != null) ? dataSource : null!;
            }
            catch (Exception exception)
            {
                await ThrowException(exception).ConfigureAwait(true);
                return null!;
                throw;
            }
        }

        private List<TValue> GetDataSource(object itemsData)
        {
            IEnumerable nodeData = Query != null && Query.IsCountRequired ? ((DataResult)itemsData).Result ?? new List<object>() : itemsData as IEnumerable;
            return nodeData.Cast<TValue>().ToList();
        }

        // Update child nodes values for Remote data binding.
        private async Task GetChildRemoteData(List<TValue> dataSource, int level)
        {
            foreach (TValue item in dataSource)
            {
                if (IsDisposed) { return; }
                listGenerationDataManager = null;
                UpdateFields(level - 1);
                object id = (listGenerationFieldsMapper.Id != null && item != null) ? Parent.GetValue(listGenerationFieldsMapper.Id, item) : null;
                UpdateFields(level);
                Query = GettingQuery(listGenerationFieldsMapper, id);
                IEnumerable<TValue> childData = await GetRemoteDataChild().ConfigureAwait(true);
                if (childData != null)
                {
                    List<TValue> child = childData.Cast<TValue>().ToList();
                    RemoteDataField(id.ToString(), listGenerationFieldsMapper, child);
                    await RenderingRemoteChild(child, level).ConfigureAwait(true);
                }

                UpdateFields(level);
            }
        }

        // Rendering remote data source child nodes.
        private async Task RenderingRemoteChild(List<TValue> dataSource, int level)
        {
            foreach (TValue item in dataSource)
            {
                if (IsDisposed) { return; }
                listGenerationDataManager = null;
                UpdateFields(level);
                object id = (listGenerationFieldsMapper.Id != null && item != null) ? Parent.GetValue(listGenerationFieldsMapper.Id, item) : null;
                UpdateFields(level + 1);
                Query = GettingQuery(listGenerationFieldsMapper, id);
                IEnumerable<TValue> valueData = await GetRemoteDataChild().ConfigureAwait(true);
                if (valueData != null)
                {
                    List<TValue> child = valueData.Cast<TValue>().ToList();
                    RemoteDataField(id.ToString(), listGenerationFieldsMapper, child);
                    await GetChildRemoteData(child, level + 1).ConfigureAwait(true);
                }
            }
        }

        private void RemoteDataField(string id, TreeFieldsMapping fields, List<TValue> childData)
        {
            if (listGenerationRemoteData != null && !isFiltering)
            {
                int index = listGenerationRemoteData.FindIndex(item => item.NodeId == id);
                if (index == -1)
                {
                    listGenerationRemoteData.Add(new RemoteFieldsData()
                    {
                        NodeId = id,
                        FieldSettings = fields,
                        RemoteData = childData
                    });
                }
                else
                {
                    listGenerationRemoteData[index] = new RemoteFieldsData()
                    {
                        NodeId = id,
                        FieldSettings = fields,
                        RemoteData = childData
                    };
                }
            }

        }

        /// <summary>
        /// Getting Query values for Remote data source.
        /// </summary>
        /// <param name="mapper">"Specifies the mapper field".</param>
        /// <param name="value">"Specifies the value".</param>
        /// <returns>"Task".</returns>
        protected virtual Query GettingQuery(TreeFieldsMapping mapper, object value = null)
        {
            Query query = new Query();
            if (mapper?.Query == null)
            {
                List<string> properties = new List<string>()
                {
                    "TableName", "Child", "Text", "Id", "ParentID", "NavigateUrl", "Expanded", "HasChildren", "HtmlAttributes", "ImageUrl", "IconCss", "Selected", "Tooltip"
                };
                List<string> columns = properties.Where(p => p != LISTGENERATION_DATASOURCE && p != "TableName" && p != "Child" && p != null)
                                .Select(p => (string)DataUtil.GetObject(p, listGenerationFieldsMapper))
                                .Where(p => p != null)
                                .Distinct()
                                .ToList();
                query.Select(columns);
                if (listGenerationFieldsMapper != null && listGenerationFieldsMapper.TableName != null)
                {
                    query.From(mapper.TableName);
                }
            }
            else
            {
                query = GetQuery(mapper.Query);
            }

            if (value != null && mapper.ParentID != null)
            {
                value = GetIdType() ? int.Parse(value.ToString(), CultureInfo.InvariantCulture) : value;
                query.Where(new WhereFilter() { Field = mapper.ParentID, Operator = "equal", value = value });
            }

            return query;
        }

        private bool GetIdType()
        {
            object data = ListData != null ? ListData.ElementAtOrDefault(0) : default;
            if (data != null)
            {
                object propertyValue = (listGenerationFieldsMapper.Id != null) ? Parent.GetValue(listGenerationFieldsMapper.Id, data) : null;
                return (propertyValue.GetType() == typeof(int));
            }

            return false;
        }

        internal static Query CloneValue(Query value)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(Query));
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, value);
                ms.Seek(0, SeekOrigin.Begin);
                return (Query)serializer.ReadObject(ms);
            }
        }

        /// <summary>
        /// Update Cloned query value for Remote data binding.
        /// </summary>
        /// <param name="query">"Specifies the QUERY parameter".</param>
        /// <returns>"Task".</returns>
        protected virtual Query GetQuery(Query query)
        {
            return (query != null) ? CloneValue(query) : ((Query != null) ? CloneValue(Query) : new Query());
        }

        // Update the child level Fields values for Remote data binding
        private TreeFieldsMapping FieldSettings(object fields)
        {
            TreeFieldsMapping localMapper = new TreeFieldsMapping();
            if (fields != null)
            {
                localMapper.Children = (object)DataUtil.GetObject("Children", fields);
                localMapper.Id = (string)Parent.GetValue("Id", fields);
                localMapper.HasChildren = (string)Parent.GetValue("HasChildren", fields);
                localMapper.Text = (string)Parent.GetValue("Text", fields);
                localMapper.HtmlAttributes = (string)Parent.GetValue("HtmlAttributes", fields);
                localMapper.Expanded = (string)Parent.GetValue("Expanded", fields);
                localMapper.ImageUrl = (string)Parent.GetValue("ImageUrl", fields);
                localMapper.IconCss = (string)Parent.GetValue("IconCss", fields);
                localMapper.Selected = (string)Parent.GetValue("Selected", fields);
                localMapper.Tooltip = (string)Parent.GetValue("Tooltip", fields);
                localMapper.ParentID = (string)Parent.GetValue("ParentID", fields);
                localMapper.Url = (string)Parent.GetValue("NavigateUrl", fields);
                localMapper.DataManager = (DataManager)Parent.GetValue("DataManager", fields);
                localMapper.Query = (Query)Parent.GetValue("Query", fields);
                localMapper.IsChecked = (string)Parent.GetValue("IsChecked", fields);
                localMapper.TableName = (string)Parent.GetValue("TableName", fields);
            }

            return localMapper;
        }

        // Update Fields values based on provided node level.
        internal void UpdateFields(int nodeLevel)
        {
            if (IsDisposed) { return; }
            TreeFieldsMapping localMapper = FieldSettings(Parent.TreeViewFields);
            for (int i = 0; i < nodeLevel; i++)
            {
                if (localMapper.Children != null)
                {
                    localMapper = FieldSettings(localMapper.Children) as TreeFieldsMapping;
                }
            }

            listGenerationDataManager = localMapper.DataManager;
            Query = localMapper.Query;
            listGenerationFieldsMapper = localMapper;
        }

        // Customize the li element values.
        internal void BeforeNodeCreate(TreeItemCreatedArgs<TValue> args)
        {
            try
            {
                if (DataType == TreeViewDataType.Hierarchical)
                {
                    RenderingHierarchicalData(args);
                }
                else if (DataType == TreeViewDataType.SelfReferential)
                {
                    RenderSelfReferentialData(args);
                }
                else if (DataType == TreeViewDataType.RemoteData)
                {
                    if (!Parent.TreeViewFields.DataManager?.Offline ?? false)
                    {
                        RenderRemoteData(args);
                    }
                    else
                    {
                        TreeViewFieldsSettings<TValue> fields = Parent.TreeViewFields;
                        if (fields.Child != null)
                        {
                            RenderingHierarchicalData(args);
                        }
                        else
                        {
                            RenderSelfReferentialData(args);
                        }
                    }
                }
            }
            catch
            {
                if (!IsDisposed)
                    throw;
            }
        }

        // Based on latest property values update the list elements.
        internal void ListUpdated()
        {
            InvokeAsync(() => StateHasChanged());
        }

        internal async Task RenderRemoteLi(string? parentID, int level, bool getChildValue = false)
        {
            UpdateFields(level);
            Query = GettingQuery(listGenerationFieldsMapper, parentID);
            object itemsData = (listGenerationDataManager != null) ? await listGenerationDataManager.ExecuteQuery<TValue>(Query).ConfigureAwait(true) : null;
            if (listGenerationDataManager != null && itemsData != null)
            {
                List<TValue> dataSource = GetDataSource(itemsData);
                if (dataSource != null && dataSource.Count > 0)
                {
                    dataSource = GetSortedData(dataSource.ToList(), Parent.SortOrder.ToString(), listGenerationFieldsMapper.Text);
                    IEnumerable<TValue> childData = dataSource;
                    if (childData != null)
                    {
                        List<TValue> child = (childData as IEnumerable).Cast<TValue>().ToList();
                        if (!getChildValue)
                        {
                            RemoteDataField(parentID.ToString(), listGenerationFieldsMapper, child);
                        }
                        else
                        {
                            AddChildListData(parentID.ToString(), child);
                        }
                    }

                    await EnsureExpandNodes(dataSource).ConfigureAwait(true);
                }
            }
        }

        private async Task EnsureExpandNodes(List<TValue> dataSource)
        {
            foreach (TValue item in dataSource)
            {
                object propertyValue = (listGenerationFieldsMapper.Id != null && item != null) ? Parent.GetValue(listGenerationFieldsMapper.Id, item) : null;
                if (Parent.AllExpandedNodes?.Count > 0 && Parent.AllExpandedNodes.Contains(propertyValue?.ToString()))
                {
                    await UpdateExpandState(propertyValue).ConfigureAwait(true);
                }
                else
                {
                    object expandVal = (listGenerationFieldsMapper.Expanded != null && item != null) ? Parent.GetValue(listGenerationFieldsMapper.Expanded, item) : null;
                    if (expandVal?.ToString() == ISTRUE)
                    {
                        await UpdateExpandState(propertyValue).ConfigureAwait(true);
                    }
                }
            }
        }

        private async Task UpdateExpandState(object propertyValue)
        {
            if (Parent.LoadOnDemand)
            {
                await RenderRemoteLi(propertyValue.ToString(), 1).ConfigureAwait(true);
            }

            if (Parent != null && Parent.ExpandedNodes != null ? !Parent.ExpandedNodes.Contains(propertyValue.ToString()) : true)
            {
                if (!Parent.InternalExpandedNodes.Contains(propertyValue.ToString()))
                {
                    Parent.InternalExpandedNodes.Add(propertyValue.ToString());
                }

                if (!RemoteExpandedValues.Contains(propertyValue.ToString()))
                {
                    RemoteExpandedValues.Add(propertyValue.ToString());
                }

                if (!Parent.AllExpandedNodes.Contains(propertyValue.ToString()))
                {
                    Parent.AllExpandedNodes.Add(propertyValue.ToString());
                }
            }

            if ((Parent != null && (!Parent.IsCompletelyRendered ? Parent.ExpandedNodes == null : true)) || listGenerationDataManager.Adaptor.ToString() == "CustomAdaptor")
            {
                await Parent.UpdateExpandedNodes().ConfigureAwait(true);
            }
        }

        // Customize the Remote data binding li element data.
        private void RenderRemoteData(TreeItemCreatedArgs<TValue> args)
        {
            UpdateFields(args.NodeLevel - 1);
            if (Parent.LoadOnDemand)
            {
                if (Parent.AllExpandedNodes != null && Parent.AllExpandedNodes.Count > 0)
                {
                    RenderRemoteData_ExpandNodes(args);
                }
                else
                {
                    RenderRemoteData_List(args);
                }
            }
            else
            {
                RenderRemoteData_LoadOnDemand_False(args);
            }

            string isSelected = UpdateSelection(args);
            args.Options.Fields = listGenerationFieldsMapper;
            object idValue = (listGenerationFieldsMapper.Id != null && args.ItemData != null) ? Parent.GetValue(listGenerationFieldsMapper.Id, args.ItemData) : null;
            TreeViewFieldsSettings<TValue> fields = Parent.TreeViewFields;
            string nodeId = Parent.GetValue(fields.Id.ToString(), args.ItemData).ToString();
            bool isLoaded = Parent.IsLoaded || nodeId != Parent.LoadedId;
            args.TreeOptions = new TreeOptions<TValue>()
            {
                ChildData = listGenerationChild != null ? GetSortedData((List<TValue>)listGenerationChild, Parent.SortOrder.ToString(), Parent.TreeViewFields.Text) : null,
                IsExpanded = listGenerationIsExpanded,
                IconClass = iconClass,
                TreeViewFields = listGenerationFieldsMapper,
                IsSelected = isSelected == TRUE,
                IsChecked = UpdateChecked(idValue.ToString()),
                IsEdit = (!string.IsNullOrEmpty(Parent.EditedNodeId) && Parent.EditedNodeId == idValue.ToString()),
                IsLoaded = isLoaded
            };
            listGenerationChild = null;
            listGenerationIsExpanded = false;
            iconClass = null;
        }

        private void RenderRemoteData_List(TreeItemCreatedArgs<TValue> args)
        {
            object expandVal = (listGenerationFieldsMapper.Expanded != null && args.ItemData != null) ? Parent.GetValue(listGenerationFieldsMapper.Expanded, args.ItemData) : null;
            if (expandVal != null)
            {
                Type expandType = expandVal.GetType();
                string parentID = (listGenerationFieldsMapper.Id != null && args.ItemData != null) ? Parent.GetValue(listGenerationFieldsMapper.Id, args.ItemData).ToString() : null;
                IEnumerable<TValue> childData = (IEnumerable<TValue>)GetChildRemoteData(parentID);
                if (((expandType == typeof(bool) && (bool)expandVal) || (expandType == typeof(string) && (string)expandVal == TRUE)) || childData != null)
                {
                    listGenerationChild = childData;
                    if (Parent != null && Parent.ExpandedNodes != null ? Array.IndexOf(Parent.ExpandedNodes, parentID.ToString()) >= 0 : false)
                    {
                        listGenerationIsExpanded = true;
                    }
                }
                else
                {
                    iconClass = (expandType == typeof(bool) && !(bool)expandVal) || (expandType == typeof(string) && (string)expandVal == FALSE) ? ICONEXPANDCLASS : null;
                }
            }
        }

        private void RenderRemoteData_ExpandNodes(TreeItemCreatedArgs<TValue> args)
        {
            object propertyValue = (listGenerationFieldsMapper.Id != null && args.ItemData != null) ? Parent.GetValue(listGenerationFieldsMapper.Id, args.ItemData) : null;
            if (Parent.AllExpandedNodes.Contains(propertyValue?.ToString()))
            {
                string parentID = (listGenerationFieldsMapper.Id != null && args.ItemData != null) ? Parent.GetValue(Parent.TreeViewFields.Id, args.ItemData).ToString() : null;
                listGenerationChild = (IEnumerable<TValue>)GetChildRemoteData(parentID);
                if (Parent != null && Parent.ExpandedNodes != null ? Array.IndexOf(Parent.ExpandedNodes, propertyValue.ToString()) >= 0 : false)
                {
                    listGenerationIsExpanded = true;
                }
            }
        }

        private void RenderRemoteData_LoadOnDemand_False(TreeItemCreatedArgs<TValue> args)
        {
            string parentID = (listGenerationFieldsMapper.Id != null && args.ItemData != null) ? Parent.GetValue(listGenerationFieldsMapper.Id, args.ItemData).ToString() : null;
            listGenerationChild = GetChildRemoteData(parentID);
            if (Parent != null && Parent.ExpandedNodes != null ? Parent.ExpandedNodes.Contains(parentID) : Parent.AllExpandedNodes.Contains(parentID))
            {
                listGenerationIsExpanded = true;
            }
            else if (listGenerationChild != null)
            {
                listGenerationIsExpanded = false;
                iconClass = ICONEXPANDCLASS;
            }
        }

        // Get the child data values for Remote data source.
        internal List<TValue> GetChildRemoteData(string id)
        {
            List<TValue> groupData = listGenerationRemoteData.FirstOrDefault(data => data.NodeId == id)?.RemoteData;
            return groupData?.Count != 0 ? groupData : null;
        }

        // Customize the Hierarchical data binding li element data.
        private void RenderingHierarchicalData(TreeItemCreatedArgs<TValue> args)
        {
            TreeViewFieldsSettings<TValue>? fields = Parent?.TreeViewFields;
            object? propertyValue = (fields?.Id != null && args.ItemData != null) ? Parent?.GetValue(fields.Id, args.ItemData) : null!;
            object childData;
            if (Parent != null && Parent.LoadOnDemand)
            {
                if (Parent.AllExpandedNodes != null && Parent.AllExpandedNodes.Count > 0)
                {
                    if (Parent.AllExpandedNodes.Contains(propertyValue?.ToString()))
                    {
                        childData = (fields.Child != null && args.ItemData != null) ? DataUtil.GetObject(fields.Child.ToString(), args.ItemData) : null;
                        listGenerationChild = ((IEnumerable<TValue>)childData);
                        if (listGenerationChild == null || !listGenerationChild.Any())
                        {
                            listGenerationChild = null;
                        }
                        else if ((Parent.ExpandedNodes != null && Array.IndexOf(Parent.ExpandedNodes, propertyValue.ToString()) >= 0) && listGenerationChild.Any())
                        {
                            listGenerationIsExpanded = true;
                        }
                    }
                }
            }
            else
            {
                childData = (fields.Child != null && args.ItemData != null) ? DataUtil.GetObject(fields.Child.ToString(), args.ItemData) : null!;
                listGenerationChild = ((IEnumerable<TValue>)childData)?.ToList();
                string parentID = (fields.Id != null && args.ItemData != null) ? Parent.GetValue(fields.Id, args.ItemData).ToString() : null;
                if (Parent != null && Parent.ExpandedNodes != null ? Array.IndexOf(Parent.ExpandedNodes, parentID) >= 0 : Parent.AllExpandedNodes.Contains(parentID))
                {
                    listGenerationIsExpanded = true;
                }
                else if (listGenerationChild != null && listGenerationChild.Any())
                {
                    listGenerationIsExpanded = false;
                    iconClass = ICONEXPANDCLASS;
                }
            }

            bool isLoaded = Parent.IsLoaded || propertyValue?.ToString() != Parent.LoadedId;
            args.TreeOptions = GetOptions(fields, UpdateSelfSelection(propertyValue.ToString()), UpdateChecked(propertyValue.ToString()), (!string.IsNullOrEmpty(Parent.EditedNodeId) && Parent.EditedNodeId == propertyValue.ToString()), Parent.AllDisabledNodes.Contains(propertyValue?.ToString()), isLoaded);
            listGenerationChild = null;
            listGenerationIsExpanded = false;
            iconClass = null;
        }

        private TreeOptions<TValue> GetOptions(TreeViewFieldsSettings<TValue> fields, bool isSelected, string isChecked, bool isEdit, bool isDisable, bool isLoaded)
        {
            return new TreeOptions<TValue>()
            {
                ChildData = listGenerationChild != null ? GetSortedData((List<TValue>)listGenerationChild, Parent.SortOrder.ToString(), fields.Text, Parent.SortComparer) : null,
                IsExpanded = listGenerationIsExpanded,
                IsSelected = isSelected,
                IconClass = iconClass,
                IsChecked = isChecked,
                IsEdit = isEdit,
                IsDisabled = isDisable,
                IsLoaded = isLoaded
            };
        }

        // Update the selected node value based multi select option in Remote data source.
        private string UpdateSelection(TreeItemCreatedArgs<TValue> args)
        {
            TreeViewFieldsSettings<TValue> fields = Parent.TreeViewFields;
            string isSelected = null;
            if (Parent.SelectedNodes != null && Parent.SelectedNodes.Length > 0)
            {
                string selectedId = (fields.Children != null && args.NodeLevel != 1) ? DataUtil.GetKeyValue("Id", fields.Children) : fields.Id;
                object propertyValue = (selectedId != null && args.ItemData != null) ? Parent.GetValue(selectedId, args.ItemData) : null;
                int pos = Array.IndexOf(Parent.SelectedNodes, propertyValue?.ToString());
                if (pos > -1)
                {
                    isSelected = TRUE;
                    if (multiSelectFlag)
                    {
                        multiSelectFlag = false;
                    }
                }
            }
            else
            {
                object selectedVal = (fields.Selected != null) ? Parent.GetValue(fields.Selected, args.ItemData) : null;
                if (selectedVal != null)
                {
                    Type selectType = selectedVal.GetType();
                    if ((multiSelectFlag && selectType == typeof(bool) && (bool)selectedVal) || (selectType == typeof(string) && (string)selectedVal == TRUE))
                    {
                        isSelected = TRUE;
                        if (!Parent.AllowMultiSelection)
                        {
                            multiSelectFlag = false;
                        }
                    }
                    else if ((Parent.AllowMultiSelection && selectType == typeof(bool) && !(bool)selectedVal) || (selectType == typeof(string) && (string)selectedVal == FALSE))
                    {
                        isSelected = FALSE;
                    }
                }
            }

            return isSelected;
        }

        // Update the selected node value based multi select option both Self Referential & Hierarchical data source.
        private bool UpdateSelfSelection(string id)
        {
            return (Parent.tempSelectedNodes != null) && Parent.tempSelectedNodes.Contains(id);
        }

        // Update the checked node value based multi select option both Self Referential & Hierarchical data source.
        private string UpdateChecked(object id)
        {
            return Parent.AllCheckedNodes != null && Parent.AllCheckedNodes.TryGetValue(id.ToString(), out object value) ? value.ToString() : FALSE;
        }

        // Customize the Self Referential data binding li element data.
        private void RenderSelfReferentialData(TreeItemCreatedArgs<TValue> args)
        {
            TreeViewFieldsSettings<TValue> fields = Parent.TreeViewFields;
            object propertyValue = (fields.Id != null && args.ItemData != null) ? Parent.GetValue(fields.Id, args.ItemData) : null;
            string parentID = propertyValue?.ToString();
            if ((bool?)Parent.GetValue(fields.HasChildren.ToString(), args.ItemData) == true)
            {
                listGenerationChild = SelfChilds.GetValueOrDefault(parentID);
            }
            if (Parent.LoadOnDemand)
            {
                if (Parent.AllExpandedNodes?.Count > 0 && Parent.AllExpandedNodes.Contains(parentID))
                {
                    listGenerationIsExpanded = Parent.ExpandedNodes?.Contains(parentID) == true;
                }
                else
                {
                    listGenerationChild = null;
                }
            }
            else
            {
                if (Parent?.ExpandedNodes != null ? Array.IndexOf(Parent.ExpandedNodes, parentID) >= 0 : Parent.AllExpandedNodes.Contains(parentID))
                {
                    listGenerationIsExpanded = true;
                }
                else if (listGenerationChild != null)
                {
                    iconClass = ICONEXPANDCLASS;
                }
            }
            bool isLoaded = (!Parent.IsLoaded && parentID == Parent.LoadedId) ? false : true;
            args.TreeOptions = GetOptions(fields, UpdateSelfSelection(parentID), UpdateChecked(parentID), (!string.IsNullOrEmpty(Parent.EditedNodeId) && Parent.EditedNodeId == parentID), Parent.AllDisabledNodes.Contains(parentID), isLoaded);
            listGenerationChild = null;
            listGenerationIsExpanded = false;
            iconClass = null;
        }

        // Grouping the data for provided Parent node id.
        internal List<TValue> GroupingData(string? parentID, List<TValue>? dataSource = null)
        {
            List<TValue> listData = dataSource ?? (ItemsData != null ? ItemsData.ToList() : new List<TValue>());
            List<TValue> groupData = listData.FindAll(item =>
            {
                string id = Parent.GetValue(Parent.TreeViewFields.ParentID, item)?.ToString();
                return parentID == id || (parentID != null && parentID.Equals(id, StringComparison.Ordinal));
            });

            return groupData.Count != 0 ? groupData : null;
        }

        /// <summary>
        /// Update the Persistence value to local storage.
        /// </summary>
        internal async Task SetLocalStorage(string persistId, string dataValue)
        {
            await Parent.InvokeMethod(SETITEM, new object[] { persistId, dataValue }).ConfigureAwait(true);
        }

        /// <summary>
        /// Updating the persisting values to our component properties.
        /// </summary>
        internal string SerializeModel()
        {
            return JsonSerializer.Serialize(new TreePersistenceValues { CheckedNodes = Parent.AllCheckedNodes, ExpandedNodes = Parent.AllExpandedNodes?.ToList(), SelectedNodes = Parent.AllSelectedNodes?.ToList() });
        }

        internal void AddChildData(string? target, TValue node, List<TValue>? datasource, bool isChild)
        {
            List<TValue> dataList = datasource?.ToList();
            foreach (TValue dataItem in dataList)
            {
                string idAttrValue = GetAttrValue(Parent.TreeViewFields?.Id, dataItem);
                IEnumerable<TValue> childs = (IEnumerable<TValue>)DataUtil.GetObject(Parent.TreeViewFields.Child.ToString(), dataItem);
                if (idAttrValue == target)
                {
                    List<TValue> newNodeData = new List<TValue>();
                    if (childs != null)
                    {
                        newNodeData = childs.ToList();
                    }

                    if (isChild)
                    {
                        newNodeData.Add(node);
                        PropertyInfo prop = dataItem.GetType().GetProperty(Parent.TreeViewFields?.Child?.ToString());
                        FieldInfo field = dataItem.GetType().GetField(Parent.TreeViewFields?.Child?.ToString());
                        if (prop != null)
                        {
                            prop.SetValue(dataItem, newNodeData);
                        }
                        else if (field != null)
                        {
                            field.SetValue(dataItem, newNodeData);
                        }
                    }

                    break;
                }

                if (childs != null)
                {
                    AddChildData(target, node, childs.ToList(), true);
                }
            }

            Parent.InternalData?.ToList().Concat(dataList);
            ItemsData = (IEnumerable<TValue>)Parent.InternalData;
        }

        // updates the child data for remote data if any data is dynamically added
        private void AddChildListData(string id, List<TValue> nodes)
        {
            int index = -1;
            foreach (RemoteFieldsData SubData in listGenerationRemoteData)
            {
                if (SubData.NodeId == id)
                {
                    index = listGenerationRemoteData.IndexOf(SubData);
                }
            }
            if (index >= 0 && !SfBaseUtils.Equals(listGenerationRemoteData[index].RemoteData, nodes))
            {
                listGenerationRemoteData[index].RemoteData.AddRange(nodes);
            }
        }

        /// <summary>
        /// Adding TreeView nodes.
        /// </summary>
        internal async void AddNodeData(List<TValue> nodes, string target = null)
        {
            List<TValue> nodeList = Parent.InternalData;
            if (nodes != null)
            {
                int nodeCount = nodes.Count;
                if (DataType == TreeViewDataType.RemoteData && target != null)
                {
                    string idValue;
                    int index = -1;
                    bool isValueUpdated = false;
                    List<TValue> rootData = ListData.ToList();
                    foreach (TValue data in rootData)
                    {
                        idValue = Parent.GetValue(Parent.TreeViewFields.Id?.ToString(), data).ToString();
                        if (idValue == target)
                        {
                            foreach (RemoteFieldsData SubData in listGenerationRemoteData)
                            {
                                if (SubData.NodeId == target)
                                {
                                    isValueUpdated = true;
                                    index = listGenerationRemoteData.IndexOf(SubData);
                                }
                            }
                            if (!isValueUpdated)
                            {
                                string HasChildren = Parent.TreeViewFields.HasChildren.ToString();
                                data.GetType().GetProperty(HasChildren)?.SetValue(data, true);
                                await Parent.TreeViewFields.DataManager.Insert<TValue>(nodes[0], Query?.FromTable, Query).ConfigureAwait(true);
                                return;
                            }
                        }
                    }
                    if (index >= 0 && isValueUpdated)
                    {
                        listGenerationRemoteData[index].RemoteData.AddRange(nodes);
                        if (Parent.TreeViewFields != null && Parent.TreeViewFields.DataManager != null && !Parent.TreeViewFields.DataManager.Offline)
                        {
                            await Parent.TreeViewFields.DataManager.Insert<TValue>(nodes[0], Query?.FromTable, Query).ConfigureAwait(true);
                        }
                    }
                    if (!isValueUpdated)
                    {
                        foreach (RemoteFieldsData data in listGenerationRemoteData)
                        {
                            idValue = data.NodeId;
                            if (idValue == target && !isValueUpdated)
                            {
                                isValueUpdated = true;
                                listGenerationRemoteData[listGenerationRemoteData.IndexOf(data)].RemoteData.AddRange(nodes);
                            }
                            else
                            {
                                foreach (TValue NodeData in data.RemoteData)
                                {
                                    idValue = Parent.GetValue(Parent.TreeViewFields.Id?.ToString(), NodeData).ToString();
                                    if (idValue == target)
                                    {
                                        isValueUpdated = true;
                                        TreeFieldsMapping settings = data.FieldSettings;
                                    }
                                }
                            }
                        }
                        if (isValueUpdated)
                        {
                            isValueUpdated = false;
                            index = -1;
                            foreach (RemoteFieldsData SubData in listGenerationRemoteData)
                            {
                                if (SubData.NodeId == target)
                                {
                                    isValueUpdated = true;
                                    index = listGenerationRemoteData.IndexOf(SubData);
                                }
                            }
                            if (index >= 0)
                            {
                                listGenerationRemoteData[index].RemoteData.AddRange(nodes);
                                if (Parent.TreeViewFields != null && Parent.TreeViewFields.DataManager != null && !Parent.TreeViewFields.DataManager.Offline)
                                {
                                    await Parent.TreeViewFields.DataManager.Insert<TValue>(nodes[0], Query?.FromTable, Query).ConfigureAwait(true);
                                }
                            }
                            if (!isValueUpdated)
                            {
                                foreach (RemoteFieldsData SubData in listGenerationRemoteData)
                                {
                                    foreach (TValue NodeData in SubData.RemoteData)
                                    {
                                        idValue = Parent.GetValue(Parent.TreeViewFields.Id?.ToString(), NodeData).ToString();
                                        if (idValue == target)
                                        {
                                            string HasChildren = Parent.TreeViewFields.HasChildren.ToString();
                                            NodeData.GetType().GetProperty(HasChildren)?.SetValue(NodeData, true);
                                        }
                                    }
                                }
                                if (Parent.TreeViewFields != null && Parent.TreeViewFields.DataManager != null && !Parent.TreeViewFields.DataManager.Offline)
                                {
                                    await Parent.TreeViewFields.DataManager.Insert<TValue>(nodes[0], Query?.FromTable, Query).ConfigureAwait(true);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (DataType == TreeViewDataType.Hierarchical && target != null)
                    {
                        AddChildData(target, nodes[0], Parent.InternalData, true);
                    }
                    else
                    {
                        for (int i = 0; i < nodeCount; i++)
                        {
                            TValue item = nodes.ElementAt(i);
                            nodeList.Add(item);
                        }
                        if ((DataType == TreeViewDataType.RemoteData && Parent.TreeViewFields != null && Parent.TreeViewFields.DataManager != null && !Parent.TreeViewFields.DataManager.Offline))
                        {
                            await Parent.TreeViewFields.DataManager.Insert<TValue>(nodes[0], Query?.FromTable, Query).ConfigureAwait(true);
                        }
                        await Parent.UpdateData(nodeList).ConfigureAwait(true);
                    }
                }
            }
        }

        /// <summary>
        /// Update Has Child after remove operation in Remote Data
        /// </summary>
        private async Task CheckForRemoteHasChild(List<object> parentIds)
        {
            if (parentIds.Count > 0)
            {
                List<TValue> rootData = ListData.ToList();
                bool updateRootChild;
                foreach (TValue data in rootData)
                {
                    updateRootChild = true;
                    if (parentIds.Contains(Parent.GetValue(Parent.TreeViewFields.Id?.ToString(), data)))
                    {
                        foreach (RemoteFieldsData SubData in listGenerationRemoteData)
                        {
                            if (SubData.NodeId == Parent.GetValue(Parent.TreeViewFields.Id?.ToString(), data).ToString())
                            {
                                updateRootChild = false;
                            }
                        }
                        if (updateRootChild && parentIds.Contains(Parent.GetValue(Parent.TreeViewFields.Id?.ToString(), data)))
                        {
                            string HasChildren = Parent.TreeViewFields.HasChildren.ToString();
                            data.GetType().GetProperty(HasChildren)?.SetValue(data, false);
                            if (Parent.TreeViewFields != null && Parent.TreeViewFields.DataManager != null && !Parent.TreeViewFields.DataManager.Offline)
                            {
                                await Parent.TreeViewFields.DataManager.Update<TValue>(Parent.TreeViewFields.Id, data, Query?.FromTable, Query).ConfigureAwait(true);
                            }
                        }
                    }
                }
                bool updateSubChild;
                foreach (RemoteFieldsData data in listGenerationRemoteData)
                {
                    updateSubChild = true;
                    foreach (TValue NodData in data.RemoteData)
                    {
                        if (parentIds.Contains(Parent.GetValue(Parent.TreeViewFields.Id?.ToString(), data)))
                        {
                            foreach (RemoteFieldsData checkData in listGenerationRemoteData)
                            {
                                if (checkData.NodeId == Parent.GetValue(Parent.TreeViewFields.Id?.ToString(), data).ToString())
                                {
                                    updateSubChild = false;
                                }
                            }
                        }
                        if (updateSubChild && parentIds.Contains(Parent.GetValue(Parent.TreeViewFields.Id?.ToString(), NodData)))
                        {
                            string HasChildren = Parent.TreeViewFields.HasChildren.ToString();
                            NodData.GetType().GetProperty(HasChildren)?.SetValue(NodData, false);
                            if (Parent.TreeViewFields != null && Parent.TreeViewFields.DataManager != null && !Parent.TreeViewFields.DataManager.Offline)
                            {
                                await Parent.TreeViewFields.DataManager.Update<TValue>(Parent.TreeViewFields.Id, NodData, Query?.FromTable, Query).ConfigureAwait(true);
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Removing TreeView nodes.
        /// </summary>
        internal async void RemoveNodes(string[] nodes)
        {
            List<TValue> nodeList = Parent.InternalData;
            if (nodes != null)
            {
                if (DataType == TreeViewDataType.RemoteData)
                {
                    List<object> parentCheckValues = new List<object>() { };
                    List<TValue> updatedData = nodes.Select(id => Parent.GetTreeData(id)[0]).ToList();
                    string idValue;
                    int[] rootIndex = Array.Empty<int>();
                    int[] subIndex = Array.Empty<int>();
                    Dictionary<int, int[]> nodeIndex = new Dictionary<int, int[]>() { };
                    List<TValue> rootData = ListData.ToList();
                    foreach (TValue data in rootData)
                    {
                        idValue = Parent.GetValue(Parent.TreeViewFields.Id?.ToString(), data).ToString();
                        object parentIdValue = Parent.TreeViewFields.ParentID != null ? Parent.GetValue(Parent.TreeViewFields.ParentID.ToString(), data) : null;
                        if (nodes.Contains(idValue))
                        {
                            if (parentIdValue != null)
                            {
                                parentCheckValues.Add(parentIdValue);
                            }
                            rootIndex = SfBaseUtils.AddArrayValue(rootIndex, rootData.IndexOf(data));
                        }
                    }
                    if (rootIndex.Length > 0)
                    {
                        foreach (int index in rootIndex)
                        {
                            rootData.RemoveAt(index);
                        }
                        ListData = Parent.InternalData = rootData;
                    }

                    foreach (RemoteFieldsData SubData in listGenerationRemoteData)
                    {
                        if (nodes.Contains(SubData.NodeId))
                        {
                            subIndex = SfBaseUtils.AddArrayValue(subIndex, listGenerationRemoteData.IndexOf(SubData));
                        }
                    }
                    if (subIndex.Length > 0)
                    {
                        foreach (int index in subIndex)
                        {
                            listGenerationRemoteData.RemoveAt(index);
                        }
                    }
                    foreach (RemoteFieldsData SubData in listGenerationRemoteData)
                    {
                        foreach (TValue NodeData in SubData.RemoteData)
                        {
                            idValue = Parent.GetValue(Parent.TreeViewFields.Id?.ToString(), NodeData).ToString();
                            object parentIdValue = Parent.TreeViewFields.ParentID != null ? Parent.GetValue(Parent.TreeViewFields.ParentID.ToString(), NodeData) : null;
                            if (nodes.Contains(idValue))
                            {
                                if (parentIdValue != null)
                                {
                                    parentCheckValues.Add(parentIdValue);
                                }
                                if (!nodeIndex.ContainsKey(listGenerationRemoteData.IndexOf(SubData)))
                                {
                                    nodeIndex.Add(listGenerationRemoteData.IndexOf(SubData), new int[] { SubData.RemoteData.IndexOf(NodeData) });
                                }
                                else
                                {
                                    nodeIndex[listGenerationRemoteData.IndexOf(SubData)] = SfBaseUtils.AddArrayValue(nodeIndex[listGenerationRemoteData.IndexOf(SubData)], SubData.RemoteData.IndexOf(NodeData));
                                }
                            }
                        }
                    }
                    if (nodeIndex.Count > 0)
                    {
                        foreach (int key in nodeIndex.Keys)
                        {
                            foreach (int value in nodeIndex[key])
                            {
                                listGenerationRemoteData[key].RemoteData.RemoveAt(value);
                            }
                        }
                    }
                    foreach (TValue data in updatedData)
                    {
                        if (Parent.TreeViewFields != null && Parent.TreeViewFields.DataManager != null && !Parent.TreeViewFields.DataManager.Offline)
                        {
                            await Parent.TreeViewFields.DataManager.Remove<TValue>(Parent.TreeViewFields.Id, Parent.GetValue(Parent.TreeViewFields.Id?.ToString(), data), Query?.FromTable, Query).ConfigureAwait(true);
                        }
                    }
                    await CheckForRemoteHasChild(parentCheckValues).ConfigureAwait(true);
                }
                else
                {
                    if (DataType == TreeViewDataType.Hierarchical)
                    {
                        foreach (string node in nodes)
                        {
                            GetHierarchicalData(node, DataSource.ToList());
                            GetRemovedHierData(node, nodeList, true, default, Parent.TreeViewRemovedData, true);
                        }
                    }
                    else
                    {
                        int dataCount = nodeList.Count;
                        for (int i = 0; i < nodes.Length; i++)
                        {
                            string nodeValue = nodes[i];
                            for (int j = dataCount - 1; j >= 0; j--)
                            {
                                TValue item = nodeList.ElementAt(j);
                                string nodeId = Parent.GetValue(Parent.TreeViewFields.Id.ToString(), item).ToString();
                                string parentId = Parent.TreeViewFields.ParentID != null && Parent.GetValue(Parent.TreeViewFields.ParentID.ToString(), item) != null ? Parent.GetValue(Parent.TreeViewFields.ParentID.ToString(), item).ToString() : null;
                                if (nodeValue == nodeId || nodeValue == parentId)
                                {
                                    nodeList.RemoveAt(j);
                                }
                            }
                        }
                    }
                }
            }

            await Parent.UpdateData(nodeList).ConfigureAwait(true);
        }

        /// <summary>
        /// Return TreeView node data.
        /// </summary>
        internal List<TValue> GetTreeViewData(string node = null)
        {
            List<TValue> treeData = new List<TValue>();
            if (node != null)
            {
                TValue data = default;
                if (DataType == TreeViewDataType.Hierarchical)
                {
                    GetHierarchicalData(node, DataSource.ToList());
                    data = Parent.TreeViewRemovedData;
                }
                else if (DataType == TreeViewDataType.SelfReferential)
                {
                    data = GetRemovedSelfData(node);
                }
                else if (DataType == TreeViewDataType.RemoteData)
                {
                    data = GetRemoteNodeData(node);
                }
                if (data == null) { return new List<TValue>() { }; }
                else
                {
                    UpdateData(new List<TValue>() { data });
                    treeData.Add(data);
                }
            }
            else
            {
                treeData = DataType == TreeViewDataType.RemoteData? GetOverAllRemoteData() : Parent.AllowDragAndDrop ? DataSource?.ToList() : Parent.TreeViewFields?.DataSource?.ToList();
                UpdateData(treeData);
            }

            return treeData;
        }

        private void UpdateData(List<TValue> data)
        {
            if (data != null)
            {
                foreach (TValue treeData in data)
                {
                    string idValue = Parent.GetValue(Parent.TreeViewFields.Id.ToString(), treeData).ToString();
                    if (Parent.TreeViewFields.Expanded != null)
                    {
                        bool isExpanded = Parent.ExpandedNodes != null && Parent.ExpandedNodes.Contains(idValue);
                        treeData.GetType().GetProperty(Parent.TreeViewFields.Expanded)?.SetValue(treeData, isExpanded);
                    }
                    if (Parent.TreeViewFields.IsChecked != null)
                    {
                        bool isChecked = Parent.CheckedNodes != null && Parent.CheckedNodes.Contains(idValue);
                        treeData.GetType().GetProperty(Parent.TreeViewFields.IsChecked)?.SetValue(treeData, isChecked);
                    }
                    if (Parent.TreeViewFields.Selected != null)
                    {
                        bool isSelected = Parent.SelectedNodes != null && Parent.SelectedNodes.Contains(idValue);
                        treeData.GetType().GetProperty(Parent.TreeViewFields.Selected)?.SetValue(treeData, isSelected);
                    }
                    if (Parent.TreeViewFields.Child != null)
                    {
                        List<TValue> children = (DataUtil.GetObject(Parent.TreeViewFields.Child.ToString(), treeData) as IEnumerable<TValue>)?.ToList();
                        if (children != null)
                        {
                            UpdateData(children);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get the node data for remote data
        /// </summary>
        internal TValue GetRemoteNodeData(string id)
        {
            bool isValueUpdated = false;
            foreach (TValue data in ListData)
            {
                if (Parent.GetValue(Parent.TreeViewFields.Id?.ToString(), data).ToString() == id)
                {
                    isValueUpdated = true;
                    isRemoteChild = false;
                    return data;
                }
            }
            if (!isValueUpdated)
            {
                foreach (RemoteFieldsData data in listGenerationRemoteData)
                {
                    foreach (TValue NodeData in data.RemoteData)
                    {
                        if (Parent.GetValue((Parent.TreeViewFields.Children == null ? Parent.TreeViewFields.Id : DataUtil.GetKeyValue("Id", Parent.TreeViewFields.Children))?.ToString(), NodeData)?.ToString() == id)
                        {
                            isValueUpdated = true;
                            isRemoteChild = Parent.TreeViewFields.Children != null;
                            return NodeData;
                        }
                    }
                }
            }
            return default; 
        }

        private List<TValue> GetOverAllRemoteData()
        {
            List<TValue> overAllData = new();
            overAllData.AddRange(ListData);
            var remoteData = listGenerationRemoteData.SelectMany(data => data.RemoteData);
            overAllData.AddRange(remoteData);
            return overAllData;
        }

        /// <summary>
        /// Get the node's data such as id, text, parentID, selected, isChecked, and expanded by passing the node element or it's ID.
        /// </summary>
        internal NodeData GetNodeDetails(string node, Dictionary<string, TValue>? dictData = null)
        {
            try
            {
                NodeData getNodeData = new NodeData();
                TValue item = default;
                if (node != null)
                {
                    if (DataType == TreeViewDataType.Hierarchical || DataType == TreeViewDataType.RemoteData)
                    {
                        GetHierarchicalData(node, DataSource?.ToList());
                        item = DataType == TreeViewDataType.Hierarchical ? Parent.TreeViewRemovedData : GetRemoteNodeData(node);
                        if (item != null)
                        {
                            object? childrenObj = Parent.TreeViewFields.Child != null ? DataUtil.GetObject(Parent.TreeViewFields.Child.ToString(), item) : null;
                            getNodeData.HasChildren = childrenObj is IList childList && childList.Count > 0;
                        }
                    }
                    else if (DataType == TreeViewDataType.SelfReferential)
                    {
                        item = dictData != null ? dictData.GetValueOrDefault(node) : GetRemovedSelfData(node);
                        if (item != null)
                        {
                            getNodeData.HasChildren = Parent.TreeViewFields.HasChildren != null && DataUtil.GetObject(Parent.TreeViewFields?.HasChildren?.ToString(), item) != null ? (bool)Parent.GetValue(Parent.TreeViewFields?.HasChildren?.ToString(), item) : false;
                        }
                    }
                }

                if (item != null)
                {
                    TreeViewFieldChild<TValue> childFieldsSettings = Parent.TreeViewFields.Children != null ? Parent.TreeViewFields.Children as TreeViewFieldChild<TValue> : null;
                    string nodeValue = Parent.GetValue((!isRemoteChild ? Parent.TreeViewFields.Id : childFieldsSettings.Id).ToString(), item).ToString();
                    if (node.ToString().Contains(nodeValue, StringComparison.Ordinal))
                    {
                        getNodeData.Id = Parent.GetValue((!isRemoteChild ? Parent.TreeViewFields.Id : childFieldsSettings.Id)?.ToString(), item)?.ToString();
                        getNodeData.Text = Parent.GetValue((!isRemoteChild ? Parent.TreeViewFields.Text : childFieldsSettings.Text).ToString(), item)?.ToString();
                        getNodeData.ParentID = (!isRemoteChild ? Parent.TreeViewFields.ParentID : childFieldsSettings.ParentID) != null ? Parent.GetValue((!isRemoteChild ? Parent.TreeViewFields.ParentID : childFieldsSettings.ParentID).ToString(), item)?.ToString() : string.Empty;
                        getNodeData.Selected = Parent.AllSelectedNodes.Contains(nodeValue);
                        getNodeData.IsChecked = Parent.AllCheckedNodes.ContainsKey(nodeValue) ? TRUE : FALSE;
                        getNodeData.Expanded = Parent.ExpandedNodes != null && Parent.ExpandedNodes.Contains(nodeValue);
                    }
                }

                return getNodeData;
            }
            catch
            {
                if (!IsDisposed)
                    throw;
                return null!;
            }
        }

        internal void GetHierarchicalData(string id, IEnumerable<TValue> dataSource)
        {
            if (dataSource == null) { return; }
            foreach (var data in dataSource)
            {
                string idAttrValue = GetAttrValue(Parent.TreeViewFields?.Id, data);
                IEnumerable<TValue> childs = Parent.TreeViewFields?.Child != null ? (IEnumerable<TValue>)DataUtil.GetObject(Parent.TreeViewFields.Child.ToString(), data) : null;
                if (idAttrValue == id)
                {
                    Parent.TreeViewRemovedData = data;
                    break;
                }

                if (childs != null)
                {
                    GetHierarchicalData(id, childs);
                }
            }
        }

        internal void GetAllNodeId(IEnumerable<TValue>? dataSource)
        {
            TreeViewFieldsSettings<TValue> fields = Parent.TreeViewFields;
            foreach (var data in dataSource)
            {
                string idAttrValue = GetAttrValue(fields.Id, data);
                if (!CheckNodeId.Contains(idAttrValue))
                {
                    CheckNodeId.Add(idAttrValue);
                }

                bool hasChild = fields.HasChildren != null && (bool?)Parent.GetValue(fields.HasChildren.ToString(), data) == true;

                if (DataType == TreeViewDataType.SelfReferential)
                {
                    List<TValue> childNodes = SelfChilds.GetValueOrDefault(idAttrValue);
                    if (childNodes?.Count > 0)
                    {
                        hasChild = true;
                    }
                }

                if (hasChild && !AllParentNodeId.Contains(idAttrValue))
                {
                    AllParentNodeId.Add(idAttrValue);
                }
                if (DataType == TreeViewDataType.Hierarchical)
                {
                    IEnumerable<TValue> childs = fields.Child != null ? (IEnumerable<TValue>)DataUtil.GetObject(fields.Child.ToString(), data) : null;

                    if (childs != null)
                    {
                        if (!AllParentNodeId.Contains(idAttrValue))
                        {
                            AllParentNodeId.Add(idAttrValue);
                        }

                        GetAllNodeId((List<TValue>)childs);
                    }
                }
            }
        }

        internal async Task UpdatePersistence()
        {
            if (Parent.EnablePersistence && Parent.IsRendered)
            {
                await SetLocalStorage(Parent.ID, SerializeModel()).ConfigureAwait(true);
            }
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                Parent = null;
                ListData = null;
                Query = null;
                iconClass = null;
                listGenerationChild = null;
                listGenerationDataManager = null;
                listGenerationRemoteData = null;
                listGenerationFieldsMapper = null;
                AllParentNodeId = null;
                CheckNodeId = null;
                ItemsData = null;
                ChildItems = null;
                DataSource = null;
                RemoteExpandedValues = null;
            }
        }

        internal class RemoteFieldsData
        {
            public string NodeId { get; set; }

            public TreeFieldsMapping FieldSettings { get; set; }

            public List<TValue> RemoteData { get; set; }
        }
    }
}
