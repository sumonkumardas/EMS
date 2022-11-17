namespace SinePulse.EMS.UseCases.Repositories
{
  public interface IUniqueDesignationPropertyChecker
  {
    bool IsUniqueDesignationName(string designationName);
    bool IsUniqueDesignationName(string designationName, long designationId);
  }
}