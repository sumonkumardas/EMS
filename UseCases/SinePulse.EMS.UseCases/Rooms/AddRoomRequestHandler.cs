using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.RoomMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Rooms
{
  public class AddRoomRequestHandler : IRequestHandler<AddRoomRequestMessage, AddRoomResponseMessage>
  {
    private readonly IAddRoomUseCase _useCase;
    private readonly AddRoomRequestMessageValidator _validator;
    private readonly EmsDbContext _dbContext;

    public AddRoomRequestHandler(IAddRoomUseCase useCase, AddRoomRequestMessageValidator validator,
      EmsDbContext dbContext)
    {
      _useCase = useCase;
      _validator = validator;
      _dbContext = dbContext;
    }

    public Task<AddRoomResponseMessage> Handle(AddRoomRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddRoomResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddRoomResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.AddRoom(request);
      _dbContext.SaveChanges();

      returnMessage = new AddRoomResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}