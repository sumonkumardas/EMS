using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.InstituteMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Institutes
{
  public class AddInstituteRequestHandler : IRequestHandler<AddInstituteRequestMessage, AddInstituteResponseMessage>
  {
    private readonly AddInstituteRequestMessageValidator _validator;
    private readonly IAddInstituteUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public AddInstituteRequestHandler(AddInstituteRequestMessageValidator validator, IAddInstituteUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<AddInstituteResponseMessage> Handle(AddInstituteRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddInstituteResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddInstituteResponseMessage(validationResult,0);
        return Task.FromResult(returnMessage);
      }

       var institute = _useCase.AddInstitute(request);
      _dbContext.SaveChanges();

      returnMessage = new AddInstituteResponseMessage(validationResult,institute.Id);
      return Task.FromResult(returnMessage);
    }
  }
}