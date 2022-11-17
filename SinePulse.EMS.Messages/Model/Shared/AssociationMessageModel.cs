using System.ComponentModel.DataAnnotations.Schema;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Messages.Model.Shared
{
  [ComplexType]
  public class AssociationMessageModel
  {
    public AssociationType AssociatedWith { get; set; }
    public InstituteMessageModel Institute { get; set; }
    public BranchMessageModel Branch { get; set; }
    public BranchMediumMessageModel BranchMedium { get; set; }
  }
}