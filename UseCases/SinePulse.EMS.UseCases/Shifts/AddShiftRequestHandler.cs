using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ShiftMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Shifts
{
  public class AddShiftRequestHandler : IRequestHandler<AddShiftRequestMessage, AddShiftResponseMessage>
  {
    private readonly AddShiftRequestMessageValidator _validator;
    private readonly IAddShiftUseCase _addShiftUseCase;
    private readonly EmsDbContext _dbContext;

    public AddShiftRequestHandler(AddShiftRequestMessageValidator validator, IAddShiftUseCase addShiftUseCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _addShiftUseCase = addShiftUseCase;
      _dbContext = dbContext;
    }

    public Task<AddShiftResponseMessage> Handle(AddShiftRequestMessage request, CancellationToken cancellationToken)
    {
      AddShiftResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddShiftResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      var shift = _addShiftUseCase.AddShift(request);
      _dbContext.SaveChanges();

      returnMessage = new AddShiftResponseMessage(validationResult, shift.Id);
      return Task.FromResult(returnMessage);
    }
  }
}