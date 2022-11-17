using System;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.StudentMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Students
{
  public class AddStudentAddressUseCase : IAddStudentAddressUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public AddStudentAddressUseCase(EmsDbContext dbContext, IRepository repository)
    {
      _dbContext = dbContext;
      _repository = repository;
    }

    public void AddStudentAddress(AddStudentAddressRequestMessage message)
    {
      var student = _repository.GetById<Student>(message.StudentId);
      var presentAddress = new Address
      {
        Street1 = message.PresentAddressStreet1,
        Street2 = message.PresentAddressStreet2,
        City = message.PresentAddressCity,
        PostalCode = message.PresentAddressPostalCode,
        AuditFields = new AuditFields
        {
          LastUpdatedBy = message.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now,
          InsertedDateTime = DateTime.Now,
          InsertedBy = message.CurrentUserName
        }
      };
      var permanentAddress = new Address
      {
        Street1 = message.PermanentAddressStreet1,
        Street2 = message.PermanentAddressStreet2,
        City = message.PermanentAddressCity,
        PostalCode = message.PermanentAddressPostalCode,
        AuditFields = new AuditFields
        {
          LastUpdatedBy = message.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now,
          InsertedDateTime = DateTime.Now,
          InsertedBy = message.CurrentUserName
        }
      };
      student.PresentAddress = presentAddress;
      student.PermanentAddress = permanentAddress;
      _dbContext.SaveChanges();
    }
  }
}