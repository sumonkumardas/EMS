using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Examinations;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.EmployeeLeaveMessages;
using SinePulse.EMS.Messages.PublicHolidayMessages;
using SinePulse.EMS.Messages.TermMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.EmployeeLeaves;
using SinePulse.EMS.UseCases.PublicHolidays;
using SinePulse.EMS.UseCases.Terms;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class PublicHolidayController : BaseController
  {
    private readonly AddPublicHolidayRequestHandler _addPublicHolidayRequestHandler;
    private readonly AddPublicHolidayResponsePresenter _addPublicHolidayResponsePresenter;
    private readonly ShowPublicHolidayListRequestHandler _showPublicHolidayListRequestHandler;
    private readonly ShowPublicHolidayListResponsePresenter _showPublicHolidayListResponsePresenter;
    private readonly ShowPublicHolidayRequestHandler _showPublicHolidayRequestHandler;
    private readonly ShowPublicHolidayResponsePresenter _showPublicHolidayResponsePresenter;
    private readonly EditPublicHolidayRequestHandler _editPublicHolidayRequestHandler;
    private readonly EditPublicHolidayResponsePresenter _editPublicHolidayResponsePresenter;
    private readonly ShowTermListRequestHandler _showTermListRequestHandler;
    private readonly ShowTermListResponsePresenter _showTermListResponsePresenter;
    private readonly ShowEmployeeLeaveListRequestHandler _showEmployeeLeaveListRequestHandler;
    private readonly ShowEmployeeLeaveListResponsePresenter _showEmployeeLeaveListResponsePresenter;
        public PublicHolidayController(AddPublicHolidayRequestHandler addPublicHolidayRequestHandler,
      AddPublicHolidayResponsePresenter addPublicHolidayResponsePresenter, ShowPublicHolidayListRequestHandler showPublicHolidayListRequestHandler, ShowPublicHolidayListResponsePresenter showPublicHolidayListResponsePresenter,
      ShowPublicHolidayRequestHandler showPublicHolidayRequestHandler, ShowPublicHolidayResponsePresenter showPublicHolidayResponsePresenter,
      EditPublicHolidayRequestHandler editPublicHolidayRequestHandler,
      EditPublicHolidayResponsePresenter editPublicHolidayResponsePresenter,
      ShowTermListRequestHandler showTermListRequestHandler,
      ShowTermListResponsePresenter showTermListResponsePresenter,
      ShowEmployeeLeaveListRequestHandler showEmployeeLeaveListRequestHandler,
      ShowEmployeeLeaveListResponsePresenter showEmployeeLeaveListResponsePresenter,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _addPublicHolidayRequestHandler = addPublicHolidayRequestHandler;
      _addPublicHolidayResponsePresenter = addPublicHolidayResponsePresenter;
      _showPublicHolidayListRequestHandler = showPublicHolidayListRequestHandler;
      _showPublicHolidayListResponsePresenter = showPublicHolidayListResponsePresenter;
      _showPublicHolidayRequestHandler = showPublicHolidayRequestHandler;
      _showPublicHolidayResponsePresenter = showPublicHolidayResponsePresenter;
      _editPublicHolidayRequestHandler = editPublicHolidayRequestHandler;
      _editPublicHolidayResponsePresenter = editPublicHolidayResponsePresenter;
      _showTermListRequestHandler = showTermListRequestHandler;
      _showTermListResponsePresenter = showTermListResponsePresenter;
      _showEmployeeLeaveListRequestHandler = showEmployeeLeaveListRequestHandler;
      _showEmployeeLeaveListResponsePresenter = showEmployeeLeaveListResponsePresenter;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.GeneralSetup, (int)Feature.GeneralSetupEnum.ViewPublicHoliday)]
    public ActionResult Index()
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowPublicHolidayListRequestMessage();
      PublicHolidayListViewModel model = new PublicHolidayListViewModel();

      var response = _showPublicHolidayListRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showPublicHolidayListResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.GeneralSetup, (int)Feature.GeneralSetupEnum.NewPublicHoliday)]
    [HttpGet]
    public ActionResult AddPublicHoliday()
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var viewModel = new AddPublicHolidayViewModel();
      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.GeneralSetup, (int)Feature.GeneralSetupEnum.NewPublicHoliday)]
    [HttpPost]
    public ActionResult AddPublicHoliday(AddPublicHolidayViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddPublicHolidayRequestMessage();

      requestMessage.HolidayName = model.HolidayName;
      requestMessage.StartDate = model.StartDate;
      requestMessage.EndDate = model.EndDate;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addPublicHolidayRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _addPublicHolidayResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (response.Result.PublicHolidayId > 0)
      {
        return RedirectToAction("Index");
      }
      else
        return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.GeneralSetup, (int)Feature.GeneralSetupEnum.EditPublicHoliday)]
    [HttpGet]
    public ActionResult UpdatePublicHoliday(long publicHolidayId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowPublicHolidayRequestMessage();
      requestMessage.PublicHolidayId = publicHolidayId;

      PublicHolidayViewModel model = new PublicHolidayViewModel();
      var response = _showPublicHolidayRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showPublicHolidayResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.GeneralSetup, (int)Feature.GeneralSetupEnum.EditPublicHoliday)]
    [HttpPost]
    public ActionResult UpdatePublicHoliday(PublicHolidayViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditPublicHolidayRequestMessage();

      requestMessage.Id = model.Id;
      requestMessage.HolidayName = model.HolidayName;
      requestMessage.StartDate = model.StartDate;
      requestMessage.EndDate = model.EndDate;
      requestMessage.Status = model.Status;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _editPublicHolidayRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _editPublicHolidayResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (response.Result.ValidationResult.IsValid)
      {
        return RedirectToAction("Index");
      }
      else
        return View(viewModel);
    }

    [HttpGet]
    public JsonResult GetAcademicYearEvents(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowPublicHolidayListRequestMessage();
      requestMessage.Year = DateTime.Now.Year;
      PublicHolidayListViewModel publicHolidayModel = new PublicHolidayListViewModel();
      var publicHolidayResponse = _showPublicHolidayListRequestHandler.Handle(requestMessage, cancellationToken);
      var publicHolidayViewModel = _showPublicHolidayListResponsePresenter.Handle(publicHolidayResponse.Result, publicHolidayModel, GetUserAssociationResponseMessage());

      var termListequestMessage = new ShowTermListRequestMessage
      {
          Year = DateTime.Now.Year, BranchMediumId = branchMediumId
      };
      ShowTermListViewModel termListModel = new ShowTermListViewModel();
      var termListResponse = _showTermListRequestHandler.Handle(termListequestMessage, cancellationToken);
      var termListViewModel = _showTermListResponsePresenter.Handle(termListResponse.Result.Data, termListModel,GetUserAssociationResponseMessage()).Terms;

      var employeeLeaveListequestMessage = new ShowEmployeeLeaveListRequestMessage
      {
          Year = DateTime.Now.Year, BranchMediumId = branchMediumId
      };
      EmployeeLeaveListViewModel employeeLeaveListModel = new EmployeeLeaveListViewModel();
      var employeeLeaveListResponse = _showEmployeeLeaveListRequestHandler.Handle(employeeLeaveListequestMessage, cancellationToken);
      var employeeLeaveListViewModel = _showEmployeeLeaveListResponsePresenter.Handle(employeeLeaveListResponse.Result, employeeLeaveListModel, GetUserAssociationResponseMessage());
            List<EventViewModel> publicHolidayListModel = new List<EventViewModel>();
      foreach (var item in publicHolidayViewModel.Holidays)
      {
          var holidayEvent = new EventViewModel
          {
              start = item.StartDate,
              end = item.EndDate.AddDays(1),
              title = item.HolidayName,
              url = "/PublicHoliday/ViewPublicHoliday?publicHolidayId=" + item.Id,
              color = "#257e4a"
          };
          publicHolidayListModel.Add(holidayEvent);
      }
      foreach (var item in termListViewModel)
      {
          var holidayEvent = new EventViewModel
          {
              start = item.StartDate,
              end = item.EndDate.AddDays(1),
              title = item.TermName,
              url = "/Term/ViewTerm?termId=" + item.TermId,
              color = "#1AF3E5"
          };
          publicHolidayListModel.Add(holidayEvent);
      }

      foreach (var item in employeeLeaveListViewModel.EmployeeLeaves)
      {
          var holidayEvent = new EventViewModel
          {
              start = item.StartDate,
              end = item.EndDate.AddDays(1),
              title = item.Employee.FullName,
              color = "#157e4a"
          };
          publicHolidayListModel.Add(holidayEvent);
      }
            return Json(publicHolidayListModel);
    }
  }
}