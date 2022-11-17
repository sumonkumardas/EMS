
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.StudentContactPersonMessages;

namespace SinePulse.EMS.UseCases.StudentContactPersons
{
  public interface IAddStudentContactPersonUseCase
  {
    ContactPerson AddContactPerson(AddStudentContactPersonRequestMessage message);
  }
}