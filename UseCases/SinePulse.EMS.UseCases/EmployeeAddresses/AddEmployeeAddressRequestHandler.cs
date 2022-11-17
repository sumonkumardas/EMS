using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.EmployeeAddressMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.EmployeeAddresses
{
  public class AddEmployeeAddressRequestHandler : IRequestHandler<AddEmployeeAddressRequestMessage, AddEmployeeAddressResponseMessage>
  {
    private readonly AddEmployeeAddressRequestMessageValidator _validator;
    private readonly IAddEmployeeAddressUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public AddEmployeeAddressRequestHandler(AddEmployeeAddressRequestMessageValidator validator,
      IAddEmployeeAddressUseCase useCase, EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<AddEmployeeAddressResponseMessage> Handle(AddEmployeeAddressRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddEmployeeAddressResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddEmployeeAddressResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.AddEmployeeAddress(request);
      _dbContext.SaveChanges();

      returnMessage = new AddEmployeeAddressResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}