using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Internal;
using Syncfusion.Blazor.Navigations.Internal;
using Syncfusion.Blazor.Spinner;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Syncfusion.Blazor.PivotView, PublicKey=00240000048000009400000006020000002400005253413100040000010001002382fcb1069523ce72d849497a557a445c151eaf4007aa79adef551a8204ca7f728e5378607d85695b16f129ec35bf4af15dcf6d3581deb8bb0debb239c33e7f1271a37c7f60f1044ae417730f5082abee5f9ec568a8a4cef04074394755706376e982dc6f9d15430faaad385ae8f00a77ef1c97517f1a1517004ce78028b9ce")]

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// The Blazor TreeView component displays hierarchical data, such as a table of contents, code examples, and file directories, in a tree-like structure.
    /// </summary>
    /// <remarks>
    /// The Blazor TreeView component offers various features for editing, load on demand, drag and drop, checkboxes, expand/collapse, and more in both Blazor WebAssembly (WASM) and Blazor Server apps.
    /// </remarks>
    /// <example>
    /// <code>
    /// <SfTreeView TValue="MailItem">
    /// <TreeViewFieldsSettings TValue="MailItem" Id="Id" DataSource="@MyFolder" Text="FolderName" ParentID="ParentId" HasChildren="HasSubFolders" Expanded="Expanded"></TreeViewFieldsSettings>
    /// </SfTreeView>
    /// </code>
    /// </example>
    /// <typeparam name="TValue">Specifies the type of data source.</typeparam>
    public partial class SfTreeView<TValue>
    {
        private const string RTLENABLE = "enableRtl";
        private const string EXPANDONTYPE = "expandOnType";
        private const string TREEVIEWFIELD = "fields";
        private const string SPACE = " ";
        private const string CLASS = "class";
        private const string IDVALUE = "id";
        private const string ROLE = "role";
        private const string ARIALABEL = "aria-label";
        private const string RTL = "e-rtl";
        private const string FULLROWWRAP = "e-fullrow-wrap";
        private const string TREEVIEWALLOWMULTISELECTION = "allowMultiSelection";
        private const string TEXTWRAP = "allowTextWrap";
        private const string TREEVIEWSHOWCHECKBOX = "showCheckBox";
        private const string TREEVIEWALLOWEDITING = "allowEditing";
        private const string TREEVIEWALLOWDRAGANDDROP = "allowDragAndDrop";
        private const string DRAGAREA = "dropArea";
        private const string TREEVIEWFULLROWSELECT = "fullRowSelect";
        private const string TREEVIEWCSSCLASS = "cssClass";
        private const string TREEVIEWDISABLED = "disabled";
        private const string EDISABLED = "e-disabled";
        private const string WRAP = "e-text-wrap";
        private const string TRUE = "true";
        private const string CHECK = "check";
        private const string TEMPLATES = "TreeViewTemplates";
        private const string HASTEMPLATE = "hasTemplate";
        private const string ENABLEVIRTUALIZATION = "enableVirtualization";
        private const string TREEVIEWANIMATIONCLASS = "animation";
        private const string VIRTUALIZATION = "e-virtualization";
        private const string PROPERTYCHANGED = "sfBlazor.TreeView.onPropertyChanged";
        private const string SETMULTISELECT = "sfBlazor.TreeView.setMultiSelect";
        private const string DRAGSTARTACTIONCONTINUE = "sfBlazor.TreeView.dragStartActionContinue";
        private const string NODEDRAGGING = "sfBlazor.TreeView.nodeDragging";
        private const string DRAGNODESTOP = "sfBlazor.TreeView.dragNodeStop";
        private const string NODESELECTION = "sfBlazor.TreeView.nodeSelection";
        private const string DATASOURCECHANGED = "sfBlazor.TreeView.dataSourceChanged";
        private const string UPDATETEXTWRAP = "sfBlazor.TreeView.updateTextWrap";
        private const string EXPANDEDNODE = "sfBlazor.TreeView.expandedNode";
        private const string UPDATESPINNERCLASS = "sfBlazor.TreeView.updateSpinnerClass";
        private const string COLLAPSEDNODE = "sfBlazor.TreeView.collapsedNode";
        private const string INITIALIZE = "sfBlazor.TreeView.initialize";
        private const string BEGINEDIT = "sfBlazor.TreeView.beginEdit";
        private const string ENSUREVISIBLE = "sfBlazor.TreeView.ensureVisible";
        private const string GETARIALEVEL = "sfBlazor.TreeView.getAriaLevel";
        private const string GETITEM = "window.localStorage.getItem";
        private const string DRAGGEDEVENT = "draggedEvent";
        private const string NODECOLLAPSED = "nodeCollapsedEvent";
        private const string CREATED = "createdEvent";

        internal Dictionary<string, ComplexListItems<TValue>>? nodeInstances = new Dictionary<string, ComplexListItems<TValue>>();
        internal bool IsLoaded = true;
        internal bool IsClearStateCall;
        internal string? LoadedId;
        internal HashSet<string> tempSelectedNodes = new();
        internal bool IsDestroyed { get; set; }
        internal bool IsNodeRendered { get; set; }
        internal bool IsNodeExpanded { get; set; }
        internal int ChildCount { get; set; }
        internal int NodeLen { get; set; }
        internal bool IsDevice { get; set; }
        //Specify the action for Checkbox interaction
        internal string? CheckAction { get; set; }
        internal string? Target { get; set; }
        internal SfSpinner? SpinnerRef { get; set; }
        internal HashSet<string>? AllExpandedNodes { get; set; } = new HashSet<string>();
        internal List<string>? InternalExpandedNodes { get; set; } = new List<string>();
        internal List<string>? CurrentExpandedNodes { get; set; } = new List<string>();
        internal List<string> CurrentSelectedNodes { get; set; } = new List<string>();
        internal HashSet<string>? AllSelectedNodes { get; set; } = new HashSet<string>();
        internal List<string>? AllDisabledNodes { get; set; } = new List<string>();
        internal Dictionary<string, object>? AllCheckedNodes { get; set; } = new Dictionary<string, object>();
        internal List<string> IsRenderedNodes { get; set; } = new List<string>();
        internal bool IsCompletelyRendered { get; set; }
        internal string EditedNodeId { get; set; } = string.Empty;
        internal bool IsTreeInteracted { get; set; }
        internal bool isInteracted { get; set; }
        internal string? InteractedNodeId { get; set; }
        internal bool IsInteractedNodeChecked { get; set; }
        internal string? LastNavigableNodeId { get; set; }
        internal string? LastSelectedId { get; set; }
        internal bool IsNodeClicked { get; set; }
        internal List<TValue>? InternalData { get; set; }
        internal bool IsNumberTypeId { get; set; }
        internal TValue? TreeViewRemovedData { get; set; }
        internal bool IsNodeDropped { get; set; }
        internal string dataId = "sfTreeView-" + Guid.NewGuid().ToString();
        internal ListGeneration<TValue>? ListReference { get; set; }
        internal TreeViewEvents<TValue>? TreeViewEvents { get; set; }
        internal TreeViewEventAggregator TreeViewEventAggregator { get; set; } = new TreeViewEventAggregator();
        internal bool IsDataSourceChanged { get; set; }
        [Inject]
        internal NavigationManager? NavigationManager { get; set; }
        internal ReflectionHelper<TValue> Accessor { get; set; } = new ReflectionHelper<TValue>();

        private bool treeViewSelectionUpdate;
        private bool shouldRender = true;
        private bool isTreeNodeExpandingCall;
        private Dictionary<string, object>? attributes = new Dictionary<string, object>();
        private bool treeExpandAll { get; set; }
        private bool isCurrentExpandedUpdated { get; set; }
        private ElementReference element { get; set; }
        private bool treeAllowDragAndDrop { get; set; }
        private bool treeAllowEditing { get; set; }
        private bool treeAllowTextWrap { get; set; }
        private bool treeAllowMultiSelection { get; set; }
        private string[]? treeCheckedNodes { get; set; }
        private bool treeAutoCheck { get; set; } = true;
        private string treeCssClass { get; set; } = string.Empty;
        private bool treeDisabled { get; set; }
        private string? treeDropArea { get; set; }
        private bool treeEnableRtl { get; set; }
        private ExpandAction treeExpandOn { get; set; } = ExpandAction.DoubleClick;
        private string[]? treeExpandedNodes { get; set; }
        private bool treeFullRowSelect { get; set; } = true;
        private string[]? treeSelectedNodes { get; set; }
        private bool treeShowCheckBox { get; set; }
        private SortOrder treeSortOrder { get; set; }
        // To identify the node drag action
        private bool isDragAction { get; set; }
        private bool NodeSelectionWasCancelled;
        private bool isCheckingInProgress = false;
        private string? LastNodeSelectedId;
        internal IEnumerable<TValue> tempDataSource;
        internal bool isDdtFiltering { get; set; }
        private bool isProcessingCheckEvent;
        internal ElementReference EditedLiElement { get; set; }

        private void SetRootAttributes()
        {
            string treeViewClassList = "e-control e-lib e-treeview";
            if (Disabled) treeViewClassList += SPACE + EDISABLED;
            if (!string.IsNullOrEmpty(CssClass)) treeViewClassList += SPACE + CssClass;
            if (EnableRtl || (SyncfusionService != null && SyncfusionService.options.EnableRtl))
                treeViewClassList += SPACE + RTL;
            if (FullRowSelect) treeViewClassList += SPACE + FULLROWWRAP;
            if (AllowTextWrap) treeViewClassList += SPACE + WRAP;
            if (EnableVirtualization) treeViewClassList += SPACE + VIRTUALIZATION;

            string role = "tree";
            string ariaLabel = "treeview";

            if (SfHtmlAttributes != null)
            {
                foreach (KeyValuePair<string, object> item in SfHtmlAttributes)
                {
                    switch (item.Key)
                    {
                        case CLASS:
                            treeViewClassList += SPACE + item.Value;
                            break;
                        case ROLE:
                            role = item.Value?.ToString() ?? role;
                            break;
                        default:
                            SfBaseUtils.UpdateDictionary(item.Key, item.Value, attributes);
                            break;
                    }
                }
            }
            if (attributes != null)
            {
                attributes[IDVALUE] = ID;
                attributes[CLASS] = treeViewClassList;
                attributes[ROLE] = role;
                attributes[ARIALABEL] = ariaLabel;
                attributes["data-id"] = dataId;
                if (!string.IsNullOrEmpty(Height))
                    attributes["data-sf-style"] = $"height:{SfBaseUtils.FormatUnit(Height)}";
            }
        }

        // Set multi select options to TreeView component.
        private async Task SetMultiSelection()
        {
            if (!AllowMultiSelection && AllSelectedNodes != null && AllSelectedNodes.Count > 1)
            {
                List<string> selectedNodes = AllSelectedNodes.ToList();
                selectedNodes.RemoveRange(1, AllSelectedNodes.Count - 1);
                AllSelectedNodes  = selectedNodes.ToHashSet();
                await UpdateSelectedNodes().ConfigureAwait(true);
            }

            await InvokeMethod(SETMULTISELECT, dataId, AllowMultiSelection).ConfigureAwait(true);
        }

        // Update the latest property values to TreeView component.
        internal async Task OnPropertyChangeHandler(Dictionary<string, object> dynamicChanges)
        {
            if (ListReference != null && !AllowDragAndDrop)
            {
                await OnPropertyChangeHandler_ListReference().ConfigureAwait(true);
            }
            if (dynamicChanges.ContainsKey(nameof(ExpandedNodes)) && ListReference != null)
            {
                await OnPropertyChangeHandler_ExpandedNodes().ConfigureAwait(true);
            }
            if (dynamicChanges.ContainsKey(nameof(SelectedNodes)))
            {
                if(SelectedNodes == null || SelectedNodes.Length == 0)
                {
                    AllSelectedNodes?.Clear();
                }
                await OnPropertyChangeHandler_SelectedNodes().ConfigureAwait(true);
                await UpdateSelectedNodes().ConfigureAwait(true);
            }
            if (dynamicChanges.ContainsKey(nameof(CheckedNodes)) && ListReference != null)
            {
                AllCheckedNodes?.Clear();
                await ListReference.UpdateCheckedNodes(true).ConfigureAwait(true);
            }
            if (dynamicChanges.ContainsKey(nameof(AllowMultiSelection)))
            {
                await SetMultiSelection().ConfigureAwait(true);
            }
            if (dynamicChanges.ContainsKey(nameof(AutoCheck)))
            {
                await UpdateCheckedState().ConfigureAwait(true);
            }
            if (dynamicChanges.ContainsKey(nameof(FullRowSelect)))
            {
                ListReference?.ListUpdated();
                await InvokeMethod(PROPERTYCHANGED, dataId, GetPropertyChanges(dynamicChanges)).ConfigureAwait(true);
            }
            Dictionary<string, object> changedProperties = GetPropertyChanges(dynamicChanges);
            if (!dynamicChanges.ContainsKey(nameof(FullRowSelect)) && !dynamicChanges.ContainsKey(nameof(AutoCheck)) && !dynamicChanges.ContainsKey(nameof(AllowMultiSelection)) && !dynamicChanges.ContainsKey(nameof(CheckedNodes)) && !dynamicChanges.ContainsKey(nameof(SelectedNodes)) && !dynamicChanges.ContainsKey(nameof(ExpandedNodes)) && changedProperties.Count > 0)
            {
                await InvokeMethod(PROPERTYCHANGED, dataId, changedProperties).ConfigureAwait(true);
            }
        }

        private async Task OnPropertyChangeHandler_ListReference()
        {
            TreeViewFields.FieldDataSource = NotifyPropertyChanges("DataSource", TreeViewFields.DataSource?.ToList(), TreeViewFields.FieldDataSource?.ToList());
            List<TValue>? treeData = ListReference?.GetTreeViewData();
            bool hasValidDataSource = treeData != null && treeData.Count > 0;  
            if (hasValidDataSource && !PropertyChanges.ContainsKey("DataSource") && ((TreeViewEvents != null && (TreeViewEvents.NodeSelecting.HasDelegate || TreeViewEvents.NodeSelected.HasDelegate || TreeViewEvents.NodeExpanding.HasDelegate || TreeViewEvents.NodeExpanded.HasDelegate)) || CheckedNodesChanged.HasDelegate || ExpandedNodesChanged.HasDelegate || SelectedNodesChanged.HasDelegate))
            {
                return;
            }

            await UpdateData(TreeViewFields != null && TreeViewFields.DataSource != null ? TreeViewFields.DataSource.ToList() : null).ConfigureAwait(true);
        }

        private async Task OnPropertyChangeHandler_ExpandedNodes()
        {
            ExpandedNodes ??= Array.Empty<string>();
            IEnumerable<string>? collapsingNodes = InternalExpandedNodes?.Except(ExpandedNodes);
            IEnumerable<string>? expandingNodes = ExpandedNodes.Except(InternalExpandedNodes);
            if (collapsingNodes?.Any() == true)
            {
                await CollapseAllAsync(collapsingNodes.ToArray()).ConfigureAwait(true);
            }

            if (expandingNodes?.Any() == true)
            {
                await ExpandAllAsync(expandingNodes.ToArray()).ConfigureAwait(true);
            }

            InternalExpandedNodes = ExpandedNodes.ToList();
        }

        private async Task OnPropertyChangeHandler_SelectedNodes()
        {
            List<string> selectedNodes = SelectedNodes?.Where(node => !AllSelectedNodes.Contains(node)).ToList() ?? new List<string>();
            foreach (string node in selectedNodes)
            {
                await TriggerNodeSelectingEvent(new SelectionEventArgs() { IsMultiSelect = AllowMultiSelection, IsCtrKey = false, IsShiftKey = false, Nodes = null!, NodeData = new NodeData() { Id = node } }).ConfigureAwait(true);
                if (node == selectedNodes.Last())
                {
                    treeViewSelectionUpdate = false;
                }
            }
        }

        /// <summary>
        /// Specifies the particular property is changes or not.
        /// </summary>
        /// <returns>"Task".</returns>
        private Dictionary<string, object> GetPropertyChanges(Dictionary<string, object> dynamicChanges)
        {
            Dictionary<string, object> properties = new Dictionary<string, object>(dynamicChanges?.Count ?? 0);
            if(dynamicChanges != null)
            {
                foreach (var propertyName in dynamicChanges.Keys)
                {
                    switch (propertyName)
                    {
                        case nameof(AllowDragAndDrop):
                            properties.Add(TREEVIEWALLOWDRAGANDDROP, AllowDragAndDrop);
                            break;
                        case nameof(AllowEditing):
                            properties.Add(TREEVIEWALLOWEDITING, AllowEditing);
                            break;
                        case nameof(AllowTextWrap):
                            properties.Add(TEXTWRAP, AllowTextWrap);
                            break;
                        case nameof(ShowCheckBox):
                            properties.Add(TREEVIEWSHOWCHECKBOX, ShowCheckBox);
                            break;
                        case nameof(EnableRtl):
                            properties.Add(RTLENABLE, EnableRtl);
                            break;
                        case nameof(Disabled):
                            properties.Add(TREEVIEWDISABLED, Disabled);
                            break;
                        case nameof(DropArea):
                            properties.Add(DRAGAREA, DropArea);
                            break;
                        case nameof(FullRowSelect):
                            properties.Add(TREEVIEWFULLROWSELECT, FullRowSelect);
                            break;
                        case nameof(CssClass):
                            properties.Add(TREEVIEWCSSCLASS, CssClass);
                            break;
                        case nameof(ExpandOn):
                            properties.Add(EXPANDONTYPE, ExpandOn);
                            break;
                    }
                }
            }
            return properties;
        }

        // Update checked nodes state for TreeView component.
        private async Task UpdateCheckedState()
        {
            List<KeyValuePair<string, object>> intermediateNodes = AllCheckedNodes?.Where(y => (string)y.Value == "intermediate").ToList() ?? new List<KeyValuePair<string, object>>();
            foreach (var item in intermediateNodes)
            {
                AllCheckedNodes?.Remove(item.Key);
            }
            if (AutoCheck)
            {
                List<string> checkedNodes = GetCheckedNodes();
                if (ListReference != null)
                {
                    ListReference.ChildItems = new List<TValue>();
                }
                await UpdateCheckedNodeState(checkedNodes, CHECK, false).ConfigureAwait(true);
            }
        }

        private Dictionary<string, object> GetInstance()
        {
            Dictionary<string, object> treeObj = new Dictionary<string, object>
            {
                { RTLENABLE, EnableRtl || (SyncfusionService != null && SyncfusionService.options.EnableRtl) },
                { EXPANDONTYPE, ExpandOn },
                { TREEVIEWANIMATIONCLASS, new Dictionary<string, object>{ { "expand", AnimationSettings.NodeAnimationExpand }, { "collapse", AnimationSettings.NodeAnimationCollapse }  } },
                { TREEVIEWALLOWMULTISELECTION, AllowMultiSelection },
                { TREEVIEWSHOWCHECKBOX, ShowCheckBox },
                { TREEVIEWALLOWEDITING, AllowEditing },
                { TEXTWRAP, AllowTextWrap },
                { TREEVIEWALLOWDRAGANDDROP, AllowDragAndDrop },
                { DRAGAREA, DropArea },
                { TREEVIEWFULLROWSELECT, FullRowSelect },
                { TREEVIEWCSSCLASS, CssClass },
                { TREEVIEWDISABLED, Disabled },
                { HASTEMPLATE, TreeViewTemplate != null },
                { ENABLEVIRTUALIZATION, EnableVirtualization },
                { DRAGGEDEVENT, TreeViewEvents?.OnNodeDragged.HasDelegate ?? false},
                { NODECOLLAPSED, TreeViewEvents?.NodeCollapsed.HasDelegate ?? false},
                { CREATED, TreeViewEvents?.Created.HasDelegate ?? false}
            };
            return treeObj;
        }

        /// <summary>
        /// Drop Node as Sibling for TreeView component.
        /// </summary>
        /// <returns>"Task".</returns>
        /// <param name="args">"Specifies the DropTree argument".</param>
        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task DropNodeAsSibling(DropTreeArgs args)
        {
            TValue removedData;
            SfTreeView<TValue>? DraggedTree = null;
            isDragAction = true;
            bool isExternalDrag = (bool)args?.IsExternalDrag;
            string dragLi = args.DragLi;
            string dropLi = args.DropLi;
            string dragParentLi = args.DragParentLi;
            string dropParentLi = args.DropParentLi;
            bool pre = args.Pre;
            if (isExternalDrag)
            {
                DraggedTree = (SfTreeView<TValue>)args.SrcTree.Value;
            }

            if (ListReference?.DataType == TreeViewDataType.SelfReferential)
            {
                removedData = isExternalDrag ? DraggedTree.ListReference.GetRemovedSelfData(dragLi?.ToString(), true) : ListReference.GetRemovedSelfData(dragLi?.ToString(), true);
                if (removedData != null)
                {
                    if(!string.IsNullOrEmpty(TreeViewFields?.ParentID))
                        removedData.GetType().GetProperty(TreeViewFields.ParentID).SetValue(removedData, IsNumberTypeId ? (object)Convert.ToInt32(dropParentLi?.ToString(), CultureInfo.InvariantCulture) :
                            InternalData != null && (GetValue(TreeViewFields?.Id, InternalData[0])).GetType() == typeof(Guid) ? Guid.Parse(dropParentLi) : dropParentLi);
                    ListReference.DropNodeAsSiblingNode(dropLi, pre, removedData, dropLi == null && isExternalDrag ? true : false);
                    if (dragParentLi != null)
                    {
                        List<TValue>? childNodes = isExternalDrag ? DraggedTree.ListReference.GroupingData(dragParentLi, DraggedTree.ListReference.DataSource?.ToList()) : ListReference.GroupingData(dragParentLi, ListReference?.DataSource?.ToList());
                        if (childNodes == null || childNodes.Count == 0)
                        {
                            if (isExternalDrag)
                            {
                                DraggedTree?.ListReference.GetRemovedSelfData(dragParentLi, false, true);
                            }
                            else
                            {
                                ListReference?.GetRemovedSelfData(dragParentLi, false, true);
                            }
                        }
                    }
                    if (isExternalDrag)
                    {
                        await UpdateDraggedTree(DraggedTree, dragLi, ListReference?.DataSource?.ToList()).ConfigureAwait(true);
                    }
                    else
                    {
                        await UpdateData(ListReference?.DataSource?.ToList()).ConfigureAwait(true);
                    }
                }
            }
            else if (ListReference?.DataType == TreeViewDataType.Hierarchical)
            {
                removedData = GetDraggedData(dragLi, DraggedTree, isExternalDrag);
                if (removedData != null)
                {
                    ListReference.DropNodeAsSiblingNodeHier(dragLi, dropLi, pre, removedData, ListReference?.ItemsData?.ToList(), default, dropLi == null && isExternalDrag ? true : false);
                    await UpdateData(ListReference?.ItemsData?.ToList()).ConfigureAwait(true);
                    if (isExternalDrag)
                    {
                        await UpdateDraggedTree(DraggedTree).ConfigureAwait(true);
                    }
                }
            }
        }

        private async Task UpdateDraggedTree(SfTreeView<TValue>? draggedTree, string? dragLi = null, List<TValue>? dataList = null)
        {
            List<TValue>? draggedDataList = draggedTree?.ListReference.DataSource?.ToList();
            if (ListReference.DataType == TreeViewDataType.SelfReferential)
            {
                List<TValue>? childData = draggedTree?.ListReference.GroupingData(dragLi, null);
                if (childData != null)
                {
                    foreach (TValue item in childData)
                    {
                        dataList?.Add(item);
                        draggedDataList?.Remove(item);
                        UpdateNestedChild(draggedTree, item, dataList);
                    }
                }
                await UpdateData(dataList).ConfigureAwait(true);
            }
            if (draggedTree != null)
            {
                await draggedTree.UpdateData(draggedDataList).ConfigureAwait(true);
            }
        }

        private void UpdateNestedChild(SfTreeView<TValue>? draggedTree, TValue childData, List<TValue>? dataList)
        {
            List<TValue>? draggedDataList = draggedTree?.ListReference.DataSource?.ToList();
            string? parentLi = draggedTree?.TreeViewFields.Id != null && GetValue(draggedTree.TreeViewFields.Id.ToString(), childData) != null ? GetValue(draggedTree.TreeViewFields.Id.ToString(), childData).ToString() : null;
            List<TValue>? subChildData = draggedTree?.ListReference.GroupingData(parentLi, null);
            if (subChildData == null) return;
            foreach (TValue item in subChildData)
            {
                dataList.Add(item);
                draggedDataList.Remove(item);
                UpdateNestedChild(draggedTree, item, dataList);
            }
        }

        private TValue GetDraggedData(string dragLi, SfTreeView<TValue>? draggedTree, bool externalDrag)
        {
            List<TValue>? itemsData;
            TValue? removedData;
            if (externalDrag)
            {
                itemsData = draggedTree?.ListReference?.ItemsData?.ToList();
                draggedTree?.ListReference?.GetAndRemovedHierData(dragLi, itemsData, null);
                removedData = draggedTree.TreeViewRemovedData;
            }
            else
            {
                itemsData = ListReference?.ItemsData?.ToList();
                ListReference?.GetAndRemovedHierData(dragLi, itemsData, null);
                removedData = TreeViewRemovedData;
            }

            return removedData;
        }

        private void UpdateDragExpanded(string? dropLi)
        {
            if (dropLi == null) return;
            if (AllExpandedNodes != null && !AllExpandedNodes.Contains(dropLi))
            {
                AllExpandedNodes.Add(dropLi.ToString());
            }
            if (InternalExpandedNodes != null && !InternalExpandedNodes.Contains(dropLi))
            {
                InternalExpandedNodes.Add(dropLi);
            }
        }

        private async Task UpdateDragData(bool isExternalDrag, string dragLi, SfTreeView<TValue>? draggedTree, List<TValue> dataList)
        {
            if (isExternalDrag)
            {
                await UpdateDraggedTree(draggedTree, dragLi, dataList).ConfigureAwait(true);
            }
            else
            {
                await UpdateData(dataList).ConfigureAwait(true);
            }
        }

        /// <summary>
        /// Drop Node as Sibling for TreeView component.
        /// </summary>
        /// <param name="args">"Specifies the DropTree argument".</param>
        /// <returns>"Task".</returns>
        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task DropNodeAsChild(DropTreeArgs args)
        {
            SfTreeView<TValue>? DraggedTree = null;
            isDragAction = true;
            bool isExternalDrag = (bool)args?.IsExternalDrag;
            string dragLi = args.DragLi;
            string dropLi = args.DropLi;
            string dragParentLi = args.DragParentLi;
            if (isExternalDrag)
            {
                DraggedTree = (SfTreeView<TValue>)args.SrcTree.Value;
            }

            TValue removedData;
            if (ListReference.DataType == TreeViewDataType.SelfReferential)
            {
                removedData = isExternalDrag ? DraggedTree.ListReference.GetRemovedSelfData(dragLi, true) : ListReference.GetRemovedSelfData(dragLi, true);
                if (removedData != null)
                {
                    if (!string.IsNullOrEmpty(TreeViewFields?.ParentID))
                        removedData.GetType().GetProperty(TreeViewFields?.ParentID).SetValue(removedData, IsNumberTypeId ? (object)Convert.ToInt32(dropLi?.ToString(), CultureInfo.InvariantCulture) :
                            InternalData != null && (GetValue(TreeViewFields?.Id, InternalData[0])).GetType() == typeof(Guid) ? Guid.Parse(dropLi) : dropLi);
                    UpdateDragExpanded(dropLi);
                    if (TreeViewFields != null && TreeViewFields.HasChildren != null)
                    {
                        TValue addedData = ListReference.GetRemovedSelfData(dropLi.ToString());
                        addedData?.GetType().GetProperty(TreeViewFields.HasChildren)?.SetValue(addedData, true);
                    }
                    ExpandedNodes = InternalExpandedNodes.ToArray();
                    if (dragParentLi != null)
                    {
                        List<TValue>? childNodes = isExternalDrag ? DraggedTree.ListReference.GroupingData(dragParentLi.ToString(), DraggedTree.ListReference.DataSource?.ToList()) : ListReference.GroupingData(dragParentLi.ToString(), ListReference.DataSource.ToList());
                        if (childNodes == null || childNodes.Count == 0)
                        {
                            if (isExternalDrag) { DraggedTree.ListReference.GetRemovedSelfData(dragParentLi.ToString(), false, true); }
                            else { ListReference.GetRemovedSelfData(dragParentLi.ToString(), false, true); }
                        }
                    }
                    List<TValue>? dataList = ListReference?.DataSource?.ToList();
                    dataList.Add(removedData);
                    await UpdateDragData(isExternalDrag, dragLi, DraggedTree, dataList).ConfigureAwait(true);
                }
            }
            else if (ListReference.DataType == TreeViewDataType.Hierarchical)
            {
                removedData = GetDraggedData(dragLi, DraggedTree, isExternalDrag);
                if (removedData != null)
                {
                    ListReference.AddChildData(dropLi?.ToString(), removedData, InternalData, true);
                    UpdateDragExpanded(dropLi);
                    ExpandedNodes = InternalExpandedNodes.ToArray();
                    await UpdateData(ListReference?.ItemsData?.ToList()).ConfigureAwait(true);
                    if (isExternalDrag)
                    {
                        await UpdateDraggedTree(DraggedTree).ConfigureAwait(true);
                    }
                }
            }
        }

        /// <summary>
        /// Trigger Node Drag Start Event for TreeView component.
        /// </summary>
        /// <param name="args">"Node Drag Start argument".</param>
        /// <param name="left">"Dragged Node position".</param>
        /// <param name="top">"Dragged Node top position".</param>
        /// <returns>"Task".</returns>
        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task TriggerDragStartEvent(DragAndDropEventArgs args, double left, double top)
        {
            if (args != null)
            {
                args.Name = "OnNodeDragStart";
                args.Left = left;
                args.Top = top;
                string? draggedNodeParent = args.DraggedNodeData?.ParentID;
                if (ListReference?.DataType != TreeViewDataType.RemoteData && args.DraggedNodeData?.Id != null)
                {
                    args.DraggedNodeData = ListReference.GetNodeDetails(args.DraggedNodeData.Id);
                    if (ListReference.DataType == TreeViewDataType.Hierarchical)
                        args.DraggedNodeData.ParentID = draggedNodeParent;
                }
                if (TreeViewEventAggregator != null)
                {
                    await TreeViewEventAggregator.NotifyAsync("OnNodeDragStart", args).ConfigureAwait(true);
                }
                if (TreeViewEvents != null && TreeViewEvents.OnNodeDragStart.HasDelegate)
                    await SfBaseUtils.InvokeEvent(TreeViewEvents.OnNodeDragStart, args).ConfigureAwait(true);
                await InvokeMethod(DRAGSTARTACTIONCONTINUE, new object[] { dataId, args.Cancel }).ConfigureAwait(true);
            }
        }

        /// <summary>
        /// Trigger Node Dragging Event for TreeView component.
        /// </summary>
        /// <param name="args">"Node Drag Start argument".</param>
        /// <param name="left">"Dragging Node position".</param>
        /// <param name="top">"Dragging Node top position".</param>
        /// <returns>"Task".</returns>
        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task TriggerNodeDraggingEvent(DragAndDropEventArgs args, double left, double top)
        {
            if (args != null)
            {
                args.Name = "OnNodeDragged";
                args.Left = left;
                args.Top = top;
                if (ListReference.DataType != TreeViewDataType.RemoteData && args.DraggedNodeData.Id != null)
                {
                    args.DraggedNodeData = ListReference.GetNodeDetails(args.DraggedNodeData.Id);
                }
                if (TreeViewEventAggregator != null)
                {
                    await TreeViewEventAggregator.NotifyAsync("OnNodeDragged", args).ConfigureAwait(true);
                }
                if (TreeViewEvents != null && TreeViewEvents.OnNodeDragged.HasDelegate)
                    await SfBaseUtils.InvokeEvent(TreeViewEvents.OnNodeDragged, args).ConfigureAwait(true);
                if (!args.Cancel)
                {
                    await InvokeMethod(NODEDRAGGING, new object[] { dataId }).ConfigureAwait(true);
                }
            }
        }

        /// <summary>
        /// Trigger Node Drag Stop Event for TreeView component.
        /// </summary>
        /// <param name="args">"Node Drop Start argument".</param>
        /// <param name="left">"Drag stop Node position".</param>
        /// <param name="top">"Drag stop Node top position".</param>
        /// <param name="instance">Dropped tree instance</param>
        /// <returns>"Task".</returns>
        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task TriggerDragStopEvent(DragAndDropEventArgs args, double left, double top, DotNetObjectReference<object> instance)
        {
            try
            {
                if (args != null)
                {
                    string draggedNodeParent = args.DraggedNodeData?.ParentID;
                    string droppedNodeParent = args.DroppedNodeData?.ParentID;
                    args.Name = "OnNodeDragStop";
                    args.Left = left;
                    args.Top = top;
                    if (ListReference != null && ListReference.DataType != TreeViewDataType.RemoteData && args.DraggedNodeData.Id != null)
                    {
                        args.DraggedNodeData = ListReference.GetNodeDetails(args.DraggedNodeData.Id);
                        if (ListReference.DataType == TreeViewDataType.Hierarchical)
                            args.DraggedNodeData.ParentID = draggedNodeParent;
                        if (args.DroppedNodeData?.Id != null)
                        {
                            dynamic? dropInstance = instance?.Value as dynamic;
                            args.DroppedNodeData = dropInstance?.ListReference.GetNodeDetails(args.DroppedNodeData.Id) ?? ListReference.GetNodeDetails(args.DroppedNodeData.Id);
                            if ((ListReference.DataType == TreeViewDataType.Hierarchical && dropInstance == null) || dropInstance?.ListReference.DataType == TreeViewDataType.Hierarchical)
                                args.DroppedNodeData.ParentID = droppedNodeParent;
                        }
                    }
                    if (TreeViewEventAggregator != null)
                    {
                        await TreeViewEventAggregator.NotifyAsync("OnNodeDragStop", args).ConfigureAwait(true);
                    }
                    if (TreeViewEvents != null && TreeViewEvents.OnNodeDragStop.HasDelegate)
                        await SfBaseUtils.InvokeEvent(TreeViewEvents.OnNodeDragStop, args).ConfigureAwait(true);
                    await InvokeMethod(DRAGNODESTOP, new object[] { dataId, args }).ConfigureAwait(true);
                }
            }
            catch
            {
                if (!IsDisposed)
                    throw;
            }
        }

        /// <summary>
        /// Trigger Node Drag Stop Event for TreeView component.
        /// </summary>
        /// <param name="args">"Dropped argument".</param>
        /// <param name="left">"Dropped stop Node position".</param>
        /// <param name="top">"Dropped stop Node top position".</param>
        /// <returns>"Task".</returns>
        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task TriggerNodeDropped(DragAndDropEventArgs args, double left, double top)
        {
            if (args != null)
            {
                args.Name = "NodeDropped";
                args.Left = left;
                args.Top = top;
                IsNodeDropped = true;
                if ( TreeViewEventAggregator != null)
                {
                    await TreeViewEventAggregator.NotifyAsync("NodeDropped", args).ConfigureAwait(true);
                }
                if (TreeViewEvents != null && TreeViewEvents.NodeDropped.HasDelegate)
                    await SfBaseUtils.InvokeEvent(TreeViewEvents.NodeDropped, args).ConfigureAwait(true);
                IsNodeDropped = false;
            }
        }

        /// <summary>
        /// Trigger Node Drag Stop Event for TreeView component.
        /// </summary>
        /// <param name="parentNodes">"Parent node".</param>
        /// <returns>"Task".</returns>
        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task UpdateExpandedNode(string[] parentNodes)
        {
            if (parentNodes != null)
            {
                foreach (string parentNode in parentNodes)
                {
                    if (!InternalExpandedNodes.Contains(parentNode))
                    {
                        Internal.ExpandEventArgs nodeArgs = new Internal.ExpandEventArgs
                        {
                            NodeData = new NodeData
                            {
                                Id = parentNode,
                                HasChildren = true
                            },
                            IsLoaded = false
                        };
                        await TriggerNodeExpandingEvent(nodeArgs).ConfigureAwait(true);
                    }
                }
            }
        }

        /// <summary>
        /// Trigger Node Selecting Event for TreeView component.
        /// </summary>
        /// <param name="selectEventArgs">"Select event argument".</param>
        /// <returns>"Task".</returns>
        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task TriggerNodeSelectingEvent(SelectionEventArgs selectEventArgs)
        {
            LastSelectedId = string.Empty;
            NodeSelectEventArgs args = new NodeSelectEventArgs() { Action = selectEventArgs?.Action, IsInteracted = selectEventArgs.IsInteracted, NodeData = selectEventArgs.NodeData };
            string parentId = args.NodeData.ParentID;
            args.Name = args.Action == "select" ? "NodeSelecting" : "NodeUnSelecting";
            args.NodeData = ListReference.GetNodeDetails(args.NodeData.Id);
            args.NodeData.ParentID = parentId;
            if (TreeViewEventAggregator != null)
                await TreeViewEventAggregator.NotifyAsync("NodeSelecting", args).ConfigureAwait(true);
            if (TreeViewEvents?.NodeSelecting.HasDelegate == true)
                await TreeViewEvents.NodeSelecting.InvokeAsync(args).ConfigureAwait(true);
            NodeSelectionWasCancelled = args.Cancel;
            if (!args.Cancel)
            {
                args.Name = args.Action == "select" ? "NodeSelected" : "NodeUnSelected";
                if (args.Action == "un-select" && AllSelectedNodes.Contains(args.NodeData.Id))
                {
                    AllSelectedNodes.Remove(args.NodeData.Id);
                }
                else
                {
                    SelectionAction(args, selectEventArgs.IsMultiSelect, selectEventArgs.IsCtrKey, selectEventArgs.IsShiftKey, selectEventArgs.Nodes);
                }
                if (selectEventArgs.IsInteracted) { IsTreeInteracted = true; }
                await UpdateSelectedNodes().ConfigureAwait(true);
                await InvokeMethod(NODESELECTION, new object[] { dataId, AllSelectedNodes }).ConfigureAwait(true);
                if (TreeViewEventAggregator != null)
                    await TreeViewEventAggregator.NotifyAsync("NodeSelected", args).ConfigureAwait(true);
                if (TreeViewEvents?.NodeSelected.HasDelegate == true)
                    await TreeViewEvents.NodeSelected.InvokeAsync(args).ConfigureAwait(true);
                CurrentSelectedNodes.Clear();
                if (IsDestroyed) return;
                if (!args.Cancel)
                {
                    if (EnablePersistence)
                        await ListReference.UpdatePersistence().ConfigureAwait(true);
                    List<TValue> selectedData = GetTreeData(args.NodeData.Id);
                    LastSelectedId = args.NodeData.Id;
                    if (!CurrentSelectedNodes.Contains(args.NodeData.Id))
                    {
                        CurrentSelectedNodes.Add(args.NodeData.Id);
                    }
                    string? url = GetValue(TreeViewFields.NavigateUrl, selectedData[0])?.ToString();
                    if (!string.IsNullOrEmpty(url) && LastNavigableNodeId == LastSelectedId)
                    {
                        NavigationManager?.NavigateTo(url);
                    }
                    LastNodeSelectedId = args.NodeData.Id;
                }
            }
            if (!string.IsNullOrEmpty(LastNavigableNodeId))
            {
                LastNavigableNodeId = string.Empty;
            }
        }

        // User interactions to update the selected node values in TreeView component.
        private void SelectionAction(NodeSelectEventArgs args, bool multiSelect, bool ctrKey, bool shiftKey, string[] array)
        {
            if (!AllowMultiSelection || (!multiSelect && !ctrKey))
            {
                AllSelectedNodes.Clear();
            }

            if (AllowMultiSelection && shiftKey && array.Length > 0)
            {
                AllSelectedNodes = array.ToHashSet();
            }
            else if (AllowMultiSelection && array == null && !AllSelectedNodes.Contains(args.NodeData.Id))
            {
                if (!treeViewSelectionUpdate)
                {
                    AllSelectedNodes = SelectedNodes?.ToHashSet();
                    treeViewSelectionUpdate = true;
                }
            }
            else if (!AllSelectedNodes.Contains(args.NodeData.Id))
            {
                AllSelectedNodes.Add(args.NodeData.Id);
            }
        }

        /// <summary>
        /// Trigger Node Editing Event for TreeView component.
        /// </summary>
        /// <param name="args">"Node Edit event argument".</param>
        /// <returns>"Task".</returns>
        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task TriggerNodeEditingEvent(NodeEditEventArgs args)
        {
            EditedNodeId = args?.NodeData.Id;
            args.NodeData = GetNode(args.NodeData.Id);
            args.OldText = args.NodeData.Text;
            args.Name = "NodeEditing";
            if (args != null)
            {
                if (TreeViewEventAggregator != null)
                    await TreeViewEventAggregator.NotifyAsync("NodeEditing", args).ConfigureAwait(true);
                if (TreeViewEvents != null && TreeViewEvents.NodeEditing.HasDelegate)
                    await SfBaseUtils.InvokeEvent(TreeViewEvents.NodeEditing, args).ConfigureAwait(true);
                if (args.Cancel)
                {
                    EditedNodeId = null!;
                }

                ListReference.ListUpdated();
            }
        }

        internal async Task TriggerNodeEditedEvent(string newText)
        {
            NodeEditEventArgs eventArgs = new NodeEditEventArgs
            {
                NewText = newText,
                NodeData = ListReference.GetNodeDetails(EditedNodeId),
                OldText = ListReference.GetNodeDetails(EditedNodeId).Text,
                Name = "NodeEdited"
            };
            if ( TreeViewEventAggregator != null)
                await TreeViewEventAggregator.NotifyAsync("NodeEdited", eventArgs).ConfigureAwait(true);
            if (TreeViewEvents != null && TreeViewEvents.NodeEdited.HasDelegate)
                await SfBaseUtils.InvokeEvent(TreeViewEvents.NodeEdited, eventArgs).ConfigureAwait(true);
            newText = eventArgs.Cancel ? eventArgs.OldText : eventArgs.NewText;
            ListReference.UpdateSelfNodeText(EditedNodeId, newText);
            EditedNodeId = null!;
            ListReference.ListUpdated();
            await TriggerDataSourceChangedEvent(true).ConfigureAwait(true);
            if (ListReference.DataType == TreeViewDataType.RemoteData)
            {
                if (TreeViewFields != null && TreeViewFields.DataManager != null && !TreeViewFields.DataManager.Offline)
                {
                    await InvokeMethod("sfBlazor.TreeView.nodeEdited", new object[] { dataId, EditedLiElement });
                    await TreeViewFields.DataManager.Update<TValue>(TreeViewFields.Id, GetTreeData(eventArgs.NodeData.Id)[0], TreeViewFields.Query?.FromTable, TreeViewFields.Query).ConfigureAwait(true);
                }
            }
        }

        internal async Task TriggerDataSourceChangedEvent(bool preventClientCall = false)
        {
            isDragAction = false;
            if (!preventClientCall && IsRendered)
            {
                await InvokeMethod(DATASOURCECHANGED, dataId).ConfigureAwait(true);
            }
            DataSourceChangedEventArgs<TValue> args = new DataSourceChangedEventArgs<TValue>() { Name = "DataSourceChanged" };
            await TreeViewEventAggregator.NotifyAsync("DataSourceChanged", args).ConfigureAwait(true);
            if (TreeViewEvents != null && TreeViewEvents.DataSourceChanged.HasDelegate)
            {
                await SfBaseUtils.InvokeEvent(TreeViewEvents.DataSourceChanged, args).ConfigureAwait(true);
            }
        }

        /// <summary>
        /// Trigger TreeView created event.
        /// </summary>
        /// <returns>"Task".</returns>
        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task CreatedEvent()
        {
            if ( TreeViewEventAggregator != null)
                await TreeViewEventAggregator.NotifyAsync("Created", null).ConfigureAwait(true);
            await SfBaseUtils.InvokeEvent<ActionEventArgs>(TreeViewEvents?.Created, null!).ConfigureAwait(true);
        }

        /// <summary>
        /// Trigger Node Expanding Event for TreeView component.
        /// </summary>
        /// <param name="arguments">"Expand event argument".</param>
        /// <returns>"Task".</returns>
        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task TriggerNodeExpandingEvent(Internal.ExpandEventArgs arguments)
        {
            bool isUpdated = false;
            if (arguments != null)
            {
                PreventRender();
                NodeExpandEventArgs args = new NodeExpandEventArgs()
                {
                    Event = arguments.Event,
                    IsInteracted = arguments.IsInteracted,
                    NodeData = arguments.NodeData,
                    Name = "NodeExpanding"
                };
                var offline = TreeViewFields.DataManager?.Offline ?? false;
                if (ListReference.DataType != TreeViewDataType.RemoteData)
                {
                    args.NodeData = ListReference.GetNodeDetails(args.NodeData.Id);
                }
                if (string.IsNullOrEmpty(args.NodeData?.Id))
                    return;
                if (TreeViewEventAggregator != null)
                    await TreeViewEventAggregator.NotifyAsync("NodeExpanding", args).ConfigureAwait(true);
                if (TreeViewEvents != null && TreeViewEvents.NodeExpanding.HasDelegate)
                    await SfBaseUtils.InvokeEvent(TreeViewEvents.NodeExpanding, args).ConfigureAwait(true);
                if (!args.Cancel)
                {
                    Target = args.NodeData?.Id;
                    if (ListReference.DataType == TreeViewDataType.RemoteData)
                    {
                        IEnumerable<TValue> childData = (IEnumerable<TValue>)ListReference.GetChildRemoteData(args.NodeData?.Id);
                        if (LoadOnDemand && !offline && !arguments.IsLoaded)
                        {
                            if (!arguments.IsInteracted) { arguments.NodeLevel = await InvokeMethod<int>(GETARIALEVEL, false, new object[] { dataId, args }).ConfigureAwait(true); }
                            await ListReference.RenderRemoteLi(args.NodeData?.Id, arguments.NodeLevel, childData != null).ConfigureAwait(true);
                        }
                    }

                    if (!arguments.IsLoaded)
                    {
                        IsNodeExpanded = true;
                        if (args.IsInteracted || treeExpandAll)
                        {
                            IsLoaded = false;
                            LoadedId = args.NodeData?.Id;
                        }
                        NodeLen = 0;
                    }
                    else
                    {
                        IsLoaded = true;
                    }

                    if (!InternalExpandedNodes.Contains(args.NodeData.Id))
                    {
                        InternalExpandedNodes.Add(args.NodeData.Id);
                        isUpdated = true;
                    }

                    if (ListReference != null && ListReference.DataType == TreeViewDataType.RemoteData && ListReference.RemoteExpandedValues != null && !ListReference.RemoteExpandedValues.Contains(args.NodeData.Id))
                    {
                        ListReference.RemoteExpandedValues.Add(args.NodeData.Id);
                    }

                    if (!AllExpandedNodes.Contains(args.NodeData.Id))
                    {
                        AllExpandedNodes.Add(args.NodeData.Id);
                        isUpdated = true;
                    }

                    if (isUpdated)
                    {
                        await UpdateExpandedNodes().ConfigureAwait(true);
                        ListReference.ListUpdated();
                    }
                    IsLoaded = true;
                    treeExpandAll = false;
                    if (args.IsInteracted == false && ExpandedNodesChanged.HasDelegate)
                    {
                        isTreeNodeExpandingCall = true;
                    }
                    if (UpdateIcon(args.NodeData.Id))
                    {
                        await InvokeMethod(EXPANDEDNODE, new object[] { dataId, args }).ConfigureAwait(true);
                    }
                }

                if (ListReference != null)
                {
                    await ListReference.UpdatePersistence().ConfigureAwait(true);
                }
                ShouldRender();
            }
        }

        private List<string> GetCheckedNodes()
        {
            List<string> checkedNodes = new List<string>();
            foreach (KeyValuePair<string, object> checkedNode in AllCheckedNodes)
            {
                if (checkedNode.Value.ToString() == TRUE)
                {
                    checkedNodes.Add(checkedNode.Key);
                }
            }

            return checkedNodes;
        }

        /// <summary>
        /// Trigger Node Checking Event for TreeView component.
        /// </summary>
        /// <param name="args">"NodeCheck event argument".</param>
        /// <param name="dictData">"Data Source for CheckAll and UnCheckAll action".</param>
        /// <returns>"Task".</returns>
        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task TriggerNodeCheckingEvent(NodeCheckEventArgs args, Dictionary<string, TValue> dictData = null)
        {
            if (args != null && !isProcessingCheckEvent)
            {
                isProcessingCheckEvent = true;
                try
                {
                    args.Name = args.Action == "check" ? "NodeChecking" : "NodeUnChecking";
                    CheckAction = args.Action;
                    isInteracted = args.IsInteracted;
                    InteractedNodeId = args.NodeData.Id;
                    if (ListReference.DataType != TreeViewDataType.RemoteData)
                    {
                        args.NodeData = ListReference.GetNodeDetails(args.NodeData.Id, dictData);
                    }
                    await UpdateCheckedNodes().ConfigureAwait(true);
                    await TreeViewEventAggregator.NotifyAsync("NodeChecking", args).ConfigureAwait(true);
                    if (TreeViewEvents != null && TreeViewEvents.NodeChecking.HasDelegate)
                        await SfBaseUtils.InvokeEvent(TreeViewEvents.NodeChecking, args).ConfigureAwait(true);
                    if (!args.Cancel && args.NodeData.Id != null)
                    {
                        if (AllCheckedNodes.ContainsKey(args.NodeData.Id) && args.Action != CHECK)
                        {
                            AllCheckedNodes.Remove(args.NodeData.Id);
                        }
                        else
                        {
                            SfBaseUtils.UpdateDictionary(args.NodeData.Id, TRUE, AllCheckedNodes);
                        }

                        if (AutoCheck)
                        {
                            if (args.Action == CHECK)
                            {
                                if (AllCheckedNodes.ContainsKey(args.NodeData.Id))
                                {
                                    AllCheckedNodes[args.NodeData.Id] = TRUE;
                                }
                                else
                                {
                                    SfBaseUtils.UpdateDictionary(args.NodeData.Id, TRUE, AllCheckedNodes);
                                }
                                ListReference.ChildItems = new List<TValue>();
                                await UpdateCheckedNodeState(new List<string>() { args.NodeData.Id }, args.Action, false, dictData).ConfigureAwait(true);
                            }
                            else
                            {
                                AllCheckedNodes.Remove(args.NodeData.Id);
                                await UpdateCheckedNodeState(new List<string>() { args.NodeData.Id }, args.Action, true, dictData).ConfigureAwait(true);
                            }
                        }

                        await UpdateCheckedNodes().ConfigureAwait(true);
                        ComplexListItems<TValue>? instance = !AutoCheck ? nodeInstances?.GetValueOrDefault(args.NodeData.Id) : null;
                        if (instance?.TreeOptions != null)
                        {
                            instance.TreeOptions.IsChecked = args.Action == CHECK ? "true" : "false";
                            instance.ReRender();
                        }
                        else
                        {
                            List<string> intermediateNodes = AllCheckedNodes.Where(node => node.Value.ToString() == "intermediate").Select(node => node.Key).ToList();
                            await InvokeMethod("sfBlazor.TreeView.nodeCheck", new object[] { dataId, CheckedNodes, intermediateNodes }).ConfigureAwait(true);
                        }
                        args.Name = args.Action == "check" ? "NodeChecked" : "NodeUnChecked";
                        if (ListReference.DataType != TreeViewDataType.RemoteData)
                        {
                            args.NodeData.IsChecked = AllCheckedNodes.ContainsKey(args.NodeData.Id) ? TRUE : "false";
                        }
                        await TreeViewEventAggregator.NotifyAsync("NodeChecked", args).ConfigureAwait(true);
                        if (TreeViewEvents != null && TreeViewEvents.NodeChecked.HasDelegate)
                        {
                            await SfBaseUtils.InvokeEvent(TreeViewEvents.NodeChecked, args).ConfigureAwait(true);
                        }
                        await ListReference.UpdatePersistence().ConfigureAwait(true); 
                    }
                }
                finally
                {
                    isProcessingCheckEvent = false;
                }
            }
            
        }

        private void UpdateCheckedValueToDatasource(List<string>? checkedNodes)
        {
            if (TreeViewFields?.IsChecked != null)
            {
                if (ListReference?.DataType == TreeViewDataType.Hierarchical)
                {
                    UpdateHierarchicalData(checkedNodes, (TreeViewEvents != null && TreeViewEvents.NodeDropped.HasDelegate) ? ListReference?.DataSource?.ToList() : TreeViewFields?.DataSource?.ToList(), TreeViewFields?.IsChecked);
                }
                else if (ListReference?.DataType == TreeViewDataType.SelfReferential)
                {
                    UpdateSelfReferentialData(checkedNodes, (TreeViewEvents != null && TreeViewEvents.NodeDropped.HasDelegate) ? ListReference?.DataSource?.ToList() : TreeViewFields?.DataSource?.ToList(), TreeViewFields?.IsChecked);
                }
            }
        }

        internal void UpdateSelfReferentialData(List<string>? id, IEnumerable<TValue>? dataSource, string? propertyName)
        {
            if (ListReference != null)
            {
                List<TValue>? dataList = dataSource?.ToList();
                if (CheckedNodesChanged.HasDelegate && dataList != null)
                {
                    foreach (TValue item in dataList)
                    {
                        string idAttrValue = ListReference.GetAttrValue(TreeViewFields.Id, item);
                        Accessor.SetValue(item, propertyName, id?.Contains(idAttrValue));
                    }
                    
                }

                ListReference.DataSource = dataList;
                InternalData = ListReference.DataSource?.ToList();
                TreeViewFields.GetType().GetProperty("DataSource").SetValue(TreeViewFields, InternalData);
                ListReference.ItemsData = ListReference.DataSource;
            }
        }

        internal void UpdateHierarchicalData(List<string>? id, IEnumerable<TValue>? dataSource, string? propertyName)
        {
            List<TValue>? dataList = dataSource?.ToList();
            if (CheckedNodesChanged.HasDelegate)
            {
                TreeViewFieldsSettings<TValue> fields = TreeViewFields;
                foreach (TValue item in dataList)
                {
                    string? idAttrValue = ListReference?.GetAttrValue(fields.Id, item);
                    IEnumerable<TValue>? childs = fields.Child != null ? (IEnumerable<TValue>)DataUtil.GetObject(fields.Child.ToString(), item) : null;
                    if (id.Contains(idAttrValue))
                    {
                        item.GetType().GetProperty(propertyName).SetValue(item, true);
                    }
                    else
                    {
                        item?.GetType()?.GetProperty(propertyName)?.SetValue(item, false);
                    }

                    if (childs != null)
                    {
                        UpdateHierarchicalData(id, (List<TValue>)childs, propertyName);
                    }
                }
            }

            ListReference.DataSource = dataList;
            InternalData = dataList;
            TreeViewFields.GetType().GetProperty("DataSource").SetValue(TreeViewFields, InternalData);
            ListReference.ItemsData = ListReference.DataSource;
        }

        // Update checked Nodes state for TreeView component.
        private async Task UpdateCheckedNodeState(List<string> checkedNodes, string action, bool flag, Dictionary<string, TValue>? dictData = null)
        {
            try
            {
                if (ListReference?.DataType == TreeViewDataType.SelfReferential)
                {
                    ListReference.UpdateChildCheckedNodes(checkedNodes, action);
                    var dictionary = dictData ?? ListReference?.ItemsData?.ToDictionary(data => GetValue(TreeViewFields?.Id, data)?.ToString() ?? string.Empty);
                    ListReference?.UpdateSelfIntermediateState(checkedNodes, dictionary, new());
                }
                else if (ListReference?.DataType == TreeViewDataType.Hierarchical)
                {
                    ListReference.UpdateHierarchicalChildCheckedNodes(checkedNodes, action);
                    ListReference.UpdateCheckedDataFromDS(flag ? GetCheckedNodes() : checkedNodes, action);
                }
                else
                {
                    await UpdateCheckedNodes().ConfigureAwait(true);
                }
            }
            catch
            {
                if (!IsDisposed)
                    throw;
            }
        }

        /// <summary>
        /// Trigger Node Click Event for TreeView component.
        /// </summary>
        /// <param name="eventArgs">"NodeClick event argument".</param>
        /// <param name="mouseEventArgs">"Specifies the original browser mouse event".</param>
        /// <param name="id">"Clicked node id".</param>
        /// <param name="left">"Clicked node Left position".</param>
        /// <param name="top">"Clicked node Top position".</param>
        /// <returns>"Task".</returns>
        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task TriggerNodeClickingEvent(ClickEventArgs eventArgs, MouseEventArgs mouseEventArgs, string id, double left, double top)
        {
            if (eventArgs != null)
            {
                NodeClickEventArgs args = new NodeClickEventArgs()
                {
                    Name = "NodeClicked",
                    NodeData = GetNode(id),
                    Event = eventArgs,
                    Left = left,
                    Top = top
                };
                args.Event.OriginalEvent = mouseEventArgs;
                InteractedNodeId = id;
                IsInteractedNodeChecked = args.NodeData.IsChecked == TRUE;
                IsNodeClicked = args.NodeData.Selected;
                if ( TreeViewEventAggregator != null)
                    await TreeViewEventAggregator.NotifyAsync("NodeClicked", args).ConfigureAwait(true);
                if (TreeViewEvents != null && TreeViewEvents.NodeClicked.HasDelegate)
                    await SfBaseUtils.InvokeEvent(TreeViewEvents.NodeClicked, args).ConfigureAwait(true);
                if (args.NodeData.Id == LastNodeSelectedId && !NodeSelectionWasCancelled)
                {
                    List<TValue> selectedData = GetTreeData(args.NodeData.Id);
                    string? url = GetValue(TreeViewFields.NavigateUrl, selectedData[0])?.ToString();
                    if (!string.IsNullOrEmpty(url))
                    {
                        NavigationManager?.NavigateTo(url);
                    }
                }
            }
        }

        private bool UpdateIcon(string id)
        {
            bool hasChild = true;
            if (ListReference?.DataType == TreeViewDataType.SelfReferential)
            {
                List<TValue> childNodes = ListReference.GroupingData(id, ListReference?.DataSource?.ToList());
                if (childNodes == null || childNodes.Count == 0)
                {
                    ListReference?.GetRemovedSelfData(id, false, true);
                    ListReference?.ListUpdated();
                    hasChild = false;
                }
            }

            return hasChild;
        }

        /// <summary>
        /// Trigger Node Expanded Event for TreeView component.
        /// </summary>
        /// <param name="args">"Node Expanded event argument".</param>
        /// <returns>"Task".</returns>
        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task TriggerNodeExpandedEvent(NodeExpandEventArgs args)
        {
            if (args != null)
            {
                PreventRender();
                args.Name = "NodeExpanded";
                if (ListReference.DataType != TreeViewDataType.RemoteData)
                {
                    args.NodeData = ListReference.GetNodeDetails(args.NodeData.Id);
                }
                Target = null!;
                if (SpinnerRef != null)
                {
                    await SpinnerRef.HideAsync().ConfigureAwait(true);
                    SpinnerRef.Dispose();
                }
                isTreeNodeExpandingCall = false;
                await TreeViewEventAggregator.NotifyAsync("NodeExpanded", args).ConfigureAwait(true);
                if (TreeViewEvents != null && TreeViewEvents.NodeExpanded.HasDelegate)
                    await SfBaseUtils.InvokeEvent(TreeViewEvents.NodeExpanded, args).ConfigureAwait(true);
                ShouldRender();
            }
        }

        /// <summary>
        /// Update the latest data source values to TreeView component (Drag and drop).
        /// </summary>
        /// <param name="dataSource">"Specifies the datasource".</param>
        /// <param name="isUpdateChecked">"Specifies the checked is true or not".</param>
        /// <returns>"Task".</returns>
        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task UpdateData(List<TValue> dataSource, bool isUpdateChecked = false)
        {
            if (!SfBaseUtils.Equals(InternalData, dataSource) && ListReference != null && isUpdateChecked)
            {
                if (IsClearStateCall)
                {
                    await ClearStateAsync().ConfigureAwait(true);
                }
                await ListReference.TriggerDataBoundEvent().ConfigureAwait(true);
            }
            IsClearStateCall = false;
            if (TreeViewFields?.DataSource != null && TreeViewFields?.DataManager == null || ListReference.isFiltering)
            {
                InternalData = dataSource != null ? ListGeneration<TValue>.GetSortedData(dataSource.ToList(), SortOrder.ToString(), TreeViewFields.Text) : new List<TValue>();
            }

            ListReference.ListData = InternalData;
            if (TreeViewFields?.DataManager?.Adaptor.ToString() == "CustomAdaptor")
            {
                ListReference.ListData = ListReference.GroupingData(null, null);
            }
            ListReference.DataSource = InternalData;
            if (InternalData != null)
            {
                ListReference.ItemsData = InternalData;
                ListReference.IsSelfChildsUpdate = true;
                ListReference.SelfChilds.Clear();
                await ListReference.IdentifyDataSource(isUpdateChecked).ConfigureAwait(true);
                ListReference.ListUpdated();
            }
            if (isUpdateChecked || isDragAction)
            {
                await TriggerDataSourceChangedEvent().ConfigureAwait(true);
            }
        }

        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task<NodeExpandEventArgs?> NodeCollapsingEventCallback(NodeExpandEventArgs args)
        {
            if ( TreeViewEventAggregator != null)
                await TreeViewEventAggregator.NotifyAsync("NodeCollapsing", args).ConfigureAwait(true);
            if (TreeViewEvents != null && TreeViewEvents.NodeCollapsing.HasDelegate)
                await SfBaseUtils.InvokeEvent(TreeViewEvents.NodeCollapsing, args).ConfigureAwait(true);
            return args;
        }

        /// <summary>
        /// Collapse Action for TreeView.
        /// </summary>
        /// <param name="args">"NodeCollapsing event argument".</param>
        /// <returns>"Task".</returns>
        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task TriggerNodeCollapsingEvent(NodeExpandEventArgs args)
        {
            bool isUpdated = false;
            if (args != null)
            {
                args.Name = "NodeCollapsing";
                if (ListReference.DataType != TreeViewDataType.RemoteData)
                {
                    args.NodeData = ListReference.GetNodeDetails(args.NodeData.Id);
                }

                if (!args.Cancel)
                {
                    if (InternalExpandedNodes.Contains(args.NodeData.Id))
                    {
                        InternalExpandedNodes.RemoveAt(InternalExpandedNodes.IndexOf(args.NodeData.Id));
                        isUpdated = true;
                    }
                    if (AllExpandedNodes.Contains(args.NodeData.Id) && !ExpandedNodesChanged.HasDelegate)
                    {
                        AllExpandedNodes.RemoveWhere(item => item == args.NodeData.Id);
                        isUpdated = true;
                    }
                    if (ListReference != null && ListReference.DataType == TreeViewDataType.RemoteData && ListReference.RemoteExpandedValues != null && ListReference.RemoteExpandedValues.Contains(args.NodeData.Id))
                    {
                        ListReference.RemoteExpandedValues.Remove(args.NodeData.Id);
                    }
                    if (isUpdated)
                    {
                        await UpdateExpandedNodes().ConfigureAwait(true);
                    }
                    if (args.IsInteracted == true && ExpandedNodesChanged.HasDelegate)
                    {
                        isTreeNodeExpandingCall = true;
                    }
                    if (args.NodeData.Expanded)
                    {
                        await InvokeMethod(COLLAPSEDNODE, new object[] { dataId, args }).ConfigureAwait(true);
                    }
                }

                if (ListReference != null)
                {
                    await ListReference.UpdatePersistence().ConfigureAwait(true);
                }
            }
        }

        /// <summary>
        /// Collapsed Action for TreeView.
        /// </summary>
        /// <param name="args">"Key press event argument".</param>
        /// <param name="id">"Specifies the Id".</param>
        /// <param name="keyAction">"Specifies the key action".</param>
        /// <param name="keyValue">"Specifies the Key value".</param>
        /// <returns>"Task".</returns>
        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task<NodeKeyPressEventArgs?> TriggerKeyboardEvent(NodeKeyPressEventArgs args, string id, string keyAction, string keyValue)
        {
            if (args != null)
            {
                args = new NodeKeyPressEventArgs() { Name = "OnKeyPress", Action = keyAction, Key = keyValue };
                if (ListReference != null && ListReference.DataType != TreeViewDataType.RemoteData && id != null)
                {
                    args.NodeData = ListReference.GetNodeDetails(id);
                }
                await TreeViewEventAggregator.NotifyAsync("OnKeyPress", args).ConfigureAwait(true);
                if (TreeViewEvents != null && TreeViewEvents.OnKeyPress.HasDelegate)
                    await SfBaseUtils.InvokeEvent(TreeViewEvents.OnKeyPress, args).ConfigureAwait(true);
                return args.Cancel ? null : args;
            }
            return null;
        }

        /// <summary>
        /// Collapsed Action for TreeView.
        /// </summary>
        /// <param name="args">"NodeCollapsed event argument".</param>
        /// <returns>"Task".</returns>
        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task TriggerNodeCollapsedEvent(NodeExpandEventArgs args)
        {
            if (args != null)
            {
                args.Name = "NodeCollapsed";
                await TreeViewEventAggregator.NotifyAsync("NodeCollapsed", args).ConfigureAwait(true);
                if (TreeViewEvents != null && TreeViewEvents.NodeCollapsed.HasDelegate)
                    await SfBaseUtils.InvokeEvent(TreeViewEvents.NodeCollapsed, args).ConfigureAwait(true);
            }
        }

        /// <summary>
        /// Update child property values in the TreeView component instance.
        /// </summary>
        /// <param name="key">Specifies the key field.</param>
        /// <param name="details">Specifies the details field.</param>
        public void UpdateChildProperties(string key, object details)
        {
            switch (key)
            {
                case TREEVIEWFIELD:
                    TreeViewFields = (TreeViewFieldsSettings<TValue>)details;
                    break;
                case TEMPLATES:
                    TreeViewTemplate = (TreeViewTemplates<TValue>)details;
                    break;
            }
        }

        internal void ClearExpandedNode()
        {
            ExpandedNodes = null!;
        }

        internal void UpdateTreeSelectedNodes()
        {
            if (AllSelectedNodes == null) return;
            SelectedNodes = AllSelectedNodes.ToArray();
            treeSelectedNodes = SelectedNodes;
            tempSelectedNodes = AllSelectedNodes.ToHashSet();
        }

        internal async Task UpdateSelectedNodes()
        {
            SelectedNodes = treeSelectedNodes = await SfBaseUtils.UpdateProperty<string[]>( AllSelectedNodes?.Count > 0 ? AllSelectedNodes.ToArray() : Array.Empty<string>(), treeSelectedNodes, SelectedNodesChanged).ConfigureAwait(true);
            tempSelectedNodes = AllSelectedNodes?.ToHashSet() ?? new HashSet<string>();
        }

        internal async Task UpdateExpandedNodes()
        {
            ExpandedNodes = treeExpandedNodes = await SfBaseUtils.UpdateProperty<string[]>(InternalExpandedNodes.Count > 0 ? InternalExpandedNodes.ToArray() : Array.Empty<string>(), treeExpandedNodes, ExpandedNodesChanged).ConfigureAwait(true);
        }

        internal async Task UpdateCheckedNodes()
        {
            try
            {
                string[]? checkedNodes = AllCheckedNodes?.Where(x => x.Value == TRUE as object).Select(x => x.Key).ToArray();
                string[] checkNodes = checkedNodes?.Length > 0 ? checkedNodes : null!;
                if (!string.IsNullOrEmpty(CssClass) && !CssClass.Contains("e-ddt", StringComparison.Ordinal))
                    UpdateCheckedValueToDatasource(checkedNodes?.ToList());
                CheckedNodes = treeCheckedNodes = await SfBaseUtils.UpdateProperty<string[]>(checkNodes, treeCheckedNodes, CheckedNodesChanged).ConfigureAwait(true);
            }
            catch
            {
                if (!IsDisposed)
                    throw;
            }
        }

        internal object GetValue(string? fieldName, object obj)
        {
            return Accessor.GetValue(obj, fieldName);
        }

        internal async override void ComponentDispose()
        {
            if (IsRendered && !IsDestroyed)
            {
                if ( TreeViewEventAggregator != null)
                    await TreeViewEventAggregator.NotifyAsync("Destroyed", null).ConfigureAwait(true);
                if (TreeViewEvents != null && TreeViewEvents.Destroyed.HasDelegate)
                {
                    await SfBaseUtils.InvokeEvent<ActionEventArgs>(TreeViewEvents.Destroyed, null!).ConfigureAwait(true);
                }

                IsDestroyed = true;
                AllExpandedNodes?.Clear();
                InternalExpandedNodes?.Clear();
                CurrentExpandedNodes?.Clear();
                AllSelectedNodes?.Clear();
                AllCheckedNodes?.Clear();
                AllDisabledNodes?.Clear();
                if (attributes == null)
                {
                    attributes = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase)
                    {
                        ["data-sf-style"] = string.Empty
                    };
                }
                else
                {
                    var keys = attributes.Keys.ToList();
                    foreach (var key in keys)
                    {
                        if (!key.Equals("data-sf-style", StringComparison.OrdinalIgnoreCase))
                        {
                            attributes.Remove(key);
                        }
                    }
                    attributes["data-sf-style"] = string.Empty;
                }
                if (ListReference != null)
                {
                    ListReference.DataSource = null;
                    ListReference.ItemsData = null;
                    ListReference.ChildItems = null;
                    ListReference.ListData = null;
                    ListReference = null;
                }
                SpinnerRef?.Dispose();
                InternalData = null;
                TreeViewEvents = null;
                AllExpandedNodes = null;
                InternalExpandedNodes = null;
                CurrentExpandedNodes = null;
                AllSelectedNodes = null;
                AllCheckedNodes = null;
                AllDisabledNodes = null;
                nodeInstances?.Clear();
                nodeInstances = null;
                await WindowInstanceDispose(dataId).ConfigureAwait(false);
            }
        }
    }
}
