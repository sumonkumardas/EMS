using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.PublicHolidayMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.PublicHolidays
{
  public class EditPublicHolidayRequestHandler
    : IRequestHandler<EditPublicHolidayRequestMessage, EditPublicHolidayResponseMessage>
  {
    private readonly EditPublicHolidayRequestMessageValidator _validator;
    private readonly IEditPublicHolidayUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public EditPublicHolidayRequestHandler(EditPublicHolidayRequestMessageValidator validator,
      IEditPublicHolidayUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<EditPublicHolidayResponseMessage> Handle(EditPublicHolidayRequestMessage request,
      CancellationToken cancellationToken)
    {
      EditPublicHolidayResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new EditPublicHolidayResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.EditPublicHoliday(request);
      _dbContext.SaveChanges();

      returnMessage = new EditPublicHolidayResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}