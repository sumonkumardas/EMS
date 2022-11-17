using AutoMapper;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.JobTypeMessages;
using SinePulse.EMS.Messages.Model.Employees;
using SinePulse.EMS.Site.Models;
using System.Collections.Generic;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowJobTypeListResponsePresenter
  {
    private MapperConfiguration _automapperConfig;
    public JobTypeListViewModel Handle(ShowJobTypeListResponseMessage message, JobTypeListViewModel viewModel,
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      _automapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<JobTypeMessageModel, JobTypeViewModel>());
      var mapper = _automapperConfig.CreateMapper();

      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;
      viewModel.JobTypes =  mapper.Map<List<JobTypeViewModel>>(message.JobTypeList);
      return viewModel;
    }
  }
}