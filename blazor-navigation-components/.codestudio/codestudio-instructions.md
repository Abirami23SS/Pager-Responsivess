# Syncfusion Blazor Component Library - Coding Standards

## Project Documentation

* [Product Vision and Goals](./../docs/overview/product.md): Understand the high-level vision and objectives of the product to ensure alignment with business goals.
* [System Architecture and Design Principles](./../docs/architecture/architecture.md): Overall system architecture, design patterns, and design principles that guide the development process.
* [Contributing Guidelines](./contributing.md): Overview of the project's contributing guidelines and collaboration practices.
* [Code Review Guidelines/Checklist](./agents/code-review.agent.md): Mandatory checklist for reviewing the Source/PRs for improve the code quality.

**Note**: Suggest to update these documents if you find any incomplete or conflicting information during your work.

## Project Overview
This is the Syncfusion.Blazor component library source code targeting .NET/Blazor applications. The library provides comprehensive UI components for Server, WASM, and Auto render modes.

## Coding Standards & Conventions

### C# Code Style

1. **Naming Conventions**
   - Use PascalCase for public methods, properties, and classes
   - Use camelCase for parameters and local variables
   - Prefix private fields with underscore (e.g., `_fieldName`)
   - Use descriptive, self-documenting names

2. **Async/Await Patterns**
   - All public async methods MUST return `Task` or `Task<T>`
   - Suffix async method names with `Async` (e.g., `AddRecordAsync`)
   - Use `ConfigureAwait(false)` for library code when awaiting tasks
   - Always use `async`/`await` keywords, avoid `.Result` or `.Wait()`

3. **Nullable Reference Types**
   - Use nullable types appropriately (`Nullable<int>`, `string?`)
   - Validate null parameters and provide meaningful error messages
   - Use null-conditional operators (`?.`) and null-coalescing operators (`??`) where appropriate

### XML Documentation Standards

**ALL public APIs MUST have comprehensive XML documentation** including:

1. **Summary Section**
   - Clear, concise description of what the method/property does
   - Start with a verb (e.g., "Adds a new record", "Gets the selected items")

2. **Parameters (`<param>`)**
   - Document each parameter with its purpose
   - Include the type context where helpful
   - Reference related properties using `<see cref="PropertyName"/>`

3. **Returns Section (`<returns>`)**
   - Always include for methods that return values
   - Document what the returned value represents

4. **Remarks Section (`<remarks>`)**
   - Include important usage notes, preconditions, and side effects
   - Document related property dependencies (e.g., "AllowAdding must be true")
   - Mention special behaviors or edge cases
   - Use `<c>` tags for inline code references

5. **Example Section (`<example>`)**
   - Provide complete, working code examples
   - Wrap examples in `<code><![CDATA[ ... ]]></code>` blocks
   - Include button triggers and component setup
   - Show realistic usage scenarios
   - Include `@code` blocks with complete method implementations

### Component Architecture

1. **Public Methods**
   - Keep method signatures simple and intuitive
   - Provide overloads for common scenarios
   - Use optional parameters with sensible defaults
   - Always validate EditSettings, SelectionSettings, etc. before operations

2. **Property References**
   - When referencing other properties in docs, use `<see cref=""/>`
   - Reference both class name and property (e.g., `GridEditSettings.AllowAdding`)
   - Link to related enums and types

3. **Error Handling**
   - Check permissions/settings before operations (AllowAdding, AllowEditing, etc.)
   - Provide clear error messages or alerts to users
   - Return early for invalid states

4. **Module Pattern**
   - Check if module is null before calling (e.g., `if (FilterModule != null)`)
   - Use null-conditional operators for module method calls

### JavaScript Interop

1. **Script Organization**
   - Keep JavaScript modules in `Scripts/modules/` directory
   - Use consistent naming: `sf-componentname.js`
   - Maintain separate resource files in `Scripts/resources/`

2. **Build Configuration**
   - Update webpack configurations when adding new components
   - Follow existing patterns in `webpack.config.js`
   - Update `gulpfile.js` for new build tasks

### Code Examples in Documentation

Every public method should include a complete example showing:
```xml
<example>
<code><![CDATA[
<button id="MethodName" @onclick="MethodHandler">Button Text</button>
<SfGrid @ref="grid" DataSource="@Orders">
 <!-- Relevant settings -->
</SfGrid>
@code{
   SfGrid<Order> grid;
   private async Task MethodHandler()
   {
       await grid.MethodAsync(/* parameters */);
   }
}
]]>
</code>
</example>
```

### Testing & Validation

1. **Before Committing**
   - Ensure all public APIs have complete XML documentation
   - Verify examples compile and run
   - Check for proper async/await usage
   - Validate null reference handling

2. **Component Testing**
   - Test across render modes (Server, WASM, Auto)
   - Verify accessibility features
   - Test keyboard navigation

### File Organization

1. **Component Structure**
   - Main component file: `ComponentName.razor`
   - Methods partial: `ComponentName.Methods.cs`
   - Properties partial: `ComponentName.Properties.cs`
   - Keep files focused and manageable

2. **Namespace Organization**
   - Follow existing namespace patterns
   - Use `Syncfusion.Blazor.ComponentArea` structure
   - Keep internal classes in `.Internal` namespace

## AI Assistant Guidelines

When modifying or creating code in this project:

1. **Always add complete XML documentation** for any new public members
2. **Follow existing patterns** seen in similar components
3. **Include working code examples** in documentation
4. **Use async/await properly** - never block synchronously
5. **Validate settings** before performing operations
6. **Reference related properties** using `<see cref=""/>`
7. **Provide helpful remarks** about usage conditions and dependencies
8. **Check module availability** before calling module methods
9. **Use consistent naming** with existing codebase
10. **Test considerations** - think about Server/WASM compatibility

## Common Patterns

### Adding Records Pattern
```csharp
public async Task AddRecordAsync(TValue data, Nullable<int> index = null)
{
    if (!EditSettings.AllowAdding)
    {
        // Return or show error
    }
    // Perform operation
    await EditModule?.AddRecord(data, index);
}
```

### Module Access Pattern
```csharp
public async Task ClearFilteringAsync()
{
    if (FilterModule != null)
    {
        await FilterModule.ClearFiltering();
    }
}
```

### Optional Parameter Overloads
```csharp
// Specific overload
public async Task AutoFitColumnAsync(string fieldName)
    => await AutoFitColumnsAsync(new string[] { fieldName });

// General method
public async Task AutoFitColumnsAsync(string[] fieldNames)
{
    // Implementation
}
```

---

**Remember**: This is a library consumed by thousands of developers. Quality, clarity, and consistency are paramount.
