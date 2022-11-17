using SinePulse.EMS.Messages.DesignationMessages;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.UseCases.Designations
{
  public interface IAddDesignationUseCase
  {
    DesignationMessageModel AddDesignation(AddDesignationRequestMessage requestMessage);
  }
}