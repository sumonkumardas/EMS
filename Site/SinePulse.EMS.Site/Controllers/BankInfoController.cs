using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Domain.Features;
using SinePulse.EMS.Messages.AuthorizationMessages;
using SinePulse.EMS.Messages.BankAccountMessages;
using SinePulse.EMS.Messages.BankBranchMessages;
using SinePulse.EMS.Messages.BankMessages;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.BankAccounts;
using SinePulse.EMS.UseCases.BankBranches;
using SinePulse.EMS.UseCases.Banks;

namespace SinePulse.EMS.Site.Controllers
{
  [Authorize]
  public class BankInfoController : BaseController
  {
    private readonly AddBankRequestHandler _addBankRequestHandler;
    private readonly AddBankInfoResponsePresenter _addBankInfoResponsePresenter;
    private readonly AddBankBranchRequestHandler _addBankBranchRequestHandler;
    private readonly AddBankBranchResponsePresenter _addBankBranchResponsePresenter;
    private readonly ShowBankRequestHandler _showBankRequestHandler;
    private readonly ShowBankInfoResponsePresenter _showBankInfoResponsePresenter;
    private readonly ShowBankBranchRequestHandler _showBankBranchRequestHandler;
    private readonly ShowBankBranchResponsePresenter _showBankBranchResponsePresenter;
    private readonly AddBankAccountRequestHandler _addBankAccountRequestHandler;
    private readonly AddBankAccountResponsePresenter _addBankAccountResponsePresenter;
    private readonly EditBankRequestHandler _editBankRequestHandler;
    private readonly EditBankResponsePresenter _editBankResponsePresenter;
    private readonly EditBankBranchRequestHandler _editBankBranchRequestHandler;
    private readonly EditBankBranchResponsePresenter _editBankBranchResponsePresenter;
    private readonly ShowBankAccountRequestHandler _showBankAccountRequestHandler;
    private readonly EditBankAccountRequestHandler _editBankAccountRequestHandler;
    private readonly EditBankAccountResponsePresenter _editBankAccountResponsePresenter;

    public BankInfoController(AddBankRequestHandler addBankRequestHandler,
      AddBankInfoResponsePresenter addBankInfoResponsePresenter,
      AddBankBranchRequestHandler addBankBranchRequestHandler,
      AddBankBranchResponsePresenter addBankBranchResponsePresenter, 
      ShowBankRequestHandler showBankRequestHandler, 
      ShowBankInfoResponsePresenter showBankInfoResponsePresenter, 
      ShowBankBranchRequestHandler showBankBranchRequestHandler, 
      ShowBankBranchResponsePresenter showBankBranchResponsePresenter,
      GetUserAssociationRequestHandler getUserAssociationRequestHandler,
      AddBankAccountRequestHandler addBankAccountRequestHandler, 
      AddBankAccountResponsePresenter addBankAccountResponsePresenter, 
      EditBankRequestHandler editBankRequestHandler, 
      EditBankResponsePresenter editBankResponsePresenter, 
      EditBankBranchRequestHandler editBankBranchRequestHandler, 
      EditBankBranchResponsePresenter editBankBranchResponsePresenter, 
      ShowBankAccountRequestHandler showBankAccountRequestHandler, 
      EditBankAccountRequestHandler editBankAccountRequestHandler, 
      EditBankAccountResponsePresenter editBankAccountResponsePresenter) : base(getUserAssociationRequestHandler)
    {
      _addBankRequestHandler = addBankRequestHandler;
      _addBankInfoResponsePresenter = addBankInfoResponsePresenter;
      _addBankBranchRequestHandler = addBankBranchRequestHandler;
      _addBankBranchResponsePresenter = addBankBranchResponsePresenter;
      _showBankRequestHandler = showBankRequestHandler;
      _showBankInfoResponsePresenter = showBankInfoResponsePresenter;
      _showBankBranchRequestHandler = showBankBranchRequestHandler;
      _showBankBranchResponsePresenter = showBankBranchResponsePresenter;
      _addBankAccountRequestHandler = addBankAccountRequestHandler;
      _addBankAccountResponsePresenter = addBankAccountResponsePresenter;
      _editBankRequestHandler = editBankRequestHandler;
      _editBankResponsePresenter = editBankResponsePresenter;
      _editBankBranchRequestHandler = editBankBranchRequestHandler;
      _editBankBranchResponsePresenter = editBankBranchResponsePresenter;
      _showBankAccountRequestHandler = showBankAccountRequestHandler;
      _editBankAccountRequestHandler = editBankAccountRequestHandler;
      _editBankAccountResponsePresenter = editBankAccountResponsePresenter;
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.BankInfo, (int)Feature.BankInfoEnum.AddBankInfo)]
    [HttpGet]
    public ActionResult AddBank(long branchMediumId)
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      return View(new AddBankViewModel
      {
        BranchMediumId = branchMediumId,
        LoginUsersBranchMediumId = branchMediumId,
        LoginName = userAssociationMessage.LoginName,
        AssociatedWith = userAssociationMessage.AssociatedWith,
        LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId,
        LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId,
        RoleFeatures = userAssociationMessage.RoleFeatures
      });
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.BankInfo, (int)Feature.BankInfoEnum.AddBankInfo)]
    [HttpPost]
    public ActionResult AddBank(AddBankViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddBankRequestMessage();

      requestMessage.BranchMediumId = (long)model.BranchMediumId;
      requestMessage.BankName = model.BankName;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addBankRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _addBankInfoResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      viewModel.LoginUsersBranchMediumId = model.LoginUsersBranchMediumId;
      if (!response.Result.ValidationResult.IsValid)
        return View(viewModel);
      return Redirect("/BranchMedium/ViewBranchMedium?branchMediumId=" + model.BranchMediumId + "#tab_bank");
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.BankInfo, (int)Feature.BankInfoEnum.ViewBankInfo)]
    [HttpGet]
    public ActionResult ViewBankInfo(long bankId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowBankRequestMessage();
      var model = new ShowBankViewModel();
      requestMessage.BankId = bankId;

      var response = _showBankRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showBankInfoResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.BankInfo, (int)Feature.BankInfoEnum.ViewBankBranch)]
    [HttpGet]
    public ActionResult ViewBankBranch(long branchId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowBankBranchRequestMessage
      {
        BankBranchId = branchId
      };
      var model = new ShowBankBranchViewModel();
      var response = _showBankBranchRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _showBankBranchResponsePresenter.Handle(response.Result, model, GetUserAssociationResponseMessage());
      return View(viewModel);
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.BankInfo, (int)Feature.BankInfoEnum.AddBankBranch)]
    [HttpGet]
    public ActionResult AddBankBranch(long bankId)
    {
      GetUserAssociationResponseMessage userAssociationMessage = GetUserAssociationResponseMessage();
      return View(new AddBankBranchViewModel
      {
        BankId = bankId,
        LoginName = userAssociationMessage.LoginName,
        AssociatedWith = userAssociationMessage.AssociatedWith,
        LoginUsersInstituteId = userAssociationMessage.LoginUsersInstituteId,
        LoginUsersBranchId = userAssociationMessage.LoginUsersBranchId,
        LoginUsersBranchMediumId = userAssociationMessage.LoginUsersBranchMediumId,
        RoleFeatures = userAssociationMessage.RoleFeatures
      });
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.BankInfo, (int)Feature.BankInfoEnum.AddBankBranch)]
    [HttpPost]
    public ActionResult AddBankBranch(AddBankBranchViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddBankBranchRequestMessage();

      requestMessage.BankId = model.BankId;
      requestMessage.BranchName = model.BranchName;
      requestMessage.Address = model.Address;
      requestMessage.CurrentUserName = HttpContext.User.Identity.Name;

      var response = _addBankBranchRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _addBankBranchResponsePresenter.Handle(response.Result, model, ModelState, GetUserAssociationResponseMessage());
      viewModel.BankId = model.BankId;

      if (!response.Result.ValidationResult.IsValid)
        return View(viewModel);
      
      return RedirectToAction("ViewBankInfo", new { bankId = model.BankId });
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.BankInfo, (int)Feature.BankInfoEnum.AddAccountNumber)]
    [HttpGet]
    public ActionResult AddBankAccount(long bankBranchId)
    {
      return View(new AddBankAccountViewModel
      {
        BankBranchId = bankBranchId
      });
    }

    [FeatureAuthorize(FeatureType.FeatureTypeEnum.BankInfo, (int)Feature.BankInfoEnum.AddAccountNumber)]
    [HttpPost]
    public ActionResult AddBankAccount(AddBankAccountViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new AddBankAccountRequestMessage
      {
        BankBranchId = model.BankBranchId,
        AccountType = model.AccountType,
        AccountNumber = model.AccountNumber,
        CurrentUserName = HttpContext.User.Identity.Name
      };

      var response = _addBankAccountRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _addBankAccountResponsePresenter.Handle(response.Result, model, ModelState);
      viewModel.BankBranchId = model.BankBranchId;

      if (!response.Result.ValidationResult.IsValid)
      {
        viewModel.BankBranchId = model.BankBranchId;
        return View(viewModel);
      }
      
      return RedirectToAction("ViewBankBranch", new { branchId = model.BankBranchId });
    }

    [HttpGet]
    public ActionResult UpdateBank(long bankId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowBankRequestMessage {BankId = bankId};
      var editBankViewModel = new EditBankViewModel();
      var response = _showBankRequestHandler.Handle(requestMessage, cancellationToken);
      editBankViewModel.BankId = bankId;
      editBankViewModel.BankName = response.Result.Bank.BankName;
      editBankViewModel.BranchMediumId = response.Result.Bank.BranchMedium.Id;
      
      return View(editBankViewModel);
    }

    [HttpPost]
    public ActionResult UpdateBank(EditBankViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditBankRequestMessage
      {
        BankId = model.BankId,
        BankName = model.BankName, 
        CurrentUserName = HttpContext.User.Identity.Name
      };

      var response = _editBankRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _editBankResponsePresenter.Handle(response.Result, model, ModelState, 
        GetUserAssociationResponseMessage());

      if (!response.Result.ValidationResult.IsValid)
      {
        viewModel.BranchMediumId = model.BranchMediumId;
        return View(viewModel);
      }

      return Redirect("/BranchMedium/ViewBranchMedium?branchMediumId="+model.BranchMediumId+"#tab_bank");
    }

    [HttpGet]
    public ActionResult UpdateBankBranch(long bankBranchId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowBankBranchRequestMessage {BankBranchId = bankBranchId};
      var editBankBranchViewModel = new EditBankBranchViewModel();
      var response = _showBankBranchRequestHandler.Handle(requestMessage, cancellationToken);
      editBankBranchViewModel.BankBranchId = bankBranchId;
      editBankBranchViewModel.BranchName = response.Result.BankBranch.BranchName;
      editBankBranchViewModel.Address = response.Result.BankBranch.Address;
      editBankBranchViewModel.BankId = response.Result.BankBranch.Bank.Id;
      return View(editBankBranchViewModel);
    }
    
    [HttpPost]
    public ActionResult UpdateBankBranch(EditBankBranchViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditBankBranchRequestMessage
      {
        BankBranchId = model.BankBranchId,
        BranchName = model.BranchName,
        Address = model.Address,
        CurrentUserName = HttpContext.User.Identity.Name
      };

      var response = _editBankBranchRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _editBankBranchResponsePresenter.Handle(response.Result, model, ModelState, 
        GetUserAssociationResponseMessage());

      if (!response.Result.ValidationResult.IsValid)
      {
        viewModel.BankBranchId = model.BankBranchId;
        viewModel.BankId = model.BankId;
        return View(viewModel);
      }

      return RedirectToAction("ViewBankInfo", new { bankId = model.BankId});
    }
    
    [HttpGet]
    public ActionResult UpdateBankAccount(long bankAccountId)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new ShowBankAccountRequestMessage {BankAccountId = bankAccountId};
      var editBankAccountViewModel = new EditBankAccountViewModel();
      var response = _showBankAccountRequestHandler.Handle(requestMessage, cancellationToken);
      editBankAccountViewModel.BankAccountId = bankAccountId;
      editBankAccountViewModel.AccountType = response.Result.AccountInfos.AccountType;
      editBankAccountViewModel.AccountNumber = response.Result.AccountInfos.AccountNo;
      editBankAccountViewModel.BankBranchId = response.Result.AccountInfos.BankBranchId;
      return View(editBankAccountViewModel);
    }
    
    [HttpPost]
    public ActionResult UpdateBankAccount(EditBankAccountViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new EditBankAccountRequestMessage
      {
        BankBranchId = model.BankBranchId,
        BankAccountId = model.BankAccountId,
        AccountNumber = model.AccountNumber,
        AccountType = model.AccountType,
        CurrentUserName = HttpContext.User.Identity.Name
      };

      var response = _editBankAccountRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _editBankAccountResponsePresenter.Handle(response.Result, model, ModelState, 
        GetUserAssociationResponseMessage());

      if (!response.Result.ValidationResult.IsValid)
      {
        viewModel.BankBranchId = model.BankBranchId;
        viewModel.BankAccountId = model.BankAccountId;
        return View(viewModel);
      }

      return RedirectToAction("ViewBankBranch", new { branchId = model.BankBranchId});
    }
    
  }                                
}