using System;
using System.Net;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Root;

namespace TddXt.AnyRoot.Network
{
  public static class AnyNetworkExtensions
  {
    public static IPAddress IpAddress(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.IpAddress());
    }

    public static Uri Uri(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Uri());
    }

    public static int Port(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Port());
    }

    public static string IpString(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.IpString());
    }

    public static string UrlString(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.UrlString());
    }

    public static byte Octet(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Byte());
    }
  }
}