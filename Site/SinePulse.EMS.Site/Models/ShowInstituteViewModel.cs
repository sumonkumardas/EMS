using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.AuthorizationMessages;

namespace SinePulse.EMS.Site.Models
{
  public class ShowInstituteViewModel : BaseViewModel
  {
    public ShowInstituteViewModel()
    {
      Institute = new Institute();
    }

    public Institute Institute { get; set; }
    public TabEnum ActiveTab { get; set; }
  }
}