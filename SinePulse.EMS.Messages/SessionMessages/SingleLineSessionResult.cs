namespace SinePulse.EMS.Messages.SessionMessages
{
  public class SingleLineSessionResult
  {
    public long SessionId { get; set; }
    public string SessionName { get; set; }

    /// <summary>
    /// Global or Institute or Branch or Medium
    /// </summary>
    public string SessionType { get; set; }

    /// <summary>
    /// Global is Session Type is Global
    /// Institute Name if Session Type is Institute 
    /// Branch Name if Session Type is Branch
    /// Medium Name if Session Type is BranchMedium
    /// </summary>
    public string SessionAssociatedObjectName { get; set; }
  }
}