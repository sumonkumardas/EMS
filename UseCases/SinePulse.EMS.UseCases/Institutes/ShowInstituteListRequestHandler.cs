using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.InstituteMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Institutes
{
  public class ShowInstituteListRequestHandler : IRequestHandler<ShowInstituteListRequestMessage, ShowInstituteListResponseMessage>
  {
    private readonly ShowInstituteListRequestMessageValidator _validator;
    private readonly IShowInstituteListUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public ShowInstituteListRequestHandler(ShowInstituteListRequestMessageValidator validator, IShowInstituteListUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ShowInstituteListResponseMessage> Handle(ShowInstituteListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowInstituteListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowInstituteListResponseMessage(validationResult,null);
        return Task.FromResult(returnMessage);
      }

       var instituteList = _useCase.ShowInstituteList(request);

      returnMessage = new ShowInstituteListResponseMessage(validationResult, instituteList);
      return Task.FromResult(returnMessage);
    }
  }
}