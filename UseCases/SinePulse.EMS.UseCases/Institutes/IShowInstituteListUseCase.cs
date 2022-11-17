using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.InstituteMessages;
using System.Collections.Generic;

namespace SinePulse.EMS.UseCases.Institutes
{
  public interface IShowInstituteListUseCase
  {
    List<Institute> ShowInstituteList(ShowInstituteListRequestMessage request);
  }
}