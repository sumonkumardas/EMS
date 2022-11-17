using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.Model.Students;
using SinePulse.EMS.Messages.StudentContactPersonMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.StudentContactPersons
{
  public class GetStudentContactPersonsUseCase : IGetStudentContactPersonsUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _mapperConfiguration;

    public GetStudentContactPersonsUseCase(IRepository repository)
    {
      _repository = repository;
      _mapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<ContactPerson, ContactPersonMessageModel>());
    }

    public IEnumerable<ContactPersonMessageModel> GetContactPersons(GetStudentContactPersonsRequestMessage message)
    {
      var contactPersons =
        _repository.GetByIdWithInclude<Student>(message.StudentId, new[] {nameof(Student.ContactPersons)})
          .ContactPersons
          .ToList();
      var mapper = _mapperConfiguration.CreateMapper();
      return mapper.Map(contactPersons, new List<ContactPersonMessageModel>());
    }
  }
}