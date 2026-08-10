# Syncfusion Blazor Grid Component - Technical Specification

**Document Type:** Technical Specification (Implementation Details)  
**Component:** SfGrid<TValue> - Syncfusion Blazor Grid  
**Version:** 18.2.0.56  
**Target Frameworks:** .NET 8.0, 9.0, 10.0  
**Document Date:** March 10, 2026  
**Status:** Complete - Approved for Reference

---

## Table of Contents

1. [Component Overview](#1-component-overview)
2. [Properties Reference](#2-properties-reference)
3. [Methods Reference](#3-methods-reference)
4. [Events Reference](#4-events-reference)
5. [Enumerations](#5-enumerations)
6. [Code Examples](#6-code-examples)
7. [Validation Rules](#7-validation-rules)
8. [Integration Patterns](#8-integration-patterns)
9. [Performance Guidelines](#9-performance-guidelines)
10. [API Compatibility](#10-api-compatibility)

---

## 1. Component Overview

### 1.1 Component Declaration
```razor
<SfGrid TValue="TValue"
        DataSource="@data"
        AllowPaging="true"
        AllowSorting="true"
        AllowFiltering="true"
        AllowGrouping="true"
        AllowExcelExport="true"
        AllowPdfExport="true"
        Height="600px"
        Width="100%">
    <GridColumns>
        <GridColumn Field=@nameof(Model.Property) HeaderText="Header" />
    </GridColumns>
    <GridEditSettings AllowAdding="true" AllowEditing="true" AllowDeleting="true" />
    <GridToolbarTemplate>
        <GridToolbarItems>
            <GridToolbarItem Text="Add" IconCss="e-icons e-add" />
            <GridToolbarItem Text="Edit" IconCss="e-icons e-edit" />
            <GridToolbarItem Text="Delete" IconCss="e-icons e-delete" />
            <GridToolbarItem Text="ExcelExport" IconCss="e-icons e-export" />
            <GridToolbarItem Text="PdfExport" IconCss="e-icons e-pdf-export" />
            <GridToolbarItem Text="Print" IconCss="e-icons e-print" />
        </GridToolbarItems>
    </GridToolbarTemplate>
</SfGrid>
```

### 1.2 Component Inheritance
```
SfGrid<TValue> : SfBaseComponent
    - Implements IDisposable
    - Implements IAsyncDisposable
    - Supports two-way binding
    - Supports parameter cascading
```

### 1.3 Generic Type Parameter
- **TValue:** The type of data item displayed in the grid
- Must be a class (reference type)
- Should have a unique identifier property
- Properties should be public with getters/setters

---

## 2. Properties Reference

### 2.1 Data Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `DataSource` | `IEnumerable<TValue>` | `null` | Data source for the grid |
| `PrimaryKey` | `string` | `null` | Unique identifier field name |
| `AllowPaging` | `bool` | `false` | Enable paging |
| `AllowSorting` | `bool` | `false` | Enable sorting |
| `AllowFiltering` | `bool` | `false` | Enable filtering |
| `AllowGrouping` | `bool` | `false` | Enable grouping |
| `AllowSearching` | `bool` | `false` | Enable search toolbar |
| `AllowSelection` | `bool` | `true` | Enable row/cell selection |
| `AllowResizing` | `bool` | `false` | Enable column resize |
| `AllowReordering` | `bool` | `false` | Enable column reorder |
| `AllowTextWrap` | `bool` | `false` | Enable text wrapping in cells |
| `AllowExcelExport` | `bool` | `false` | Enable Excel export |
| `AllowPdfExport` | `bool` | `false` | Enable PDF export |
| `EnableVirtualization` | `bool` | `false` | Enable row virtualization |
| `EnableColumnVirtualization` | `bool` | `false` | Enable column virtualization |

### 2.2 Appearance Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Height` | `string` | `"auto"` | Grid height (px, %, vh) |
| `Width` | `string` | `"100%"` | Grid width (px, %) |
| `GridLines` | `GridLine` | `GridLine.Both` | Grid border style |
| `RowHeight` | `int` | `45` | Row height in pixels |
| `ColumnWidth` | `int` | `120` | Default column width |
| `ShowHeader` | `bool` | `true` | Show/hide header row |
| `ShowColumnChooser` | `bool` | `false` | Show column chooser button |
| `ShowSummaryRow` | `bool` | `false` | Show summary row |
| `EnableRtl` | `bool` | `false` | Right-to-left layout |
| `EnableStickyHeader` | `bool` | `false` | Sticky header on scroll |
| `CssClass` | `string` | `""` | Custom CSS class |
| `Locale` | `string` | `"en-US"` | Culture code for localization |

### 2.3 Behavior Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `EditMode` | `EditMode` | `EditMode.Normal` | Edit mode type |
| `EditType` | `EditType` | `EditType.DefaultEdit` | Edit type (dialog/inline) |
| `NavigationMode` | `NavigationMode` | `NavigationMode.Row` | Keyboard navigation mode |
| `SelectionMode` | `SelectionMode` | `SelectionMode.Single` | Selection mode |
| `SelectionType` | `SelectionType` | `SelectionType.Row` | Row or cell selection |
| `FilterMode` | `FilterMode` | `FilterMenu` | Filter UI type |
| `SortMode` | `SortMode` | `SortMode.Single` | Single or multi-column sort |
| `GroupSettings` | `GroupSettings` | `null` | Grouping configuration |
| `PageSettings` | `PageSettings` | `null` | Paging configuration |
| `SearchSettings` | `SearchSettings` | `null` | Search configuration |
| `SortSettings` | `SortSettings` | `null` | Sorting configuration |
| `FilterSettings` | `FilterSettings` | `null` | Filtering configuration |

### 2.4 State Management Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `EnablePersistence` | `bool` | `false` | Persist state in localStorage |
| `PersistState` | `string[]` | `null` | State properties to persist |
| `StateStorageKey` | `string` | `"GridState"` | localStorage key |

### 2.5 Performance Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `EnableLazyLoading` | `bool` | `false` | Lazy load hierarchical data |
| `BatchSize` | `int` | `100` | Records per batch request |
| `InitialPage` | `int` | `1` | Initial page number |
| `PageSize` | `int` | `20` | Records per page |
| `TotalRecords` | `int` | `0` | Total records for remote data |

---

## 3. Methods Reference

### 3.1 Data Operations Methods

#### `Refresh()`
Reloads the grid data from the data source.

**Signature:**
```csharp
public async Task Refresh()
```

**Usage:**
```csharp
@code {
    private SfGrid<Order> grid;
    
    private async Task ReloadData()
    {
        await grid.Refresh();
    }
}
```

#### `AddRecordAsync(TValue data)`
Programmatically adds a new record.

**Signature:**
```csharp
public async Task AddRecordAsync(TValue data)
```

**Usage:**
```csharp
var newOrder = new Order { OrderID = 1001, CustomerName = "John" };
await grid.AddRecordAsync(newOrder);
```

#### `UpdateRecordAsync(TValue data)`
Updates an existing record.

**Signature:**
```csharp
public async Task UpdateRecordAsync(TValue data)
```

**Usage:**
```csharp
order.Status = "Shipped";
await grid.UpdateRecordAsync(order);
```

#### `DeleteRecordAsync(object key)`
Deletes a record by primary key.

**Signature:**
```csharp
public async Task DeleteRecordAsync(object key)
```

**Usage:**
```csharp
await grid.DeleteRecordAsync(1001);
```

#### `GetSelectedRowIndexes()`
Returns indexes of selected rows.

**Signature:**
```csharp
public int[] GetSelectedRowIndexes()
```

**Usage:**
```csharp
var selectedIndexes = grid.GetSelectedRowIndexes();
```

#### `GetSelectedRecords()`
Returns selected row data.

**Signature:**
```csharp
public TValue[] GetSelectedRecords()
```

**Usage:**
```csharp
var selectedOrders = grid.GetSelectedRecords();
```

### 3.2 Export Methods

#### `ExcelExportAsync()`
Exports grid data to Excel.

**Signature:**
```csharp
public async Task ExcelExportAsync(ExcelExportProperties properties = null)
```

**Usage:**
```csharp
await grid.ExcelExportAsync();
```

**With Properties:**
```csharp
var exportProps = new ExcelExportProperties
{
    FileName = "Orders.xlsx",
    IncludeHiddenColumn = false
};
await grid.ExcelExportAsync(exportProps);
```

#### `PdfExportAsync()`
Exports grid data to PDF.

**Signature:**
```csharp
public async Task PdfExportAsync(PdfExportProperties properties = null)
```

**Usage:**
```csharp
await grid.PdfExportAsync();
```

#### `CsvExportAsync()`
Exports grid data to CSV.

**Signature:**
```csharp
public async Task CsvExportAsync(CsvExportProperties properties = null)
```

**Usage:**
```csharp
await grid.CsvExportAsync();
```

### 3.3 Column Methods

#### `ShowColumnsAsync(string[] fieldNames, bool show)`
Shows or hides columns.

**Signature:**
```csharp
public async Task ShowColumnsAsync(string[] fieldNames, bool show)
```

**Usage:**
```csharp
await grid.ShowColumnsAsync(new[] { "OrderID", "CustomerName" }, false);
```

#### `ReorderColumnAsync(string field, int newIndex)`
Moves column to new position.

**Signature:**
```csharp
public async Task ReorderColumnAsync(string field, int newIndex)
```

**Usage:**
```csharp
await grid.ReorderColumnAsync("OrderID", 0);
```

#### `SetColumnWidthAsync(string field, int width)`
Sets column width.

**Signature:**
```csharp
public async Task SetColumnWidthAsync(string field, int width)
```

**Usage:**
```csharp
await grid.SetColumnWidthAsync("OrderID", 150);
```

### 3.4 Selection Methods

#### `SelectRowsByIndexAsync(int[] indexes)`
Selects rows by index.

**Signature:**
```csharp
public async Task SelectRowsByIndexAsync(int[] indexes)
```

**Usage:**
```csharp
await grid.SelectRowsByIndexAsync(new[] { 0, 2, 4 });
```

#### `ClearSelectionAsync()`
Clears all selections.

**Signature:**
```csharp
public async Task ClearSelectionAsync()
```

**Usage:**
```csharp
await grid.ClearSelectionAsync();
```

#### `SelectRowsAsync(TValue[] data)`
Selects rows by data.

**Signature:**
```csharp
public async Task SelectRowsAsync(TValue[] data)
```

**Usage:**
```csharp
var ordersToSelect = orders.Take(3).ToArray();
await grid.SelectRowsAsync(ordersToSelect);
```

### 3.5 Grouping Methods

#### `GroupByAsync(string[] fieldNames)`
Groups data by specified fields.

**Signature:**
```csharp
public async Task GroupByAsync(string[] fieldNames)
```

**Usage:**
```csharp
await grid.GroupByAsync(new[] { "CustomerName", "OrderDate" });
```

#### `UngroupByAsync(string[] fieldNames)`
Removes grouping from specified fields.

**Signature:**
```csharp
public async Task UngroupByAsync(string[] fieldNames)
```

**Usage:**
```csharp
await grid.UngroupByAsync(new[] { "CustomerName" });
```

#### `CollapseAllGroupsAsync()`
Collapses all groups.

**Signature:**
```csharp
public async Task CollapseAllGroupsAsync()
```

**Usage:**
```csharp
await grid.CollapseAllGroupsAsync();
```

#### `ExpandAllGroupsAsync()`
Expands all groups.

**Signature:**
```csharp
public async Task ExpandAllGroupsAsync()
```

**Usage:**
```csharp
await grid.ExpandAllGroupsAsync();
```

### 3.6 Sorting Methods

#### `SortByColumnAsync(string field, SortDirection direction)`
Sorts grid by column.

**Signature:**
```csharp
public async Task SortByColumnAsync(string field, SortDirection direction)
```

**Usage:**
```csharp
await grid.SortByColumnAsync("OrderDate", SortDirection.Descending);
```

#### `ClearSortingAsync()`
Clears all sorting.

**Signature:**
```csharp
public async Task ClearSortingAsync()
```

**Usage:**
```csharp
await grid.ClearSortingAsync();
```

### 3.7 Filtering Methods

#### `FilterByColumnAsync(string field, string filterOperator, object value)`
Applies filter to column.

**Signature:**
```csharp
public async Task FilterByColumnAsync(string field, string filterOperator, object value)
```

**Usage:**
```csharp
await grid.FilterByColumnAsync("Status", "equal", "Shipped");
```

#### `ClearFilteringAsync()`
Clears all filters.

**Signature:**
```csharp
public async Task ClearFilteringAsync()
```

**Usage:**
```csharp
await grid.ClearFilteringAsync();
```

### 3.8 State Management Methods

#### `SaveStateAsync()`
Saves current state to localStorage.

**Signature:**
```csharp
public async Task SaveStateAsync()
```

**Usage:**
```csharp
await grid.SaveStateAsync();
```

#### `LoadStateAsync()`
Loads state from localStorage.

**Signature:**
```csharp
public async Task LoadStateAsync()
```

**Usage:**
```csharp
await grid.LoadStateAsync();
```

#### `ClearStateAsync()`
Clears saved state.

**Signature:**
```csharp
public async Task ClearStateAsync()
```

**Usage:**
```csharp
await grid.ClearStateAsync();
```

---

## 4. Events Reference

### 4.1 Data Events

#### `OnDataSourceChanged`
Fired when data source changes.

**Event Arguments:**
```csharp
public event EventCallback<DataSourceChangedEventArgs> OnDataSourceChanged
```

**Properties:**
- `Action`: Insert | Update | Delete
- `Data`: Changed record(s)
- `Cancel`: Cancel the operation

**Usage:**
```csharp
private async Task OnDataChanged(DataSourceChangedEventArgs args)
{
    if (args.Action == CRUDAction.Insert)
    {
        await DataService.AddAsync(args.Data);
    }
    else if (args.Action == CRUDAction.Update)
    {
        await DataService.UpdateAsync(args.Data);
    }
    else if (args.Action == CRUDAction.Delete)
    {
        await DataService.DeleteAsync(args.Data);
    }
}
```

#### `OnDataBound`
Fired after data is bound and rendered.

**Event Arguments:**
```csharp
public event EventCallback<GridDataBoundEventArgs> OnDataBound
```

**Usage:**
```csharp
private void OnGridDataBound(GridDataBoundEventArgs args)
{
    Console.WriteLine($"Grid bound with {args.TotalRecords} records");
}
```

#### `OnActionBegin`
Fired before grid action starts.

**Event Arguments:**
```csharp
public event EventCallback<GridActionEventArgs> OnActionBegin
```

**Properties:**
- `RequestType`: Paging | Sorting | Filtering | Grouping | etc.
- `Cancel`: Cancel the action

**Usage:**
```csharp
private void OnActionBegin(GridActionEventArgs args)
{
    if (args.RequestType == "delete")
    {
        var confirm = await JSRuntime.InvokeAsync<bool>("confirm", "Delete?");
        if (!confirm) args.Cancel = true;
    }
}
```

#### `OnActionComplete`
Fired after grid action completes.

**Event Arguments:**
```csharp
public event EventCallback<GridActionEventArgs> OnActionComplete
```

**Usage:**
```csharp
private void OnActionComplete(GridActionEventArgs args)
{
    Console.WriteLine($"Action {args.RequestType} completed");
}
```

#### `OnActionFailure`
Fired when grid action fails.

**Event Arguments:**
```csharp
public event EventCallback<GridActionFailureEventArgs> OnActionFailure
```

**Properties:**
- `Error`: Error message
- `StatusCode`: HTTP status code

**Usage:**
```csharp
private void OnActionFailure(GridActionFailureEventArgs args)
{
    Console.WriteLine($"Error: {args.Error}");
    NotificationService.ShowError(args.Error);
}
```

### 4.2 Selection Events

#### `OnRowSelected`
Fired when a row is selected.

**Event Arguments:**
```csharp
public event EventCallback<RowSelectedEventArgs> OnRowSelected
```

**Properties:**
- `Data`: Selected row data
- `Index`: Row index

**Usage:**
```csharp
private void OnRowSelect(RowSelectedEventArgs args)
{
    selectedOrder = args.Data;
    Console.WriteLine($"Selected order: {args.Data.OrderID}");
}
```

#### `OnRowDeselected`
Fired when a row is deselected.

**Event Arguments:**
```csharp
public event EventCallback<RowSelectedEventArgs> OnRowDeselected
```

**Usage:**
```csharp
private void OnRowDeselect(RowSelectedEventArgs args)
{
    Console.WriteLine($"Deselected order: {args.Data.OrderID}");
}
```

#### `OnCellSelected`
Fired when a cell is selected.

**Event Arguments:**
```csharp
public event EventCallback<CellSelectedEventArgs> OnCellSelected
```

**Properties:**
- `RowIndex`: Row index
- `ColumnIndex`: Column index
- `Value`: Cell value

**Usage:**
```csharp
private void OnCellSelect(CellSelectedEventArgs args)
{
    Console.WriteLine($"Cell [{args.RowIndex}, {args.ColumnIndex}]: {args.Value}");
}
```

### 4.3 Edit Events

#### `OnBeginEdit`
Fired when edit starts.

**Event Arguments:**
```csharp
public event EventCallback<BeginEditEventArgs> OnBeginEdit
```

**Properties:**
- `Data`: Row data being edited
- `Cancel`: Cancel edit

**Usage:**
```csharp
private void OnBeginEdit(BeginEditEventArgs args)
{
    if (args.Data.Status == "Locked")
    {
        args.Cancel = true;
        NotificationService.ShowWarning("Cannot edit locked records");
    }
}
```

#### `OnEndEdit`
Fired when edit ends.

**Event Arguments:**
```csharp
public event EventCallback<EndEditEventArgs> OnEndEdit
```

**Properties:**
- `Data`: Modified data
- `Action`: Save | Cancel
- `Cancel`: Cancel the action

**Usage:**
```csharp
private async Task OnEndEdit(EndEditEventArgs args)
{
    if (args.Action == CRUDAction.Update)
    {
        var isValid = await ValidateOrder(args.Data);
        if (!isValid) args.Cancel = true;
    }
}
```

#### `OnValidateForm`
Fired during form validation.

**Event Arguments:**
```csharp
public event EventCallback<ValidateFormEventArgs> OnValidateForm
```

**Properties:**
- `Model`: Data model
- `Errors`: Validation errors dictionary

**Usage:**
```csharp
private void OnValidateForm(ValidateFormEventArgs args)
{
    if (args.Model.OrderAmount < 0)
    {
        args.Errors.Add("OrderAmount", "Amount cannot be negative");
    }
}
```

### 4.4 Column Events

#### `OnColumnResize`
Fired when column is resized.

**Event Arguments:**
```csharp
public event EventCallback<ColumnResizeEventArgs> OnColumnResize
```

**Properties:**
- `Field`: Column field name
- `Width`: New width

**Usage:**
```csharp
private void OnColumnResize(ColumnResizeEventArgs args)
{
    Console.WriteLine($"Column {args.Field} resized to {args.Width}px");
}
```

#### `OnColumnReorder`
Fired when column is reordered.

**Event Arguments:**
```csharp
public event EventCallback<ColumnReorderEventArgs> OnColumnReorder
```

**Properties:**
- `Field`: Column field name
- `FromIndex`: Original index
- `ToIndex`: New index

**Usage:**
```csharp
private void OnColumnReorder(ColumnReorderEventArgs args)
{
    Console.WriteLine($"Column {args.Field} moved from {args.FromIndex} to {args.ToIndex}");
}
```

### 4.5 Export Events

#### `OnExcelExportComplete`
Fired after Excel export completes.

**Event Arguments:**
```csharp
public event EventCallback<ExportCompleteEventArgs> OnExcelExportComplete
```

**Properties:**
- `FileName`: Exported file name
- `Success`: Export success status

**Usage:**
```csharp
private void OnExcelExportComplete(ExportCompleteEventArgs args)
{
    if (args.Success)
    {
        NotificationService.ShowSuccess($"Exported {args.FileName}");
    }
}
```

#### `OnPdfExportComplete`
Fired after PDF export completes.

**Event Arguments:**
```csharp
public event EventCallback<ExportCompleteEventArgs> OnPdfExportComplete
```

**Usage:**
```csharp
private void OnPdfExportComplete(ExportCompleteEventArgs args)
{
    Console.WriteLine($"PDF export completed: {args.FileName}");
}
```

### 4.6 Navigation Events

#### `OnCellNavigated`
Fired when cell navigation occurs.

**Event Arguments:**
```csharp
public event EventCallback<CellNavigateEventArgs> OnCellNavigated
```

**Properties:**
- `RowIndex`: Current row index
- `ColumnIndex`: Current column index
- `Direction`: Navigation direction

**Usage:**
```csharp
private void OnCellNavigated(CellNavigateEventArgs args)
{
    Console.WriteLine($"Navigated to cell [{args.RowIndex}, {args.ColumnIndex}]");
}
```

#### `OnRowFocus`
Fired when row receives focus.

**Event Arguments:**
```csharp
public event EventCallback<RowFocusEventArgs> OnRowFocus
```

**Usage:**
```csharp
private void OnRowFocus(RowFocusEventArgs args)
{
    Console.WriteLine($"Row {args.Index} focused");
}
```

---

## 5. Enumerations

### 5.1 EditMode
Defines the edit mode for the grid.

```csharp
public enum EditMode
{
    Normal,      // Single row edit
    Batch,       // Multiple row batch edit
    Dialog,      // Edit in dialog
    Inline       // Edit in place
}
```

### 5.2 EditType
Defines the edit type.

```csharp
public enum EditType
{
    DefaultEdit,
    DialogEdit,
    InlineEdit
}
```

### 5.3 SelectionMode
Defines selection mode.

```csharp
public enum SelectionMode
{
    Single,      // Single row selection
    Multiple     // Multiple row selection
}
```

### 5.4 SelectionType
Defines selection type.

```csharp
public enum SelectionType
{
    Row,         // Row selection
    Cell         // Cell selection
}
```

### 5.5 FilterMode
Defines filter UI mode.

```csharp
public enum FilterMode
{
    FilterMenu,           // Filter menu
    Excel,                // Excel-style filter
    CheckBox,             // Checkbox filter
    Search                // Search box filter
}
```

### 5.6 SortMode
Defines sort mode.

```csharp
public enum SortMode
{
    Single,      // Single column sort
    Multiple     // Multi-column sort
}
```

### 5.7 SortDirection
Defines sort direction.

```csharp
public enum SortDirection
{
    Ascending,
    Descending
}
```

### 5.8 GridLine
Defines grid border style.

```csharp
public enum GridLine
{
    None,        // No borders
    Horizontal,  // Horizontal borders only
    Vertical,    // Vertical borders only
    Both         // Both borders
}
```

### 5.9 NavigationMode
Defines keyboard navigation mode.

```csharp
public enum NavigationMode
{
    Row,         // Navigate by row
    Cell         // Navigate by cell
}
```

### 5.10 CRUDAction
Defines CRUD action types.

```csharp
public enum CRUDAction
{
    Insert,
    Update,
    Delete,
    Save,
    Cancel,
    BeginEdit,
    EndEdit
}
```

### 5.11 AggregateType
Defines aggregate function types.

```csharp
public enum AggregateType
{
    Sum,
    Average,
    Min,
    Max,
    Count,
    True,
    False
}
```

### 5.12 FilterOperator
Defines filter operators.

```csharp
public enum FilterOperator
{
    Equal,
    NotEqual,
    GreaterThan,
    GreaterThanOrEqual,
    LessThan,
    LessThanOrEqual,
    Contains,
    EndsWith,
    StartsWith,
    DoesNotContain,
    IsNull,
    IsNotNull,
    Empty,
    NotEmpty,
    Between,
    Before,
    After
}
```

---

## 6. Code Examples

### 6.1 Basic Grid with CRUD

```razor
@page "/orders"
@using Syncfusion.Blazor.Grids
@using Syncfusion.Blazor.Buttons

<SfGrid TValue="Order" DataSource="@orders" AllowPaging="true" Height="600px">
    <GridEditSettings AllowAdding="true" AllowEditing="true" AllowDeleting="true" 
                      Mode="EditMode.Dialog" />
    <GridToolbarTemplate>
        <GridToolbarItems>
            <GridToolbarItem Text="Add" IconCss="e-icons e-add" />
            <GridToolbarItem Text="Edit" IconCss="e-icons e-edit" />
            <GridToolbarItem Text="Delete" IconCss="e-icons e-delete" />
            <GridToolbarItem Text="ExcelExport" IconCss="e-icons e-export" />
        </GridToolbarItems>
    </GridToolbarTemplate>
    <GridColumns>
        <GridColumn Field=@nameof(Order.OrderID) HeaderText="Order ID" IsPrimaryKey="true" 
                    IsReadOnly="true" Width="100" />
        <GridColumn Field=@nameof(Order.CustomerName) HeaderText="Customer" Width="150" />
        <GridColumn Field=@nameof(Order.OrderDate) HeaderText="Order Date" Format="d" 
                    Width="120" />
        <GridColumn Field=@nameof(Order.Amount) HeaderText="Amount" Format="c2" 
                    Width="100" TextAlign="TextAlign.Right" />
        <GridColumn Field=@nameof(Order.Status) HeaderText="Status" Width="120">
            <Template>
                @{
                    var order = (context as Order);
                    <span class="status-@order.Status.ToLower()">@order.Status</span>
                }
            </Template>
        </GridColumn>
    </GridColumns>
</SfGrid>

@code {
    private List<Order> orders;

    protected override async Task OnInitializedAsync()
    {
        orders = await OrderService.GetOrdersAsync();
    }

    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
    }
}
```

### 6.2 Remote Data with OData

```razor
@page "/remote-orders"
@using Syncfusion.Blazor.Grids
@using Syncfusion.Blazor.Data

<SfGrid TValue="Order" AllowPaging="true" AllowSorting="true" AllowFiltering="true" 
        Height="600px">
    <SfDataManager Url="https://api.example.com/orders" 
                   Adaptor="Adaptors.WebApiAdaptor"
                   CrossDomain="true"
                   EnableCaching="true"
                   CacheMode="CacheMode.Sliding"
                   BatchSize="100">
        <DataManagerEvents OnAdaptorDataBound="OnDataBound" />
    </SfDataManager>
    <GridColumns>
        <GridColumn Field=@nameof(Order.OrderID) HeaderText="Order ID" IsPrimaryKey="true" />
        <GridColumn Field=@nameof(Order.CustomerName) HeaderText="Customer" />
        <GridColumn Field=@nameof(Order.OrderDate) HeaderText="Order Date" Format="d" />
        <GridColumn Field=@nameof(Order.Amount) HeaderText="Amount" Format="c2" />
    </GridColumns>
</SfGrid>

@code {
    private async Task OnDataBound(DataBoundEventArgs args)
    {
        Console.WriteLine($"Loaded {args.TotalRecords} records");
    }

    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Amount { get; set; }
    }
}
```

### 6.3 Grouping with Aggregates

```razor
@page "/grouped-orders"
@using Syncfusion.Blazor.Grids

<SfGrid TValue="Order" DataSource="@orders" AllowGrouping="true" Height="600px">
    <GridGroupSettings>
        <GridGroupSettings Columns="@(new string[] { "CustomerName", "Status" })" />
    </GridGroupSettings>
    <GridAggregates>
        <GridAggregates>
            <GridAggregate Column=@nameof(Order.Amount) Type="AggregateType.Sum">
                <GridAggregateFormat Format="c2" />
            </GridAggregate>
            <GridAggregate Column=@nameof(Order.Amount) Type="AggregateType.Average">
                <GridAggregateFormat Format="c2" />
            </GridAggregate>
            <GridAggregate Column=@nameof(Order.OrderID) Type="AggregateType.Count" />
        </GridAggregates>
    </GridAggregates>
    <GridColumns>
        <GridColumn Field=@nameof(Order.OrderID) HeaderText="Order ID" Width="100" />
        <GridColumn Field=@nameof(Order.CustomerName) HeaderText="Customer" Width="150" />
        <GridColumn Field=@nameof(Order.Status) HeaderText="Status" Width="120" />
        <GridColumn Field=@nameof(Order.OrderDate) HeaderText="Date" Format="d" Width="120" />
        <GridColumn Field=@nameof(Order.Amount) HeaderText="Amount" Format="c2" Width="100" />
    </GridColumns>
</SfGrid>

@code {
    private List<Order> orders;

    protected override async Task OnInitializedAsync()
    {
        orders = await OrderService.GetOrdersAsync();
    }

    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Amount { get; set; }
    }
}
```

### 6.4 Master-Detail with Template

```razor
@page "/orders-with-details"
@using Syncfusion.Blazor.Grids

<SfGrid TValue="Order" DataSource="@orders" Height="600px">
    <GridDetailTemplate>
        <Template>
            @{
                var order = (context as Order);
                <div class="order-details">
                    <h4>Order Details for #@order.OrderID</h4>
                    <SfGrid TValue="OrderItem" DataSource="@order.Items" 
                            ShowHeader="false" Height="200px">
                        <GridColumns>
                            <GridColumn Field=@nameof(OrderItem.Product) 
                                        HeaderText="Product" />
                            <GridColumn Field=@nameof(OrderItem.Quantity) 
                                        HeaderText="Qty" />
                            <GridColumn Field=@nameof(OrderItem.Price) 
                                        HeaderText="Price" Format="c2" />
                        </GridColumns>
                    </SfGrid>
                </div>
            }
        </Template>
    </GridDetailTemplate>
    <GridColumns>
        <GridColumn Field=@nameof(Order.OrderID) HeaderText="Order ID" Width="100" />
        <GridColumn Field=@nameof(Order.CustomerName) HeaderText="Customer" Width="150" />
        <GridColumn Field=@nameof(Order.OrderDate) HeaderText="Date" Width="120" />
        <GridColumn Field=@nameof(Order.Total) HeaderText="Total" Format="c2" Width="100" />
    </GridColumns>
</SfGrid>

@code {
    private List<Order> orders;

    protected override async Task OnInitializedAsync()
    {
        orders = await OrderService.GetOrdersWithItemsAsync();
    }

    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public List<OrderItem> Items { get; set; }
    }

    public class OrderItem
    {
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
```

### 6.5 Batch Editing

```razor
@page "/batch-edit"
@using Syncfusion.Blazor.Grids

<SfGrid TValue="Product" DataSource="@products" Height="600px">
    <GridEditSettings AllowAdding="true" AllowEditing="true" AllowDeleting="true" 
                      Mode="EditMode.Batch" />
    <GridToolbarTemplate>
        <GridToolbarItems>
            <GridToolbarItem Text="Add" IconCss="e-icons e-add" />
            <GridToolbarItem Text="Update" IconCss="e-icons e-update" />
            <GridToolbarItem Text="Cancel" IconCss="e-icons e-cancel" />
        </GridToolbarItems>
    </GridToolbarTemplate>
    <GridColumns>
        <GridColumn Field=@nameof(Product.ProductID) HeaderText="ID" IsPrimaryKey="true" 
                    IsReadOnly="true" Width="80" />
        <GridColumn Field=@nameof(Product.ProductName) HeaderText="Product" Width="200">
            <EditTemplate>
                <SfTextBox @bind-Value="@(context.ProductName)" />
            </EditTemplate>
        </GridColumn>
        <GridColumn Field=@nameof(Product.Category) HeaderText="Category" Width="150">
            <EditTemplate>
                <SfDropDownList TItem="Category" TValue="string" 
                                DataSource="@categories"
                                @bind-Value="@(context.Category)"
                                Placeholder="Select category">
                    <DropDownListFieldSettings Text="Name" Value="Name" />
                </SfDropDownList>
            </EditTemplate>
        </GridColumn>
        <GridColumn Field=@nameof(Product.Price) HeaderText="Price" Format="c2" Width="100">
            <EditTemplate>
                <SfNumericTextBox @bind-Value="@(context.Price)" Format="c2" />
            </EditTemplate>
        </GridColumn>
        <GridColumn Field=@nameof(Product.InStock) HeaderText="In Stock" Width="100">
            <EditTemplate>
                <SfCheckBox @bind-Checked="@(context.InStock)" />
            </EditTemplate>
        </GridColumn>
    </GridColumns>
</SfGrid>

@code {
    private List<Product> products;
    private List<Category> categories;

    protected override async Task OnInitializedAsync()
    {
        products = await ProductService.GetProductsAsync();
        categories = await CategoryService.GetCategoriesAsync();
    }

    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public bool InStock { get; set; }
    }

    public class Category
    {
        public string Name { get; set; }
    }
}
```

### 6.6 Virtualization for Large Dataset

```razor
@page "/large-dataset"
@using Syncfusion.Blazor.Grids

<SfGrid TValue="Record" DataSource="@records" EnableVirtualization="true" 
        Height="600px" RowHeight="40">
    <GridPageSettings PageSize="100" />
    <GridColumns>
        <GridColumn Field=@nameof(record.Id) HeaderText="ID" Width="80" />
        <GridColumn Field=@nameof(record.Name) HeaderText="Name" Width="200" />
        <GridColumn Field=@nameof(record.Value) HeaderText="Value" Width="100" />
        <GridColumn Field=@nameof(record.Date) HeaderText="Date" Format="d" Width="120" />
        <GridColumn Field=@nameof(record.Status) HeaderText="Status" Width="100" />
    </GridColumns>
</SfGrid>

@code {
    private List<Record> records;

    protected override async Task OnInitializedAsync()
    {
        // Load 100,000 records
        records = await RecordService.GetLargeDatasetAsync(100000);
    }

    public class Record
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
```

### 6.7 Custom Filtering with Template

```razor
@page "/custom-filter"
@using Syncfusion.Blazor.Grids
@using Syncfusion.Blazor.Inputs

<SfGrid TValue="Order" DataSource="@orders" AllowFiltering="true" 
        FilterMode="FilterMode.Excel" Height="600px">
    <GridFilterSettings>
        <GridFilterSettings Type="FilterType.Excel" />
    </GridFilterSettings>
    <GridColumns>
        <GridColumn Field=@nameof(Order.OrderID) HeaderText="Order ID" Width="100" />
        <GridColumn Field=@nameof(Order.CustomerName) HeaderText="Customer" Width="150" />
        <GridColumn Field=@nameof(Order.Status) HeaderText="Status" Width="120">
            <FilterTemplate>
                @{
                    var column = (context as GridColumn);
                    <SfDropDownList TItem="string" TValue="string" 
                                    DataSource="@statusOptions"
                                    Placeholder="Select status"
                                    Change="@((args) => OnStatusFilterChange(args, column))">
                    </SfDropDownList>
                }
            </FilterTemplate>
        </GridColumn>
        <GridColumn Field=@nameof(Order.Amount) HeaderText="Amount" Format="c2" Width="100" />
    </GridColumns>
</SfGrid>

@code {
    private List<Order> orders;
    private List<string> statusOptions = new() { "Pending", "Processing", "Shipped", "Delivered" };

    protected override async Task OnInitializedAsync()
    {
        orders = await OrderService.GetOrdersAsync();
    }

    private async Task OnStatusFilterChange(ChangeEventArgs args, GridColumn column)
    {
        var filterValue = args.Value?.ToString();
        if (!string.IsNullOrEmpty(filterValue))
        {
            await grid.FilterByColumnAsync(nameof(Order.Status), "equal", filterValue);
        }
    }

    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
    }
}
```

### 6.8 State Persistence

```razor
@page "/persistent-grid"
@using Syncfusion.Blazor.Grids

<SfGrid TValue="Order" DataSource="@orders" EnablePersistence="true" 
        StateStorageKey="OrdersGridState" Height="600px">
    <GridPageSettings PageSize="20" />
    <GridColumns>
        <GridColumn Field=@nameof(Order.OrderID) HeaderText="Order ID" Width="100" />
        <GridColumn Field=@nameof(Order.CustomerName) HeaderText="Customer" Width="150" />
        <GridColumn Field=@nameof(Order.OrderDate) HeaderText="Date" Width="120" />
        <GridColumn Field=@nameof(Order.Amount) HeaderText="Amount" Width="100" />
    </GridColumns>
</SfGrid>

<button @onclick="SaveState">Save State</button>
<button @onclick="LoadState">Load State</button>
<button @onclick="ClearState">Clear State</button>

@code {
    private SfGrid<Order> grid;
    private List<Order> orders;

    protected override async Task OnInitializedAsync()
    {
        orders = await OrderService.GetOrdersAsync();
    }

    private async Task SaveState()
    {
        await grid.SaveStateAsync();
        Console.WriteLine("State saved");
    }

    private async Task LoadState()
    {
        await grid.LoadStateAsync();
        Console.WriteLine("State loaded");
    }

    private async Task ClearState()
    {
        await grid.ClearStateAsync();
        Console.WriteLine("State cleared");
    }

    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Amount { get; set; }
    }
}
```

### 6.9 Responsive Grid with Breakpoints

```razor
@page "/responsive-grid"
@using Syncfusion.Blazor.Grids

<SfGrid TValue="Product" DataSource="@products" Height="600px" CssClass="responsive-grid">
    <GridColumns>
        <GridColumn Field=@nameof(Product.ProductID) HeaderText="ID" Width="80" 
                    Visible="@isDesktop" />
        <GridColumn Field=@nameof(Product.ProductName) HeaderText="Product" Width="200" />
        <GridColumn Field=@nameof(Product.Category) HeaderText="Category" Width="150" 
                    Visible="@isTabletOrDesktop" />
        <GridColumn Field=@nameof(Product.Price) HeaderText="Price" Format="c2" Width="100" />
        <GridColumn Field=@nameof(Product.Stock) HeaderText="Stock" Width="100" 
                    Visible="@isDesktop" />
    </GridColumns>
</SfGrid>

@code {
    private List<Product> products;
    private bool isDesktop = true;
    private bool isTabletOrDesktop = true;

    protected override async Task OnInitializedAsync()
    {
        products = await ProductService.GetProductsAsync();
        
        // Check screen size
        var screenWidth = await JSRuntime.InvokeAsync<int>("eval", "window.innerWidth");
        isDesktop = screenWidth >= 1024;
        isTabletOrDesktop = screenWidth >= 768;
    }

    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}

<style>
    .responsive-grid .e-grid {
        font-size: 14px;
    }
    
    @@media (max-width: 768px) {
        .responsive-grid .e-grid {
            font-size: 12px;
        }
    }
</style>
```

### 6.10 Server-Side Operations

```razor
@page "/server-side-grid"
@using Syncfusion.Blazor.Grids
@using Syncfusion.Blazor.Data

<SfGrid TValue="Order" AllowPaging="true" AllowSorting="true" AllowFiltering="true" 
        Height="600px" TotalRecords="@totalRecords">
    <SfDataManager Url="https://api.example.com/orders" 
                   Adaptor="Adaptors.WebApiAdaptor"
                   EnableCaching="false">
        <DataManagerEvents OnAdaptorDataBound="OnDataBound" />
    </SfDataManager>
    <GridPageSettings PageSize="50" />
    <GridColumns>
        <GridColumn Field=@nameof(Order.OrderID) HeaderText="Order ID" IsPrimaryKey="true" />
        <GridColumn Field=@nameof(Order.CustomerName) HeaderText="Customer" />
        <GridColumn Field=@nameof(Order.OrderDate) HeaderText="Date" Format="d" />
        <GridColumn Field=@nameof(Order.Amount) HeaderText="Amount" Format="c2" />
        <GridColumn Field=@nameof(Order.Status) HeaderText="Status" />
    </GridColumns>
</SfGrid>

@code {
    private int totalRecords;

    private void OnDataBound(DataBoundEventArgs args)
    {
        totalRecords = args.TotalRecords;
        StateHasChanged();
    }

    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
    }
}
```

---

## 7. Validation Rules

### 7.1 Built-in Validation Attributes

The grid supports standard .NET data annotations:

```csharp
public class Order
{
    [Required(ErrorMessage = "Order ID is required")]
    public int OrderID { get; set; }

    [Required(ErrorMessage = "Customer name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be 2-100 characters")]
    public string CustomerName { get; set; }

    [Required(ErrorMessage = "Order date is required")]
    [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
    public DateTime OrderDate { get; set; }

    [Range(0.01, 1000000, ErrorMessage = "Amount must be between 0.01 and 1,000,000")]
    public decimal Amount { get; set; }

    [RegularExpression(@"^(Pending|Processing|Shipped|Delivered)$", 
                        ErrorMessage = "Invalid status")]
    public string Status { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string CustomerEmail { get; set; }

    [Phone(ErrorMessage = "Invalid phone number")]
    public string CustomerPhone { get; set; }
}
```

### 7.2 Custom Validation

```csharp
private void OnValidateForm(ValidateFormEventArgs args)
{
    var order = args.Model as Order;
    
    // Custom validation logic
    if (order.OrderDate < DateTime.Today.AddYears(-1))
    {
        args.Errors.Add(nameof(order.OrderDate), "Order date cannot be older than 1 year");
    }
    
    if (order.Amount > 10000 && order.Status != "Approved")
    {
        args.Errors.Add(nameof(order.Status), "Large orders require approval");
    }
    
    if (string.IsNullOrEmpty(order.CustomerEmail) && string.IsNullOrEmpty(order.CustomerPhone))
    {
        args.Errors.Add("Contact", "Either email or phone is required");
    }
}
```

### 7.3 Async Validation

```csharp
private async Task OnEndEdit(EndEditEventArgs args)
{
    var order = args.Data as Order;
    
    // Async validation - check if customer exists
    var customerExists = await CustomerService.ExistsAsync(order.CustomerName);
    if (!customerExists)
    {
        args.Cancel = true;
        NotificationService.ShowError("Customer does not exist");
    }
    
    // Check for duplicate order number
    var isDuplicate = await OrderService.IsDuplicateOrderNumberAsync(order.OrderID);
    if (isDuplicate)
    {
        args.Cancel = true;
        NotificationService.ShowError("Duplicate order number");
    }
}
```

---

## 8. Integration Patterns

### 8.1 Dependency Injection Pattern

```csharp
// Program.cs
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddSyncfusionBlazor();

// Component
@inject IOrderService OrderService

<SfGrid TValue="Order" DataSource="@orders">
    ...
</SfGrid>

@code {
    private List<Order> orders;

    protected override async Task OnInitializedAsync()
    {
        orders = await OrderService.GetOrdersAsync();
    }
}
```

### 8.2 Repository Pattern

```csharp
// Repository
public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}

// Component
@inject IRepository<Order> OrderRepository

<SfGrid TValue="Order" DataSource="@orders">
    <GridEditSettings AllowAdding="true" AllowEditing="true" AllowDeleting="true" />
    ...
</SfGrid>

@code {
    private List<Order> orders;

    protected override async Task OnInitializedAsync()
    {
        orders = (await OrderRepository.GetAllAsync()).ToList();
    }

    private async Task OnDataChanged(DataSourceChangedEventArgs args)
    {
        if (args.Action == CRUDAction.Insert)
        {
            await OrderRepository.AddAsync(args.Data as Order);
        }
        else if (args.Action == CRUDAction.Update)
        {
            await OrderRepository.UpdateAsync(args.Data as Order);
        }
        else if (args.Action == CRUDAction.Delete)
        {
            await OrderRepository.DeleteAsync((args.Data as Order).OrderID);
        }
    }
}
```

### 8.3 Unit of Work Pattern

```csharp
@inject IUnitOfWork UnitOfWork

<SfGrid TValue="Order" DataSource="@orders">
    <GridEditSettings AllowAdding="true" AllowEditing="true" AllowDeleting="true" />
    ...
</SfGrid>

@code {
    private List<Order> orders;

    private async Task OnDataChanged(DataSourceChangedEventArgs args)
    {
        try
        {
            if (args.Action == CRUDAction.Insert)
            {
                UnitOfWork.Orders.Add(args.Data as Order);
            }
            else if (args.Action == CRUDAction.Update)
            {
                UnitOfWork.Orders.Update(args.Data as Order);
            }
            else if (args.Action == CRUDAction.Delete)
            {
                UnitOfWork.Orders.Delete(args.Data as Order);
            }
            
            await UnitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            await UnitOfWork.RollbackAsync();
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
```

### 8.4 CQRS Pattern

```csharp
@inject IMediator Mediator

<SfGrid TValue="Order" DataSource="@orders">
    ...
</SfGrid>

@code {
    private List<Order> orders;

    protected override async Task OnInitializedAsync()
    {
        var query = new GetOrdersQuery();
        orders = await Mediator.Send(query);
    }

    private async Task OnDataChanged(DataSourceChangedEventArgs args)
    {
        if (args.Action == CRUDAction.Insert)
        {
            var command = new CreateOrderCommand(args.Data as Order);
            await Mediator.Send(command);
        }
        else if (args.Action == CRUDAction.Update)
        {
            var command = new UpdateOrderCommand(args.Data as Order);
            await Mediator.Send(command);
        }
        else if (args.Action == CRUDAction.Delete)
        {
            var command = new DeleteOrderCommand((args.Data as Order).OrderID);
            await Mediator.Send(command);
        }
    }
}
```

---

## 9. Performance Guidelines

### 9.1 Data Volume Recommendations

| Records | Recommended Approach |
|---------|---------------------|
| 0-100 | In-memory, client-side operations |
| 100-1,000 | In-memory with virtualization |
| 1,000-10,000 | Server-side paging + virtualization |
| 10,000-100,000 | Server-side all operations |
| 100,000+ | Server-side + infinite scrolling |

### 9.2 Column Count Recommendations

| Columns | Recommended Approach |
|---------|---------------------|
| 0-10 | Standard rendering |
| 10-20 | Enable column virtualization |
| 20-50 | Column virtualization + frozen columns |
| 50+ | Consider redesign or tabs |

### 9.3 Optimization Techniques

**1. Enable Virtualization:**
```razor
<SfGrid EnableVirtualization="true" RowHeight="40" Height="600px">
```

**2. Use Server-Side Operations:**
```razor
<SfGrid DataSource="@remoteData" AllowPaging="true" AllowSorting="true" AllowFiltering="true">
    <SfDataManager Url="api/orders" Adaptor="Adaptors.WebApiAdaptor" />
</SfGrid>
```

**3. Limit Visible Columns:**
```razor
<GridColumn Field=@nameof(Order.ID) Visible="false" />
```

**4. Optimize Templates:**
```razor
<!-- Avoid complex logic in templates -->
<Template>
    @{
        var order = context as Order;
        <span>@order.CustomerName</span> <!-- Simple is better -->
    }
</Template>
```

**5. Use Readonly for Non-Editable Columns:**
```razor
<GridColumn Field=@nameof(Order.ID) IsReadOnly="true" />
```

**6. Batch Multiple Operations:**
```csharp
await Task.WhenAll(
    grid.ShowColumnsAsync(fieldsToShow, true),
    grid.HideColumnsAsync(fieldsToHide, true)
);
```

### 9.4 Memory Management

**1. Dispose Grid Properly:**
```csharp
@implements IDisposable

public void Dispose()
{
    grid?.Dispose();
}
```

**2. Clear Data When Not Needed:**
```csharp
private void ClearGrid()
{
    orders.Clear();
    grid.Refresh();
}
```

**3. Limit Batch Size:**
```razor
<SfDataManager BatchSize="100" />
```

### 9.5 Rendering Optimization

**1. Use TrackBy for Lists:**
```csharp
// In data source, ensure unique IDs
orders = orders.OrderBy(x => x.OrderID).ToList();
```

**2. Minimize StateHasChanged Calls:**
```csharp
// Batch updates
private async Task UpdateMultiple()
{
    // Make all changes
    // Then call once
    StateHasChanged();
}
```

**3. Lazy Load Hierarchical Data:**
```razor
<SfGrid EnableLazyLoading="true">
    <GridDetailTemplate>
        <Template>
            @{
                var order = context as Order;
                // Load details only when expanded
                <SfGrid DataSource="@order.Details" />
            }
        </Template>
    </GridDetailTemplate>
</SfGrid>
```

---

## 10. API Compatibility

### 10.1 Supported .NET Versions

| .NET Version | Support Status | Notes |
|--------------|----------------|-------|
| .NET 8.0 | ✅ Full Support | Recommended |
| .NET 9.0 | ✅ Full Support | Recommended |
| .NET 10.0 | ✅ Full Support | Latest |
| .NET 7.0 | ⚠️ Limited | Deprecated |
| .NET 6.0 | ⚠️ Limited | Deprecated |
| .NET 5.0 | ❌ Not Supported | End of life |
| .NET Core 3.1 | ❌ Not Supported | End of life |

### 10.2 Blazor Rendering Modes

| Mode | Support | Notes |
|------|---------|-------|
| Blazor Server | ✅ Full Support | Recommended for enterprise |
| Blazor WebAssembly | ✅ Full Support | Recommended for offline |
| Blazor Hybrid (MAUI) | ✅ Full Support | Mobile/desktop apps |

### 10.3 Browser Compatibility

| Browser | Version | Support |
|---------|---------|---------|
| Chrome | 90+ | ✅ Full |
| Firefox | 88+ | ✅ Full |
| Safari | 14+ | ✅ Full |
| Edge | 90+ | ✅ Full |
| IE 11 | 11 | ⚠️ Limited (deprecated) |

### 10.4 Breaking Changes by Version

**Version 18.2.0.56:**
- No breaking changes from 18.1.x

**Version 17.x to 18.x:**
- `GridFilterSettings.Type` enum changed
- `EditSettings.Mode` property renamed
- Event argument properties updated

**Migration Guide:**
```csharp
// Old (v17)
<GridFilterSettings FilterType="FilterType.Menu" />

// New (v18)
<GridFilterSettings Type="FilterType.Excel" />
```

### 10.5 Deprecated Features

| Feature | Deprecated In | Removal In | Alternative |
|---------|---------------|------------|-------------|
| IE 11 Support | 18.0 | 19.0 | Modern browsers |
| Classic Edit Mode | 17.2 | 18.5 | Dialog/Inline mode |
| Legacy Adaptor | 18.0 | 19.0 | WebApiAdaptor |
| Old Export API | 17.4 | 18.2 | New Export API |

---

## 11. Troubleshooting Technical Issues

### 11.1 Common Technical Issues

**Issue: Grid not rendering**
```csharp
// Check:
// 1. SfGrid tag properly closed
// 2. TValue specified
// 3. DataSource not null
// 4. Browser console for errors
```

**Issue: Events not firing**
```csharp
// Check:
// 1. EventCallback properly bound
// 2. Async/await used correctly
// 3. Event arguments match signature
```

**Issue: Edit not saving**
```csharp
// Check:
// 1. PrimaryKey defined
// 2. OnDataSourceChanged implemented
// 3. Validation passing
// 4. Data source updated
```

**Issue: Export failing**
```csharp
// Check:
// 1. AllowExcelExport/AllowPdfExport enabled
// 2. Browser popup blocker disabled
// 3. Memory sufficient for large exports
// 4. Server-side export for large datasets
```

### 11.2 Debugging Tips

**1. Enable Debug Mode:**
```razor
<SfGrid ... CssClass="debug-grid">
```

```css
.debug-grid .e-grid {
    border: 2px solid red;
}
```

**2. Log Events:**
```csharp
private void OnActionBegin(GridActionEventArgs args)
{
    Console.WriteLine($"Action Begin: {args.RequestType}");
}
```

**3. Check State:**
```csharp
private async Task CheckGridState()
{
    var state = await grid.GetStateAsync();
    Console.WriteLine($"Current state: {state}");
}
```

---

## 12. Conclusion

This Technical Specification provides comprehensive implementation details for the **Syncfusion Blazor Grid Component** version 18.2.0.56. It includes:

- ✅ Complete properties reference
- ✅ All methods with signatures and examples
- ✅ Event documentation with usage patterns
- ✅ Enumeration definitions
- ✅ Extensive code examples for common scenarios
- ✅ Validation rules and patterns
- ✅ Integration architecture patterns
- ✅ Performance optimization guidelines
- ✅ API compatibility matrix
- ✅ Troubleshooting guide

### 12.1 Document Usage

This specification serves as:
1. **Developer Reference:** Daily reference for implementation
2. **Code Review Guide:** Standards for code quality
3. **Testing Specification:** Test case development
4. **Training Material:** Developer onboarding
5. **API Documentation:** Integration reference

### 12.2 Maintenance

Update this document when:
- New properties/methods added
- Breaking changes introduced
- New integration patterns discovered
- Performance optimizations identified
- Bug fixes affect API

### 12.3 Related Documents

- **Product Requirements Document (grid-prd.md):** Business requirements
- **API Reference:** Complete method signatures
- **User Guide:** End-user documentation
- **Release Notes:** Version changes

---

**Document Version:** 1.0  
**Component Version:** 18.2.0.56  
**Last Updated:** March 10, 2026  
**Status:** Complete - Approved for Reference

---

## Appendix: Quick Reference

### A.1 Most Used Properties
```
DataSource, AllowPaging, AllowSorting, AllowFiltering, 
AllowGrouping, Height, Width, EditMode, PrimaryKey
```

### A.2 Most Used Methods
```
Refresh(), ExcelExportAsync(), PdfExportAsync(), 
GetSelectedRecords(), ClearSelectionAsync()
```

### A.3 Most Used Events
```
OnDataSourceChanged, OnActionBegin, OnActionComplete, 
OnRowSelected, OnBeginEdit, OnEndEdit
```

### A.4 Common Enumerations
```
EditMode, SelectionMode, FilterMode, SortMode, 
AggregateType, FilterOperator
```

### A.5 Performance Checklist
```
✓ Enable virtualization for 1000+ rows
✓ Use server-side for 10000+ rows
✓ Enable column virtualization for 20+ columns
✓ Set RowHeight for virtualization
✓ Use BatchSize for remote data
✓ Optimize templates
✓ Enable persistence selectively
```
