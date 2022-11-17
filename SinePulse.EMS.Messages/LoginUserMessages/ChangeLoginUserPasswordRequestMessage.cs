using MediatR;

namespace SinePulse.EMS.Messages.LoginUserMessages
{
  public class ChangeLoginUserPasswordRequestMessage : IRequest<ChangeLoginUserPasswordResponseMessage>
  {
    public string EmployeeCode { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string RepeatPassword { get; set; }
    public string CurrentUserRoleName { get; set; }
    public string EmployeeRoleName { get; set; }
  }
}