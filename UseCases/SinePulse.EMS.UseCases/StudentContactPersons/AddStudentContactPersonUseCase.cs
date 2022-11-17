using System;
using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.EmployeeAddressMessages;
using SinePulse.EMS.Messages.StudentContactPersonMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.StudentContactPersons
{
  public class AddStudentContactPersonUseCase : IAddStudentContactPersonUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public AddStudentContactPersonUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
    }

    public ContactPerson AddContactPerson(AddStudentContactPersonRequestMessage message)
    {
      var student = _repository.GetById<Student>(message.StudentId);
      var contactPerson = new ContactPerson
      {
        Designation = message.Designation,
        EmailAddress = message.EmailAddress,
        ImageUrl = message.ImageUrl,
        Name = message.Name,
        NID = message.NID,
        OfficeContactNo = message.OfficeContactNo,
        OfficeNameAddress = message.OfficeNameAddress,
        PhoneNo = message.PhoneNo,
        Profession = message.Profession,
        RelationType = (Domain.Enums.RelationTypeEnum)message.RelationType,
        Status = StatusEnum.Active,
        Student = student,
        AuditFields = new AuditFields
        {
          InsertedBy = message.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = message.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        }
      };
      _repository.Insert(contactPerson);
      _dbContext.SaveChanges();

      return contactPerson;


    }
  }
}