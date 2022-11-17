using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SinePulse.EMS.Utility.SmsSending.Tests
{
  public class RobiSmsSenderTest
  {
    private RobiSmsSender _smsSender;

    public RobiSmsSenderTest()
    {
      _smsSender = new RobiSmsSender();
    }

    [Fact]
    public async Task AbleToSendSms()
    {
      var result = await _smsSender.SendSms(
        new Sms
        {
          PhoneNumbers = new List<string> {"01713032885", "01919838788", "01534643055"},
          Text = "This is a test message. Please inform me if you got this message. -- Nafeez"
        });
      Assert.True(result);
    }
  }
}