using SinePulse.EMS.Messages.DesignationMessages;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.UseCases.Designations
{
  public interface IShowDesignationUseCase
  {
    DesignationMessageModel ShowDesignation(ShowDesignationRequestMessage message);
  }
}