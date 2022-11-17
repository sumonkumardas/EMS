using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.InstituteMessages;

namespace SinePulse.EMS.UseCases.Institutes
{
  public interface IShowInstituteUseCase
  {
    Institute ShowInstitute(ShowInstituteRequestMessage request);
  }
}