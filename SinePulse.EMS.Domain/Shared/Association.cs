using System.ComponentModel.DataAnnotations.Schema;
using SinePulse.EMS.Domain.Academic;

namespace SinePulse.EMS.Domain.Shared
{
  [ComplexType]
  public class Association
  {
    public virtual AssociationType AssociatedWith { get; set; }
    public virtual Institute Institute { get; set; }
    public virtual Branch Branch { get; set; }
    public virtual BranchMedium BranchMedium { get; set; }
  }
}