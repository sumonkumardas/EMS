using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.EmployeeMessages;
using SinePulse.EMS.Messages.StudentContactPersonMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.StudentContactPersons
{
  public class AddStudentContactPersonImageRequestHandler : IRequestHandler<AddStudentContactPersonImageRequestMessage, AddStudentContactPersonImageResponseMessage>
  {
    private readonly AddStudentContactPersonImageRequestMessageValidator _validator;
    private readonly IAddStudentContactPersonImageUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public AddStudentContactPersonImageRequestHandler(AddStudentContactPersonImageRequestMessageValidator validator, IAddStudentContactPersonImageUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<AddStudentContactPersonImageResponseMessage> Handle(AddStudentContactPersonImageRequestMessage request, CancellationToken cancellationToken)
    {
      AddStudentContactPersonImageResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddStudentContactPersonImageResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var previousImage = _useCase.UploadStudentContactPersonImage(request);
      _dbContext.SaveChanges();

      returnMessage = new AddStudentContactPersonImageResponseMessage(validationResult, previousImage);
      return Task.FromResult(returnMessage);
    }
  }
}