namespace SinePulse.EMS.UseCases.Repositories
{
  public interface ISaveSalarySheetValidationChecker
  {
    bool IsSalarySheetAlreadySaved(int year, int month, long branchMediumId);
  }
}