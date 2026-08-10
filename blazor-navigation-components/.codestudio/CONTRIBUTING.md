# Contributing to Syncfusion Blazor Components

Thank you for your interest in contributing to Syncfusion.Blazor! This document provides guidelines and best practices for developers working on this project.

## Getting Started

### Prerequisites
- Visual Studio 2022 or later or Visual Studio Code
- .NET 8, 9, or 10 SDK
- Node.js (v14+) and npm
- Git

### Repository Setup
```bash
# Clone the repository
git clone https://gitea.syncfusion.com/essential-studio/ej2-blazor-source.git
cd ej2-blazor-source

# run bat file. All are pre-configured. 

compile.bat

```

## Development Workflow

### 1. Branch Strategy
- **Development Branch**: Work on feature branches from `development`
- **Branch Naming**: `azure-task-id-component-name-description` or `azure-task-id-issue-description`. Ex: `12345-grid-template-issue`
- **Pull Requests**: Target `development`, `hotfix/{{version}}` branches based on commitments.

### 2. Making Changes

**For New Components:**
1. Create component folder under appropriate category (e.g., `Syncfusion.Blazor/Grids/`)
2. Follow the standard component structure:
   - `ComponentName.razor` - Main component
   - `ComponentName.razor.cs` - Code-behind
   - `ComponentName.Methods.cs` - Public API methods
   - `ComponentName.Properties.cs` - Component properties
3. Add JavaScript module in `Scripts/modules/sf-componentname.js`
4. Update `config.json` with component dependencies
5. Add component to webpack configuration

**For Bug Fixes:**
1. Identify the affected component and files
2. Write a test case to reproduce the issue (in test repository)
3. Implement the fix
4. Verify the fix resolves the issue without side effects
5. Update XML documentation if API changes

### 3. Code Quality Standards

**XML Documentation (REQUIRED)**
- Every public method, property, and class MUST have comprehensive XML documentation
- Include `<summary>`, `<param>`, `<returns>`, `<remarks>`, and `<example>` sections
- Provide complete, working code examples in `<example>` section
- Use `<see cref=""/>` for cross-references

**Coding Conventions**
- Follow existing patterns in the codebase
- Use PascalCase for public members, camelCase for parameters/locals
- Prefix private fields with underscore: `_fieldName`
- All async methods must:
  - Return `Task` or `Task<T>`
  - Have `Async` suffix in name
  - Use `ConfigureAwait(false)` when awaiting
- Validate settings/permissions before operations
- Check module availability: `if (Module != null)`

**Best Practices**
- Keep methods focused and single-purpose
- Provide method overloads for common scenarios
- Use optional parameters with sensible defaults
- Handle null references gracefully
- Return early for invalid states

### 4. Testing

**Unit Testing**
- Write tests in the [blazor-tests-automation](https://gitea.syncfusion.com/essential-studio/blazor-tests-automation) repository
- Maintain consistent folder structure between source and test repos
- Aim for 80%+ code coverage minimum

**Code Coverage**
- [Follow this guidelines](https://gitea.syncfusion.com/essential-studio/blazor-tests-automation/wiki/Guidelines-for-getting-code-coverage-report-using-Dot-Cover-using-individual-project)

**Manual Testing**
- Test across render modes: Server, WebAssembly, Auto
- Test keyboard navigation and accessibility
- Test with different data sizes (small, large datasets)
- Verify RTL support if applicable

### 5. Accessibility Requirements

All components must:
- Meet WCAG 2.2 Level AA standards
- Support keyboard navigation
- Include proper ARIA attributes
- Work with screen readers
- Support high contrast mode

### 6. Building & Bundling

```bash
# Full build
npm run build

# Bundle JavaScript only
npm run bundle

# Generate specific component package
gulp generate-nuget --option Release --project Syncfusion.Blazor.Buttons
```

## Code Review Process

### Before Submitting PR
- [ ] All public APIs have complete XML documentation
- [ ] Code examples in documentation compile and run
- [ ] No StyleCop warnings
- [ ] Code builds successfully for all target frameworks
- [ ] Unit tests pass with 80%+ coverage
- [ ] Accessibility requirements met
- [ ] No security vulnerabilities (run `npm run gitleaks-test`)

### PR Template for Bugs
```markdown
### Bug description
Clearly and concisely describe the problem (this cannot be empty).

### Root cause
Briefly describe the root cause and analysis of the problem.
If there is an internal discussion on the forum, provide the link.

### Solution description
Describe the changes made in the code in detail for the reviewers.

### Code Studio usage(Mandatory)
* Code Studio used in this PR/MR?
    - [ ] Yes
    - [ ] No
* If `Yes`: Primary use (choose one)
    - [ ] Generate new code
    - [ ] Refactor/improve existing code
    - [ ] Tests
    - [ ] Bug fix / debugging help
    - [ ] Docs / comments
    - [ ] Review assistance (explanations/summaries)
    - [ ] Other: 

* Outcome
    - [ ] Saved time
    - [ ] Neutral
    - [ ] Cost time
* If “Cost time” explain in short (1 or 2 lines):

### Impact assessment
* [ ] Low - Affects a single feature with minimal user impact
* [ ] Medium - Affects multiple features or has moderate user impact
* [ ] High - Critical functionality or significant user impact

### Reason for not identifying earlier
Provide the reason for not identifying the bug earlier.
     
### Areas tested against this fix
* [ ] Tested using standard test cases
* [ ] Tested against feature matrix. [Centralized location](https://syncfusion.sharepoint.com/:f:/r/sites/Blazor/Shared%20Documents/Feature%20Matrix%20-%20Documents?csf=1&web=1&e=UToeuF)
* [ ] NA

### Breaking changes
* [ ] Yes (Tag `breaking-issue`)
* [ ] No
 
If yes, provide breaking commit details link and migration guidance.
 
### Regression testing
* [ ] Verified fix doesn't reintroduce previous bugs
* [ ] Checked edge cases and error scenarios

### Action taken to prevent recurrence
* [ ] Added/updated unit tests
* [ ] Other (specify): _________________
* [ ] NA

### Automation status
* [ ] BUnit (provide PR link: _________________)
* [ ] Playwight (provide PR link: _________________)
* [ ] NA

### Cross-platform verification
* [ ] Blazor Server
* [ ] Blazor WASM
* [ ] NA
 
### Related issues
Is this issue present in EJ2 or other components?
* [ ] Resolved in EJ2 (PR link: _________________)
* [ ] Created task for EJ2 (Task link: _________________)
* [ ] Needs attention in other components (tag `needs-attention-coreteam`)
* [ ] NA

### Output screenshots
Post the output screenshots if a UI is affected or added due to this bug.

### API changes
* [ ] New API added (API Review task link: _________________)
* [ ] Existing API renamed/modified (API Review task link: _________________)
* [ ] No API changes

### Performance verification
* [ ] Verified no memory leaks introduced
* [ ] Verified no performance degradation
* [ ] Not applicable

### Reviewer Checklist
* [ ] Reviewed the provided Code Studio usages related information.
* [ ] Code changes follow component guidelines
* [ ] All provided information reviewed and verified
* [ ] Solution addresses the root cause effectively
```
-------------------------------------

### PR Template for Feature
```markdown
### Feature description
Clearly and concisely describe the feature.

### Requirement and specification document
* [ ] API Review (Task link: _________________)
* [ ] Azure Task with detailed feature information (Task link: _________________)
* [ ] Not applicable

### Feature scope and impact
* [ ] Core functionality (high impact)
* [ ] Enhancement to existing feature
* [ ] New standalone feature
* [ ] Experimental feature

### Code Studio usage(Mandatory)
* Code Studio used in this PR/MR?
    - [ ] Yes
    - [ ] No
* If `Yes`: Primary use (choose one)
    - [ ] Generate new code
    - [ ] Refactor/improve existing code
    - [ ] Tests
    - [ ] Bug fix / debugging help
    - [ ] Docs / comments
    - [ ] Review assistance (explanations/summaries)
    - [ ] Other: 

* Outcome
    - [ ] Saved time
    - [ ] Neutral
    - [ ] Cost time
* If “Cost time” explain in short (1 or 2 lines):

### Output screenshots/demos
Post the output screenshots or demo links if UI is affected or added.

### Feature matrix documentation
* [ ] Feature matrix document updated [Centralized location](https://syncfusion.sharepoint.com/:f:/r/sites/Blazor/Shared%20Documents/Feature%20Matrix%20-%20Documents?csf=1&web=1&e=OGX1td)
* [ ] Not applicable

### Testing coverage
* [ ] Tested against feature matrix
* [ ] Edge cases and error scenarios verified
* [ ] Cross-browser testing completed

### Test cases and automation
* [ ] Test cases documented and attached to this PR
* [ ] BUnit tests added (PR link: _________________)
* [ ] Playwright tests added (PR link: _________________)
* [ ] Manual testing only (justification: _________________)
* [ ] Not applicable

### Test bed sample location
Provide the test bed sample location for code reviewers to verify the feature's behavior.
 
### Platform verification
* [ ] Verified in Blazor Server
* [ ] Verified in Blazor WASM
* [ ] Not applicable

### Related components
* [ ] Feature applicable to other components (Tasks created: _________________) (tagged "needs-attention-coreteam")
* [ ] Not applicable to other components

### Breaking changes
* [ ] Contains breaking changes - provide migration guidance (added "breaking-change" label)
* [ ] No breaking changes

### Performance verification
* [ ] Memory usage verified (no leaks)
* [ ] Rendering performance verified
* [ ] Not applicable

### Reviewer Checklist
* [ ] Reviewed the provided Code Studio usages related information.
* [ ] Feature matrix documentation verified
* [ ] Test coverage verified
* [ ] Sample implementation reviewed
* [ ] Code meets standards and best practices
* [ ] C# implementation used appropriately (minimal JavaScript)
```

## Common Patterns

### Adding Records to Grid
```csharp
public async Task AddRecordAsync(TValue data, Nullable<int> index = null)
{
    if (!EditSettings.AllowAdding) return;
    await EditModule?.AddRecord(data, index);
}
```

### Module Access
```csharp
public async Task OperationAsync()
{
    if (ModuleName != null)
    {
        await ModuleName.Operation();
    }
}
```

### Method Overloads
```csharp
public async Task AutoFitColumnAsync(string fieldName)
    => await AutoFitColumnsAsync(new string[] { fieldName });
```

## Configuration Files

**config.json**: Component dependencies and script configuration
**package.json**: npm scripts and build tools
**webpack.config.js**: JavaScript bundling configuration
**gulpfile.js**: Build automation tasks

## Questions or Issues?

- Review existing documentation in [ARCHITECTURE.md](./ARCHITECTURE.md),[CODE-REVIEW](./CODE-REVIEW.md)  and [PRODUCT.md](./PRODUCT.md)
- Check the [README.md](./README.md) for configuration details
- Contact the Blazor team for guidance

## License

By contributing, you agree that your contributions will be licensed under the same license as the project.

---

**Remember**: Quality over speed. Our components are used by thousands of developers worldwide. Take the time to write clean, well-documented, accessible code.
