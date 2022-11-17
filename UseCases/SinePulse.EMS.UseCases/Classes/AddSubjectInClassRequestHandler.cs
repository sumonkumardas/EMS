using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ClassMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Classes
{
  public class
    AddSubjectInClassRequestHandler : IRequestHandler<AddSubjectInClassRequestMessage, AddSubjectInClassResponseMessage>
  {
    private readonly AddSubjectInClassRequestMessageValidator _validator;
    private readonly IAddSubjectInClassUseCase _useCase;
    private readonly EmsDbContext _dbContext;


    public AddSubjectInClassRequestHandler(AddSubjectInClassRequestMessageValidator validator,
      IAddSubjectInClassUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<AddSubjectInClassResponseMessage> Handle(AddSubjectInClassRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddSubjectInClassResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddSubjectInClassResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.AddSubjectInClass(request);
      _dbContext.SaveChanges();

      returnMessage = new AddSubjectInClassResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}