using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Internal;
using Syncfusion.Blazor.Navigations.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syncfusion.Blazor.Navigations
{
    /// <inheritdoc/>
    public partial class SfTreeView<TValue> : SfBaseComponent, ITreeView
    {

        /// <inheritdoc/>
        protected override async Task OnInitializedAsync()
        {
            ScriptModules = SfScriptModules.SfTreeView;
            if (EnablePersistence && string.IsNullOrEmpty(ID))
                throw new InvalidOperationException($"The {nameof(ID)} property of TreeView must not be null or empty when using EnablePersistence.");
            SetRootAttributes();
            await base.OnInitializedAsync().ConfigureAwait(true);
            IsDevice = await SyncfusionService.IsDevice().ConfigureAwait(true);
            EnableRtl = (EnableRtl || (SyncfusionService != null && SyncfusionService.options.EnableRtl));
            UpdateAnimationProperties(AnimationSettings);
            treeSortOrder = SortOrder;
            treeAllowDragAndDrop = AllowDragAndDrop;
            treeAllowEditing = AllowEditing;
            treeAllowTextWrap = AllowTextWrap;
            treeDropArea = DropArea;
            treeAllowMultiSelection = AllowMultiSelection;
            treeShowCheckBox = ShowCheckBox;
            treeEnableRtl = EnableRtl;
            treeAutoCheck = AutoCheck;
            treeCssClass = CssClass;
            treeDisabled = Disabled;
            treeFullRowSelect = FullRowSelect;
            treeExpandOn = ExpandOn;
            treeSelectedNodes = SelectedNodes;
            treeExpandedNodes = ExpandedNodes;
            treeCheckedNodes = CheckedNodes;
            if (SelectedNodes?.Length > 0)
            {
                CurrentSelectedNodes = SelectedNodes.ToList();
            }
        }

        /// <inheritdoc/>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender).ConfigureAwait(true);
            if (firstRender)
            {
                TreeViewFieldChild<TValue>? treeViewFieldChild = TreeViewFields?.Children as TreeViewFieldChild<TValue>;
                UpdateChildProperties(TREEVIEWFIELD, TreeViewFields);
                if (TreeViewFields?.DataSource != null && ListReference != null)
                {
                    InternalData = ListGeneration<TValue>.GetSortedData(TreeViewFields.DataSource.ToList(), SortOrder.ToString(), TreeViewFields.Text, this.SortComparer);
                    if (InternalData != null && ListReference.DataSource != null && (ListReference.DataSource.ToHashSet()?.SetEquals(InternalData.ToHashSet()) == false))
                    {
                        ListReference.ListData = InternalData;
                        ListReference.DataSource = InternalData;
                        ListReference.ItemsData = InternalData;
                        await ListReference.IdentifyDataSource().ConfigureAwait(true);
                        ListReference.ListUpdated();
                    }
                }
                ActionEventArgs args = new ActionEventArgs() { Name = "Created" };
                if (TreeViewEventAggregator != null)
                    await TreeViewEventAggregator.NotifyAsync("Created", args).ConfigureAwait(true);
                if (TreeViewEvents != null && TreeViewEvents.Created.HasDelegate)
                {
                    if (TreeViewEvents.Created.HasDelegate)
                        await TreeViewEvents.Created.InvokeAsync(args).ConfigureAwait(true);
                }

                if (ListReference != null && ListReference.DataType == TreeViewDataType.RemoteData && !IsCompletelyRendered)
                {
                    IsCompletelyRendered = true;
                    IsDataSourceChanged = true;
                }
            }
            else if (AllowTextWrap && IsDataSourceChanged)
            {
                await InvokeMethod(UPDATETEXTWRAP, new object[] { dataId }).ConfigureAwait(true);
                IsDataSourceChanged = false;
            }
            else if (isTreeNodeExpandingCall && ExpandedNodesChanged.HasDelegate)
            {
                await InvokeMethod(UPDATESPINNERCLASS, new object[] { dataId }).ConfigureAwait(true);
                SpinnerRef?.HideAsync();
                if (SpinnerRef != null)
                {
                    SpinnerRef.Dispose();
                }
                isTreeNodeExpandingCall = false;
            }
        }

        /// <inheritdoc/>
        internal override async Task OnAfterScriptRendered()
        {
            try
            {
                if (ListReference != null && ListReference.DataType != TreeViewDataType.RemoteData)
                {
                    IsCompletelyRendered = true;
                    if (!isCurrentExpandedUpdated && !SfBaseUtils.Equals(CurrentExpandedNodes, ExpandedNodes?.ToList()))
                    {
                        isCurrentExpandedUpdated = true;
                        if (EnablePersistence)
                        {
                            TreePersistenceValues localStorageValue = await InvokeMethod<TreePersistenceValues>(GETITEM, true, new object[] { ID }).ConfigureAwait(true);
                            if (localStorageValue == null)
                            {
                                await ListReference.SetLocalStorage(ID, ListReference.SerializeModel()).ConfigureAwait(true);
                            }
                            else
                            {
                                InternalExpandedNodes = localStorageValue.ExpandedNodes;
                            }
                        }
                        else if (!EnableVirtualization)
                        {
                            InternalExpandedNodes = CurrentExpandedNodes;
                        }

                        await UpdateExpandedNodes().ConfigureAwait(true);
                        ListReference?.ListUpdated();
                    }
                }

                await InvokeMethod(INITIALIZE, new object[] { dataId, element, GetInstance(), DotnetObjectReference }).ConfigureAwait(true);
                if (ExpandedNodes != null)
                {
                    AllExpandedNodes = ExpandedNodes.ToHashSet();
                }
            }
            catch
            {
                if (!IsDisposed)
                    throw;
            }
        }

        /// <inheritdoc/>
        protected override bool ShouldRender()
        {
            bool tmp = shouldRender;
            shouldRender = true;
            return tmp;
        }

        /// <inheritdoc/>
        public void PreventRender(bool preventRender = true) => shouldRender = !preventRender;

        /// <inheritdoc/>
        protected override async Task OnParametersSetAsync()
        {
            try
            {
                await base.OnParametersSetAsync().ConfigureAwait(true);
                Dictionary<string, object> changedProperties = new Dictionary<string, object>();
                if (treeAllowMultiSelection != AllowMultiSelection)
                {
                    treeAllowMultiSelection = AllowMultiSelection;
                    changedProperties.Add(nameof(AllowMultiSelection), AllowMultiSelection);
                }
                if (treeShowCheckBox != ShowCheckBox)
                {
                    treeShowCheckBox = ShowCheckBox;
                    changedProperties.Add(nameof(ShowCheckBox), ShowCheckBox);
                }
                if (treeEnableRtl != EnableRtl)
                {
                    treeEnableRtl = EnableRtl;
                    changedProperties.Add(nameof(EnableRtl), EnableRtl);
                }
                if (treeAutoCheck != AutoCheck)
                {
                    treeAutoCheck = AutoCheck;
                    changedProperties.Add(nameof(AutoCheck), AutoCheck);
                }
                if (treeDisabled != Disabled)
                {
                    treeDisabled = Disabled;
                    changedProperties.Add(nameof(Disabled), Disabled);
                }
                if (treeAllowDragAndDrop != AllowDragAndDrop)
                {
                    treeAllowDragAndDrop = AllowDragAndDrop;
                    changedProperties.Add(nameof(AllowDragAndDrop), AllowDragAndDrop);
                }
                if (treeAllowEditing != AllowEditing)
                {
                    treeAllowEditing = AllowEditing;
                    changedProperties.Add(nameof(AllowEditing), AllowEditing);
                }
                if (treeAllowTextWrap != AllowTextWrap)
                {
                    treeAllowTextWrap = AllowTextWrap;
                    changedProperties.Add(nameof(AllowTextWrap), AllowTextWrap);
                }
                if (treeFullRowSelect != FullRowSelect)
                {
                    treeFullRowSelect = FullRowSelect;
                    changedProperties.Add(nameof(FullRowSelect), FullRowSelect);
                }
                if (!string.Equals(treeCssClass, CssClass, StringComparison.Ordinal))
                {
                    treeCssClass = CssClass;
                    changedProperties.Add(nameof(CssClass), CssClass);
                }
                if (!string.Equals(treeDropArea, DropArea, StringComparison.Ordinal))
                {
                    treeDropArea = DropArea;
                    changedProperties.Add(nameof(DropArea), DropArea);
                }
                if (!SfBaseUtils.Equals(treeSortOrder, SortOrder))
                {
                    treeSortOrder = SortOrder;
                    changedProperties.Add(nameof(SortOrder), SortOrder);
                }
                if (!SfBaseUtils.Equals(treeExpandOn, ExpandOn))
                {
                    treeExpandOn = ExpandOn;
                    changedProperties.Add(nameof(ExpandOn), ExpandOn);
                }
                if (!SfBaseUtils.Equals(treeExpandedNodes, ExpandedNodes))
                {
                    treeExpandedNodes = ExpandedNodes;
                    changedProperties.Add(nameof(ExpandedNodes), ExpandedNodes);
                }
                if (!SfBaseUtils.Equals(treeSelectedNodes, SelectedNodes))
                {
                    treeSelectedNodes = SelectedNodes;
                    changedProperties.Add(nameof(SelectedNodes), SelectedNodes);
                }
                if (!SfBaseUtils.Equals(treeCheckedNodes, CheckedNodes))
                {
                    treeCheckedNodes = CheckedNodes;
                    changedProperties.Add(nameof(CheckedNodes), CheckedNodes);
                }
                if (changedProperties.Count > 0)
                {
                    await OnPropertyChangeHandler(changedProperties).ConfigureAwait(true);
                }
            }
            catch
            {
                if (!IsDisposed)
                    throw;
            }
        }
    }
}