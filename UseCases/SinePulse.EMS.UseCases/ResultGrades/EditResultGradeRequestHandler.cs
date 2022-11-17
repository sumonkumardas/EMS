using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.ResultGradeMessages;
using SinePulse.EMS.Messages.Shared;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.ResultGrades
{
  public class EditResultGradeRequestHandler
    : IRequestHandler<EditResultGradeRequestMessage, ValidatedData<EditResultGradeResponseMessage>>
  {
    private readonly EditResultGradeRequestMessageValidator _validator;
    private readonly IEditResultGradeUseCase _useCase;
    private readonly EmsDbContext _dbContext;


    public EditResultGradeRequestHandler(EditResultGradeRequestMessageValidator validator,
      IEditResultGradeUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ValidatedData<EditResultGradeResponseMessage>> Handle(EditResultGradeRequestMessage request,
      CancellationToken cancellationToken)
    {
      ValidatedData<EditResultGradeResponseMessage> returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ValidatedData<EditResultGradeResponseMessage>(validationResult);
        return Task.FromResult(returnMessage);
      }

      var response = _useCase.EditResultGrade(request);
      _dbContext.SaveChanges();

      returnMessage = new ValidatedData<EditResultGradeResponseMessage>(validationResult, response);
      return Task.FromResult(returnMessage);
    }
  }
}