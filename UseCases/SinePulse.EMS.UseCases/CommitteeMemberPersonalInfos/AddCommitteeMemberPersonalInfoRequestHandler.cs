using MediatR;
using SinePulse.EMS.Messages.CommitteeMemberPersonalInfoMessages;
using SinePulse.EMS.Persistence.Facade;
using System.Threading;
using System.Threading.Tasks;

namespace SinePulse.EMS.UseCases.CommitteeMemberPersonalInfos
{
  public class AddCommitteeMemberPersonalInfoRequestHandler : IRequestHandler<AddCommitteeMemberPersonalInfoRequestMessage, AddCommitteeMemberPersonalInfoResponseMessage>
  {
    private readonly AddCommitteeMemberPersonalInfoRequestMessageValidator _validator;
    private readonly IAddCommitteeMemberPersonalInfoUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public AddCommitteeMemberPersonalInfoRequestHandler(AddCommitteeMemberPersonalInfoRequestMessageValidator validator, IAddCommitteeMemberPersonalInfoUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<AddCommitteeMemberPersonalInfoResponseMessage> Handle(AddCommitteeMemberPersonalInfoRequestMessage request, CancellationToken cancellationToken)
    {
      AddCommitteeMemberPersonalInfoResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddCommitteeMemberPersonalInfoResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.AddCommitteeMemberPersonalInfo(request);
      _dbContext.SaveChanges();

      returnMessage = new AddCommitteeMemberPersonalInfoResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}
