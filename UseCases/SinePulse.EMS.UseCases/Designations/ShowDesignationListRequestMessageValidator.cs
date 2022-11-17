using FluentValidation;
using SinePulse.EMS.Messages.DesignationMessages;

namespace SinePulse.EMS.UseCases.Designations
{
  public class ShowDesignationListRequestMessageValidator : AbstractValidator<ShowDesignationListRequestMessage>
  {

    public ShowDesignationListRequestMessageValidator()
    {
    }
    
  }
}