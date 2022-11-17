using FluentValidation;
using SinePulse.EMS.Messages.ResultGradeMessages;

namespace SinePulse.EMS.UseCases.ResultGrades
{
  public class ShowResultGradeListRequestMessageValidator : AbstractValidator<ShowResultGradeListRequestMessage>
  {
    
    public ShowResultGradeListRequestMessageValidator()
    {
    }
  }
}