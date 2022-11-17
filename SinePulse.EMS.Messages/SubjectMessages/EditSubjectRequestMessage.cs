using System.Collections.Generic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Messages.SubjectMessages
{
  public class EditSubjectRequestMessage
  {
    public long SubjectId { get; set; }
    public string SubjectName { get; set; }
    public int SubjectCode { get; set; }
    public StatusEnum Status { get; set; }
    public string CurrentUserName { get; set; }
  }
}