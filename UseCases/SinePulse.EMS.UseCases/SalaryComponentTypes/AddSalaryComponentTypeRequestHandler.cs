using MediatR;
using SinePulse.EMS.Messages.SalaryComponentTypeMessage;
using SinePulse.EMS.Persistence.Facade;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SinePulse.EMS.UseCases.SalaryComponentTypes
{
  public class AddSalaryComponentTypeRequestHandler : IRequestHandler<AddSalaryComponentTypeRequestMessage,
    AddSalaryComponentTypeResponseMessage>
  {
    private readonly AddSalaryComponentTypeRequestMessageValidator _validator;
    private readonly IAddSalaryComponentTypeUseCase _addSalaryComponentTypeUseCase;
    private readonly EmsDbContext _dbContext;
    public AddSalaryComponentTypeRequestHandler(AddSalaryComponentTypeRequestMessageValidator validator,
      IAddSalaryComponentTypeUseCase addSalaryComponentTypeUseCase, EmsDbContext dbContext)
    {
      _validator = validator;
      _addSalaryComponentTypeUseCase = addSalaryComponentTypeUseCase;
      _dbContext = dbContext;
    }
    public Task<AddSalaryComponentTypeResponseMessage> Handle(AddSalaryComponentTypeRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddSalaryComponentTypeResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddSalaryComponentTypeResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      var componentType = _addSalaryComponentTypeUseCase.AddSalaryComponentType(request);
      _dbContext.SaveChanges();

      returnMessage = new AddSalaryComponentTypeResponseMessage(validationResult, componentType.Id);
      return Task.FromResult(returnMessage);
    }
  }
}
