using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.SectionMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Sections
{
  public class AddSectionRequestHandler : IRequestHandler<AddSectionRequestMessage, AddSectionResponseMessage>
  {
    private readonly AddSectionRequestMessageValidator _validator;
    private readonly IAddSectionUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public AddSectionRequestHandler(AddSectionRequestMessageValidator validator, IAddSectionUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<AddSectionResponseMessage> Handle(AddSectionRequestMessage request, CancellationToken cancellationToken)
    {
      AddSectionResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddSectionResponseMessage(validationResult,0);
        return Task.FromResult(returnMessage);
      }

      var section = _useCase.AddSection(request);
      _dbContext.SaveChanges();

      returnMessage = new AddSectionResponseMessage(validationResult,section.Id);
      return Task.FromResult(returnMessage);
    }
  }
}