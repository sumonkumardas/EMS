using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Enums;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.ContactInfoMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.ContactInfos;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class ContactInfoController : BaseController
  {
    private readonly AddContactInfoRequestHandler _addContactInfoRequestHandler;
    private readonly AddContactInfoResponsePresenter _presenter;
    private readonly ShowContactInfoRequestHandler _showContactInfoRequestHandler;
    private readonly ShowContactInfoResponsePresenter _showContactInfoResponsePresenter;
    private readonly AddPresentAddressInContactInfoRequestHandler _addPresentAddressInContactInfoRequestHandler;
    private readonly AddPresentAddressInContactInfoResponsePresenter _addPresentAddressInContactInfoResponsePresenter;
    private readonly AddPermanentAddressInContactInfoRequestHandler _addPermanentAddressInContactInfoRequestHandler;
    private readonly AddPermanentAddressInContactInfoResponsePresenter _addPermanentAddressInContactInfoResponsePresenter;

    public ContactInfoController(
      AddContactInfoRequestHandler addContactInfoRequestHandler, 
      AddContactInfoResponsePresenter presenter, 
      ShowContactInfoRequestHandler showContactInfoRequestHandler, 
      ShowContactInfoResponsePresenter showContactInfoResponsePresenter, 
      AddPresentAddressInContactInfoRequestHandler addPresentAddressInContactInfoRequestHandler, 
      AddPresentAddressInContactInfoResponsePresenter addPresentAddressInContactInfoResponsePresenter, 
      AddPermanentAddressInContactInfoRequestHandler addPermanentAddressInContactInfoRequestHandler, 
      AddPermanentAddressInContactInfoResponsePresenter addPermanentAddressInContactInfoResponsePresenter,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler) : base(getUserAssociationRequestHandler)
    {
      _addContactInfoRequestHandler = addContactInfoRequestHandler;
      _presenter = presenter;
      _showContactInfoRequestHandler = showContactInfoRequestHandler;
      _showContactInfoResponsePresenter = showContactInfoResponsePresenter;
      _addPresentAddressInContactInfoRequestHandler = addPresentAddressInContactInfoRequestHandler;
      _addPresentAddressInContactInfoResponsePresenter = addPresentAddressInContactInfoResponsePresenter;
      _addPermanentAddressInContactInfoRequestHandler = addPermanentAddressInContactInfoRequestHandler;
      _addPermanentAddressInContactInfoResponsePresenter = addPermanentAddressInContactInfoResponsePresenter;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Student, (int)Feature.StudentEnum.AddContactInfo)]
    [HttpGet]
    public ActionResult AddContactInfo(long studentId, long branchMediumId)
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();

      var viewModel = new AddContactInfoViewModel();
      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;

      viewModel.StudentId = studentId;
      viewModel.LoginUsersBranchMediumId = branchMediumId;
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Student, (int)Feature.StudentEnum.AddContactInfo)]
    [HttpPost]
    public ActionResult AddContactInfo(AddContactInfoViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddContactInfoRequestMessage();

      requestMessage.StudentId = model.StudentId;
      requestMessage.PhoneNo = model.PhoneNo;
      requestMessage.EmailAddress = model.EmailAddress;
      requestMessage.Status = StatusEnum.Active;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addContactInfoRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _presenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      if (response.Result.ValidationResult.IsValid)
        return RedirectToAction("ViewStudent", "Student", new { studentId = model.StudentId, branchMediumId = model.LoginUsersBranchMediumId });
      viewModel.StudentId = model.StudentId;
      viewModel.LoginUsersBranchMediumId = model.LoginUsersBranchMediumId;
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Student, (int)Feature.StudentEnum.ViewContactInfo)]
    [HttpGet]
    public ActionResult ViewContactInfo(long contactInfoId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowContactInfoRequestMessage();
      var model = new ContactInfoViewModel();
      requestMessage.ContactInfoId = contactInfoId;
      var response = _showContactInfoRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showContactInfoResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());

      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Student, (int)Feature.StudentEnum.AddAddress)]
    [HttpGet]
    public ActionResult AddPresentAddressInContactInfo(long contactInfoId)
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var viewModel = new AddPresentAddressInContactInfoViewModel();
      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;

      viewModel.ContactInfoId = contactInfoId;
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Student, (int)Feature.StudentEnum.AddAddress)]
    [HttpPost]
    public ActionResult AddPresentAddressInContactInfo(AddPresentAddressInContactInfoViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddPresentAddressInContactInfoRequestMessage();

      requestMessage.ContactInfoId = model.ContactInfoId;
      requestMessage.Street1 = model.Street1;
      requestMessage.Street2 = model.Street2;
      requestMessage.PostalCode = model.PostalCode;
      requestMessage.City = model.City;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addPresentAddressInContactInfoRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _addPresentAddressInContactInfoResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      if (response.Result.ValidationResult.IsValid)
        return RedirectToAction("ViewContactInfo", new { contactInfoId = response.Result.ContactInfoId });
      viewModel.ContactInfoId = model.ContactInfoId;
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Student, (int)Feature.StudentEnum.AddAddress)]
    [HttpGet]
    public ActionResult AddPermanentAddressInContactInfo(long contactInfoId)
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      var viewModel = new AddPermanentAddressInContactInfoViewModel();
      viewModel.LoginName = userAssociationMessage.LoginName;
      viewModel.LoginImageUrl = userAssociationMessage.ImageUrl;
      viewModel.AssociatedWith = userAssociationMessage.AssociatedWith;
      viewModel.LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId;
      viewModel.LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId;
      viewModel.LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId;
      viewModel.RoleFeatures = userAssociationMessage.RoleFeatures;
      viewModel.InstituteBanner = userAssociationMessage.InstituteBanner;

      viewModel.ContactInfoId = contactInfoId;
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.Student, (int)Feature.StudentEnum.AddAddress)]
    [HttpPost]
    public ActionResult AddPermanentAddressInContactInfo(AddPermanentAddressInContactInfoViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddPermanentAddressInContactInfoRequestMessage();

      requestMessage.ContactInfoId = model.ContactInfoId;
      requestMessage.Street1 = model.Street1;
      requestMessage.Street2 = model.Street2;
      requestMessage.PostalCode = model.PostalCode;
      requestMessage.City = model.City;
      requestMessage.IsSameAsPresentAddress = model.SameAsPresentAddress;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addPermanentAddressInContactInfoRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _addPermanentAddressInContactInfoResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      if (response.Result.ValidationResult.IsValid)
        return RedirectToAction("ViewContactInfo", new { contactInfoId = response.Result.ContactInfoId });
      viewModel.ContactInfoId = model.ContactInfoId;
      return View(viewModel);
    }
  }
}