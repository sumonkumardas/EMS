using FluentValidation;
using SinePulse.EMS.Messages.ExamConfigurationMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.ExamConfigurations
{
  public class ShowExamTypeRequestMessageValidator : AbstractValidator<ShowExamTypeRequestMessage>
  {
    private readonly IValidExamTypeChecker _validExamTypeChecker;

    public ShowExamTypeRequestMessageValidator(IValidExamTypeChecker validExamTypeChecker)
    {
      _validExamTypeChecker = validExamTypeChecker;

      RuleFor(x => x.ExamTypeId).Must(IsValidExamType).WithMessage("This Exam Type doesn't exists");
    }

    private bool IsValidExamType(long examTypeId)
    {
      return _validExamTypeChecker.IsValidExamType(examTypeId);
    }
  }
}