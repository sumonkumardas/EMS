using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Academic;

namespace SinePulse.EMS.Messages.SectionMessages
{
  public class ShowBranchMediumSectionListResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public IEnumerable<SectionMessageModel> Sections { get; }

    public ShowBranchMediumSectionListResponseMessage(ValidationResult validationResult, IEnumerable<SectionMessageModel> sections)
    {
      ValidationResult = validationResult;
      Sections = sections;
    } 
  }
}