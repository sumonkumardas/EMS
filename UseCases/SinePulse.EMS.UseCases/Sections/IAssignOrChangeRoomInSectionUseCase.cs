using SinePulse.EMS.Messages.SectionMessages;

namespace SinePulse.EMS.UseCases.Sections
{
  public interface IAssignOrChangeRoomInSectionUseCase
  {
    void AssignOrChangeRoomInSection(AssignOrChangeRoomInSectionRequestMessage message);
  }
}