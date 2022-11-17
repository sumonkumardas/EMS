using SinePulse.EMS.Messages.StudentContactPersonMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowStudentContactPersonsResponsePresenter
  {
    public ContactPersonViewModel Handle(GetStudentContactPersonsResponseMessage message,
      ContactPersonViewModel viewModel)
    {
      viewModel.ContactPersons = message.ContactPersons;
      return viewModel;
    }
  }
}