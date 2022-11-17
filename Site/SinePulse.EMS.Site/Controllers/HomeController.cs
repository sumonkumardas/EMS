using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Domain.Shared;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.ProjectPrimitives;

namespace SinePulse.EMS.Site.Controllers
{
  public class HomeController : Controller
  {
    private readonly GetUserAssociationRequestHandler _getUserAssociationRequestHandler;

    public HomeController(GetUserAssociationRequestHandler getUserAssociationRequestHandler)
    {
      _getUserAssociationRequestHandler = getUserAssociationRequestHandler;
    }

    [Authorize]
    public IActionResult Index()
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetUserAssociationRequestMessage
      {
        Username = HttpContext.User.Identity.Name
      };

      var response = _getUserAssociationRequestHandler.Handle(requestMessage, cancellationToken);
      if (!response.Result.ValidationResult.IsValid || response.Result.Data.IsBillDueDateOver)
      {
        return RedirectToAction("Login", "Account");
      }

      var message = response.Result.Data;
      if (message.RoleFeatures.Any())
      {
        RoleTypeEnum roleType = message.RoleFeatures[0].Role.RoleType;
        switch (roleType)
        {
          case RoleTypeEnum.SuperAdmin:
            return RedirectToAction("Index", "Institute");
          case RoleTypeEnum.InstituteAdmin:
            return RedirectToAction("ViewInstitute", "Institute",
              new { instituteId = message.LoginUsersInstituteId });
          case RoleTypeEnum.BranchAdmin:
            return RedirectToAction("ViewBranch", "Branch",
              new { branchId = message.LoginUsersBranchId });
          case RoleTypeEnum.BranchMediumAdmin:
          case RoleTypeEnum.BranchMediumUser:
            return RedirectToAction("ViewBranchMedium", "BranchMedium",
              new { branchMediumId = message.LoginUsersBranchMediumId });
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
      else
      {
        return RedirectToAction("Login", "Account");
      }
    }

    public IActionResult About()
    {
      ViewData["Message"] = "Your application description page.";

      return View();
    }

    public IActionResult Contact()
    {
      ViewData["Message"] = "Your contact page.";

      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
