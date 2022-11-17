using System.Collections.Generic;
using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.StudentPromotionMessages
{
  public class PromoteStudentsRequestMessage : IRequest<ValidatedData<PromoteStudentsResponseMessage>>
  {
    public long CurrentSectionId { get; set; }
    public ICollection<StudentPromotionRequest> StudentPromotionRequests { get; set; }
    public string CurrentUserName { get; set; }

    public class StudentPromotionRequest
    {
      public long StudentId { get; set; }
      public long CurrentStudentSectionId { get; set; }
      public long NextSectionId { get; set; }
    }
  }
}