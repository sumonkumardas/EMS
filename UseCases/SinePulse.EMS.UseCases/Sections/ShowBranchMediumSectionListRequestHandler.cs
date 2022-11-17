using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SinePulse.EMS.Messages.SectionMessages;

namespace SinePulse.EMS.UseCases.Sections
{
  public class ShowBranchMediumSectionListRequestHandler : IRequestHandler<ShowBranchMediumSectionListRequestMessage,
    ShowBranchMediumSectionListResponseMessage>
  {
    private readonly ShowBranchMediumSectionListRequestMessageValidator _validator;
    private readonly IShowBranchMediumSectionListUseCase _useCase;

    public ShowBranchMediumSectionListRequestHandler(ShowBranchMediumSectionListRequestMessageValidator validator,
      IShowBranchMediumSectionListUseCase useCase)
    {
      _validator = validator;
      _useCase = useCase;
    }

    public Task<ShowBranchMediumSectionListResponseMessage> Handle(ShowBranchMediumSectionListRequestMessage request,
      CancellationToken cancellationToken)
    {
      ShowBranchMediumSectionListResponseMessage returnMessage;

      var validationResult = _validator.Validate(request);
      if (validationResult.IsValid == false)
      {
        returnMessage = new ShowBranchMediumSectionListResponseMessage(validationResult, null);
        return Task.FromResult(returnMessage);
      }

      var sections = _useCase.ShowBranchMediumSectionList(request);

      returnMessage = new ShowBranchMediumSectionListResponseMessage(validationResult, sections);
      return Task.FromResult(returnMessage);
    }
  }
}