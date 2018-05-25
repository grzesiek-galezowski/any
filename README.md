# Any

An anonymous object generator. Successor of Tdd Toolkit's Any class. 

This version is built on .NET Standard 2.0, so it requires at least .NET Core 2.0 or .NET Framework 4.6.2.

# How to use?

Use the package [![NuGet](https://img.shields.io/nuget/v/Any.svg?style=flat-square)](https://www.nuget.org/packages/Any/)

In the code, static use the `TddXt.AnyRoot.Root` property like this:

```csharp
using static TddXt.AnyRoot.Root;
```

This will allow you to use several core methods like `Any.Instance<T>()`:

```csharp
var anInt = Any.Instance<int>();
```

Most of the features of Any are achieved through extension methods on the `Any` object. 
You can add the `using`s for the extension methods yourself or let your IDE do it for you (e.g. Resharper has this ability).

In case you wonder, the complete list of usings to include all extension methods is:

```csharp
using TddXt.AnyRoot.Collections;
using TddXt.AnyRoot.Exploding;
using TddXt.AnyRoot.Invokable;
using TddXt.AnyRoot.Math;
using TddXt.AnyRoot.Network;
using TddXt.AnyRoot.NSubstitute;
using TddXt.AnyRoot.Numbers;
using TddXt.AnyRoot.Reflection;
using TddXt.AnyRoot.Strings;
using TddXt.AnyRoot.Time;
```

This will allow you to use methods such as:

```csharp
var anIntList = Any.List<int>();
var aString = Any.String();
var shortInt = Any.Short();
```

etc.

# Extending the library

As `Any` is in fact an object, you can write your own extension methods. 
Take a look at the source code of the bundled ones to see how this can be achieved.

[![](https://codescene.io/projects/2875/status.svg) Get more details at **codescene.io**.](https://codescene.io/projects/2875/jobs/latest-successful/results)
