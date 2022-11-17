using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.SectionMessages;

namespace SinePulse.EMS.UseCases.Sections
{
  public interface IShowSectionUseCase
  {
    Section ShowSection(ShowSectionRequestMessage request);
    BreakTime ShowBreakTime(ShowSectionRequestMessage request);
  }
}