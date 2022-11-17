using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.EmployeeGradeMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.EmployeeGrade
{
  public class ShowEmployeeGradeRequestHandler : IRequestHandler<ShowEmployeeGradeRequestMessage, ShowEmployeeGradeResponseMessage>
  {
    private readonly ShowEmployeeGradeRequestMessageValidator _validator;
    private readonly IShowEmployeeGradeUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public ShowEmployeeGradeRequestHandler(ShowEmployeeGradeRequestMessageValidator validator, IShowEmployeeGradeUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ShowEmployeeGradeResponseMessage> Handle(ShowEmployeeGradeRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowEmployeeGradeResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowEmployeeGradeResponseMessage(validationResult,null);
        return Task.FromResult(returnMessage);
      }

       var employeeGrade = _useCase.ShowEmployeeGrade(request);

      returnMessage = new ShowEmployeeGradeResponseMessage(validationResult, employeeGrade);
      return Task.FromResult(returnMessage);
    }
  }
}