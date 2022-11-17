using System.Collections.Generic;
using MediatR;
using SinePulse.EMS.Domain.Academic;

namespace SinePulse.EMS.Messages.ClassMessages
{
  public class ShowClassesListRequestMessage : IRequest<ShowClassesListResponseMessage>
  {
    public IEnumerable<Class> Classes { get; set; }
  }
}