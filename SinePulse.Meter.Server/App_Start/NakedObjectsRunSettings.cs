// Copyright Naked Objects Group Ltd, 45 Station Road, Henley on Thames, UK, RG9 1AT
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.

using System;
using NakedObjects.Core.Configuration;
using NakedObjects.Persistor.Entity.Configuration;
using NakedObjects.Architecture.Menu;
using NakedObjects.Menu;
using NakedObjects.Meta.Audit;
using NakedObjects.Meta.Authorization;
using NakedObjects.Meta.Profile;
using NakedObjects.Web.Mvc.Models;
using System.Linq;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Core.Objects;
using NakedObjects.Reflect.FacetFactory;
using SinePulse.EMS.NO.Domain.Repositories;
using SinePulse.EMS.NO.Domain.Enums;
using SinePulse.EMS.NO.Domain.Shared;
using SinePulse.EMS.ProjectPrimitives;
using SinePulse.EMS.NO.Domain.Employees;
using SinePulse.EMS.NO.Domain.Banks;
using SinePulse.EMS.NO.Domain.Facade;
using SinePulse.EMS.NO.Domain.Academic;
using SinePulse.EMS.NO.Domain.UserManagement;
using SinePulse.EMS.NO.Domain.Features;

namespace SinePulse.SmartMeter.Server
{

  // Use this class to configure the application running under Naked Objects
  public class NakedObjectsRunSettings
  {
    public static string RestRoot
    {
      get { return null; }
    }

    private static string[] ModelNamespaces
    {
      get
      {
        return new string[] { "SinePulse.EMS.NO.Domain" }; //Add top-level namespace(s) that cover the domain model
      }
    }

    private static Type[] Services
    {
      get
      {
        return new Type[]
        {
          typeof (InstituteManagementRepository),
          typeof (SetupRepository),
          typeof (EmployeeRepository),
          typeof (UserManagementRepository),
          typeof (LoggedInUserInfoRepository),
          typeof (FinanceRepository),
          typeof (StudentRepository)
        };
      }
    }

    private static Type[] Types
    {
      get
      {
        return new Type[]
        {
          //These types must be registered because they are defined in
          //NakedObjects.Mvc, not in Core.
          typeof (ActionResultModelQ<>),
          typeof (ActionResultModel<>),
          typeof (PropertyViewModel),
          typeof (FindViewModel),
          typeof (StatusEnum),
          typeof (AuditFields),
          typeof (WeekDays),
          typeof (StatusEnum),
          typeof (GroupEnum),
          typeof (ResultTypeEnum),
          typeof (EmployeeTypeEnum),
          typeof (GenderEnum),
          typeof (ReligionEnum),
          typeof (BloodGroupEnum),
          typeof (EducationDegreeEnum),
          typeof (NOEmployeePersonalInfo),
          typeof (NOBankInfo),
          typeof (AccountPeriodEnum),
          typeof (AccountHeadCategoryEnum),
          typeof (AccountHeadTypeEnum),
          typeof (ObjectTypeEnum),
          typeof (SassionwiseDataEnum),
          typeof (ExamTypeEnum),
          typeof (NOBreakTime),
          typeof (RelationTypeEnum),
          typeof (AccountTransactionTypeEnum),
          typeof (IncreaseDecreaseEnum),
          typeof (ChartOfAccountTypeEnum),
          typeof (FinancialStatementsEnum),
          typeof (VoucherTypeEnum),
          typeof (NOBankAccountHead)
          //Add any domain types that cannot be reached by traversing model from the registered services
        };
      }
    }

    public static ReflectorConfiguration ReflectorConfig()
    {
      return new ReflectorConfiguration(Types, Services, ModelNamespaces, MainMenus);
    }

    public static EntityObjectStoreConfiguration EntityObjectStoreConfig()
    {
      var config = new EntityObjectStoreConfiguration();
      config.UsingCodeFirstContext(() => new EducationManagementDbContext());
      config.SpecifyTypesNotAssociatedWithAnyContext(() => new[] { typeof(PropertyViewModel), typeof(FindViewModel) });
      return config;
    }

    public static IAuditConfiguration AuditConfig()
    {
      return null; //No auditing set up
      //Example:
      //config.AddNamespaceAuditor<SmartMeterAuditor>("SinePulse.SmartMeter.Audit.TariffAudits.TariffAudit");
    }

    public static IAuthorizationConfiguration AuthorizationConfig()
    {
      return null; //No authorization set up
                   //Example:
                   //var config = new AuthorizationConfiguration<MyDefaultAuthorizer>();
                   //config.AddTypeAuthorizer<Foo, FooAuthorizer>();
                   //config.AddNamespaceAuthorizer<BarAuthorizer>("MySpace.Bar");
                   //return config;
    }

    public static IProfileConfiguration ProfileConfig()
    {
      return null;
      //Example:
      //var events = new HashSet<ProfileEvent>() { ProfileEvent.ActionInvocation }; //etc
      //return new ProfileConfiguration<MyProfiler>() { EventsToProfile = events };
    }

    /// <summary>
    /// Return an array of IMenus (obtained via the factory, then configured) to
    /// specify the Main Menus for the application.
    /// </summary>
    public static IMenu[] MainMenus(IMenuFactory factory)
    {
      var instituteMenu = factory.NewMenu<InstituteManagementRepository>();
      InstituteManagementRepository.Menu(instituteMenu);
      
      var studentMenu = factory.NewMenu<StudentRepository>();
      StudentRepository.Menu(studentMenu);

      var employeeMenu = factory.NewMenu<EmployeeRepository>();
      EmployeeRepository.Menu(employeeMenu);

      var financeMenu = factory.NewMenu<FinanceRepository>();
      FinanceRepository.Menu(financeMenu);

      var userManagementMenu = factory.NewMenu<UserManagementRepository>();
      UserManagementRepository.Menu(userManagementMenu);

      var setupMenu = factory.NewMenu<SetupRepository>();
      SetupRepository.Menu(setupMenu);

      return new IMenu[]
      {
        instituteMenu,
        studentMenu,
        employeeMenu,
        financeMenu,
        userManagementMenu,
        setupMenu
      };
    }
  }
}