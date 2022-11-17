namespace SinePulse.EMS.Messages.ResultGradeMessages
{
  public class AddResultGradeResponseMessage
  {
    public long ResultGradeId { get; }

    public AddResultGradeResponseMessage(long resultGradeId)
    {
      ResultGradeId = resultGradeId;
    }
  }
}