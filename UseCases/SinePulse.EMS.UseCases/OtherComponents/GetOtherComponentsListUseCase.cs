using System.Collections.Generic;
using System.Linq;
using SinePulse.EMS.Domain.Payroll;
using SinePulse.EMS.Messages.OtherComponentMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.OtherComponents
{
  public class GetOtherComponentsListUseCase : IGetOtherComponentsListUseCase
  {
    private readonly IRepository _repository;

    public GetOtherComponentsListUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public List<GetOtherComponentsListResponseMessage.OtherComponent> GetOtherComponentsList(GetOtherComponentsListRequestMessage message)
    {
      var otherComponents = _repository.Table<OtherComponent>().ToList();
      var requestComponents = new List<GetOtherComponentsListResponseMessage.OtherComponent>();
      foreach (var otherComponent in otherComponents)
      {
        requestComponents.Add(new GetOtherComponentsListResponseMessage.OtherComponent
        {
          ComponentName = otherComponent.ComponentName,
          ComponentId = otherComponent.Id
        });
      }
      return requestComponents;
    }
  }
}