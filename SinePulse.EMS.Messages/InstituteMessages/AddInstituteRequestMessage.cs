using MediatR;
using SinePulse.EMS.Domain.Enums;

namespace SinePulse.EMS.Messages.InstituteMessages
{
  public class AddInstituteRequestMessage : IRequest<AddInstituteResponseMessage>
  {
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
    public string CurrentUserName { get; set; }
  }
}