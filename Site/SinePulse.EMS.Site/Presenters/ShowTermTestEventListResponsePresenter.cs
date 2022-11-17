using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SinePulse.EMS.Messages.TermTestMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowTermTestEventListResponsePresenter
  {
    private static readonly Random random = new Random();
    public List<EventViewModel> Handle(ShowTermTestListResponseMessage message)
    {
    
    var model = message.TermTestList;
      if (model != null)
      {
        var enevtList = model.Select(ToViewEvent).ToList();
        return enevtList;
      }

      return new List<EventViewModel>();
    }

    private EventViewModel ToViewEvent(ShowTermTestListResponseMessage.TermTest termTest)
    {
      var color =  Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
      return new EventViewModel
      {
        end = termTest.EndTime,
        start = termTest.StartTime,
        title = termTest.Title,
        color = $"#{color.R:X2}{color.G:X2}{color.B:X2}"
      };
    }
  }
}