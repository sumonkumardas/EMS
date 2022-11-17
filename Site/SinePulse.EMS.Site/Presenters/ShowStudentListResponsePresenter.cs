using System.Collections.Generic;
using AutoMapper;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.Model.Students;
using SinePulse.EMS.Messages.StudentMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowStudentListResponsePresenter
  {
    private MapperConfiguration _autoMapperConfig;

    public ShowStudentListViewModel Handle(ShowStudentListResponseMessage message, ShowStudentListViewModel viewModel, GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.Students = new List<StudentViewModel>();
      _autoMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<StudentMessageModel, StudentViewModel>());
      var mapper = _autoMapperConfig.CreateMapper();
      viewModel.Students = mapper.Map<List<StudentViewModel>>(message.Students);
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