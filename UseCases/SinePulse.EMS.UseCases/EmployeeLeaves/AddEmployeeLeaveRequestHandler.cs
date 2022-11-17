using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.EmployeeLeaveMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.EmployeeLeaves
{
  public class AddEmployeeLeaveRequestHandler : IRequestHandler<AddEmployeeLeaveRequestMessage, AddEmployeeLeaveResponseMessage>
  {
    private readonly AddEmployeeLeaveRequestMessageValidator _validator;
    private readonly IAddEmployeeLeaveUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public AddEmployeeLeaveRequestHandler(AddEmployeeLeaveRequestMessageValidator validator, IAddEmployeeLeaveUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<AddEmployeeLeaveResponseMessage> Handle(AddEmployeeLeaveRequestMessage request, CancellationToken cancellationToken)
    {
      AddEmployeeLeaveResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddEmployeeLeaveResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.AddEmployeeLeave(request);
      _dbContext.SaveChanges();

      returnMessage = new AddEmployeeLeaveResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}