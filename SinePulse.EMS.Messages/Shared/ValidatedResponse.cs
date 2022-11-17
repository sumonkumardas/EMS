using FluentValidation.Results;

namespace SinePulse.EMS.Messages.Shared
{
  public class ValidatedData<TData>
  {
    public ValidationResult ValidationResult { get; set; }
    public TData Data { get; }

    public ValidatedData(ValidationResult validationResult)
    {
      ValidationResult = validationResult;
    }

    public ValidatedData(ValidationResult validationResult, TData data)
    {
      ValidationResult = validationResult;
      Data = data;
    }
  }
}