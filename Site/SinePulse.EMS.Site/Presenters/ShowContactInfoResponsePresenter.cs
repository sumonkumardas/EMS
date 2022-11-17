using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.ContactInfoMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowContactInfoResponsePresenter
  {
    public ContactInfoViewModel Handle(ShowContactInfoResponseMessage message, ContactInfoViewModel viewModel,
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.ContactInfoId = message.ContactInfo.Id;
      viewModel.PhoneNo = message.ContactInfo.PhoneNo;
      viewModel.EmailAddress = message.ContactInfo.EmailAddress;
      if (message.ContactInfo.PermanentAddress != null)
        viewModel.PermanentAddress = message.ContactInfo.PermanentAddress;
      if (message.ContactInfo.PresentAddress != null) 
        viewModel.PresentAddress = message.ContactInfo.PresentAddress;
      viewModel.Status = message.ContactInfo.Status;

      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;
      return viewModel;
    } 
  }
}