using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.TermMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Terms
{
  public class
    AddTermRequestHandler : IRequestHandler<AddTermRequestMessage, AddTermResponseMessage>
  {
    private readonly AddTermRequestMessageValidator _validator;
    private readonly IAddTermUseCase _useCase;
    private readonly EmsDbContext _dbContext;


    public AddTermRequestHandler(AddTermRequestMessageValidator validator,
      IAddTermUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<AddTermResponseMessage> Handle(AddTermRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddTermResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddTermResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      var term = _useCase.AddTerm(request);
      _dbContext.SaveChanges();

      returnMessage = new AddTermResponseMessage(validationResult, term.Id);
      return Task.FromResult(returnMessage);
    }
  }
}