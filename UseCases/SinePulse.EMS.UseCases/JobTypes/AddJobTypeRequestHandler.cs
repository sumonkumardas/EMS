using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.JobTypeMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.JobTypes
{
  public class AddJobTypeRequestHandler : IRequestHandler<AddJobTypeRequestMessage, AddJobTypeResponseMessage>
  {
    private readonly IAddJobTypeUseCase _useCase;
    private readonly AddJobTypeRequestMessageValidator _validator;
    private readonly EmsDbContext _dbContext;

    public AddJobTypeRequestHandler(EmsDbContext dbContext, IAddJobTypeUseCase useCase,
      AddJobTypeRequestMessageValidator validator)
    {
      _dbContext = dbContext;
      _useCase = useCase;
      _validator = validator;
    }

    public Task<AddJobTypeResponseMessage> Handle(AddJobTypeRequestMessage request, CancellationToken cancellationToken)
    {
      AddJobTypeResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddJobTypeResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      var jobType = _useCase.AddJobType(request);
      _dbContext.SaveChanges();

      returnMessage = new AddJobTypeResponseMessage(validationResult, jobType.Id);
      return Task.FromResult(returnMessage);
    }
  }
}