using NakedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinePulse.EMS.NO.Domain.Shared
{
  public class NOBaseEntity
  {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [NakedObjectsIgnore]
    public virtual long Id { get; set; }
  }
}
