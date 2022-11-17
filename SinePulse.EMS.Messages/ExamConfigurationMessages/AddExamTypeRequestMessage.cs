using MediatR;

namespace SinePulse.EMS.Messages.ExamConfigurationMessages
{
  public class AddExamConfigurationRequestMessage : IRequest<AddExamConfigurationResponseMessage>
  {
    public int SubjectiveFullMark { get; set; }
    public int SubjectivePassMark { get; set; }
    public int ObjectiveFullMark { get; set; }
    public int ObjectivePassMark { get; set; }
    public int PracticalFullMark { get; set; }
    public int PracticalPassMark { get; set; }
    public int ClassTestPercentage { get; set; }
    public long TermId { get; set; }
    public long ClassId { get; set; }
    public long GroupId { get; set; }
    public long SubjectId { get; set; }
    public string CurrentUserName { get; set; }

    public enum ExamType
    {
      SubjectiveFullMarks=1,
      SubjectivePassMarks=2,
      ObjectiveFullMark=3,
      ObjectivePassMark=4,
      PracticalFullMark=5,
      PracticalPassMark=6
    }
  }
}