using SinePulse.EMS.Messages.StudentPromotionMessages;

namespace SinePulse.EMS.UseCases.StudentPromotions
{
  public interface IPromoteStudentOptionUseCase
  {
    PromoteStudentOptionResponseMessage PromoteStudentOption(PromoteStudentOptionRequestMessage requestMessage);
  }
}