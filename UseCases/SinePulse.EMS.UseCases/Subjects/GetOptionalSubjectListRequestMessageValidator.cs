using FluentValidation;
using SinePulse.EMS.Messages.SubjectMessages;

namespace SinePulse.EMS.UseCases.Subjects
{
  public class GetOptionalSubjectListRequestMessageValidator : AbstractValidator<GetOptionalSubjectListRequestMessage>
  {
    public GetOptionalSubjectListRequestMessageValidator()
    {
    }
  }
}