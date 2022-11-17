using FluentValidation;
using SinePulse.EMS.Messages.ImportSessionDataMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.ImportSessionDatas
{
  public class ImportSessionDataRequestMessageValidator : AbstractValidator<ImportSessionDataRequestMessage>
  {
    private readonly IValidSectionChecker _validSectionChecker;

    public ImportSessionDataRequestMessageValidator(IValidSectionChecker validSectionChecker)
    {
      _validSectionChecker = validSectionChecker;
      RuleFor(x => x.OnlySectionInfo).Must(IsAtMostOneSectionImportSelected).WithMessage(
        "You can select at most one section import option");
      RuleFor(x => x.OnlyExamTerm).Must(IsAtMostOneExamImportSelected).WithMessage(
        "You can select at most one exam import option");
      RuleFor(x => x.PreviousSessionId).Must(IsValidSession).WithMessage(
        "Please Specify a positive value for TotalStudent");
    }

    private bool IsAtMostOneSectionImportSelected(ImportSessionDataRequestMessage message, bool onlySectionInfo)
    {
      return !message.OnlySectionInfo || !message.SectionInfoWithClassRoutine;
    }

    private bool IsAtMostOneExamImportSelected(ImportSessionDataRequestMessage message, bool onlyExamTerm)
    {
      return !message.OnlyExamTerm || !message.ExamTermWithExamConfiguration;
    }

    private bool IsValidSession(long sessionId)
    {
      return _validSectionChecker.IsValidSection(sessionId);
    }
  }
}