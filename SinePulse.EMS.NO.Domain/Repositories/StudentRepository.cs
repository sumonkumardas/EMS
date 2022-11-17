using NakedObjects;
using NakedObjects.Menu;
using NakedObjects.Services;
using SinePulse.EMS.NO.Domain.Enums;
using SinePulse.EMS.NO.Domain.Academic;
using SinePulse.EMS.NO.Domain.Students;
using SinePulse.EMS.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinePulse.EMS.NO.Domain.Repositories
{
  [DisplayName("Students")]
  public class StudentRepository : AbstractFactoryAndRepository
  {
    private const int StudentIdLength = 6;
    public static void Menu(IMenu menu)
    {
      menu.AddAction("NewAdmission");
      menu.AddAction("FindStudent");
      menu.AddAction("ShowStudentList");
    }

    public NOStudent NewAdmission(string fullName, DateTime dOB, RelationTypeEnum guardian)
    {
      NOStudent student = Container.NewTransientInstance<NOStudent>();
      student.FullName = fullName;
      student.StudentId = GetStudentId();
      student.DOB = dOB;
      student.Guardian = guardian;
      student.Status = StatusEnum.Active;
      Container.Persist(ref student);
      return student;

    }
    private string GetStudentId()
    {
      string key = StringGenerator.GenerateUniqueNumberKey(StudentIdLength);

      while (IsStudentIdExist(key))
      {
        key = StringGenerator.GenerateUniqueNumberKey(StudentIdLength);
      }

      return key;
    }

    private bool IsStudentIdExist(string studentId)
    {
      NOStudent user = Container.Instances<NOStudent>().Where(w => w.StudentId == studentId).FirstOrDefault();

      if (user != null)
      {
        return true;
      }

      return false;
    }

    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "FullName", "StudentId","DOB", "Class", "Guardian")]
    public IList<NOStudent> FindStudent(NOInstitute institute, NOBranch branch, BranchMedium medium)
    {
      return Container.Instances<NOStudentSection>().Where(w => w.Section.BranchMedium.Id == medium.Id).Select(s => s.Student).ToList();
    }
    public IList<NOInstitute> Choices0FindStudent()
    {
      return Container.Instances<NOInstitute>().ToList();
    }
    public IList<NOBranch> Choices1FindStudent(NOInstitute institute)
    {
      if (institute != null) return institute.Branches;

      return new List<NOBranch>();
    }
    public IList<BranchMedium> Choices2FindStudent(NOBranch branch)
    {
      if (branch != null) return branch.Mediums;

      return new List<BranchMedium>();
    }

    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "FullName", "StudentId", "DOB", "Class", "Guardian")]
    public IList<NOStudent> ShowStudentList()
    {
      return Container.Instances<NOStudent>().ToList();
    }
  }
}
