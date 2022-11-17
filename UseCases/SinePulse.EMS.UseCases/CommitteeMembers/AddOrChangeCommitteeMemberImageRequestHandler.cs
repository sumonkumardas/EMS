using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.CommitteeMemberMessages;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.CommitteeMembers
{
  public class AddOrChangeCommitteeMemberImageRequestHandler : IRequestHandler<
    AddOrChangeCommitteeMemberImageRequestMessage,
    AddOrChangeCommitteeMemberImageResponseMessage>
  {
    private readonly AddOrChangeCommitteeMemberImageRequestMessageValidator _validator;
    private readonly IAddOrChangeCommitteeMemberImageUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public AddOrChangeCommitteeMemberImageRequestHandler(
      AddOrChangeCommitteeMemberImageRequestMessageValidator validator,
      IAddOrChangeCommitteeMemberImageUseCase useCase,
      EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<AddOrChangeCommitteeMemberImageResponseMessage> Handle(
      AddOrChangeCommitteeMemberImageRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddOrChangeCommitteeMemberImageResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddOrChangeCommitteeMemberImageResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var previousImage = _useCase.UploadCommitteeMemberImage(request);
      _dbContext.SaveChanges();

      returnMessage = new AddOrChangeCommitteeMemberImageResponseMessage(validationResult, previousImage);
      return Task.FromResult(returnMessage);
    }
  }
}