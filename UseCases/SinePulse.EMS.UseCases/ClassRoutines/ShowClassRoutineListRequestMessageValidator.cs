using FluentValidation;
using SinePulse.EMS.Messages.ClassRoutineMessages;

namespace SinePulse.EMS.UseCases.ClassRoutines
{
  public class ShowClassRoutineListRequestMessageValidator : AbstractValidator<ShowClassRoutineListRequestMessage>
  {

    public ShowClassRoutineListRequestMessageValidator()
    {
    }
    
  }
}