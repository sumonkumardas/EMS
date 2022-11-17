using System.Threading;
using Microsoft.AspNetCore.Mvc;
using SinePulse.EMS.Messages.LoginMessages;
using SinePulse.EMS.Messages.LogoutMessages;
using SinePulse.EMS.Messages.RegisterMessages;
using SinePulse.EMS.Site.Models;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.Login;
using SinePulse.EMS.UseCases.Logout;
using SinePulse.EMS.UseCases.Register;

namespace SinePulse.EMS.Site.Controllers
{
  public class AccountController : Controller
  {
    private readonly DisplayLoginPageRequestHandler _displayLoginPageRequestHandler;
    private readonly DisplayLoginPageResponsePresenter _displayLoginPageResponsePresenter;
    private readonly LoginRequestHandler _loginRequestHandler;
    private readonly LogoutRequestHandler _logoutRequestHandler;
    private readonly DisplayRegisterPageRequestHandler _displayRegisterPageRequestHandler;
    private readonly DisplayRegisterPageResponsePresenter _displayRegisterPageResponsePresenter;
    private readonly RegisterResponsePresenter _registerResponsePresenter;

    private readonly RegisterRequestHandler _registerRequestHandler;


    public AccountController(
      DisplayLoginPageRequestHandler displayLoginPageRequestHandler,
      DisplayLoginPageResponsePresenter displayLoginPageResponsePresenter,
      LoginRequestHandler loginRequestHandler,
      LogoutRequestHandler logoutRequestHandler,
      DisplayRegisterPageRequestHandler displayRegisterPageRequestHandler,
      DisplayRegisterPageResponsePresenter displayRegisterPageResponsePresenter,
      RegisterResponsePresenter registerResponsePresenter,
      RegisterRequestHandler registerRequestHandler)
    {
      _displayLoginPageRequestHandler = displayLoginPageRequestHandler;
      _displayLoginPageResponsePresenter = displayLoginPageResponsePresenter;
      _loginRequestHandler = loginRequestHandler;
      _logoutRequestHandler = logoutRequestHandler;
      _displayRegisterPageRequestHandler = displayRegisterPageRequestHandler;
      _displayRegisterPageResponsePresenter = displayRegisterPageResponsePresenter;
      _registerResponsePresenter = registerResponsePresenter;
      _registerRequestHandler = registerRequestHandler;
    }

    [HttpGet]
    public ActionResult Login()
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new DisplayLoginPageRequestMessage();

      var model = new LoginViewModel();

      var response = _displayLoginPageRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _displayLoginPageResponsePresenter.Handle(response.Result, model, ModelState);

      return View(viewModel);
    }

    [HttpPost]
    public ActionResult Login(LoginViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new LoginRequestMessage();

      requestMessage.UserName = model.UserName;
      requestMessage.Password = model.Password;

      var response = _loginRequestHandler.Handle(requestMessage, cancellationToken);
      if (response.Result.ValidationResult.IsValid && response.Result.Data.IsSuccess)
      {
        return RedirectToAction("Index", "Home");
      }

      return View(model);
    }

    public ActionResult Logout()
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new LogoutRequestMessage();
      _logoutRequestHandler.Handle(requestMessage, cancellationToken);
      return RedirectToAction("Index", "Home");
    }
    
    [HttpGet]
    public ActionResult Register()
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new DisplayRegisterPageRequestMessage();

      var model = new RegisterViewModel();

      var response = _displayRegisterPageRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _displayRegisterPageResponsePresenter.Handle(response.Result, model, ModelState);

      return View(viewModel);
    }
    
    [HttpPost]
    public ActionResult Register(RegisterViewModel model)
    {
      var cancellationToken = new CancellationToken();
      var requestMessage = new RegisterRequestMessage();

      requestMessage.FullName = model.FullName;
      requestMessage.DOB = model.DOB;
      requestMessage.JoiningDate = model.JoiningDate;
      requestMessage.UserName = model.UserName;
      requestMessage.Password = model.Password;

      var response = _registerRequestHandler.Handle(requestMessage, cancellationToken);
      var viewModel = _registerResponsePresenter.Handle(response.Result, model, ModelState);
      if (!response.Result.ValidationResult.IsValid || !response.Result.Data.IsSuccess)
      {
        return View(model);
      }
      return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public ActionResult AccessDenied()
    {
      return View();
    }
  }
}