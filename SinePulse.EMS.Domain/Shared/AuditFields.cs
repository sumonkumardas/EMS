using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SinePulse.EMS.Domain.Shared
{
  [ComplexType]
  public class AuditFields
  {
    public virtual string InsertedBy { get; set; }
    public virtual DateTime InsertedDateTime { get; set; }
    public virtual string LastUpdatedBy { get; set; }
    public virtual DateTime LastUpdatedDateTime { get; set; }
  }
}
