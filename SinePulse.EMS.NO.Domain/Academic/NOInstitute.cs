using NakedObjects;
using SinePulse.EMS.NO.Domain.Enums;
using SinePulse.EMS.NO.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NakedObjects.Menu;

namespace SinePulse.EMS.NO.Domain.Academic
{
  [Table("Institutes")]
  [DisplayName("Institute")]
  public class NOInstitute : NOBaseEntity
  {
    #region Injected Services
    public IDomainObjectContainer Container { set; protected get; }
    #endregion

    #region Life Cycle Methods
    public virtual void Persisting()
    {
      AuditFields.InsertedBy = Container.Principal.Identity.Name;
      AuditFields.InsertedDateTime = DateTime.Now;
      AuditFields.LastUpdatedBy = Container.Principal.Identity.Name;
      AuditFields.LastUpdatedDateTime = DateTime.Now;
    }
    public virtual void Updating()
    {
      AuditFields.LastUpdatedBy = Container.Principal.Identity.Name;
      AuditFields.LastUpdatedDateTime = DateTime.Now;
    }
    #endregion

    #region Primitive Properties
    [Required, StringLength(250), MemberOrder(10), Title]
    [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Name")]
    public virtual string OrganisationName { get; set; }
    [Required, StringLength(50), MemberOrder(20)]
    [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Name")]
    public virtual string ShortName { get; set; }
    [Optionally, StringLength(250), MemberOrder(30)]
    [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Name")]
    public virtual string Slogan { get; set; }
    [Optionally, MemberOrder(540)]
    public virtual string Domain { get; set; }
    [MemberOrder(550), Optionally]
    [StringLength(250)]
    [RegEx(Validation = @"^[\-\w\.]+@[\-\w\.]+\.[A-Za-z]+$", Message = "Not a valid email address")]
    public virtual string Email { get; set; }
    [Optionally, MemberOrder(560)]
    public virtual byte[] Favicon { get; set; }
    [Optionally, MemberOrder(570)]
    public virtual byte[] Logo { get; set; }
    [Optionally, MemberOrder(580)]
    public virtual byte[] Banner { get; set; }
    [MemberOrder(590), Optionally]
    [StringLength(500)]
    public virtual string Description { get; set; }
    [MemberOrder(600), Optionally]
    [StringLength(500)]
    public virtual string WhyChooseInstitue { get; set; }
    [Optionally, MemberOrder(610)]
    public virtual string FacebookPageURL { get; set; }
    [Required, MemberOrder(40)]
    public virtual StatusEnum Status { get; set; }
    #endregion

    #region Complex Properties
    private AuditFields _auditFields = new AuditFields();
    [Required]
    public virtual AuditFields AuditFields
    {
      get { return _auditFields; }
      set { _auditFields = value; }
    }
    public bool HideAuditFields()
    {
      return true;
    }
    #endregion

    #region Sessions (collection)
    private ICollection<NOSession> _sessions = new List<NOSession>();
    [MemberOrder(50)]
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "SessionName", "StartDate", "EndDate", "IsSessionClosed")]
    public virtual ICollection<NOSession> Sessions
    {
      get
      {
        return _sessions;
      }
      set
      {
        _sessions = value;
      }
    }
    #endregion

    #region Get Properties
    [NotMapped, MemberOrder(60)]
    [Eagerly(EagerlyAttribute.Do.Rendering)]
    [TableView(true, "BranchCode", "WeeklyHoliday", "Status")]
    public virtual IList<NOBranch> Branches
    {
      get { return Container.Instances<NOBranch>().Where(w => w.Institute.Id == this.Id).ToList(); }
    }
    #endregion

    #region Behaviour
    public void AddSession(string sessionName, DateTime startDate, DateTime endDate, [Optionally] IEnumerable<SassionwiseDataEnum> dataNeedToCopy)
    {
      NOSession session = Container.NewTransientInstance<NOSession>();
      session.SessionName = sessionName;
      session.StartDate = startDate;
      session.EndDate = endDate;
      session.IsSessionClosed = false;
      session.SessionType = ObjectTypeEnum.Institute;
      session.Status = StatusEnum.Active;
      Container.Persist(ref session);
      this.Sessions.Add(session);
    }
    public NOBranch AddBranch([Required] string name, [Required]string branchCode,
      [Optionally, RegEx(Validation = @"^[\-\w\.]+@[\-\w\.]+\.[A-Za-z]+$", Message = "Not a valid email address")]
    string email)
    {
      NOBranch branch = Container.NewTransientInstance<NOBranch>();
      branch.BranchName = name;
      branch.BranchCode = branchCode;
      branch.Email = email;
      branch.Status = StatusEnum.Active;
      branch.Institute = this;
      Container.Persist(ref branch);
      return branch;
    }
    #endregion

    public static void Menu(IMenu menu)
    {
      menu.AddAction("AddSession");
      menu.AddAction("AddBranch");
    }
  }
}
