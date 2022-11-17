using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.EmployeeGradeMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.EmployeeGrade
{
  public class AddEmployeeGradeRequestHandler : IRequestHandler<AddEmployeeGradeRequestMessage, AddEmployeeGradeResponseMessage>
  {
    private readonly AddEmployeeGradeRequestMessageValidator _validator;
    private readonly IAddEmployeeGradeUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public AddEmployeeGradeRequestHandler(AddEmployeeGradeRequestMessageValidator validator,
      IAddEmployeeGradeUseCase useCase, EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<AddEmployeeGradeResponseMessage> Handle(AddEmployeeGradeRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddEmployeeGradeResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddEmployeeGradeResponseMessage(validationResult,0);
        return Task.FromResult(returnMessage);
      }

      var grade = _useCase.AddEmployeeGrade(request);
      _dbContext.SaveChanges();

      returnMessage = new AddEmployeeGradeResponseMessage(validationResult,grade.Id);
      return Task.FromResult(returnMessage);
    }
  }
}