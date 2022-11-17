using System;
using System.Collections.Generic;
using SinePulse.EMS.Messages.Model.Shared;

namespace SinePulse.EMS.Messages.Model.Transactions
{
    public class TransactionMessageModel : BaseEntityMessageModel
    {

        #region Primitive Properties

        public string TransactionNo { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }

        #endregion

        #region  Collection Properties

        public ICollection<TransactionEntryMessageModel> TransactionEntries { get; set; }

        #endregion
    }
}