using AutoMapper;
using SinePulse.EMS.Messages.BranchMediumMessages;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Site.Models;
using System.Collections.Generic;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowBranchMediumListResponsePresenter
  {
    private MapperConfiguration _automapperConfig;
    public List<BranchMediumViewModel> Handle(ShowBranchMediumListResponseMessage message, List<BranchMediumViewModel> viewModel)
    {
      _automapperConfig = new MapperConfiguration(
        cfg => {
          cfg.CreateMap<BranchMessageModel, BranchViewModel>();
          cfg.CreateMap<MediumMessageModel, MediumViewModel>();
          cfg.CreateMap<BranchMediumMessageModel, BranchMediumViewModel>();
        }
        
        );
      var mapper = _automapperConfig.CreateMapper();
      viewModel = new List<BranchMediumViewModel>();
      viewModel =  mapper.Map<List<BranchMediumViewModel>>(message.BranchMediumList);
      return viewModel;
    }
  }
}