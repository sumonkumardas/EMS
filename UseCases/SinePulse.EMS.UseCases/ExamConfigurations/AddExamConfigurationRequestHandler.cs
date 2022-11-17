using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ExamConfigurationMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.ExamConfigurations
{
  public class AddExamConfigurationRequestHandler : IRequestHandler<AddExamConfigurationRequestMessage, AddExamConfigurationResponseMessage>
  {
    private readonly AddExamConfigurationRequestMessageValidator _validator;
    private readonly IAddExamConfigurationUseCase _useCase;
    private readonly EmsDbContext _dbContext;


    public AddExamConfigurationRequestHandler(AddExamConfigurationRequestMessageValidator validator,
      IAddExamConfigurationUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<AddExamConfigurationResponseMessage> Handle(AddExamConfigurationRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddExamConfigurationResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddExamConfigurationResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      var examConfiguration = _useCase.AddExamConfiguration(request);
      _dbContext.SaveChanges();

      returnMessage = new AddExamConfigurationResponseMessage(validationResult, examConfiguration.Id);
      return Task.FromResult(returnMessage);
    }
  }
}