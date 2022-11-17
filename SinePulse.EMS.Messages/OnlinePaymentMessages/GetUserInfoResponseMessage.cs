using FluentValidation.Results;

namespace SinePulse.EMS.Messages.OnlinePaymentMessages
{
  public class GetUserInfoResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public UserInfo UserInfos { get; }
    
    public GetUserInfoResponseMessage(ValidationResult validationResult, UserInfo userInfos)
    {
      ValidationResult = validationResult;
      UserInfos = userInfos;
    }
    
    public class UserInfo
    {
      public string Fullname { get; set; }
      public string EmailAddress { get; set; }
      public string PhoneNo { get; set; }
    }
  }
}