using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.CommitteeMemberPersonalInfoMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.CommitteeMemberPersonalInfos;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class CommitteeMemberPersonalInfoController : BaseController
  {
    private readonly AddCommitteeMemberPersonalInfoRequestHandler _addCommitteeMemberPersonalInfoRequestHandler;
    private readonly AddCommitteeMemberPersonalInfoResponsePresenter _presenter;
    private readonly GetCommitteeMemberPersonalInfoRequestHandler _getCommitteeMemberPersonalInfoRequestHandler;

    public CommitteeMemberPersonalInfoController(AddCommitteeMemberPersonalInfoRequestHandler addCommitteeMemberPersonalInfoRequestHandler,
      AddCommitteeMemberPersonalInfoResponsePresenter presenter,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler,
      GetCommitteeMemberPersonalInfoRequestHandler getCommitteeMemberPersonalInfoRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _addCommitteeMemberPersonalInfoRequestHandler = addCommitteeMemberPersonalInfoRequestHandler;
      _presenter = presenter;
      _getCommitteeMemberPersonalInfoRequestHandler = getCommitteeMemberPersonalInfoRequestHandler;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Institute, (int)Feature.InstituteEnum.AddCommitteeMemberPersonalInfo)]
    [HttpGet]
    public ActionResult AddCommitteeMemberPersonalInfo(long committeeMemberId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetCommitteeMemberPersonalInfoRequestMessage
      {
        CommitteeMemberId = committeeMemberId
      };
      var response = _getCommitteeMemberPersonalInfoRequestHandler.Handle(requestMessage, cancellationToken);
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();

      var viewModel = new AddCommitteeMemberPersonalInfoViewModel();
      viewModel.CommitteeMemberId = committeeMemberId;

      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;
      viewModel.Gender = response.Result.CommitteeMemberPersonalInfos.GenderEnum;
      viewModel.BloodGroup = response.Result.CommitteeMemberPersonalInfos.BloodGroupEnum;
      viewModel.Religion = response.Result.CommitteeMemberPersonalInfos.ReligionEnum;
      viewModel.FatherName = response.Result.CommitteeMemberPersonalInfos.FathersName;
      viewModel.MotherName = response.Result.CommitteeMemberPersonalInfos.MothersName;
      viewModel.SpouseName = response.Result.CommitteeMemberPersonalInfos.SpouseName;
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Institute, (int)Feature.InstituteEnum.AddCommitteeMemberPersonalInfo)]
    [HttpPost]
    public ActionResult AddCommitteeMemberPersonalInfo(AddCommitteeMemberPersonalInfoViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddCommitteeMemberPersonalInfoRequestMessage();

      requestMessage.BloodGroup = model.BloodGroup;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;
      requestMessage.FatherName = model.FatherName;
      requestMessage.Gender = model.Gender;
      requestMessage.MotherName = model.MotherName;
      requestMessage.Religion = model.Religion;
      requestMessage.SpouseName = model.SpouseName;
      requestMessage.CommitteeMemberId = model.CommitteeMemberId;

      var response = _addCommitteeMemberPersonalInfoRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _presenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (response.Result.ValidationResult.IsValid)
      {
        return Redirect("/CommitteeMember/ViewManagingCommittee?committeeMemberId=" + model.CommitteeMemberId + "#tab_personalInfo");
      }
      return View(viewModel);
    }

  }
}