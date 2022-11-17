using MediatR;
using SinePulse.EMS.Messages.SalaryComponentMessages;
using SinePulse.EMS.Persistence.Facade;
using System.Threading;
using System.Threading.Tasks;

namespace SinePulse.EMS.UseCases.SalaryComponents
{
  public class AddSalaryComponentRequestHandler : IRequestHandler<AddSalaryComponentRequestMessage,
    AddSalaryComponentResponseMessage>
  {
    private readonly AddSalaryComponentRequestMessageValidator _validator;
    private readonly IAddSalaryComponentUseCase _addSalaryComponentUseCase;
    private readonly EmsDbContext _dbContext;
    public AddSalaryComponentRequestHandler(AddSalaryComponentRequestMessageValidator validator,
      IAddSalaryComponentUseCase addSalaryComponentUseCase, EmsDbContext dbContext)
    {
      _validator = validator;
      _addSalaryComponentUseCase = addSalaryComponentUseCase;
      _dbContext = dbContext;
    }
    public Task<AddSalaryComponentResponseMessage> Handle(AddSalaryComponentRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddSalaryComponentResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddSalaryComponentResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      var componentType = _addSalaryComponentUseCase.AddSalaryComponent(request);
      _dbContext.SaveChanges();

      returnMessage = new AddSalaryComponentResponseMessage(validationResult, componentType.Id);
      return Task.FromResult(returnMessage);
    }
  }
}
