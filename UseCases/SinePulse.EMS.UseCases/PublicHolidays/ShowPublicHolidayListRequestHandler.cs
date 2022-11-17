using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.Model.Holidays;
using SinePulse.EMS.Messages.PublicHolidayMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.PublicHolidays
{
  public class ShowPublicHolidayListRequestHandler
    : IRequestHandler<ShowPublicHolidayListRequestMessage, ShowPublicHolidayListResponseMessage>
  {
    private readonly ShowPublicHolidayListRequestMessageValidator _validator;
    private readonly IShowPublicHolidayListUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public ShowPublicHolidayListRequestHandler(ShowPublicHolidayListRequestMessageValidator validator,
      IShowPublicHolidayListUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ShowPublicHolidayListResponseMessage> Handle(ShowPublicHolidayListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowPublicHolidayListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowPublicHolidayListResponseMessage(validationResult, new List<PublicHolidayMessageModel>());
        return Task.FromResult(returnMessage);
      }

      var publicHolidayList = _useCase.ShowPublicHolidayList(request);

      returnMessage = new ShowPublicHolidayListResponseMessage(validationResult, publicHolidayList);
      return Task.FromResult(returnMessage);
    }
  }
}