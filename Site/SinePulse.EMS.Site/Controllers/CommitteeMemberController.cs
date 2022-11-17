using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.CommitteeMemberAddressMessages;
using SinePulse.EMS.Messages.CommitteeMemberMessages;
using SinePulse.EMS.Messages.CommitteeMemberPersonalInfoMessages;
using SinePulse.EMS.Messages.DesignationMessages;
using SinePulse.EMS.Messages.Model.Shared;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.CommitteeMemberAddresses;
using SinePulse.EMS.UseCases.CommitteeMemberPersonalInfos;
using SinePulse.EMS.UseCases.CommitteeMembers;
using SinePulse.EMS.UseCases.Designations;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class CommitteeMemberController : BaseController
  {
    private readonly ShowDesignationListRequestHandler _showDesignationListRequestHandler;
    private readonly AddCommitteeMemberRequestHandler _addCommitteeMemberRequestHandler;
    private readonly AddCommitteeMemberResponsePresenter _addCommitteeMemberResponsePresenter;
    private readonly ShowCommitteeMemberRequestHandler _showCommitteeMemberRequestHandler;
    private readonly ShowCommitteeMemberResponsePresenter _showCommitteeMemberResponsePresenter;
    private readonly IHostingEnvironment _appEnvironment;
    private readonly AddOrChangeCommitteeMemberImageRequestHandler _addOrChangeCommitteeMemberImageRequestHandler;
    private readonly GetCommitteeMemberAddressRequestHandler _getCommitteeMemberAddressRequestHandler;
    private readonly AddCommitteeMemberAddressRequestHandler _addCommitteeMemberAddressRequestHandler;
    private readonly AddCommitteeMemberAddressResponsePresenter _addCommitteeMemberAddressResponsePresenter;
    private readonly ShowCommitteeMemberAddressResponsePresenter _showCommitteeMemberAddressResponsePresenter;
    private readonly GetCommitteeMemberPersonalInfoRequestHandler _getCommitteeMemberPersonalInfoRequestHandler;
    private readonly ShowCommitteeMemberPersonalInfoResponsePresenter _showCommitteeMemberPersonalInfoResponsePresenter;
    public CommitteeMemberController(ShowDesignationListRequestHandler showDesignationListRequestHandler,
      AddCommitteeMemberRequestHandler addCommitteeMemberRequestHandler,
      AddCommitteeMemberResponsePresenter addCommitteeMemberResponsePresenter, 
      ShowCommitteeMemberRequestHandler showCommitteeMemberRequestHandler,
      ShowCommitteeMemberResponsePresenter showCommitteeMemberResponsePresenter, 
      IHostingEnvironment appEnvironment, 
      AddOrChangeCommitteeMemberImageRequestHandler addOrChangeCommitteeMemberImageRequestHandler,
      GetCommitteeMemberAddressRequestHandler getCommitteeMemberAddressRequestHandler,
      AddCommitteeMemberAddressRequestHandler addCommitteeMemberAddressRequestHandler,
      AddCommitteeMemberAddressResponsePresenter addCommitteeMemberAddressResponsePresenter,
      ShowCommitteeMemberAddressResponsePresenter showCommitteeMemberAddressResponsePresenter,
      GetCommitteeMemberPersonalInfoRequestHandler getCommitteeMemberPersonalInfoRequestHandler,
      ShowCommitteeMemberPersonalInfoResponsePresenter showCommitteeMemberPersonalInfoResponsePresenter,
       GetUserAssociationRequestHandler getUserAssociationRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _showDesignationListRequestHandler = showDesignationListRequestHandler;
      _addCommitteeMemberRequestHandler = addCommitteeMemberRequestHandler;
      _addCommitteeMemberResponsePresenter = addCommitteeMemberResponsePresenter;
      _showCommitteeMemberRequestHandler = showCommitteeMemberRequestHandler;
      _showCommitteeMemberResponsePresenter = showCommitteeMemberResponsePresenter;
      _appEnvironment = appEnvironment;
      _addOrChangeCommitteeMemberImageRequestHandler = addOrChangeCommitteeMemberImageRequestHandler;
      _getCommitteeMemberAddressRequestHandler = getCommitteeMemberAddressRequestHandler;
      _addCommitteeMemberAddressRequestHandler = addCommitteeMemberAddressRequestHandler;
      _addCommitteeMemberAddressResponsePresenter = addCommitteeMemberAddressResponsePresenter;
      _showCommitteeMemberAddressResponsePresenter = showCommitteeMemberAddressResponsePresenter;
      _getCommitteeMemberPersonalInfoRequestHandler = getCommitteeMemberPersonalInfoRequestHandler;
      _showCommitteeMemberPersonalInfoResponsePresenter = showCommitteeMemberPersonalInfoResponsePresenter;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Institute, (int)Feature.InstituteEnum.ViewCommitteeMember)]
    [HttpGet]
    public ActionResult ViewManagingCommittee(long committeeMemberId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowCommitteeMemberRequestMessage
      {
        CommitteeMemberId = committeeMemberId
      };
      var model = new ShowCommitteeMemberViewModel();
      var response = _showCommitteeMemberRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showCommitteeMemberResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Institute, (int)Feature.InstituteEnum.NewCommitteeMember)]
    [HttpGet]
    public ActionResult AddManagingCommitteeMember(long instituteId)
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var viewModel = new AddCommitteeMemberViewModel();
      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      ((BaseViewModel)viewModel).LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;

      viewModel.InstituteId = instituteId;
      viewModel.Designations = GetDesignationsByEmployeeType(EmployeeTypeEnum.Committee);
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Institute, (int)Feature.InstituteEnum.NewCommitteeMember)]
    [HttpPost]
    public ActionResult AddManagingCommitteeMember(AddCommitteeMemberViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddCommitteeMemberRequestMessage();

      requestMessage.InstituteId = model.InstituteId;
      requestMessage.DesignationId = model.DesignationId;
      requestMessage.FullName = model.FullName;
      requestMessage.DOB = model.DOB;
      requestMessage.MobileNo = model.MobileNo;
      requestMessage.EmailAddress = model.EmailAddress;
      requestMessage.NationalId = model.NationalId;
      requestMessage.Status = StatusEnum.Active;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addCommitteeMemberRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _addCommitteeMemberResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      if (!response.Result.ValidationResult.IsValid)
      {
        viewModel.Designations = GetDesignationsByEmployeeType(EmployeeTypeEnum.Committee);
        viewModel.InstituteId = model.InstituteId;
        return View(viewModel);
      }

      return RedirectToAction("ViewInstitute", "Institute", new {instituteId = model.InstituteId, activeTab = TabEnum.CommitteeMembers});
    }

    public IEnumerable<DesignationMessageModel> GetDesignationsByEmployeeType(EmployeeTypeEnum employeeType)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowDesignationListRequestMessage();
      requestMessage.EmployeeType = employeeType;
      var model = new List<DesignationViewModel>();

      var designationList = _showDesignationListRequestHandler.Handle(requestMessage, cancellationToken).Result
        .DesignationList;
      return designationList;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Institute, (int)Feature.InstituteEnum.NewCommitteeMember)]
    [HttpGet]
    public ActionResult UploadCommitteeMemberImage(long committeeMemberId)
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();

      var viewModel = new AddOrChangeCommitteeMemberImageViewModel();
      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;

      viewModel.CommitteeMemberId = committeeMemberId;
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Institute, (int)Feature.InstituteEnum.NewCommitteeMember)]
    [HttpPost]
    public ActionResult UploadCommitteeMemberImage(AddOrChangeCommitteeMemberImageViewModel model)
    {
      var files = HttpContext.Request.Form.Files;
      foreach (var image in files)
      {
        if (image != null && image.Length > 0)
        {
          var file = image;
          var uploads = Path.Combine(_appEnvironment.WebRootPath, "Uploads\\CommitteeMember\\");
          if (!Directory.Exists(uploads))
            Directory.CreateDirectory(uploads);
          if (file.Length > 0)
          {
            var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
            {
              file.CopyTo(fileStream);
              var cancellationToken = new CancellationToken();
              var requestMessage = new AddOrChangeCommitteeMemberImageRequestMessage();
              requestMessage.CommitteeMemberId = model.CommitteeMemberId;
              requestMessage.ImageUrl = fileName;
              requestMessage.CurrentUserName = HttpContext.User.Identity.Name;
              var response = _addOrChangeCommitteeMemberImageRequestHandler.Handle(requestMessage, cancellationToken);
              if (!string.IsNullOrEmpty(response.Result.PreviousImage))
              {
                var previousImageFile = Path.Combine(_appEnvironment.WebRootPath, "Uploads\\CommitteeMember\\") + response.Result.PreviousImage;
                if(System.IO.File.Exists(previousImageFile)){ System.IO.File.Delete(previousImageFile);}
              }
            }
          }
        }
      }
      return RedirectToAction("ViewManagingCommittee", new { committeeMemberId = model.CommitteeMemberId});
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Institute, (int)Feature.InstituteEnum.AddCommitteeMemberAddress)]
    [HttpGet]
    public ActionResult AddCommitteeMemberAddress(long committeeMemberId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetCommitteeMemberAddressRequestMessage
      {
        CommitteeMemberId = committeeMemberId
      };
      var response = _getCommitteeMemberAddressRequestHandler.Handle(requestMessage, cancellationToken);
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var model = new AddCommitteeMemberAddressViewModel
      {
        LoginName = userAssociationMessage.LoginName,
        AssociatedWith = userAssociationMessage.AssociatedWith,
        LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId,
        LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId,
        LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId,
        RoleFeatures = userAssociationMessage.RoleFeatures,
        InstituteBanner = userAssociationMessage.InstituteBanner,
        PresentAddressCity = response.Result.Address.PresentCity,
        PresentAddressPostalCode = response.Result.Address.PresentPostalCode,
        PresentAddressStreet1 = response.Result.Address.PresentStreet1,
        PresentAddressStreet2 = response.Result.Address.PresentStreet2,
        PermanentAddressCity = response.Result.Address.PermanentCity,
        PermanentAddressPostalCode = response.Result.Address.PermanentPostalCode,
        PermanentAddressStreet1 = response.Result.Address.PermanentStreet1,
        PermanentAddressStreet2 = response.Result.Address.PermanentStreet2

      };

      model.CommitteeMemberId = committeeMemberId;

      return View(model);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Institute, (int)Feature.InstituteEnum.AddCommitteeMemberAddress)]
    [HttpPost]
    public ActionResult AddCommitteeMemberAddress(AddCommitteeMemberAddressViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddCommitteeMemberAddressRequestMessage();
      requestMessage.CommitteeMemberId = model.CommitteeMemberId;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;
      requestMessage.PermanentAddressCity = model.PermanentAddressCity;
      requestMessage.PermanentAddressPostalCode = model.PermanentAddressPostalCode;
      requestMessage.PermanentAddressStreet1 = model.PermanentAddressStreet1;
      requestMessage.PermanentAddressStreet2 = model.PermanentAddressStreet2;
      requestMessage.PresentAddressCity = model.PresentAddressCity;
      requestMessage.PresentAddressPostalCode = model.PresentAddressPostalCode;
      requestMessage.PresentAddressStreet1 = model.PresentAddressStreet1;
      requestMessage.PresentAddressStreet2 = model.PresentAddressStreet2;
      requestMessage.SameAsPermanentAddress = model.SameAsPermanentAddress;

      var response = _addCommitteeMemberAddressRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _addCommitteeMemberAddressResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());

      if (response.Result.ValidationResult.IsValid)
      {
        return Redirect("/CommitteeMember/ViewManagingCommittee?committeeMemberId=" + model.CommitteeMemberId + "#tab_address");
      }

      viewModel.CommitteeMemberId = model.CommitteeMemberId;
      return View(viewModel);

    }

    public ActionResult LoadCommitteeMemberAddressPartialView(long committeeMemberId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetCommitteeMemberAddressRequestMessage
      {
        CommitteeMemberId = committeeMemberId
      };
      var response = _getCommitteeMemberAddressRequestHandler.Handle(requestMessage, cancellationToken);
      var committeeMemberAddressModel = _showCommitteeMemberAddressResponsePresenter.Handle(response.Result, new AddressViewModel());

      return PartialView("CommitteeMemberAddress", committeeMemberAddressModel);
    }

    public ActionResult LoadCommitteeMemberPersonalInfoPartialView(long committeeMemberId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new GetCommitteeMemberPersonalInfoRequestMessage
      {
        CommitteeMemberId = committeeMemberId
      };
      var response = _getCommitteeMemberPersonalInfoRequestHandler.Handle(requestMessage, cancellationToken);
      var committeeMemberPersonalInfoModel = _showCommitteeMemberPersonalInfoResponsePresenter.Handle(response.Result,
        new CommitteeMemberPersonalInfoViewModel());

      return PartialView("CommitteeMemberPersonalInfo", committeeMemberPersonalInfoModel);
    }
  }
}