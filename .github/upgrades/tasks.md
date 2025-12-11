# SayedHa.Commands .NET 10.0 Upgrade Tasks

## Overview

This document tracks the execution of the SayedHa.Commands solution upgrade from .NET 8.0 to .NET 10.0. All three projects will be upgraded simultaneously in a single atomic operation, followed by testing and validation.

**Progress**: 0/3 tasks complete (0%) ![0%](https://progress-bar.xyz/0)

---

## Tasks

### [▶] TASK-001: Verify prerequisites
**References**: Plan §2 Phase 0

- [✓] (1) Verify .NET 10 SDK installed and available
- [▶] (2) .NET 10 SDK version meets minimum requirements (**Verify**)
- [ ] (3) Check global.json compatibility (if present in repository)
- [ ] (4) global.json compatible with .NET 10 SDK or updated (**Verify**)

---

### [ ] TASK-002: Atomic framework and dependency upgrade with compilation fixes
**References**: Plan §4 Project-by-Project Plans, Plan §5 Package Update Reference, Plan §6 Breaking Changes Catalog

- [ ] (1) Update TargetFramework to `net10.0` in all 3 projects per Plan §4 (SayedHa.Commands.Shared, SayedHa.Commands, SayedHa.Commands.Test)
- [ ] (2) All project files updated to `net10.0` (**Verify**)
- [ ] (3) Update package references per Plan §5: Microsoft.Extensions.DependencyInjection 8.0.0 → 10.0.1 (SayedHa.Commands.Shared, SayedHa.Commands); System.Security.Cryptography.ProtectedData 8.0.0 → 10.0.1 (SayedHa.Commands); replace or remove deprecated LibGit2Sharp 0.26.0 (SayedHa.Commands.Shared)
- [ ] (4) All package references updated per specification (**Verify**)
- [ ] (5) Restore all dependencies across solution
- [ ] (6) All dependencies restored successfully (**Verify**)
- [ ] (7) Build solution and fix all compilation errors per Plan §6 Breaking Changes Catalog (serialization constructors, DataProtectionScope/ProtectedData API changes, WebClient replacement with HttpClient, HttpContent behavioral changes)
- [ ] (8) Solution builds with 0 errors (**Verify**)
- [ ] (9) Commit changes with message: "TASK-002: Upgrade to .NET 10.0 - atomic framework and dependency update"

---

### [ ] TASK-003: Run test suite and validate upgrade
**References**: Plan §4 SayedHa.Commands.Test, Plan §7 Testing & Validation Strategy

- [ ] (1) Run all tests in SayedHa.Commands.Test project
- [ ] (2) Fix any test failures related to framework upgrade (reference Plan §6 for breaking changes: serialization, data protection, HTTP changes)
- [ ] (3) Re-run tests after fixes
- [ ] (4) All tests pass with 0 failures (**Verify**)
- [ ] (5) Commit test fixes with message: "TASK-003: Complete .NET 10.0 upgrade testing and validation"

---

