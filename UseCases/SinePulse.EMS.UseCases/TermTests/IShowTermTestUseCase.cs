using System.Collections.Generic;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Messages.TermTestMessages;

namespace SinePulse.EMS.UseCases.TermTests
{
  public interface IShowTermTestUseCase
  {
      TermTest ShowTermTest(ShowTermTestRequestMessage requestMessage);
  }
}