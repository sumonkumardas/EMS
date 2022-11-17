using SinePulse.EMS.Messages.StudentPromotionMessages;

namespace SinePulse.EMS.UseCases.StudentPromotions
{
  public interface IDisplayPromoteStudentOptionPageUseCase
  {
    DisplayPromoteStudentOptionPageResponseMessage DisplayPromoteStudentOptionPage(
      DisplayPromoteStudentOptionPageRequestMessage requestMessage);
  }
}