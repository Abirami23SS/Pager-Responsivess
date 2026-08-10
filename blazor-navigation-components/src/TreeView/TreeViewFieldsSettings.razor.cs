using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    ///  A class used for configuring the TreeView fields setting properties.
    /// </summary>
    /// <typeparam name="TValue">"Specifies the TValue parameter".</typeparam>
    public partial class TreeViewFieldsSettings<TValue> : TreeViewFieldOptions<TValue>
    {
        [CascadingParameter]
        private SfTreeView<TValue>? Parent { get; set; }

        /// <summary>
        /// Specifies the child content.
        /// </summary>
        /// <exclude/>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Invokes when data source changes.
        /// </summary>
        [Parameter]
        public EventCallback<IEnumerable<TValue>> DataSourceChanged { get; set; }

        private Query? TreeQuery { get; set; }

        /// <summary>
        /// Method invoked when the component is ready to start.
        /// </summary>
        /// <returns>"Task".</returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(true);
            Parent?.UpdateChildProperties("fields", this);
            Dictionary<string, string> properties = new Dictionary<string, string>
            {
                { nameof(Child), Child },
                { nameof(Expanded), Expanded },
                { nameof(HasChildren), HasChildren },
                { nameof(HtmlAttributes), HtmlAttributes },
                { nameof(IconCss), IconCss },
                { nameof(Id), Id },
                { nameof(ImageUrl), ImageUrl },
                { nameof(IsChecked), IsChecked },
                { nameof(ParentID), ParentID },
                { nameof(Selected), Selected },
                { nameof(TableName), TableName },
                { nameof(Text), Text },
                { nameof(Tooltip), Tooltip },
                { nameof(NavigateUrl), NavigateUrl }
            };
            foreach (var property in properties)
            {
                if (property.Value?.Length == 0)
                    throw new InvalidOperationException($"{property.Key} of TreeView cannot be empty.");
            }
        }

        /// <summary>
        /// Method invoked when any changes in component state occurs.
        /// </summary>
        /// <returns>"Task".</returns>
        protected override async Task OnParametersSetAsync()
        {
            if (Parent == null)
            {
                return;
            }
            try
            {
                TreeQuery = Parent.NotifyPropertyChanges(nameof(Query), Query, TreeQuery);
                if (DataSource != null && FieldDataSource != null)
                {
                    if (DataSource?.ToHashSet()?.SetEquals(FieldDataSource.ToHashSet()) == false || DataSource?.SequenceEqual(FieldDataSource) == false)
                    {
                        Parent.PropertyChanges.TryAdd(nameof(DataSource), DataSource);
                        FieldDataSource = DataSource.ToList();
                    }
                }
                Parent.IsDataSourceChanged = Parent.PropertyChanges.ContainsKey(nameof(DataSource)) ? true : false;
                if (!Parent.IsDataSourceChanged && !Parent.PropertyChanges.ContainsKey(nameof(Query)) && (Parent.CheckedNodesChanged.HasDelegate || Parent.ExpandedNodesChanged.HasDelegate || Parent.SelectedNodesChanged.HasDelegate))
                {
                    return;
                }

                if (Parent.ListReference != null)
                {
                    if (Parent.ListReference.DataType == Internal.TreeViewDataType.Hierarchical)
                        Parent.ListReference.HierarchicalChilds.Clear();
                    if (Parent.PropertyChanges.ContainsKey(nameof(Query)))
                    {
                        Parent.ListReference.UpdateFields(0);
                        await Parent.ListReference.GetDataManagerData().ConfigureAwait(true);
                    }
                    if (IsDisposed) { return; }
                    Parent.UpdateChildProperties("fields", this);
                    List<TValue>? datasource = Parent.TreeViewFields != null && Parent.TreeViewFields.DataSource != null
                        ? ((Parent.IsNumberTypeId || !Parent.IsDataSourceChanged || Parent.IsNodeDropped || Parent.ListReference.IsRefreshNode || Parent.AllowDragAndDrop) && !Parent.PropertyChanges.ContainsKey(nameof(DataSource))) ? Parent?.ListReference?.DataSource?.ToList() : Parent.TreeViewFields.DataSource.ToList()
                        : null;
                    if (!Parent.IsClearStateCall && Parent.ExpandedNodes?.Length != 0 && datasource?.Count > Parent.InternalData?.Count || (datasource != null && Parent.CurrentExpandedNodes?.Count == 0))
                    {
                        Parent.ClearExpandedNode();
                    }
                    Parent.ListReference.IsSelfChildsUpdate = Parent.PropertyChanges.ContainsKey(nameof(DataSource));
                    if (Parent.ListReference.IsSelfChildsUpdate)
                    {
                        Parent.ListReference.SelfChilds.Clear();
                    }
                    if (Parent.isDdtFiltering)
                        Parent.tempDataSource = datasource.ToList();
                    await Parent.UpdateData(datasource, Parent.PropertyChanges.ContainsKey(nameof(DataSource))).ConfigureAwait(true);
                }
            }
            catch
            {
                if (!IsDisposed)
                    throw;
            }
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
