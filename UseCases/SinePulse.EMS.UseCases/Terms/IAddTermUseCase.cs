using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.TermMessages;

namespace SinePulse.EMS.UseCases.Terms
{
  public interface IAddTermUseCase
  {
    ExamTerm AddTerm(AddTermRequestMessage requestMessage);
  }
}