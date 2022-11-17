using System;
using AutoMapper;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.SubjectMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Subjects
{
  public class EditSubjectUseCase : IEditSubjectUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _mapperConfiguration;

    public EditSubjectUseCase(IRepository repository)
    {
      _repository = repository;
      _mapperConfiguration = new MapperConfiguration(c => c.CreateMap<Subject, SubjectMessageModel>());
    }

    public SubjectMessageModel UpdateSubject(EditSubjectRequestMessage message)
    {
      var subject = _repository.GetById<Subject>(message.SubjectId);
      
      subject.SubjectName = message.SubjectName;
      subject.SubjectCode = message.SubjectCode;
      subject.Status = message.Status;
      subject.AuditFields.LastUpdatedBy = message.CurrentUserName;
      subject.AuditFields.LastUpdatedDateTime = DateTime.Now;

      var mapper = _mapperConfiguration.CreateMapper();
      return mapper.Map(subject, new SubjectMessageModel());
    }
  }
}