using SinePulse.EMS.Domain.Enums;
namespace SinePulse.EMS.Messages.EmployeeAddressMessages
{
  public class AddEmployeeAddressRequestMessage : MediatR.IRequest<AddEmployeeAddressResponseMessage>
  {
    public string PresentAddressStreet1 { get; set; }
    public string PresentAddressStreet2 { get; set; }
    public string PresentAddressPostalCode { get; set; }
    public string PresentAddressCity { get; set; }
    public string PermanentAddressStreet1 { get; set; }
    public string PermanentAddressStreet2 { get; set; }
    public string PermanentAddressPostalCode { get; set; }
    public string PermanentAddressCity { get; set; }
    public bool SameAsPermanentAddress { get; set; }
    public long EmployeeId { get; set; }
    public string CurrentUserName { get; set; }
  }
}