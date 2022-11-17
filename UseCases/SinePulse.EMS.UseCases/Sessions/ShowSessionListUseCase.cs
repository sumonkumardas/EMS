using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.SessionMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Sessions
{
  public class ShowSessionListUseCase : IShowSessionListUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _autoMapperConfig;

    public ShowSessionListUseCase(IRepository repository)
    {
      _repository = repository;
      _autoMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Session, SessionMessageModel>());
    }

    public IEnumerable<SessionMessageModel> ShowSessionList(ShowSessionListRequestMessage requestMessage)
    {
      var mapper = _autoMapperConfig.CreateMapper();
      var sessions = _repository.Table<Session>().Where(w=>w.BranchMedium.Id == requestMessage.BranchMediumId).OrderByDescending(o => o.StartDate).ToList();
      return mapper.Map(sessions, new List<SessionMessageModel>());
    }
  }
}