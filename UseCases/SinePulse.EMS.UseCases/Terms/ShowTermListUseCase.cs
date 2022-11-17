using System;
using System.Linq;
using System.Collections.Generic;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.SessionMessages;
using SinePulse.EMS.Messages.TermMessages;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.UseCases.Sessions;

namespace SinePulse.EMS.UseCases.Terms
{
  public class ShowTermListUseCase : IShowTermListUseCase
  {
    private readonly IRepository _repository;
    private readonly ICurrentSessionProvider _currentSessionProvider;

    public ShowTermListUseCase(IRepository repository, ICurrentSessionProvider currentSessionProvider)
    {
      _repository = repository;
      _currentSessionProvider = currentSessionProvider;
    }

    public ShowTermListResponseMessage ShowTermList(ShowTermListRequestMessage message)
    {
      Session currentSession = _currentSessionProvider.GetCurrentSession(message.BranchMediumId);
      var includes = new[]
      {
        nameof(ExamTerm.Session),
        nameof(ExamTerm.Session) + "." + nameof(BranchMedium)
      };
      var termEntityList = _repository.Table<ExamTerm>(includes).Where(x => x.Session.BranchMedium.Id == message.BranchMediumId && x.EndDate.Year == message.Year).ToList();
      if (currentSession != null && message.SessionId == 0)
      {
        termEntityList = termEntityList.Where(x => x.Session.Id == currentSession.Id).ToList();
      }
      if(message.SessionId > 0)
      {
        termEntityList = termEntityList.Where(x => x.Session.Id == message.SessionId).ToList();
      }

      return new ShowTermListResponseMessage(TermEntityList(termEntityList));
    }

    private List<ShowTermListResponseMessage.Term> TermEntityList(List<ExamTerm> termEntityList)
    {
      List<ShowTermListResponseMessage.Term> model = new List<ShowTermListResponseMessage.Term>();

      foreach (var examTerm in termEntityList)
      {
        var term = new ShowTermListResponseMessage.Term()
        {
          Id = examTerm.Id,
          TermName = examTerm.TermName,
          StartDate = examTerm.StartDate,
          EndDate = examTerm.EndDate,
          SessionName = examTerm.Session.SessionName,
          SessionId = examTerm.Session.Id
        };
        model.Add(term);
      }

      return model;
    }
  }
}