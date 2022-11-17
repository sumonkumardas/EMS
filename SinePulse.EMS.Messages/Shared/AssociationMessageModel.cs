using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Messages.Shared
{
  public class AssociationMessageModel
  {
    public AssociationType AssociatedWith { get; set; }
    public InstituteMessageModel Institute { get; set; }
    public BranchMessageModel Branch { get; set; }
    public MediumMessageModel Medium { get; set; }
    public BranchMediumMessageModel BranchMedium { get; set; }
  }
}