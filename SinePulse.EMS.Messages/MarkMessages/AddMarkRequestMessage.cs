using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.MarkMessages
{
  public class AddMarkRequestMessage : IRequest<ValidatedData<AddMarkResponseMessage>>
  {
    public decimal ObtainedMark { get; set; }
    public decimal GraceMark { get; set; }
    public string Comment { get; set; }
    public long TestId { get; set; }
    public long StudentSectionId { get; set; }
    public string CurrentUserName { get; set; }
  }
}