using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.StudentMessages;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Students
{
  public class AddStudentRequestHandler
    : IRequestHandler<AddStudentRequestMessage, ValidatedData<AddStudentResponseMessage>>
  {
    private readonly AddStudentRequestMessageValidator _validator;
    private readonly IAddStudentUseCase _useCase;
    private readonly EmsDbContext _dbContext;


    public AddStudentRequestHandler(AddStudentRequestMessageValidator validator,
      IAddStudentUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ValidatedData<AddStudentResponseMessage>> Handle(AddStudentRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<AddStudentResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<AddStudentResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.AddStudent(request);
      _dbContext.SaveChanges();

      returnMessage = new ValidatedData<AddStudentResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}