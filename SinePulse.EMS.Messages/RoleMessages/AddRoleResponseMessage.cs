namespace SinePulse.EMS.Messages.RoleMessages
{
  public class AddRoleResponseMessage
  {
    public bool IsSuccess { get; }

    public AddRoleResponseMessage(bool IsSuccess)
    {
      this.IsSuccess = IsSuccess;
    }
  }
}