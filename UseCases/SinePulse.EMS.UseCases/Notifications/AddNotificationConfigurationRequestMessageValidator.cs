using FluentValidation;
using SinePulse.EMS.Messages.NotificationMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.Notifications
{
  public class AddNotificationConfigurationRequestMessageValidator : AbstractValidator<AddNotificationConfigurationRequestMessage>
  {

    public AddNotificationConfigurationRequestMessageValidator()
    {
      RuleFor(x => x.EntryDelayPeriod).NotEmpty().WithMessage("Please specify Entry Delay Period");
      RuleFor(x => x.EntryDelayPeriod).NotNull().WithMessage("Please specify Entry Delay Period");
      RuleFor(x => x.EntryDelayPeriod).GreaterThanOrEqualTo(0).WithMessage("Entry Delay Period can not be negative");

      RuleFor(x => x.AbsentPeriod).NotEmpty().WithMessage("Please specify Absent Period");
      RuleFor(x => x.AbsentPeriod).NotNull().WithMessage("Please specify Absent Period");
      RuleFor(x => x.AbsentPeriod).GreaterThanOrEqualTo(0).WithMessage("Absent Period can not be negative");

      RuleFor(x => x.ExamStartPeriod).NotEmpty().WithMessage("Please specify Exam Start Period");
      RuleFor(x => x.ExamStartPeriod).NotNull().WithMessage("Please specify Exam Start Period");
      RuleFor(x => x.ExamStartPeriod).GreaterThanOrEqualTo(0).WithMessage("Exam Start Period can not be negative");

      RuleFor(x => x.ClassTestStartPeriod).NotEmpty().WithMessage("Please specify Class Test Start Period");
      RuleFor(x => x.ClassTestStartPeriod).NotNull().WithMessage("Please specify Class Test Start Period");
      RuleFor(x => x.ClassTestStartPeriod).GreaterThanOrEqualTo(0).WithMessage("Class Test Start Period can not be negative");

      RuleFor(x => x.TermTestStartPeriod).NotEmpty().WithMessage("Please specify Term Test Start Period");
      RuleFor(x => x.TermTestStartPeriod).NotNull().WithMessage("Please specify Term Test Start Period");
      RuleFor(x => x.TermTestStartPeriod).GreaterThanOrEqualTo(0).WithMessage("Term Test Star tPeriod can not be negative");

      RuleFor(x => x.ResultGradePreparePeriod).NotEmpty().WithMessage("Please specify Result Grade Prepare Period");
      RuleFor(x => x.ResultGradePreparePeriod).NotNull().WithMessage("Please specify Result Grade Prepare Period");
      RuleFor(x => x.ResultGradePreparePeriod).GreaterThanOrEqualTo(0).WithMessage("Result Grade Prepare Period can not be negative");
    }
  }
}