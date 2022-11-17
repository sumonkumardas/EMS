using System;
using AutoMapper;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.ContactInfoMessages;
using SinePulse.EMS.Messages.Model.Students;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.ContactInfos
{
  public class AddContactInfoUseCase : IAddContactInfoUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;
    private readonly MapperConfiguration _mapperConfiguration;

    public AddContactInfoUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
      _mapperConfiguration = new MapperConfiguration(c => c.CreateMap<ContactInfo, ContactInfoMessageModel>());
    }

    public ContactInfoMessageModel AddContactInfo(AddContactInfoRequestMessage message)
    {
      var student = _repository.GetById<Student>(message.StudentId);
      var contactInfo = new ContactInfo
      {
        PhoneNo = message.PhoneNo,
        EmailAddress = message.EmailAddress,
        Status = message.Status,
        AuditFields = new AuditFields
        {
          InsertedBy = message.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = message.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        }
      };

      _repository.Insert(contactInfo);
      //student.ContactInfo = contactInfo;
      student.AuditFields.LastUpdatedBy = message.CurrentUserName;
      student.AuditFields.LastUpdatedDateTime = DateTime.Now;
      _dbContext.SaveChanges();
      var mapper = _mapperConfiguration.CreateMapper();

      return mapper.Map(contactInfo, new ContactInfoMessageModel());
    }
  }
}