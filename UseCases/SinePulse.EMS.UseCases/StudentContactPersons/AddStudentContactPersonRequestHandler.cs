using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.EmployeeAddressMessages;
using SinePulse.EMS.Messages.StudentContactPersonMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.UseCases.EmployeeAddresses;

namespace SinePulse.EMS.UseCases.StudentContactPersons
{
  public class AddStudentContactPersonRequestHandler : IRequestHandler<AddStudentContactPersonRequestMessage, AddStudentContactPersonResponseMessage>
  {
    private readonly AddStudentContactPersonRequestMessageValidator _validator;
    private readonly IAddStudentContactPersonUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public AddStudentContactPersonRequestHandler(AddStudentContactPersonRequestMessageValidator validator,
      IAddStudentContactPersonUseCase useCase, EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<AddStudentContactPersonResponseMessage> Handle(AddStudentContactPersonRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddStudentContactPersonResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddStudentContactPersonResponseMessage(validationResult,0);
        return Task.FromResult(returnMessage);
      }

      var contactPerson = _useCase.AddContactPerson(request);
      _dbContext.SaveChanges();

      returnMessage = new AddStudentContactPersonResponseMessage(validationResult, contactPerson.Id);
      return Task.FromResult(returnMessage);
    }
  }
}