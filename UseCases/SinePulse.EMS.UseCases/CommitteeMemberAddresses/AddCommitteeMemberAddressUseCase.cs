using SinePulse.EMS.Domain.ManagingCommittee;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Messages.CommitteeMemberAddressMessages;
using SinePulse.EMS.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SinePulse.EMS.UseCases.CommitteeMemberAddresses
{
  public class AddCommitteeMemberAddressUseCase : IAddCommitteeMemberAddressUseCase
  {
    private readonly IRepository _repository;

    public AddCommitteeMemberAddressUseCase(IRepository repository)
    {
      _repository = repository;
    }

    public void AddCommitteeMemberAddress(AddCommitteeMemberAddressRequestMessage message)
    {
      var employee = _repository.GetByIdWithInclude<CommitteeMember>(message.CommitteeMemberId,
        new[] { nameof(CommitteeMember.PresentAddress), nameof(CommitteeMember.PermanentAddress) });

      if (employee.PermanentAddress != null && employee.PresentAddress != null)
      {
        employee.PermanentAddress.City = message.PermanentAddressCity;
        employee.PermanentAddress.PostalCode = message.PermanentAddressPostalCode;
        employee.PermanentAddress.Street1 = message.PermanentAddressStreet1;
        employee.PermanentAddress.Street2 = message.PermanentAddressStreet2;
        employee.PermanentAddress.AuditFields.InsertedBy = message.CurrentUserName;
        employee.PermanentAddress.AuditFields.InsertedDateTime = DateTime.Now;
        employee.PermanentAddress.AuditFields.LastUpdatedBy = message.CurrentUserName;
        employee.PermanentAddress.AuditFields.LastUpdatedDateTime = DateTime.Now;

        employee.PresentAddress.City = message.PresentAddressCity;
        employee.PresentAddress.PostalCode = message.PresentAddressPostalCode;
        employee.PresentAddress.Street1 = message.PresentAddressStreet1;
        employee.PresentAddress.Street2 = message.PresentAddressStreet2;
        employee.PresentAddress.AuditFields.InsertedBy = message.CurrentUserName;
        employee.PresentAddress.AuditFields.InsertedDateTime = DateTime.Now;
        employee.PresentAddress.AuditFields.LastUpdatedBy = message.CurrentUserName;
        employee.PresentAddress.AuditFields.LastUpdatedDateTime = DateTime.Now;
      }
      else
      {
        var permanentAddress = new Address
        {
          City = message.PermanentAddressCity,
          PostalCode = message.PermanentAddressPostalCode,
          Street1 = message.PermanentAddressStreet1,
          Street2 = message.PermanentAddressStreet2,
          AuditFields = new AuditFields
          {
            InsertedBy = message.CurrentUserName,
            InsertedDateTime = DateTime.Now,
            LastUpdatedBy = message.CurrentUserName,
            LastUpdatedDateTime = DateTime.Now
          }
        };
        var presentAddress = new Address
        {
          City = message.PresentAddressCity,
          PostalCode = message.PresentAddressPostalCode,
          Street1 = message.PresentAddressStreet1,
          Street2 = message.PresentAddressStreet2,
          AuditFields = new AuditFields
          {
            InsertedBy = message.CurrentUserName,
            InsertedDateTime = DateTime.Now,
            LastUpdatedBy = message.CurrentUserName,
            LastUpdatedDateTime = DateTime.Now
          }
        };

        employee.PermanentAddress = permanentAddress;
        employee.PresentAddress = presentAddress;
      }

    }
  }
}
