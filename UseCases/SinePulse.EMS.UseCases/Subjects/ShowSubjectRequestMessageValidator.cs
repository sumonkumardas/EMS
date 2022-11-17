using FluentValidation;
using SinePulse.EMS.Messages.SubjectMessages;

namespace SinePulse.EMS.UseCases.Subjects
{
  public class ShowSubjectRequestMessageValidator : AbstractValidator<ShowSubjectRequestMessage>
  {
    public ShowSubjectRequestMessageValidator()
    {
    }
  }
}