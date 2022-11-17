using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.StudentMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Students
{
  public class GetStudentAddressUseCase : IGetStudentAddressUseCase
  {
    private readonly IRepository _repository;

    public GetStudentAddressUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public GetStudentAddressResponseMessage.StudentAddress GetStudentAddress(GetStudentAddressRequestMessage message)
    {
      var student = _repository.GetByIdWithInclude<Student>(message.StudentId,
        new[] {nameof(Student.PresentAddress), nameof(Student.PermanentAddress)});

      if (student.PermanentAddress != null && student.PresentAddress != null)
      {
        return new GetStudentAddressResponseMessage.StudentAddress
        {
          PresentStreet1 = student.PresentAddress.Street1,
          PresentStreet2 = student.PresentAddress.Street2,
          PresentPostalCode = student.PresentAddress.PostalCode,
          PresentCity = student.PresentAddress.City,
          PermanentStreet1 = student.PermanentAddress.Street1,
          PermanentStreet2 = student.PermanentAddress.Street2,
          PermanentPostalCode = student.PermanentAddress.PostalCode,
          PermanentCity = student.PermanentAddress.City
        };
      }

      return new GetStudentAddressResponseMessage.StudentAddress();
    }
  }
}