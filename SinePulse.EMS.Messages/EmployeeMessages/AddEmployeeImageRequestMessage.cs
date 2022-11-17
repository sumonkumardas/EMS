using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.ProjectPrimitives;
using System;

namespace SinePulse.EMS.Messages.EmployeeMessages
{
  public class AddEmployeeImageRequestMessage : MediatR.IRequest<AddEmployeeImageResponseMessage>
  {
    public long EmployeeId { get; set; }
    public string ImageUrl { get; set; }
    public string CurrentUserName { get; set; }
  }
}