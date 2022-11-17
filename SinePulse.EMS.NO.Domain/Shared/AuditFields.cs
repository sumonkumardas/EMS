using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinePulse.EMS.NO.Domain.Shared
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
