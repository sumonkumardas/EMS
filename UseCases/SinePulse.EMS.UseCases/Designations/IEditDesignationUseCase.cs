using SinePulse.EMS.Messages.DesignationMessages;

namespace SinePulse.EMS.UseCases.Designations
{
  public interface IEditDesignationUseCase
  {
    void EditDesignation(EditDesignationRequestMessage message);
  }
}