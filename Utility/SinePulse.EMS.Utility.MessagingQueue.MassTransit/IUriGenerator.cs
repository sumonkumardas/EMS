using System;

namespace SinePulse.EMS.Utility.MessagingQueue.MassTransit
{
  public interface IUriGenerator
  {
    Uri GenerateUri(string address);
  }
}