using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.SectionMessages;

namespace SinePulse.EMS.UseCases.Sections
{
  public interface IAddSectionUseCase
  {
    Section AddSection(AddSectionRequestMessage request);
  }
}