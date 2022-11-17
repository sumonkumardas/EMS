using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.EmployeeGradeMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.EmployeeGrade
{
  public class EditEmployeeGradeRequestHandler : IRequestHandler<EditEmployeeGradeRequestMessage, EditEmployeeGradeResponseMessage>
  {
    private readonly EditEmployeeGradeRequestMessageValidator _validator;
    private readonly IEditEmployeeGradeUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public EditEmployeeGradeRequestHandler(EditEmployeeGradeRequestMessageValidator validator,
      IEditEmployeeGradeUseCase useCase, EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<EditEmployeeGradeResponseMessage> Handle(EditEmployeeGradeRequestMessage request,
      CancellationToken cancellationToken)
    {
      EditEmployeeGradeResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new EditEmployeeGradeResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.EditEmployeeGrade(request);
      _dbContext.SaveChanges();

      returnMessage = new EditEmployeeGradeResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}