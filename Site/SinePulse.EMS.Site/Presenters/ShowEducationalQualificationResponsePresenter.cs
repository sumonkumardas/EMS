using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.EmployeeEducationalQualificationMessages;
using SinePulse.EMS.Site.Models;

namespace SinePulse.EMS.Site.Presenters
{
  public class ShowEducationalQualificationResponsePresenter
  {
    public EducationalQualificationViewModel Handle(ShowEducationalQualificationResponseMessage message, EducationalQualificationViewModel viewModel)
    {
      viewModel.DegreeName = (EducationDegreeEnum) message.EducationalQualification.DegreeName;
      viewModel.PassingYear = message.EducationalQualification.PassingYear;
      viewModel.InstituteName = message.EducationalQualification.InstituteName;
      viewModel.MajorSubject = message.EducationalQualification.MajorSubject;
      return viewModel;
    }
  }
}