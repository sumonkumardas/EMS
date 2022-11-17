using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.Model.Students;
using SinePulse.EMS.Messages.WaiverMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Waivers
{
  public class GetStudentWaiversUseCase : IGetStudentWaiversUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _mapperConfiguration;

    public GetStudentWaiversUseCase(IRepository repository)
    {
      _repository = repository;
      _mapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<StudentWaiver, StudentWaiverMessageModel>());
    }

    public IEnumerable<StudentWaiverMessageModel> GetStudentWaivers(GetStudentWaiversRequestMessage message)
    {
      var studentWaivers = _repository.Table<StudentWaiver>()
        .Include(nameof(StudentWaiver.AccountHead))
        .Where(w => w.Section.Id == message.SectionId &&
                    w.Student.Id == message.StudentId &&
                    w.AccountHead.Session.Id == message.SessionId &&
                    (int) w.AccountHead.AccountPeriod == (int) message.FeePeriod)
        .ToList();
      var mapper = _mapperConfiguration.CreateMapper();
      return mapper.Map(studentWaivers, new List<StudentWaiverMessageModel>());
    }
  }
}