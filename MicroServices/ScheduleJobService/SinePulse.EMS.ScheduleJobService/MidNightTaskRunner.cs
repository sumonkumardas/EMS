using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Constants;
using SinePulse.EMS.Domain.ScheduleJobManagement;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using SinePulse.EMS.Messages.FinancialJobMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.ScheduleJobService
{
  public class MidNightTaskRunner : IMidNightTaskRunner
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;
    private readonly IClosingCandidateSessionProvider _closingCandidateSessionProvider;
    private readonly IMessageSender _messageSender;

    public MidNightTaskRunner(IRepository repository, EmsDbContext dbContext,
      IClosingCandidateSessionProvider closingCandidateSessionProvider, IMessageSender messageSender)
    {
      _repository = repository;
      _dbContext = dbContext;
      _closingCandidateSessionProvider = closingCandidateSessionProvider;
      _messageSender = messageSender;
    }

    public async Task RunMidNightTask()
    {
      using (var atomicTransaction = _dbContext.Database.BeginTransaction())
      {
        try
        {
          var today = DateTime.Today;
          var alreadyRun = await _repository.Filter<MidNightTaskTrackingInfo>(x => x.Date == today)
            .AnyAsync();
          if (alreadyRun) return;

          _repository.Insert(new MidNightTaskTrackingInfo
          {
            Date = today,
            Timestamp = DateTime.Now
          });

          await _dbContext.SaveChangesAsync();
          atomicTransaction.Commit();

          await SendConfigureDailyAlertMessage(today);
          await SendYearClosingMessages();
        }
        catch (Exception)
        {
          atomicTransaction.Rollback();
          throw;
        }
      }
    }

    private async Task SendConfigureDailyAlertMessage(DateTime today)
    {
      var message = new ConfigureDailyAlertMessage {Date = today};
      await _messageSender.Send(message, MicroServiceAddresses.AlertProcessingService);
    }

    private async Task SendYearClosingMessages()
    {
      foreach (var session in _closingCandidateSessionProvider.GetClosingCandidateSessions())
      {
        var message = new RunYearClosingMessage {SessionId = session.Id};
        await _messageSender.Send(message, MicroServiceAddresses.FinancialJobService);
      }
    }
  }
}