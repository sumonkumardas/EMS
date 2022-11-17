
using SinePulse.EMS.Messages.DesignationMessages;
using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.UseCases.Designations
{
  public interface IShowDesignationListUseCase
  {
    List<DesignationMessageModel> ShowDesignationList(ShowDesignationListRequestMessage request);
  }
}