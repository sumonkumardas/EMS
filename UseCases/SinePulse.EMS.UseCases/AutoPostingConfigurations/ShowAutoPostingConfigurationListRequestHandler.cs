using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.AutoPostingConfigurationMessages;
using SinePulse.EMS.Messages.Model.Accounts;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.AutoPostingConfigurations
{
  public class ShowAutoPostingConfigurationListRequestHandler : IRequestHandler<ShowAutoPostingConfigurationListRequestMessage, ShowAutoPostingConfigurationListResponseMessage>
  {
    private readonly ShowAutoPostingConfigurationListRequestMessageValidator _validator;
    private readonly IShowAutoPostingConfigurationListUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public ShowAutoPostingConfigurationListRequestHandler(ShowAutoPostingConfigurationListRequestMessageValidator validator,
      IShowAutoPostingConfigurationListUseCase useCase, EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ShowAutoPostingConfigurationListResponseMessage> Handle(ShowAutoPostingConfigurationListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowAutoPostingConfigurationListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowAutoPostingConfigurationListResponseMessage(validationResult,new List<AutoPostingConfigurationMessageModel>());
        return Task.FromResult(returnMessage);
      }

      var list = _useCase.ShowAutoPostingConfigurationList(request);

      returnMessage = new ShowAutoPostingConfigurationListResponseMessage(validationResult,list);
      return Task.FromResult(returnMessage);
    }
  }
}