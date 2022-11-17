using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.LeaveTypeMessages;
using SinePulse.EMS.Messages.NotificationMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.LeaveTypes;
using SinePulse.EMS.UseCases.Notifications;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class NotificationConfigurationController : BaseController
  {
    private readonly AddNotificationConfigurationRequestHandler _addNotificationConfigurationRequestHandler;
    private readonly AddNotificationConfigurationResponsePresenter _addNotificationConfigurationResponsePresenter;
    private readonly ShowNotificationConfigurationRequestHandler _showNotificationConfigurationRequestHandler;
    private readonly ShowNotificationConfigurationResponsePresenter _showNotificationConfigurationResponsePresenter;
    private readonly EditNotificationConfigurationRequestHandler _editNotificationConfigurationRequestHandler;
    private readonly EditNotificationConfigurationResponsePresenter _editNotificationConfigurationResponsePresenter;
    public NotificationConfigurationController(AddNotificationConfigurationRequestHandler addNotificationConfigurationRequestHandler,
      AddNotificationConfigurationResponsePresenter addNotificationConfigurationResponsePresenter,
      ShowNotificationConfigurationRequestHandler showNotificationConfigurationRequestHandler,
      ShowNotificationConfigurationResponsePresenter showNotificationConfigurationResponsePresenter,
      EditNotificationConfigurationRequestHandler editNotificationConfigurationRequestHandler,
      EditNotificationConfigurationResponsePresenter editNotificationConfigurationResponsePresenter,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _addNotificationConfigurationRequestHandler = addNotificationConfigurationRequestHandler;
      _addNotificationConfigurationResponsePresenter = addNotificationConfigurationResponsePresenter;
      _showNotificationConfigurationRequestHandler = showNotificationConfigurationRequestHandler;
      _showNotificationConfigurationResponsePresenter = showNotificationConfigurationResponsePresenter;
      _editNotificationConfigurationRequestHandler = editNotificationConfigurationRequestHandler;
      _editNotificationConfigurationResponsePresenter = editNotificationConfigurationResponsePresenter;
    }
    
    [HttpGet]
    public ActionResult AddNotificationConfiguration(long branchMediumId)
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var viewModel = new AddNotificationConfigurationViewModel
      {
        BranchMediumId = branchMediumId,
        LoginName = userAssociationMessage.LoginName,
        LoginImageUrl = userAssociationMessage.ImageUrl,
        AssociatedWith = userAssociationMessage.AssociatedWith,
        LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId,
        LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId,
        LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId,
        RoleFeatures = userAssociationMessage.RoleFeatures,
        InstituteBanner = userAssociationMessage.InstituteBanner
      };
      return View(viewModel);
    }
    
    [HttpPost]
    public ActionResult AddNotificationConfiguration(AddNotificationConfigurationViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddNotificationConfigurationRequestMessage();

      requestMessage.BranchMediumId = model.BranchMediumId;
      requestMessage.AbsentPeriod = model.AbsentPeriod;
      requestMessage.AbsentTimeIntervalType = model.AbsentTimeIntervalType;
      requestMessage.ClassTestStartPeriod = model.ClassTestStartPeriod;
      requestMessage.ClassTestStartTimeIntervalType = model.ClassTestStartTimeIntervalType;
      requestMessage.EntryDelayPeriod = model.EntryDelayPeriod;
      requestMessage.EntryDelayTimeIntervalType = model.EntryDelayTimeIntervalType;
      requestMessage.ExamStartPeriod =model.ExamStartPeriod;
      requestMessage.ExamStartTimeIntervalType = model.ExamStartTimeIntervalType;
      requestMessage.ResultGradePreparePeriod = model.ResultGradePreparePeriod;
      requestMessage.ResultGradePrepareTimeIntervalType = model.ResultGradePrepareTimeIntervalType;
      requestMessage.TermTestStartPeriod = model.TermTestStartPeriod;
      requestMessage.TermTestStartTimeIntervalType = model.TermTestStartTimeIntervalType;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addNotificationConfigurationRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _addNotificationConfigurationResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (response.Result.ConfigurationId > 0)
      {
        return Redirect("/BranchMedium/ViewBranchMedium?branchMediumId=" + model.BranchMediumId + "#tab_notificationConfiguration");
      }
      else
        return View(viewModel);
    } 
    [HttpGet]
    public ActionResult ViewNotificationConfiguration(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowNotificationConfigurationRequestMessage();
      requestMessage.BranchMediumId = branchMediumId;
      NotificationConfigurationViewModel model = new NotificationConfigurationViewModel();
      var response = _showNotificationConfigurationRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showNotificationConfigurationResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());
      return PartialView(viewModel);
    }
    
    [HttpGet]
    public ActionResult UpdateNotificationConfiguration(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowNotificationConfigurationRequestMessage();
      requestMessage.BranchMediumId = branchMediumId;

      NotificationConfigurationViewModel model = new NotificationConfigurationViewModel();
      var response = _showNotificationConfigurationRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showNotificationConfigurationResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      return View(viewModel);
    }
    
    [HttpPost]
    public ActionResult UpdateNotificationConfiguration(NotificationConfigurationViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditNotificationConfigurationRequestMessage();

      requestMessage.ConfigurationId = model.ConfigurationId;
      requestMessage.BranchMediumId = model.BranchMediumId;
      requestMessage.AbsentPeriod = model.AbsentPeriod;
      requestMessage.ClassTestStartPeriod = model.ClassTestStartPeriod;
      requestMessage.EntryDelayPeriod = model.EntryDelayPeriod;
      requestMessage.ExamStartPeriod = model.ExamStartPeriod;
      requestMessage.ResultGradePreparePeriod = model.ResultGradePreparePeriod;
      requestMessage.TermTestStartPeriod = model.TermTestStartPeriod;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _editNotificationConfigurationRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _editNotificationConfigurationResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (response.Result.ValidationResult.IsValid)
      {
        return Redirect("/BranchMedium/ViewBranchMedium?branchMediumId=" + model.BranchMediumId + "#tab_notificationConfiguration");
      }
      else
        return View(viewModel);
    }
  }
}