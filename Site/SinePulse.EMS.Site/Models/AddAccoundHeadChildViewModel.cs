using System;
using System.Collections.Generic;
using SinePulse.EMS.Domain.Academic;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Accounts;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Site.Models
{
    public class AddAccoundHeadChildViewModel : BaseViewModel
    {
        public long Id { get; set; }
        public long BranchMediumId { get; set; }
        public long SessionId { get; set; }
        public string TreeUi { get; set; }
        public long ParentAccountHeadId { get; set; }
        public string AccountCode { get; set; }
        public string AccountHeadName { get; set; }
        public AccountHeadTypeEnum AccountHeadType { get; set; }
        public AccountPeriodEnum AccountPeriod { get; set; }
        public bool IsLedger { get; set; }
        public string ErrorMessage { get; set; }
        public List<Session> Sessions { get; set; }
        public MonthType Month { get; set; }
        public bool IsSessionClosed { get; set; }

    public class Session
        {
          public long SessionId { get; set; }
          public string SessionName { get; set; }
          public DateTime StartDate { get; set; }
          public DateTime EndDate { get; set; }
        }
  }

    
}