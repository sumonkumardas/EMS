using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.EmployeeEducationalQualificationMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.EmployeeEducationalQualifications
{
  public class AddEmployeeEducationalQualificationRequestHandler : IRequestHandler<AddEmployeeEducationalQualificationRequestMessage, AddEmployeeEducationalQualificationResponseMessage>
  {
    private readonly AddEmployeeEducationalQualificationRequestMessageValidator _validator;
    private readonly IAddEmployeeEducationalQualificationUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public AddEmployeeEducationalQualificationRequestHandler(AddEmployeeEducationalQualificationRequestMessageValidator validator, IAddEmployeeEducationalQualificationUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<AddEmployeeEducationalQualificationResponseMessage> Handle(AddEmployeeEducationalQualificationRequestMessage request, CancellationToken cancellationToken)
    {
      AddEmployeeEducationalQualificationResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddEmployeeEducationalQualificationResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.AddEmployeeEducationalQualification(request);
      _dbContext.SaveChanges();

      returnMessage = new AddEmployeeEducationalQualificationResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}