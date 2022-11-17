using FluentValidation;
using SinePulse.EMS.Messages.EmployeeMessages;
using System;

namespace SinePulse.EMS.UseCases.Employee
{
  public class AddEmployeeImageRequestMessageValidator : AbstractValidator<AddEmployeeImageRequestMessage>
  {
    public AddEmployeeImageRequestMessageValidator()
    {
    }
  }
}