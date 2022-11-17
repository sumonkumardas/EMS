using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.DesignationMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Designations
{
  public class ShowDesignationListRequestHandler : IRequestHandler<ShowDesignationListRequestMessage, ShowDesignationListResponseMessage>
  {
    private readonly ShowDesignationListRequestMessageValidator _validator;
    private readonly IShowDesignationListUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public ShowDesignationListRequestHandler(ShowDesignationListRequestMessageValidator validator, IShowDesignationListUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ShowDesignationListResponseMessage> Handle(ShowDesignationListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowDesignationListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowDesignationListResponseMessage(validationResult,null);
        return Task.FromResult(returnMessage);
      }

       var designationList = _useCase.ShowDesignationList(request);

      returnMessage = new ShowDesignationListResponseMessage(validationResult, designationList);
      return Task.FromResult(returnMessage);
    }
  }
}