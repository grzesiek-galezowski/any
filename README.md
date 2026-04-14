# Any

> **⚠️ v11.0.0 contains breaking API changes.** The import changed from `using static TddXt.AnyRoot.Root` to `using TddXt.AnyRoot`. Call-site syntax is unchanged. See the [Changelog](src/CHANGELOG.md) and the [Migration Guide](src/MIGRATION_GUIDE.md).

An anonymous object generator for .NET tests. Successor of Tdd Toolkit's Any class.

[![NuGet](https://img.shields.io/nuget/v/Any.svg?style=flat-square)](https://www.nuget.org/packages/Any/)

## Requirements

- .NET 10
- C# 14

## How to use?

Install the [Any NuGet package](https://www.nuget.org/packages/Any/), then add a global using to your test project:

```csharp
global using TddXt.AnyRoot;
```

This will allow you to use several core methods like `Any.Instance<T>()`:

```csharp
var anInt = Any.Instance<int>();
```

Most features are provided through extension methods on the `Any` class, which live in separate namespaces.

## Extension method namespaces

Most features are provided through extension methods on the `Any` class. Your IDE can add the imports for you, but the full list is:

```csharp
using TddXt.AnyRoot.Collections;
using TddXt.AnyRoot.Enums;
using TddXt.AnyRoot.Exploding;
using TddXt.AnyRoot.Invokable;
using TddXt.AnyRoot.Math;
using TddXt.AnyRoot.Network;
using TddXt.AnyRoot.Numbers;
using TddXt.AnyRoot.Reflection;
using TddXt.AnyRoot.Strings;
using TddXt.AnyRoot.Time;
```

You can add these as global usings if that is more convenient.

## Extending the library

You can write your own extension methods on the `Any` class to add domain-specific generators:

```csharp
using TddXt.AnyRoot;

namespace MyProject.TestHelpers;

public static class MyDomainExtensions
{
    extension(Any)
    { 1`2


        public static Money Money()
        {
            return new Money(Any.Instance<decimal>(), Any.Instance<Currency>());
        }
    }
}
```

Then use it like any built-in method:

```csharp
var money = Any.Money();
```

See the [Migration Guide](src/MIGRATION_GUIDE.md) if you are upgrading existing custom extensions from a previous version.

---

[![CodeScene general](https://codescene.io/images/analyzed-by-codescene-badge.svg)](https://codescene.io/projects/79076)

[![CodeScene Average Code Health](https://codescene.io/projects/79076/status-badges/average-code-health)](https://codescene.io/projects/79076)

[![CodeScene Hotspot Code Health](https://codescene.io/projects/79076/status-badges/hotspot-code-health)](https://codescene.io/projects/79076)

[![CodeScene Missed Goals](https://codescene.io/projects/79076/status-badges/missed-goals)](https://codescene.io/projects/79076)

[![CodeScene System Mastery](https://codescene.io/projects/79076/status-badges/system-mastery)](https://codescene.io/projects/79076)
