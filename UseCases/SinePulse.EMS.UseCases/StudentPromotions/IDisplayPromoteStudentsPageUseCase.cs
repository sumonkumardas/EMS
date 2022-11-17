using SinePulse.EMS.Messages.StudentPromotionMessages;

namespace SinePulse.EMS.UseCases.StudentPromotions
{
  public interface IDisplayPromoteStudentsPageUseCase
  {
    DisplayPromoteStudentsPageResponseMessage DisplayPromoteStudentsPage(
      DisplayPromoteStudentsPageRequestMessage requestMessage);
  }
}