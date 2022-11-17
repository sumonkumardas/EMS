using System;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.StudentMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Students
{
  public class AddOrChangeStudentImageUseCase : IAddOrChangeStudentImageUseCase
  {
    private readonly IRepository _repository;

    public AddOrChangeStudentImageUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public string UploadStudentImage(AddOrChangeStudentImageRequestMessage request)
    {
      var student = _repository.GetById<Student>(request.StudentId);
      var previousImage = student.ImageUrl;
      student.ImageUrl = request.ImageUrl;
      student.AuditFields.LastUpdatedBy = request.CurrentUserName;
      student.AuditFields.LastUpdatedDateTime = DateTime.Now;
      if (!string.IsNullOrEmpty(request.ImageUrl))
      {
        return previousImage;
      }
      return null;
    }
  }
}