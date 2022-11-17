using SinePulse.EMS.Messages.YearClosingMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.UseCases.Sessions;

namespace SinePulse.EMS.UseCases.YearClosings
{
  public class YearClosingUseCase : IYearClosingUseCase
  {
    private readonly ICurrentSessionProvider _currentSessionProvider;
    private readonly IRepository _repository;
    private readonly EmsDbContext _dbContext;

    public YearClosingUseCase(ICurrentSessionProvider currentSessionProvider, IRepository repository,
      EmsDbContext dbContext)
    {
      _currentSessionProvider = currentSessionProvider;
      _repository = repository;
      _dbContext = dbContext;
    }

    public YearClosingResponseMessage YearClosing(YearClosingRequestMessage request)
    {
      var currentSession = _currentSessionProvider.GetCurrentSession(request.BranchMediumId);
      currentSession.IsSessionClosing = true;
      _dbContext.SaveChanges();
      return new YearClosingResponseMessage();
    }
  }
}