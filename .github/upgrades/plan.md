# .NET 10 Upgrade Plan for SayedHa.Commands

## Table of Contents
1. Executive Summary
2. Migration Strategy
3. Detailed Dependency Analysis
4. Project-by-Project Plans
5. Package Update Reference
6. Breaking Changes Catalog
7. Testing & Validation Strategy
8. Risk Management
9. Complexity & Effort Assessment
10. Source Control Strategy
11. Success Criteria

---

## 1. Executive Summary

### Selected Strategy
**All-At-Once Strategy** - All projects upgraded simultaneously in single operation.

**Rationale**:
- 3 projects (small solution)
- All currently on .NET 8.0
- Clear dependency structure (1 shared library consumed by app and tests)
- Package updates identified and compatible versions available
- You requested inclusion of security fixes and upgrading all NuGet packages

### Scope
- Solution: `C:\data\mycode\sayedha.commands\SayedHa.Commands.sln`
- Projects:
  - `SayedHa.Commands.Shared` (ClassLibrary)
  - `SayedHa.Commands` (Console/Tool app)
  - `SayedHa.Commands.Test` (Test project)

### Target State
- Target frameworks: all projects to `net10.0`
- Packages: apply suggested upgrades from assessment; replace deprecated `LibGit2Sharp` or remove if unused
- Resolve source-incompatible APIs (23) and 1 behavioral change

### Key Risks / Special Notes
- Deprecated package: `LibGit2Sharp 0.26.0` in `SayedHa.Commands.Shared`
- DataProtection APIs source incompatibilities (ProtectedData, DataProtectionScope)
- `WebClient` usage flagged; consider `HttpClient`

---

## 2. Migration Strategy

### Approach
- All-At-Once Strategy with atomic update of target frameworks and package references across all projects.
- No intermediate states; single coordinated commit where feasible.

### Phases
- Phase 0: Preparation
  - Validate .NET 10 SDK installation and any `global.json` compatibility
  - Confirm working branch `upgrade-to-NET10`
- Phase 1: Atomic Upgrade
  - Update `TargetFramework` to `net10.0` in all projects
  - Update packages per §Package Update Reference
  - Address compilation errors and breaking changes per §Breaking Changes Catalog
  - Build solution and verify 0 errors
- Phase 2: Test Validation
  - Run all test projects, fix failures

---

## 3. Detailed Dependency Analysis

### Dependency Graph Summary
- `SayedHa.Commands.Shared` has no project dependencies; dependants: `SayedHa.Commands`, `SayedHa.Commands.Test`.
- `SayedHa.Commands` depends on `SayedHa.Commands.Shared`; dependant: `SayedHa.Commands.Test`.
- `SayedHa.Commands.Test` depends on both `SayedHa.Commands` and `SayedHa.Commands.Shared`.

### Migration Grouping (All-At-Once)
- All projects are updated simultaneously, but breaking change fixes may reference dependency order:
  - Library first for API fixes (`Shared`), then app (`Commands`), then tests (`Test`) for code changes if needed.

### Critical Path
- Any API changes in `Shared` ripple to `Commands` and `Test`.

---

## 4. Project-by-Project Plans

### Project: SayedHa.Commands.Shared
**Current State**: `net8.0`; packages: `LibGit2Sharp 0.26.0` (deprecated), `Microsoft.Extensions.DependencyInjection 8.0.0`
**Target State**: `net10.0`; `Microsoft.Extensions.DependencyInjection 10.0.1`; `LibGit2Sharp` replaced/removed
**Migration Steps**:
1. Update `TargetFramework` to `net10.0`.
2. Update packages:
   - `Microsoft.Extensions.DependencyInjection` 8.0.0 → 10.0.1
   - Replace/remove `LibGit2Sharp` 0.26.0 (deprecated)
3. Expected breaking changes:
   - Serialization constructor usage (`Exception(SerializationInfo, StreamingContext)` occurrences)
   - Data protection API changes (see §Breaking Changes Catalog)
4. Code modifications:
   - Replace `WebClient` with `HttpClient` if present
   - Adjust `ProtectedData`/`DataProtectionScope` usage for .NET 10 compatibility
5. Testing:
   - Build with no warnings/errors
   - Execute dependent tests via `SayedHa.Commands.Test`
6. Validation:
   - [ ] Builds without errors
   - [ ] No deprecated package usage remains

### Project: SayedHa.Commands
**Current State**: `net8.0`; packages: `Microsoft.Extensions.DependencyInjection 8.0.0`, `System.Security.Cryptography.ProtectedData 8.0.0`, `Spectre.Console 0.43.0`, `TextCopy 1.5.2`, `McMaster.Extensions.CommandLineUtils 2.3.4`
**Target State**: `net10.0`; `Microsoft.Extensions.DependencyInjection 10.0.1`; `System.Security.Cryptography.ProtectedData 10.0.1`; others remain compatible
**Migration Steps**:
1. Update `TargetFramework` to `net10.0`.
2. Update packages:
   - `Microsoft.Extensions.DependencyInjection` 8.0.0 → 10.0.1
   - `System.Security.Cryptography.ProtectedData` 8.0.0 → 10.0.1
3. Expected breaking changes:
   - Data protection API source incompatibilities; adjust scope usage
   - Behavioral change flagged in `HttpContent` (see §Breaking Changes Catalog)
4. Code modifications:
   - Ensure DI registrations align with `Microsoft.Extensions.*` v10 patterns
   - Replace `WebClient` with `HttpClient` if used
   - Review `HttpContent` behaviors
5. Testing:
   - Build & run unit/integration tests `SayedHa.Commands.Test`
6. Validation:
   - [ ] Builds without errors
   - [ ] Tests pass

### Project: SayedHa.Commands.Test
**Current State**: `net8.0`; packages: `xunit 2.4.0`, `xunit.runner.visualstudio 2.4.0`, `Microsoft.NET.Test.Sdk 16.0.1`, `Moq 4.13.0`
**Target State**: `net10.0` (packages currently marked compatible)
**Migration Steps**:
1. Update `TargetFramework` to `net10.0`.
2. Package updates: keep current versions unless failures suggest bump (assessment shows compatible).
3. Validation:
   - [ ] Builds without errors
   - [ ] All tests pass

---

## 5. Package Update Reference

### Common Package Updates (affecting multiple projects)
- `Microsoft.Extensions.DependencyInjection`: 8.0.0 → 10.0.1 (2 projects: `Commands`, `Shared`) Reason: framework alignment and security fixes.

### Security & Critical Updates
- `System.Security.Cryptography.ProtectedData`: 8.0.0 → 10.0.1 (project: `Commands`) Reason: recommended upgrade, security alignment.
- `LibGit2Sharp`: 0.26.0 → [Replace/Remove] (project: `Shared`) Reason: deprecated.

### Compatible Packages (no change unless needed)
- `McMaster.Extensions.CommandLineUtils 2.3.4`
- `Spectre.Console 0.43.0`
- `TextCopy 1.5.2`
- `xunit 2.4.0`, `xunit.runner.visualstudio 2.4.0`, `Microsoft.NET.Test.Sdk 16.0.1`
- `Moq 4.13.0`

---

## 6. Breaking Changes Catalog

### Framework/API Categories
- Serialization constructors: `System.Exception(SerializationInfo, StreamingContext)` usages may be source-incompatible; refactor custom exceptions to avoid obsolete serialization patterns.
- Cryptography & Data Protection:
  - `T:System.Security.Cryptography.DataProtectionScope`
  - `F:DataProtectionScope.CurrentUser`
  - `T:System.Security.Cryptography.ProtectedData`
  - `M:ProtectedData.Protect(byte[], byte[], DataProtectionScope)`
  Guidance: Prefer `Microsoft.AspNetCore.DataProtection` where appropriate or validate `ProtectedData` availability on platforms; ensure scope values are valid in .NET 10.
- Networking:
  - `System.Net.WebClient` constructors flagged as source-incompatible; replace with `HttpClient`.
- HTTP Content:
  - `System.Net.Http.HttpContent` behavioral change; verify content disposal/encoding assumptions.

---

## 7. Testing & Validation Strategy

### Phase 1: Atomic Upgrade Validation
- Restore and build entire solution; expected 0 errors.
- Fix compilation errors due to framework/package updates in one bounded pass.

### Phase 2: Test Execution
- Run `SayedHa.Commands.Test`; fix failures related to API changes (serialization, data protection, HTTP).

### Validation Checklist (per project)
- [ ] Builds without errors
- [ ] Builds without warnings
- [ ] Unit tests pass
- [ ] Integration tests pass (if applicable)
- [ ] No security vulnerabilities remain

---

## 8. Risk Management

### High-Risk Items
- Deprecated `LibGit2Sharp` in `Shared`: Mitigation—remove usage or migrate to maintained alternative; add adapter layer if needed.
- Data Protection changes: Mitigation—centralize data protection usage; add tests around protect/unprotect; validate scope.

### Contingency Plans
- If `LibGit2Sharp` is required, evaluate alternatives (e.g., managed git libraries) or pin to safe version temporarily with clear TODO.
- If DI upgrade causes runtime issues, audit service lifetimes and configuration.

---

## 9. Complexity & Effort Assessment

- Solution classification: Simple
- Relative complexity:
  - `Shared`: Medium (API changes, deprecated package)
  - `Commands`: Medium (crypto/HTTP changes)
  - `Test`: Low

---

## 10. Source Control Strategy

- Branching: Perform all changes on `upgrade-to-NET10`.
- Commit Strategy: Prefer single commit for atomic framework and package upgrade across all projects.
- PR & Review: Single PR referencing plan sections; validate build and test passes.

---

## 11. Success Criteria

- All projects target `net10.0`.
- All package updates applied as specified; deprecated packages resolved.
- Solution builds with 0 errors.
- All tests pass.
- No security vulnerabilities remain.
