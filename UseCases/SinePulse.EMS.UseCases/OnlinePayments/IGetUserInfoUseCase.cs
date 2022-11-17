using SinePulse.EMS.Messages.OnlinePaymentMessages;

namespace SinePulse.EMS.UseCases.OnlinePayments
{
  public interface IGetUserInfoUseCase
  {
    GetUserInfoResponseMessage.UserInfo GetUserInfo(GetUserInfoRequestMessage message);
  }
}