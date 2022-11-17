using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Routines;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.ClassRoutineMessages
{
  public class ShowClassRoutineListResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public List<ClassRoutineMessageModel> ClassRoutineList { get; }

    public ShowClassRoutineListResponseMessage(ValidationResult validationResult, List<ClassRoutineMessageModel> classRoutineList)
    {
      ValidationResult = validationResult;
      ClassRoutineList = classRoutineList;
    }
  }
}