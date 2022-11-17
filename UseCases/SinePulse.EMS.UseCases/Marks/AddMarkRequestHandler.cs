using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.MarkMessages;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Marks
{
  public class AddMarkRequestHandler : IRequestHandler<AddMarkRequestMessage, ValidatedData<AddMarkResponseMessage>>
  {
    private readonly AddMarkRequestMessageValidator _validator;
    private readonly IAddMarkUseCase _useCase;
    private readonly EmsDbContext _dbContext;


    public AddMarkRequestHandler(AddMarkRequestMessageValidator validator,
      IAddMarkUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ValidatedData<AddMarkResponseMessage>> Handle(AddMarkRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<AddMarkResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<AddMarkResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.AddMark(request);
      _dbContext.SaveChanges();

      returnMessage = new ValidatedData<AddMarkResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}