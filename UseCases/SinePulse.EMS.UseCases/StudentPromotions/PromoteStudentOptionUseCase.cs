using SinePulse.EMS.Messages.StudentPromotionMessages;

namespace SinePulse.EMS.UseCases.StudentPromotions
{
  public class PromoteStudentOptionUseCase : IPromoteStudentOptionUseCase
  {
    public PromoteStudentOptionResponseMessage PromoteStudentOption(PromoteStudentOptionRequestMessage requestMessage)
    {
      return new PromoteStudentOptionResponseMessage();
    }
  }
}