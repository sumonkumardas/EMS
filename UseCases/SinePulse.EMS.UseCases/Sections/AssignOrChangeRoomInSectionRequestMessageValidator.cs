using FluentValidation;
using SinePulse.EMS.Messages.SectionMessages;

namespace SinePulse.EMS.UseCases.Sections
{
  public class AssignOrChangeRoomInSectionRequestMessageValidator : AbstractValidator<AssignOrChangeRoomInSectionRequestMessage>
  {
    public AssignOrChangeRoomInSectionRequestMessageValidator()
    {
    }
  }
}