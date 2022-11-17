using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.Messages.Model.Examinations
{
  public class ResultGradeMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties

    public string GradeLetter { get; set; }
    public decimal GradePoint { get; set; }

    public int StartMark { get; set; }

    public int EndMark { get; set; }

    public StatusEnum Status { get; set; }

    #endregion

    #region  Navigation Properties

    public BranchMediumMessageModel BranchMedium { get; set; }

    #endregion
  }
}
