using SinePulse.EMS.Messages.InstituteMessages;

namespace SinePulse.EMS.UseCases.Institutes
{
  public interface IEditInstituteUseCase
  {
    void EditInstitute(EditInstituteRequestMessage request);
  }
}