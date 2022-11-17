using System.Collections.Generic;
using SinePulse.EMS.Messages.EmployeeEducationalQualificationMessages;
using SinePulse.EMS.Messages.Model.Employees;

namespace SinePulse.EMS.UseCases.EmployeeEducationalQualifications
{
  public interface IGetEmployeeEducationalQualificationsUseCase
  {
    IEnumerable<EducationalQualificationMessageModel> GetEducationalQualifications(
      GetEmployeeEducationalQualificationsRequestMessage message);
  }
}