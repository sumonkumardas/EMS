using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Students;
using SinePulse.EMS.Messages.StudentContactPersonMessages;

namespace SinePulse.EMS.UseCases.StudentContactPersons
{
  public interface IGetStudentContactPersonsUseCase
  {
    IEnumerable<ContactPersonMessageModel> GetContactPersons(GetStudentContactPersonsRequestMessage message);
  }
}