using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.Model.Holidays;
using SinePulse.EMS.Messages.PublicHolidayMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.PublicHolidays
{
  public class ShowPublicHolidayRequestHandler
    : IRequestHandler<ShowPublicHolidayRequestMessage, ShowPublicHolidayResponseMessage>
  {
    private readonly ShowPublicHolidayRequestMessageValidator _validator;
    private readonly IShowPublicHolidayUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public ShowPublicHolidayRequestHandler(ShowPublicHolidayRequestMessageValidator validator,
      IShowPublicHolidayUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ShowPublicHolidayResponseMessage> Handle(ShowPublicHolidayRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowPublicHolidayResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowPublicHolidayResponseMessage(validationResult, new PublicHolidayMessageModel());
        return Task.FromResult(returnMessage);
      }

      var publicHoliday = _useCase.ShowPublicHoliday(request);

      returnMessage = new ShowPublicHolidayResponseMessage(validationResult, publicHoliday);
      return Task.FromResult(returnMessage);
    }
  }
}