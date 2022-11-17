using System;
using FluentValidation;
using SinePulse.EMS.Messages.ClassMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Classes
{
  public class AddClassRequestMessageValidator : AbstractValidator<AddClassRequestMessage>
  {
    private readonly IUniqueClassPropertyChecker _uniqueClassPropertyChecker;

    public AddClassRequestMessageValidator(IUniqueClassPropertyChecker uniqueClassPropertyChecker)
    {
      _uniqueClassPropertyChecker = uniqueClassPropertyChecker;
      RuleFor(x => x.ClassName).NotEmpty().WithMessage("Please specify Class name");
      RuleFor(x => x.ClassName).NotNull().WithMessage("Please specify Class name");
      RuleFor(x => x.ClassName).MaximumLength(200).WithMessage("Class name is too long");
      RuleFor(x => x.ClassName).Must(IsUniqueClassName).WithMessage("Class name already exists.");
      RuleFor(x => x.ClassCode).NotEmpty().WithMessage("Please specify Class Code");
      RuleFor(x => x.ClassCode).NotNull().WithMessage("Please specify Class Code");
      RuleFor(x => x.ClassCode).Must(IsUniqueClassCode).WithMessage("Class Code already exists.");
    }

    private bool IsUniqueClassCode(int classCode)
    {
      return _uniqueClassPropertyChecker.IsUniqueClassCode(classCode);
    }

    private bool IsUniqueClassName(string className)
    {
      return _uniqueClassPropertyChecker.IsUniqueClassName(className);
    }
  }
}