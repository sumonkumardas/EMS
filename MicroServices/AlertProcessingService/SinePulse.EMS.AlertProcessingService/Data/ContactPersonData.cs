using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.AlertProcessingService.Data
{
  public class ContactPersonData
  {
    public virtual RelationTypeEnum RelationType { get; set; }
    public virtual string Name { get; set; }
    public virtual string PhoneNo { get; set; }
    public virtual string EmailAddress { get; set; }
  }
}