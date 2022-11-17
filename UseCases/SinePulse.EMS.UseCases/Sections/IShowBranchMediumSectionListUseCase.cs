using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.SectionMessages;

namespace SinePulse.EMS.UseCases.Sections
{
  public interface IShowBranchMediumSectionListUseCase
  {
    IEnumerable<SectionMessageModel> ShowBranchMediumSectionList(ShowBranchMediumSectionListRequestMessage message);
  }
}