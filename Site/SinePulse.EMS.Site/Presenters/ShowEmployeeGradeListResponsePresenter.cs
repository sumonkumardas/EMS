using AutoMapper;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.EmployeeGradeMessages;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Employees;
using SinePulse.EMS.Site.Models;
using System.Collections.Generic;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowEmployeeGradeListResponsePresenter
  {
    private MapperConfiguration _automapperConfig;
    public GradeListViewModel Handle(ShowEmployeeGradeListResponseMessage message, GradeListViewModel viewModel,
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      _automapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<GradeMessageModel, GradeViewModel>());
      var mapper = _automapperConfig.CreateMapper();

      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;
      viewModel.Grades =  mapper.Map<List<GradeViewModel>>(message.EmployeeGradeList);
      return viewModel;
    }
  }
}