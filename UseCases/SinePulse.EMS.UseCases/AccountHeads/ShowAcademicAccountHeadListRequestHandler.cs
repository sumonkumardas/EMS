using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.AccountHeadMessages;

namespace SinePulse.EMS.UseCases.AccountHeads
{
  public class ShowAcademicAccountHeadListRequestHandler : IRequestHandler<ShowAcademicAccountHeadListRequestMessage,
    ShowAcademicAccountHeadListResponseMessage>
  {
    private readonly ShowAcademicAccountHeadListRequestMessageValidator _validator;
    private readonly IShowAcademicAccountHeadListUseCase _useCase;

    public ShowAcademicAccountHeadListRequestHandler(ShowAcademicAccountHeadListRequestMessageValidator validator,
      IShowAcademicAccountHeadListUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowAcademicAccountHeadListResponseMessage> Handle(ShowAcademicAccountHeadListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowAcademicAccountHeadListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowAcademicAccountHeadListResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var accountHeads = _useCase.ShowAcademicAccountHeadList(request);

      returnMessage = new ShowAcademicAccountHeadListResponseMessage(validationResult, accountHeads);
      return Task.FromResult(returnMessage);
    }
  }
}