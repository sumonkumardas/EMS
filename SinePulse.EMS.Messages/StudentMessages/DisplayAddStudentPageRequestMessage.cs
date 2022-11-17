using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.StudentMessages
{
  public class DisplayAddStudentPageRequestMessage
    : IRequest<ValidatedData<DisplayAddStudentPageResponseMessage>>
  {
  }
}