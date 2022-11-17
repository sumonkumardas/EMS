using NakedObjects;
using NakedObjects.Value;
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
  [Table("ContactPersons")]
  [DisplayName("Contact Person")]
  public class NOContactPerson : NOBaseEntity
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

    public string Title()
    {
      var t = Container.NewTitleBuilder();

      string title = this.Name + " (" + this.RelationType.ToString() + ")";

      t.Append(title);

      return t.ToString();
    }

    #region Primitive Properties
    [Required, MemberOrder(10)]
    public virtual RelationTypeEnum RelationType { get; set; }
    [Required, StringLength(250), MemberOrder(20)]
    [RegEx(Validation = @"^[^<>%$]*$", Message = "Invalid Name")]
    public virtual string Name { get; set; }
    [Required, StringLength(50), MemberOrder(30)]
    public virtual string PhoneNo { get; set; }
    [Optionally, MemberOrder(40)]
    public virtual string EmailAddress { get; set; }
    [Required, MemberOrder(50)]
    public virtual string NID { get; set; }
    [Optionally, MemberOrder(60)]
    public virtual string Profession { get; set; }
    [Optionally, MemberOrder(70)]
    public virtual string Designation { get; set; }
    [Optionally, MemberOrder(80)]
    public virtual string OfficeNameAddress { get; set; }
    [Optionally, MemberOrder(90)]
    public virtual string OfficeContactNo { get; set; }
    [MemberOrder(100), Required]
    public virtual StatusEnum Status { get; set; }
    [NotMapped, MemberOrder(25)]
    public virtual Image Photo
    {
      get
      {
        if (PhotoContent != null)
        {
          return new Image(PhotoContent, PhotoName, PhotoMime);
        }
        else
        {
          return null;
        }
      }
    }

    [NakedObjectsIgnore, Optionally]
    public virtual byte[] PhotoContent { get; set; }

    [NakedObjectsIgnore, Optionally]
    public virtual string PhotoName { get; set; }

    [NakedObjectsIgnore, Optionally]
    public virtual string PhotoMime { get; set; }
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
    [Required, MemberOrder(500)]
    public virtual NOStudent Student { get; set; }
    [MemberOrder(510), Optionally]
    public virtual NOAddress PresentAddress { get; set; }
    [MemberOrder(520), Optionally]
    public virtual NOAddress PermanentAddress { get; set; }
    #endregion

    #region Behavior
    public void AddOrChangePhoto(Image newImage)
    {
      PhotoContent = newImage.GetResourceAsByteArray();
      PhotoName = newImage.Name;
      PhotoMime = newImage.MimeType;
    }
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
