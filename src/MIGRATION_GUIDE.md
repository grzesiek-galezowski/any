# Migration Guide: v10.x → v11.0.0

This guide covers migrating from Any **v10.x** to **v11.0.0** (`.NET 10` / C# 14).

## Why This Change Was Made

### The old approach: `using static` trick

Previously, to get the `Any.Integer()` call syntax, you needed a special `using static` import:

```csharp
using static TddXt.AnyRoot.Root;
```

This was a workaround — `Any` was not actually a type, it was a static property on a class called `Root`. The `using static` directive pulled that property into scope so it *looked* like a type. This meant consumers had to know to write `using static` (not a regular `using`), and the `Root` indirection was confusing.

### The new approach: `Any` is a real class

C# 14 (shipping with .NET 10) introduces **static extension members on types** — you can extend a class itself with static methods. With this feature, `Any` is now a proper static class, and all generator methods like `Integer()`, `String()`, etc. are static extensions on that class.

The result is the same call-site syntax — `Any.Integer()` — but with a straightforward import:

```csharp
using TddXt.AnyRoot;
```

No tricks, no indirection. `Any` is a real type and the methods are genuinely its static members.

---

## Migrating Your Tests

### Step 1: Update your target framework

This version requires **.NET 10**. Update your project file:

```xml
<TargetFramework>net10.0</TargetFramework>
```

### Step 2: Replace `using static TddXt.AnyRoot.Root` with `using TddXt.AnyRoot`

```csharp
// Before
global using static TddXt.AnyRoot.Root;

// After
global using TddXt.AnyRoot;
```

The same applies to per-file imports.

### Step 3: Remove any references to `Root`

The `Root` class no longer exists. If you referenced it anywhere (e.g. `Root.Any.Integer()`), replace with just `Any.Integer()`.

### Step 4: Done — call-site syntax is unchanged

All your existing calls continue to work with the same signatures:

```csharp
var id = Any.Integer();
var name = Any.String();
var user = Any.Instance<User>();
var items = Any.List<string>();
var other = Any.OtherThan("skip");
```

---

## Migrating Your Homegrown Extension Methods

If you wrote custom extension methods to add domain-specific generators (e.g. `Any.Money()`, `Any.OrderId()`), you need to update them. There are four mechanical changes:

1. Change `extension(BasicGenerator gen)` → `extension(Any)`
2. Add `static` to each method: `public T Method()` → `public static T Method()`
3. Replace `gen.Instance<T>()` / `gen.InstanceOf(...)` with `Any.Instance<T>()` / `Any.InstanceOf(...)`
4. Replace `using TddXt.AnyExtensibility;` with `using TddXt.AnyRoot;`

### Full before/after example

**Before:**
```csharp
using TddXt.AnyExtensibility;

namespace MyProject.TestHelpers;

public static class MyDomainExtensions
{
    extension(BasicGenerator gen)
    {
        public Money Money()
        {
            return new Money(gen.Instance<decimal>(), gen.Instance<Currency>());
        }

        public OrderId OrderId()
        {
            return new OrderId(gen.Instance<Guid>());
        }
    }
}
```

**After:**
```csharp
using TddXt.AnyRoot;

namespace MyProject.TestHelpers;

public static class MyDomainExtensions
{
    extension(Any)
    {
        public static Money Money()
        {
            return new Money(Any.Instance<decimal>(), Any.Instance<Currency>());
        }

        public static OrderId OrderId()
        {
            return new OrderId(Any.Instance<Guid>());
        }
    }
}
```

The call sites (`Any.Money()`, `Any.OrderId()`) remain unchanged.

> **Note:** If you still use `InlineGenerator<T>` or `GenerationCustomization` directly (e.g. for custom inline generators), you still need `using TddXt.AnyExtensibility` alongside `using TddXt.AnyRoot`.

---

## Quick Checklist

- [ ] Target .NET 10 (`net10.0`)
- [ ] Replace `using static TddXt.AnyRoot.Root` → `using TddXt.AnyRoot`
- [ ] Remove any direct references to the deleted `Root` class
- [ ] Update homegrown extensions: `extension(BasicGenerator gen)` → `extension(Any)`, add `static`, replace `gen.` with `Any.`
- [ ] Build and run tests — call-site syntax (`Any.Xyz()`) is unchanged
