using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Messages.BranchMediumMessages
{
  public class ShowBranchMediumResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public BranchMedium BranchMedium { get; }
    public ICollection<SessionMessageModel> InstituteSessions { get;}
    public InstituteMessageModel Institute { get; }
    public SessionMessageModel CurrentSession { get; set; }

    public ShowBranchMediumResponseMessage(ValidationResult validationResult, BranchMedium branchMedium, 
      ICollection<SessionMessageModel> sessions, InstituteMessageModel institute, SessionMessageModel currentSession)
    {
      ValidationResult = validationResult;
      BranchMedium = branchMedium;
      InstituteSessions = sessions;
      Institute = institute;
      CurrentSession = currentSession;
    }
  }
}