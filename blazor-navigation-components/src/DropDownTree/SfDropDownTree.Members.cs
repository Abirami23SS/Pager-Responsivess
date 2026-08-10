using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Internal;

namespace Syncfusion.Blazor.Navigations
{
    public partial class SfDropDownTree<TValue, TItem> : SfBaseComponent
    {
        /// <exclude/>
        /// <summary>
        /// Gets or sets the content to display within a Dropdown Tree component.
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Gets or sets the ID attribute for the <see cref="SfDropDownTree{TValue,TItem}"/> element.
        /// </summary>
        /// <value>
        /// A string value representing the ID of the Dropdown Tree element.
        /// </value>
        [Parameter]
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the filtering option is enabled in the <see cref="SfDropDownTree{TValue,TItem}"/> component.
        /// </summary>
        ///<value>
        /// Set to <c>true</c> to enable the filtering functionality; otherwise, set to 'false'.
        ///</value>
        ///<remarks>
        /// Filter action is performed when the user types in the search box, and the matched items are collected through the 'Filtering' event.
        /// If searching character does not match, `NoRecordsTemplate` property value will be shown.
        /// </remarks>
        [Parameter]
        public bool AllowFiltering { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether multi-selection of nodes is enabled in the <see cref=" SfDropDownTree {TValue,TItem}"/> component.
        /// </summary>
        /// <value>
        ///  Set to <c>true</c> if multi-selection is enabled; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// When multi-selection is enabled, users can select multiple nodes in the <see cref=" SfDropDownTree {TValue,TItem}"/> component by holding down the CTRL key and clicking on the nodes they want to select.
        /// Consecutive nodes can be selected by holding down the SHIFT key and clicking on the initial and final nodes of the range to be selected.
        /// The <see cref="ShowCheckBox"/> property can also be used to enable checkbox support for node selection.
        /// </remarks>
        [Parameter]
        public bool AllowMultiSelection { get; set; }

        /// <summary>
        /// Gets or sets the template used to render the content of the popup list in the <see cref="SfDropDownTree{TValue,TItem}"/> component when the data fetch request from the remote server fails.
        /// </summary>
        /// <value>
        /// The template content. The default value in <c>null</c>.
        /// </value>
        /// <remarks>
        /// You can specify a custom message to be displayed to users when the data fetch request fails.
        /// </remarks>
        /// <example>
        /// In the following code example, used the <see cref="SfDropDownTree{TValue,TItem}.ActionFailureTemplate"/> to customize the failure content. 
        /// <code><![CDATA[
        /// @using Syncfusion.Blazor.DropDowns
        /// @using Syncfusion.Blazor.Data
        ///
        /// <SfDropDownTree TValue="string" TItem="TreeData" ID="default1" Width="580px" PopupWidth="580px" Placeholder="Select Folder" Text="Nancy">
        ///     <ChildContent>
        ///         <DropDownTreeField TItem="TreeData" Query="@employeeQuery" Id="EmployeeID" Text="FirstName" HasChildren="EmployeeID">
        ///             <SfDataManager Url="https://services.odata.org/V4/Northwind/Northwind.svcs" Adaptor="Syncfusion.Blazor.Adaptors.ODataV4Adaptor" CrossDomain="true"></SfDataManager>
        ///         </DropDownTreeField>
        ///         <DropDownTreeField TItem="TreeData" Level="1" Query="@orderQuery" Id="OrderID" Text="ShipName" ParentID="EmployeeID">
        ///             <SfDataManager Url="https://services.odata.org/V4/Northwind/Northwind.svcs" Adaptor="Syncfusion.Blazor.Adaptors.ODataV4Adaptor" CrossDomain="true"></SfDataManager>
        ///         </DropDownTreeField>
        ///     </ChildContent>
        ///     <ActionFailureTemplate>
        ///         <div>action failure</div>
        ///     </ActionFailureTemplate>
        /// </SfDropDownTree>
        ///
        /// @code {
        /// // Specify the column value of the employee table.
        /// public static List<string> EmployeeDetails = new List<string>() { "EmployeeID", "FirstName", "Title" };
        ///
        /// // Specify the query value of the DropDownTree component.
        /// Query employeeQuery = new Query().From("Employees").Select(EmployeeDetails).Take(5);
        ///
        /// // Specify the column value of the order table.
        /// public static List<string> OrderDetails = new List<string>() { "OrderID", "EmployeeID", "ShipName" };
        ///
        /// // Specify the query value of the DropDownTree component.
        /// Query orderQuery = new Query().From("Orders").Select(OrderDetails).Take(5);
        ///
        /// class TreeData
        /// {
        ///     public int? EmployeeID { get; set; }
        ///     public int OrderID { get; set; }
        ///     public string ShipName { get; set; }
        ///     public string FirstName { get; set; }
        /// }
        /// }
        /// ]]></code>
        /// </example>
        [Parameter]
        public RenderFragment ActionFailureTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template that renders a customized footer content at the bottom of the pop-up list.
        /// </summary>
        /// <value>
        /// The template content. The default value in <c>null</c>.
        /// </value>
        /// <example>
        /// In the following code example, used the <see cref="SfDropDownTree{TValue,TItem}.FooterTemplate"/> to customize the footer content. 
        /// <code><![CDATA[
        /// @using Syncfusion.Blazor.Navigations
        ///
        /// <SfDropDownTree TValue="string" TItem="TreeData">
        ///     <ChildContent>
        ///         <DropDownTreeField TItem="TreeData" DataSource="@TreeDataSource" Id="Id" ParentID="Pid" Text="Name" HasChildren="HasChild" Expanded="Expanded" Selected="IsSelected"></DropDownTreeField>
        ///     </ChildContent>
        ///     <FooterTemplate>
        ///         <div> Total collection @TreeDataSource.Count</div>
        ///     </FooterTemplate>
        /// </SfDropDownTree>
        ///
        /// @code {
        ///     List<TreeData> TreeDataSource = new List<TreeData>();
        ///     protected override void OnInitialized()
        ///     {
        ///         base.OnInitialized();
        ///         TreeDataSource.Add(new TreeData
        ///             {
        ///                 Id = "1",
        ///                 Name = "Discover Music",
        ///                 HasChild = true,
        ///                 Expanded = true
        ///             });
        ///         TreeDataSource.Add(new TreeData
        ///             {
        ///                 Id = "2",
        ///                 Pid = "1",
        ///                 Name = "Hot Singles",
        ///             });
        ///         TreeDataSource.Add(new TreeData
        ///             {
        ///                 Id = "3",
        ///                 Pid = "1",
        ///                 Name = "Rising Artists"
        ///             });
        ///         TreeDataSource.Add(new TreeData
        ///             {
        ///                 Id = "4",
        ///                 Pid = "1",
        ///                 Name = "Live Music"
        ///             });
        ///     }
        ///
        ///     class TreeData
        ///     {
        ///         public string Id { get; set; }
        ///         public string Pid { get; set; }
        ///         public bool HasChild { get; set; }
        ///         public bool Expanded { get; set; }
        ///         public bool IsSelected { get; set; }
        ///         public string Name { get; set; }
        ///     }
        /// }
        /// ]]></code>
        /// </example>
        [Parameter]
        public RenderFragment FooterTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template that renders a customized header content at the top of the pop-up list.
        /// </summary>
        /// <value>
        /// The template content. The default value in <c>null</c>.
        /// </value>
        /// <example>
        /// In the following code example, used the <see cref="SfDropDownTree{TValue,TItem}.HeaderTemplate"/> to customize the header content. 
        /// <code><![CDATA[
        /// @using Syncfusion.Blazor.Navigations
        ///
        /// <SfDropDownTree TValue="string" TItem="TreeData">
        ///     <ChildContent>
        ///         <DropDownTreeField TItem="TreeData" DataSource="@TreeDataSource" Id="Id" ParentID="Pid" Text="Name" HasChildren="HasChild" Expanded="Expanded" Selected="IsSelected"></DropDownTreeField>
        ///     </ChildContent>
        ///     <HeaderTemplate>
        ///         <div> Music Categories</div>
        ///     </HeaderTemplate>
        /// </SfDropDownTree>
        ///
        /// @code {
        ///     List<TreeData> TreeDataSource = new List<TreeData>();
        ///     protected override void OnInitialized()
        ///     {
        ///         base.OnInitialized();
        ///         TreeDataSource.Add(new TreeData
        ///             {
        ///                 Id = "1",
        ///                 Name = "Discover Music",
        ///                 HasChild = true,
        ///                 Expanded = true
        ///             });
        ///         TreeDataSource.Add(new TreeData
        ///             {
        ///                 Id = "2",
        ///                 Pid = "1",
        ///                 Name = "Hot Singles",
        ///             });
        ///         TreeDataSource.Add(new TreeData
        ///             {
        ///                 Id = "3",
        ///                 Pid = "1",
        ///                 Name = "Rising Artists"
        ///             });
        ///         TreeDataSource.Add(new TreeData
        ///             {
        ///                 Id = "4",
        ///                 Pid = "1",
        ///                 Name = "Live Music"
        ///             });
        ///     }
        ///
        ///     class TreeData
        ///     {
        ///         public string Id { get; set; }
        ///         public string Pid { get; set; }
        ///         public bool HasChild { get; set; }
        ///         public bool Expanded { get; set; }
        ///         public bool IsSelected { get; set; }
        ///         public string Name { get; set; }
        ///     }
        /// }
        /// ]]></code>
        /// </example>
        [Parameter]
        public RenderFragment HeaderTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template design and assign it to each tree list item present in the popup.
        /// </summary>
        /// <value>
        /// The template content. The default value is <c>null</c>.
        /// </value>
        /// <example>
        /// In the following code example, the Dropdown Tree list items are customized with employee information such as name and job using the <see cref="SfDropDownTree{TValue,TItem}.ItemTemplate"/> property.
        /// <code><![CDATA[
        /// @using Syncfusion.Blazor.Navigations
        ///
        /// <SfDropDownTree TItem="EmployeeData" TValue="string" Width="100%" CssClass="custom" Placeholder="Select an employee" PopupHeight="250px">
        ///     <ChildContent>
        ///         <DropDownTreeField TItem="EmployeeData" DataSource="Data" Id="Id" Text="Name" HasChildren="HasChild" ParentID="PId"></DropDownTreeField>
        ///     </ChildContent>
        ///     <ItemTemplate>
        ///         <div>
        ///             <div>
        ///                 <span>@((context as EmployeeData).Name) - </span>
        ///                 <span>@((context as EmployeeData).Job)</span>
        ///             </div>
        ///         </div>
        ///     </ItemTemplate>
        /// </SfDropDownTree>
        ///
        /// @code {
        ///     List<EmployeeData> TreeDataSource = new List<EmployeeData>();
        ///     
        ///     public class EmployeeData
        ///     {
        ///         public string Id { get; set; }
        ///         public string Name { get; set; }
        ///         public string Job { get; set; }
        ///         public string image { get; set; }
        ///         public bool HasChild { get; set; }
        ///         public bool Expanded { get; set; }
        ///         public string Status { get; set; }
        ///         public string PId { get; set; }
        ///     }
        ///     
        ///     List<EmployeeData> Data = new List<EmployeeData>
        ///     {
        ///         new EmployeeData() {Id="1", Name = "Steven Buchanan",  Job = "General Manager", image= "10",HasChild=true,Expanded=true,Status="busy" },
        ///         new EmployeeData() {Id="2",PId="1", Name = "Laura Callahan",  Job = "Product Manager", image= "2",HasChild=true,Status="online" },
        ///         new EmployeeData() {Id="3",PId="2", Name = "Andrew Fuller",  Job = "Team Lead", image= "7",HasChild=true,Status="away" },
        ///         new EmployeeData() {Id="4",PId="3", Name = "Anne",  Job = "Developer", image= "1",Status="busy" },
        ///         new EmployeeData() {Id="5",PId="1", Name = "Nancy",  Job = "Product Manager", image= "4",HasChild=true,Status="away" },
        ///         new EmployeeData() {Id="6",PId="5", Name = "Michael",  Job = "Team Lead", image= "9",HasChild=true,Status="online" },
        ///         new EmployeeData() {Id="7",PId="6", Name = "Robert King",  Job = "Developer", image= "8",Status="online" }
        ///     };
        /// }
        /// ]]></code>
        /// </example>
        [Parameter]
        public RenderFragment<TItem> ItemTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template design and assigns it to popup list of component, when no data is available on the component.
        /// </summary>
        /// <value>
        /// The template content. The default value is <c>null</c>.
        /// </value>
        /// <example>
        /// In the following sample, popup list content displays the notification of no data available.
        /// <code><![CDATA[
        /// @using Syncfusion.Blazor.Navigations
        ///
        /// <SfDropDownTree TItem="EmployeeData" TValue="string" Width="100%" CssClass="custom" Placeholder="Select an employee" PopupHeight="250px">
        ///     <ChildContent>
        ///         <DropDownTreeField TItem="EmployeeData" DataSource="Data" Id="Id" Text="Name"></DropDownTreeField>
        ///     </ChildContent>
        ///     <NoRecordsTemplate>
        ///         <span> NO DATA AVAILABLE</span>
        ///     </NoRecordsTemplate>
        /// </SfDropDownTree>
        ///
        /// @code {
        ///     
        ///     public class EmployeeData
        ///     {
        ///         public string Id { get; set; }
        ///         public string Name { get; set; }
        ///     }
        ///     
        ///     List<EmployeeData> Data = new List<EmployeeData> { };
        /// }
        ///
        /// ]]></code>
        /// </example>
        [Parameter]
        public RenderFragment NoRecordsTemplate { get; set; }

        /// <summary>
        /// Gets or sets a CSS class string to customize the appearance of the <see cref=" SfDropDownTree {TValue,TItem}"/> component.
        /// </summary>
        /// <value>
        /// Accepts a CSS class string separated by space to customize the appearance. The default value is <c>String.Empty</c>. 
        /// </value>
        /// <remarks>
        /// Multiple CSS classes can be added for the component using this property to customize its styles.
        /// </remarks>
        [Parameter]
        public string CssClass { get; set; } = string.Empty;

        /// <summary>
        /// Sets the value separator character in the input element when the <see cref="AllowMultiSelection"/> or <see cref="ShowCheckBox"/> support is enabled in the <see cref=" SfDropDownTree {TValue,TItem}"/> component.
        /// </summary>
        /// <value>
        /// Accepts the string value. The default value is <c>,</c>.
        /// </value>
        /// <remarks>
        /// The delimiter character is applicable only for default and delimiter visibility modes. 
        /// </remarks>
        [Parameter]
        public string DelimiterChar { get; set; } = ",";

        /// <summary>
        /// Gets or sets whether diacritic characters or accents are ignored when filtering.
        /// </summary>
        /// <value>
        /// Set to <c>true</c>, ignores the diacritic characters or accents when filtering. The default value is <c>false</c>.
        /// </value>
        [Parameter]
        public bool IgnoreAccent { get; set; }

        /// <summary>
        /// Gets or sets whether to persist the state of the <see cref="SfDropDownTree{TValue,TItem}"/> component between page reloads.
        /// </summary>
        /// <value>
        /// Set to <c>true</c>, if the component's state persistence is enabled. The default value is <c>false</c>.
        /// </value>
        /// <remarks>
        /// The following properties will be stored in browser local storage to persist the state of the component when the page reloads:
        /// <list type="number">
        /// <item>
        /// <term>Value</term>
        /// <description>The value of nodes that are selected in the Dropdown Tree component.</description>
        /// </item>
        /// <item>
        /// <term>Text</term>
        /// <description>The text of nodes that are selected in the Dropdown Tree component.</description>
        /// </item>
        /// </list>
        /// </remarks> 
        [Parameter]
        public bool EnablePersistence { get; set; }

        /// <summary>
        /// Enables or disables the <see cref="SfDropDownTree{TValue,TItem}"/> component.
        /// </summary>
        /// <value>
        /// Set to <c>true</c>, Allow the user to interact with the component. Otherwise, <c>false</c>. The default value is <c>false</c>.
        /// </value>
        [Parameter]
        public bool Disabled { get; set; }

        /// <summary>
        /// Gets or sets the text that is displayed when the filter textbox has no text and removes the focus.
        /// </summary>
        /// <value>
        /// The text that is displayed when the filter text box has no search text. The default value is <c>String.Empty</c>.
        /// </value>
        /// <remarks>
        /// This property value is updated only when the <see cref="AllowFiltering" /> is enabled.
        /// </remarks>
        [Parameter]
        public string FilterBarPlaceholder { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the filter type,the component needs to be considered on search action.
        /// </summary>
        /// <value> 
        /// One of the <see cref="FilterType"/> enumeration. The default value is <see cref="FilterType.StartsWith"/> 
        /// </value> 
        /// <remarks> 
        /// If the <c>FilterType</c> is <c>StartsWith</c>, the filtering will be performed using starts with operator.
        /// If the <c>FilterType</c> is <c>EndsWith</c>, the filtering will be performed using ends with operator.
        /// If the <c>FilterType</c> is <c>Contains</c>, the filtering will be performed using contains operator.
        /// </remarks>
        [Parameter]
        public FilterType FilterType { get; set; }

        /// <summary>
        /// Gets or sets the floating label behavior of the input that the placeholder text floats above the input based on the following values.
        /// <para>Possible values are:</para>
        /// <list type="bullet">
        /// <item>
        /// <term>Never</term>
        /// <description>The label will never float in the input when the placeholder is available.</description>
        /// </item>
        /// <item>
        /// <term>Always</term>
        /// <description>The floating label will always float above the input.</description>
        /// </item>
        /// <item>
        /// <term>Auto</term>
        /// <description>The floating label will float above the input after focusing or entering a value in the input.</description>
        /// </item>
        /// </list>
        /// </summary>
        /// <value>
        /// The default value is <c>Never</c>.
        /// </value> 
        [Parameter]
        public FloatLabelType FloatLabelType { get; set; }

        /// <summary>
        /// Gets or sets whether case-sensitivity is enabled or disabled when searching for suggestions.
        /// </summary>
        /// <value>
        /// Set to <c>false</c>, consider the `case-sensitive` on performing the search to find suggestions. The default value is <c>true</c>.
        /// </value>
        [Parameter]
        public bool IgnoreCase { get; set; } = true;

        /// <summary>
        /// Gets or sets the possible values for visualizing selected items in the <see cref="SfDropDownTree{TValue,TItem}"/> component when <see cref="AllowMultiSelection"/> or <see cref="ShowCheckBox"/> is enabled.
        /// Possible values are:
        /// <list type="bullet">
        /// <item>
        /// <term>Box</term>
        /// <description>Selected items will be visualized in chip format.</description>
        /// </item>
        /// <item>
        /// <term>Delimiter</term>
        /// <description>Selected items will be visualized in the text content. </description>
        /// </item>
        /// <item>
        /// <term>Default</term>
        /// <description> When focused, the component will act in the box mode. When blurred, the component will act in the delimiter mode.</description>
        /// </item>
        /// </list>
        /// </summary>
        /// <value>
        /// Specifies the mode that determines the visibility and interactivity of the component.
        /// </value> 
        [Parameter]
        public DdtVisualMode Mode { get; set; }

        /// <summary>
        /// Gets or sets the placeholder value that describes the expected value of the Dropdown Tree component.
        /// </summary>
        /// <value>
        /// Accepts a string. The default value is <c>Null</c>.
        /// </value>
        [Parameter]
        public string Placeholder { get; set; }

        /// <summary>
        /// Gets or sets the height of the popup list. By default, it renders based on its list item.
        /// </summary>
        /// <value>
        /// Accepts the string value. The default value is <c>300px</c>.
        /// </value>
        [Parameter]
        public string PopupHeight { get; set; } = "300px";

        /// <summary>
        /// Gets or sets the width of the popup list and percentage values is calculated based on input width.
        /// </summary>
        /// <value>
        /// Accepts the string value. The default value is <c>100%</c>.
        /// </value>
        [Parameter]
        public string PopupWidth { get; set; } = "100%";

        private Dictionary<string, object> htmlAttributes = new Dictionary<string, object>();

        /// <summary>
        /// Gets or sets a a value that indicates the collection of additional attributes that will be applied to the <see cref="SfDropDownTree{TValue,TItem}"/>  container element.
        /// </summary>
        /// <value>The value as dictionary collection.The default value is <c>null</c></value>
        /// <remarks>
        /// Additional attributes can be added by specifying as inline attributes or by specifying <c>@attributes</c> directive.
        /// </remarks>
        /// <example> 
        /// <code><![CDATA[
        /// @using Syncfusion.Blazor.Navigations
        ///
        /// <SfDropDownTree TItem="EmployeeData" TValue="string" Width="100%" CssClass="custom" Placeholder="Select an employee" PopupHeight="250px" HtmlAttributes="@htmlAttribute">
        ///     <DropDownTreeField TItem="EmployeeData" DataSource="Data" Id="Id" Text="Name" HasChildren="HasChild" ParentID="PId"></DropDownTreeField>
        /// </SfDropDownTree>
        ///
        /// @code {
        ///     Dictionary<string, object> htmlAttribute = new Dictionary<string, object>()
        ///     {
        ///         {"name", "employees" },
        ///         {"style", "background-color: yellow; text-align: right" },
        ///         {"title", "Syncfusion DropDownTree" }
        ///     };
        ///     
        ///     public class EmployeeData
        ///     {
        ///         public string Id { get; set; }
        ///         public string Name { get; set; }
        ///         public string Job { get; set; }
        ///         public string image { get; set; }
        ///         public bool HasChild { get; set; }
        ///         public bool Expanded { get; set; }
        ///         public string Status { get; set; }
        ///         public string PId { get; set; }
        ///     }
        ///     
        ///     List<EmployeeData> Data = new List<EmployeeData>
        ///     {
        ///         new EmployeeData() {Id="1", Name = "Steven Buchanan",  Job = "General Manager", image= "10",HasChild=true,Expanded=true,Status="busy" },
        ///         new EmployeeData() {Id="2",PId="1", Name = "Laura Callahan",  Job = "Product Manager", image= "2",HasChild=true,Status="online" },
        ///     };
        /// }
        /// ]]></code>
        /// </example> 
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> HtmlAttributes
        {
            get => htmlAttributes;
            set => htmlAttributes = SfBaseUtils.SanitizeHtmlAttributes(value);
        }

        /// <summary> 
        /// Gets or sets a value that indicates whether to show checkboxes in each node of the <see cref="SfDropDownTree{TValue,TItem}"/> component popup.
        /// </summary>
        /// <value>
        /// Set to <c>true</c> to show checkboxes on each tree view node in the popup; otherwise, 'false'. The default value is <c>false</c>.
        /// </value>
        /// <remarks>
        /// The `ShowCheckBox` property is used to show or hide the checkboxes in tree nodes of popup list.
        /// The checkboxes have tri-state behavior, such as checked, unchecked, and indeterminate.
        /// The check/uncheck action can be performed either through checkbox click or pressing the space key.
        /// The checkboxes are displayed next to the expand/collapse icon of the nodes.
        /// </remarks>
        [Parameter]
        public bool ShowCheckBox { get; set; }

        /// <summary>
        /// Gets or sets a boolean value that indicates whether the clear button is displayed in the <see cref="SfDropDownTree{TValue,TItem}"/> input.
        /// </summary>
        /// <value>
        /// Set to <c>true</c>, if the clear button should be shown. Otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// Enabling this option adds a clear button to the input, allowing users to clear multiple selected values at once.
        /// </remarks>
        [Parameter]
        public bool ShowClearButton { get; set; } = true;

        /// <summary>
        /// Gets or sets a value that determines whether to show or hide the selectAll option on the component.
        /// </summary>
        /// <value>
        /// Set to <c>true</c>, if the select all option should be shown. Otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// Enabling this option allows users to select all items in the popup through a single checkbox action.
        /// </remarks>
        [Parameter]
        public bool ShowSelectAll { get; set; }

        /// <summary>
        /// Gets or sets a value indicating how the items are sorted in ascending or descending order, or not sorted at all.
        /// </summary>
        /// <value>
        /// Available sort order types include:
        /// <list type="bullet">
        /// <item>
        /// <term>None</term>
        /// <description>The items are not sorted.</description>
        /// </item>
        /// <item>
        /// <term>Ascending</term>
        /// <description>The items are sorted in ascending order.</description>
        /// </item>
        /// <item>
        /// <term>Descending</term>
        /// <description>The items are sorted in descending order.</description>
        /// </item>
        /// </list>
        /// </value>
        [Parameter]
        public SortOrder SortOrder { get; set; }

        /// <summary>
        /// Gets or sets the display text of the selected item, which corresponds to the data text field in the component.
        /// </summary>
        /// <value>
        /// The default value is <c>Null</c>.
        /// </value>
        /// <remarks>
        /// This property stores the selected text value(s) that are displayed in the input of the Dropdown Tree.
        /// </remarks>
        [Parameter]
        public string Text { get; set; }

        /// <summary>
        /// Gets the value of the selected item in the Dropdown Tree component. This will be used with a two-way binding.
        /// </summary>
        /// <value>
        /// The value of the selected item in the Dropdown Tree component. The default is <c>null</c>.
        /// </value>
        /// <remarks>
        /// Use the <see cref="Value"/> property to specify or determine the value displayed in the <see cref="SfDropDownTree{TValue,TItem}"/> component.
        /// </remarks>
        [Parameter]
        public List<TValue> Value { get; set; }

        /// <summary>
        /// Gets or sets the width of the <see cref="SfDropDownTree{TValue,TItem}"/> component.
        /// </summary>
        /// <value>
        /// The preferred width of the drop down tree can be in pixels or percentage value. The default value is <c>100%</c>.
        /// </value>
        [Parameter]
        public string Width { get; set; } = "100%";

        /// <summary>
        /// Gets or sets the z-index value of the component popup element.
        /// </summary>
        /// <value>
        /// The ZIndex value for the popup element.The default value is <c>1000</c>.
        /// </value>
        [Parameter]
        public double ZIndex { get; set; } = 1000;

        /// <summary>
        /// Gets or sets a value that determines whether the checked state of parent nodes in the <see cref="SfDropDownTree{TValue, TItem}"/> component
        /// is automatically updated based on the checked state of their child nodes.
        /// </summary>
        /// <value>
        /// <c>true</c> if automatic update of parent node checked states is enabled; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// When the <see cref="AutoUpdateCheckState"/> property is set to `true`, the checked state of parent nodes in the <see cref="SfDropDownTree{TValue, TItem}"/> component
        /// will be automatically updated based on the checked state of their child nodes. This is useful for maintaining the consistency of the
        /// <see cref="SfDropDownTree{TValue, TItem}"/> checkbox hierarchy and ensuring that parent nodes are checked only if all their child nodes are also checked.
        /// This property only works when the <see cref="ShowCheckBox"/> property is set to `true`.
        /// </remarks>
        [Parameter]
        public bool AutoUpdateCheckState { get; set; }

        /// <summary>
        /// Gets or sets a value that determines whether child nodes will be rendered while expanding and collapsing a parent node inside the popup,
        /// instead of loading all tree nodes initially in the <see cref="SfDropDownTree{TValue, TItem}"/> component.
        /// </summary>
        /// <value>
        /// Set to <c>true</c> to load child nodes dynamically on expanding a parent node; otherwise, set to <c>false</c>. The default value is <c>false</c>.
        /// </value>
        /// <remarks>
        /// By default, <see cref="LoadOnDemand"/> is disabled, so child nodes are rendered during initial rendering.
        /// Enabling this property as <c>true</c> can improve the performance of the <see cref="SfDropDownTree{TValue, TItem}"/> component on initial load,
        /// as it loads only parent nodes initially.
        /// </remarks>
        [Parameter]
        public bool LoadOnDemand { get; set; }

        /// <summary>
        /// Gets or sets the value that indicates the action on which the node expands or collapses in the popup of the <see cref="SfDropDownTree{TValue, TItem}"/> component.
        /// </summary>
        /// <value>
        /// The default value is <c>ExpandAction.DoubleClick</c>.
        /// </value>
        /// <remarks>
        /// The available actions are:
        /// - <c>ExpandAction.Click</c>: The expand/collapse operation happens when you single-click on the node in desktop.
        /// - <c>ExpandAction.DblClick</c>: The expand/collapse operation happens when you double-click on the node in desktop.
        /// - <c>ExpandAction.None</c>: The expand/collapse operation will not happen.
        /// In mobile devices, the node expand/collapse action happens on single tap always.
        /// </remarks>
        /// <example>
        /// <code>
        /// code example 
        /// </code>
        /// </example>
        [Parameter]
        public ExpandAction ExpandOn { get; set; } = ExpandAction.DoubleClick;

        /// <summary>
        /// Gets or sets whether to wrap the selected items into multiple lines when the selected item's text content exceeds the input width limit.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the selected items into multiple lines. Otherwise, <c>false</c>.
        /// </value>
        [Parameter]
        public bool TextWrap { get; set; }

        /// <summary>
        /// Gets or sets the template that will be added to the input instead of the selected item text in the <see cref="SfDropDownTree{TValue, TItem}"/> component when the <see cref="AllowMultiSelection"/> or <see cref="ShowCheckBox"/> support is enabled.
        /// </summary>
        /// <value>
        /// The template content. The default value is <c>null</c>.
        ///</value>
        /// <remarks>
        /// This property is used to customize the display text of the selected items in the <see cref="SfDropDownTree{TValue, TItem}"/>. When setting this property, the mode must be set to `custom` in the Dropdown Tree.
        /// </remarks>
        [Parameter]
        public RenderFragment<SelectedItemTemplate<TValue>> SelectedItemTemplate { get; set; }

        /// <summary> 
        /// Gets or sets the template to customize the display of selected values in the <see cref="SfDropDownTree{TValue, TItem}"/> component based on application requirements. 
        /// </summary> 
        /// <value> 
        /// The template content. The default value is <c>null</c>. 
        /// </value> 
        /// <remarks> 
        /// This property allows customization of the selected values displayed in the <see cref="SfDropDownTree{TValue, TItem}"/> component, enabling features such as displaying the value field, icons, or other elements along with the selected text. 
        /// </remarks> 
        [Parameter]
        public RenderFragment<TItem> ValueTemplate { get; set; }

        /// <summary>
        /// Gets or sets a callback that triggers when the value changed in the <see cref="SfDropDownTree{TValue,TItem}"/> component.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        [Parameter]
        public EventCallback<List<TValue>> ValueChanged { get; set; }

        /// <summary>
        /// Gets or sets an event callback that is raised while any Dropdown Tree action failed to fetch the desired results.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <remarks>
        /// You can capture this failure and throw error message for users in required cases.
        /// </remarks>
        [Parameter]
        public EventCallback<object> OnActionFailure { get; set; }

        /// <summary>
        /// Gets or sets an event callback that will be invoked when the Dropdown Tree popup is opened after animation.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        [Parameter]
        public EventCallback<PopupEventArgs> OnPopupOpen { get; set; }

        /// <summary>
        /// Gets or sets an event callback that will be invoked when the Dropdown Tree popup is closed after animation.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        [Parameter]
        public EventCallback<PopupEventArgs> OnPopupClose { get; set; }

        /// <summary>
        /// Gets or sets an event callback that will be invoked when the component is created.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        [Parameter]
        public EventCallback<object> Created { get; set; }

        /// <summary>
        /// Gets or sets the event callback that will be invoked when the component is destroyed.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        [Parameter]
        public EventCallback<object> Destroyed { get; set; }

        /// <summary>
        /// Gets or sets an event callback that will be invoked when the <see cref="SfDropDownTree{TValue,TItem}. Value"/> property changed.
        /// </summary>
        /// <remarks>
        /// This event triggers when an item in a popup is selected or when the model value is changed by user.
        /// </remarks>
        /// <value>
        /// An event callback function.
        /// </value>
        [Parameter]
        public EventCallback<DdtChangeEventArgs<TValue>> ValueChanging { get; set; }

        /// <summary>
        /// Gets or sets an event callback that will be invoked when user types a text in search box.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        [Parameter]
        public EventCallback<DdtFilteringEventArgs> Filtering { get; set; }
    }
}
