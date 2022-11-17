using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.ContactInfoMessages;
using SinePulse.EMS.Messages.Model.Students;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.ContactInfos
{
  public class ShowContactInfoUseCase : IShowContactInfoUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _mapperConfiguration;

    public ShowContactInfoUseCase(IRepository repository)
    {
      _repository = repository;
      _mapperConfiguration = new MapperConfiguration(c => c.CreateMap<ContactInfo, ContactInfoMessageModel>());
    }

    public ContactInfoMessageModel ShowContactInfo(ShowContactInfoRequestMessage message)
    {
      var contactInfo = _repository.Table<ContactInfo>()
        .Include(nameof(ContactInfo.PresentAddress))
        .Include(nameof(ContactInfo.PermanentAddress))
        .FirstOrDefault(c => c.Id == message.ContactInfoId);
      var mapper = _mapperConfiguration.CreateMapper();
      return mapper.Map(contactInfo, new ContactInfoMessageModel());
    }
  }
}