using FluentValidation;
using SinePulse.EMS.Messages.StudentMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Students
{
  public class EditStudentRequestMessageValidator : AbstractValidator<EditStudentRequestMessage>
  {
    private readonly IUniqueRollPropertyChecker _uniqueRollPropertyChecker;
    public EditStudentRequestMessageValidator(IUniqueRollPropertyChecker uniqueRollPropertyChecker)
    {
      _uniqueRollPropertyChecker = uniqueRollPropertyChecker;
      RuleFor(_ => _.BirthDate).NotEmpty().WithMessage("Please specify Birth Date of Student");
      RuleFor(_ => _.BirthDate).NotNull().WithMessage("Please specify Birth Date of Student");
      RuleFor(_ => _.FullName).NotEmpty().WithMessage("Please specify Full Name of Student");
      RuleFor(_ => _.FullName).NotNull().WithMessage("Please specify Full Name of Student");
      RuleFor(_ => _.Email).EmailAddress().WithMessage("Invalid Email Address. Please enter a valid email address");
      RuleFor(x => x.RollNo).Must((m, x) => IsUniqueRollNo(m.RollNo, m.SectionId, m.ClassId,m.StudentId))
        .WithMessage("Entered Roll is already assigned for some other student.");
    }

    private bool IsUniqueRollNo(int rollNo, long sectionId, long classId,long studentId)
    {
      return _uniqueRollPropertyChecker.IsUniqueRollNo(rollNo, sectionId, classId,studentId);
    }
  }
}