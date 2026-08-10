using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Inputs.Internal;
using Syncfusion.Blazor.DropDowns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Syncfusion.Blazor.Internal;
using Syncfusion.Blazor.Data;
using Microsoft.AspNetCore.Components.Web;
using System.ComponentModel;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Navigations.Internal;
using Syncfusion.Blazor.Inputs;
using System.Globalization;
using Syncfusion.Blazor.Popups;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// The Dropdown Tree component allows to select required values in input from hierarchical tree structure of data.
    /// </summary>
    /// <example>
    /// In the following code example, a basic Dropdown Tree component is initialized with simple tree list items.
    /// <code><![CDATA[
    /// @using Syncfusion.Blazor.DropDowns
    /// <SfDropDownTree TItem="TreeItem" TValue="string">
    ///    <DropDownTreeField  TItem="TreeItem" DataSource="TreeDataSource" Id="NodeId" Text="NodeText" Expanded="Expanded" Child="@("Child")"></DropDownTreeField>
    /// </SfDropDownTree>
    /// 
    /// @code {
    ///     List<TreeItem> TreeDataSource = new List<TreeItem>();
    ///     protected async override Task OnInitializedAsync()
    ///     {
    ///         await base.OnInitializedAsync();
    ///         TreeDataSource.Add(new TreeItem
    ///         {
    ///             NodeId = "01",
    ///             NodeText = "Local Disk (C:)",
    ///             Expanded = true,
    ///             Child = new List<TreeItem>()
    ///             {
    ///                 new TreeItem { NodeId = "01-01", NodeText = "Program Files",
    ///                 Child = new List<TreeItem>()
    ///                 {
    ///                     new TreeItem { NodeId = "01-01-01", NodeText = "Windows NT" },
    ///                     new TreeItem { NodeId = "01-01-02", NodeText = "Windows Mail" },
    ///                 },
    ///             },
    ///         },
    ///         });
    ///         }
    ///          public class TreeItem
    ///          {
    ///             public string NodeId { get; set; }
    ///             public string NodeText { get; set; }
    ///             public string Icon { get; set; }
    ///             public bool Expanded { get; set; }
    ///             public bool Selected { get; set; }
    ///             public List<TreeItem> Child { get; set; }
    ///           }
    ///     }
    /// ]]></code>
    /// </example>
    /// <typeparam name="TValue">Specifies the value type.</typeparam>
    /// <typeparam name="TItem">Specifies the type of <see cref="SfDropDownTree{TValue,TItem}"/>.</typeparam>
    public partial class SfDropDownTree<TValue, TItem> : IDropDownTree
    {
        private const string DROPDOWNICON = "e-ddt-icon e-icons";
        private const string HIDEICON = "e-ddt-icon-hide";
        private const string RTL = "e-rtl";
        private const string TABINDEX = "tabindex";
        private const string ROLE = "role";
        private const string ARIAHASPOPUP = "aria-haspopup";
        private const string ARIAEXPANDED = "aria-expanded";
        private const string ARIAACTIVEDESCENDANT = "aria-activedescendant";
        private const string ARIADESCRIBEDBY = "aria-describedby";
        private const string COMBOBX = "combobox";
        private const string TREE = "tree";
        private const string FALSE = "false";
        private const string OVERFLOWCLASS = "e-overflow e-icon-hide";
        private const string NODATA = "e-no-data";
        private const string SPACE = " ";
        private const string CONTAINERCLASS = "e-ddt e-lib e-show-dd-icon";
        private const string ROOTCLASS = "e-control e-dropdowntree e-lib";
        private const string NO_RECORD_KEY = "DropDownTree_NoRecords";
        private const string THE_REQUEST_FAILED_KEY = "DropDownTree_RequestFailed";
        private const string SELECT_ALL_kEY = "DropDownTree_SelectAll";
        private const string UN_SELECT_ALL_kEY = "DropDownTree_UnSelectAll";

        private SfTreeView<TItem>? TreeObj { get; set; }
        private SfInputBase? InputBaseObj { get; set; }
        private IEnumerable<TItem>? DataSource { get; set; } = new List<TItem>();
        private ReflectionHelper<TItem> Accessor { get; set; } = new();
        private DropDownTreeField<TItem> DropDownTreeFields { get; set; } = new();

        private Dictionary<string, object> inputAttributes = new();
        private ElementReference popupContentElement;
        private List<TValue>? previousValue;
        // SelectedNodes and CheckedNodes are internally bound to TreeView
        private string[] selectedNodes = Array.Empty<string>();
        private string[] checkedNodes = Array.Empty<string>();
        private string[]? expandedNodes;
        private string[]? internalExpandedNodes;
        private bool isClearButtonClick;
        private string popupClass = "e-ddt e-popup";
        private bool actionFailure;
        private Dictionary<string, string> selectedData = new();
        private List<ChipItems> chipItems = new();
        private bool isChipDelete;
        private bool isPopupOpen;
        private List<TValue> currentValue = new();
        private List<TItem>? filteredData;
        private bool isFilteredData;
        private bool isDestroyed;
        private bool isInternalChange;
        private string? filterValue;
        private bool isCheckActionPrevent;
        private int overAllLiItems;
        private bool isFromNodeClick;
        private bool isSelectAllChecked;
        private string uniqueID = Guid.NewGuid().ToString();
        private bool shouldRender = true;
        private bool showPopupTree;
        private TreeViewDataType dataType;
        private Dictionary<string, TreeData<TItem>> AllData = new();

        private List<TValue>? value;
        private bool showCheckBox;
        private bool showClearButton;
        private bool showSelectAll;
        private string? popupWidth;
        private string? popupHeight;
        private double zIndex;
        private bool allowFiltering;
        private bool allowMultiSelection;
        private bool disabled;
        private string? text;
        private string? delimiterChar;
        private DdtVisualMode mode;
        private bool textWrap;
        private bool isCancelled;
        private bool isInternalSync;
        internal string dataId = "sfDropDownTree-" + Guid.NewGuid().ToString();

        private async Task OnContainerClick(MouseEventArgs? eventArgs)
        {
            if (Disabled)
            {
                return;
            }
            if (isClearButtonClick)
            {
                isClearButtonClick = false;
                if (ShowCheckBox && ShowSelectAll)
                    isSelectAllChecked = checkedNodes != null && overAllLiItems == checkedNodes.Length;
                return;
            }
            if (isPopupOpen)
            {
                await InvokeMethod("sfBlazor.DropDownTree.invokePopupEvent", new object[] { dataId, currentValue, eventArgs! }).ConfigureAwait(true);
            }
            else
            {
                showPopupTree = true;
                if (eventArgs == null)
                {
                    await InvokeAsync(() => StateHasChanged()).ConfigureAwait(true);
                    PreventRender();
                }
                await ShowPopup(eventArgs).ConfigureAwait(true);
                PreventRender();
            }
        }

        private List<TItem> GetValueData()
        {
            List<TItem> DataList = new List<TItem>();
            if (Value == null)
            {
                return DataList;
            }
            foreach(var item in currentValue)
            {
                TItem? ValueData = GetTreeData(item!.ToString()).FirstOrDefault();
                if (ValueData != null)
                {
                    DataList.Add(ValueData);
                }
            }
            return DataList;
        }

        private async Task ShowPopup(MouseEventArgs? eventArgs = null)
        {
            PopupEventArgs args = new() { Cancel = false };
            if (OnPopupOpen.HasDelegate)
            {
                await OnPopupOpen.InvokeAsync(args).ConfigureAwait(true);
            }
            if (isFilteredData)
            {
                await ClearFilteredData().ConfigureAwait(true);
            }
            if (!args.Cancel)
            {
                await Task.Yield();
                await InvokeMethod("sfBlazor.DropDownTree.showPopup", new object[] { dataId, currentValue, eventArgs!, popupContentElement }).ConfigureAwait(true);
            }

        }

        private async Task ClearFilteredData()
        {
            filterValue = string.Empty;
            isFilteredData = false;
            filteredData = DataSource?.ToList();
            expandedNodes = internalExpandedNodes;
            if (TreeObj?.ListReference != null)
            {
                TreeObj.ListReference.IsSelfChildsUpdate = true;
                TreeObj.ListReference.SelfChilds.Clear();
                await TreeObj.UpdateData(DataSource!.ToList()).ConfigureAwait(true);
            }
        }

        private async Task SetTreeText(bool isDynamicChange = false)
        {
            if (Value != null && !isDynamicChange)
                return;
            if (!string.IsNullOrEmpty(Text))
            {
                List<TValue> valueList = new();
                string[] textArr = (ShowCheckBox || AllowMultiSelection) ? Text.Split(DelimiterChar + SPACE) : new[] { Text };

                foreach (string item in textArr)
                {
                    List<KeyValuePair<string, TreeData<TItem>>> matches = AllData.Where(entry => string.Equals(entry.Value.Text, item, IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal)).ToList();
                    if (matches.Count == 1)
                    {
                        valueList.Add((TValue)SfBaseUtils.ChangeType(matches[0].Key, typeof(TValue)));
                    }
                }

                if (valueList.Count != 0)
                {
                    Value = currentValue = valueList;
                    await SetValidValue().ConfigureAwait(true);

                }
                await UpdateTwoWayBinding().ConfigureAwait(true);
            }
        }

        private async Task UpdateTwoWayBinding()
        {
            if (!isInternalSync)
            {  
                previousValue = Value = currentValue = await SfBaseUtils.UpdateProperty(currentValue, Value, ValueChanged).ConfigureAwait(true);
            }
            else
            {
                previousValue = Value = currentValue;
            }
        }

        private void UpdateState(string field, List<string>? nodeList, string id, TItem itemData)
        {
            bool? state = (bool?)Accessor.GetValue(itemData, field);
            if (state == true && nodeList != null && !nodeList.Contains(id))
                nodeList.Add(id);
        }

        private void UpdateAllData(List<TItem>? tempData, List<string>? eNodes = null, List<string>? cNodes = null, List<string>? sNodes = null)
        {
            if (tempData == null || tempData.Count == 0)
                return;
            foreach (TItem itemData in tempData)
            {
                string? id = Accessor.GetValue(itemData, DropDownTreeFields.ID)?.ToString();
                if (!string.IsNullOrEmpty(DropDownTreeFields.Expanded))
                    UpdateState(DropDownTreeFields.Expanded, eNodes, id!, itemData);
                if (!string.IsNullOrEmpty(DropDownTreeFields.Selected))
                    UpdateState(DropDownTreeFields.Selected, sNodes, id!, itemData);
                if (!string.IsNullOrEmpty(DropDownTreeFields.IsChecked) && ShowCheckBox)
                    UpdateState(DropDownTreeFields.IsChecked, cNodes, id!, itemData);
                if (!string.IsNullOrEmpty(DropDownTreeFields.Child))
                {
                    List<TItem>? child = (List<TItem>)DataUtil.GetObject(DropDownTreeFields.Child, itemData);
                    if (!AllData.TryGetValue(id!, out TreeData<TItem>? value))
                    {
                        AllData.Add(id!, new TreeData<TItem>() { Child = child ?? new(), Text = Accessor.GetValue(itemData, DropDownTreeFields.Text)?.ToString() });
                    }
                    else
                    {
                        value?.Child?.AddRange(child);
                    }
                    UpdateAllData(child, eNodes, cNodes, sNodes);
                }
                else
                {
                    string? parenID = Accessor.GetValue(itemData, DropDownTreeFields.ParentID)?.ToString();
                    if (!AllData.ContainsKey(id!))
                    {
                        AllData[id!] = new TreeData<TItem>() { Child = new(), Text = Accessor.GetValue(itemData, DropDownTreeFields.Text)?.ToString() };
                    }
                    if (parenID != null)
                    {
                        if (!AllData.TryGetValue(parenID, out TreeData<TItem>? value))
                        {
                            TItem? parentItem = tempData.Find(x => Accessor.GetValue(x, DropDownTreeFields.ID)?.ToString() == parenID);
                            AllData.Add(parenID, new TreeData<TItem>() { Child = new() { itemData }, Text = Accessor.GetValue(parentItem!, DropDownTreeFields.Text)?.ToString() });
                        }
                        else
                        {
                            value?.Child?.Add(itemData);
                        }
                    }
                }
            }
            expandedNodes = eNodes!.ToArray();
            checkedNodes = cNodes!.ToArray();
            selectedNodes = sNodes!.ToArray();
        }

        private async Task ClearAll(bool removeFocus = false)
        {
            if (Disabled)
            {
                return;
            }
            DdtChangeEventArgs<TValue> changeEventArgs = new() { Action = DdtAction.Unselect, Cancel = false, IsInteracted = !removeFocus, PreviousValue = previousValue! };
            await ValueChanging.InvokeAsync(changeEventArgs).ConfigureAwait(true);
            if (changeEventArgs.Cancel) return;
            isClearButtonClick = true;
            await ResetValue().ConfigureAwait(true);
            await InvokeMethod("sfBlazor.DropDownTree.clearIconClick", new object[] { dataId, currentValue, removeFocus }).ConfigureAwait(true);
        }

        private async Task ResetValue(bool isDynamicChange = false)
        {
            if ((currentValue != null && currentValue.Count == 0) && Text == null)
            {
                return;
            }
            await InputBaseObj!.SetValue(null, FloatLabelType, ShowClearButton).ConfigureAwait(true);
            Text = null!;
            selectedNodes = Array.Empty<string>();
            if (showPopupTree)
                TreeObj!.AllSelectedNodes = new HashSet<string>();
            if (!isDynamicChange)
            {
                currentValue = new List<TValue>();
                await UpdateTwoWayBinding().ConfigureAwait(true);
            }
            if (ShowCheckBox)
            {
                if (showPopupTree)
                    await TreeObj!.UncheckAllAsync().ConfigureAwait(true);
                else
                {
                    checkedNodes = Array.Empty<string>();
                    isSelectAllChecked = false;
                }
            }
            if (AllowMultiSelection || ShowCheckBox)
            {
                chipItems.Clear();
                await SetMultiSelect(isDynamicChange).ConfigureAwait(true);

            }
        }

        private void SetAttributes()
        {
            inputAttributes = new Dictionary<string, object>
            {
                { ROLE, COMBOBX },
                { ARIAHASPOPUP, TREE }, { ARIAEXPANDED, FALSE }, {"aria-controls", ID + "_options_" + uniqueID}
            };
            popupClass += SPACE + HIDEICON;
            if (SyncfusionService.options.EnableRtl)
            {
                popupClass += SPACE + RTL;
            }
        }

        private async Task FilterChangeHandler(InputEventArgs args)
        {
            filterValue = args.Value;
            try
            {
                DdtFilteringEventArgs filterArgs = new() { Cancel = false, Text = args.Value };
                if (Filtering.HasDelegate)
                    await Filtering.InvokeAsync(filterArgs).ConfigureAwait(true);
                if (!filterArgs.Cancel)
                {
                    isFilteredData = true;
                    if (args.Value == null || args.Value.Length == 0)
                    {
                        if (TreeObj != null)
                            TreeObj.isDdtFiltering = false;
                        await ClearFilteredData().ConfigureAwait(true);
                    }
                    else if (!string.IsNullOrEmpty(args.Value))
                    {
                        await FilterHandler(args.Value).ConfigureAwait(true);
                        await TreeObj!.ExpandAllAsync().ConfigureAwait(true);
                    }
                } 
            }
            catch
            {
                if (!IsDisposed)
                    throw;
            }
        }

        private async Task FilterHandler(string value)
        {
            List<TItem> matchedDataSource;
            if (dataType == TreeViewDataType.Hierarchical)
            {
                matchedDataSource = HierarchicalFilter(value);
            }
            else
            {
                TreeObj!.ListReference.IsSelfChildsUpdate = true;
                TreeObj?.ListReference.SelfChilds.Clear();
                matchedDataSource = SelfReferentialFilter(value);
            }
            filteredData = matchedDataSource;
            await RefreshPopup().ConfigureAwait(true);
            if (filteredData.Count == 0)
            {
                await InvokeAsync(StateHasChanged);
            }
            if (TreeObj != null)
                TreeObj.isDdtFiltering = true;
            await TreeObj!.UpdateData(matchedDataSource).ConfigureAwait(true);
        }

        private static List<TItem> GetClonedList(List<TItem>? items)
        {
            return items?.Select(item => (TItem)CloneUtils.Clone(item, typeof(TItem))).ToList() ?? new List<TItem>();
        }

        private List<TItem> SelfReferentialFilter(string value)
        {
            List<TItem> matchedData = new();
            List<TItem> matchedDataSource = new();
            List<TItem> treeData = GetClonedList(DataSource?.ToList());
            matchedData = treeData.FindAll((item) => IsMatchedNode(value, item));
            foreach (TItem data in matchedData)
            {
                if (matchedDataSource.IndexOf(data) == -1)
                {
                    matchedDataSource.Add(data);
                    string? parentId = DataUtil.GetObject(DropDownTreeFields.ParentID, data)?.ToString();
                    while (parentId != null)
                    {
                        TItem? parent = default;
                        foreach (TItem item in treeData)
                        {
                            string? id = DataUtil.GetObject(DropDownTreeFields.ID, item)?.ToString();
                            if (id != null && id == parentId)
                            {
                                parent = item;
                                break;
                            }
                        }
                        if (parent != null && matchedDataSource.IndexOf(parent) == -1)
                        {
                            matchedDataSource.Add(parent);
                            parentId = DataUtil.GetObject(DropDownTreeFields.ParentID, parent)?.ToString();
                        }
                        else
                            break;
                    }
                }
            }
            foreach (TItem data in matchedDataSource)
            {
                string? nodeId = DataUtil.GetObject(DropDownTreeFields.ID, data)?.ToString();
                if (matchedDataSource.Find((item) => DataUtil.GetObject(DropDownTreeFields.ParentID, item)?.ToString() == nodeId) == null)
                {
                    ReflectionExtension.SetValue(data, DropDownTreeFields.HasChildren.ToString(), false);
                    ReflectionExtension.SetValue(data, DropDownTreeFields.Expanded.ToString(), false);
                }
            }
            return matchedDataSource;
        }

        private List<TItem> HierarchicalFilter(string value)
        {
            List<TItem> matchedDataSource = new();
            List<TItem> treeData = DataSource!.ToList();
            foreach (TItem data in treeData)
            {
                TItem? filteredChild = HierarchicalChildFilter(value, data);
                if (filteredChild != null)
                {
                    matchedDataSource.Add(filteredChild);
                }
            }
            return matchedDataSource;
        }

        private TItem? HierarchicalChildFilter(string value, TItem node)
        {
            List<TItem>? children = (List<TItem>)DataUtil.GetObject(DropDownTreeFields.Child, node);
            if (children == null)
            {
                return IsMatchedNode(value, node) ? node : default;
            }
            else
            {
                List<TItem> matchedChildren = new();
                foreach (TItem item in children)
                {
                    TItem? filteredChild = HierarchicalChildFilter(value, item);
                    if (filteredChild != null)
                    {
                        matchedChildren.Add(filteredChild);
                    }
                }
                TItem filteredItems = (TItem)CloneUtils.Clone(node, typeof(TItem));
                if (matchedChildren.Count != 0)
                {
                    ReflectionExtension.SetValue(filteredItems, DropDownTreeFields.Child.ToString(), matchedChildren);
                    return filteredItems;
                }
                else
                {
                    ReflectionExtension.SetValue(filteredItems, DropDownTreeFields.Child.ToString(), null);
                    return IsMatchedNode(value, filteredItems) ? filteredItems : default;
                }
            }
        }

        private static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;
            text = text.Normalize(NormalizationForm.FormD);
            var chars = text.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormD);
        }

        private bool IsMatchedNode(string value, TItem node, int level = 0)
        {
            string checkValue = (string)DataUtil.GetObject(DropDownTreeFields?.Text, node);
            if (IgnoreCase)
            {
                checkValue = checkValue.ToLower(CultureInfo.CurrentCulture);
                value = value.ToLower(CultureInfo.CurrentCulture);
            }
            if (IgnoreAccent)
            {
                checkValue = RemoveDiacritics(checkValue);
                value = RemoveDiacritics(value);
            }
            if (FilterType == DropDowns.FilterType.StartsWith)
                return checkValue.Length >= value.Length && checkValue[..value.Length] == value;
            else if (FilterType == DropDowns.FilterType.EndsWith)
                return checkValue.Length >= value.Length && checkValue[^value.Length..] == value;
            else
                return checkValue.Contains(value, StringComparison.Ordinal);
        }

        private async Task BeforeCheck(Task incomingTask, object? treeArgs)
        {
            if (treeArgs is not NodeCheckEventArgs args)
            {
                return;
            }
            PreventRender();
            isCheckActionPrevent = args.IsInteracted;
            if ((args.IsInteracted || isFromNodeClick) && ValueChanging.HasDelegate)
            {
                isFromNodeClick = false;
                isInternalChange = true;
                List<TItem>? currentNodeFromDS = TreeObj?.GetTreeData(args.NodeData.Id) ?? default;
                TValue currentNodeId = (TValue)DataUtil.GetObject(DropDownTreeFields.ID, currentNodeFromDS!.FirstOrDefault());
                DdtChangeEventArgs<TValue> changeEventArgs = new() { Action = (args.Action == "check" ? DdtAction.Select : DdtAction.Unselect), Cancel = false, IsInteracted = args.IsInteracted, NodeData = args.NodeData, PreviousValue = previousValue!, CurrentValue = currentNodeId };
                await ValueChanging.InvokeAsync(changeEventArgs).ConfigureAwait(true);
                args.Cancel = changeEventArgs.Cancel;
                isInternalChange = false;
            }
        }

        private async Task OnBeforeSelect(Task incomingTask, object? treeArgs)
        {
            if (treeArgs is not NodeSelectEventArgs args)
            {
                return;
            }
            PreventRender(false);
            if (args.IsInteracted)
            {
                internalExpandedNodes = expandedNodes = TreeObj?.ExpandedNodes;
                if (!ValueChanging.HasDelegate)
                    return;
                isInternalChange = true;
                isFromNodeClick = true;
                List<TItem>? currentNodeFromDS = TreeObj?.GetTreeData(args.NodeData.Id);
                TValue currentNodeId = (TValue)DataUtil.GetObject(DropDownTreeFields.ID, currentNodeFromDS!.FirstOrDefault());
                DdtAction action = ShowCheckBox ? (args.NodeData.IsChecked == "true" ? DdtAction.Unselect : DdtAction.Select) : (args.Action == "select" ? DdtAction.Select : DdtAction.Unselect);
                DdtChangeEventArgs<TValue> changeEventArgs = new() { Action = action, Cancel = false, IsInteracted = args.IsInteracted, NodeData = args.NodeData, CurrentValue = currentNodeId, PreviousValue = previousValue! };
                await ValueChanging.InvokeAsync(changeEventArgs).ConfigureAwait(true);
                args.Cancel = isCancelled = changeEventArgs.Cancel;
                isInternalChange = false;
            }
        }

        private async Task OnNodeSelected(Task incomingTask, object? treeArgs)
        {
            if (treeArgs is not NodeSelectEventArgs args)
            {
                return;
            }
            PreventRender();
            isInternalChange = true;
            if (ShowCheckBox)
            {
                return;
            }
            if (args.IsInteracted)
            {
                string id = args.NodeData.Id;
                if (!AllowMultiSelection)
                {
                    TValue tempId = (TValue)SfBaseUtils.ChangeType(id, typeof(TValue))!;
                    currentValue = new List<TValue>() { tempId };
                    Text = args.NodeData.Text;
                    await InputBaseObj!.SetValue(Text, FloatLabelType, ShowClearButton).ConfigureAwait(true);
                    await UpdateTwoWayBinding().ConfigureAwait(true);
                    if (isDestroyed) return;
                    SfBaseUtils.UpdateDictionary(ARIADESCRIBEDBY, ID, InputBaseObj.ContainerAttr);
                    SfBaseUtils.UpdateDictionary(ARIAACTIVEDESCENDANT, id, InputBaseObj.ContainerAttr);
                    if (ValueTemplate != null && !AllowMultiSelection && !ShowCheckBox)
                    {
                        await InvokeMethod("sfBlazor.DropDownTree.updateValue", new object[] { dataId, currentValue }).ConfigureAwait(true);
                    }
                    else
                    {
                        await InvokeMethod("sfBlazor.DropDownTree.closePopup", new object[] { dataId }).ConfigureAwait(true);
                    }
                }
                else if (AllowMultiSelection)
                {
                    await SetMultiSelect().ConfigureAwait(true);
                }
                await UpdatePersistence().ConfigureAwait(true);
            }
            isInternalChange = false;
        }

        private async Task OnNodeChecked(Task incomingTask, object? treeArgs)
        {
            if (treeArgs is not NodeCheckEventArgs args)
            {
                return;
            }
            PreventRender();
            isCheckActionPrevent = args.IsInteracted;
            if (!isChipDelete && args.IsInteracted)
            {
                isInternalChange = true;
                await SetMultiSelect().ConfigureAwait(true);
            }
            if (ShowSelectAll)
            {
                isSelectAllChecked = checkedNodes != null && overAllLiItems == checkedNodes.Length;
            }
            currentValue = checkedNodes != null ? checkedNodes.Select(item => (TValue)SfBaseUtils.ChangeType(item, typeof(TValue)))?.ToList()! : new List<TValue>();
            await UpdateTwoWayBinding().ConfigureAwait(true);
            await UpdatePersistence().ConfigureAwait(true);
            isInternalChange = false;
        }

        private async Task OnKeyPress(Task incomingTask, object? treeArgs)
        {
            if (treeArgs is not NodeKeyPressEventArgs args)
            {
                return;
            }
            if (showCheckBox && args.Key == "Enter")
            {
                if (args.NodeData.IsChecked == "true")
                {
                    args.Cancel = true;
                }
                isCheckActionPrevent = false;
                NodeClickEventArgs clickArgs = new() { NodeData = args.NodeData };
                await TreeObj!.TreeViewEventAggregator.NotifyAsync("NodeClicked", clickArgs).ConfigureAwait(true);
            }
        }

        private async Task OnDataSourceChanged(Task incomingTask, object? args)
        {
            TreeObj?.PreventRender();
            if (DropDownTreeFields?.isDataSourceUpdated == true)
            {
                DropDownTreeFields.isDataSourceUpdated = false;
                UpdateAllData(DataSource?.ToList(), TreeObj?.ExpandedNodes?.ToList() ?? new(), checkedNodes?.ToList() ?? new(), selectedNodes?.ToList() ?? new());
                overAllLiItems = AllData.Count;
                await UpdateValue(Value).ConfigureAwait(true);
                await SetTreeText(false).ConfigureAwait(true);
            }
        }

        private async Task UpdateExpandedNodes(Task incomingTask, object? treeArgs = null)
        {
            NodeExpandEventArgs? args = (NodeExpandEventArgs?)treeArgs;
            PreventRender();
            if (args == null || args.IsInteracted)
                internalExpandedNodes = expandedNodes = TreeObj?.ExpandedNodes;
            if (args != null)
                await RefreshPopup().ConfigureAwait(true);
        }

        private async Task OnNodeExpanding(Task incomingTask, object? treeArgs)
        {
            if (treeArgs is not NodeExpandEventArgs args)
            {
                return;
            }
            isCheckActionPrevent = args.IsInteracted;
            PreventRender();
        }

        private async Task RefreshPopup(Task incomingTask = null!, object? args = null)
        {
            expandedNodes = TreeObj?.ExpandedNodes;
            PreventRender();
            TreeObj?.PreventRender();
            if (showPopupTree)
                await InvokeMethod("sfBlazor.DropDownTree.refreshPosition", new object[] { dataId }).ConfigureAwait(true);
        }

        private async Task OnNodeClicked(Task incomingTask, object? treeArgs)
        {
            if (treeArgs is not NodeClickEventArgs args)
            {
                return;
            }
            PreventRender();
            isInternalChange = true;
            if (isCheckActionPrevent)
            {
                isCheckActionPrevent = false;
                return;
            }
            if (!AllowMultiSelection && !ShowCheckBox)
            {
                await InvokeMethod("sfBlazor.DropDownTree.onNodeSelected", new object[] { dataId, currentValue }).ConfigureAwait(true);
            }
            isFromNodeClick = !isFromNodeClick;
            if (ShowCheckBox && !isCancelled)
            {
                if (args.NodeData.IsChecked == "true")
                {
                    await TreeObj!.UncheckAllAsync(new string[] { args.NodeData.Id }).ConfigureAwait(true);
                }
                else
                {
                    await TreeObj!.CheckAllAsync(new string[] { args.NodeData.Id }).ConfigureAwait(true);
                }
                await SetMultiSelect().ConfigureAwait(true);
                await UpdatePersistence().ConfigureAwait(true);
                isInternalChange = false;
            }
        }

        private async Task SetTreeValue(bool IsDynamicChange = false)
        {
            if (currentValue != null)
            {
                TreeData<TItem>? data;
                if (ShowCheckBox || AllowMultiSelection)
                {
                    List<TValue>? valueList = new(Value);
                    int count = valueList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        data = AllData.GetValueOrDefault(Value[i]?.ToString() ?? string.Empty);
                        if (data == null)
                        {
                            valueList.Remove(Value[i]);
                        }
                    }
                    if (valueList.Count == 0 && !SfBaseUtils.Equals(Value, valueList))
                    {
                        currentValue = new(value!);
                        await UpdateTwoWayBinding().ConfigureAwait(true);
                        return;
                    }
                    currentValue = valueList;
                    await UpdateTwoWayBinding().ConfigureAwait(true);
                    if (currentValue.Count != 0)
                    {
                        isInternalChange = true;
                        await SetValidValue(IsDynamicChange).ConfigureAwait(true);
                    }
                }
                else
                {
                    data = AllData.GetValueOrDefault(Value.FirstOrDefault()?.ToString() ?? string.Empty);
                    if (data != null)
                    {
                        isInternalChange = true;
                        Text = data.Text!;
                        await SetValidValue(IsDynamicChange).ConfigureAwait(true);
                    }
                    else
                    {
                        currentValue = new(value!);
                        await UpdateTwoWayBinding().ConfigureAwait(true);
                        return;
                    }
                }
                isInternalChange = false;
            }
        }

        private async Task SetValidValue(bool IsDynamicChange = false)
        {
            if (!ShowCheckBox && !AllowMultiSelection)
            {
                if (InputBaseObj != null)
                {
                    await InputBaseObj.SetValue(Text, FloatLabelType, ShowClearButton).ConfigureAwait(true);
                }
                string id = currentValue.Count != 0 ? currentValue.FirstOrDefault()?.ToString()! : string.Empty;
                if ((TreeObj == null && !showPopupTree) || (TreeObj?.SelectedNodes.Length == 0) || (TreeObj?.SelectedNodes.FirstOrDefault() != id))
                {
                    selectedNodes = new string[] { id };
                }
                await InvokeMethod("sfBlazor.DropDownTree.updateSelectedValue", new object[] { dataId, currentValue, false }).ConfigureAwait(true);
            }
            else
            {
                List<string>? tempCurrentValue = currentValue?.Select(item => (string)SfBaseUtils.ChangeType(item, typeof(string)))?.ToList();
                if (ShowCheckBox)
                {
                    List<TValue>? tempCheckedNodes = checkedNodes != null ? checkedNodes.Select(item => (TValue)SfBaseUtils.ChangeType(item, typeof(TValue)))?.ToList() : new List<TValue>();
                    if (!SfBaseUtils.Equals(currentValue, tempCheckedNodes) || AutoUpdateCheckState)
                    {
                        if (showPopupTree && tempCurrentValue != null)
                            try
                            {
                                isInternalSync = true;
                                await TreeObj!.CheckAllAsync(tempCurrentValue.ToArray()).ConfigureAwait(true);
                            }
                            finally
                            {
                                isInternalSync = false;
                            }
                        else if (tempCurrentValue != null)
                        {
                            checkedNodes = tempCurrentValue.ToArray();
                            if (AutoUpdateCheckState)
                            {
                                List<string> result = new(tempCurrentValue);
                                GetChild(tempCurrentValue, result);
                                GetParents(tempCurrentValue, result);
                                result = result.Distinct().ToList();
                                checkedNodes = result.ToArray();
                            }
                        }
                        await SetMultiSelect(IsDynamicChange, true).ConfigureAwait(true);
                    }
                }
                else
                {
                    if (!showPopupTree && tempCurrentValue != null)
                        selectedNodes = tempCurrentValue.ToArray();
                    await UpdateSelectedValues(!string.IsNullOrEmpty(Text)).ConfigureAwait(true);
                }
            }
        }

        private void GetParents(List<string>? checkedNodes, List<string>? result)
        {
            if (dataType == TreeViewDataType.SelfReferential && checkedNodes != null)
            {
                foreach (string checkedNode in checkedNodes)
                {
                    TItem? data = DataSource!.FirstOrDefault(data => Accessor.GetValue(data, DropDownTreeFields.ID)?.ToString() == checkedNode);
                    string? parentID = data != null ? Accessor.GetValue(data, DropDownTreeFields.ParentID)?.ToString() : null;
                    if (parentID != null && !result!.Contains(parentID))
                    {
                        List<TItem>? siblingNodes = AllData.GetValueOrDefault(parentID)?.Child?.ToList();
                        int checkCount = 0;
                        if (siblingNodes != null)
                        {
                            foreach (TItem siblingNode in siblingNodes)
                            {
                                string? idAttrValue = Accessor.GetValue(siblingNode, DropDownTreeFields.ID)?.ToString();
                                if (result.Contains(idAttrValue!))
                                    checkCount++;
                            }
                        }
                        if (checkCount == siblingNodes?.Count && !result.Contains(parentID))
                            result.Add(parentID);
                        GetParents(new List<string> { parentID }, result);
                    }
                }
            }
            else
            {
                List<TItem>? itemsData = DataSource != null ? DataSource.ToList() : new List<TItem>();
                foreach (TItem itemData in itemsData)
                {
                    IEnumerable<TItem>? childItems = DropDownTreeFields?.Child != null ? (IEnumerable<TItem>)DataUtil.GetObject(DropDownTreeFields.Child.ToString(), itemData) : null;
                    if (childItems?.Any() == true)
                    {
                        UpdateHierarchicalParents(childItems.ToList(), itemData, result!);
                    }
                }
            }
        }

        private void UpdateHierarchicalParents(List<TItem> childItems, TItem treeData, List<string> result)
        {
            string? checkedParent = Accessor.GetValue(treeData, DropDownTreeFields?.ID)?.ToString();
            if (checkedParent != null && !result.Contains(checkedParent))
            {
                int checkCount = 0;
                foreach (TItem childItem in childItems)
                {
                    string? checkedChild = Accessor.GetValue(childItem, DropDownTreeFields?.ID)?.ToString();
                    IEnumerable<TItem>? child = AllData.GetValueOrDefault(checkedChild ?? string.Empty)?.Child;
                    List<TItem>? childData = child?.ToList();
                    if (childData != null && childData.Count > 0)
                    {
                        UpdateHierarchicalParents(childData, childItem, result);
                    }
                    if (checkedChild != null && result.Contains(checkedChild))
                    {
                        checkCount++;
                    }
                }
                if (checkCount == childItems.Count && !result.Contains(checkedParent))
                {
                    result.Add(checkedParent);
                }
            }
        }

        private void GetAutoCheckId(string? tempCurrentValue, List<string> result)
        {
            GetChild(new() { tempCurrentValue! }, result);
            List<TItem>? tempDataSource = DataSource?.ToList();
            if (dataType == TreeViewDataType.SelfReferential && tempDataSource!= null)
            {
                TItem? item = tempDataSource.Find(data => Accessor.GetValue(data, DropDownTreeFields.ID)?.ToString() == tempCurrentValue);
                if (item != null)
                {
                    string? parentId = Accessor.GetValue(item, DropDownTreeFields.ParentID)?.ToString();
                    while (!string.IsNullOrEmpty(parentId))
                    {
                        result.Add(parentId);
                        item = tempDataSource.Find(data => Accessor.GetValue(data, DropDownTreeFields.ID)?.ToString() == parentId);
                        parentId = Accessor.GetValue(item!, DropDownTreeFields.ParentID)?.ToString();
                    }
                }
            }
            else
            {
                GetHierarchicalParents(tempCurrentValue, tempDataSource, result);
            }

        }

        private TItem? GetHierarchicalParents(string? id, List<TItem>? dataSource, List<string> result)
        {
            TItem? nodeData = default;
            if (dataSource != null)
            {
                foreach (TItem item in dataSource)
                {
                    string? nodeId = Accessor.GetValue(item, DropDownTreeFields.ID)?.ToString();
                    if (string.Equals(nodeId, id, StringComparison.Ordinal))
                    {
                        return item;
                    }
                    List<TItem>? child = (List<TItem>)DataUtil.GetObject(DropDownTreeFields.Child, item);
                    if (child != null)
                    {
                        nodeData = GetHierarchicalParents(id, child, result);
                        if (nodeData != null)
                        {
                            result.Add(nodeId!);
                            break;
                        }
                    }
                }
            }
            return nodeData;
        }

        private void GetChild(List<string> tempCurrentValue, List<string> result)
        {
            foreach (string item in tempCurrentValue)
            {
                List<string> tempChild = new();
                AllData.GetValueOrDefault(item ?? string.Empty)?.Child?.ForEach(x =>
                {
                    tempChild.Add(Accessor.GetValue(x, DropDownTreeFields.ID)?.ToString()!);
                });
                result.AddRange(tempChild);
                if (tempChild.Count > 0)
                {
                    GetChild(tempChild, result);
                }
            }
        }

        private async Task SetMultiSelect(bool IsDynamicChange = false, bool isFromSelectAll = false)
        {
            if (ShowCheckBox && !IsDynamicChange)
            {
                await SetMultiSelectValue(checkedNodes?.ToArray()).ConfigureAwait(true);
            }
            else
            {
                List<string>? tempCurrentValue = currentValue?.Select(item => (string)SfBaseUtils.ChangeType(item, typeof(string)))?.ToList();
                string[]? ddtValue = AllowMultiSelection ? (ShowCheckBox ? checkedNodes : selectedNodes) : (currentValue != null ? (ShowCheckBox ? tempCurrentValue?.ToArray() : new string[] { currentValue.FirstOrDefault()?.ToString()! }) : null);
                if (ddtValue != null)
                {
                    await SetMultiSelectValue(ddtValue).ConfigureAwait(true);
                }
                if (ShowCheckBox && currentValue != null && tempCurrentValue != null)
                {
                    if (showPopupTree && TreeObj != null)
                        try
                        {
                            isInternalSync = true;
                            await TreeObj.CheckAllAsync(tempCurrentValue.ToArray()).ConfigureAwait(true);
                        }
                        finally
                        {
                            isInternalSync = false;
                        }
                    else
                        checkedNodes = tempCurrentValue.ToArray();
                }
            }
            await UpdateSelectedValues(true, isFromSelectAll).ConfigureAwait(true);
        }

        private async Task SetMultiSelectValue(string[] newValues)
        {
            if (!isFilteredData)
            {
                List<TValue>? tempValues = newValues?.Select(item => (TValue)SfBaseUtils.ChangeType(item, typeof(TValue)))?.ToList();
                currentValue = (tempValues?.Count == 0 ? currentValue : tempValues) ?? new List<TValue>();
                await UpdateTwoWayBinding().ConfigureAwait(true);
                List<string>? tempCurrentValue = currentValue?.Select(item => (string)SfBaseUtils.ChangeType(item, typeof(string)))?.ToList();
                if (newValues != null && newValues.Length != 0 && !ShowCheckBox && tempCurrentValue != null)
                {
                    selectedNodes = tempCurrentValue.ToArray();
                }
            }
            else
            {
                List<TValue>? tempNewValue = newValues?.Select(item => (TValue)SfBaseUtils.ChangeType(item, typeof(TValue)))?.ToList();
                List<TValue> selectedValues = currentValue ?? new List<TValue>();
                if (tempNewValue != null)
                {
                    foreach (TValue item in tempNewValue)
                    {
                        if (currentValue == null || !currentValue.Contains(item))
                        {
                            selectedValues.Add(item);
                        }
                    }
                    currentValue = selectedValues;
                }
            }
        }

        private async Task UpdateSelectedValues(bool setChipWrapper = false, bool isFromSelectAll = false)
        {
            try
            {
                setChipWrapper = ValueTemplate != null ? true : setChipWrapper;
                if (currentValue != null && currentValue.Count != 0)
                {
                    string temp;
                    string textValue = string.Empty;
                    List<string> selectedText = new();
                    if (ShowCheckBox && ShowSelectAll)
                        isSelectAllChecked = checkedNodes != null && overAllLiItems == checkedNodes.Length;
                    if (!isChipDelete || AutoUpdateCheckState)
                    {
                        chipItems.Clear();
                    }
                    if (!isFilteredData)
                    {
                        selectedData.Clear();
                    }
                    for (int i = 0; i < currentValue.Count; i++)
                    {
                        string selectedNodeText = GetSelectedData(currentValue[i]?.ToString()!);
                        selectedText.Add(selectedNodeText);
                        temp = selectedText[selectedText.Count - 1];
                        textValue += selectedText.Count > 1 ? DelimiterChar + SPACE + temp : temp;
                        if (Mode != DdtVisualMode.Delimiter && (!isChipDelete || AutoUpdateCheckState) && (AllowMultiSelection || ShowCheckBox))
                        {
                            chipItems.Add(new ChipItems() { Text = temp, Value = currentValue[i]?.ToString()! });
                        }
                    }
                    if (InputBaseObj != null)
                    {
                        await InputBaseObj.SetValue(textValue, FloatLabelType, ShowClearButton).ConfigureAwait(true);
                    }
                    Text = string.IsNullOrEmpty(textValue) ? null! : textValue;
                }
                else
                {
                    chipItems.Clear();
                    if (InputBaseObj != null)
                    {
                        await InputBaseObj.SetValue(null, FloatLabelType, ShowClearButton).ConfigureAwait(true);
                    }
                    Text = null!;
                }
                if (isFromSelectAll)
                    await Task.Yield();
                await InvokeMethod("sfBlazor.DropDownTree.updateSelectedValue", new object[] { dataId, currentValue!, setChipWrapper }).ConfigureAwait(true);
            }
            catch
            {
                if (!IsDisposed)
                    throw;
            }
        }

        private string GetSelectedData(string value)
        {
            string? text = string.Empty;
            if (isFilteredData)
            {
                text = selectedData.GetValueOrDefault(value)!;
            }
            if (string.IsNullOrEmpty(text))
            {
                TreeData<TItem>? data = AllData.GetValueOrDefault(value ?? string.Empty);
                if (data != null)
                {
                    text = data.Text;
                    selectedData.Add(value!, text!);
                }
            }
            return text!;
        }

        private async Task UpdateValue(List<TValue>? value)
        {
            if (value == null || value.Count == 0)
            {
                await ResetValue(true).ConfigureAwait(true);
            }
            else
            {
                await SetTreeValue(true).ConfigureAwait(true);
            }
        }

        private string GetPopupContentClass()
        {
            string popupContentClass = "e-popup-content e-dropdown";
            if ((DataSource != null && !DataSource.Any() && DropDownTreeFields?.DataManager == null) || actionFailure || (filteredData != null && filteredData.Count == 0))
            {
                popupContentClass += SPACE + NODATA;
            }
            return popupContentClass;
        }

        private void RemoveSelectedData(string value)
        {
            if (currentValue != null)
            {
                List<TValue> tempList = new(currentValue);
                tempList.Remove((TValue)SfBaseUtils.ChangeType(value, typeof(TValue)));
                currentValue = tempList.ToList();
            }
            selectedData.Remove(value);
        }

        private Dictionary<string, object> GetInstance()
        {
            Dictionary<string, object> treeObj = new()
            {
                { "disabled", Disabled },
                { "allowMultiSelection", AllowMultiSelection },
                { "popupWidth", PopupWidth },
                { "zIndex", ZIndex },
                { "popupHeight", PopupHeight },
                { "showCheckBox", ShowCheckBox },
                { "showSelectAll", ShowSelectAll },
                { "showClearButton", ShowClearButton },
                { "value", currentValue },
                { "mode", Mode },
                { "allowFiltering", AllowFiltering },
                { "delimiterChar", DelimiterChar },
                { "textWrap", TextWrap }
            };
            return treeObj;
        }

        private void OnFailure(Exception e)
        {
            if (OnActionFailure.HasDelegate)
                OnActionFailure.InvokeAsync(new FailureEventArgs() { Error = e, Name = "OnActionFailure" });
            actionFailure = true;
        }

        private string SerializeModel()
        {
            return JsonSerializer.Serialize(Value);
        }

        private async Task UpdatePersistence()
        {
            if (EnablePersistence && IsRendered)
            {
                await SetLocalStorage(ID, SerializeModel()).ConfigureAwait(true);
            }
        }

        private async Task SetLocalStorage(string persistId, string dataValue)
        {
            await InvokeMethod("window.localStorage.setItem", new object[] { persistId, dataValue }).ConfigureAwait(true);
        }

        private List<TItem> GetTreeData(string? id = null)
        {
            List<TItem>? treeData = new();
            if (id != null)
            {
                TItem? item = default;
                List<TItem>? dataList = DataSource?.ToList();
                if (dataType == TreeViewDataType.SelfReferential)
                {
                    item = dataList!.Find(data => Accessor.GetValue(data, DropDownTreeFields.ID)?.ToString() == id);
                }
                else
                {
                    item = GetHierarchicalData(dataList, id);
                }
                if (item == null) { return new List<TItem>() { }; }
                else
                {
                    TItem tempItem = (TItem)CloneUtils.Clone(item, typeof(TItem));
                    UpdateData(new() { tempItem });
                    treeData.Add(tempItem);
                }
            }
            else
            {
                treeData = DataSource?.ToList();
                UpdateData(treeData);
            }
            return treeData!;
        }

        private TItem? GetHierarchicalData(List<TItem>? dataSource, string? id)
        {
            if (dataSource == null || dataSource.Count == 0)
                return default;
            TItem? newData = default;
            foreach (TItem data in dataSource)
            {
                string? nodeId = DataUtil.GetObject(DropDownTreeFields.ID, data)?.ToString();
                if (!string.IsNullOrEmpty(nodeId) && string.Equals(nodeId, id, StringComparison.Ordinal))
                {
                    return data;
                }
                else
                {
                    List<TItem>? childData = (List<TItem>)DataUtil.GetObject(DropDownTreeFields.Child, data);
                    if (childData != null)
                    {
                        newData = GetHierarchicalData(childData, id);
                        if (newData != null)
                            break;
                    }
                }
            }
            return newData;
        }

        private void UpdateData(List<TItem>? data)
        {
            DropDownTreeField<TItem> field = DropDownTreeFields;
            if (data != null)
            {
                foreach (TItem treeData in data)
                {
                    string? idValue = Accessor.GetValue(treeData, field.ID.ToString()).ToString();
                    if (field.Expanded != null && treeData != null)
                    {
                        bool isExpanded = expandedNodes != null && expandedNodes.Contains(idValue);
                        treeData.GetType().GetProperty(field.Expanded)?.SetValue(treeData, isExpanded);
                    }
                    if (field.IsChecked != null && treeData != null)
                    {
                        bool isChecked = checkedNodes != null && checkedNodes.Contains(idValue);
                        treeData.GetType().GetProperty(field.IsChecked)?.SetValue(treeData, isChecked);
                    }
                    if (field.Selected != null && treeData != null)
                    {
                        bool isSelected = selectedNodes != null && selectedNodes.Contains(idValue);
                        treeData.GetType().GetProperty(field.Selected)?.SetValue(treeData, isSelected);
                    }
                    if (field.Child != null)
                    {
                        List<TItem>? children = (DataUtil.GetObject(field.Child.ToString(), treeData) as IEnumerable<TItem>)?.ToList();
                        if (children != null)
                        {
                            UpdateData(children);
                        }
                    }
                }
            }
        }

        private Dictionary<string, object> GetContainerAttributes()
        {
            Dictionary<string, object> attr = new() { { TABINDEX, 0 } };
            if (Disabled)
                attr.Add("aria-disabled", "true");
            return attr;
        }

        /// <summary>
        /// This method updates the child properties of Dropdown Tree.
        /// </summary>
        /// <param name="details">Specifies the property value parameter.</param>
        public async Task UpdateChildProperties(object details)
        {
            try
            {
                DropDownTreeFields = (DropDownTreeField<TItem>)details;
                DataSource = DropDownTreeFields?.DataSource;
                if (DropDownTreeFields?.isDataSourceUpdated == true)
                {
                    DropDownTreeFields.isDataSourceUpdated = false;
                    AllData.Clear();
                    UpdateAllData(DataSource?.ToList(), expandedNodes?.ToList() ?? new(), checkedNodes?.ToList() ?? new(), selectedNodes?.ToList() ?? new());
                    overAllLiItems = AllData.Count;
                    await UpdateValue(Value).ConfigureAwait(true);
                    await SetTreeText(false).ConfigureAwait(true);
                }
            }
            catch
            {
                if (!IsDisposed)
                    throw;
            }
        }

        /// <exclude/>
        /// <summary>
        /// Update the IsPopupOpen state from client side.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [JSInvokable]
        public void UpdatePopupState(bool state)
        {
            if (state)
            {
                isPopupOpen = true;
            }
            else
            {
                isPopupOpen = false;
                isClearButtonClick = false;
            }
        }

        /// <exclude/>
        /// <summary>
        /// Invokes the popup event.
        /// </summary>
        /// <param name="popupArgs">Popup event args</param>
        /// <returns>Task.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [JSInvokable]
        public async Task InvokePopupEvent(PopupModel? popupArgs)
        {
            if (popupArgs == null)
            {
                await OnContainerClick(null).ConfigureAwait(true);
                return;
            }
            PopupEventArgs args = new() { Popup = popupArgs, Cancel = false };
            if (OnPopupClose.HasDelegate && InputBaseObj != null)
            {
                popupArgs.RelateTo = InputBaseObj.ContainerElement;
                await OnPopupClose.InvokeAsync(args).ConfigureAwait(true);
            }
            if (!args.Cancel)
            {
                await InvokeMethod("sfBlazor.DropDownTree.closePopup", new object[] { dataId }).ConfigureAwait(true);
                showPopupTree = false;
                if (TreeObj != null)
                    TreeObj.isDdtFiltering = false;
                await InvokeAsync(() => StateHasChanged()).ConfigureAwait(true);
            }
        }

        /// <exclude/>
        /// <summary>
        /// Perform the select all actions.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [JSInvokable]
        public async Task OnSelectAllClick()
        {
            DdtChangeEventArgs<TValue> changeEventArgs = new() { Action = isSelectAllChecked ? DdtAction.Unselect : DdtAction.Select, Cancel = false, IsInteracted = true, PreviousValue = previousValue! };
            await ValueChanging.InvokeAsync(changeEventArgs).ConfigureAwait(true);
            if (changeEventArgs.Cancel) return;
            if (!isSelectAllChecked && TreeObj != null)
            {
                isSelectAllChecked = true;
                await TreeObj.CheckAllAsync().ConfigureAwait(true);
            }
            else if (TreeObj != null)
            {
                isSelectAllChecked = false;
                await TreeObj.UncheckAllAsync().ConfigureAwait(true);
            }
            await SetMultiSelect(false, true).ConfigureAwait(true);
        }

        /// <exclude/>
        /// <summary>
        /// This method remove the chip.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [JSInvokable]
        public async Task RemoveChip(string id = "")
        {
            DdtChangeEventArgs<TValue> changeEventArgs = new() { Action = DdtAction.Unselect, Cancel = false, IsInteracted = true, PreviousValue = previousValue!, CurrentValue = (!string.IsNullOrEmpty(id)) ? (TValue)SfBaseUtils.ChangeType(id, typeof(TValue)) : default! };
            await ValueChanging.InvokeAsync(changeEventArgs).ConfigureAwait(true);
            if (changeEventArgs.Cancel) return;
            if (Disabled)
            {
                return;
            }
            isChipDelete = true;
            isClearButtonClick = true;
            isInternalChange = true;
            List<string> removeID = new() { id };
            if (AutoUpdateCheckState)
                GetAutoCheckId(id, removeID);
            removeID.ForEach((nodeID) =>
            {
                RemoveSelectedData(nodeID);
                ChipItems? item = chipItems.Find((data) => data.Value == nodeID);
                chipItems.Remove(item!);
            });
            List<string>? tempValue = currentValue?.Select(item => (string)SfBaseUtils.ChangeType(item, typeof(string)))?.ToList();
            if (AllowMultiSelection && tempValue != null)
            {
                selectedNodes = tempValue.ToArray();
                if (showPopupTree && TreeObj != null)
                    TreeObj.AllSelectedNodes = tempValue.ToHashSet();
                await UpdateSelectedValues(true).ConfigureAwait(true);
            }
            if (ShowCheckBox && tempValue != null)
            {
                if (showPopupTree && TreeObj != null)
                    await TreeObj.UncheckAllAsync(new string[] { id }).ConfigureAwait(true);
                else
                    checkedNodes = tempValue.ToArray();
                await SetMultiSelect(false, true).ConfigureAwait(true);
            }
            isChipDelete = false;
            isInternalChange = false;
        }
    }
}

