using System.Collections.Generic;
using System.Linq;
using SinePulse.EMS.Messages.TermTestMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowTermTestListResponsePresenter
  {
    public List<TermTestViewModel> Handle(ShowTermTestListResponseMessage message, List<TermTestViewModel> viewModel)
    {
      viewModel = message.TermTestList.Select(ToViewTermTest).ToList();
      return viewModel;
    }

    private TermTestViewModel ToViewTermTest(ShowTermTestListResponseMessage.TermTest classTest)
    {
      return new TermTestViewModel
      {
        TermTestId = classTest.Id,
        StartTime = classTest.StartTime,
        EndTime = classTest.EndTime
      };
    }
  }
}