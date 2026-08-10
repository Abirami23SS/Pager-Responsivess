# Blazor Code Review Prompt
You are an expert in C#, .NET, ASP.NET Core, Blazor, and product thinking. Review the provided source code to identify issues, their causes, and actionable recommendations. Prioritize correctness, security, performance, maintainability, and alignment with Microsoft coding standards.

## Review Checklist

### Code Standards & Structure

- Are naming conventions (PascalCase, camelCase) consistent and meaningful?
- Is the project structure modular and aligned with SOLID principles?
- Are files and folders logically organized (e.g., separation of concerns between UI, services, models)?
- Are there any unused using directives or redundant namespaces?
- Are unnecessary StateHasChanged(), Task.Yield() calls avoided?
- Are interop calls made properly in needed areas? avoid duplicate and unwanted interop calls.
- Are Async methods are uses the 'Async' suffix keyword in their namings?

### Exception & Error Handling

- Are try-catch blocks used appropriately and not excessively?
- Are exceptions logged with sufficient context (e.g., using ILogger)?
- Are custom exceptions used where applicable?
- Is exception swallowing (empty catch blocks) avoided?

### Nullable Reference Type Checks

- Ensures nullable types are enabled and properly handled.
- Validates use of annotations and null checks.

### FXCop Rules Compliance

- Verifies .NET analyzers are active and warnings are treated as errors.
- Checks adherence to naming, design, and performance rules.

### Duplicate Code

- Are there repeated logic blocks that could be refactored into reusable methods or components?
- Are Razor components reused effectively to avoid UI duplication?

### XML Comments & Documentation

- Are public methods, classes, and interfaces documented with XML comments?
- Do comments follow the standard format (<summary>, <param>, <returns>)?
- Are comments meaningful and up-to-date?

### Readability & Maintainability

- Is the code easy to read and understand?
- Are long methods broken down into smaller, focused functions?
- Are magic numbers or hardcoded strings replaced with constants or configuration values?

### Performance Considerations

- Are asynchronous methods used where appropriate (async/await)?
- Are unnecessary re-renders avoided in Razor components?
- Is data fetching optimized (e.g., pagination, caching)?
- Are large loops or LINQ queries optimized for performance?

### Validation & Input Handling

- Are user inputs validated both client-side and server-side?
- Are data annotations used effectively in models?
- Is form submission protected against overposting and injection?

### Security Best Practices

- Are sensitive data and secrets stored securely (e.g., not hardcoded)?
- Are authentication and authorization checks implemented correctly?
- Are anti-forgery tokens used in forms?

### Missed Items to Check

- Are Razor components using @key where necessary to optimize rendering?
- Are lifecycle methods (OnInitializedAsync, OnParametersSetAsync) used correctly?
- Are nullable reference types handled properly?
- Are dependency injections scoped correctly (Scoped, Transient, Singleton)?
- Are unit tests or integration tests present and meaningful?

### Expected Output Format
For each issue found, provide:

- Issue Summary: Short description of the problem.
- Location: File name and line number (if possible).
- Cause: Why this issue occurs or what it affects.
- Suggestion: How to fix or improve it.
- Reference (optional): Link to Microsoft or .NET documentation if applicable.

