using AutoMapper;
using SinePulse.EMS.Messages.ClassRoutineMessages;
using SinePulse.EMS.Messages.Model.Employees;
using SinePulse.EMS.Messages.Model.Routines;
using SinePulse.EMS.Site.Models;
using System.Collections.Generic;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowClassRoutineListResponsePresenter
  {
    private MapperConfiguration _automapperConfig;
    public List<ClassRoutineViewModel> Handle(ShowClassRoutineListResponseMessage message, List<ClassRoutineViewModel> viewModel)
    {
      _automapperConfig = new MapperConfiguration(cfg => {

        cfg.CreateMap<ClassRoutineMessageModel, ClassRoutineViewModel>();
        //.ForMember(x => x.BranchMedium, opt => opt.Ignore());

      });
      var mapper = _automapperConfig.CreateMapper();
      viewModel = new List<ClassRoutineViewModel>();
      viewModel =  mapper.Map<List<ClassRoutineViewModel>>(message.ClassRoutineList);
      return viewModel;
    }
  }
}