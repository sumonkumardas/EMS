using System;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Examinations
{
  public class TestMessageModel : BaseEntityMessageModel
  {

    #region Primitive Properties

    public string TestName { get; set; }

    public string TestDescription { get; set; }

    public decimal Weight { get; set; }

    public decimal FullMarks { get; set; }

    public DateTime StartTimestamp { get; set; }

    public DateTime EndTimestamp { get; set; }

    public StatusEnum Status { get; set; }

    #endregion

    #region  Navigation Properties

    public ExamConfigurationMessageModel ExamType { get; set; }

    public SectionMessageModel Section { get; set; }

    public BranchMediumMessageModel BranchMedium { get; set; }

    #endregion
  }
}