using System.Collections.Generic;
using SinePulse.EMS.Messages.ClassTestMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
    public class ShowClassTestEventResponsePresenter
    {
        public List<ClassTestViewModel> Handle(ShowClassTestListResponseMessage message, List<ClassTestViewModel> viewModel)
        {
            viewModel = new List<ClassTestViewModel>();
            foreach (var classTest in message.ClassTestData)
            {
                var model = new ClassTestViewModel
                {
                    ClassTestId = classTest.TestId,
                    StartTimestamp = classTest.StartTimestamp,
                    EndTimestamp = classTest.EndTimestamp,
                    ClassTestName = classTest.TestName
                };
                viewModel.Add(model);
            }


            return viewModel;
        }
    }
}