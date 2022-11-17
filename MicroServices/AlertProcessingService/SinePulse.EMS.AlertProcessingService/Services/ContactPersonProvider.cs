using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.AlertProcessingService.Data;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.AlertProcessingService.Services
{
  public class ContactPersonProvider : IContactPersonProvider
  {
    private readonly IRepository _repository;

    public ContactPersonProvider(IRepository repository)
    {
      _repository = repository;
    }

    public async Task<ICollection<ContactPersonData>> GetContactPersons(long studentId)
    {
      var student = await _repository.GetByIdAsync<Student>(studentId);

      var contactPersons = await _repository.Filter<ContactPerson>(
          x => x.RelationType == student.Guardian &&
               x.Student.Id == studentId)
        .ToListAsync();
      return contactPersons.Select(ToContactPersonData).ToList();
    }

    private ContactPersonData ToContactPersonData(ContactPerson contactPerson)
    {
      return new ContactPersonData
      {
        RelationType = contactPerson.RelationType,
        Name = contactPerson.Name,
        PhoneNo = contactPerson.PhoneNo,
        EmailAddress = contactPerson.EmailAddress
      };
    }
  }
}