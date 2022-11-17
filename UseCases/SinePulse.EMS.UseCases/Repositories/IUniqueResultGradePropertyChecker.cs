namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IUniqueResultGradePropertyChecker
  {
    bool IsUniqueResultGradeLetter(string gradeLetter, long branchMediumId);
    bool IsUniqueResultGradePoint(decimal gradePoint, long branchMediumId);
      bool IsUniqueResultGradeLetter(string gradeLetter, long branchMediumId, long resultGradeId);
    bool IsUniqueResultGradePoint(decimal gradePoint, long branchMediumId, long resultGradeId);
  }
}