using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.StudentMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Students
{
  public class AddOrChangeStudentImageRequestHandler : IRequestHandler<AddOrChangeStudentImageRequestMessage,
    AddOrChangeStudentImageResponseMessage>
  {
    private readonly AddOrChangeStudentImageRequestMessageValidator _validator;
    private readonly IAddOrChangeStudentImageUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public AddOrChangeStudentImageRequestHandler(AddOrChangeStudentImageRequestMessageValidator validator,
      IAddOrChangeStudentImageUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<AddOrChangeStudentImageResponseMessage> Handle(AddOrChangeStudentImageRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddOrChangeStudentImageResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddOrChangeStudentImageResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var previousImage =  _useCase.UploadStudentImage(request);
      _dbContext.SaveChanges();

      returnMessage = new AddOrChangeStudentImageResponseMessage(validationResult, previousImage);
      return Task.FromResult(returnMessage);
    }
  }
}