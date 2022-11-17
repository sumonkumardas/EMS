namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IUniqueClassPropertyChecker
  {
    bool IsUniqueClassName(string className);
    bool IsUniqueClassCode(int classCode);
    bool IsUniqueClassCode(int classCode, long classId);
    bool IsUniqueClassName(string className, long classClassId);
  }
}