using System.Collections.Generic;
using AutoMapper;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.SessionMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Sessions
{
  public class GetBranchMediumSessionsUseCase : IGetBranchMediumSessionsUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _mapperConfiguration;

    public GetBranchMediumSessionsUseCase(IRepository repository)
    {
      _repository = repository;
      _mapperConfiguration = new MapperConfiguration(c => c.CreateMap<Session, SessionMessageModel>());
    }

    public IEnumerable<SessionMessageModel> GetBranchMediumSessions(GetBranchMediumSessionsRequestMessage message)
    {
      var sessions = _repository.GetById<BranchMedium>(message.BranchMediumId, new[] {nameof(BranchMedium.Sessions)}).Sessions;
      var mapper = _mapperConfiguration.CreateMapper();
      return mapper.Map(sessions, new List<SessionMessageModel>());
    }
  }
}