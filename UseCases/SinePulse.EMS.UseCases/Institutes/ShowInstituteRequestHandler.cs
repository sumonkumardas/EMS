using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.InstituteMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Institutes
{
  public class ShowInstituteRequestHandler : IRequestHandler<ShowInstituteRequestMessage, ShowInstituteResponseMessage>
  {
    private readonly ShowInstituteRequestMessageValidator _validator;
    private readonly IShowInstituteUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public ShowInstituteRequestHandler(ShowInstituteRequestMessageValidator validator, IShowInstituteUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ShowInstituteResponseMessage> Handle(ShowInstituteRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowInstituteResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowInstituteResponseMessage(validationResult,null);
        return Task.FromResult(returnMessage);
      }

       var institute = _useCase.ShowInstitute(request);

      returnMessage = new ShowInstituteResponseMessage(validationResult, institute);
      return Task.FromResult(returnMessage);
    }
  }
}