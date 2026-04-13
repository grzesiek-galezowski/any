using System;
using System.Net;

namespace TddXt.AnyRoot.Network;

public static class AnyNetworkExtensions
{
  extension(Any)
  {
    public static IPAddress IpAddress()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.IpAddress());
    }

    public static Uri Uri()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Uri());
    }

    public static int Port()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Port());
    }

    public static string IpString()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.IpString());
    }

    public static string UrlString()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.UrlString());
    }

    public static byte Octet()
    {
      return Any.Instance<byte>();
    }
  }
}
