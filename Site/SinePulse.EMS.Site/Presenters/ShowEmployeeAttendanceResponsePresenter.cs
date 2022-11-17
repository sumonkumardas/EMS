using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Messages.AttendanceMessages;
using SinePulse.EMS.Messages.Model.Attendance;
using SinePulse.EMS.Messages.Model.Employees;
using SinePulse.EMS.Messages.AuthorizationMessages;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowEmployeeAttendanceResponsePresenter
  {
    private MapperConfiguration _autoMapperConfig;

    public EmployeeAttendanceViewModel Handle(ShowEmployeeAttendanceResponseMessage message, 
      EmployeeAttendanceViewModel viewModel, GetUserAssociationResponseMessage userAssociationMessage)
    {
      _autoMapperConfig = new MapperConfiguration(cfg => {
        cfg.CreateMap<EmployeeMessageModel, EmployeeViewModel>();
        cfg.CreateMap<EmployeeAttendanceMessageModel, EmployeeAttendanceViewModel>(); });
      var mapper = _autoMapperConfig.CreateMapper();
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
      viewModel = mapper.Map<EmployeeAttendanceViewModel>(message.EmployeeAttendance);
      return viewModel;
    }
  }
}