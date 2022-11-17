using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.EmployeePersonalInfoMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.EmployeePersonalInfo
{
  public class AddEmployeePersonalInfoRequestHandler : IRequestHandler<AddEmployeePersonalInfoRequestMessage, AddEmployeePersonalInfoResponseMessage>
  {
    private readonly AddEmployeePersonalInfoRequestMessageValidator _validator;
    private readonly IAddEmployeePersonalInfoUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public AddEmployeePersonalInfoRequestHandler(AddEmployeePersonalInfoRequestMessageValidator validator, IAddEmployeePersonalInfoUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<AddEmployeePersonalInfoResponseMessage> Handle(AddEmployeePersonalInfoRequestMessage request, CancellationToken cancellationToken)
    {
      AddEmployeePersonalInfoResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddEmployeePersonalInfoResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.AddEmployeePersonalInfo(request);
      _dbContext.SaveChanges();

      returnMessage = new AddEmployeePersonalInfoResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}