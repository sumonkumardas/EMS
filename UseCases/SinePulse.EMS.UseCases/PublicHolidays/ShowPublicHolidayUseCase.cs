using System;
using AutoMapper;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Holidays;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.Model.Holidays;
using SinePulse.EMS.Messages.PublicHolidayMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.PublicHolidays
{
  public class ShowPublicHolidayUseCase : IShowPublicHolidayUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _automapperConfig;

    public ShowPublicHolidayUseCase(IRepository repository)
    {
      _repository = repository;
      _automapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<PublicHoliday, PublicHolidayMessageModel>());
    }

    public PublicHolidayMessageModel ShowPublicHoliday(ShowPublicHolidayRequestMessage request)
    {
      var dbHoliday = _repository.GetById<PublicHoliday>(request.PublicHolidayId);
      var messageModelHoliday = new PublicHolidayMessageModel();
      var mapper = _automapperConfig.CreateMapper();
      return mapper.Map<PublicHolidayMessageModel>(dbHoliday);

    }
  }
}