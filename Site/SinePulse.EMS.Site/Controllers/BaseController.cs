using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.UseCases.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SinePulse.EMS.Site.Controllers
{
  public class BaseController : Controller
  {
    protected readonly GetUserAssociationRequestHandler _getUserAssociationRequestHandler;

    public BaseController(GetUserAssociationRequestHandler getUserAssociationRequestHandler)
    {
      _getUserAssociationRequestHandler = getUserAssociationRequestHandler;
    }

    public GetUserAssociationResponseMessage GetUserAssociationResponseMessage()
    {
      var cancellationToken = new CancellationToken();
      var userAssociationrequestMessage = new GetUserAssociationRequestMessage
      {
        Username = HttpContext.User.Identity.Name
      };

      var userAssociationResponseMessage = _getUserAssociationRequestHandler.Handle(userAssociationrequestMessage, cancellationToken);
      return userAssociationResponseMessage.Result.Data;
    }
  }
}
