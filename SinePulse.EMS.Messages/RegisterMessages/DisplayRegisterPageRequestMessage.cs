using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.RegisterMessages
{
  public class DisplayRegisterPageRequestMessage : IRequest<ValidatedData<DisplayRegisterPageResponseMessage>>
  {
  }
}