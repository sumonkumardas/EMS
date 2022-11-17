using System;
using System.Collections.Generic;
using AutoMapper;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Holidays;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.Model.Holidays;
using SinePulse.EMS.Messages.PublicHolidayMessages;
using SinePulse.EMS.Persistence.Repositories;
using System.Linq;

namespace SinePulse.EMS.UseCases.PublicHolidays
{
  public class ShowPublicHolidayListUseCase : IShowPublicHolidayListUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _automapperConfig;

    public ShowPublicHolidayListUseCase(IRepository repository)
    {
      _repository = repository;
      _automapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<PublicHoliday, PublicHolidayMessageModel>());
    }

    public List<PublicHolidayMessageModel> ShowPublicHolidayList(ShowPublicHolidayListRequestMessage request)
    {
      var messageModelHolidayList = new List<PublicHolidayMessageModel>();
      var mapper = _automapperConfig.CreateMapper();
      if (request.Year == null)
      {
        var dbHolidayList = _repository.Table<PublicHoliday>();
        return mapper.Map<List<PublicHolidayMessageModel>>(dbHolidayList);
      }
      else
      {
        var dbHolidayList = _repository.Table<PublicHoliday>().Where(x=>x.StartDate.Year ==request.Year.Value && x.EndDate.Year == request.Year.Value);
        return mapper.Map<List<PublicHolidayMessageModel>>(dbHolidayList);
      }
      

    }
  }
}