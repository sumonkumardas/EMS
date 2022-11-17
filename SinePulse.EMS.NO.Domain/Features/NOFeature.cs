using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NakedObjects;

namespace SinePulse.EMS.NO.Domain.Features
{
  [Table("Features")]
  [DisplayName("Feature")]
  public class NOFeature
  {
    #region Primitive Properties

    [Key, NakedObjectsIgnore]
    public virtual int FeatureId { get; set; }

    [Title, Required]
    [MemberOrder(20)]
    [StringLength(100)]
    public virtual string FeatureName { get; set; }

    [MemberOrder(10)]
    public virtual int FeatureCode { get; set; }

    #endregion

    #region Navigation Properties

    [MemberOrder(10), Required, Disabled]
    public virtual NOFeatureType FeatureType { get; set; }

    #endregion

    #region Feature Enums
    public enum GeneralSetupEnum
    {
      NewPublicHoliday = 1,
      ViewPublicHoliday = 2,
      EditPublicHoliday = 3,
      FindPublicHoliday = 4
    }
    public enum AcademicSetupEnum
    {
      NewMedium = 1,
      ViewMedium = 2,
      EditMedium = 3,
      FindMedium = 4,
      NewSubject = 5,
      ViewSubject = 6,
      EditSubject = 7,
      FindSubject = 8,
      NewClass = 9,
      ViewClass = 10,
      EditClass = 11,
      FindClass = 12,
      AddSubject = 13
    }
    public enum EmployeeSetupEnum
    {
      NewDesignation = 1,
      ViewDesignation = 2,
      EditDesignation = 3,
      FindDesignation = 4,
      NewJobType = 5,
      ViewJobType = 6,
      EditJobType = 7,
      FindJobType = 8,
      NewGrade = 9,
      ViewGrade = 10,
      EditGrade = 11,
      FindtGrade = 12,
      NewLeaveType = 13,
      ViewLeaveType = 14,
      EditLeaveType = 15,
      FindLeaveType = 16
    }
    public enum EmployeeEnum
    {
      NewEmployee = 1,
      ViewEmployee = 2,
      EditEmployee = 3,
      FindEmployee = 4,
      AddPersonalDetail = 5,
      ViewPersonalDetail = 6,
      EditPersonalDetail = 7,
      AddEducationalInfo = 8,
      ViewEducationalInfo = 9,
      EditEducationalInfo = 10,
      AddAddress = 11,
      ViewAddress = 12,
      EditAddress = 13,
      AddOrChangePhoto = 14,
      ApplyLeave = 15,
      ApproveLeave = 16,
      AddLogin = 17,
      ChangePassword = 18
    }
    public enum StudentEnum
    {
      NewAdmission = 1,
      ViewStudent = 2,
      UpdateStudent = 3,
      FindStudent = 4,
      AddOrChangePhoto = 5,
      AddSection = 6,
      AddContactInfo = 7,
      AddContactPerson = 8
    }
    public enum SessionEnum
    {
      NewSession = 1,
      ViewSession = 2,
      EditSession = 3,
      FindSession = 4,
      SetASCurrentSession = 5
    }
    public enum InstituteEnum
    {
      NewInstitute = 1,
      ViewInstitute = 2,
      EditInstitute = 3,
      FindInstitute = 4
    }
    public enum BranchEnum
    {
      AddBranch = 1,
      ViewBranch = 2,
      EditBranch = 3,
      FindBranch = 4,
      AddShift = 5,
      ViewShift = 6,
      EditShift = 7,
      FindShift = 8
    }
    public enum BranchMediumEnum
    {
      AddMedium = 1,
      ViewMedium = 2,
      EditMedium = 3,
      FindMedium = 4,
      AddClassBteakDown = 5,
      ViewClassBreakDown = 6,
      EditClassBreakDown = 7,
      FindClassBreakDown = 8,
      NewRoom = 9,
      ViewRoom = 10,
      EditRoom = 11,
      FindRoom = 12,
      SetAcademicFees = 13,
      ImportData = 14
    }
    public enum SectionEnum
    {
      NewSection = 1,
      ViewSection = 2,
      EditSection = 3,
      FindSection = 4,
      AssignOrChangeRoom = 5,
      AddClassRoutine = 6,
      EditClassRoutine = 7
    }
    public enum ExaminationEnum
    {
      NewExamTerm = 1,
      ViewExamTerm = 2,
      EditExamTerm = 3,
      FindExamTerm = 4,
      AddExamType = 5,
      ViewxamType = 6,
      EditExamType = 7,
      FindExamType = 8,
      AddExamRoutine = 9,
      EditExamRoutine = 10,
      FindExamRoutine = 11,
      AddClassTest = 12,
      ViewClassTest = 13,
      EditClassTest = 14,
      FindClassTest = 15
    }
    public enum UserManagementEnum
    {
      AddLoginUser = 1,
      ChangePassword = 2,
      AddRole = 3,
      EditRole = 4,
      AssignRoleToUser = 5,
      AssignFeatureToRole = 6,
      RemoveFeatureFromRole = 7,
      ShowAllRoles = 8,
      ShowFeatures = 9
    }
    #endregion
  }
}