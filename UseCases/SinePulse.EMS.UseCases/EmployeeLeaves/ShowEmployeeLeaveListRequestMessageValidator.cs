using FluentValidation;
using SinePulse.EMS.Messages.EmployeeLeaveMessages;
using SinePulse.EMS.UseCases.Repositories;
using System;

namespace SinePulse.EMS.UseCases.EmployeeLeaves
{
  public class ShowEmployeeLeaveListRequestMessageValidator : AbstractValidator<ShowEmployeeLeaveListRequestMessage>
  {
    private readonly ILeaveAmountValidityChecker _leaveAmountValidityChecker;
    public ShowEmployeeLeaveListRequestMessageValidator()
    {
     
    }

    private bool IsLeaveAmountValid(AddEmployeeLeaveRequestMessage message,DateTime endDate)
    {
      return _leaveAmountValidityChecker.IsLeaveAmountValid(message.EmployeeId, message.LeaveTypeId, message.StartDate, message.EndDate);
    }
  }
}