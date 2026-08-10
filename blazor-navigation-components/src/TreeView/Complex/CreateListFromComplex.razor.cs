using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Syncfusion.Blazor.Navigations.Internal
{
    /// <summary>
    /// Specifies the ComplexCreateList.
    /// </summary>
    /// <typeparam name="TValue">"TypeParam".</typeparam>
    public partial class CreateListFromComplex<TValue> : SfOwningComponentBase
    {
        [CascadingParameter]
        internal SfTreeView<TValue>? TreeParent { get; set; }

        /// <summary>
        /// Specifies the datasource of list element.
        /// </summary>
        [Parameter]
        public IEnumerable<TValue> ListData { get; set; }

        /// <summary>
        /// Specifies the TreeOption field values.
        /// </summary>
        [Parameter]
        public TreeOptions<TValue> TreeOptions { get; set; }

        /// <summary>
        /// Specifies the tree node level of TreeView nodes.
        /// </summary>
        [Parameter]
        public int TreeNodeLevel { get; set; } = 1;

        /// <summary>
        /// Specifies the TreeView list base option model.
        /// </summary>
        [Parameter]
        public ListModel ListModel { get; set; }

        private Dictionary<string, object> fieldProp = new Dictionary<string, object>();
        private FieldsMapping fieldsMap = new FieldsMapping();

        /// <inheritdoc/>
        protected override void OnInitialized()
        {
            base.OnInitialized();
            ListModel = new ListModel { Fields = new FieldsMapping() };
            if (TreeParent?.TreeViewFields != null)
            {
                MapSettings(ListModel, false);
            }
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                TreeOptions = null!;
            }
        }

        // Return Ul element attribute details.
        private Dictionary<string, object> GetAttributes()
        {
            string parentUl = "e-list-parent e-ul";
            if (TreeOptions != null)
            {
                parentUl = SfBaseUtils.AddClass(parentUl, TreeOptions.IsLoaded ? string.Empty : "e-display-none");
                parentUl = SfBaseUtils.AddClass(parentUl, TreeOptions.IsExpanded ? string.Empty : "e-display-none");
            }
            return new ()
            {
                { "class", parentUl },
                { "role", "group" }
            };
        }

        // Maps the user given field values with the listBase fields.
        private FieldsValueMapping<List<TValue>> GetMappedData(TValue fieldData)
        {
            try
            {
                return fieldProp.Count > 0 ? new FieldsValueMapping<List<TValue>>
                {
                    Child = fieldProp["Child"] != null ? ((IEnumerable<TValue>)DataUtil.GetObject(fieldProp["Child"].ToString(), fieldData))?.ToList() : null,
                    HtmlAttributes = fieldProp["HtmlAttributes"] != null ? (Dictionary<string, object>)DataUtil.GetObject(fieldProp["HtmlAttributes"].ToString(), fieldData) : null,
                    IconCss = fieldProp["IconCss"] != null ? (string)DataUtil.GetObject(fieldProp["IconCss"].ToString(), fieldData) : null,
                    Id = DataUtil.GetObject(fieldProp["Id"].ToString(), fieldData)?.ToString(),
                    Text = (string)DataUtil.GetObject(fieldProp["Text"].ToString(), fieldData),
                    Tooltip = fieldProp["Tooltip"] != null ? (string)DataUtil.GetObject(fieldProp["Tooltip"]?.ToString(), fieldData) : null,
                    HasChildren = fieldProp["HasChildren"] != null ? Convert.ToBoolean(DataUtil.GetObject(fieldProp["HasChildren"]?.ToString(), fieldData), CultureInfo.InvariantCulture) : false,
                    Expanded = (bool)(fieldProp["Expanded"] != null ? (DataUtil.GetObject(fieldProp["Expanded"]?.ToString(), fieldData) ?? false) : false),
                    ImageUrl = fieldProp["ImageUrl"] != null ? (string)DataUtil.GetObject(fieldProp["ImageUrl"]?.ToString(), fieldData) : null,
                    Url = fieldProp["Url"] != null ? (string)DataUtil.GetObject(fieldProp["Url"]?.ToString(), fieldData) : null
                }
                : new FieldsValueMapping<List<TValue>>();
            }
            catch (NullReferenceException e)
            {
                throw new InvalidCastException("Invalid mapping in List field settings. Please provide valid fields mapping for your Datasource.", e);
            }
        }

        /// <summary>
        /// Returns TreeItemCreatedArgs for a list item for which TreeItemCreating event invoked.
        /// </summary>
        /// <param name="item">"Specifies the item".</param>
        /// <param name="nodeLevel">"Specifies the nodeLevel".</param>
        /// <returns>"Task".</returns>
        private TreeItemCreatedArgs<TValue> InvokeTreeItemCreating(TValue item, int nodeLevel = 1)
        {
            TreeItemCreatedArgs<TValue> treeItemCreatingArgs = new TreeItemCreatedArgs<TValue> { ItemData = item, TreeOptions = new TreeOptions<TValue>(), NodeLevel = nodeLevel, Options = ListModel };
            try
            {
                TreeParent?.ListReference.BeforeNodeCreate(treeItemCreatingArgs);
            }
            catch (Exception e)
            {
                throw new InvalidCastException("Exception", e);
            }

            return treeItemCreatingArgs;
        }

        /// <summary>
        /// Maps the default setting for the list generated.
        /// </summary>
        /// <param name="options">"Specifies the options field".</param>
        /// <param name="isField">"Specifies the isField attribute".</param>
        private void MapSettings(ListModel options, bool isField)
        {
            TreeViewFieldsSettings<TValue>? parentField = TreeParent?.TreeViewFields;
            if (isField)
                fieldsMap = ListBasePropertyMapper<FieldsMapping>.PropertyMapper(options.Fields, fieldsMap)[1];
            fieldProp.Clear();
            if (parentField != null)
            {
                fieldProp = new Dictionary<string, object>()
                {
                    { nameof(parentField.Child), isField ? fieldsMap.Child : parentField.Child },
                    { nameof(parentField.Expanded), isField ? fieldsMap.Expanded : parentField.Expanded },
                    { nameof(parentField.HasChildren), isField ? fieldsMap.HasChildren : parentField.HasChildren },
                    { nameof(parentField.HtmlAttributes), isField ? fieldsMap.HtmlAttributes : parentField.HtmlAttributes },
                    { nameof(parentField.ImageUrl), isField ? fieldsMap.ImageUrl : parentField.ImageUrl },
                    { nameof(parentField.IconCss), isField ? fieldsMap.IconCss : parentField.IconCss },
                    { nameof(parentField.Id), isField ? fieldsMap.Id : parentField.Id },
                    { nameof(parentField.IsChecked), isField ? fieldsMap.IsChecked : parentField.IsChecked },
                    { nameof(parentField.Selected), parentField.Selected ?? fieldsMap.Selected },
                    { nameof(parentField.Text), isField ? fieldsMap.Text : parentField.Text },
                    { nameof(parentField.Tooltip), isField ? fieldsMap.Tooltip : parentField.Tooltip },
                    { nameof(fieldsMap.Url), isField ? fieldsMap.Url : parentField.NavigateUrl }
                };
            }
        }

        /// <summary>
        /// ListBase Property.
        /// </summary>
        /// <typeparam name="T">"T".</typeparam>
        private static class ListBasePropertyMapper<T>
        {
            /// <summary>
            /// Specifies the property mapper.
            /// </summary>
            /// <param name="customizedProp">"Specifies the customized prop".</param>
            /// <param name="mappedProp">"Specifies the mapped property".</param>
            /// <returns>"Task".</returns>
            internal static List<T> PropertyMapper(T customizedProp, T mappedProp)
            {
                PropertyInfo[] properties = typeof(T).GetProperties();
                Type? customizedType = customizedProp?.GetType();
                foreach (PropertyInfo property in properties)
                {
                    PropertyInfo? mappedProperty = customizedType?.GetProperty(property.Name);
                    if (mappedProperty != null)
                    {
                        object? mappedPropVal = mappedProperty.GetValue(customizedProp);
                        if (mappedPropVal != null)
                        {
                            mappedProperty.SetValue(mappedProp, mappedPropVal);
                        }
                    }
                }

                return new List<T> { customizedProp, mappedProp };
            }
        }
    }
}