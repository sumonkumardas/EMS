using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Examinations;
using SinePulse.EMS.Messages.Model.Routines;
using SinePulse.EMS.Messages.Model.Shared;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.Model.Academic
{
  public class SectionMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties
    public GroupEnum Group { get; set; }
    public string SectionName { get; set; }
    public int NumberOfStudents { get; set; }
    public int TotalClasses { get; set; }
    public int DurationOfClass { get; set; }
    public StatusEnum Status { get; set; }

    #endregion

    #region  Navigation Properties

    public ClassMessageModel Class { get; set; }
    public BranchMediumMessageModel BranchMedium { get; set; }
    public SessionMessageModel Session { get; set; }
    public ICollection<ClassRoutineMessageModel> Routines { get; set; } = new List<ClassRoutineMessageModel>();
    public ICollection<TestMessageModel> ClassTests { get; set; } = new List<TestMessageModel>();
    public RoomMessageModel Room { get; set; }
    #endregion
  }
}