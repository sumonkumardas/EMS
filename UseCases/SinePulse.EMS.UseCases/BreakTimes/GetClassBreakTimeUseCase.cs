using System.Linq;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.BreakTimeMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.BreakTimes
{
  public class GetClassBreakTimeUseCase : IGetClassBreakTimeUseCase
  {
    private readonly IRepository _repository;

    public GetClassBreakTimeUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public GetClassBreakTimeResponseMessage.BreakTimeProperty GetClassBreakTime(GetClassBreakTimeRequestMessage message)
    {
      var breakTime = _repository.Table<BreakTime>()
        .FirstOrDefault(b => b.BranchMedium.Id == message.BranchMediumId);

      if (breakTime != null)
        return new GetClassBreakTimeResponseMessage.BreakTimeProperty
        {
          Status = breakTime.Status,
          StartTime = breakTime.StartTime,
          EndTime = breakTime.EndTime
        };
      return new GetClassBreakTimeResponseMessage.BreakTimeProperty();
    }
  }
}