using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Navigations.Internal;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Syncfusion.Blazor.Navigations
{
    public partial class SfTreeView<TValue> : ITreeView
    {
        /// <exclude/>
        /// <summary>
        /// Gets or sets the content to display within a TreeView component.
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="SfTreeView{TValue}"/> allows nodes to be reordered using drag and drop.
        /// </summary>
        /// <value>
        /// <c>true</c> if drag and drop is allowed; otherwise, <c>false</c>. The default value is <c>false</c>.
        /// </value>
        /// <remarks>
        /// On desktop devices, nodes can be reordered by clicking and dragging them to the target position and releasing the mouse.
        /// On touch devices, nodes can be reordered using touch start, touch move, and touch end events.
        /// </remarks>
        [Parameter]
        public bool AllowDragAndDrop { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether node text can be edited by pressing the F2 key or double-clicking a <see cref="SfTreeView{TValue}"/> node.
        /// </summary>
        /// <value>
        /// <c>true</c> if editing is allowed; otherwise, <c>false</c>. The default value is <c>false</c>.
        /// </value>
        /// <remarks>
        /// When <c>AllowEditing</c> is set to <c>true</c>, the TreeView allows editing a node by double-clicking it or pressing the F2 key while the node is selected.  
        /// When set to <c>false</c>, the TreeView allows the node text to be read-only.
        /// </remarks>
        [Parameter]
        public bool AllowEditing { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether multi-selection of nodes is enabled in the <see cref="SfTreeView{TValue}"/> component.
        /// </summary>
        /// <value>
        /// <c>true</c> if multi-selection is enabled; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// When multi-selection is enabled, users can select multiple nodes in the <see cref="SfTreeView{TValue}"/> component
        /// by holding down the <c>CTRL</c> key and clicking on the nodes they want to select. Consecutive nodes can be selected
        /// by holding down the <c>SHIFT</c> key and clicking on the initial and final nodes of the range to be selected.
        /// The <see cref="ShowCheckBox"/> property can also be used to enable checkbox support for node selection.
        /// </remarks>
        [Parameter]
        public bool AllowMultiSelection { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether the text of nodes in the <see cref="SfTreeView{TValue}"/> component is allowed to wrap to the next line when it exceeds the width of the node.
        /// </summary>
        /// <value>
        /// <c>true</c> if text wrapping is enabled; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// When the <c>AllowTextWrap</c> property is set to <c>true</c>, the text of nodes in the <see cref="SfTreeView{TValue}"/> component
        /// will wrap to the next line if its length exceeds the width of the node. This can be useful for displaying long
        /// or multi-line text in the tree view without truncating or clipping it.
        /// </remarks>
        [Parameter]
        public bool AllowTextWrap { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the checked state of parent nodes in the <see cref="SfTreeView{TValue}"/> component  
        /// is automatically updated based on the checked state of their child nodes.
        /// </summary>
        /// <value>
        /// <c>true</c> if the automatic update of parent node checked states is enabled; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// When the <c>AutoCheck</c> property is set to <c>true</c>, the checked state of parent nodes in the <see cref="SfTreeView{TValue}"/> component
        /// will be automatically updated based on the checked state of their child nodes. This can be useful for maintaining the
        /// consistency of the tree view's checkbox hierarchy and ensuring that parent nodes are only checked if all of their child
        /// nodes are also checked. This property only works when the <see cref="ShowCheckBox"/> property is set to <c>true</c>.
        /// </remarks>
        [Parameter]
        public bool AutoCheck { get; set; } = true;

        /// <summary>
        /// Gets or sets the IDs of the nodes that are checked in the <see cref="SfTreeView{TValue}"/> component.
        /// </summary>
        /// <value>
        /// A collection of strings containing the IDs of the nodes that are checked in the tree view.
        /// </value>
        /// <remarks>
        /// The <c>CheckedNodes</c> property supports two-way binding in the <see cref="SfTreeView{TValue}"/> component. 
        /// By passing a collection of node IDs to this property, the checked state of the specified nodes can be set.
        /// The property can also be used to retrieve the IDs of the nodes that are currently checked in the tree view.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// <SfTreeView TValue="MusicAlbum" ShowCheckBox="true" @bind-CheckedNodes="@CheckedNodes">
        ///     <TreeViewFieldsSettings TValue="MusicAlbum" Id="Id" DataSource="@Albums" Text="Name" ParentID="ParentId" 
        ///         HasChildren="HasChild" Expanded="Expanded" IsChecked="IsChecked">
        ///     </TreeViewFieldsSettings>
        /// </SfTreeView>
        ///
        /// @code{
        ///     string[] CheckedNodes = new string[] { "16", "18" };
        /// }
        /// ]]></code>
        /// </example>
        [Parameter]
        public string[] CheckedNodes { get; set; }

        /// <summary>
        /// Triggers an event callback when the checked state of the node's checkbox has changed in the <see cref="SfTreeView{TValue}"/> component.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        [Parameter]
        public EventCallback<string[]> CheckedNodesChanged { get; set; }

        /// <summary>
        /// Gets or sets one or more CSS classes that customize the appearance of the <see cref="SfTreeView{TValue}"/> component.
        /// </summary>
        /// <value>
        /// A string containing one or more CSS class names separated by spaces. The default value is `String.Empty`.
        /// </value>
        /// <remarks>
        /// When one or more CSS classes are set on the <c>CssClass</c> property, the corresponding styles will be applied to the UI elements of the <see cref="SfTreeView{TValue}"/> component.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// <SfTreeView TValue="MusicAlbum" CssClass="e-custom e-tree">
        ///     <TreeViewFieldsSettings TValue="MusicAlbum" Id="Id" DataSource="@Albums" Text="Name" ParentID="ParentId" HasChildren="HasChild" Expanded="Expanded" IsChecked="IsChecked"></TreeViewFieldsSettings>
        /// </SfTreeView>
        ///
        /// @code{
        ///     public class MusicAlbum
        ///     {
        ///         public int Id { get; set; }
        ///         public int? ParentId { get; set; }
        ///         public string Name { get; set; }
        ///         public bool Expanded { get; set; }
        ///         public bool? IsChecked { get; set; }
        ///         public bool HasChild { get; set; }
        ///     }
        ///
        ///     List<MusicAlbum> Albums = new List<MusicAlbum>();
        ///
        ///     protected override void OnInitialized()
        ///     {
        ///         base.OnInitialized();
        ///         Albums.Add(new MusicAlbum
        ///         {
        ///             Id = 14,
        ///             HasChild = true,
        ///             Name = "MP3 Albums",
        ///             Expanded = true
        ///         });
        ///         Albums.Add(new MusicAlbum
        ///         {
        ///             Id = 15,
        ///             ParentId = 14,
        ///             Name = "Rock"
        ///         });
        ///         Albums.Add(new MusicAlbum
        ///         {
        ///             Id = 16,
        ///             ParentId = 14,
        ///             Name = "Gospel"
        ///         });
        ///         Albums.Add(new MusicAlbum
        ///         {
        ///             Id = 17,
        ///             ParentId = 14,
        ///             Name = "Latin Music"
        ///         });
        ///         Albums.Add(new MusicAlbum
        ///         {
        ///             Id = 18,
        ///             ParentId = 14,
        ///             Name = "Jazz"
        ///         });
        ///     }
        /// }
        ///
        /// <style>
        ///     .e-custom .e-tree {
        ///         padding: 10px 0;
        ///         font-weight: 800;
        ///     }
        ///     .e-custom.e-treeview .e-fullrow {
        ///         height: 200px;
        ///     }
        /// </style>
        /// ]]></code>
        /// </example>
        [Parameter]
        public string CssClass { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="SfTreeView{TValue}"/> component is enabled or disabled.
        /// </summary>
        /// <value> 
        /// <c>true</c> if the TreeView component is disabled; otherwise, <c>false</c>. The default value is <c>false</c>. 
        /// </value> 
        /// <remarks> 
        /// When set to <c>true</c>, user interaction with the TreeView component, including all its nodes and associated elements (such as checkboxes and icons), will be prevented.  
        /// 
        /// For example, to disable the <see cref="SfTreeView{TValue}"/> component when a button is clicked, you can use the following code:
        /// 
        /// <code><![CDATA[
        /// @code {
        ///     bool isTreeViewDisabled = false;
        ///
        ///     void ToggleTreeViewDisabled()
        ///     {
        ///         isTreeViewDisabled = !isTreeViewDisabled;
        ///     }
        /// }
        ///
        /// <SfTreeView TValue="MusicAlbum" Disabled="@isTreeViewDisabled">
        ///     ...
        /// </SfTreeView>
        ///
        /// <button @onclick="ToggleTreeViewDisabled">Toggle disabled state</button>
        /// ]]></code>
        /// </remarks>
        [Parameter]
        public bool Disabled { get; set; }

        /// <summary>
        /// Gets or sets the target element where the draggable node can be moved and dropped.
        /// </summary>
        /// <value>
        /// A string representing the ID or CSS class of the target element.
        /// </value>
        /// <remarks> 
        /// By default, the draggable element can be moved within the TreeView component. To specify a different drop target,
        /// set the <c>DropArea</c> property to the ID or CSS class of the element that should act as the drop target.  
        /// </remarks> 
        /// <example>
        /// <code><![CDATA[
        /// <div class="treeParent">
        ///     <SfTreeView TValue="MusicAlbum" DropArea=".treeParent">
        ///         <TreeViewFieldsSettings TValue="MusicAlbum" Id="Id" DataSource="@Albums" 
        ///             Text="Name" ParentID="ParentId" HasChildren="HasChild" 
        ///             Expanded="Expanded" IsChecked="IsChecked">
        ///         </TreeViewFieldsSettings>
        ///     </SfTreeView>
        /// </div>
        ///
        /// @code {
        ///     public class MusicAlbum
        ///     {
        ///         public int Id { get; set; }
        ///         public int? ParentId { get; set; }
        ///         public string Name { get; set; }
        ///         public bool Expanded { get; set; }
        ///         public bool? IsChecked { get; set; }
        ///         public bool HasChild { get; set; }
        ///     }
        ///
        ///     List<MusicAlbum> Albums = new List<MusicAlbum>();
        ///
        ///     protected override void OnInitialized()
        ///     {
        ///         base.OnInitialized();
        ///         Albums.Add(new MusicAlbum
        ///         {
        ///             Id = 14,
        ///             HasChild = true,
        ///             Name = "MP3 Albums",
        ///             Expanded = true
        ///         });
        ///
        ///         Albums.Add(new MusicAlbum
        ///         {
        ///             Id = 15,
        ///             ParentId = 14,
        ///             Name = "Rock"
        ///         });
        ///     }
        /// }
        /// ]]></code>
        /// </example>
        [Parameter]
        public string DropArea { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to persist the state of the <see cref="SfTreeView{TValue}"/> component between page reloads.
        /// </summary>
        /// <value>
        /// <c>true</c> if state persistence is enabled; otherwise, <c>false</c>. The default value is <c>false</c>.
        /// </value>
        /// <remarks>
        /// When state persistence is enabled, the following properties will be stored in the browser's local storage to retain the state of the component after a page reload:
        /// <list type="number">
        ///     <item>
        ///         <term><see cref="SelectedNodes"/></term>
        ///         <description>The nodes that are selected in the TreeView component.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="CheckedNodes"/></term>
        ///         <description>The nodes that are checked in the TreeView component.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="ExpandedNodes"/></term>
        ///         <description>The nodes that are expanded in the TreeView component.</description>
        ///     </item>
        /// </list>
        /// </remarks>
        [Parameter]
        public bool EnablePersistence { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether right-to-left (RTL) rendering is enabled for the <see cref="SfTreeView{TValue}"/> component.
        /// </summary>
        /// <value>
        /// <c>true</c> if the right-to-left direction is enabled for the <see cref="SfTreeView{TValue}"/> component; otherwise, <c>false</c>. The default value is <c>false</c>.
        /// </value>
        /// <remarks>
        /// When set to <c>true</c>, the layout of the <see cref="SfTreeView{TValue}"/> component will be rendered from right-to-left.
        /// </remarks>
        [Parameter]
        public bool EnableRtl { get; set; }


        /// <summary>
        /// Gets or sets the action that triggers the expand or collapse operation for nodes in the <see cref="SfTreeView{TValue}"/> component.
        /// </summary>
        /// <value>
        /// The default value is <see cref="ExpandAction.DoubleClick"/>.
        /// </value>
        /// <remarks>
        /// The available actions are:
        /// - <see cref="ExpandAction.Click"/> – The expand/collapse operation happens when you single-click on the node on desktop.
        /// - <see cref="ExpandAction.DoubleClick"/> – The expand/collapse operation happens when you double-click on the node on desktop.
        /// - <see cref="ExpandAction.None"/> – The expand/collapse operation is disabled.
        /// In mobile devices, the node expand/collapse action always happens on a single tap.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// <SfTreeView TValue="MusicAlbum" ExpandOn="ExpandAction.Click">
        ///     <TreeViewFieldsSettings TValue="MusicAlbum" 
        ///         Id="Id" 
        ///         DataSource="@Albums" 
        ///         Text="Name" 
        ///         ParentID="ParentId" 
        ///         HasChildren="HasChild" 
        ///         Expanded="Expanded" 
        ///         IsChecked="IsChecked">
        ///     </TreeViewFieldsSettings>
        /// </SfTreeView>
        ///
        /// @code{
        ///    public class MusicAlbum
        ///    {
        ///        public int Id { get; set; }
        ///        public int? ParentId { get; set; }
        ///        public string Name { get; set; }
        ///        public bool Expanded { get; set; }
        ///        public bool? IsChecked { get; set; }
        ///        public bool HasChild { get; set; }
        ///    }
        ///
        ///    SfTreeView<MusicAlbum> tree;
        ///    List<MusicAlbum> Albums = new List<MusicAlbum>();
        ///
        ///    protected override void OnInitialized()
        ///    {
        ///        base.OnInitialized();
        ///        Albums.Add(new MusicAlbum
        ///        {
        ///            Id = 14,
        ///            HasChild = true,
        ///            Name = "MP3 Albums",
        ///            Expanded = true
        ///        });
        ///        Albums.Add(new MusicAlbum
        ///        {
        ///            Id = 15,
        ///            ParentId = 14,
        ///            Name = "Rock"
        ///        });
        ///        Albums.Add(new MusicAlbum
        ///        {
        ///            Id = 16,
        ///            ParentId = 14,
        ///            Name = "Gospel"
        ///        });
        ///        Albums.Add(new MusicAlbum
        ///        {
        ///            Id = 17,
        ///            ParentId = 14,
        ///            Name = "Latin Music"
        ///        });
        ///        Albums.Add(new MusicAlbum
        ///        {
        ///            Id = 18,
        ///            ParentId = 14,
        ///            Name = "Jazz"
        ///        });
        ///    }
        /// }
        /// ]]></code>
        /// </example>
        [Parameter]
        public ExpandAction ExpandOn { get; set; } = ExpandAction.DoubleClick;

        /// <summary>
        /// Gets or sets the IDs of expanded nodes in the <see cref="SfTreeView{TValue}"/> component.
        /// </summary>
        /// <value>
        /// Pass the node's ID as a string array collection.
        /// </value>
        /// <remarks>
        /// This property is used to track which nodes are currently expanded in the TreeView,
        /// allowing users to retain their expanded view even after the application is closed and reopened.
        /// This property supports two-way binding, ensuring that any changes to the expanded nodes in the TreeView
        /// will be reflected in the bound string array collection.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// <SfTreeView TValue="MusicAlbum" ShowCheckBox="true" @bind-ExpandedNodes="@ExpandedNodes">
        ///     <TreeViewFieldsSettings TValue="MusicAlbum" 
        ///         Id="Id" 
        ///         DataSource="@Albums" 
        ///         Text="Name" 
        ///         ParentID="ParentId" 
        ///         HasChildren="HasChild" 
        ///         Expanded="Expanded" 
        ///         IsChecked="IsChecked">
        ///     </TreeViewFieldsSettings>
        /// </SfTreeView>
        ///
        /// @code {
        ///     public class MusicAlbum
        ///     {
        ///         public int Id { get; set; }
        ///         public int? ParentId { get; set; }
        ///         public string Name { get; set; }
        ///         public bool Expanded { get; set; }
        ///         public bool? IsChecked { get; set; }
        ///         public bool HasChild { get; set; }
        ///     }
        ///
        ///     string[] ExpandedNodes = new string[] { "16", "18" };
        ///
        ///     List<MusicAlbum> Albums = new List<MusicAlbum>();
        ///
        ///     protected override void OnInitialized()
        ///     {
        ///         base.OnInitialized();
        ///         Albums.Add(new MusicAlbum
        ///         {
        ///             Id = 14,
        ///             HasChild = true,
        ///             Name = "MP3 Albums"
        ///         });
        ///
        ///         Albums.Add(new MusicAlbum
        ///         {
        ///             Id = 15,
        ///             ParentId = 14,
        ///             Name = "Rock"
        ///         });
        ///     }
        /// }
        /// ]]></code>
        ///
        /// In this example:
        /// - The `ExpandedNodes` property is bound to a string array containing the IDs of the expanded nodes.
        /// - The `MusicAlbum` class defines the data structure of each node in the tree.
        /// - The `Albums` list is populated with sample data in the `OnInitialized` method.
        /// - The <see cref="ShowCheckBox"/> property is set to <c>true</c> to enable checkboxes for each node.
        /// </example>
        [Parameter]
        public string[] ExpandedNodes { get; set; }

        /// <summary>
        /// This event is raised whenever the expanded state of a node in the <see cref="SfTreeView{TValue}"/> component changes.
        /// The callback receives an array of strings containing the IDs of the nodes whose expanded state has changed.
        /// This can be used to update the application's state or perform other actions in response to changes in the expanded nodes.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        [Parameter]
        public EventCallback<string[]> ExpandedNodesChanged { get; set; }


        /// <summary>
        /// Gets or sets the data source and mapping fields to render TreeView nodes.
        /// </summary>
        internal TreeViewFieldsSettings<TValue>? TreeViewFields { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entire <see cref="SfTreeView{TValue}"/> node is navigable instead of just the text element.
        /// </summary>
        /// <value>
        /// <c>true</c> if the entire TreeView node is navigable; otherwise, <c>false</c>. The default value is <c>false</c>.
        /// </value>
        /// <remarks>
        /// When this property is set to <c>true</c>, navigation occurs when the user clicks on any part of the TreeView node.  
        /// Otherwise, navigation occurs only when the user clicks on the text of the TreeView node.
        /// </remarks>
        [Parameter]
        public bool FullRowNavigable { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether the entire row of the <see cref="SfTreeView{TValue}"/> node is selected when clicked.
        /// </summary>
        /// <value>
        /// <c>true</c> if the entire node is selected when clicked; otherwise, only the node's text section will be selected. The default value is <c>true</c>.
        /// </value>
        /// <remarks>
        /// If this property is set to <c>true</c>, the entire tree node will be selectable when clicked. If set to <c>false</c>, only the text section of the node will be selectable.
        /// </remarks>     
        [Parameter]
        public bool FullRowSelect { get; set; } = true;

        /// <summary>
        /// Gets or sets a value that indicates whether child nodes will be rendered dynamically when expanding or collapsing a parent node, instead of loading all tree nodes initially in the <see cref="SfTreeView{TValue}"/> component.
        /// </summary>
        /// <value>
        /// Set to <c>true</c> to load child nodes dynamically when expanding a parent node; otherwise, set to <c>false</c>. The default value is <c>true</c>.
        /// </value>
        /// <remarks>
        /// By default, <c>LoadOnDemand</c> is enabled, so child nodes are rendered dynamically when a parent node is expanded.
        /// This improves the performance of the <see cref="SfTreeView{TValue}"/> component on initial load by loading only parent nodes initially.
        /// If this property is set to <c>false</c>, all parent and child nodes are rendered on initial load, which may impact performance for large data sets.
        /// </remarks>
        [Parameter]
        public bool LoadOnDemand { get; set; } = true;


        /// <summary>
        /// Gets or sets the IDs of the selected nodes in the <see cref="SfTreeView{TValue}"/> component.
        /// </summary>
        /// <value>
        /// Pass the node's ID as a string array collection.
        /// </value>
        /// <remarks>
        /// The <c>SelectedNodes</c> property supports two-way binding. Changes to the selected nodes in the TreeView
        /// will be reflected in the bound string array collection. This property allows you to track and manage the selected state of nodes in the TreeView component.
        /// </remarks>
        /// <example> 
        /// <code><![CDATA[ 
        /// <SfTreeView TValue="MusicAlbum" @bind-SelectedNodes="@SelectedNodes">
        ///     <TreeViewFieldsSettings TValue="MusicAlbum" Id="Id" DataSource="@Albums" Text="Name" 
        ///         ParentID="ParentId" HasChildren="HasChild" Expanded="Expanded" IsChecked="IsChecked">
        ///     </TreeViewFieldsSettings>
        /// </SfTreeView>
        ///
        /// @code{
        ///     public class MusicAlbum
        ///     {
        ///         public int Id { get; set; }
        ///         public int? ParentId { get; set; }
        ///         public string Name { get; set; }
        ///         public bool Expanded { get; set; }
        ///         public bool? IsChecked { get; set; }
        ///         public bool HasChild { get; set; }
        ///     }
        ///
        ///     string[] SelectedNodes = new string[] { "16", "18" };
        ///     List<MusicAlbum> Albums = new List<MusicAlbum>();
        ///
        ///     protected override void OnInitialized()
        ///     {
        ///         base.OnInitialized();
        ///         Albums.Add(new MusicAlbum
        ///         {
        ///             Id = 14,
        ///             HasChild = true,
        ///             Name = "MP3 Albums"
        ///         });
        ///         Albums.Add(new MusicAlbum
        ///         {
        ///             Id = 15,
        ///             ParentId = 14,
        ///             Name = "Rock"
        ///         });
        ///     }
        /// }
        /// ]]></code>         
        /// </example> 
        [Parameter]
        public string[] SelectedNodes { get; set; }

        /// <summary>
        /// This event is raised whenever the selected state of a node changes in the <see cref="SfTreeView{TValue}"/> component.
        /// The callback receives an array of strings containing the IDs of the nodes whose selected state has changed.
        /// This allows updating the application state or performing other actions in response to node selection changes.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        [Parameter]
        public EventCallback<string[]> SelectedNodesChanged { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to display checkboxes for each node in the <see cref="SfTreeView{TValue}"/> component.
        /// </summary>
        /// <value>
        /// Set to <c>true</c> to show checkboxes next to each tree view node; otherwise, <c>false</c>. The default value is <c>false</c>.
        /// </value>
        /// <remarks>
        /// The `ShowCheckBox` property controls the visibility of checkboxes in tree nodes. The checkboxes support tri-state behavior: checked, unchecked, and indeterminate.  
        /// Checking or unchecking can be done by clicking the checkbox or pressing the space key.  
        /// The checkboxes are displayed next to the node's expand/collapse icon.
        /// </remarks>
        [Parameter]
        public bool ShowCheckBox { get; set; }

        /// <summary>
        /// Gets or sets the sort order for the nodes in the <see cref="SfTreeView{TValue}"/> component.
        /// </summary>
        /// <value>
        /// A value of the <see cref="SortOrder"/> enumeration.
        /// </value>
        /// <remarks>
        /// The available options for the sort order are:  
        /// - <see cref="SortOrder.None"/> : The nodes are not sorted.  
        /// - <see cref="SortOrder.Ascending"/> :The nodes are sorted in ascending order.  
        /// - <see cref="SortOrder.Descending"/> : The nodes are sorted in descending order.  
        /// </remarks>
        [Parameter]
        public SortOrder SortOrder { get; set; }

        /// <summary>
        /// Gets or sets a custom sort comparer object to define a custom sorting logic.
        /// </summary>
        /// <value>
        /// Name of sort comparer object to be executed. The default value is <c>null</c>.
        /// </value>
        /// <remarks>
        /// To define custom sorting logic, implement the comparer class using the [IComparer](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.icomparer-1?view=net-8.0) interface.  
        /// This allows overriding the default sorting behavior of the <see cref="SfTreeView{TValue}"/> component.
        /// </remarks>
        [Parameter]
        [DefaultValue(null)]
        public IComparer<object> SortComparer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether virtualization is enabled in the <see cref="SfTreeView{TValue}"/> component, which loads data on-demand through vertical scrolling.
        /// </summary>
        /// <value>
        /// <c>true</c> to enable virtualization in the TreeView; otherwise, <c>false</c>. The default value is <c>false</c>.
        /// </value>
        /// <remarks>
        /// When set to <c>true</c>, virtualization is enabled, and the TreeView will load only the nodes that are currently visible in the viewport, which can significantly improve the performance and responsiveness of the TreeView when dealing with large datasets.
        /// Subsequent nodes will be loaded dynamically as the user scrolls vertically through the TreeView.  
        /// `Note:`    
        /// - Virtualization is not compatible with expand and collapse animation.  
        /// - The <see cref="Height"/> property of the TreeView must be set to use this virtualization feature.  
        /// - The "Select All" action will only select visible items in the UI.
        /// </remarks>
        [Parameter]
        [DefaultValue(false)]
        [JsonPropertyName("enableVirtualization")]
        public bool EnableVirtualization { get; set; }

        /// <summary>
        /// Gets or sets the scrollable height of the <see cref="SfTreeView{TValue}"/> component.
        /// </summary>
        /// <value>
        /// The height value, defined in pixels by the user, to be applied to the TreeView container.
        /// </value>
        /// <remarks>
        /// The TreeView will render within the container based on the specified height. Scrolling will occur based on this height, and the virtualization feature will operate according to this value.
        /// </remarks>
        [Parameter]
        [JsonPropertyName("height")]
        public string Height { get; set; }

        /// <summary>
        /// Gets or sets the HtmlAttributes for TreeView.
        /// </summary>
        private Dictionary<string, object>? SfHtmlAttributes { get; set; }

        /// <summary>
        /// Gets or sets the custom item template of TreeView Node item.
        /// </summary>
        internal TreeViewTemplates<TValue>? TreeViewTemplate { get; set; }

        /// <summary>
        /// Gets or sets the ID attribute for the <see cref="SfTreeView{TValue}"/> component.
        /// </summary>
        /// <value>
        /// A string value representing the ID of the TreeView component.
        /// </value>
        [Parameter]
        public string ID { get; set; }

        internal TreeViewNodeAnimationSettings? AnimationSettings { get; set; }

        /// <summary>
        /// Specifies the Animation properties.
        /// </summary>
        /// <param name="animationSettings">"Specifies the animation settings".</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void UpdateAnimationProperties(TreeViewNodeAnimationSettings animationSettings)
        {
            var treeAnimation = animationSettings;
            if (treeAnimation == null)
            {
                treeAnimation = new TreeViewNodeAnimationSettings();
                treeAnimation.UpdateExpandProperties(treeAnimation.NodeAnimationExpand, SyncfusionService);
                treeAnimation.UpdateCollapseProperties(treeAnimation.NodeAnimationCollapse, SyncfusionService);
            }
            AnimationSettings = treeAnimation;
        }
    }
}
