namespace SinePulse.EMS.UseCases.Transactions
{
  public interface ITransactionNoTrackingDataManager
  {
    string GetNextTransactionNo(string transactionPrefix, long branchMediumId, string currentUserName);
  }
}