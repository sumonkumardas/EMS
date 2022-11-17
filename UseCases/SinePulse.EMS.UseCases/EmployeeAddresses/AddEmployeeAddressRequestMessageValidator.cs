using FluentValidation;
using FluentValidation.Validators;
using SinePulse.EMS.Messages.EmployeeAddressMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.EmployeeAddresses
{
  public class AddEmployeeAddressRequestMessageValidator : AbstractValidator<AddEmployeeAddressRequestMessage>
  {

    public AddEmployeeAddressRequestMessageValidator()
    {
      RuleFor(x => x.PermanentAddressStreet1).NotEmpty().WithMessage("Please Specify Permanent Address Street 1.");
      RuleFor(x => x.PermanentAddressStreet1).NotNull().WithMessage("Please Specify Permanent Address Street 1.");

      RuleFor(x => x.PermanentAddressCity).NotEmpty().WithMessage("Please Specify Permanent Address City.");
      RuleFor(x => x.PermanentAddressCity).NotNull().WithMessage("Please Specify Permanent Address City.");

      RuleFor(x => x.PermanentAddressPostalCode).NotEmpty().WithMessage("Please Specify Permanent Postal code.");
      RuleFor(x => x.PermanentAddressPostalCode).NotNull().WithMessage("Please Specify Permanent Postal code.");
    }
    
  }
}