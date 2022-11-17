using System.Collections.Generic;
using MediatR;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Messages.BranchMediumMessages
{
  public class EditBranchMediumRequestMessage : IRequest<EditBranchMediumResponseMessage>
  {
    public long BranchMediumId { get; set; }
    public IEnumerable<WeekDays> WeeklyHolidaysList { get; set; }
    public long BranchId { get; set; }
    public long MediumId { get; set; }
    public long ShiftId { get; set; }
    public string CurrentUserName { get; set; }
    public int SessionBufferPeriod { get; set; }
  }
}