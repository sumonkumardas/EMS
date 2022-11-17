using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.StudentPromotionMessages;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.UseCases.Sessions;

namespace SinePulse.EMS.UseCases.StudentPromotions
{
  public class DisplayPromoteStudentOptionPageUseCase : IDisplayPromoteStudentOptionPageUseCase
  {
    private readonly IRepository _repository;
    private readonly ICurrentSessionProvider _currentSessionProvider;

    public DisplayPromoteStudentOptionPageUseCase(IRepository repository,
      ICurrentSessionProvider currentSessionProvider)
    {
      _repository = repository;
      _currentSessionProvider = currentSessionProvider;
    }

    public DisplayPromoteStudentOptionPageResponseMessage DisplayPromoteStudentOptionPage(
      DisplayPromoteStudentOptionPageRequestMessage requestMessage)
    {
      var section = _repository.GetById<Section>(requestMessage.SectionId);
      var currentSession = _currentSessionProvider.GetCurrentSession(section.BranchMediumId);
      var sessions = _repository.Filter<Session>(x => x.StartDate > currentSession.StartDate).AsEnumerable()
        .Select(ToResponseSession).ToList();
      var nextSessionClasses = _repository.Filter<Class>(x => true).AsEnumerable()
        .Select(ToResponseClass).ToList();

      return new DisplayPromoteStudentOptionPageResponseMessage
      {
        Sessions = sessions,
        Classes = nextSessionClasses
      };
    }

    private DisplayPromoteStudentOptionPageResponseMessage.Class ToResponseClass(Class @class)
    {
      return new DisplayPromoteStudentOptionPageResponseMessage.Class
      {
        ClassId = @class.Id,
        ClassName = @class.ClassName
      };
    }

    private DisplayPromoteStudentOptionPageResponseMessage.Session ToResponseSession(Session section)
    {
      return new DisplayPromoteStudentOptionPageResponseMessage.Session
      {
        SessionId = section.Id,
        SessionName = section.SessionName
      };
    }
  }
}