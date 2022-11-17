using SinePulse.EMS.Messages.EmployeeEducationalQualificationMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowEmployeeEducationalQualificationsResponsePresenter
  {
    public EmployeeEducationalQualificationsViewModel Handle(GetEmployeeEducationalQualificationsResponseMessage message, 
      EmployeeEducationalQualificationsViewModel viewModel)
    {
      viewModel.EducationalQualifications = message.EducationalQualifications;
      return viewModel;
    }
  }
}