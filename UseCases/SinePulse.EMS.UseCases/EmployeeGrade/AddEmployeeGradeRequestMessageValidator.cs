using FluentValidation;
using FluentValidation.Validators;
using SinePulse.EMS.Messages.EmployeeGradeMessages;
using SinePulse.EMS.UseCases.Repositories;

namespace SinePulse.EMS.UseCases.EmployeeGrade
{
  public class AddEmployeeGradeRequestMessageValidator : AbstractValidator<AddEmployeeGradeRequestMessage>
  {
    private readonly IUniqueEmployeeGradePropertyChecker _uniqueEmployeeGradePropertyChecker;

    public AddEmployeeGradeRequestMessageValidator(
      IUniqueEmployeeGradePropertyChecker uniqueEmployeeGradePropertyChecker)
    {
      _uniqueEmployeeGradePropertyChecker = uniqueEmployeeGradePropertyChecker;
      RuleFor(x => x.GradeTitle).NotEmpty().WithMessage("Please Specify Grade Title.");
      RuleFor(x => x.GradeTitle).NotNull().WithMessage("Please Specify Grade Title.");
      RuleFor(x => x.GradeTitle).Must(IsUniqueGradeTitle).WithMessage("Grade Title already exists.");
      
      RuleFor(x => x.MaxSalary).NotNull().WithMessage("Please Specify Maximum Salary.");
      RuleFor(x => x.MaxSalary).LessThan(9999999999).WithMessage("Maximum Salary is too Big.");
      RuleFor(x => x.MaxSalary).GreaterThan(0).WithMessage("Maximum Salary cannot be Negative or Zero.");
      RuleFor(x => x.MaxSalary).GreaterThanOrEqualTo(x => x.MinSalary)
        .WithMessage("Maximum Salary must be greater or equal to Minimum Salary.");

      RuleFor(x => x.MinSalary).NotNull().WithMessage("Please Specify Minimum Salary.");
      RuleFor(x => x.MinSalary).LessThan(9999999999).WithMessage("Minimum Salary is too Big.");
      RuleFor(x => x.MinSalary).GreaterThan(0).WithMessage("Minimum Salary cannot be Negative or Zero.");
      RuleFor(x => x).Custom(IsUniqueMinAndMaxSalary);
    }

    private void IsUniqueMinAndMaxSalary(AddEmployeeGradeRequestMessage employeeGrade, CustomContext context)
    {
      var isUniqueSalaryRange = _uniqueEmployeeGradePropertyChecker.IsUniqueMinAndMaxSalary(employeeGrade);
      if (!isUniqueSalaryRange)
      {
         context.AddFailure("Entered Maximum and Minimum Salary Range Already Exists.");
      }
    }

    private bool IsUniqueGradeTitle(string gradeTitle)
    {
      return _uniqueEmployeeGradePropertyChecker.IsUniqueEmployeeGradeTitle(gradeTitle);
    }
  }
}