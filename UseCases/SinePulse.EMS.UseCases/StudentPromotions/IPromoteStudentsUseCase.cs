using SinePulse.EMS.Messages.StudentPromotionMessages;

namespace SinePulse.EMS.UseCases.StudentPromotions
{
  public interface IPromoteStudentsUseCase
  {
    PromoteStudentsResponseMessage PromoteStudents(PromoteStudentsRequestMessage requestMessage);
  }
}