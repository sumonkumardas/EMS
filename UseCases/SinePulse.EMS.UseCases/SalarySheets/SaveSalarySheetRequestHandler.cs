using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.GenerateSalarySheetMessages;
using SinePulse.EMS.Messages.SalarySheetMessages;

namespace SinePulse.EMS.UseCases.SalarySheets
{
  public class SaveSalarySheetRequestHandler : IRequestHandler<SaveSalarySheetRequestMessage, SaveSalarySheetResponseMessage>
  {
    private readonly SaveSalarySheetRequestMessageValidator _validator;
    private readonly ISaveSalarySheetUseCase _useCase;


    public SaveSalarySheetRequestHandler(SaveSalarySheetRequestMessageValidator validator,
      ISaveSalarySheetUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<SaveSalarySheetResponseMessage> Handle(SaveSalarySheetRequestMessage request,
      CancellationToken cancellationToken)
    {
      SaveSalarySheetResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new SaveSalarySheetResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

     _useCase.SaveSalarySheet(request);

      returnMessage = new SaveSalarySheetResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}