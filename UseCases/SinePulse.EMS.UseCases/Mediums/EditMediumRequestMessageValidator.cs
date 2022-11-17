using FluentValidation;
using SinePulse.EMS.Messages.MediumMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Mediums
{
  public class EditMediumRequestMessageValidator : AbstractValidator<EditMediumRequestMessage>
  {
    private readonly IUniqueMediumPropertyChecker _uniqueMediumPropertyChecker;

    public EditMediumRequestMessageValidator(IUniqueMediumPropertyChecker uniqueMediumPropertyChecker)
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

    private bool IsUniqueMediumCode(EditMediumRequestMessage medium, string mediumCode)
    {
      return _uniqueMediumPropertyChecker.IsUniqueMediumCode(mediumCode, medium.MediumId);
    }

    private bool IsUniqueMediumName(EditMediumRequestMessage medium, string mediumName)
    {
      return _uniqueMediumPropertyChecker.IsUniqueMediumName(mediumName, medium.MediumId);
    }
  }
}