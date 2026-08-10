# Syncfusion Blazor Components - System Architecture

## Overview

Syncfusion.Blazor is a comprehensive UI component library providing 100+ native Blazor components that support multiple render modes (Server, WebAssembly, and Auto). The library is designed as a high-performance, enterprise-grade solution targeting .NET 8, 9, and 10 frameworks.

## Architecture Layers

### 1. Component Layer (C#/Razor)
The component layer consists of Blazor components written in C# and Razor syntax, organized by functional categories:

**Component Categories:**
- **Data Grids & Tables**: Grid, TreeGrid, PivotView
- **Charts & Data Visualization**: Charts, Accumulation Charts, 3D Charts, Gauges, HeatMap, TreeMap, Sankey
- **Editors**: RichTextEditor, DocumentEditor, Spreadsheet, BlockEditor, MarkdownConverter
- **Inputs & Forms**: DataForm, TextBox, NumericTextBox, ColorPicker, DatePicker, TimePicker, FileUpload
- **Navigations**: Menu, Toolbar, TreeView, Tabs, Breadcrumb, Accordion, Sidebar
- **Layout Components**: Dashboard, Dialog, Splitter, Card, Avatar
- **Calendars & Scheduling**: Calendar, DateRangePicker, Scheduler, Gantt
- **Lists & DropDowns**: ListView, ListBox, DropDown, AutoComplete, ComboBox, MultiColumnComboBox
- **Interactive & AI**: InteractiveChat, SmartComponents (Smart PDF Viewer, Smart RTE)

**Component Structure Pattern:**
```
ComponentName/
├── ComponentName.razor          # Main component markup
├── ComponentName.razor.cs       # Component logic (code-behind)
├── ComponentName.Methods.cs     # Public API methods (partial class)
├── ComponentName.Properties.cs  # Component properties (partial class)
└── Internal/                    # Internal helper classes
```

### 2. JavaScript Interop Layer

The library uses a hybrid architecture combining C# and JavaScript for optimal performance:

**JavaScript Module Organization:**
```
Scripts/
├── syncfusion-blazor.js         # Main bundle
├── syncfusion-blazor-extended.js # Extended functionality
├── sf-utils.js                   # Utility functions
└── modules/                      # Individual component scripts
    ├── sf-grid.js
    ├── sf-chart.js
    ├── sf-calendar.js
    └── [90+ component modules]
```

**Build System:**
- **Webpack**: Bundles JavaScript modules for production
- **Babel**: Transpiles modern JavaScript for browser compatibility
- **Gulp**: Automates build, bundling, and packaging tasks

### 3. CSS/Themes

These UI components use the built‑in themes located in the Syncfusion.Blazor/wwwroot/styles folder. This folder includes the `Fluent 2`, `Material 3`, `Bootstrap 5`, and `Tailwind 3` theme sets, each available in normal, lite, and dark variants.
```
styles/
├── fluent2.css                 # Include comprehensive styling for both normal and bigger size modes, ensuring full UI flexibility but with a larger file size.
├── fluent2-lite.css            # Include styles exclusively for normal size mode and omit bigger size mode styles.
├── fluent2-dark.css            # Include comprehensive dark styling for both normal and bigger size modes, ensuring full UI flexibility but with a larger file size.
└── fluent2-dark-lite.css       # Include dark styles exclusively for normal size mode and omit bigger size mode styles.

```

### 4. Data Layer

**Data Binding Architecture:**
- Supports synchronous and asynchronous data sources
- Integrates with `Syncfusion.Blazor.Data` for remote data operations
- Implements `IDataBoundComponent` interface for consistent data handling
- Supports LINQ operations, filtering, sorting, grouping, and paging

**Data Operations:**
- Client-side data operations (in-memory)
- Server-side data operations (via adapters)
- Custom data adapters for remote services (OData, Web API, GraphQL)

## Design Principles

### 1. Modularity & Separation of Concerns
- Components are organized into logical namespaces (e.g., `Syncfusion.Blazor.Grids`)
- Partial classes separate concerns: methods, properties, and event handlers
- Internal classes hidden in `.Internal` namespaces

### 2. Async-First Design
- All public async methods return `Task` or `Task<T>`
- Method names suffixed with `Async`
- Proper use of `ConfigureAwait(false)` for library code
- Non-blocking UI operations

### 3. Accessibility & Standards Compliance
- WCAG 2.2 Level AA compliance
- Keyboard navigation support
- ARIA attributes for screen readers
- RTL (Right-to-Left) language support

### 4. Performance Optimization
- Virtualization for large datasets (Grid, TreeView, ListView)
- On-demand loading of JavaScript modules
- Lazy loading support for components
- Efficient rendering with minimal DOM manipulation

### 5. Render Mode Agnostic
Components work seamlessly across:
- **Blazor Server**: SignalR-based server rendering
- **Blazor WebAssembly**: Client-side execution in browser
- **Blazor Auto**: Automatic selection of optimal render mode

## Key Architectural Patterns

### 1. Module Pattern
Components use a module-based architecture where features are injected:

```csharp
public class SfGrid<TValue>
{
    public FilterModule FilterModule { get; set; }
    public EditModule EditModule { get; set; }
    
    public async Task ClearFilteringAsync()
    {
        if (FilterModule != null)
        {
            await FilterModule.ClearFiltering();
        }
    }
}
```

### 2. Settings Pattern
Component behavior configured through dedicated settings classes:

```csharp
<SfGrid DataSource="@orders">
    <GridEditSettings AllowAdding="true" AllowEditing="true" Mode="EditMode.Batch"/>
    <GridFilterSettings Type="FilterType.Menu"/>
    <GridSelectionSettings Mode="SelectionMode.Multiple"/>
</SfGrid>
```

### 3. Event System
Comprehensive event system for lifecycle and user interactions:

```csharp
<GridEvents TValue="Order" 
    OnRecordClick="RecordClickHandler"
    DataBound="DataBoundHandler"
    ActionBegin="ActionBeginHandler"/>
```

### 4. Template Support
Flexible templating system using `RenderFragment`:

```razor
<GridColumn Field="@nameof(Order.CustomerID)">
    <Template>
        @{
            var order = context as Order;
            <div>@order.CustomerID</div>
        }
    </Template>
</GridColumn>
```

## Build & Distribution Architecture

### Multi-Target Framework Support
- **Target Frameworks**: net8.0, net9.0, net10.0
- **Language Versions**: C# 12 (net8.0), C# 13 (net9.0), C# 14 (net10.0)
- Conditional compilation for framework-specific features

### Package Generation
```
gulp update-config          # Update version configuration
gulp source-build           # Compile source code
gulp bundling               # Bundle JavaScript files
gulp generate-nuget         # Generate NuGet packages
```

### NuGet Package Structure
- **Main Package**: `Syncfusion.Blazor` (all components)
- **Individual Packages**: Component-specific packages (e.g., `Syncfusion.Blazor.Grid`)
- **Dependencies**: Core, Data, PDF Export, Excel Export libraries

### Custom Resource Generator (CRG)
Allows developers to generate custom script bundles with only required components, reducing bundle size.

## Dependencies

**External Dependencies:**
- `Microsoft.AspNetCore.Components.Web` - Blazor framework
- `Newtonsoft.Json` - JSON serialization
- `System.Text.Json` - Modern JSON APIs
- `Syncfusion.PdfExport.Net.Core` - PDF generation
- `Syncfusion.ExcelExport.Net.Core` - Excel generation

**Internal Dependencies:**
Component dependencies managed through `config.json` for optimal script bundling.

## Testing & Quality Assurance

### Code Coverage (DotCover)
- Automated code coverage analysis
- Target: 80%+ coverage for all components
- Reports generated in `CCReport/` directory

### Code Analysis
- StyleCop for code style enforcement
- Roslyn analyzers for best practices
- Accessibility validation tools

### Security
- XSS prevention through markup sanitization
- Code leaks detection (Gitleaks)
- Signed assemblies with strong-name key (`sf.snk`)

## Development Workflow

1. **Component Development**: Create/modify components in `Syncfusion.Blazor/`
2. **JavaScript Development**: Update scripts in `Scripts/modules/`
3. **Configuration**: Update `config.json` for dependencies
4. **Build**: Run `npm run build` to compile and bundle
5. **Test**: Run automated tests in separate test repository
6. **Package**: Generate NuGet packages with `gulp generate-nuget`
7. **Publish**: Publish to NuGet.org with `gulp publish-nuget`

---

**Version**: 32.1.19  
**Last Updated**: February 2026  
**License**: Commercial (Syncfusion License)
