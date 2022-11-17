using System.Threading;
using System.Threading.Tasks;
using SinePulse.EMS.Messages.SubjectMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Subjects
{
  public class EditSubjectRequestHandler
  {
    private readonly EditSubjectRequestMessageValidator _validator;
    private readonly IEditSubjectUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public EditSubjectRequestHandler(EditSubjectRequestMessageValidator validator, IEditSubjectUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<EditSubjectResponseMessage> Handle(EditSubjectRequestMessage request,
      CancellationToken cancellationToken)
    {
      EditSubjectResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new EditSubjectResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      var subject = _useCase.UpdateSubject(request);
      _dbContext.SaveChanges();

      returnMessage = new EditSubjectResponseMessage(validationResult, subject.Id);
      return Task.FromResult(returnMessage);
    }
  }
}