using System.Collections.Generic;
using AutoMapper;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.LeaveTypeMessages;
using SinePulse.EMS.Messages.Model.Examinations;
using SinePulse.EMS.Messages.Model.Leaves;
using SinePulse.EMS.Messages.RoomMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowLeaveTypeListResponsePresenter
  {
    private MapperConfiguration _autoMapperConfig;

    public LeaveTypeListViewModel Handle(ShowLeaveTypeListResponseMessage message, LeaveTypeListViewModel viewModel,
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;
      _autoMapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<LeaveTypeMessageModel, LeaveTypeViewModel>(); });
      var mapper = _autoMapperConfig.CreateMapper();
      viewModel.LeaveTypes = mapper.Map<List<LeaveTypeViewModel>>(message.LeaveTypeList);
      return viewModel;
    }
  }
}