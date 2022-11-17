using NakedObjects;
using SinePulse.EMS.NO.Domain.Enums;
using SinePulse.EMS.NO.Domain.Repositories;
using SinePulse.EMS.NO.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinePulse.EMS.NO.Domain.Students
{
  [Table("ContactInfos")]
  [DisplayName("Contact Info")]
  public class NOContactInfo : NOBaseEntity
  {
    #region Injected Services
    public IDomainObjectContainer Container { set; protected get; }
    public LoggedInUserInfoRepository LoggedInUserRepository { set; protected get; }
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
    [Required, StringLength(50), MemberOrder(10)]
    [Title]
    public virtual string PhoneNo { get; set; }
    [Optionally, MemberOrder(20)]
    public virtual string EmailAddress { get; set; }
    [MemberOrder(80), Required]
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

    #region  Navigation Properties
    [MemberOrder(150), Optionally]
    public virtual NOAddress PresentAddress { get; set; }
    [MemberOrder(160), Optionally]
    public virtual NOAddress PermanentAddress { get; set; }
    #endregion

    #region Behavior
    public void AddPresentAddress(string street1, [Optionally]string street2, string postalCode, string city)
    {
      NOAddress address = Container.NewTransientInstance<NOAddress>();
      address.Street1 = street1;
      address.Street2 = street2;
      address.PostalCode = postalCode;
      address.City = city;
      Container.Persist(ref address);
      this.PresentAddress = address;
    }
    public void AddPermanentAddress(bool sameAsPresentAddress, [Optionally]string street1, [Optionally]string street2, [Optionally]string postalCode, [Optionally]string city)
    {
      if (sameAsPresentAddress)
      {
        PermanentAddress = PresentAddress;
      }
      else
      {
        NOAddress address = Container.NewTransientInstance<NOAddress>();
        address.Street1 = street1;
        address.Street2 = street2;
        address.PostalCode = postalCode;
        address.City = city;
        Container.Persist(ref address);
        this.PermanentAddress = address;
      }
    }
    #endregion
  }
}
