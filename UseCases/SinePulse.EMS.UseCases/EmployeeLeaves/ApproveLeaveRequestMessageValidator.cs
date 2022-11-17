using FluentValidation;
using SinePulse.EMS.Messages.EmployeeLeaveMessages;
using SinePulse.EMS.UseCases.Repositories;
using System;

namespace SinePulse.EMS.UseCases.EmployeeLeaves
{
  public class ApproveLeaveRequestMessageValidator : AbstractValidator<ApproveLeaveRequestMessage>
  {
    private readonly ILeaveAmountValidityChecker _leaveAmountValidityChecker;
    public ApproveLeaveRequestMessageValidator()
    {
      
    }

    private bool IsLeaveAmountValid(AddEmployeeLeaveRequestMessage message,DateTime endDate)
    {
      return _leaveAmountValidityChecker.IsLeaveAmountValid(message.EmployeeId, message.LeaveTypeId, message.StartDate, message.EndDate);
    }
  }
}