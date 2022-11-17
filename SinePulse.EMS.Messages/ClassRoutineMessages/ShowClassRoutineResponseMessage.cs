using FluentValidation.Results;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Messages.Model.Routines;
using System.Collections.Generic;

namespace SinePulse.EMS.Messages.ClassRoutineMessages
{
  public class ShowClassRoutineResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public ClassRoutineMessageModel ClassRoutine { get; }

    public ShowClassRoutineResponseMessage(ValidationResult validationResult, ClassRoutineMessageModel classRoutine)
    {
      ValidationResult = validationResult;
      ClassRoutine = classRoutine;
    }
  }
}