using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.InstituteMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Institutes
{
  public class EditInstituteRequestHandler : IRequestHandler<EditInstituteRequestMessage, EditInstituteResponseMessage>
  {
    private readonly EditInstituteRequestMessageValidator _validator;
    private readonly IEditInstituteUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public EditInstituteRequestHandler(EditInstituteRequestMessageValidator validator, IEditInstituteUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<EditInstituteResponseMessage> Handle(EditInstituteRequestMessage request,
      CancellationToken cancellationToken)
    {
      EditInstituteResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new EditInstituteResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.EditInstitute(request);
      _dbContext.SaveChanges();

      returnMessage = new EditInstituteResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}