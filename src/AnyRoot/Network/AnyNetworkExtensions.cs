using System;
using System.Net;
using TddXt.AnyExtensibility;

namespace TddXt.AnyRoot.Network;

public static class AnyNetworkExtensions
{
  extension(BasicGenerator gen)
  {
    public IPAddress IpAddress()
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.IpAddress());
    }

    public Uri Uri()
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.Uri());
    }

    public int Port()
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.Port());
    }

    public string IpString()
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.IpString());
    }

    public string UrlString()
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.UrlString());
    }

    public byte Octet()
    {
      return gen.Instance<byte>();
    }
  }
}
