using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.LoginMessages
{
  public class DisplayLoginPageRequestMessage : IRequest<ValidatedData<DisplayLoginPageResponseMessage>>
  {
  }
}