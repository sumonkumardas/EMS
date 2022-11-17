using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.PublicHolidayMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.PublicHolidays
{
  public class AddPublicHolidayRequestHandler
    : IRequestHandler<AddPublicHolidayRequestMessage, AddPublicHolidayResponseMessage>
  {
    private readonly AddPublicHolidayRequestMessageValidator _validator;
    private readonly IAddPublicHolidayUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public AddPublicHolidayRequestHandler(AddPublicHolidayRequestMessageValidator validator,
      IAddPublicHolidayUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<AddPublicHolidayResponseMessage> Handle(AddPublicHolidayRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddPublicHolidayResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddPublicHolidayResponseMessage(validationResult, 0);
        return Task.FromResult(returnMessage);
      }

      var publicHoliday = _useCase.AddPublicHoliday(request);
      _dbContext.SaveChanges();

      returnMessage = new AddPublicHolidayResponseMessage(validationResult, publicHoliday.Id);
      return Task.FromResult(returnMessage);
    }
  }
}