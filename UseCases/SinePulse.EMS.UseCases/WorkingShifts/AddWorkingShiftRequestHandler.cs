using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.WorkingShiftMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.WorkingShifts
{
  public class AddWorkingShiftRequestHandler : IRequestHandler<AddWorkingShiftRequestMessage, AddWorkingShiftResponseMessage>
  {
    private readonly AddWorkingShiftRequestMessageValidator _validator;
    private readonly IAddWorkingShiftUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public AddWorkingShiftRequestHandler(AddWorkingShiftRequestMessageValidator validator, IAddWorkingShiftUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<AddWorkingShiftResponseMessage> Handle(AddWorkingShiftRequestMessage request, CancellationToken cancellationToken)
    {
      AddWorkingShiftResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddWorkingShiftResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.AddWorkingShift(request);
      _dbContext.SaveChanges();

      returnMessage = new AddWorkingShiftResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}