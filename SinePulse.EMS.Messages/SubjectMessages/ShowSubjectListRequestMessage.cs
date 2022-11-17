using System.Collections.Generic;
using MediatR;
using SinePulse.EMS.Domain.Academic;

namespace SinePulse.EMS.Messages.SubjectMessages
{
  public class ShowSubjectListRequestMessage : IRequest<ShowSubjectListResponseMessage>
  {
    public IEnumerable<Subject> Subjects { get; set; }
  }
}