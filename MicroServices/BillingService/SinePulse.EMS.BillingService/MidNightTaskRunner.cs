using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.BillingService.Model;
using SinePulse.EMS.Constants;
using SinePulse.EMS.Domain.ScheduleJobManagement;
using SinePulse.EMS.Messages.AlertProcessingMessages;
using SinePulse.EMS.Messages.EmailSendingMessages;
using SinePulse.EMS.Messages.SmsSendingMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.Utility.MessagingQueue;

namespace SinePulse.EMS.BillingService
{
  public class MidNightTaskRunner : IMidNightTaskRunner
  {

    private readonly IMessageSender _messageSender;
    private readonly IDueBillChecker _dueBillChecker;

    public MidNightTaskRunner(IMessageSender messageSender, IDueBillChecker dueBillChecker)
    {
      _messageSender = messageSender;
      _dueBillChecker = dueBillChecker;
    }

    public async Task RunMidNightTask()
    {
      try
      {
        if (DateTime.Now.Day == 1)
        {
          var list = _dueBillChecker.GetDueBillsOfBranchMedium();
          foreach (var dueBillOfBranchMedium in list)
          {
            await SendBillingAlertEmailMessage(dueBillOfBranchMedium);
            await SendBillingAlertSmsMessage(dueBillOfBranchMedium);
          }
        }

        
      }
      catch (Exception ex)
      {
        throw;
      }

    }

    private async Task SendBillingAlertEmailMessage(DueBillOfBranchMedium dueBillOfBranchMedium)
    {
      if (!(string.IsNullOrEmpty(dueBillOfBranchMedium.Email)) && dueBillOfBranchMedium.Email.Trim().Length > 0)
      {
        var message = new EmailMessage()
        {
          Subject = "Due Bill",
          Body = $"Dear Consumer, Due bill of {dueBillOfBranchMedium.Month:G}, {dueBillOfBranchMedium.Year} of {dueBillOfBranchMedium.BranchMediumName} is BDT {dueBillOfBranchMedium.DueBill:0.00}.\n- SinePulseEms",
          From = "",
          ToList = new List<string> { dueBillOfBranchMedium.Email }
        };
        await _messageSender.Send(message, MicroServiceAddresses.EmailSendingService);
      }
    }
    private async Task SendBillingAlertSmsMessage(DueBillOfBranchMedium dueBillOfBranchMedium)
    {
      if (!(string.IsNullOrEmpty(dueBillOfBranchMedium.MobileNo)) && dueBillOfBranchMedium.MobileNo.Trim().Length > 0)
      {

        var smsMessage = new SmsMessage
        {
          PhoneNumbers = new List<string> { dueBillOfBranchMedium.MobileNo },
          Message =
            $"Dear Consumer, Due bill of {dueBillOfBranchMedium.Month:G}, {dueBillOfBranchMedium.Year} of {dueBillOfBranchMedium.BranchMediumName} is BDT {dueBillOfBranchMedium.DueBill:0.00}.\n- SinePulseEms"
        };
        await _messageSender.Send(smsMessage, MicroServiceAddresses.SmsSendingService);
      }
    }
  }
}
