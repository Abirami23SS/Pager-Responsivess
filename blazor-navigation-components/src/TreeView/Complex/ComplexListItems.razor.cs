using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Internal;
using System.Collections.Generic;
using System.Globalization;

namespace Syncfusion.Blazor.Navigations.Internal
{
    /// <summary>
    /// Specifies the complex list items.
    /// </summary>
    /// <typeparam name="TValue">"TypeParam".</typeparam>
    public partial class ComplexListItems<TValue> : SfOwningComponentBase
    {
        [CascadingParameter]
        internal SfTreeView<TValue> TreeParent { get; set; }

        /// <summary>
        /// Specifies the TreeOption field values.
        /// </summary>
        [Parameter]
        public TreeOptions<TValue> TreeOptions { get; set; }

        /// <summary>
        /// Specifies the Treeview mapped data values.
        /// </summary>
        [Parameter]
        public FieldsValueMapping<List<TValue>> MappedData { get; set; }

        /// <summary>
        /// Specifies the index position of Treeview node.
        /// </summary>
        [Parameter]
        public int Index { get; set; }

        /// <summary>
        /// Specifies the TreeView list base option model.
        /// </summary>
        [Parameter]
        public ListModel ListModel { get; set; }

        /// <summary>
        /// Specifies the datasource of list element.
        /// </summary>
        [Parameter]
        public TValue ListData { get; set; }

        /// <summary>
        /// Specifies the tree node level of TreeView nodes.
        /// </summary>
        [Parameter]
        public int TreeNodeLevel { get; set; } = 1;

        private const string LISTITEM = "e-list-item";
        private const string NAVIGABLE = "e-navigable";
        private const string ACTIVE = "e-active";
        private const string COMPLEXLISTITEMSHASCHILD = "e-has-child";
        private const string DISABLE = "e-disable";
        private const string NODELEVEL = "e-level-";
        private const string SPINNERCLASS = " e-icons-spinner";
        private const string NODEEDITED = "sfBlazor.TreeView.nodeEdited";
        private const string UPDATETEXTWRAP = "sfBlazor.TreeView.updateTextWrap";
        private const string SETFOCUS = "sfBlazor.TreeView.setFocus";
        private const string IDPREFIX = "sftreeview-";
        private const string STYLE = "data-sf-style";

        private string nodeId = string.Empty;

        // Returns the list item classes for the list.
        private string GetListItemClass()
        {
            List<string> classNames = new List<string>
            {
                LISTITEM,
                NODELEVEL + TreeNodeLevel.ToString(CultureInfo.CurrentCulture)
            };

            if (MappedData.HtmlAttributes != null && MappedData.HtmlAttributes.TryGetValue("class", out object? value) && value != null)
            {
                classNames.Add(value.ToString()!);
            }

            if (TreeOptions != null)
            {
                classNames.Remove(ACTIVE);

                if (TreeOptions.IsSelected)
                {
                    classNames.Add(ACTIVE);
                }

                if (TreeParent.FullRowNavigable)
                {
                    classNames.Add(NAVIGABLE);
                }

                if (TreeOptions.ChildData != null)
                {
                    classNames.Add(COMPLEXLISTITEMSHASCHILD);
                }

                if (TreeOptions.IsDisabled)
                {
                    classNames.Add(DISABLE);
                }
            }

            return string.Join(" ", classNames);
        }

        // Returns the list item attributes for the list.
        private Dictionary<string, object> GetAttributes()
        {
            nodeId = MappedData.Id ?? (SfBaseUtils.GenerateID(IDPREFIX) + "-" + Index);
            string FirstElementId = string.Empty;
            if (TreeParent != null && TreeParent.EnableVirtualization && TreeParent.InternalData != null && TreeParent.TreeViewFields != null)
            {
                FirstElementId = TreeParent.ListReference.GetAttrValue(TreeParent.TreeViewFields.Id, TreeParent.InternalData[0]);
            }
            Dictionary<string, object> attributes = new Dictionary<string, object>()
            {
                { "class", GetListItemClass() },
                { "role", "treeitem" },
                { "data-uid", nodeId }
            };

            if (TreeParent != null && !TreeParent.IsDevice)
                attributes.Add("tabindex", FirstElementId == MappedData.Id ? "0" : "-1");

            if (TreeOptions != null)
            {
                attributes["aria-level"] = TreeNodeLevel.ToString(CultureInfo.CurrentCulture);
                if (TreeOptions.ChildData != null || MappedData.HasChildren || MappedData.Child?.Count > 0)
                {
                    attributes["aria-expanded"] = TreeOptions.IsExpanded ? "true" : "false";
                }
                attributes["aria-selected"] = TreeOptions.IsSelected ? "true" : "false";
            }

            if (MappedData.HtmlAttributes != null)
            {
                Dictionary<string, object> nonClassAttributes = new (MappedData.HtmlAttributes);
                nonClassAttributes.Remove("class");
                foreach (var pair in nonClassAttributes)
                {
                    bool isDataSfStyle = pair.Key.Equals(STYLE, System.StringComparison.OrdinalIgnoreCase);
                    if (isDataSfStyle || pair.Key.Equals("style", System.StringComparison.OrdinalIgnoreCase))
                    {
                        if (!attributes.ContainsKey(STYLE))
                        {
                            SfBaseUtils.UpdateDictionary(STYLE, pair.Value, attributes);
                        }
                        else if (isDataSfStyle)
                        {
                            attributes[STYLE] = pair.Value;
                        }
                    }
                    else
                    {
                        attributes[pair.Key] = pair.Value;
                    }
                }
            }

            if (MappedData.Tooltip != null)
            {
                attributes["title"] = MappedData.Tooltip;
            }

            return attributes;
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                if (TreeOptions?.ChildData != null)
                {
                    TreeOptions.ChildData = null!;
                }
                TreeOptions = null!;
                MappedData = null!;
                ListModel = null!;
            }
        }
    }
}