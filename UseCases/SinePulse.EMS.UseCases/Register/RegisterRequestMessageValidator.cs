using FluentValidation;
using SinePulse.EMS.Messages.EmployeeMessages;
using SinePulse.EMS.Messages.RegisterMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Register
{
  public class RegisterRequestMessageValidator : AbstractValidator<RegisterRequestMessage>
  {
    private readonly IValidEmployeeRegisterDataChecker _validEmployeeRegisterDataChecker;
    private readonly IValidEmployeeRegistrationChecker _validEmployeeRegistrationChecker;

    public RegisterRequestMessageValidator(IValidEmployeeRegisterDataChecker validEmployeeRegisterDataChecker,
      IValidEmployeeRegistrationChecker validEmployeeRegistrationChecker)
    {
      _validEmployeeRegisterDataChecker = validEmployeeRegisterDataChecker;
      _validEmployeeRegistrationChecker = validEmployeeRegistrationChecker;
      RuleFor(x => x.FullName).NotEmpty().WithMessage("Please specify Full Name.");
      RuleFor(x => x.FullName).NotNull().WithMessage("Please specify Full Name.");

      RuleFor(x => x.DOB).NotEmpty().WithMessage("Please specify Date of Birth.");
      RuleFor(x => x.DOB).NotNull().WithMessage("Please specify Date of Birth.");

      RuleFor(x => x.JoiningDate).NotEmpty().WithMessage("Please specify Joining Date.");
      RuleFor(x => x.JoiningDate).NotNull().WithMessage("Please specify Joining Date.");

      RuleFor(x => x.UserName).NotEmpty().WithMessage("Please specify User Name.");
      RuleFor(x => x.UserName).NotNull().WithMessage("Please specify User Name.");

      RuleFor(x => x.Password).NotEmpty().WithMessage("Please specify Password.");
      RuleFor(x => x.Password).NotNull().WithMessage("Please specify Password.");

      RuleFor(x => x).Must(IsEmployeeExists)
        .WithMessage("No employee found with these information. Please check again.");
      RuleFor(x => x).Must(IsAlreadyRegistered)
        .WithMessage("You are already registered. Please Log in.");
    }

    private bool IsAlreadyRegistered(RegisterRequestMessage requestMessage)
    {
      var registerDataCheckRequestMessage = GetRegisterDataCheckRequestMessage(requestMessage);
      return _validEmployeeRegistrationChecker.IsValidRegistration(registerDataCheckRequestMessage);
    }


    private bool IsEmployeeExists(RegisterRequestMessage requestMessage)
    {
      var dataCheckRequestMessage = GetRegisterDataCheckRequestMessage(requestMessage);
      return _validEmployeeRegisterDataChecker.IsValidEmployee(dataCheckRequestMessage);
    }

    private static RegisterDataCheckRequestMessage GetRegisterDataCheckRequestMessage(
      RegisterRequestMessage requestMessage)
    {
      RegisterDataCheckRequestMessage registerDataCheckRequestMessage = new RegisterDataCheckRequestMessage
      {
        FullName = requestMessage.FullName,
        DOB = requestMessage.DOB,
        JoiningDate = requestMessage.JoiningDate
      };
      return registerDataCheckRequestMessage;
    }
  }
}