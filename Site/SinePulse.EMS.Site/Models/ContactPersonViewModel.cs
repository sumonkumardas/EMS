using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Students;

namespace SinePulse.EMS.Site.Models
{
  public class ContactPersonViewModel : BaseViewModel
  {
    public IEnumerable<ContactPersonMessageModel> ContactPersons { get; set; }
  }
}