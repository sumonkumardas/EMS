using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.SectionMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Sections
{
  public class ShowSectionRequestHandler : IRequestHandler<ShowSectionRequestMessage, ShowSectionResponseMessage>
  {
    private readonly ShowSectionRequestMessageValidator _validator;
    private readonly IShowSectionUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public ShowSectionRequestHandler(ShowSectionRequestMessageValidator validator, IShowSectionUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<ShowSectionResponseMessage> Handle(ShowSectionRequestMessage request, CancellationToken cancellationToken)
    {
      ShowSectionResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowSectionResponseMessage(validationResult,null,null);
        return Task.FromResult(returnMessage);
      }

      var section = _useCase.ShowSection(request);
      var breakTime = _useCase.ShowBreakTime(request);

      returnMessage = new ShowSectionResponseMessage(validationResult,section, breakTime);
      return Task.FromResult(returnMessage);
    }
  }
}