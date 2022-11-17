using FluentValidation;
using SinePulse.EMS.Messages.SectionMessages;

namespace SinePulse.EMS.UseCases.Sections
{
  public class AddSectionRequestMessageValidator : AbstractValidator<AddSectionRequestMessage>
  {
    //private readonly IUniqueSectionPropertyChecker _uniqueSectionPropertyChecker;

    public AddSectionRequestMessageValidator() //(IUniqueSectionPropertyChecker uniqueSectionPropertyChecker)
    {
      // _uniqueSectionPropertyChecker = uniqueSectionPropertyChecker;

      RuleFor(x => x.SectionName).NotEmpty().WithMessage("Please specify Section.");
      RuleFor(x => x.SectionName).MaximumLength(200).WithMessage("Section name is too long.");
      RuleFor(x => x.SectionName).MinimumLength(3)
        .WithMessage("'Section Name' is too short, it should have minimum 3 characters.");
      RuleFor(x => x.SectionName).NotNull().WithMessage("Please specify Section.");
      //RuleFor(x => x.SectionName).Must(IsUniqueSectionName).WithMessage("Section name already exists.");

      RuleFor(x => x.NumberOfStudents).NotEmpty().WithMessage("Please specify Number of Students.");
      RuleFor(x => x.NumberOfStudents).NotNull().WithMessage("Please specify Number of Students.");

      RuleFor(x => x.TotalClasses).NotEmpty().WithMessage("Please specify Total Classes.");
      RuleFor(x => x.TotalClasses).NotNull().WithMessage("Please specify Total Classes.");
      RuleFor(x => x.TotalClasses).GreaterThan(0).WithMessage("Total Classes must be bigger than 0.");
      RuleFor(x => x.TotalClasses).LessThan(13).WithMessage("Total Classes must be less than 13.");

      RuleFor(x => x.DurationOfClass).NotEmpty().WithMessage("Please specify Duration Of Class.");
      RuleFor(x => x.DurationOfClass).NotNull().WithMessage("Please specify Duration Of Class.");
      RuleFor(x => x.DurationOfClass).GreaterThan(0).WithMessage("Duration Of Class must be bigger than 0.");
    }

    //private bool IsUniqueSectionName(string sessionName)
    //{
    //  return _uniqueSectionPropertyChecker.IsUniqueSectionName(sessionName);
    //}
  }
}