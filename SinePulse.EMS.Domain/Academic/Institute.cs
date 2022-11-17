using SinePulse.EMS.Domain.Employees;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.ManagingCommittee;
using SinePulse.EMS.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SinePulse.EMS.Domain.Academic
{
  public class Institute : BaseEntity
  {
    public override string Title()
    {
      return this.OrganisationName + " (" + this.ShortName + ")";
    }

    #region Primitive Properties
    public virtual string OrganisationName { get; set; }
    public virtual string ShortName { get; set; }
    public virtual string Slogan { get; set; }
    public virtual string Domain { get; set; }
    public virtual string Email { get; set; }
    public virtual byte[] Favicon { get; set; }
    public virtual byte[] Logo { get; set; }
    public virtual byte[] Banner { get; set; }
    public virtual string Description { get; set; }
    public virtual string WhyChooseInstitue { get; set; }
    public virtual string FacebookPageURL { get; set; }
    public virtual StatusEnum Status { get; set; }
    #endregion

    #region Complex Properties
    private AuditFields _auditFields = new AuditFields();

    public virtual AuditFields AuditFields
    {
      get { return _auditFields; }
      set { _auditFields = value; }
    }
    #endregion

    #region  Collection Properties
    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();
    public virtual ICollection<CommitteeMember> CommitteeMembers { get; set; } = new List<CommitteeMember>();
    #endregion

    #region Behaviour
    public Branch AddBranch([Required] string name, [Required]string branchCode, string email, StatusEnum status)
    {
      Branch branch = new Branch();
      branch.BranchName = name;
      branch.BranchCode = branchCode;
      branch.Email = email;
      branch.Status = status;
      branch.Institute = this;
      
      return branch;
    }
    #endregion
  }
}
