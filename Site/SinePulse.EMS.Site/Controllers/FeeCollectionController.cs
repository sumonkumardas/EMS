using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AcademicFeeMessages;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.BranchMediumAccountsHeadMessages;
using SinePulse.EMS.Messages.FeeCollection;
using SinePulse.EMS.Messages.Model.Academic;
using SinePulse.EMS.Messages.Model.Students;
using SinePulse.EMS.Messages.SessionMessages;
using SinePulse.EMS.Messages.StudentMessages;
using SinePulse.EMS.Messages.WaiverMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.AcademicFees;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.BranchMediumAccountsHeads;
using SinePulse.EMS.UseCases.FeeCollections;
using SinePulse.EMS.UseCases.Sessions;
using SinePulse.EMS.UseCases.Students;
using SinePulse.EMS.UseCases.Waivers;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class FeeCollectionController : BaseController
  {
    private readonly ShowStudentRequestHandler _showStudentRequestHandler;
    private readonly GetBankAccountAccountHeadsRequestHandler _getBankAccountAccountHeadsRequestHandler;
    private readonly GetStudentWaiversRequestHandler _getStudentWaiversRequestHandler;
    private readonly ShowSessionListRequestHandler _showSessionListRequestHandler;
    private readonly FeeCollectionRequestHandler _feeCollectionRequestHandler;
    private readonly FeeCollectionResponsePresenter _feeCollectionResponsePresenter;
    private readonly GetAcademicFeesRequestHandler _getAcademicFeesRequestHandler;

    public FeeCollectionController(ShowStudentRequestHandler showStudentRequestHandler,
      GetBankAccountAccountHeadsRequestHandler getBankAccountAccountHeadsRequestHandler,
      GetStudentWaiversRequestHandler getStudentWaiversRequestHandler,
      ShowSessionListRequestHandler showSessionListRequestHandler, 
      FeeCollectionRequestHandler feeCollectionRequestHandler, 
      FeeCollectionResponsePresenter feeCollectionResponsePresenter, 
      GetAcademicFeesRequestHandler getAcademicFeesRequestHandler,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _showStudentRequestHandler = showStudentRequestHandler;
      _getBankAccountAccountHeadsRequestHandler = getBankAccountAccountHeadsRequestHandler;
      _getStudentWaiversRequestHandler = getStudentWaiversRequestHandler;
      _showSessionListRequestHandler = showSessionListRequestHandler;
      _feeCollectionRequestHandler = feeCollectionRequestHandler;
      _feeCollectionResponsePresenter = feeCollectionResponsePresenter;
      _getAcademicFeesRequestHandler = getAcademicFeesRequestHandler;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.BranchMedium, (int)Feature.BranchMediumEnum.CollectAcademicFees)]
    [HttpGet]
    public ActionResult CollectAcademicFee(long studentId, long branchMediumId, AcademicFeePeriodEnum feePeriod)
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();

      var viewModel = new FeeCollectionViewModel();
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
      viewModel.FeeType = feePeriod;

      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.BranchMedium, (int)Feature.BranchMediumEnum.CollectAcademicFees)]
    [HttpPost]
    public ActionResult CollectAcademicFee(FeeCollectionViewModel model)
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var cancellationToken = new CancellationToken();
      var requestMessage = new FeeCollectionRequestMessage();

      requestMessage.Amounts = model.Amounts;
      requestMessage.Waivers = model.Waivers;
      requestMessage.FeeType = model.FeeType;
      requestMessage.PaymentMethod = model.PaymentMethod;
      requestMessage.Month = model.Month;
      requestMessage.StudentId = model.StudentId;
      requestMessage.SessionId = model.SessionId;
      requestMessage.SectionId = model.SectionId;
      requestMessage.BranchMediumId = model.BranchMediumId;
      requestMessage.ClassId = model.ClassId;
      requestMessage.Roll = model.Roll;
      requestMessage.BankAccountAccountHeadId = Convert.ToInt64(model.BankAccountAccountHeadId);
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _feeCollectionRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _feeCollectionResponsePresenter.Handle(response.Result, model, ModelState, userAssociationMessage);
      if (!response.Result.ValidationResult.IsValid)
      {
        viewModel.BranchMediumId = model.BranchMediumId;
        viewModel.FeeType = model.FeeType;
        viewModel.Sessions = GetSessions(model.BranchMediumId);
        viewModel.ClassId = model.ClassId;
        viewModel.SectionId = model.SectionId;
        viewModel.StudentName = model.StudentName;
        viewModel.ClassName = model.ClassName;
        viewModel.SectionName = model.SectionName;
        viewModel.SectionId = model.SectionId;
        viewModel.Roll = model.Roll;

        viewModel.LoginName = userAssociationMessage.LoginName;
        viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
        viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
        viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
        viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
        viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
        viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;
        viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
        return View(viewModel);
      }

      return RedirectToAction("ViewStudent", "Student",
        new {studentId = model.StudentId, branchMediumId = model.BranchMediumId});
    }

    [HttpPost]
    public JsonResult GetBankAccountAccountHeads(long branchMediumId, long sessionId, PaymentMethod paymentMethod)
    {
      if (branchMediumId > 0 && sessionId > 0 && paymentMethod == PaymentMethod.ByBank)
      {
        var cancellationToken = new CancellationToken();
        var requestMessage = new GetBankAccountAccountHeadsRequestMessage();
        requestMessage.SessionId = sessionId;
        requestMessage.BranchMediumId = branchMediumId;
        var response = _getBankAccountAccountHeadsRequestHandler.Handle(requestMessage, cancellationToken);
        var bankAccountAccountHeads = response.Result.BankAccountAccountHeadsList;
        return Json(bankAccountAccountHeads);
      }

      return Json(null);
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

    [HttpPost]
    public JsonResult GetStudentWaiver(long studentId, long sectionId, long sessionId, AcademicFeePeriodEnum feePeriod, 
      long classId, long branchMediumId)
    {
      var waivers = GetWaivers(studentId, sectionId, sessionId, feePeriod);
      if (waivers != null && waivers.Any())
        return Json(waivers);
      else
      {
        var cancellationToken = new CancellationToken();
        var requestMessage = new GetAcademicFeesRequestMessage();
        requestMessage.FeePeriod = feePeriod;
        requestMessage.ClassId = classId;
        requestMessage.SessionId = sessionId;
        requestMessage.BranchMediumId = branchMediumId;
        var response = _getAcademicFeesRequestHandler.Handle(requestMessage, cancellationToken);
        if (response.Result.AcademicFees.Any())
          return Json(response.Result.AcademicFees);
        if(classId > 0 && sessionId > 0 && branchMediumId > 0 && feePeriod > 0)
        {
          return Json(new {academicFeeNotAddedMsg = feePeriod.ToString("G") + " Academic Fee Not Added."});
        }

        return Json(new {disableCollectButton = true});
      }
    }

    private IEnumerable<StudentWaiverMessageModel> GetWaivers(long studentId, long sectionId,
      long sessionId, AcademicFeePeriodEnum feePeriod)
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

    private IEnumerable<SessionMessageModel> GetSessions(long branchMediumId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowSessionListRequestMessage();
      requestMessage.BranchMediumId = branchMediumId;
      var response = _showSessionListRequestHandler.Handle(requestMessage, cancellationToken);
      return response.Result.Sessions;
    }
  }
}