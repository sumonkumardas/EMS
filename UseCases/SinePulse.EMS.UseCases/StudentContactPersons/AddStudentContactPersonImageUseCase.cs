using System;
using SinePulse.EMS.Domain.Students;
using SinePulse.EMS.Messages.StudentContactPersonMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.StudentContactPersons
{
  public class AddStudentContactPersonImageUseCase : IAddStudentContactPersonImageUseCase
  {
    private readonly IRepository _repository;

    public AddStudentContactPersonImageUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public string UploadStudentContactPersonImage(AddStudentContactPersonImageRequestMessage request)
    {
      var contactPerson = _repository.GetById<ContactPerson>(request.ContactPersonId);
      var previousImage = contactPerson.ImageUrl;
      contactPerson.ImageUrl = request.ImageUrl;
      contactPerson.AuditFields.LastUpdatedBy = request.CurrentUserName;
      contactPerson.AuditFields.LastUpdatedDateTime = DateTime.Now;
      if (!string.IsNullOrEmpty(request.ImageUrl))
        return previousImage;
      return null;
    }
  }
}