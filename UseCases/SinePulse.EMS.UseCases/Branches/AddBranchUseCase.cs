using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.BranchMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Branches
{
  public class AddBranchUseCase : IAddBranchUseCase
  {
    private readonly IRepository _repository;

    public AddBranchUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public Branch AddBranch(AddBranchRequestMessage request)
    {
      var institute = _repository.GetById<Institute>(request.InstituteId);
      var branch = new Branch
      {
        BranchName = request.BranchName,
        BranchCode = request.BranchCode,
        MobileNo = request.MobileNo,
        Email = request.Email,
        Fax = request.Fax,
        Status = Domain.Enums.StatusEnum.Active,
        Institute = institute,

        AuditFields = new AuditFields
        {
          InsertedBy = request.CurrentUserName,
          InsertedDateTime = DateTime.Now,
          LastUpdatedBy = request.CurrentUserName,
          LastUpdatedDateTime = DateTime.Now
        }
      };

      _repository.Insert(branch);
      return branch;
    }
  }
}