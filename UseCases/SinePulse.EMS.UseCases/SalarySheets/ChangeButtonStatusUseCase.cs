using System.Linq;
using SinePulse.EMS.Domain.Payroll;
using SinePulse.EMS.Messages.SalarySheetMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.SalarySheets
{
  public class ChangeButtonStatusUseCase : IChangeButtonStatusUseCase
  {
    private readonly IRepository _repository;

    public ChangeButtonStatusUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public ChangeButtonStatusResponseMessage.ButtonStatus ChangeButtonStatus(ChangeButtonStatusRequestMessage message)
    {
      var buttonStatus = new ChangeButtonStatusResponseMessage.ButtonStatus();
      var salarySheet = _repository.Table<SalarySheet>()
        .FirstOrDefault(s => s.Month == message.Month &&
                             s.Year == message.Year &&
                             s.BranchMedium.Id == message.BranchMediumId);
      if (salarySheet != null)
      {
        buttonStatus.ShowAccPostPrintStateAndExportButton = true;
        if (salarySheet.IsAccountPosted)
        {
          buttonStatus.DisableSaveButton = true;
          buttonStatus.DisablePostAccount = true;
        }
      }
      return buttonStatus;
    }
  }
}