using MediatR;
using SinePulse.EMS.Messages.ServiceChargeMessages;
using SinePulse.EMS.Persistence.Facade;
using System.Threading;
using System.Threading.Tasks;

namespace SinePulse.EMS.UseCases.ServiceCharges
{
  public class AddServiceChargeRequestHandler : IRequestHandler<AddServiceChargeRequestMessage,
    AddServiceChargeResponseMessage>
  {
    private readonly AddServiceChargeRequestMessageValidator _validator;
    private readonly IAddServiceChargeUseCase _addServiceChargeUseCase;
    private readonly EmsDbContext _dbContext;
    public AddServiceChargeRequestHandler(AddServiceChargeRequestMessageValidator validator,
      IAddServiceChargeUseCase addServiceChargeUseCase, EmsDbContext dbContext)
    {
      _validator = validator;
      _addServiceChargeUseCase = addServiceChargeUseCase;
      _dbContext = dbContext;
    }
    public Task<AddServiceChargeResponseMessage> Handle(AddServiceChargeRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddServiceChargeResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddServiceChargeResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      var serviceCharge = _addServiceChargeUseCase.AddServiceCharge(request);
      _dbContext.SaveChanges();

      returnMessage = new AddServiceChargeResponseMessage(validationResult, serviceCharge.Id);
      return Task.FromResult(returnMessage);
    }
  }
}
