using FluentValidation;
using SinePulse.EMS.Messages.StudentMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Students
{
  public class AddStudentRequestMessageValidator : AbstractValidator<AddStudentRequestMessage>
  {
    private readonly IUniqueRollPropertyChecker _uniqueRollPropertyChecker;
    public AddStudentRequestMessageValidator(IUniqueRollPropertyChecker uniqueRollPropertyChecker)
    {
      _uniqueRollPropertyChecker = uniqueRollPropertyChecker;
      RuleFor(_ => _.FullName).NotEmpty().WithMessage("Please specify Full Name of Student");
      RuleFor(_ => _.FullName).NotNull().WithMessage("Please specify Full Name of Student");
      RuleFor(_ => _.BirthDate).NotEmpty().WithMessage("Please specify Birth Date of Student");
      RuleFor(_ => _.BirthDate).NotNull().WithMessage("Please specify Birth Date of Student");
      RuleFor(x => x.RollNo).Must((m, x) => IsUniqueRollNo(m.RollNo, m.SectionId, m.ClassId))
        .WithMessage("Entered Roll is already assigned for some other student.");
      RuleFor(_ => _.Email).EmailAddress().WithMessage("Invalid Email Address. Please enter a valid email address");
    }

    private bool IsUniqueRollNo(int rollNo,long sectionId,long classId)
    {
      return _uniqueRollPropertyChecker.IsUniqueRollNo(rollNo,sectionId,classId);
    }
  }
}