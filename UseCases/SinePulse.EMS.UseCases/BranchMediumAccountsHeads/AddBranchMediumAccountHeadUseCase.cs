using SinePulse.EMS.Domain.Accounts;
using SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.Domain.Shared;
using System;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Persistence.Facade;

namespace SinePulse.EMS.UseCases.BranchMediumAccountsHeads
{
    public class AddBranchMediumAccountHeadUseCase : IAddBranchMediumAccountHeadUseCase
    {
        private readonly IRepository _repository;
        private readonly EmsDbContext _dbContext;

        public AddBranchMediumAccountHeadUseCase(IRepository repository, EmsDbContext dbContext)
        {
            _repository = repository;
            _dbContext = dbContext;
        }

        public BranchMediumAccountHead AddAccountHead(AddBranchMediumAccountHeadRequestMessage message)
        {
            var parentHead = _repository.GetById<BranchMediumAccountHead>(message.ParentAccountHeadId,new []{nameof(BranchMediumAccountHead.AccountType)});
            var branchMedium = _repository.GetById<BranchMedium>(message.BranchMediumId);
            var accountType = parentHead.AccountType;//_repository.GetById<AccountType>(message.BranchMediumId);
            var session = _repository.GetById<Session>(message.CurrentSessionId);
            var accountHead = new BranchMediumAccountHead
            {
                AccountCode = message.AccountCode,
                AccountHeadName = message.AccountHeadName,
                AccountHeadType = message.AccountHeadType,
                AccountPeriod = message.AccountPeriod,
                AccountType = accountType,
                BranchMedium = branchMedium,
                IsLedger = message.IsLedger,
                Session = session,
                ParentHead = parentHead,
                Status = message.Status,
                AuditFields = new AuditFields
                {
                    InsertedBy = message.CurrentUserName,
                    InsertedDateTime = DateTime.Now,
                    LastUpdatedBy = message.CurrentUserName,
                    LastUpdatedDateTime = DateTime.Now
                }
            };
            _repository.Insert(accountHead);
            _dbContext.SaveChanges();
            return accountHead;
        }
    }
}