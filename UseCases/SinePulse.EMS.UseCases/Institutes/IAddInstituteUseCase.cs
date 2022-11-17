using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.InstituteMessages;

namespace SinePulse.EMS.UseCases.Institutes
{
  public interface IAddInstituteUseCase
  {
    Institute AddInstitute(AddInstituteRequestMessage request);
  }
}