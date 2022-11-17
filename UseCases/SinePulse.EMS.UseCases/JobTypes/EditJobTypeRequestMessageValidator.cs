using FluentValidation;
using SinePulse.EMS.Messages.JobTypeMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.JobTypes
{
  public class EditJobTypeRequestMessageValidator : AbstractValidator<EditJobTypeRequestMessage>
  {
    private readonly IUniqueJobTypePropertyChecker _uniqueJobTypePropertyChecker;

    public EditJobTypeRequestMessageValidator(IUniqueJobTypePropertyChecker uniqueJobTypePropertyChecker)
    {
      _uniqueJobTypePropertyChecker = uniqueJobTypePropertyChecker;
      RuleFor(x => x.JobTitle).NotEmpty().NotNull().WithMessage("Please specify Job Title");
      RuleFor(x => x).Must((m, x) => IsUniqueTitle(m.JobTitle, m.JobTypeId)).WithMessage("Job Title already exists!");
    }

    private bool IsUniqueTitle(string title, long jobTypeId)
    {
      return _uniqueJobTypePropertyChecker.IsUniqueJobTypeTitle(title, jobTypeId);
    }
  }
}