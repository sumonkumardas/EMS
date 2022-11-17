using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.JobTypeMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.JobTypes
{
  public class ShowJobTypeListRequestHandler : IRequestHandler<ShowJobTypeListRequestMessage, ShowJobTypeListResponseMessage>
  {
    private readonly ShowJobTypeListRequestMessageValidator _validator;
    private readonly IShowJobTypeListUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public ShowJobTypeListRequestHandler(ShowJobTypeListRequestMessageValidator validator, IShowJobTypeListUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ShowJobTypeListResponseMessage> Handle(ShowJobTypeListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowJobTypeListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowJobTypeListResponseMessage(validationResult,null);
        return Task.FromResult(returnMessage);
      }

       var JobTypeList = _useCase.ShowJobTypeList(request);

      returnMessage = new ShowJobTypeListResponseMessage(validationResult, JobTypeList);
      return Task.FromResult(returnMessage);
    }
  }
}