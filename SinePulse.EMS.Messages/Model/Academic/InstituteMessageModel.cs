using SinePulse.EMS.Messages.Model.Enums;
using SinePulse.EMS.Messages.Model.Shared;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.Model.Academic
{
  public class InstituteMessageModel : BaseEntityMessageModel
  {
    #region Primitive Properties
    public string OrganisationName { get; set; }
    public string ShortName { get; set; }
    public string Slogan { get; set; }
    public string Domain { get; set; }
    public string Email { get; set; }
    public byte[] Favicon { get; set; }
    public byte[] Logo { get; set; }
    public byte[] Banner { get; set; }
    public string Description { get; set; }
    public string WhyChooseInstitue { get; set; }
    public string FacebookPageURL { get; set; }
    public StatusEnum Status { get; set; }
    #endregion

    #region  Navigation Properties

    public ICollection<SessionMessageModel> Sessions { get; set; } = new List<SessionMessageModel>();

    #endregion
  }
}
