using NakedObjects;
using NakedObjects.Web.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SinePulse.SmartMeter.Server.Controllers
{
  public class TestElectricController : SystemControllerImpl
  {
    public TestElectricController(INakedObjectsFramework nakedObjectsFramework) : base(nakedObjectsFramework)
        {
      // Uncomment this if you wish to have NakedObject Container and services injected 
      //nakedObjectsFramework.ContainerInjector.InitDomainObject(this);
      nakedObjectsFramework.DomainObjectInjector.InjectInto(this);
    }
    // GET: TestElectric
    public ActionResult Index()
    {
      return View();
    }
  }
}