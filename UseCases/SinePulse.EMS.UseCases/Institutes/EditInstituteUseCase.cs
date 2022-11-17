using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.InstituteMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Institutes
{
  public class EditInstituteUseCase : IEditInstituteUseCase
  {
    private readonly IRepository _repository;

    public EditInstituteUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public void EditInstitute(EditInstituteRequestMessage request)
    {
      var institute = _repository.GetById<Institute>(request.Id);

      institute.OrganisationName = request.OrganisationName;
      institute.ShortName = request.ShortName;
      institute.Slogan = request.Slogan;
      institute.Domain = request.Domain;
      institute.Email = request.Email;
      institute.Favicon = request.Favicon;
      institute.Logo = request.Logo;
      institute.Banner = request.Banner;
      institute.Description = request.Description;
      institute.WhyChooseInstitue = request.WhyChooseInstitue;
      institute.FacebookPageURL = request.FacebookPageURL;
      institute.Status = Domain.Enums.StatusEnum.Active;
        institute.Id = request.Id;
      institute.AuditFields.LastUpdatedBy = request.CurrentUserName;
      institute.AuditFields.LastUpdatedDateTime = DateTime.Now;
    }
  }
}