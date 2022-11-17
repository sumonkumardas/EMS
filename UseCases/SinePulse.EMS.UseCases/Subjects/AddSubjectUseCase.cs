using System;
using AutoMapper;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.SubjectMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Subjects
{
  public class AddSubjectUseCase : IAddSubjectUseCase
  {
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;
    private readonly MapperConfiguration _autoMapperConfig;

    public AddSubjectUseCase(IRepository repository, EmsDbContext dbContext)
    {
      _repository = repository;
      _dbContext = dbContext;
      _autoMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Subject, SubjectMessageModel>());
    }

    public SubjectMessageModel AddSubject(AddSubjectRequestMessage requestMessage)
    {
      var subject = new Subject
      {
        SubjectName = requestMessage.SubjectName,
        SubjectCode = requestMessage.SubjectCode,
        Status = StatusEnum.Active,

        AuditFields = new AuditFields
        {
          LastUpdatedBy = requestMessage.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now,
          InsertedBy = requestMessage.CurrentUserName,
          InsertedDateTime = DateTime.Now
        }
      };

      _repository.Insert(subject);
      _dbContext.SaveChanges();
      var mapper = _autoMapperConfig.CreateMapper();
      return mapper.Map(subject, new SubjectMessageModel());
    }

    private Subject GetCombinedSubject(long combinedSubjectId)
    {
      return combinedSubjectId == -1 ? null : _repository.GetById<Subject>(combinedSubjectId);
    }
  }
}