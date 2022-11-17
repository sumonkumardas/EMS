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
  public class AddPresentAddressInContactInfoUseCase : IAddPresentAddressInContactInfoUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;
    private readonly MapperConfiguration _mapperConfiguration;

    public AddPresentAddressInContactInfoUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
      _mapperConfiguration = new MapperConfiguration(c => c.CreateMap<ContactInfo, ContactInfoMessageModel>());
    }

    public ContactInfoMessageModel AddPresentAddressInContactInfo(AddPresentAddressInContactInfoRequestMessage message)
    {
      var contactInfo = _repository.GetById<ContactInfo>(message.ContactInfoId);
      var address = new Address
      {
        Street1 = message.Street1,
        Street2 = message.Street2,
        City = message.City,
        PostalCode = message.PostalCode,
        
        AuditFields = new AuditFields
        {
          InsertedDateTime = DateTime.Now,
          InsertedBy = message.CurrentUserName,
          LastUpdatedBy = message.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        }
      };
      contactInfo.PresentAddress = address;
      _dbContext.SaveChanges();
      var mapper = _mapperConfiguration.CreateMapper();
      return mapper.Map(contactInfo, new ContactInfoMessageModel());
    }
  }
}