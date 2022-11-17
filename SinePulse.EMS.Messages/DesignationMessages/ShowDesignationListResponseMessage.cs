using FluentValidation.Results;

using SinePulse.EMS.Messages.Model.Shared;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.DesignationMessages
{
  public class ShowDesignationListResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public List<DesignationMessageModel> DesignationList { get; }

    public ShowDesignationListResponseMessage(ValidationResult validationResult, List<DesignationMessageModel> designationList)
    {
      ValidationResult = validationResult;
      DesignationList = designationList;
    }
  }
}