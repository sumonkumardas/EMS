using SinePulse.EMS.Messages.ContactInfoMessages;
using SinePulse.EMS.Messages.Model.Students;

namespace SinePulse.EMS.UseCases.ContactInfos
{
  public interface IAddPermanentAddressInContactInfoUseCase
  {
    ContactInfoMessageModel AddPermanentAddressInContactInfo(AddPermanentAddressInContactInfoRequestMessage message);
  }
}