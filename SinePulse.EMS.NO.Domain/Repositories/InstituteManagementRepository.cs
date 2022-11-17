using NakedObjects;
using NakedObjects.Menu;
using NakedObjects.Services;
using SinePulse.EMS.NO.Domain.Enums;
using SinePulse.EMS.NO.Domain.Academic;
using SinePulse.EMS.ProjectPrimitives;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinePulse.EMS.NO.Domain.Repositories
{
  [DisplayName("Academic")]
  public class InstituteManagementRepository: AbstractFactoryAndRepository
  {
    public static void Menu(IMenu menu)
    {
      IMenu instituteMenu = menu.CreateSubMenu("Institute");
      instituteMenu.AddAction("AddInstitute");
      instituteMenu.AddAction("AllInstitutes");

      IMenu branchMenu = menu.CreateSubMenu("Branch");
      branchMenu.AddAction("NewBranch");
      branchMenu.AddAction("FindBranch");

      IMenu mediumMenu = menu.CreateSubMenu("Medium");
      mediumMenu.AddAction("NewMedium");
      mediumMenu.AddAction("FindMedium");

      menu.AddAction("ShowAcademicCalendar");
    }

    #region Institute
    public NOInstitute AddInstitute([Required] string name, [Required]string shortName,
      [Optionally, RegEx(Validation = @"^[\-\w\.]+@[\-\w\.]+\.[A-Za-z]+$", Message = "Not a valid email address")]
      string email)
    {
      NOInstitute institute = Container.NewTransientInstance<NOInstitute>();
      institute.OrganisationName = name;
      institute.ShortName = shortName;
      institute.Email = email;
      institute.Status = StatusEnum.Active;

      Container.Persist(ref institute);

      return institute;
    }

    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "OrganisationName", "ShortName", "Status")]
    public IList<NOInstitute> AllInstitutes()
    {
      return Container.Instances<NOInstitute>().ToList();
    }
    #endregion

    #region Branch
    public NOBranch NewBranch(NOInstitute institute, [Required] string branchName, [Required]string branchCode)
    {
      NOBranch branch = Container.NewTransientInstance<NOBranch>();
      branch.BranchName = branchName;
      branch.BranchCode = branchCode;
      branch.Status = StatusEnum.Active;
      branch.Institute = institute;
      Container.Persist(ref branch);
      return branch;
    }
    public IList<NOInstitute> Choices0NewBranch()
    {
      return Container.Instances<NOInstitute>().ToList();
    }
    public IList<NOBranch> FindBranch(NOInstitute institute)
    {
      return institute.Branches;
    }
    public IList<NOInstitute> Choices0FindBranch()
    {
      return Container.Instances<NOInstitute>().ToList();
    }
    #endregion

    #region Medium
    public BranchMedium NewMedium(NOInstitute institute, NOBranch branch, NOShift shift, NOMedium medium, IEnumerable<WeekDays> weeklyHolidays)
    {
      WeekDays holiday = WeekDays.None;
      foreach (WeekDays day in weeklyHolidays)
      {
        if (holiday == WeekDays.None)
        {
          holiday = day;
        }
        else
        {
          holiday |= day;
        }
      }

      BranchMedium branchMedium = Container.NewTransientInstance<BranchMedium>();
      branchMedium.Branch = branch;
      branchMedium.Shift = shift;
      branchMedium.Medium = medium;
      branchMedium.WeeklyHolidays = holiday;
      Container.Persist(ref branchMedium);
      return branchMedium;
    }
    public IList<NOInstitute> Choices0NewMedium()
    {
      return Container.Instances<NOInstitute>().ToList();
    }
    public IList<NOBranch> Choices1NewMedium(NOInstitute institute)
    {
      if (institute != null) return institute.Branches;

      return new List<NOBranch>();
    }
    public IList<NOShift> Choices2NewMedium(NOBranch branch)
    {
      if (branch != null) return branch.Shifts.ToList();

      return new List<NOShift>();
    }
    public IList<BranchMedium> FindMedium(NOInstitute institute, NOBranch branch)
    {
      return branch.Mediums;
    }
    public IList<NOInstitute> Choices0FindMedium()
    {
      return Container.Instances<NOInstitute>().ToList();
    }
    public IList<NOBranch> Choices1FindMedium(NOInstitute institute)
    {
      if (institute != null) return institute.Branches;

      return new List<NOBranch>();
    }
    #endregion

    #region Academic Calendar
    public void ShowAcademicCalendar(NOInstitute institute, NOBranch branch, BranchMedium medium)
    {

    }
    public IList<NOInstitute> Choices0ShowAcademicCalendar()
    {
      return Container.Instances<NOInstitute>().ToList();
    }
    public IList<NOBranch> Choices1ShowAcademicCalendar(NOInstitute institute)
    {
      if (institute != null) return institute.Branches;

      return new List<NOBranch>();
    }
    public IList<BranchMedium> Choices2ShowAcademicCalendar(NOBranch branch)
    {
      if (branch != null) return branch.Mediums;

      return new List<BranchMedium>();
    }
    #endregion
  }
}
