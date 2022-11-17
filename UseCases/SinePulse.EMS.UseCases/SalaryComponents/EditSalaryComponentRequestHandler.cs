using MediatR;
using SinePulse.EMS.Messages.SalaryComponentMessages;
using SinePulse.EMS.Persistence.Facade;
using System.Threading;
using System.Threading.Tasks;

namespace SinePulse.EMS.UseCases.SalaryComponents
{
  public class EditSalaryComponentRequestHandler : IRequestHandler<EditSalaryComponentRequestMessage,
    EditSalaryComponentResponseMessage>
  {
    private readonly EditSalaryComponentRequestMessageValidator _validator;
    private readonly IEditSalaryComponentUseCase _editSalaryComponentUseCase;
    private readonly EmsDbContext _dbContext;
    public EditSalaryComponentRequestHandler(EditSalaryComponentRequestMessageValidator validator,
      IEditSalaryComponentUseCase editSalaryComponentUseCase, EmsDbContext dbContext)
    {
      _validator = validator;
      _editSalaryComponentUseCase = editSalaryComponentUseCase;
      _dbContext = dbContext;
    }
    public Task<EditSalaryComponentResponseMessage> Handle(EditSalaryComponentRequestMessage request,
      CancellationToken cancellationToken)
    {
      EditSalaryComponentResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new EditSalaryComponentResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _editSalaryComponentUseCase.EditSalaryComponent(request);
      _dbContext.SaveChanges();

      returnMessage = new EditSalaryComponentResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}
