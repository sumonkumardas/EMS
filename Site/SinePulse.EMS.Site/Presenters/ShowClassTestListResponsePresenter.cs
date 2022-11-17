using System.Collections.Generic;
using SinePulse.EMS.Messages.ClassTestMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
    public class ShowClassTestListResponsePresenter
    {
        public List<EventViewModel> Handle(ShowClassTestListResponseMessage message, List<EventViewModel> viewModel)
        {
            viewModel = new List<EventViewModel>();
            foreach (var classTest in message.ClassTestData)
            {
                var model = new EventViewModel
                {
                    start = classTest.StartTimestamp,
                    end = classTest.EndTimestamp,
                    title = classTest.TestName,
                    url= "/ClassTest/UpdateClassTest?classTestId="+classTest.TestId
                };
                viewModel.Add(model);
            }


            return viewModel;
        }
    }
}