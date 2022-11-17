using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Messages.AcademicFeeMessages;
using SinePulse.EMS.Messages.Model.Accounts;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.AcademicFees
{
  public class GetAcademicFeesUseCase : IGetAcademicFeesUseCase
  {
    private readonly IRepository _repository;
    private readonly MapperConfiguration _mapperConfiguration;

    public GetAcademicFeesUseCase(IRepository repository)
    {
      _repository = repository;
      _mapperConfiguration = new MapperConfiguration(c => c.CreateMap<AcademicFee, AcademicFeeMessageModel>());
    }

    public IEnumerable<AcademicFeeMessageModel> GetAcademicFees(GetAcademicFeesRequestMessage message)
    {
      var academicFees = _repository.Table<AcademicFee>()
        .Include(nameof(AcademicFee.AccountHead))
        .Include(nameof(Class))
        .Where(a => a.FeePeriod == message.FeePeriod
                    && a.AccountHead.BranchMedium.Id == message.BranchMediumId
                    && a.AccountHead.Session.Id == message.SessionId
                    && a.Class.Id == message.ClassId)
        .ToList();
      var mapper = _mapperConfiguration.CreateMapper();
      return mapper.Map(academicFees, new List<AcademicFeeMessageModel>());
    }
  }
}