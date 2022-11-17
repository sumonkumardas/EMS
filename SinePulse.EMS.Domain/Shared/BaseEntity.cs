using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SinePulse.EMS.Domain.Shared
{
  public class BaseEntity
  {
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual long Id { get; set; }

    public virtual string Title()
    {
      return String.Empty;
    }
  }
}
