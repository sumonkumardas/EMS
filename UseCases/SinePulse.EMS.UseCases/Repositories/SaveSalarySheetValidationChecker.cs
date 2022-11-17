using System.Linq;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Repositories
{
  public class SaveSalarySheetValidationChecker : ISaveSalarySheetValidationChecker
  {
    private readonly EmsDbContext _dbContext;

    public SaveSalarySheetValidationChecker(EmsDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public bool IsSalarySheetAlreadySaved(int year, int month, long branchMediumId)
    {
      return !_dbContext.SalarySheets.Any(s => s.Month == month &&
                                               s.Year == year &&
                                               s.BranchMedium.Id == branchMediumId);
    }
  }
}