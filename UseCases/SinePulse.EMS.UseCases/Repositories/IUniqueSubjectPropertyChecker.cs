namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IUniqueSubjectPropertyChecker
  {
    bool IsUniqueSubjectName(string subjectName);
    bool IsUniqueSubjectCode(int subjectCode);
    bool IsUniqueSubjectCode(int subjectCode, long subjectId);
    bool IsUniqueSubjectName(string subjectName, long subjectId);
  }
}