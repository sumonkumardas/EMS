using FluentValidation;
using SinePulse.EMS.Messages.BranchMediumMessages;

namespace SinePulse.EMS.UseCases.BranchMediums
{
  public class ImportCOAFromMasterRequestMessageValidator : AbstractValidator<ImportCOAFromMasterRequestMessage>
  {
    public ImportCOAFromMasterRequestMessageValidator()
    {
    }
  }
}