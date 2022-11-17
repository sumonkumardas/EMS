using System.Collections.Generic;
using SinePulse.EMS.Messages.FeatureMessages;

namespace SinePulse.EMS.Site.Models
{
  public class AddFeatureInRoleViewModel : BaseViewModel
  {
    public string RoleId { get; set; }
    public long FeatureTypeId { get; set; }
    public long[] FeatureIds { get; set; }

    public List<GetFeaturesToAddInRoleResponseMessage.Feature> Features { get; set; } =
      new List<GetFeaturesToAddInRoleResponseMessage.Feature>();

    public List<GetFeatureTypeListResponseMessage.FeatureType> FeatureTypes { get; set; } =
      new List<GetFeatureTypeListResponseMessage.FeatureType>();
  }
}