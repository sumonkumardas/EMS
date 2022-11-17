using FluentValidation;
using SinePulse.EMS.Messages.StudentMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Students
{
  public class ShowStudentRequestMessageValidator : AbstractValidator<ShowStudentRequestMessage>
  {
    private readonly IValidStudentChecker _validStudentChecker;

    public ShowStudentRequestMessageValidator(IValidStudentChecker validStudentChecker)
    {
      _validStudentChecker = validStudentChecker;

      RuleFor(x => x.StudentId).Must(IsValidStudent).WithMessage("This Grade doesn't exists");
    }

    private bool IsValidStudent(long examTypeId)
    {
      return _validStudentChecker.IsValidStudent(examTypeId);
    }
  }
}