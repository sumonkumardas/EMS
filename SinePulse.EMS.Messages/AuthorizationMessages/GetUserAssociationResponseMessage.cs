using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Domain.Shared;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.AuthorizationMessages
{
  public class GetUserAssociationResponseMessage
  {
    public string LoginName { get; }
    public string ImageUrl { get; }
    public AssociationType AssociatedWith { get; }
    public long? LoginUsersInstituteId { get; }
    public long? LoginUsersBranchId { get; }
    public long? LoginUsersBranchMediumId { get; }
    public IList<RoleFeature> RoleFeatures { get; }
    public byte[] InstituteBanner { get; }
    public decimal PendingAmount { get; }
    public bool IsBillDueDateOver { get; }

    public GetUserAssociationResponseMessage(string loginName, string imageUrl, 
      AssociationType associatedWith, long? instituteId, byte[] instituteBanner, long? branchId, 
      long? branchMediumId, IList<RoleFeature> roleFeatures, decimal pendingAmount, bool isBillDueDateOver)
    {
      LoginName = loginName;
      ImageUrl = imageUrl;
      AssociatedWith = associatedWith;
      LoginUsersInstituteId = instituteId;
      LoginUsersBranchId = branchId;
      LoginUsersBranchMediumId = branchMediumId;
      RoleFeatures = roleFeatures;
      InstituteBanner = instituteBanner;
      PendingAmount = pendingAmount;
      IsBillDueDateOver = isBillDueDateOver;
    }
  }
}