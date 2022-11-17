using FluentValidation;
using SinePulse.EMS.Messages.ExamConfigurationMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.ExamConfigurations
{
  public class ShowExamConfigurationRequestMessageValidator : AbstractValidator<ShowExamConfigurationRequestMessage>
  {
    private readonly IValidExamConfigurationChecker _validExamConfigurationChecker;

    public ShowExamConfigurationRequestMessageValidator(IValidExamConfigurationChecker validExamConfigurationChecker)
    {
      _validExamConfigurationChecker = validExamConfigurationChecker;

      RuleFor(x => x.ExamConfigurationId).Must(IsValidExamConfiguration).WithMessage("This Exam configuration doesn't exists");
    }

    private bool IsValidExamConfiguration(long examConfigurationId)
    {
      return _validExamConfigurationChecker.IsValidExamConfiguration(examConfigurationId);
    }
  }
}