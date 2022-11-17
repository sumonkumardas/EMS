using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.SessionMessages;
using SinePulse.EMS.Messages.TermMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Terms
{
  public class ShowTermUseCase : IShowTermUseCase
  {
    private readonly IRepository _repository;

    public ShowTermUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public ShowTermResponseMessage ShowTerm(ShowTermRequestMessage message)
    {
      var includes = new[]
      {
        nameof(ExamTerm.ExamConfigurations),
        nameof(ExamTerm.ExamConfigurations)+"."+nameof(ExamConfiguration.Class),
        nameof(ExamTerm.ExamConfigurations)+"."+nameof(ExamConfiguration.Subject),
        nameof(ExamTerm.Session),
        nameof(ExamTerm.Session) +"."+ nameof(BranchMedium),
        $"{nameof(ExamTerm.Session) +"."+ nameof(BranchMedium)}.{nameof(BranchMedium.Branch)}",
        $"{nameof(ExamTerm.Session) +"."+ nameof(BranchMedium)}.{nameof(BranchMedium.Medium)}",
        $"{nameof(ExamTerm.Session) +"."+ nameof(BranchMedium)}.{nameof(BranchMedium.Branch)}.{nameof(BranchMedium.Branch.Institute)}"
      };
      var termEntity = _repository.GetByIdWithInclude<ExamTerm>(message.TermId, includes);
      return new ShowTermResponseMessage(TermEntity(termEntity));
    }

    private ShowTermResponseMessage.Term TermEntity(ExamTerm termEntity)
    {
      return new ShowTermResponseMessage.Term
      {
        Id = termEntity.Id,
        TermName = termEntity.TermName,
        StartDate = termEntity.StartDate,
        EndDate = termEntity.EndDate,
        Status = ConvertToMessageStatus(termEntity.Status),
        Session = ToSessionResult(termEntity.Session),
        BranchMedium = new ShowTermResponseMessage.BranchMedium
        {
          Id = termEntity.Session.BranchMedium.Id,
          InstituteName = termEntity.Session.BranchMedium.Branch.Institute.OrganisationName,
          BranchId = termEntity.Session.BranchMedium.Branch.Id,
          BranchName = termEntity.Session.BranchMedium.Branch.BranchName,
          MediumName = termEntity.Session.BranchMedium.Medium.MediumName
        },
        ExamTypes = termEntity.ExamConfigurations
      };
    }

    private static Messages.Model.Enums.StatusEnum ConvertToMessageStatus(StatusEnum entityStatus)
    {
      switch (entityStatus)
      {
        case StatusEnum.Active:
          return Messages.Model.Enums.StatusEnum.Active;
        case StatusEnum.Inactive:
          return Messages.Model.Enums.StatusEnum.Inactive;
        case StatusEnum.Pending:
          return Messages.Model.Enums.StatusEnum.Pending;
        case StatusEnum.Transferred:
          return Messages.Model.Enums.StatusEnum.Transferred;
        case StatusEnum.Passed:
          return Messages.Model.Enums.StatusEnum.Passed;
        default:
          throw new ArgumentOutOfRangeException(nameof(entityStatus), entityStatus, null);
      }
    }

    private SingleLineSessionResult ToSessionResult(Session session)
    {
      switch (session.SessionType)
      {
        case ObjectTypeEnum.Global:
          return new SingleLineSessionResult
          {
            SessionId = session.Id,
            SessionName = session.SessionName,
            SessionType = "Global",
            SessionAssociatedObjectName = ""
          };
        case ObjectTypeEnum.Institute:
          return new SingleLineSessionResult
          {
            SessionId = session.Id,
            SessionName = session.SessionName,
            SessionType = "Institute",
            SessionAssociatedObjectName = GetInstituteFromSession(session.Id).OrganisationName
          };
        case ObjectTypeEnum.Branch:
          return new SingleLineSessionResult
          {
            SessionId = session.Id,
            SessionName = session.SessionName,
            SessionType = "Branch",
            SessionAssociatedObjectName = GetBranchFromSession(session.Id).BranchName
          };
        case ObjectTypeEnum.Medium:
        case ObjectTypeEnum.BranchMedium:
          return new SingleLineSessionResult
          {
            SessionId = session.Id,
            SessionName = session.SessionName,
            SessionType = "Medium",
            SessionAssociatedObjectName = GetMediumFromSession(session.Id).MediumName
          };
        default:
          throw new ArgumentOutOfRangeException();
      }
    }


    private Institute GetInstituteFromSession(long sessionId)
    {
      var includes = new[]
      {
        nameof(Session.Institute)
      };
      return _repository.GetByIdWithInclude<Session>(sessionId, includes).Institute;
    }

    private Branch GetBranchFromSession(long sessionId)
    {
      var includes = new[]
      {
        nameof(Session.Branch)
      };
      return _repository.GetByIdWithInclude<Session>(sessionId, includes).Branch;
    }

    private Medium GetMediumFromSession(long sessionId)
    {
      var includes = new[]
      {
        nameof(Session.BranchMedium),
        $"{nameof(Session.BranchMedium)}.{nameof(BranchMedium.Medium)}"
      };
      return _repository.GetByIdWithInclude<Session>(sessionId, includes).BranchMedium.Medium;
    }
  }
}