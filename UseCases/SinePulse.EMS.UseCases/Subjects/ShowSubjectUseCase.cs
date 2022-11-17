using AutoMapper;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.SubjectMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Subjects
{
  public class ShowSubjectUseCase : IShowSubjectUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _mapperConfiguration;

    public ShowSubjectUseCase(IRepository repository)
    {
      _repository = repository;
      _mapperConfiguration = new MapperConfiguration(c => c.CreateMap<Subject, SubjectMessageModel>());
    }

    public SubjectMessageModel ShowSubject(ShowSubjectRequestMessage message)
    {
      var subject = _repository.GetById<Subject>(message.SubjectId);
      var mapper = _mapperConfiguration.CreateMapper();
      return mapper.Map(subject, new SubjectMessageModel());
    }
  }
}