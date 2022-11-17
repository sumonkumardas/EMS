using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AcademicFeeMessages;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Accounts;
using SinePulse.EMS.Messages.Model.Students;
using SinePulse.EMS.Messages.SessionMessages;
using SinePulse.EMS.Messages.StudentMessages;
using SinePulse.EMS.Messages.WaiverMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.AcademicFees;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.Sessions;
using SinePulse.EMS.UseCases.Students;
using SinePulse.EMS.UseCases.Waivers;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class WaiverController : BaseController
  {
    private readonly ShowStudentRequestHandler _showStudentRequestHandler;
    private readonly ShowSessionListRequestHandler _showSessionListRequestHandler;
    private readonly GetAcademicFeesRequestHandler _getAcademicFeesRequestHandler;
    private readonly AddWaiverRequestHandler _addWaiverRequestHandler;
    private readonly AddWaiverResponsePresenter _addWaiverResponsePresenter;
    private readonly GetStudentWaiversRequestHandler _getStudentWaiversRequestHandler;

    public WaiverController(ShowStudentRequestHandler showStudentRequestHandler,
      ShowSessionListRequestHandler showSessionListRequestHandler,
      GetAcademicFeesRequestHandler getAcademicFeesRequestHandler,
      AddWaiverRequestHandler addWaiverRequestHandler,
      AddWaiverResponsePresenter addWaiverResponsePresenter,
      GetStudentWaiversRequestHandler getStudentWaiversRequestHandler,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _showStudentRequestHandler = showStudentRequestHandler;
      _showSessionListRequestHandler = showSessionListRequestHandler;
      _getAcademicFeesRequestHandler = getAcademicFeesRequestHandler;
      _addWaiverRequestHandler = addWaiverRequestHandler;
      _addWaiverResponsePresenter = addWaiverResponsePresenter;
      _getStudentWaiversRequestHandler = getStudentWaiversRequestHandler;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Student, (int)Feature.StudentEnum.WaiveAcademicFees)]
    [HttpGet]
    public ActionResult AddStudentWaiver(long studentId, long branchMediumId)
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var viewModel = new AddWaiverViewModel();
      var studentData = GetStudentData(studentId, branchMediumId);
      viewModel.StudentName = studentData.FullName;
      viewModel.ClassName = studentData.ClassName;
      viewModel.ClassId = studentData.ClassId;
      viewModel.SectionName = studentData.SectionName;
      viewModel.SectionId = studentData.SectionId;
      viewModel.Roll = studentData.Roll;
      viewModel.Sessions = GetSessions(branchMediumId);
      viewModel.StudentId = studentId;
      viewModel.BranchMediumId = branchMediumId;

      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      ((BaseViewModel)viewModel).LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Student, (int)Feature.StudentEnum.WaiveAcademicFees)]
    [HttpPost]
    public ActionResult AddStudentWaiver(AddWaiverViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddWaiverRequestMessage();
      requestMessage.Waivers = model.WaiverCollection;
      requestMessage.InPercentages = model.InPercentage;
      requestMessage.StudentId = model.StudentId;
      requestMessage.ClassId = model.ClassId;
      requestMessage.SectionId = model.SectionId;
      requestMessage.SessionId = model.SessionId;
      requestMessage.BranchMediumId = model.BranchMediumId;
      requestMessage.AcademicFees =
        AcademicFees(model.ClassId, model.SessionId, model.BranchMediumId, model.AcademicFeePeriod);
      requestMessage.InPercentagesBooleanArray = GetInPercentageBooleanArray(model.InPercentage, requestMessage.AcademicFees.Count());
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addWaiverRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _addWaiverResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      if (!response.Result.ValidationResult.IsValid)
      {
        viewModel.BranchMediumId = model.BranchMediumId;
        viewModel.AcademicFeePeriod = model.AcademicFeePeriod;
        viewModel.Sessions = GetSessions(model.BranchMediumId);
        viewModel.ClassId = model.ClassId;
        viewModel.SectionId = model.SectionId;
        viewModel.StudentName = model.StudentName;
        viewModel.ClassName = model.ClassName;
        viewModel.SectionName = model.SectionName;
        viewModel.SectionId = model.SectionId;
        viewModel.Roll = model.Roll;
        return View(viewModel);
      }

      return RedirectToAction("ViewStudent", "Student",
        new {studentId = model.StudentId, branchMediumId = model.BranchMediumId});
    }

    private IEnumerable<AcademicFeeMessageModel> AcademicFees(long classId, long sessionId, long branchMediumId,
      AcademicFeePeriodEnum academicFeePeriod)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetAcademicFeesRequestMessage();
      requestMessage.FeePeriod = academicFeePeriod;
      requestMessage.ClassId = classId;
      requestMessage.SessionId = sessionId;
      requestMessage.BranchMediumId = branchMediumId;
      var response = _getAcademicFeesRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.AcademicFees;
    }

    [HttpPost]
    public JsonResult GetAcademicFees(long studentId, long sectionId, long classId, long sessionId, long branchMediumId,
      AcademicFeePeriodEnum feePeriod)
    {
      if (feePeriod > 0 && sessionId > 0)
      {
        var studentWaivers = GetStudentWaivers(studentId, sectionId, sessionId, feePeriod);
        if (studentWaivers.Any())
          return Json(studentWaivers);
        var cancellationToken = new CancellationToken();
        var requestMessage = new GetAcademicFeesRequestMessage();
        requestMessage.FeePeriod = feePeriod;
        requestMessage.ClassId = classId;
        requestMessage.SessionId = sessionId;
        requestMessage.BranchMediumId = branchMediumId;
        var response = _getAcademicFeesRequestHandler.Handle(requestMessage, cancellationToken);
        if (response.Result.AcademicFees.Any())
          return Json(response.Result.AcademicFees);
        return Json(new {academicFeeNotAddedMsg = feePeriod.ToString("G") + " Academic Fee Not Added."});
      }

      return Json(new {disableSaveButtonMsg = "Disable Save Button"});
    }


    private ShowStudentResponseMessage.Student GetStudentData(long studentId, long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowStudentRequestMessage
      {
        StudentId = studentId,
        BranchMediumId = branchMediumId
      };
      var response = _showStudentRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.Data.StudentData;
    }

    private IEnumerable<SessionMessageModel> GetSessions(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowSessionListRequestMessage();
      requestMessage.BranchMediumId = branchMediumId;
      var response = _showSessionListRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.Sessions;
    }

    private IEnumerable<StudentWaiverMessageModel> GetStudentWaivers(long studentId, long sectionId,
      long sessionId,
      AcademicFeePeriodEnum feePeriod)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetStudentWaiversRequestMessage();
      requestMessage.StudentId = studentId;
      requestMessage.SectionId = sectionId;
      requestMessage.SessionId = sessionId;
      requestMessage.FeePeriod = feePeriod;
      var response = _getStudentWaiversRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.StudentWaivers;
    }
    
    private bool[] GetInPercentageBooleanArray(int[] inPercentageIndexes, int size)
    {
      if (inPercentageIndexes == null)
        return new bool[size];
      var inPercentageArray = new bool[size];
      foreach (var i in inPercentageIndexes)
      {
        inPercentageArray[i] = true;
      }

      return inPercentageArray;
    }
  }
}