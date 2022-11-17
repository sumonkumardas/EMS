namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IGradeMarksOverlappingChecker
  {
    bool IsNonOverlappingGradeMarks(decimal gradePoint, int startMark, int endMark, long branchMediumId);
    bool IsNonOverlappingGradeMarks(int startMark, int endMark, long branchMediumId,long resultGradeId);
  }
}