using MediatR;
using SinePulse.EMS.Messages.CommitteeMemberAddressMessages;
using SinePulse.EMS.Persistence.Facade;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SinePulse.EMS.UseCases.CommitteeMemberAddresses
{
  public class AddCommitteeMemberAddressRequestHandler : IRequestHandler<AddCommitteeMemberAddressRequestMessage, AddCommitteeMemberAddressResponseMessage>
  {
    private readonly AddCommitteeMemberAddressRequestMessageValidator _validator;
    private readonly IAddCommitteeMemberAddressUseCase _useCase;
    private readonly EmsDbContext _dbContext;

    public AddCommitteeMemberAddressRequestHandler(AddCommitteeMemberAddressRequestMessageValidator validator,
      IAddCommitteeMemberAddressUseCase useCase, EmsDbContext dbContext)
    {
      _validator = validator;
      _useCase = useCase;
      _dbContext = dbContext;
    }

    public Task<AddCommitteeMemberAddressResponseMessage> Handle(AddCommitteeMemberAddressRequestMessage request,
      CancellationToken cancellationToken)
    {
      AddCommitteeMemberAddressResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new AddCommitteeMemberAddressResponseMessage(validationResult);
        return Task.FromResult(returnMessage);
      }

      _useCase.AddCommitteeMemberAddress(request);
      _dbContext.SaveChanges();

      returnMessage = new AddCommitteeMemberAddressResponseMessage(validationResult);
      return Task.FromResult(returnMessage);
    }
  }
}
