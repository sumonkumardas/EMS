using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ExamConfigurationMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.ExamConfigurations
{
  public class EditExamConfigurationRequestHandler : IRequestHandler<EditExamConfigurationRequestMessage, EditExamConfigurationResponseMessage>
  {
    private readonly EditExamConfigurationRequestMessageValidator _validator;
    private readonly IEditExamConfigurationUseCase _useCase;
    private readonly EmsDbContext _dbContext;


    public EditExamConfigurationRequestHandler(EditExamConfigurationRequestMessageValidator validator,
      IEditExamConfigurationUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<EditExamConfigurationResponseMessage> Handle(EditExamConfigurationRequestMessage request,
      CancellationToken cancellationToken)
    {
      EditExamConfigurationResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new EditExamConfigurationResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.EditExamConfiguration(request);
      _dbContext.SaveChanges();

      returnMessage = new EditExamConfigurationResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}