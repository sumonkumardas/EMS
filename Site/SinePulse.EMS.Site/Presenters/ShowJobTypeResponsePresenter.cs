using AutoMapper;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.JobTypeMessages;
using SinePulse.EMS.Messages.Model.Employees;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowJobTypeResponsePresenter
  {
    private MapperConfiguration _autoMapperConfig;

    public JobTypeViewModel Handle(ShowJobTypeResponseMessage message, JobTypeViewModel viewModel,
      GetUserAssociationResponseMessage userAssociationMessage)
    {
      _autoMapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<JobTypeMessageModel, JobTypeViewModel>(); });
      var mapper = _autoMapperConfig.CreateMapper();
      viewModel = mapper.Map<JobTypeViewModel>(message.JobType);

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