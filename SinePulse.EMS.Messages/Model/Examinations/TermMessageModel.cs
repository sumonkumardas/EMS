using System;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Examinations
{
  public class TermMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties

    public string TermName { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public StatusEnum Status { get; set; }

    #endregion

    #region  Navigation Properties

    public SessionMessageModel Session { set; get; }

    #endregion
  }
}