# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

## [11.0.0] — .NET 10 Extension Members Migration

> **This is a breaking release.** See the [Migration Guide](MIGRATION_GUIDE.md) for step-by-step upgrade instructions.

### Breaking Changes

- **Requires .NET 10** — this version uses C# 14 static extension members.
- **`using static` import replaced by a plain `using`** — replace `using static TddXt.AnyRoot.Root` with `using TddXt.AnyRoot`. All call-site syntax (`Any.Integer()`, `Any.String()`, `Any.Instance<T>()`, etc.) remains unchanged.
- **`Root` class removed** — if you referenced `Root.Any` anywhere, replace it with just `Any`.
- **Homegrown extension methods must be updated** — custom extensions need to be migrated to the new C# 14 static extension syntax. See the [Migration Guide](MIGRATION_GUIDE.md) for details.

### Added

- `CancellationToken` generation support — `Any.Instance<CancellationToken>()` now works out of the box.

[11.0.0]: https://github.com/grzesiek-galezowski/any/compare/4.0.2...v11.0.0
