using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.LoginMessages
{
  public class LoginRequestMessage : IRequest<ValidatedData<LoginResponseMessage>>
  {
    public string UserName { get; set; }
    public string Password { get; set; }
  }
}