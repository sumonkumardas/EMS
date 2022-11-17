using System.Collections.Generic;
using AutoMapper;
using SinePulse.EMS.Messages.EmployeeLeaveMessages;
using SinePulse.EMS.Messages.LeaveTypeMessages;
using SinePulse.EMS.Messages.Model.Examinations;
using SinePulse.EMS.Messages.Model.Leaves;
using System.Linq;
using SinePulse.EMS.Messages.Model.Employees;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Messages.AuthorizationMessages;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowEmployeeLeaveListResponsePresenter
  {
    private MapperConfiguration _autoMapperConfig;

    public EmployeeLeaveListViewModel Handle(ShowEmployeeLeaveListResponseMessage message, EmployeeLeaveListViewModel viewModel,
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      _autoMapperConfig = new MapperConfiguration(cfg =>
      {
          cfg.CreateMap<LeaveTypeMessageModel, LeaveTypeViewModel>();
          cfg.CreateMap<EmployeeMessageModel, EmployeeViewModel>()
              .ForMember(x => x.AssociatedWith, opt => opt.Ignore())
              .ForMember(x => x.LoginUsersBranchId, opt => opt.Ignore())
              .ForMember(x => x.LoginUsersBranchMediumId, opt => opt.Ignore())
              .ForMember(x => x.LoginUsersInstituteId, opt => opt.Ignore())
              .ForMember(x => x.LoginImageUrl, opt => opt.Ignore())
              .ForMember(x => x.RoleFeatures, opt => opt.Ignore())
              .ForMember(x => x.LoginName, opt => opt.Ignore());
          cfg.CreateMap<EmployeeLeaveMessageModel, EmployeeLeaveViewModel>();
      });
      var mapper = _autoMapperConfig.CreateMapper();
      viewModel.EmployeeLeaves = mapper.Map<List<EmployeeLeaveViewModel>>(message.UnapprovedLeaveList);

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