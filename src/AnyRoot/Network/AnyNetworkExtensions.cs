using System;
using System.Net;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Root;

namespace TddXt.AnyRoot.Network;

public static class AnyNetworkExtensions
{
  extension(BasicGenerator gen)
  {
    public IPAddress IpAddress()
    {
      return gen.InstanceOf(InlineGenerators.IpAddress());
    }

    public Uri Uri()
    {
      return gen.InstanceOf(InlineGenerators.Uri());
    }

    public int Port()
    {
      return gen.InstanceOf(InlineGenerators.Port());
    }

    public string IpString()
    {
      return gen.InstanceOf(InlineGenerators.IpString());
    }

    public string UrlString()
    {
      return gen.InstanceOf(InlineGenerators.UrlString());
    }

    public byte Octet()
    {
      return gen.Instance<byte>();
    }
  }
}
