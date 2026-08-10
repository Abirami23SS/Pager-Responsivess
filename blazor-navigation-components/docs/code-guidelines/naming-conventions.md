# Naming Conventions — Syncfusion Blazor Components

**NOTE** > Standard Skills available in `.codestudio/skills` location.

## C# Conventions

| Symbol | Convention | Example |
|---|---|---|
| Class | PascalCase | `SfGrid<TValue>`, `EditModule` |
| Interface | `I` + PascalCase | `IDataBoundComponent` |
| Enum | PascalCase | `EditMode`, `FilterType` |
| Enum member | PascalCase | `EditMode.Batch`, `FilterType.Menu` |
| Public method | PascalCase | `AddRecordAsync`, `AutoFitColumnsAsync` |
| Public property / parameter | PascalCase | `AllowEditing`, `DataSource` |
| Private field | `_` + camelCase | `_editModule`, `_dotNetRef` |
| Local variable | camelCase | `rowIndex`, `fieldName` |
| Method parameter | camelCase | `data`, `index` |
| Async method | PascalCase + `Async` | `SaveBatchAsync`, `ClearFilteringAsync` |
| Generic type parameter | `T` prefix | `TValue`, `TKey` |
| Constant | PascalCase | `DefaultPageSize` |
| Event | PascalCase, past tense | `OnRecordClick`, `ActionBegin` |
| Event args class | `…Args<TValue>` | `ActionEventArgs<Order>` |

---

## Blazor / Razor Conventions

| Symbol | Convention | Example |
|---|---|---|
| Component file | PascalCase | `SfGrid.razor`, `GridEditSettings.razor` |
| Code-behind | `ComponentName.razor.cs` | `SfGrid.razor.cs` |
| Methods partial | `ComponentName.Methods.cs` | `SfGrid.Methods.cs` |
| Properties partial | `ComponentName.Properties.cs` | `SfGrid.Properties.cs` |
| Child settings component | Parent + feature + `Settings` | `GridEditSettings`, `GridFilterSettings` |
| Events child component | Parent + `Events` | `GridEvents` |

---

## JavaScript Module Conventions

| Symbol | Convention | Example |
|---|---|---|
| Module file | `sf-` + kebab-case | `sf-grid.js`, `sf-datepicker.js` |
| Utility file | descriptive | `sf-utils.js`, `popupsbase.js` |
| JS function / variable | camelCase | `initGrid`, `scrollPosition` |

---

## Namespace Conventions

```
Syncfusion.Blazor                     ← root namespace (Syncfusion.Blazor.csproj)
Syncfusion.Blazor.Grids               ← public API for Grid components
Syncfusion.Blazor.Grids.Internal      ← internal helpers (not part of public API)
Syncfusion.Blazor.Data                ← data binding and adapters
```

---

## Branch Naming

Format: `{azure-task-id}-{component}-{short-description}`

Examples:
- `12345-grid-batch-editing-fix`
- `67890-scheduler-timezone-feature`
- `hotfix/32.1.20-grid-null-ref`

---

## Common Mistakes to Avoid

| ❌ Wrong | ✅ Correct |
|---|---|
| `async void SaveData()` | `async Task SaveDataAsync()` |
| `private int pageSize` | `private int _pageSize` |
| `FilterModule.Clear()` (no null check) | `FilterModule?.Clear()` or `if (FilterModule != null)` |
| `public method()` (no XML doc) | Full `/// <summary>…` documentation |

