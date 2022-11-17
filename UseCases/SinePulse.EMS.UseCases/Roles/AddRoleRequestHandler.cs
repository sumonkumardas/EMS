using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.RoleMessages;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Roles
{
  public class AddRoleRequestHandler : IRequestHandler<AddRoleRequestMessage, ValidatedData<AddRoleResponseMessage>>
  {
    private readonly AddRoleRequestMessageValidator _validator;
    private readonly IAddRoleUseCase _useCase;
    private readonly EmsDbContext _dbContext;


    public AddRoleRequestHandler(AddRoleRequestMessageValidator validator,
      IAddRoleUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ValidatedData<AddRoleResponseMessage>> Handle(AddRoleRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<AddRoleResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<AddRoleResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.AddRole(request);
      _dbContext.SaveChanges();

      returnMessage = new ValidatedData<AddRoleResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}