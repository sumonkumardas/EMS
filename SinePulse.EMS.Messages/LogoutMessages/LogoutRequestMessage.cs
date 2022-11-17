using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.LogoutMessages
{
  public class LogoutRequestMessage : IRequest<ValidatedData<LogoutResponseMessage>>
  {
  }
}