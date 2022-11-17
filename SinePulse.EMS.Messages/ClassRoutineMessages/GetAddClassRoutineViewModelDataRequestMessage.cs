using System.Collections.Generic;
using MediatR;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Employees;
using SinePulse.EMS.Messages.Model.Examinations;

namespace SinePulse.EMS.Messages.ClassRoutineMessages
{
  public class GetAddClassRoutineViewModelDataRequestMessage : IRequest<GetAddClassRoutineViewModelDataResponseMessage>
  {
    public long SectionId { get; set; }
    public IEnumerable<RoomMessageModel> Rooms { set; get; }
    public IEnumerable<EmployeeMessageModel> Teachers { set; get; }
    public IEnumerable<SubjectMessageModel> Subjects { set; get; }

  }
}