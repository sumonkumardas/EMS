using System;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Academic
{
  public class SessionMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties
    public string SessionName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsSessionClosed { get; set; }
    public ObjectTypeEnum SessionType { get; set; }
    public StatusEnum Status { get; set; }
    #endregion

    #region  Navigation Properties

    public InstituteMessageModel Institute { get; set; }
    public BranchMessageModel Branch { get; set; }
    public BranchMediumMessageModel BranchMedium { get; set; }

    #endregion
  }
}