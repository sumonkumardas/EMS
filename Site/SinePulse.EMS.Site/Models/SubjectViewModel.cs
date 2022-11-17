using System.Collections.Generic;
using AutoMapper;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Site.Models
{
  public class SubjectViewModel : BaseViewModel
  {
    public long Id { get; set; }
    [IgnoreMap]
    public long SubjectId { get; set; }
    public string SubjectName { get; set; }
    public int SubjectCode { get; set; }
    public StatusEnum Status { get; set; }
   
  }
}