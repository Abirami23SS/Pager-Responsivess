using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    ///  A class used for configuring the TreeView fields setting properties.
    /// </summary>
    /// <typeparam name="TValue">"Specifies the TValue parameter".</typeparam>
    public partial class TreeViewFieldOptions<TValue> : SfOwningComponentBase
    {
        /// <summary>
        /// Gets or sets the string value that represents the name of the child data source holding a list of objects.
        /// </summary>
        /// <example> 
        /// <code><![CDATA[ 
        /// <SfTreeView TValue="MailItem" >
        ///     <TreeViewFieldsSettings TValue="MailItem" Child="SubFolders" DataSource="@MyFolder" Text="FolderName"></TreeViewFieldsSettings>
        /// </SfTreeView>
        /// @code{ 
        ///     public class MailItem
        ///     {
        ///        public string FolderName { get; set; }
        ///        public List<MailItem> SubFolders { get; set; }
        ///     }
        ///     List<MailItem> MyFolder = new List<MailItem>();
        ///     List<MailItem> Folder1 = new List<MailItem>();
        ///     MyFolder.Add(new MailItem
        ///     {
        ///         SubFolders = Folder1
        ///     });
        /// ]]></code> 
        /// </example>
        [Parameter]
        public string Child { get; set; }

        /// <summary>
        /// Gets or sets the data source for rendering the TreeView component. The data source value can be of any type that implements IEnumerable.
        /// </summary>
        [Parameter]
        public IEnumerable<TValue> DataSource { get; set; }

        /// <summary>
        /// Gets or sets the mapping field for the expand state of the TreeView node.
        /// </summary>
        [Parameter]
        public string Expanded { get; set; } = nameof(Expanded);

        /// <summary>
        /// Gets or sets the mapping field for determining whether a node has child nodes or not.
        /// </summary>
        [Parameter]
        public string HasChildren { get; set; } = nameof(HasChildren);

        /// <summary>
        /// Gets or sets the mapping field for adding custom HTML attributes to the TreeView node.
        /// </summary>
        /// <example> 
        /// <code><![CDATA[ 
        /// <SfTreeView TValue="MailItem" >
        ///     <TreeViewFieldsSettings TValue="MailItem" Id="Id" HtmlAttributes="HtmlAttributes"></TreeViewFieldsSettings>
        /// </SfTreeView>
        /// @code{ 
        ///     Dictionary<string, object> HtmlAttributes = new Dictionary<string, object>() 
        ///    { 
        ///        { "class", "treeview" } 
        ///    }; 
        /// ]]></code> 
        /// </example>
        [Parameter]
        public string HtmlAttributes { get; set; } = nameof(HtmlAttributes);

        /// <summary>
        /// The DataManager class provides functionality for performing data operations in applications. 
        /// It serves as an abstraction layer for working with remote data sources.
        /// </summary>
        public DataManager DataManager { get; set; }

        /// <summary>
        /// Gets or sets the mapping field for the icon class of each TreeView node, which will be added before the node's text.
        /// </summary>
        [Parameter]
        public string IconCss { get; set; } = nameof(IconCss);

        /// <summary>
        /// Gets or sets the Id field mapped in the dataSource.
        /// </summary>
        [Parameter]
        public string Id { get; set; } = nameof(Id);

        /// <summary>
        /// Gets or sets the mapping field for the image URL of each TreeView node, where the image will be added before the node's text.
        /// </summary>
        [Parameter]
        public string ImageUrl { get; set; } = nameof(ImageUrl);

        /// <summary>
        /// Gets or sets the field for the checked state of the TreeView node.
        /// </summary>
        [Parameter]
        public string IsChecked { get; set; }

        /// <summary>
        /// Gets or sets the mapping field for navigateUrl to add it as the hyperlink of the TreeView node.
        /// </summary>
        [Parameter]
        public string NavigateUrl { get; set; } = nameof(NavigateUrl);

        /// <summary>
        /// Gets or sets the parent ID field mapped in the dataSource.
        /// </summary>
        [Parameter]
        public string ParentID { get; set; }

        /// <summary>
        ///  Gets or sets the query to select particular data from the dataSource.
        /// </summary>
        /// <example> 
        /// <code><![CDATA[ 
        /// <SfTreeView TValue="TreeData">
        ///     <TreeViewFieldsSettings TValue="TreeData" Query="@Query" Id="EmployeeID" Text="FirstName" >
        ///         <SfDataManager Url="http://services.odata.org/V4/Northwind/Northwind.svc" Adaptor="@Syncfusion.Blazor.Adaptors.ODataV4Adaptor" CrossDomain="true">
        ///         </SfDataManager>
        ///     </TreeViewFieldsSettings>
        /// </SfTreeView>
        /// @code{ 
        ///      public Query Query = new Query().From("Employees").Select(new List<string> { "EmployeeID", "FirstName" }).Take(5).RequiresCount();
        /// }
        /// ]]></code> 
        /// </example>
        [Parameter]
        public Query Query { get; set; }

        /// <summary>
        /// Gets or sets the mapping field for the selected state of the TreeView node.
        /// </summary>
        [Parameter]
        public string Selected { get; set; }

        /// <summary>
        /// Gets or sets the table name used to fetch data from a specific table in the server.
        /// </summary>
        [Parameter]
        public string TableName { get; set; } = nameof(TableName);

        /// <summary>
        /// Gets or sets the mapping field for the text displayed as the TreeView node's display text.
        /// </summary>
        [Parameter]
        public string Text { get; set; } = nameof(Text);

        /// <summary>
        /// Gets or sets the mapping field for the tooltip that will be displayed as hovering text of the TreeView node.
        /// </summary>
        [Parameter]
        public string Tooltip { get; set; } = nameof(Tooltip);

        /// <exclude/>
        /// <summary>
        /// Gets or sets the child nodes data as object type.
        /// </summary>
        public object Children { get; set; }

        internal IEnumerable<TValue>? FieldDataSource { get; set; }

        /// <summary>
        /// Updates the child property.
        /// </summary>
        /// <param name="prop">"The argument that specifies the text of the child".</param>
        /// <param name="details">"The argument that specifies the details of the child node".</param>
        internal void UpdateChildProperties(string prop, object details)
        {
            if (!string.IsNullOrEmpty(prop))
            {
                Children = details;
            }
        }

        /// <inheritdoc/>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(true);
            FieldDataSource = DataSource?.ToList();
            UpdateChildProperties("child", Child);
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                Tooltip = null!;
                Children = null!;
                Query = null!;
                DataManager = null!;
            }
        }
    }
}
