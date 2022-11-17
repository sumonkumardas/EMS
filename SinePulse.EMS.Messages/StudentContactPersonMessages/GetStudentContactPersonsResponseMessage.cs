using System.Collections.Generic;
using FluentValidation.Results;
using SinePulse.EMS.Messages.Model.Students;

namespace SinePulse.EMS.Messages.StudentContactPersonMessages
{
  public class GetStudentContactPersonsResponseMessage
  {
    public ValidationResult ValidationResult { get; }
    public IEnumerable<ContactPersonMessageModel> ContactPersons { get; set; }

    public GetStudentContactPersonsResponseMessage(ValidationResult validationResult,
      IEnumerable<ContactPersonMessageModel> contactPersons)
    {
      ValidationResult = validationResult;
      ContactPersons = contactPersons;
    }
  }
}