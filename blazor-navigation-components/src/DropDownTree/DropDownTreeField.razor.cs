using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Internal;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    ///  A class used for configuring the Dropdown Tree fields setting properties.
    /// </summary>
    /// <typeparam name="TItem">Specifies the type of <see cref="DropDownTreeField{TItem}"/>.</typeparam>
    public partial class DropDownTreeField<TItem> : SfOwningComponentBase
    {
        [CascadingParameter]
        private IDropDownTree? Parent { get; set; }

        /// <exclude/>
        /// <summary>
        /// Specifies the child content.
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Gets or sets the data source for rendering the DropDownTree component. The data source value can be of any type that implements IEnumerable.
        /// </summary>
        /// <value>
        /// The value can be any IEnumerable list of data. The default value is <c>null</c>.
        /// </value>
        [Parameter]
        public IEnumerable<TItem> DataSource { get; set; }

        private IEnumerable<TItem>? DdtFieldDataSource { get; set; }

        /// <summary>
        /// The DataManager class provides functionality for performing data operations in applications.
        /// It serves as an abstraction layer for working with remote data sources.
        /// </summary>
        /// <value>
        /// Map the remote data details for the component using this property.
        /// </value>
        public DataManager DataManager { get; set; }

        /// <summary>
        ///  Gets or sets the query to select particular data from the dataSource.
        /// </summary>
        /// <value>
        /// The set of data that must be queried from the entire data source. The default value is <c>null</c>
        /// </value>
        [Parameter]
        public Query Query { get; set; }

        /// <summary>
        /// Gets or sets the string value that represents the name of the child data source holding a list of objects.
        /// </summary>
        /// <value>
        /// The value denotes the name of child data in assigned data source. The default value is <c>null</c>.
        /// </value>
        /// <example> 
        /// <code><![CDATA[
        /// @using Syncfusion.Blazor.DropDowns
        /// <SfDropDownTree TItem="string" TValue="TreeItem">
        ///    <DropDownTreeField TItem="string" DataSource="TreeDataSource" Id="NodeId" Text="NodeText" Expanded="Expanded" Child="@("Child")"></DropDownTreeField>
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
        [Parameter]
        public string Child { get; set; }

        /// <summary>
        /// Gets or sets the mapping field for the expand state of the tree node in the popup.
        /// </summary>
        /// <value>
        ///  <c>true</c> if the node must be expanded during initial rendering. The default value is <c>false</c>
        /// </value>
        [Parameter]
        public string Expanded { get; set; } = nameof(Expanded);

        /// <summary>
        /// Gets or sets the mapping field for determining whether a node has child nodes or not.
        /// </summary>
        /// <value>
        /// <c>true</c> if the node contains child nodes. The default value is <c>false</c>
        /// </value>
        [Parameter]
        public string HasChildren { get; set; } = nameof(HasChildren);

        /// <summary>
        /// Gets or sets the mapping field for adding custom HTML attributes to the tree node in the popup.
        /// </summary>
        /// <value>
        /// Specifies the additional attribute to be added for the required tree nodes.
        /// </value>
        /// <example>
        /// <code><![CDATA[
        /// @using Syncfusion.Blazor.Navigations
        ///
        /// <SfDropDownTree TItem="EmployeeData" TValue="string" Width="100%" CssClass="custom" Placeholder="Select an employee" PopupHeight="250px">
        ///     <DropDownTreeField TItem="EmployeeData" DataSource="Data" Id="Id" Text="Name" HasChildren="HasChild" ParentID="PId" HtmlAttributes="htmlAttribute"></DropDownTreeField>
        /// </SfDropDownTree>
        ///
        /// @code {
        ///     public class EmployeeData
        ///     {
        ///         public string Id { get; set; }
        ///         public string Name { get; set; }
        ///         public string Job { get; set; }
        ///         public string Image { get; set; }
        ///         public bool HasChild { get; set; }
        ///         public bool Expanded { get; set; }
        ///         public string Status { get; set; }
        ///         public string PId { get; set; }
        ///         public Dictionary<string, object> htmlAttribute { get; set; }
        ///     }
        ///     
        ///     List<EmployeeData> Data = new List<EmployeeData>
        ///     {
        ///         new EmployeeData() {Id="1", Name = "Steven Buchanan",  Job = "General Manager", Image= "10",HasChild=true,Expanded=true,Status="busy",htmlAttribute=new Dictionary<string, object>() { {"style", "background-color: yellow;"},   } },
        ///         new EmployeeData() {Id="2",PId="1", Name = "Laura Callahan",  Job = "Product Manager", Image= "2",HasChild=true,Status="online" }
        ///     };
        /// }
        ///
        /// ]]></code>  
        /// </example>
        [Parameter]
        public string HtmlAttributes { get; set; } = nameof(HtmlAttributes);

        /// <summary>
        /// Gets or sets the mapping field for the icon class of each tree node, which will be added before the node's text.
        /// </summary>
        /// <value>
        /// Specifies the CSS class names to render icons for tree nodes.
        /// </value>
        [Parameter]
        public string IconCss { get; set; } = nameof(IconCss);

        /// <summary>
        /// Gets or sets the Id field mapped in the dataSource.
        /// </summary>
        /// <value>
        /// Specifies the id field of tree node. The default value is <c>null</c>
        /// </value>
        [Parameter]
        public string ID { get; set; } = nameof(ID);

        /// <summary>
        /// Gets or sets the mapping field for the image URL of each tree node, where the image will be added before the node's text in the popup.
        /// </summary>
        /// <value>
        /// Specifies the url for the image that must be loaded in the required tree node.
        /// </value>
        [Parameter]
        public string ImageUrl { get; set; } = nameof(ImageUrl);

        /// <summary>
        /// Gets or sets the field for the checked state of the tree node in the popup.
        /// </summary>
        /// <value>
        /// The checked state of tree node during initial rendering. The default value is <c>false</c>
        /// </value>
        [Parameter]
        public string IsChecked { get; set; }

        /// <summary>
        /// Gets or sets the parent ID field mapped in the dataSource.
        /// </summary>
        /// <value>
        /// The parent ID of the corresponding node to which the node must be mapped as children. The default value is <c>null</c>
        /// </value>
        [Parameter]
        public string ParentID { get; set; }

        /// <summary>
        /// Gets or sets the mapping field for the selected state of the tree node.
        /// </summary>
        /// <value>
        /// Specifies the selected state of node during initial rendering. The default value is <c>false</c>
        /// </value>
        [Parameter]
        public string Selected { get; set; }

        /// <summary>
        /// Gets or sets the table name used to fetch data from a specific table in the server.
        /// </summary>
        /// <value>
        /// The table name to fetch data.
        /// </value>
        [Parameter]
        public string TableName { get; set; } = nameof(TableName);

        /// <summary>
        /// Gets or sets the mapping field for the text displayed as the tree node's display text.
        /// </summary>
        /// <value>
        /// The text to be displayed in tree node.
        /// </value>
        [Parameter]
        public string Text { get; set; } = nameof(Text);

        /// <summary>
        /// Gets or sets the mapping field for the tooltip that will be displayed as hovering text of the tree node.
        /// </summary>
        /// <value>
        /// The tooltip that must be shown during node hover.
        /// </value>
        [Parameter]
        public string Tooltip { get; set; } = nameof(Tooltip);

        internal bool isDataSourceUpdated;

        /// <inheritdoc/>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(true);
            DdtFieldDataSource = DataSource;
            Dictionary<string, string> properties = new Dictionary<string, string>
            {
                { nameof(Child), Child },
                { nameof(Expanded), Expanded },
                { nameof(HasChildren), HasChildren },
                { nameof(HtmlAttributes), HtmlAttributes },
                { nameof(IconCss), IconCss },
                { nameof(ID), ID },
                { nameof(ImageUrl), ImageUrl },
                { nameof(IsChecked), IsChecked },
                { nameof(ParentID), ParentID },
                { nameof(Selected), Selected },
                { nameof(TableName), TableName },
                { nameof(Text), Text },
                { nameof(Tooltip), Tooltip }
            };
            foreach (var property in properties)
            {
                if (property.Value?.Length == 0)
                    throw new InvalidOperationException($"{property.Key} of Dropdown Tree cannot be empty.");
            }
            if (Parent != null)
            {
                await Parent.UpdateChildProperties(this).ConfigureAwait(true);
            }
        }

        /// <inheritdoc/>
        protected override async Task OnParametersSetAsync()
        {
            try
            {
                await base.OnParametersSetAsync().ConfigureAwait(true);
                if (!SfBaseUtils.Equals(DdtFieldDataSource, DataSource))
                {
                    isDataSourceUpdated = true;
                    DdtFieldDataSource = DataSource;
                    if (Parent != null)
                    {
                        await Parent.UpdateChildProperties(this).ConfigureAwait(true);
                    }
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
                Tooltip = null!;
                Query = null!;
                DataManager = null!;
            }
        }
    }
}
