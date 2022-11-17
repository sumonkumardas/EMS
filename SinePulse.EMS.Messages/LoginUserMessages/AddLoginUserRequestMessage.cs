using MediatR;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Messages.LoginUserMessages
{
  public class AddLoginUserRequestMessage : IRequest<AddLoginUserResponseMessage>
  {
    public long EmployeeId { get; set; }
    public string Password { get; set; }
    public string CurrentUserRoleName { get; set; }
    public string RepeatPassword { get; set; }
    public string RoleId { get; set; }
    public string InstituteId { get; set; }
    public string BranchId { get; set; }
    public string BranchMediumId { get; set; }
  }
}