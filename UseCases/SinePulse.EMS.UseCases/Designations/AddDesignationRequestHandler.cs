using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.DesignationMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Designations
{
  public class AddDesignationRequestHandler : IRequestHandler<AddDesignationRequestMessage, AddDesignationResponseMessage>
  {
    private readonly IAddDesignationUseCase _useCase;
    private readonly AddDesignationRequestMessageValidator _validator;
    private readonly EmsDbContext _dbContext;

    public AddDesignationRequestHandler(IAddDesignationUseCase useCase, AddDesignationRequestMessageValidator validator,
      EmsDbContext dbContext)
    {
      _useCase = useCase;
      _validator = validator;
      _dbContext = dbContext;
    }

    public Task<AddDesignationResponseMessage> Handle(AddDesignationRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddDesignationResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddDesignationResponseMessage(validationResult, 0);
        return Task.FromResult(returnMessage);
      }

      var designationMessageModel = _useCase.AddDesignation(request);
      _dbContext.SaveChanges();

      returnMessage = new AddDesignationResponseMessage(validationResult, designationMessageModel.Id);
      return Task.FromResult(returnMessage);
    }
  }
}