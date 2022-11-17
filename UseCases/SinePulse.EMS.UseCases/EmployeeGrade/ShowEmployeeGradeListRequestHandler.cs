using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.EmployeeGradeMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.EmployeeGrade
{
  public class ShowEmployeeGradeListRequestHandler : IRequestHandler<ShowEmployeeGradeListRequestMessage, ShowEmployeeGradeListResponseMessage>
  {
    private readonly ShowEmployeeGradeListRequestMessageValidator _validator;
    private readonly IShowEmployeeGradeListUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public ShowEmployeeGradeListRequestHandler(ShowEmployeeGradeListRequestMessageValidator validator, IShowEmployeeGradeListUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ShowEmployeeGradeListResponseMessage> Handle(ShowEmployeeGradeListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowEmployeeGradeListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowEmployeeGradeListResponseMessage(validationResult,null);
        return Task.FromResult(returnMessage);
      }

       var employeeGradeList = _useCase.ShowEmployeeGradeList(request);

      returnMessage = new ShowEmployeeGradeListResponseMessage(validationResult, employeeGradeList);
      return Task.FromResult(returnMessage);
    }
  }
}