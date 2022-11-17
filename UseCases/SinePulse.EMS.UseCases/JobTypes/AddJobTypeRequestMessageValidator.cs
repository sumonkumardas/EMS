using FluentValidation;
using SinePulse.EMS.Messages.JobTypeMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.JobTypes
{
  public class AddJobTypeRequestMessageValidator : AbstractValidator<AddJobTypeRequestMessage>
  {
    private readonly IUniqueJobTypePropertyChecker _uniqueJobTypePropertyChecker;

    public AddJobTypeRequestMessageValidator(IUniqueJobTypePropertyChecker uniqueJobTypePropertyChecker)
    {
      _uniqueJobTypePropertyChecker = uniqueJobTypePropertyChecker;
      RuleFor(x => x.Title).NotEmpty().NotNull().WithMessage("Please specify Title");
      RuleFor(x => x.Title).Must(IsUniqueTitle).WithMessage("Job Title already exists!");
    }

    private bool IsUniqueTitle(string title)
    {
      return _uniqueJobTypePropertyChecker.IsUniqueJobTypeTitle(title);
    }
  }
}