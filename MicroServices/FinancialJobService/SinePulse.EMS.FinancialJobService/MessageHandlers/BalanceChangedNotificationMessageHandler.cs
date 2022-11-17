using System.Threading.Tasks;
using SinePulse.EMS.FinancialJobService.Services;
using SinePulse.EMS.Messages.FinancialJobMessages;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.FinancialJobService.MessageHandlers
{
  public class BalanceChangedNotificationMessageHandler : IMessageHandler<BalanceChangedNotificationMessage>
  {
    private readonly IDailyBalanceUpdater _dailyBalanceUpdater;

    public BalanceChangedNotificationMessageHandler(IDailyBalanceUpdater dailyBalanceUpdater)
    {
      _dailyBalanceUpdater = dailyBalanceUpdater;
    }

    public async Task Handle(BalanceChangedNotificationMessage message)
    {
      await _dailyBalanceUpdater.UpdateDailyBalance(message.Date, message.BranchMediumAccountsHeadId);
    }
  }
}