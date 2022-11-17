using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ResultGradeMessages;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.ResultGrades
{
  public class AddResultGradeRequestHandler
    : IRequestHandler<AddResultGradeRequestMessage, ValidatedData<AddResultGradeResponseMessage>>
  {
    private readonly AddResultGradeRequestMessageValidator _validator;
    private readonly IAddResultGradeUseCase _useCase;
    private readonly EmsDbContext _dbContext;


    public AddResultGradeRequestHandler(AddResultGradeRequestMessageValidator validator,
      IAddResultGradeUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ValidatedData<AddResultGradeResponseMessage>> Handle(AddResultGradeRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<AddResultGradeResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<AddResultGradeResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.AddResultGrade(request);
      _dbContext.SaveChanges();

      returnMessage = new ValidatedData<AddResultGradeResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}