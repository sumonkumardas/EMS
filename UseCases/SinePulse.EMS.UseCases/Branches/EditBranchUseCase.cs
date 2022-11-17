using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.BranchMessages;
using SinePulse.EMS.Persistence.Repositories;

namespace SinePulse.EMS.UseCases.Branches
{
  public class EditBranchUseCase : IEditBranchUseCase
  {
    private readonly IRepository _repository;

    public EditBranchUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public void EditBranch(EditBranchRequestMessage request)
    {
      var branch = _repository.GetById<Branch>(request.Id);
      var institute = _repository.GetById<Institute>(request.InstituteId);
      branch.BranchName = request.BranchName;
      branch.BranchCode = request.BranchCode;
      branch.Email = request.Email;
      branch.MobileNo = request.MobileNo;
      branch.Fax = request.Fax;
      branch.Institute = institute;
      branch.Status = Domain.Enums.StatusEnum.Active;
        branch.Id = request.Id;
      branch.AuditFields.LastUpdatedBy = request.CurrentUserName;
      branch.AuditFields.LastUpdatedDateTime = DateTime.Now;
    }
  }
}