using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.InstituteMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Institutes
{
  public class AddInstituteUseCase : IAddInstituteUseCase
  {
    private readonly IRepository _repository;

    public AddInstituteUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public Institute AddInstitute(AddInstituteRequestMessage request)
    {
      var institute = new Institute
      {
        OrganisationName = request.OrganisationName,
        ShortName = request.ShortName,
        Slogan = request.Slogan,
        Domain = request.Domain,
        Email = request.Email,
        Favicon = request.Favicon,
        Logo = request.Logo,
        Banner = request.Banner,
        Description = request.Description,
        WhyChooseInstitue = request.WhyChooseInstitue,
        FacebookPageURL = request.FacebookPageURL,
        Status = Domain.Enums.StatusEnum.Active,

        AuditFields = new AuditFields
        {
          InsertedBy = request.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = request.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        }
      };

       _repository.Insert(institute);
      return institute;
    }
  }
}