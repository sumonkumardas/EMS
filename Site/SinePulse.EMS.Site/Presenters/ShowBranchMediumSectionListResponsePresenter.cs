using AutoMapper;
using SinePulse.EMS.Messages.BranchMediumMessages;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Site.Models;
using System.Collections.Generic;
using SinePulse.EMS.Messages.AuthorizationMessages;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowBranchMediumSectionListResponsePresenter
  {
    public ShowBranchMediumSectionListViewModel Handle(List<SectionMessageModel> sectionList, ShowBranchMediumSectionListViewModel viewModel, GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.Sections = new List<SectionMessageModel>();
      viewModel.Sections = sectionList;

      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;
      viewModel.PendingAmount = userAssociationMessage.PendingAmount;
      viewModel.IsBillDueDateOver = userAssociationMessage.IsBillDueDateOver;
      return viewModel;
    }
  }
}