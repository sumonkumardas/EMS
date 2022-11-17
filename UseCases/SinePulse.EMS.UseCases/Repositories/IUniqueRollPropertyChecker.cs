namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IUniqueRollPropertyChecker
  {
    bool IsUniqueRollNo(int rollNo,long sectionId,long classId);
    bool IsUniqueRollNo(int rollNo, long sectionId, long classId, long studentId);
  }
}