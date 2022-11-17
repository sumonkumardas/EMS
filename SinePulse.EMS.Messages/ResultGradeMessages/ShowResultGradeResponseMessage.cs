using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Examinations;

namespace SinePulse.EMS.Messages.ResultGradeMessages
{
  public class ShowResultGradeResponseMessage
  {
    public ResultGradeMessageModel ResultGradeData { get; }

    public ShowResultGradeResponseMessage(ResultGradeMessageModel resultGrade)
    {
      ResultGradeData = resultGrade;
    }
    
  }
}