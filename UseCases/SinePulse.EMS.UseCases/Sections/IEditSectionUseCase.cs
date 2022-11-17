using SinePulse.EMS.Messages.SectionMessages;

namespace SinePulse.EMS.UseCases.Sections
{
  public interface IEditSectionUseCase
  {
    void EditSection(EditSectionRequestMessage request);
  }
}