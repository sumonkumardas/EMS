using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.SectionMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.Sections
{
  public class EditSectionRequestHandler : IRequestHandler<EditSectionRequestMessage, EditSectionResponseMessage>
  {
    private readonly EditSectionRequestMessageValidator _validator;
    private readonly IEditSectionUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public EditSectionRequestHandler(EditSectionRequestMessageValidator validator, IEditSectionUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<EditSectionResponseMessage> Handle(EditSectionRequestMessage request, CancellationToken cancellationToken)
    {
      EditSectionResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new EditSectionResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.EditSection(request);
      _dbContext.SaveChanges();

      returnMessage = new EditSectionResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}