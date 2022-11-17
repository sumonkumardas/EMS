using FluentValidation;
using SinePulse.EMS.Messages.EmployeeEducationalQualificationMessages;
using SinePulse.EMS.UseCases.Repositories;
using System;

namespace SinePulse.EMS.UseCases.EmployeeEducationalQualifications
{
  public class AddEmployeeEducationalQualificationRequestMessageValidator : AbstractValidator<AddEmployeeEducationalQualificationRequestMessage>
  {
    public AddEmployeeEducationalQualificationRequestMessageValidator()
    {
      RuleFor(x => x.InstituteName).NotEmpty().WithMessage("Please specify Institute name");
      RuleFor(x => x.InstituteName).MaximumLength(200).WithMessage("Institute name is too long.");
      RuleFor(x => x.InstituteName).NotNull().WithMessage("Please specify Institute name");

      RuleFor(x => x.DegreeName).NotEmpty().WithMessage("Please specify Degree name");
      RuleFor(x => x.DegreeName).NotNull().WithMessage("Please specify Degree name");

      RuleFor(x => x.MajorSubject).NotEmpty().WithMessage("Please specify Major Subject");
      RuleFor(x => x.MajorSubject).MaximumLength(200).WithMessage("Major Subject is too long.");
      RuleFor(x => x.MajorSubject).NotNull().WithMessage("Please specify Major Subject");

      RuleFor(x => x.PassingYear).NotEmpty().WithMessage("Please specify Passing Year");
      RuleFor(x => x.PassingYear).NotNull().WithMessage("Please specify Passing Year");
    }
  }
}