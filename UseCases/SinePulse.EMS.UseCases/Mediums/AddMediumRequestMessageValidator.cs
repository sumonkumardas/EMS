using FluentValidation;
using SinePulse.EMS.Messages.MediumMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Mediums
{
  public class AddMediumRequestMessageValidator : AbstractValidator<AddMediumRequestMessage>
  {
    private readonly IUniqueMediumPropertyChecker _uniqueMediumPropertyChecker;

    public AddMediumRequestMessageValidator(IUniqueMediumPropertyChecker uniqueMediumPropertyChecker)
    {
      _uniqueMediumPropertyChecker = uniqueMediumPropertyChecker;
      RuleFor(x => x.MediumName).NotEmpty().WithMessage("Please specify Medium name");
      RuleFor(x => x.MediumName).NotNull().WithMessage("Please specify Medium name");
      RuleFor(x => x.MediumName).MaximumLength(200).WithMessage("Medium name is too long");
      RuleFor(x => x.MediumName).Must(IsUniqueMediumName).WithMessage("Medium name already exists.");

      RuleFor(x => x.MediumCode).NotEmpty().WithMessage("Please specify Medium Code");
      RuleFor(x => x.MediumCode).NotNull().WithMessage("Please specify Medium Code");
      RuleFor(x => x.MediumCode).MaximumLength(200).WithMessage("Medium Code is too long");
      RuleFor(x => x.MediumCode).Must(IsUniqueMediumCode).WithMessage("Medium Code already exists.");
    }

    private bool IsUniqueMediumCode(string mediumCode)
    {
      return _uniqueMediumPropertyChecker.IsUniqueMediumCode(mediumCode);
    }

    private bool IsUniqueMediumName(string mediumName)
    {
      return _uniqueMediumPropertyChecker.IsUniqueMediumName(mediumName);
    }
  }
}