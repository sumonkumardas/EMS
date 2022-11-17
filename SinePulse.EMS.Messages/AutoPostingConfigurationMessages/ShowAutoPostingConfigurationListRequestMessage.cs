using MediatR;
using SinePulse.EMS.Messages.Model.Enums;


namespace SinePulse.EMS.Messages.AutoPostingConfigurationMessages
{
  public class ShowAutoPostingConfigurationListRequestMessage : IRequest<ShowAutoPostingConfigurationListResponseMessage>
  {
      public VoucherTypeEnum VoucherType { get; set; }
  }
}