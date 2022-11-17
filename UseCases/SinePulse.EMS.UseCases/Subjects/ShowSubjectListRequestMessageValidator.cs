using FluentValidation;
using SinePulse.EMS.Messages.SubjectMessages;

namespace SinePulse.EMS.UseCases.Subjects
{
  public class ShowSubjectListRequestMessageValidator : AbstractValidator<ShowSubjectListRequestMessage>
  {
    public ShowSubjectListRequestMessageValidator()
    {
    }
  }
}