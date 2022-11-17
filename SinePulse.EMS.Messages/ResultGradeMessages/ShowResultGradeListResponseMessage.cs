using SinePulse.EMS.Messages.Model.Examinations;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.ResultGradeMessages
{
  public class ShowResultGradeListResponseMessage
  {
    public List<ResultGradeMessageModel> ResultGradeList { get; set; }
    public ShowResultGradeListResponseMessage(List<ResultGradeMessageModel> resultGradeList)
    {
      ResultGradeList = resultGradeList;
    }
  }
}