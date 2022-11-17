using System;

namespace SinePulse.EMS.Utility.MessagingQueue.MassTransit
{
  public class UriGenerator : IUriGenerator
  {
    private readonly string _uriPrefix;

    public UriGenerator(string uriPrefix)
    {
      _uriPrefix = uriPrefix;
    }

    public Uri GenerateUri(string address)
    {
      return new Uri($"{_uriPrefix}{address}");
    }
  }
}