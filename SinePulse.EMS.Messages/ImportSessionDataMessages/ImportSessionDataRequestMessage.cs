using MediatR;
using SinePulse.EMS.Messages.Shared;

namespace SinePulse.EMS.Messages.ImportSessionDataMessages
{
  public class ImportSessionDataRequestMessage : IRequest<ValidatedData<ImportSessionDataResponseMessage>>
  {
    public bool OnlySectionInfo { get; set; }
    public bool SectionInfoWithClassRoutine { get; set; }
    public bool OnlyExamTerm { get; set; }
    public bool ExamTermWithExamConfiguration { get; set; }
    public long PreviousSessionId { get; set; }
    public string CurrentUserName { get; set; }
  }
}