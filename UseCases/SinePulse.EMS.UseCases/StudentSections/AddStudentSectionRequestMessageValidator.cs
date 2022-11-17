using FluentValidation;
using SinePulse.EMS.Messages.StudentSectionMessages;

namespace SinePulse.EMS.UseCases.StudentSections
{
  public class AddStudentSectionRequestMessageValidator : AbstractValidator<AddStudentSectionRequestMessage>
  {
    public AddStudentSectionRequestMessageValidator()
    {
      RuleFor(x => x.RollNo).NotEmpty().WithMessage("Please Specify Roll No.");
      RuleFor(x => x.RollNo).GreaterThan(0).WithMessage("Invalid Roll No.");
      RuleFor(x => x.SectionId).GreaterThan(0).WithMessage("Please Select Section");
    }
  }
}