using SinePulse.EMS.Messages.ContactInfoMessages;
using SinePulse.EMS.Messages.Model.Students;

namespace SinePulse.EMS.UseCases.ContactInfos
{
  public interface IAddPresentAddressInContactInfoUseCase
  {
    ContactInfoMessageModel AddPresentAddressInContactInfo(AddPresentAddressInContactInfoRequestMessage message);
  }
}