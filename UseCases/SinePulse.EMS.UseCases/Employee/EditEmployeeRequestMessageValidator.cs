using FluentValidation;
using SinePulse.EMS.Messages.EmployeeMessages;
using SinePulse.EMS.UseCases.Repositories;
using System;

namespace SinePulse.EMS.UseCases.Employee
{
  public class EditEmployeeRequestMessageValidator : AbstractValidator<EditEmployeeRequestMessage>
  {
    public EditEmployeeRequestMessageValidator()
    {
      RuleFor(x => x.FullName).NotEmpty().WithMessage("Please specify Full name");
      RuleFor(x => x.FullName).MaximumLength(200).WithMessage("Full name is too long.");
      RuleFor(x => x.FullName).NotNull().WithMessage("Please specify Full name");

      RuleFor(x => x.DOB).NotEmpty().WithMessage("Please specify Date of Birth");
      RuleFor(x => x.DOB).NotNull().WithMessage("Please specify Date of Birth");
      RuleFor(x => x.DOB).LessThan(p => DateTime.Now).WithMessage("Please specify a valid Date of Birth");

      RuleFor(x => x.JoiningDate).NotEmpty().WithMessage("Please specify Joining Date");
      RuleFor(x => x.JoiningDate).NotNull().WithMessage("Please specify Joining Date");
      RuleFor(x => x.JoiningDate).LessThan(p => DateTime.Now).WithMessage("Please specify a valid Joining Date");
      RuleFor(x => x.JoiningDate).GreaterThan(p => p.DOB).WithMessage("Joining Date must be greater than Date of Birth.");

      RuleFor(x => x.EmployeeType).NotEmpty().WithMessage("Please specify Employee Type");
      RuleFor(x => x.EmployeeType).NotNull().WithMessage("Please specify Employee Type");
      RuleFor(x => x.EmailAddress).EmailAddress().WithMessage("Please specify a valid email address."); 
      
    }
  }
}