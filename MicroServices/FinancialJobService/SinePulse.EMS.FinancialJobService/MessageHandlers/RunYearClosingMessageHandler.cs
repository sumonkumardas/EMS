using System;
using System.Threading.Tasks;
using SinePulse.EMS.FinancialJobService.Services;
using SinePulse.EMS.Messages.FinancialJobMessages;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.FinancialJobService.MessageHandlers
{
  public class RunYearClosingMessageHandler : IMessageHandler<RunYearClosingMessage>
  {
    private readonly IYearCloser _yearCloser;

    public RunYearClosingMessageHandler(IYearCloser yearCloser)
    {
      _yearCloser = yearCloser;
    }

    public async Task Handle(RunYearClosingMessage message)
    {
      await _yearCloser.DoYearClosing(message.SessionId);
    }
  }
}