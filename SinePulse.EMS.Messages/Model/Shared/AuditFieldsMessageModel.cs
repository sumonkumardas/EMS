using System;

namespace SinePulse.EMS.Messages.Model.Shared
{
  public class AuditFieldsMessageModel
  {
    public string InsertedBy { get; set; }
    public DateTime InsertedDateTime { get; set; }
    public string LastUpdatedBy { get; set; }
    public DateTime LastUpdatedDateTime { get; set; }
  }
}
