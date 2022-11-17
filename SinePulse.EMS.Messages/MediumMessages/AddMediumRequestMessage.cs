using MediatR;

namespace SinePulse.EMS.Messages.MediumMessages
{
  public class AddMediumRequestMessage : IRequest<AddMediumResponseMessage>
  {
    public string MediumName { get; set; }
    public string MediumCode { get; set; }
    public string CurrentUserName { get; set; }
  }
}