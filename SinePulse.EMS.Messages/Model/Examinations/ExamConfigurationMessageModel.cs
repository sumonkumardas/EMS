using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Examinations
{
  public class ExamConfigurationMessageModel : BaseEntityMessageModel
  {

    #region Primitive Properties

    public string ExamTypeName { get; set; }

    public int SubjectiveFullMark { get; set; }
    public int SubjectivePassMark { get; set; }
    public int ObjectiveFullMark { get; set; }
    public int ObjectivePassMark { get; set; }
    public int PracticalFullMark { get; set; }
    public int PracticalPassMark { get; set; }
    public int ClassTestPercentage { get; set; }

    public StatusEnum Status { get; set; }

    #endregion

    #region  Navigation Properties

    public TermMessageModel Term { get; set; }

    public ClassSubjectMessageModel ClassSubject { get; set; }

    #endregion
  }
}
