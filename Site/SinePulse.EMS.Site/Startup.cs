using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SinePulse.EMS.Constants;
using SinePulse.EMS.Domain.UserManagement;
using SinePulse.EMS.Messages.EmployeeSalaryMessages;
using SinePulse.EMS.Persistence.Facade;
using SinePulse.EMS.Persistence.Repositories;
using SinePulse.EMS.Site.Authorization;
using SinePulse.EMS.Site.Presenters;
using SinePulse.EMS.UseCases.AcademicFees;
using SinePulse.EMS.UseCases.AccountHeads;
using SinePulse.EMS.UseCases.Attendances;
using SinePulse.EMS.UseCases.Authorization;
using SinePulse.EMS.UseCases.AutoPostingConfigurations;
using SinePulse.EMS.UseCases.BalanceSheets;
using SinePulse.EMS.UseCases.BankAccounts;
using SinePulse.EMS.UseCases.BankBranches;
using SinePulse.EMS.UseCases.Banks;
using SinePulse.EMS.UseCases.Billings;
using SinePulse.EMS.UseCases.ServiceCharges;
using SinePulse.EMS.UseCases.Branches;
using SinePulse.EMS.UseCases.BranchMediumAccountsHeads;
using SinePulse.EMS.UseCases.BranchMediums;
using SinePulse.EMS.UseCases.BreakTimes;
using SinePulse.EMS.UseCases.Classes;
using SinePulse.EMS.UseCases.ClassRoutines;
using SinePulse.EMS.UseCases.ClassTests;
using SinePulse.EMS.UseCases.CommitteeMembers;
using SinePulse.EMS.UseCases.ContactInfos;
using SinePulse.EMS.UseCases.Designations;
using SinePulse.EMS.UseCases.Employee;
using SinePulse.EMS.UseCases.EmployeeAddresses;
using SinePulse.EMS.UseCases.EmployeeEducationalQualifications;
using SinePulse.EMS.UseCases.EmployeeGrade;
using SinePulse.EMS.UseCases.EmployeeLeaves;
using SinePulse.EMS.UseCases.EmployeePersonalInfo;
using SinePulse.EMS.UseCases.ExamConfigurations;
using SinePulse.EMS.UseCases.ExamTerms;
using SinePulse.EMS.UseCases.Features;
using SinePulse.EMS.UseCases.FeeCollections;
using SinePulse.EMS.UseCases.ImportSessionDatas;
using SinePulse.EMS.UseCases.IncomeStatements;
using SinePulse.EMS.UseCases.Institutes;
using SinePulse.EMS.UseCases.JobTypes;
using SinePulse.EMS.UseCases.LeaveTypes;
using SinePulse.EMS.UseCases.Login;
using SinePulse.EMS.UseCases.LoginUsers;
using SinePulse.EMS.UseCases.Logout;
using SinePulse.EMS.UseCases.Marks;
using SinePulse.EMS.UseCases.Mediums;
using SinePulse.EMS.UseCases.Notifications;
using SinePulse.EMS.UseCases.OnlinePayments;
using SinePulse.EMS.UseCases.Payrolls;
using SinePulse.EMS.UseCases.PublicHolidays;
using SinePulse.EMS.UseCases.Register;
using SinePulse.EMS.UseCases.Repositories;
using SinePulse.EMS.UseCases.ResultGrades;
using SinePulse.EMS.UseCases.ResultSheets;
using SinePulse.EMS.UseCases.Roles;
using SinePulse.EMS.UseCases.Rooms;
using SinePulse.EMS.UseCases.SalaryComponentTypes;
using SinePulse.EMS.UseCases.SeatPlans;
using SinePulse.EMS.UseCases.Sections;
using SinePulse.EMS.UseCases.Sessions;
using SinePulse.EMS.UseCases.Shifts;
using SinePulse.EMS.UseCases.StudentContactPersons;
using SinePulse.EMS.UseCases.StudentPromotions;
using SinePulse.EMS.UseCases.Students;
using SinePulse.EMS.UseCases.StudentSections;
using SinePulse.EMS.UseCases.Subjects;
using SinePulse.EMS.UseCases.Terms;
using SinePulse.EMS.UseCases.TermTestMarks;
using SinePulse.EMS.UseCases.TermTests;
using SinePulse.EMS.UseCases.Transactions;
using SinePulse.EMS.UseCases.TrialBalances;
using SinePulse.EMS.UseCases.Waivers;
using SinePulse.EMS.UseCases.WorkingShifts;
using SinePulse.EMS.Utility;
using SinePulse.EMS.Utility.MessagingQueue;
using SinePulse.EMS.Utility.MessagingQueue.MassTransit;
using SinePulse.EMS.Utility.SslPaymentGateway;
using SinePulse.EMS.UseCases.SalaryComponentTypes;
using SinePulse.EMS.UseCases.SalaryComponents;
using SinePulse.EMS.UseCases.CommitteeMemberAddresses;
using SinePulse.EMS.UseCases.CommitteeMemberPersonalInfos;
using SinePulse.EMS.UseCases.EmployeeSalaries;
using SinePulse.EMS.UseCases.GenerateSalarySheets;
using SinePulse.EMS.TaxService;
using SinePulse.EMS.UseCases.OtherComponents;
using SinePulse.EMS.UseCases.SalarySheets;

namespace SinePulse.EMS.Site
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;

      using (var client = new EmsDbContext(Configuration.GetConnectionString("DefaultConnection"), new SchemaInitializer()))
      {
        client.Database.EnsureCreated();
      }
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddScoped<ISchemaInitializer, SchemaInitializer>();
      services.Configure<CookiePolicyOptions>(options =>
      {
              // This lambda determines whether user consent for non-essential cookies is needed for a given request.
              options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });

      services.AddDbContext<EmsDbContext>(options =>
            //options.UseInMemoryDatabase(dbName));
      options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      services.AddIdentity<LoginUser, Role>(options =>
        {
          options.Password.RequiredLength = 8;
          options.Password.RequireNonAlphanumeric = true;
          options.Password.RequireUppercase = true;
          options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<EmsDbContext>();
      
      // Replace the default authorization policy provider with our own
      // custom provider which can return authorization policies for given
      // policy names (instead of using the default policy provider)
      services.AddSingleton<IAuthorizationPolicyProvider, FeatureAuthorizationPolicyProvider>();

      // As always, handlers must be provided for the requirements of the authorization policies
      services.AddSingleton<IAuthorizationHandler, FeatureAuthorizationHandler>();

      services.AddSingleton<ISingletonIsAuthorizedUseCase, SingletonIsAuthorizedUseCase>();
      services.AddScoped<IIsAuthorizedUseCase, IsAuthorizedUseCase>();

      services.AddLocalization(options => options.ResourcesPath = "Resources");

      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
        .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

      services.AddMvc()
        .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

      services.AddMvc()
        .AddJsonOptions(
          options => options.SerializerSettings.ReferenceLoopHandling =            
            ReferenceLoopHandling.Ignore
        );
      
      // Add cookie authentication so that it's possible to sign-in to test the 
      // custom authorization policy behavior of the sample
      services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
          options.AccessDeniedPath = new PathString("/Account/Denied");
          options.LoginPath = new PathString("/Account/Login");
        });


      var busCreator = new RabbitMqBusCreator(new RabbitMqConfig
      {
        Username = "sinepulse.ems.admin",
        Password = "sinepulse.ems.admin",
        ServerHost = "localhost",
        RabbitMqVirtualHost = "SinePulse.EducationManagementSystem"
      });

      var bus = busCreator.CreateBus((cfg, host) =>
      {
        cfg.ReceiveEndpoint(host, MicroServiceAddresses.WebSite,
          e => { });
      });

      services.AddSingleton(bus);
      services.AddSingleton<IUriGenerator>(new UriGenerator(busCreator.GetRabbitMqUri()));
      services.AddSingleton<IEndpointProvider, EndpointProvider>();
      services.AddSingleton<IMessageSender, MassTransitMessageSender>();

      
      services.AddSingleton<IIntegerOverlappingChecker, IntegerOverlappingChecker>();
      services.AddSingleton<ITimeOverlappingChecker, TimeOverlappingChecker>();
      services.AddSingleton<ITimestampOverlappingChecker, TimestampOverlappingChecker>();

      services.AddScoped<IRepository, GenericRepository>();

      services.AddScoped<ISessionProvider, SessionProvider>();
      services.AddScoped<ICurrentSessionProvider, CurrentSessionProvider>();

      services.AddScoped<IDbInitializer, DbInitializer>();
      services.AddScoped<ISchemaInitializer, SchemaInitializer>();
      services.AddScoped<IUserTableInitializer, UserTableInitializer>();
      services.AddScoped<IRoleTableInitializer, RoleTableInitializer>();
      services.AddScoped<IChartsOfAccountsInitializer, ChartsOfAccountsInitializer>();

      services.AddScoped<IUniqueEmailChecker, UniqueEmailChecker>();
      services.AddScoped<IUniqueRolePropertyChecker, UniqueRolePropertyChecker>();
      services.AddScoped<IValidUsernameChecker, ValidUsernameChecker>();
      services.AddScoped<IValidStudentChecker, ValidStudentChecker>();
      services.AddScoped<IValidSectionChecker, ValidSectionChecker>();
      services.AddScoped<IValidBranchMediumChecker, ValidBranchMediumChecker>();
      services.AddScoped<IValidExamConfigurationChecker, ValidExamConfigurationChecker>();
      services.AddScoped<IValidTermTestChecker, ValidTermTestChecker>();
      services.AddScoped<IValidClassTestChecker, ValidClassTestChecker>();
      services.AddScoped<IValidSeatPlanChecker, ValidSeatPlanChecker>();
      services.AddScoped<IValidResultGradeChecker, ValidResultGradeChecker>();
      services.AddScoped<IValidMarkChecker, ValidMarkChecker>();
      services.AddScoped<IValidRoleChecker, ValidRoleChecker>();
      services.AddScoped<IValidEmployeeRegisterDataChecker, ValidEmployeeRegisterDataChecker>();
      services.AddScoped<IValidEmployeeRegistrationChecker, ValidEmployeeRegistrationChecker>();

      services.AddScoped<DisplayRegisterPageRequestMessageValidator, DisplayRegisterPageRequestMessageValidator>();
      services.AddScoped<IDisplayRegisterPageUseCase, DisplayRegisterPageUseCase>();
      services.AddScoped<DisplayRegisterPageRequestHandler, DisplayRegisterPageRequestHandler>();
      services.AddScoped<DisplayRegisterPageResponsePresenter, DisplayRegisterPageResponsePresenter>();
      services.AddScoped<RegisterResponsePresenter, RegisterResponsePresenter>();
      services.AddScoped<RegisterRequestMessageValidator, RegisterRequestMessageValidator>();
      services.AddScoped<IRegisterUseCase, RegisterUseCase>();
      services.AddScoped<RegisterRequestHandler, RegisterRequestHandler>();
      
      services.AddScoped<DisplayLoginPageRequestMessageValidator, DisplayLoginPageRequestMessageValidator>();
      services.AddScoped<IDisplayLoginPageUseCase, DisplayLoginPageUseCase>();
      services.AddScoped<DisplayLoginPageRequestHandler, DisplayLoginPageRequestHandler>();
      services.AddScoped<DisplayLoginPageResponsePresenter, DisplayLoginPageResponsePresenter>();
      services.AddScoped<LoginRequestMessageValidator, LoginRequestMessageValidator>();
      services.AddScoped<ILoginUseCase, LoginUseCase>();
      services.AddScoped<LoginRequestHandler, LoginRequestHandler>();
      
      services.AddScoped<LogoutRequestMessageValidator, LogoutRequestMessageValidator>();
      services.AddScoped<ILogoutUseCase, LogoutUseCase>();
      services.AddScoped<LogoutRequestHandler, LogoutRequestHandler>();

      services.AddScoped<GetUserAssociationRequestMessageValidator, GetUserAssociationRequestMessageValidator>();
      services.AddScoped<IGetUserAssociationUseCase, GetUserAssociationUseCase>();
      services.AddScoped<GetUserAssociationRequestHandler, GetUserAssociationRequestHandler>();
      
      services.AddScoped<DisplayAddStudentPageRequestMessageValidator, DisplayAddStudentPageRequestMessageValidator>();
      services.AddScoped<IDisplayAddStudentPageUseCase, DisplayAddStudentPageUseCase>();
      services.AddScoped<DisplayAddStudentPageRequestHandler, DisplayAddStudentPageRequestHandler>();
      services.AddScoped<DisplayAddStudentPageResponsePresenter, DisplayAddStudentPageResponsePresenter>();

      services.AddScoped<AddStudentRequestMessageValidator, AddStudentRequestMessageValidator>();
      services.AddScoped<IAddStudentUseCase, AddStudentUseCase>();
      services.AddScoped<AddStudentRequestHandler, AddStudentRequestHandler>();
      services.AddScoped<AddStudentResponsePresenter, AddStudentResponsePresenter>();
      
      services.AddScoped<AddOrChangeStudentImageRequestMessageValidator, AddOrChangeStudentImageRequestMessageValidator>();
      services.AddScoped<IAddOrChangeStudentImageUseCase, AddOrChangeStudentImageUseCase>();
      services.AddScoped<AddOrChangeStudentImageRequestHandler, AddOrChangeStudentImageRequestHandler>();
      
      services.AddScoped<EditStudentRequestMessageValidator, EditStudentRequestMessageValidator>();
      services.AddScoped<IEditStudentUseCase, EditStudentUseCase>();
      services.AddScoped<EditStudentRequestHandler, EditStudentRequestHandler>();
      services.AddScoped<EditStudentResponsePresenter, EditStudentResponsePresenter>();

      services.AddScoped<ShowStudentRequestMessageValidator, ShowStudentRequestMessageValidator>();
      services.AddScoped<IShowStudentUseCase, ShowStudentUseCase>();
      services.AddScoped<ShowStudentRequestHandler, ShowStudentRequestHandler>();
      services.AddScoped<ShowStudentResponsePresenter, ShowStudentResponsePresenter>();
      
      services.AddScoped<ShowStudentListRequestMessageValidator, ShowStudentListRequestMessageValidator>();
      services.AddScoped<IShowStudentListUseCase, ShowStudentListUseCase>();
      services.AddScoped<ShowStudentListRequestHandler, ShowStudentListRequestHandler>();
      services.AddScoped<ShowStudentListResponsePresenter, ShowStudentListResponsePresenter>();


      services.AddScoped<IDisplayPromoteStudentOptionPageUseCase, DisplayPromoteStudentOptionPageUseCase>();
      services.AddScoped<DisplayPromoteStudentOptionPageRequestHandler,
        DisplayPromoteStudentOptionPageRequestHandler>();
      services.AddScoped<DisplayPromoteStudentOptionPageResponsePresenter,
        DisplayPromoteStudentOptionPageResponsePresenter>();
      services.AddScoped<DisplayPromoteStudentOptionPageRequestMessageValidator,
        DisplayPromoteStudentOptionPageRequestMessageValidator>();
      services.AddScoped<IPromoteStudentOptionUseCase, PromoteStudentOptionUseCase>();
      services.AddScoped<PromoteStudentOptionRequestHandler, PromoteStudentOptionRequestHandler>();
      services.AddScoped<PromoteStudentOptionRequestMessageValidator, PromoteStudentOptionRequestMessageValidator>();

      services.AddScoped<IDisplayPromoteStudentsPageUseCase, DisplayPromoteStudentsPageUseCase>();
      services.AddScoped<DisplayPromoteStudentsPageRequestHandler, DisplayPromoteStudentsPageRequestHandler>();
      services.AddScoped<DisplayPromoteStudentsPageResponsePresenter, DisplayPromoteStudentsPageResponsePresenter>();
      services.AddScoped<DisplayPromoteStudentsPageRequestMessageValidator,
        DisplayPromoteStudentsPageRequestMessageValidator>();
      services.AddScoped<IPromoteStudentsUseCase, PromoteStudentsUseCase>();
      services.AddScoped<PromoteStudentsRequestHandler, PromoteStudentsRequestHandler>();
      services.AddScoped<PromoteStudentsResponsePresenter, PromoteStudentsResponsePresenter>();
      services.AddScoped<PromoteStudentsRequestMessageValidator, PromoteStudentsRequestMessageValidator>();
      
      
      services.AddScoped<IUniqueInstitutePropertyChecker, UniqueInstitutePropertyChecker>();
      services.AddScoped<AddInstituteRequestMessageValidator, AddInstituteRequestMessageValidator>();
      services.AddScoped<IAddInstituteUseCase, AddInstituteUseCase>();
      services.AddScoped<AddInstituteRequestHandler, AddInstituteRequestHandler>();
      services.AddScoped<AddInstituteResponsePresenter, AddInstituteResponsePresenter>();
      
      services.AddScoped<IUniqueBranchPropertyChecker, UniqueBranchPropertyChecker>();
      services.AddScoped<AddBranchRequestMessageValidator, AddBranchRequestMessageValidator>();
      services.AddScoped<IAddBranchUseCase, AddBranchUseCase>();
      services.AddScoped<AddBranchRequestHandler, AddBranchRequestHandler>();
      services.AddScoped<AddBranchResponsePresenter, AddBranchResponsePresenter>();
      services.AddScoped<ShowBranchListRequestMessageValidator, ShowBranchListRequestMessageValidator>();
      services.AddScoped<IShowBranchListUseCase, ShowBranchListUseCase>();
      services.AddScoped<ShowBranchListRequestHandler, ShowBranchListRequestHandler>();

      services.AddScoped<IUniqueMediumPropertyChecker, UniqueMediumPropertyChecker>();
      services.AddScoped<AddMediumRequestMessageValidator, AddMediumRequestMessageValidator>();
      services.AddScoped<IAddMediumUseCase, AddMediumUseCase>();
      services.AddScoped<AddMediumRequestHandler, AddMediumRequestHandler>();
      services.AddScoped<AddMediumResponsePresenter, AddMediumResponsePresenter>();
      services.AddScoped<ShowMediumListRequestMessageValidator, ShowMediumListRequestMessageValidator>();
      services.AddScoped<IShowMediumListUseCase, ShowMediumListUseCase>();
      services.AddScoped<ShowMediumListRequestHandler, ShowMediumListRequestHandler>();
      services.AddScoped<ShowMediumListResponsePresenter, ShowMediumListResponsePresenter>();
      services.AddScoped<EditMediumRequestMessageValidator, EditMediumRequestMessageValidator>();
      services.AddScoped<IEditMediumUseCase, EditMediumUseCase>();
      services.AddScoped<EditMediumRequestHandler, EditMediumRequestHandler>();
      services.AddScoped<EditMediumResponsePresenter, EditMediumResponsePresenter>();
      services.AddScoped<ShowMediumRequestMessageValidator, ShowMediumRequestMessageValidator>();
      services.AddScoped<IShowMediumUseCase, ShowMediumUseCase>();
      services.AddScoped<ShowMediumRequestHandler, ShowMediumRequestHandler>();
      services.AddScoped<ShowMediumResponsePresenter, ShowMediumResponsePresenter>();

      services.AddScoped<IUniqueSessionPropertyChecker, UniqueSessionPropertyChecker>();
      services.AddScoped<ISessionOverlappingChecker, SessionOverlappingChecker>();
      services.AddScoped<AddSessionRequestMessageValidator, AddSessionRequestMessageValidator>();
      services.AddScoped<IAddSessionUseCase, AddSessionUseCase>();
      services.AddScoped<AddSessionRequestHandler, AddSessionRequestHandler>();
      services.AddScoped<AddSessionResponsePresenter, AddSessionResponsePresenter>();
      services.AddScoped<IShowSessionListUseCase, ShowSessionListUseCase>();
      services.AddScoped<IEditSessionUseCase, EditSessionUseCase>();
      services.AddScoped<EditSessionRequestHandler, EditSessionRequestHandler>();
      services.AddScoped<EditSessionResponsePresenter, EditSessionResponsePresenter>();
      services.AddScoped<EditSessionRequestMessageValidator, EditSessionRequestMessageValidator>();
      services.AddScoped<ShowSessionRequestHandler, ShowSessionRequestHandler>();
      services.AddScoped<ShowSessionResponsePresenter, ShowSessionResponsePresenter>();
      services.AddScoped<ShowSessionRequestMessageValidator, ShowSessionRequestMessageValidator>();
      services.AddScoped<IShowSessionUseCase, ShowSessionUseCase>();
      services.AddScoped<IShowCurrentSessionListUseCase, ShowCurrentSessionListUseCase>();
      services.AddScoped<ShowSessionListRequestHandler, ShowSessionListRequestHandler>();
      services.AddScoped<ShowCurrentSessionListRequestHandler, ShowCurrentSessionListRequestHandler>();
      services.AddScoped<ShowSessionListResponsePresenter, ShowSessionListResponsePresenter>();
      services.AddScoped<ShowCurrentSessionListResponsePresenter, ShowCurrentSessionListResponsePresenter>();
      services.AddScoped<ShowSessionListRequestMessageValidator, ShowSessionListRequestMessageValidator>();

      services.AddScoped<IDisplayImportSessionDataPageUseCase, DisplayImportSessionDataPageUseCase>();
      services.AddScoped<DisplayImportSessionDataPageRequestHandler, DisplayImportSessionDataPageRequestHandler>();
      services.AddScoped<DisplayImportSessionDataPageResponsePresenter,
        DisplayImportSessionDataPageResponsePresenter>();
      services.AddScoped<DisplayImportSessionDataPageRequestMessageValidator,
        DisplayImportSessionDataPageRequestMessageValidator>();
      services.AddScoped<IImportSessionDataUseCase, ImportSessionDataUseCase>();
      services.AddScoped<ImportSessionDataRequestHandler, ImportSessionDataRequestHandler>();
      services.AddScoped<ImportSessionDataResponsePresenter, ImportSessionDataResponsePresenter>();
      services.AddScoped<ImportSessionDataRequestMessageValidator, ImportSessionDataRequestMessageValidator>();
      
      services.AddScoped<IUniqueShiftPropertyChecker, UniqueShiftPropertyChecker>();
      services.AddScoped<AddShiftRequestMessageValidator, AddShiftRequestMessageValidator>();
      services.AddScoped<IAddShiftUseCase, AddShiftUseCase>();
      services.AddScoped<AddShiftRequestHandler, AddShiftRequestHandler>();
      services.AddScoped<AddShiftResponsePresenter, AddShiftResponsePresenter>();
      services.AddScoped<ShowShiftListRequestMessageValidator, ShowShiftListRequestMessageValidator>();
      services.AddScoped<IShowShiftListUseCase, ShowShiftListUseCase>();
      services.AddScoped<ShowShiftListRequestHandler, ShowShiftListRequestHandler>();
      services.AddScoped<ShowShiftListResponsePresenter, ShowShiftListResponsePresenter>();
      services.AddScoped<ShowShiftRequestMessageValidator, ShowShiftRequestMessageValidator>();
      services.AddScoped<IShowShiftUseCase, ShowShiftUseCase>();
      services.AddScoped<ShowShiftRequestHandler, ShowShiftRequestHandler>();
      services.AddScoped<ShowShiftResponsePresenter, ShowShiftResponsePresenter>();
      services.AddScoped<EditShiftRequestMessageValidator, EditShiftRequestMessageValidator>();
      services.AddScoped<IEditShiftUseCase, EditShiftUseCase>();
      services.AddScoped<EditShiftRequestHandler, EditShiftRequestHandler>();
      services.AddScoped<EditShiftResponsePresenter, EditShiftResponsePresenter>();
      
      services.AddScoped<IUniqueRoomPropertyChecker, UniqueRoomPropertyChecker>();
      services.AddScoped<AddRoomRequestMessageValidator, AddRoomRequestMessageValidator>();
      services.AddScoped<IAddRoomUseCase, AddRoomUseCase>();
      services.AddScoped<AddRoomRequestHandler, AddRoomRequestHandler>();
      services.AddScoped<AddRoomResponsePresenter, AddRoomResponsePresenter>();
      services.AddScoped<ShowRoomRequestMessageValidator, ShowRoomRequestMessageValidator>();
      services.AddScoped<IShowRoomUseCase, ShowRoomUseCase>();
      services.AddScoped<ShowRoomRequestHandler, ShowRoomRequestHandler>();
      services.AddScoped<ShowRoomResponsePresenter, ShowRoomResponsePresenter>();
      services.AddScoped<EditRoomRequestMessageValidator, EditRoomRequestMessageValidator>();
      services.AddScoped<IEditRoomUseCase, EditRoomUseCase>();
      services.AddScoped<EditRoomRequestHandler, EditRoomRequestHandler>();
      services.AddScoped<EditRoomResponsePresenter, EditRoomResponsePresenter>();
      services.AddScoped<ShowRoomsListRequestMessageValidator, ShowRoomsListRequestMessageValidator>();
      services.AddScoped<IShowRoomsListUseCase, ShowRoomsListUseCase>();
      services.AddScoped<ShowRoomsListRequestHandler, ShowRoomsListRequestHandler>();
      services.AddScoped<ShowRoomsListResponsePresenter, ShowRoomsListResponsePresenter>();

      services.AddScoped<IUniqueMediumPropertyChecker, UniqueMediumPropertyChecker>();
      services.AddScoped<AddMediumRequestMessageValidator, AddMediumRequestMessageValidator>();
      services.AddScoped<IAddMediumUseCase, AddMediumUseCase>();
      services.AddScoped<AddMediumRequestHandler, AddMediumRequestHandler>();
      services.AddScoped<AddMediumResponsePresenter, AddMediumResponsePresenter>();
      
      services.AddScoped<AddBranchMediumRequestMessageValidator, AddBranchMediumRequestMessageValidator>();
      services.AddScoped<IAddBranchMediumUseCase, AddBranchMediumUseCase>();
      services.AddScoped<AddBranchMediumRequestHandler, AddBranchMediumRequestHandler>();
      services.AddScoped<AddBranchMediumResponsePresenter, AddBranchMediumResponsePresenter>();
      
      services.AddScoped<IUniqueClassPropertyChecker, UniqueClassPropertyChecker>();
      services.AddScoped<AddClassRequestMessageValidator, AddClassRequestMessageValidator>();
      services.AddScoped<IAddClassUseCase, AddClassUseCase>();
      services.AddScoped<AddClassRequestHandler, AddClassRequestHandler>();
      services.AddScoped<AddClassResponsePresenter, AddClassResponsePresenter>();
      services.AddScoped<AddSubjectInClassResponsePresenter, AddSubjectInClassResponsePresenter>();
      services.AddScoped<AddSubjectInClassRequestHandler, AddSubjectInClassRequestHandler>();
      services.AddScoped<AddSubjectInClassRequestMessageValidator, AddSubjectInClassRequestMessageValidator>();
      services.AddScoped<IAddSubjectInClassUseCase, AddSubjectInClassUseCase>();
      services.AddScoped<IShowClassesListUseCase, ShowClassesListUseCase>();
      services.AddScoped<ShowClassesListRequestHandler, ShowClassesListRequestHandler>();
      services.AddScoped<ShowClassesListResponsePresenter, ShowClassesListResponsePresenter>();
      services.AddScoped<ShowClassesListRequestMessageValidator, ShowClassesListRequestMessageValidator>();
      services.AddScoped<IShowClassUseCase, ShowClassUseCase>();
      services.AddScoped<ShowClassRequestHandler, ShowClassRequestHandler>();
      services.AddScoped<ShowClassResponsePresenter, ShowClassResponsePresenter>();
      services.AddScoped<ShowClassRequestMessageValidator, ShowClassRequestMessageValidator>();
      services.AddScoped<IEditClassUseCase, EditClassUseCase>();
      services.AddScoped<EditClassRequestHandler, EditClassRequestHandler>();
      services.AddScoped<EditClassResponsePresenter, EditClassResponsePresenter>();
      services.AddScoped<EditClassRequestMessageValidator, EditClassRequestMessageValidator>();
      
      services.AddScoped<IUniqueSectionPropertyChecker, UniqueSectionPropertyChecker>();
      services.AddScoped<AddSectionRequestMessageValidator, AddSectionRequestMessageValidator>();
      services.AddScoped<IAddSectionUseCase, AddSectionUseCase>();
      services.AddScoped<AddSectionRequestHandler, AddSectionRequestHandler>();
      services.AddScoped<AddSectionResponsePresenter, AddSectionResponsePresenter>();
      services.AddScoped<AssignOrChangeRoomInSectionRequestMessageValidator, AssignOrChangeRoomInSectionRequestMessageValidator>();
      services.AddScoped<IAssignOrChangeRoomInSectionUseCase, AssignOrChangeRoomInSectionUseCase>();
      services.AddScoped<AssignOrChangeRoomInSectionRequestHandler, AssignOrChangeRoomInSectionRequestHandler>();
      services.AddScoped<AssignOrChangeRoomInSectionResponsePresenter, AssignOrChangeRoomInSectionResponsePresenter>();

      services.AddScoped<IUniqueSubjectPropertyChecker, UniqueSubjectPropertyChecker>();
      services.AddScoped<AddSubjectRequestMessageValidator, AddSubjectRequestMessageValidator>();
      services.AddScoped<IAddSubjectUseCase, AddSubjectUseCase>();
      services.AddScoped<AddSubjectRequestHandler, AddSubjectRequestHandler>();
      services.AddScoped<AddSubjectResponsePresenter, AddSubjectResponsePresenter>();
      services.AddScoped<ShowSubjectListRequestMessageValidator, ShowSubjectListRequestMessageValidator>();
      services.AddScoped<IShowSubjectListUseCase, ShowSubjectListUseCase>();
      services.AddScoped<ShowSubjectListRequestHandler, ShowSubjectListRequestHandler>();
      services.AddScoped<ShowSubjectListResponsePresenter, ShowSubjectListResponsePresenter>();
      services.AddScoped<ShowSubjectRequestMessageValidator, ShowSubjectRequestMessageValidator>();
      services.AddScoped<IShowSubjectUseCase, ShowSubjectUseCase>();
      services.AddScoped<ShowSubjectRequestHandler, ShowSubjectRequestHandler>();
      services.AddScoped<ShowSubjectResponsePresenter, ShowSubjectResponsePresenter>();
      services.AddScoped<EditSubjectRequestMessageValidator, EditSubjectRequestMessageValidator>();
      services.AddScoped<IEditSubjectUseCase, EditSubjectUseCase>();
      services.AddScoped<EditSubjectRequestHandler, EditSubjectRequestHandler>();
      services.AddScoped<EditSubjectResponsePresenter, EditSubjectResponsePresenter>();

      services.AddScoped<DisplayAddTermPageRequestMessageValidator, DisplayAddTermPageRequestMessageValidator>();
      services.AddScoped<IDisplayAddTermPageUseCase, DisplayAddTermPageUseCase>();
      services.AddScoped<DisplayAddTermPageRequestHandler, DisplayAddTermPageRequestHandler>();
      services.AddScoped<DisplayAddTermPageResponsePresenter, DisplayAddTermPageResponsePresenter>();

      services.AddScoped<IUniqueTermPropertyChecker, UniqueTermPropertyChecker>();
      services.AddScoped<AddTermRequestMessageValidator, AddTermRequestMessageValidator>();
      services.AddScoped<IAddTermUseCase, AddTermUseCase>();
      services.AddScoped<AddTermRequestHandler, AddTermRequestHandler>();
      services.AddScoped<AddTermResponsePresenter, AddTermResponsePresenter>();
      
      services.AddScoped<IValidTermChecker, ValidTermChecker>();
      services.AddScoped<ShowTermRequestMessageValidator, ShowTermRequestMessageValidator>();
      services.AddScoped<IShowTermUseCase, ShowTermUseCase>();
      services.AddScoped<ShowTermRequestHandler, ShowTermRequestHandler>();
      services.AddScoped<ShowTermResponsePresenter, ShowTermResponsePresenter>();

      services.AddScoped<DisplayAddExamTypePageRequestMessageValidator,
        DisplayAddExamTypePageRequestMessageValidator>();
      services.AddScoped<IDisplayAddExamTypePageUseCase, DisplayAddExamTypePageUseCase>();
      services.AddScoped<DisplayAddExamTypePageRequestHandler, DisplayAddExamTypePageRequestHandler>();
      //services.AddScoped<DisplayAddExamTypePageResponsePresenter, DisplayAddExamTypePageResponsePresenter>();

      services.AddScoped<IUniqueExamConfigurationPropertyChecker, UniqueExamConfigurationPropertyChecker>();
      services.AddScoped<AddExamConfigurationRequestMessageValidator, AddExamConfigurationRequestMessageValidator>();
      services.AddScoped<IAddExamConfigurationUseCase, AddExamConfigurationUseCase>();
      services.AddScoped<AddExamConfigurationRequestHandler, AddExamConfigurationRequestHandler>();
      services.AddScoped<AddExamConfigurationResponsePresenter, AddExamConfigurationResponsePresenter>();

      services.AddScoped<ShowExamConfigurationRequestMessageValidator, ShowExamConfigurationRequestMessageValidator>();
      services.AddScoped<IShowExamConfigurationUseCase, ShowExamConfigurationUseCase>();
      services.AddScoped<ShowExamConfigurationRequestHandler, ShowExamConfigurationRequestHandler>();
      services.AddScoped<ShowExamConfigurationResponsePresenter, ShowExamConfigurationResponsePresenter>();

      services.AddScoped<DisplayAddClassTestPageRequestMessageValidator,
        DisplayAddClassTestPageRequestMessageValidator>();
      services.AddScoped<IDisplayAddClassTestPageUseCase, DisplayAddClassTestPageUseCase>();
      services.AddScoped<DisplayAddClassTestPageRequestHandler, DisplayAddClassTestPageRequestHandler>();
      services.AddScoped<DisplayAddClassTestPageResponsePresenter, DisplayAddClassTestPageResponsePresenter>();

      services.AddScoped<IUniqueClassTestPropertyChecker, UniqueClassTestPropertyChecker>();
      services.AddScoped<IClassTestOverlappingChecker, ClassTestOverlappingChecker>();
      services.AddScoped<AddClassTestRequestMessageValidator, AddClassTestRequestMessageValidator>();
      services.AddScoped<IAddClassTestUseCase, AddClassTestUseCase>();
      services.AddScoped<AddClassTestRequestHandler, AddClassTestRequestHandler>();
      services.AddScoped<AddClassTestResponsePresenter, AddClassTestResponsePresenter>();

      services.AddScoped<ShowClassTestRequestMessageValidator, ShowClassTestRequestMessageValidator>();
      services.AddScoped<IShowClassTestUseCase, ShowClassTestUseCase>();
      services.AddScoped<ShowClassTestRequestHandler, ShowClassTestRequestHandler>();
      services.AddScoped<ShowClassTestResponsePresenter, ShowClassTestResponsePresenter>();

      services.AddScoped<ITermTestOverlappingChecker, TermTestOverlappingChecker>();
      services.AddScoped<AddTermTestRequestMessageValidator, AddTermTestRequestMessageValidator>();
      services.AddScoped<IAddTermTestUseCase, AddTermTestUseCase>();
      services.AddScoped<AddTermTestRequestHandler, AddTermTestRequestHandler>();
      services.AddScoped<AddTermTestResponsePresenter, AddTermTestResponsePresenter>();

      services.AddScoped<DisplayAddSeatPlanPageRequestMessageValidator,
        DisplayAddSeatPlanPageRequestMessageValidator>();
      services.AddScoped<IDisplayAddSeatPlanPageUseCase, DisplayAddSeatPlanPageUseCase>();
      services.AddScoped<DisplayAddSeatPlanPageRequestHandler, DisplayAddSeatPlanPageRequestHandler>();
      services.AddScoped<DisplayAddSeatPlanPageResponsePresenter, DisplayAddSeatPlanPageResponsePresenter>();

      services.AddScoped<IGradeMarksOverlappingChecker, GradeMarksOverlappingChecker>();
      services.AddScoped<ITotalRegisteredStudentProvider, TotalRegisteredStudentProvider>();
      services.AddScoped<ITotalSeatPlanedStudentProvider, TotalSeatPlanedStudentProvider>();
      services.AddScoped<IAddSeatPlanValidationHelper, SeatPlanValidationHelper>();
      services.AddScoped<AddSeatPlanRequestMessageValidator, AddSeatPlanRequestMessageValidator>();
      services.AddScoped<IAddSeatPlanUseCase, AddSeatPlanUseCase>();
      services.AddScoped<AddSeatPlanRequestHandler, AddSeatPlanRequestHandler>();
      services.AddScoped<AddSeatPlanResponsePresenter, AddSeatPlanResponsePresenter>();

      services.AddScoped<DisplayEditSeatPlanPageRequestMessageValidator,
        DisplayEditSeatPlanPageRequestMessageValidator>();
      services.AddScoped<IDisplayEditSeatPlanPageUseCase, DisplayEditSeatPlanPageUseCase>();
      services.AddScoped<DisplayEditSeatPlanPageRequestHandler, DisplayEditSeatPlanPageRequestHandler>();
      services.AddScoped<DisplayEditSeatPlanPageResponsePresenter, DisplayEditSeatPlanPageResponsePresenter>();

      services.AddScoped<IEditSeatPlanValidationHelper, SeatPlanValidationHelper>();
      services.AddScoped<EditSeatPlanRequestMessageValidator, EditSeatPlanRequestMessageValidator>();
      services.AddScoped<IEditSeatPlanUseCase, EditSeatPlanUseCase>();
      services.AddScoped<EditSeatPlanRequestHandler, EditSeatPlanRequestHandler>();
      services.AddScoped<EditSeatPlanResponsePresenter, EditSeatPlanResponsePresenter>();

      services.AddScoped<ShowSeatPlanRequestMessageValidator, ShowSeatPlanRequestMessageValidator>();
      services.AddScoped<IShowSeatPlanUseCase, ShowSeatPlanUseCase>();
      services.AddScoped<ShowSeatPlanRequestHandler, ShowSeatPlanRequestHandler>();
      services.AddScoped<ShowSeatPlanResponsePresenter, ShowSeatPlanResponsePresenter>();

      services.AddScoped<DisplayAddResultGradePageRequestMessageValidator,
        DisplayAddResultGradePageRequestMessageValidator>();
      services.AddScoped<IDisplayAddResultGradePageUseCase, DisplayAddResultGradePageUseCase>();
      services.AddScoped<DisplayAddResultGradePageRequestHandler, DisplayAddResultGradePageRequestHandler>();
      services.AddScoped<DisplayAddResultGradePageResponsePresenter, DisplayAddResultGradePageResponsePresenter>();

      services.AddScoped<IUniqueResultGradePropertyChecker, UniqueResultGradePropertyChecker>();
      services.AddScoped<IGradeMarksOverlappingChecker, GradeMarksOverlappingChecker>();
      services.AddScoped<AddResultGradeRequestMessageValidator, AddResultGradeRequestMessageValidator>();
      services.AddScoped<IAddResultGradeUseCase, AddResultGradeUseCase>();
      services.AddScoped<AddResultGradeRequestHandler, AddResultGradeRequestHandler>();
      services.AddScoped<AddResultGradeResponsePresenter, AddResultGradeResponsePresenter>();

      services.AddScoped<ShowResultGradeRequestMessageValidator, ShowResultGradeRequestMessageValidator>();
      services.AddScoped<IShowResultGradeUseCase, ShowResultGradeUseCase>();
      services.AddScoped<ShowResultGradeRequestHandler, ShowResultGradeRequestHandler>();
      services.AddScoped<ShowResultGradeResponsePresenter, ShowResultGradeResponsePresenter>();


      services.AddScoped<DisplayAddMarkPageRequestMessageValidator, DisplayAddMarkPageRequestMessageValidator>();
      services.AddScoped<IDisplayAddMarkPageUseCase, DisplayAddMarkPageUseCase>();
      services.AddScoped<DisplayAddMarkPageRequestHandler, DisplayAddMarkPageRequestHandler>();
      services.AddScoped<DisplayAddMarkPageResponsePresenter, DisplayAddMarkPageResponsePresenter>();

      services.AddScoped<IGradeMarksOverlappingChecker, GradeMarksOverlappingChecker>();
      services.AddScoped<AddMarkRequestMessageValidator, AddMarkRequestMessageValidator>();
      services.AddScoped<IAddMarkUseCase, AddMarkUseCase>();
      services.AddScoped<AddMarkRequestHandler, AddMarkRequestHandler>();
      services.AddScoped<AddMarkResponsePresenter, AddMarkResponsePresenter>();

      services.AddScoped<ShowMarkRequestMessageValidator, ShowMarkRequestMessageValidator>();
      services.AddScoped<IShowMarkUseCase, ShowMarkUseCase>();
      services.AddScoped<ShowMarkRequestHandler, ShowMarkRequestHandler>();
      services.AddScoped<ShowMarkResponsePresenter, ShowMarkResponsePresenter>();

      
      services.AddScoped<DisplayAddMarkPageRequestMessageValidator, DisplayAddMarkPageRequestMessageValidator>();
      services.AddScoped<IDisplayAddMarkPageUseCase, DisplayAddMarkPageUseCase>();
      services.AddScoped<DisplayAddMarkPageRequestHandler, DisplayAddMarkPageRequestHandler>();

      services.AddScoped<IUniqueClassTestPropertyChecker, UniqueClassTestPropertyChecker>();
      services.AddScoped<IClassTestOverlappingChecker, ClassTestOverlappingChecker>();
      services.AddScoped<AddMarkRequestMessageValidator, AddMarkRequestMessageValidator>();
      services.AddScoped<IAddMarkUseCase, AddMarkUseCase>();
      services.AddScoped<AddMarkRequestHandler, AddMarkRequestHandler>();
      
      services.AddScoped<DisplayAddRolePageRequestMessageValidator, DisplayAddRolePageRequestMessageValidator>();
      services.AddScoped<IDisplayAddRolePageUseCase, DisplayAddRolePageUseCase>();
      services.AddScoped<DisplayAddRolePageRequestHandler, DisplayAddRolePageRequestHandler>();
      services.AddScoped<DisplayAddRolePageResponsePresenter, DisplayAddRolePageResponsePresenter>();

      services.AddScoped<IGradeMarksOverlappingChecker, GradeMarksOverlappingChecker>();
      services.AddScoped<AddRoleRequestMessageValidator, AddRoleRequestMessageValidator>();
      services.AddScoped<IAddRoleUseCase, AddRoleUseCase>();
      services.AddScoped<AddRoleRequestHandler, AddRoleRequestHandler>();
      services.AddScoped<AddRoleResponsePresenter, AddRoleResponsePresenter>();

      services.AddScoped<ShowRoleRequestMessageValidator, ShowRoleRequestMessageValidator>();
      services.AddScoped<IShowRoleUseCase, ShowRoleUseCase>();
      services.AddScoped<ShowRoleRequestHandler, ShowRoleRequestHandler>();
      services.AddScoped<ShowRoleResponsePresenter, ShowRoleResponsePresenter>();
      services.AddScoped<ShowRoleListRequestMessageValidator, ShowRoleListRequestMessageValidator>();
      services.AddScoped<IShowRoleListUseCase, ShowRoleListUseCase>();
      services.AddScoped<ShowRoleListRequestHandler, ShowRoleListRequestHandler>();
      services.AddScoped<ShowRoleListResponsePresenter, ShowRoleListResponsePresenter>();

      services.AddScoped<AddEmployeeRequestMessageValidator, AddEmployeeRequestMessageValidator>();
      services.AddScoped<IAddEmployeeUseCase, AddEmployeeUseCase>();
      services.AddScoped<AddEmployeeRequestHandler, AddEmployeeRequestHandler>();
      services.AddScoped<AddEmployeeResponsePresenter, AddEmployeeResponsePresenter>();

      services.AddScoped<AddEmployeePersonalInfoRequestMessageValidator, AddEmployeePersonalInfoRequestMessageValidator>();
      services.AddScoped<IAddEmployeePersonalInfoUseCase, AddEmployeePersonalInfoUseCase>();
      services.AddScoped<AddEmployeePersonalInfoRequestHandler, AddEmployeePersonalInfoRequestHandler>();
      services.AddScoped<AddEmployeePersonalInfoResponsePresenter, AddEmployeePersonalInfoResponsePresenter>();
      
      services.AddScoped<IUniqueWorkingShiftPropertyChecker, UniqueWorkingShiftPropertyChecker>();
      services.AddScoped<AddWorkingShiftRequestMessageValidator, AddWorkingShiftRequestMessageValidator>();
      services.AddScoped<IAddWorkingShiftUseCase, AddWorkingShiftUseCase>();
      services.AddScoped<AddWorkingShiftRequestHandler, AddWorkingShiftRequestHandler>();
      services.AddScoped<AddWorkingShiftResponsePresenter, AddWorkingShiftResponsePresenter>();

      services.AddScoped<IUniquePublicHolidayPropertyChecker, UniquePublicHolidayPropertyChecker>();
      services.AddScoped<AddPublicHolidayRequestMessageValidator, AddPublicHolidayRequestMessageValidator>();
      services.AddScoped<IAddPublicHolidayUseCase, AddPublicHolidayUseCase>();
      services.AddScoped<AddPublicHolidayRequestHandler, AddPublicHolidayRequestHandler>();
      services.AddScoped<AddPublicHolidayResponsePresenter, AddPublicHolidayResponsePresenter>();
      
      services.AddScoped<IUniqueJobTypePropertyChecker, UniqueJobTypePropertyChecker>();
      services.AddScoped<AddJobTypeRequestMessageValidator, AddJobTypeRequestMessageValidator>();
      services.AddScoped<IAddJobTypeUseCase, AddJobTypeUseCase>();
      services.AddScoped<AddJobTypeRequestHandler, AddJobTypeRequestHandler>();
      services.AddScoped<AddJobTypeResponsePresenter, AddJobTypeResponsePresenter>();
                                
      services.AddScoped<IUniqueDesignationPropertyChecker, UniqueDesignationPropertyChecker>();
      services.AddScoped<AddDesignationRequestMessageValidator, AddDesignationRequestMessageValidator>();
      services.AddScoped<IAddDesignationUseCase, AddDesignationUseCase>();
      services.AddScoped<AddDesignationRequestHandler, AddDesignationRequestHandler>();
      services.AddScoped<AddDesignationResponsePresenter, AddDesignationResponsePresenter>();
      services.AddScoped<ShowDesignationRequestMessageValidator, ShowDesignationRequestMessageValidator>();
      services.AddScoped<IShowDesignationUseCase, ShowDesignationUseCase>();
      services.AddScoped<ShowDesignationRequestHandler, ShowDesignationRequestHandler>();
      services.AddScoped<ShowDesignationResponsePresenter, ShowDesignationResponsePresenter>();
      services.AddScoped<ShowDesignationListRequestMessageValidator, ShowDesignationListRequestMessageValidator>();
      services.AddScoped<IShowDesignationListUseCase, ShowDesignationListUseCase>();
      services.AddScoped<ShowDesignationListRequestHandler, ShowDesignationListRequestHandler>();
      services.AddScoped<ShowDesignationListResponsePresenter, ShowDesignationListResponsePresenter>();
      services.AddScoped<EditDesignationRequestMessageValidator, EditDesignationRequestMessageValidator>();
      services.AddScoped<IEditDesignationUseCase, EditDesignationUseCase>();
      services.AddScoped<EditDesignationRequestHandler, EditDesignationRequestHandler>();
      services.AddScoped<EditDesignationResponsePresenter, EditDesignationResponsePresenter>();
      
      services.AddScoped<IUniqueEmployeeGradePropertyChecker, UniqueEmployeeGradePropertyChecker>();
      services.AddScoped<AddEmployeeGradeRequestMessageValidator, AddEmployeeGradeRequestMessageValidator>();
      services.AddScoped<IAddEmployeeGradeUseCase, AddEmployeeGradeUseCase>();
      services.AddScoped<AddEmployeeGradeRequestHandler, AddEmployeeGradeRequestHandler>();
      services.AddScoped<AddEmployeeGradeResponsePresenter, AddEmployeeGradeResponsePresenter>();
      
      services.AddScoped<IUniqueLeaveTypePropertyChecker, UniqueLeaveTypePropertyChecker>();
      services.AddScoped<AddLeaveTypeRequestMessageValidator, AddLeaveTypeRequestMessageValidator>();
      services.AddScoped<IAddLeaveTypeUseCase, AddLeaveTypeUseCase>();
      services.AddScoped<AddLeaveTypeRequestHandler, AddLeaveTypeRequestHandler>();
      services.AddScoped<AddLeaveTypeResponsePresenter, AddLeaveTypeResponsePresenter>();

      services.AddScoped<AddEmployeeEducationalQualificationRequestMessageValidator, AddEmployeeEducationalQualificationRequestMessageValidator>();
      services.AddScoped<IAddEmployeeEducationalQualificationUseCase, AddEmployeeEducationalQualificationUseCase>();
      services.AddScoped<AddEmployeeEducationalQualificationRequestHandler, AddEmployeeEducationalQualificationRequestHandler>();
      services.AddScoped<AddEmployeeEducationalQualificationResponsePresenter, AddEmployeeEducationalQualificationResponsePresenter>();

      services.AddScoped<AddEmployeeLeaveRequestMessageValidator, AddEmployeeLeaveRequestMessageValidator>();
      services.AddScoped<IAddEmployeeLeaveUseCase, AddEmployeeLeaveUseCase>();
      services.AddScoped<AddEmployeeLeaveRequestHandler, AddEmployeeLeaveRequestHandler>();
      services.AddScoped<AddEmployeeLeaveResponsePresenter, AddEmployeeLeaveResponsePresenter>();

      services.AddScoped<IClassRoutineOverlappingChecker, ClassRoutineOverlappingChecker>();
      services.AddScoped<AddClassRoutineRequestMessageValidator, AddClassRoutineRequestMessageValidator>();
      services.AddScoped<IAddClassRoutineUseCase, AddClassRoutineUseCase>();
      services.AddScoped<AddClassRoutineRequestHandler, AddClassRoutineRequestHandler>();
      services.AddScoped<AddClassRoutineResponsePresenter, AddClassRoutineResponsePresenter>();

      services.AddScoped<ShowInstituteListRequestMessageValidator, ShowInstituteListRequestMessageValidator>();
      services.AddScoped<IShowInstituteListUseCase, ShowInstituteListUseCase>();
      services.AddScoped<ShowInstituteListRequestHandler, ShowInstituteListRequestHandler>();
      services.AddScoped<ShowInstituteListResponsePresenter, ShowInstituteListResponsePresenter>();

      services.AddScoped<EditInstituteRequestMessageValidator, EditInstituteRequestMessageValidator>();
      services.AddScoped<IEditInstituteUseCase, EditInstituteUseCase>();
      services.AddScoped<EditInstituteRequestHandler, EditInstituteRequestHandler>();
      services.AddScoped<EditInstituteResponsePresenter, EditInstituteResponsePresenter>();

      services.AddScoped<ShowInstituteRequestMessageValidator, ShowInstituteRequestMessageValidator>();
      services.AddScoped<IShowInstituteUseCase, ShowInstituteUseCase>();
      services.AddScoped<ShowInstituteRequestHandler, ShowInstituteRequestHandler>();
      services.AddScoped<ShowInstituteResponsePresenter, ShowInstituteResponsePresenter>();

      services.AddScoped<EditBranchRequestMessageValidator, EditBranchRequestMessageValidator>();
      services.AddScoped<IEditBranchUseCase, EditBranchUseCase>();
      services.AddScoped<EditBranchRequestHandler, EditBranchRequestHandler>();
      services.AddScoped<EditBranchResponsePresenter, EditBranchResponsePresenter>();

      services.AddScoped<ShowBranchRequestMessageValidator, ShowBranchRequestMessageValidator>();
      services.AddScoped<IShowBranchUseCase, ShowBranchUseCase>();
      services.AddScoped<ShowBranchRequestHandler, ShowBranchRequestHandler>();
      services.AddScoped<ShowBranchResponsePresenter, ShowBranchResponsePresenter>();

      services.AddScoped<EditBranchRequestMessageValidator, EditBranchRequestMessageValidator>();
      services.AddScoped<IEditBranchUseCase, EditBranchUseCase>();
      services.AddScoped<EditBranchRequestHandler, EditBranchRequestHandler>();
      services.AddScoped<EditBranchResponsePresenter, EditBranchResponsePresenter>();

      services.AddScoped<ShowBranchRequestMessageValidator, ShowBranchRequestMessageValidator>();
      services.AddScoped<IShowBranchUseCase, ShowBranchUseCase>();
      services.AddScoped<ShowBranchRequestHandler, ShowBranchRequestHandler>();
      services.AddScoped<ShowBranchResponsePresenter, ShowBranchResponsePresenter>();

      services.AddScoped<EditBranchMediumRequestMessageValidator, EditBranchMediumRequestMessageValidator>();
      services.AddScoped<IEditBranchMediumUseCase, EditBranchMediumUseCase>();
      services.AddScoped<EditBranchMediumRequestHandler, EditBranchMediumRequestHandler>();
      services.AddScoped<EditBranchMediumResponsePresenter, EditBranchMediumResponsePresenter>();

      services.AddScoped<ShowBranchMediumRequestMessageValidator, ShowBranchMediumRequestMessageValidator>();
      services.AddScoped<IShowBranchMediumUseCase, ShowBranchMediumUseCase>();
      services.AddScoped<ShowBranchMediumRequestHandler, ShowBranchMediumRequestHandler>();
      services.AddScoped<ShowBranchMediumResponsePresenter, ShowBranchMediumResponsePresenter>();

      services.AddScoped<EditSectionRequestMessageValidator, EditSectionRequestMessageValidator>();
      services.AddScoped<IEditSectionUseCase, EditSectionUseCase>();
      services.AddScoped<EditSectionRequestHandler, EditSectionRequestHandler>();
      services.AddScoped<EditSectionResponsePresenter, EditSectionResponsePresenter>();

      services.AddScoped<ShowSectionRequestMessageValidator, ShowSectionRequestMessageValidator>();
      services.AddScoped<IShowSectionUseCase, ShowSectionUseCase>();
      services.AddScoped<ShowSectionRequestHandler, ShowSectionRequestHandler>();
      services.AddScoped<ShowSectionResponsePresenter, ShowSectionResponsePresenter>();

      services.AddScoped<ShowEmployeeRequestMessageValidator, ShowEmployeeRequestMessageValidator>();
      services.AddScoped<IShowEmployeeUseCase, ShowEmployeeUseCase>();
      services.AddScoped<ShowEmployeeRequestHandler, ShowEmployeeRequestHandler>();
      services.AddScoped<ShowEmployeeResponsePresenter, ShowEmployeeResponsePresenter>();

      services.AddScoped<EditEmployeeRequestMessageValidator, EditEmployeeRequestMessageValidator>();
      services.AddScoped<IEditEmployeeUseCase, EditEmployeeUseCase>();
      services.AddScoped<EditEmployeeRequestHandler, EditEmployeeRequestHandler>();
      services.AddScoped<EditEmployeeResponsePresenter, EditEmployeeResponsePresenter>();

      services.AddScoped<ShowEmployeeListRequestMessageValidator, ShowEmployeeListRequestMessageValidator>();
      services.AddScoped<IShowEmployeeListUseCase, ShowEmployeeListUseCase>();
      services.AddScoped<ShowEmployeeListRequestHandler, ShowEmployeeListRequestHandler>();
      services.AddScoped<ShowEmployeeListResponsePresenter, ShowEmployeeListResponsePresenter>();
      
        services.AddScoped<IMediumNotPreviouslyAddedChecker, MediumNotPreviouslyAddedChecker>();
      services.AddScoped<ShowBranchMediumListRequestMessageValidator, ShowBranchMediumListRequestMessageValidator>();
      services.AddScoped<IShowBranchMediumListUseCase, ShowBranchMediumListUseCase>();
      services.AddScoped<ShowBranchMediumListRequestHandler, ShowBranchMediumListRequestHandler>();
      services.AddScoped<ShowBranchMediumListResponsePresenter, ShowBranchMediumListResponsePresenter>();

      services.AddScoped<ShowEmployeeGradeListRequestMessageValidator, ShowEmployeeGradeListRequestMessageValidator>();
      services.AddScoped<IShowEmployeeGradeListUseCase, ShowEmployeeGradeListUseCase>();
      services.AddScoped<ShowEmployeeGradeListRequestHandler, ShowEmployeeGradeListRequestHandler>();
      services.AddScoped<ShowEmployeeGradeListResponsePresenter, ShowEmployeeGradeListResponsePresenter>();

      services.AddScoped<ShowDesignationListRequestMessageValidator, ShowDesignationListRequestMessageValidator>();
      services.AddScoped<IShowDesignationListUseCase, ShowDesignationListUseCase>();
      services.AddScoped<ShowDesignationListRequestHandler, ShowDesignationListRequestHandler>();
      services.AddScoped<ShowDesignationListResponsePresenter, ShowDesignationListResponsePresenter>();

      services.AddScoped<ShowJobTypeListRequestMessageValidator, ShowJobTypeListRequestMessageValidator>();
      services.AddScoped<IShowJobTypeListUseCase, ShowJobTypeListUseCase>();
      services.AddScoped<ShowJobTypeListRequestHandler, ShowJobTypeListRequestHandler>();
      services.AddScoped<ShowJobTypeListResponsePresenter, ShowJobTypeListResponsePresenter>();
      services.AddScoped<ShowJobTypeRequestMessageValidator, ShowJobTypeRequestMessageValidator>();
      services.AddScoped<IShowJobTypeUseCase, ShowJobTypeUseCase>();
      services.AddScoped<ShowJobTypeRequestHandler, ShowJobTypeRequestHandler>();
      services.AddScoped<ShowJobTypeResponsePresenter, ShowJobTypeResponsePresenter>();
      services.AddScoped<EditJobTypeRequestMessageValidator, EditJobTypeRequestMessageValidator>();
      services.AddScoped<IEditJobTypeUseCase, EditJobTypeUseCase>();
      services.AddScoped<EditJobTypeRequestHandler, EditJobTypeRequestHandler>();
      services.AddScoped<EditJobTypeResponsePresenter, EditJobTypeResponsePresenter>();

      services.AddScoped<ShowClassRoutineListRequestMessageValidator, ShowClassRoutineListRequestMessageValidator>();
      services.AddScoped<IShowClassRoutineListUseCase, ShowClassRoutineListUseCase>();
      services.AddScoped<ShowClassRoutineListRequestHandler, ShowClassRoutineListRequestHandler>();
      services.AddScoped<ShowClassRoutineListResponsePresenter, ShowClassRoutineListResponsePresenter>();
      
      services.AddScoped<AddBreakTimeRequestMessageValidator, AddBreakTimeRequestMessageValidator>();
      services.AddScoped<IAddBreakTimeUseCase, AddBreakTimeUseCase>();
      services.AddScoped<IUniqueBreakTimePropertyChecker, UniqueBreakTimePropertyChecker>();
      services.AddScoped<AddBreakTimeRequestHandler, AddBreakTimeRequestHandler>();
      services.AddScoped<AddBreakTimeResponsePresenter, AddBreakTimeResponsePresenter>();

      services.AddScoped<EditEmployeeGradeRequestMessageValidator, EditEmployeeGradeRequestMessageValidator>();
      services.AddScoped<IEditEmployeeGradeUseCase, EditEmployeeGradeUseCase>();
      services.AddScoped<EditEmployeeGradeRequestHandler, EditEmployeeGradeRequestHandler>();
      services.AddScoped<EditEmployeeGradeResponsePresenter, EditEmployeeGradeResponsePresenter>();

      services.AddScoped<ShowEmployeeGradeRequestMessageValidator, ShowEmployeeGradeRequestMessageValidator>();
      services.AddScoped<IShowEmployeeGradeUseCase, ShowEmployeeGradeUseCase>();
      services.AddScoped<ShowEmployeeGradeRequestHandler, ShowEmployeeGradeRequestHandler>();
      services.AddScoped<ShowEmployeeGradeResponsePresenter, ShowEmployeeGradeResponsePresenter>();

      services.AddScoped<EditLeaveTypeRequestMessageValidator, EditLeaveTypeRequestMessageValidator>();
      services.AddScoped<IEditLeaveTypeUseCase, EditLeaveTypeUseCase>();
      services.AddScoped<EditLeaveTypeRequestHandler, EditLeaveTypeRequestHandler>();
      services.AddScoped<EditLeaveTypeResponsePresenter, EditLeaveTypeResponsePresenter>();

      services.AddScoped<ShowLeaveTypeRequestMessageValidator, ShowLeaveTypeRequestMessageValidator>();
      services.AddScoped<IShowLeaveTypeUseCase, ShowLeaveTypeUseCase>();
      services.AddScoped<ShowLeaveTypeRequestHandler, ShowLeaveTypeRequestHandler>();
      services.AddScoped<ShowLeaveTypeResponsePresenter, ShowLeaveTypeResponsePresenter>();

      services.AddScoped<ShowLeaveTypeListRequestMessageValidator, ShowLeaveTypeListRequestMessageValidator>();
      services.AddScoped<IShowLeaveTypeListUseCase, ShowLeaveTypeListUseCase>();
      services.AddScoped<ShowLeaveTypeListRequestHandler, ShowLeaveTypeListRequestHandler>();
      services.AddScoped<ShowLeaveTypeListResponsePresenter, ShowLeaveTypeListResponsePresenter>();

      services.AddScoped<EditPublicHolidayRequestMessageValidator, EditPublicHolidayRequestMessageValidator>();
      services.AddScoped<IEditPublicHolidayUseCase, EditPublicHolidayUseCase>();
      services.AddScoped<EditPublicHolidayRequestHandler, EditPublicHolidayRequestHandler>();
      services.AddScoped<EditPublicHolidayResponsePresenter, EditPublicHolidayResponsePresenter>();

      services.AddScoped<ShowPublicHolidayRequestMessageValidator, ShowPublicHolidayRequestMessageValidator>();
      services.AddScoped<IShowPublicHolidayUseCase, ShowPublicHolidayUseCase>();
      services.AddScoped<ShowPublicHolidayRequestHandler, ShowPublicHolidayRequestHandler>();
      services.AddScoped<ShowPublicHolidayResponsePresenter, ShowPublicHolidayResponsePresenter>();

      services.AddScoped<ShowPublicHolidayListRequestMessageValidator, ShowPublicHolidayListRequestMessageValidator>();
      services.AddScoped<IShowPublicHolidayListUseCase, ShowPublicHolidayListUseCase>();
      services.AddScoped<ShowPublicHolidayListRequestHandler, ShowPublicHolidayListRequestHandler>();
      services.AddScoped<ShowPublicHolidayListResponsePresenter, ShowPublicHolidayListResponsePresenter>();

      services.AddScoped<ILeaveAmountValidityChecker, LeaveAmountValidityChecker>();
      
      services.AddScoped<AddAccountHeadRequestMessageValidator, AddAccountHeadRequestMessageValidator>();
      services.AddScoped<IAddAccountHeadUseCase, AddAccountHeadUseCase>();
      services.AddScoped<IUniqueAccountHeadPropertyChecker, UniqueAccountHeadPropertyChecker>();
      services.AddScoped<AddAccountHeadRequestHandler, AddAccountHeadRequestHandler>();
      services.AddScoped<AddAccountHeadResponsePresenter, AddAccountHeadResponsePresenter>();
      services.AddScoped<ShowAccountHeadListRequestMessageValidator, ShowAccountHeadListRequestMessageValidator>();
      services.AddScoped<IShowAccountHeadListUseCase, ShowAccountHeadListUseCase>();
      services.AddScoped<ShowAccountHeadListRequestHandler, ShowAccountHeadListRequestHandler>();
      services.AddScoped<ShowAccountHeadListResponsePresenter, ShowAccountHeadListResponsePresenter>();
      services.AddScoped<ShowAcademicAccountHeadListRequestMessageValidator, ShowAcademicAccountHeadListRequestMessageValidator>();
      services.AddScoped<IShowAcademicAccountHeadListUseCase, ShowAcademicAccountHeadListUseCase>();
      services.AddScoped<ShowAcademicAccountHeadListRequestHandler, ShowAcademicAccountHeadListRequestHandler>();
      services.AddScoped<ShowAccountHeadRequestMessageValidator, ShowAccountHeadRequestMessageValidator>();
      services.AddScoped<IShowAccountHeadUseCase, ShowAccountHeadUseCase>();
      services.AddScoped<ShowAccountHeadRequestHandler, ShowAccountHeadRequestHandler>();
      services.AddScoped<ShowAccountHeadResponsePresenter, ShowAccountHeadResponsePresenter>();
      services.AddScoped<EditAccountHeadRequestMessageValidator, EditAccountHeadRequestMessageValidator>();
      services.AddScoped<IEditAccountHeadUseCase, EditAccountHeadUseCase>();
      services.AddScoped<EditAccountHeadRequestHandler, EditAccountHeadRequestHandler>();
      services.AddScoped<EditAccountHeadResponsePresenter, EditAccountHeadResponsePresenter>();
      
      services.AddScoped<AddAcademicFeeRequestMessageValidator, AddAcademicFeeRequestMessageValidator>();
      services.AddScoped<IAddAcademicFeeUseCase, AddAcademicFeeUseCase>();
      services.AddScoped<AddAcademicFeeRequestHandler, AddAcademicFeeRequestHandler>();
      services.AddScoped<AddAcademicFeeResponsePresenter, AddAcademicFeeResponsePresenter>();

      services.AddScoped<AddEmployeeImageRequestMessageValidator, AddEmployeeImageRequestMessageValidator>();
      services.AddScoped<IAddEmployeeImageUseCase, AddEmployeeImageUseCase>();
      services.AddScoped<AddEmployeeImageRequestHandler, AddEmployeeImageRequestHandler>();
      
      services.AddScoped<AddContactInfoRequestMessageValidator, AddContactInfoRequestMessageValidator>();
      services.AddScoped<IAddContactInfoUseCase, AddContactInfoUseCase>();
      services.AddScoped<AddContactInfoRequestHandler, AddContactInfoRequestHandler>();
      services.AddScoped<AddContactInfoResponsePresenter, AddContactInfoResponsePresenter>();
      
      services.AddScoped<ShowContactInfoRequestMessageValidator, ShowContactInfoRequestMessageValidator>();
      services.AddScoped<IShowContactInfoUseCase, ShowContactInfoUseCase>();
      services.AddScoped<ShowContactInfoRequestHandler, ShowContactInfoRequestHandler>();
      services.AddScoped<ShowContactInfoResponsePresenter, ShowContactInfoResponsePresenter>();
      
      services.AddScoped<AddPresentAddressInContactInfoRequestMessageValidator, AddPresentAddressInContactInfoRequestMessageValidator>();
      services.AddScoped<IAddPresentAddressInContactInfoUseCase, AddPresentAddressInContactInfoUseCase>();
      services.AddScoped<AddPresentAddressInContactInfoRequestHandler, AddPresentAddressInContactInfoRequestHandler>();
      services.AddScoped<AddPresentAddressInContactInfoResponsePresenter, AddPresentAddressInContactInfoResponsePresenter>();
      
      services.AddScoped<AddPermanentAddressInContactInfoRequestMessageValidator, AddPermanentAddressInContactInfoRequestMessageValidator>();
      services.AddScoped<IPresentAddressInContactInfoChecker, PresentAddressInContactInfoChecker>();
      services.AddScoped<IAddPermanentAddressInContactInfoUseCase, AddPermanentAddressInContactInfoUseCase>();
      services.AddScoped<AddPermanentAddressInContactInfoRequestHandler, AddPermanentAddressInContactInfoRequestHandler>();
      services.AddScoped<AddPermanentAddressInContactInfoResponsePresenter, AddPermanentAddressInContactInfoResponsePresenter>();
      
      services.AddScoped<AddStudentSectionRequestMessageValidator, AddStudentSectionRequestMessageValidator>();
      services.AddScoped<IAddStudentSectionUseCase, AddStudentSectionUseCase>();
      services.AddScoped<AddStudentSectionRequestHandler, AddStudentSectionRequestHandler>();
      services.AddScoped<AddStudentSectionResponsePresenter, AddStudentSectionResponsePresenter>();
      services.AddScoped<ShowBranchMediumSectionListRequestHandler, ShowBranchMediumSectionListRequestHandler>();
      services.AddScoped<ShowBranchMediumSectionListRequestMessageValidator, ShowBranchMediumSectionListRequestMessageValidator>();
      services.AddScoped<IShowBranchMediumSectionListUseCase, ShowBranchMediumSectionListUseCase>();
      
      services.AddScoped<ShowEmployeeLeaveListRequestMessageValidator, ShowEmployeeLeaveListRequestMessageValidator>();
      services.AddScoped<IShowEmployeeLeaveListUseCase, ShowEmployeeLeaveListUseCase>();
      services.AddScoped<ShowEmployeeLeaveListRequestHandler, ShowEmployeeLeaveListRequestHandler>();
      services.AddScoped<ShowEmployeeLeaveListResponsePresenter, ShowEmployeeLeaveListResponsePresenter>();

      services.AddScoped<ApproveLeaveRequestMessageValidator, ApproveLeaveRequestMessageValidator>();
      services.AddScoped<IApproveLeaveUseCase, ApproveLeaveUseCase>();
      services.AddScoped<ApproveLeaveRequestHandler, ApproveLeaveRequestHandler>();

      services.AddScoped<AddEmployeeAddressRequestMessageValidator, AddEmployeeAddressRequestMessageValidator>();
      services.AddScoped<IAddEmployeeAddressUseCase, AddEmployeeAddressUseCase>();
      services.AddScoped<AddEmployeeAddressRequestHandler, AddEmployeeAddressRequestHandler>();
      services.AddScoped<AddEmployeeAddressResponsePresenter, AddEmployeeAddressResponsePresenter>();

      services.AddScoped<AddStudentContactPersonRequestMessageValidator, AddStudentContactPersonRequestMessageValidator>();
      services.AddScoped<IAddStudentContactPersonUseCase, AddStudentContactPersonUseCase>();
      services.AddScoped<AddStudentContactPersonRequestHandler, AddStudentContactPersonRequestHandler>();
      services.AddScoped<AddStudentContactPersonResponsePresenter, AddStudentContactPersonResponsePresenter>();

      services.AddScoped<AddStudentContactPersonImageRequestMessageValidator, AddStudentContactPersonImageRequestMessageValidator>();
      services.AddScoped<IAddStudentContactPersonImageUseCase, AddStudentContactPersonImageUseCase>();
      services.AddScoped<AddStudentContactPersonImageRequestHandler, AddStudentContactPersonImageRequestHandler>();

      services.AddScoped<AddStudentAttendanceRequestMessageValidator, AddStudentAttendanceRequestMessageValidator>();
      services.AddScoped<IAddStudentAttendanceUseCase, AddStudentAttendanceUseCase>();
      services.AddScoped<AddStudentAttendanceRequestHandler, AddStudentAttendanceRequestHandler>();
      services.AddScoped<AddStudentAttendanceResponsePresenter, AddStudentAttendanceResponsePresenter>();

      services.AddScoped<AddEmployeeAttendanceRequestMessageValidator, AddEmployeeAttendanceRequestMessageValidator>();
      services.AddScoped<IAddEmployeeAttendanceUseCase, AddEmployeeAttendanceUseCase>();
      services.AddScoped<AddEmployeeAttendanceRequestHandler, AddEmployeeAttendanceRequestHandler>();
      services.AddScoped<AddEmployeeAttendanceResponsePresenter, AddEmployeeAttendanceResponsePresenter>();
      services.AddScoped<ShowCurrentMonthAttendanceListRequestMessageValidator, ShowCurrentMonthAttendanceListRequestMessageValidator>();
      services.AddScoped<IShowCurrentMonthAttendanceListUseCase, ShowCurrentMonthAttendanceListUseCase>();
      services.AddScoped<ShowCurrentMonthAttendanceListRequestHandler, ShowCurrentMonthAttendanceListRequestHandler>();
      services.AddScoped<ShowEmployeeAttendanceListResponsePresenter, ShowEmployeeAttendanceListResponsePresenter>();

      services.AddScoped<ShowEmployeeAttendanceRequestMessageValidator, ShowEmployeeAttendanceRequestMessageValidator>();
      services.AddScoped<IShowEmployeeAttendanceUseCase, ShowEmployeeAttendanceUseCase>();
      services.AddScoped<ShowEmployeeAttendanceRequestHandler, ShowEmployeeAttendanceRequestHandler>();
      services.AddScoped<ShowEmployeeAttendanceResponsePresenter, ShowEmployeeAttendanceResponsePresenter>();

      services.AddScoped<EditEmployeeAttendanceRequestMessageValidator, EditEmployeeAttendanceRequestMessageValidator>();
      services.AddScoped<IEditEmployeeAttendanceUseCase, EditEmployeeAttendanceUseCase>();
      services.AddScoped<EditEmployeeAttendanceRequestHandler, EditEmployeeAttendanceRequestHandler>();
      services.AddScoped<EditEmployeeAttendanceResponsePresenter, EditEmployeeAttendanceResponsePresenter>();

      services.AddScoped<EditResultGradeRequestMessageValidator, EditResultGradeRequestMessageValidator>();
      services.AddScoped<IEditResultGradeUseCase, EditResultGradeUseCase>();
      services.AddScoped<EditResultGradeRequestHandler, EditResultGradeRequestHandler>();
      services.AddScoped<EditResultGradeResponsePresenter, EditResultGradeResponsePresenter>();

      services.AddScoped<ShowResultGradeListRequestMessageValidator, ShowResultGradeListRequestMessageValidator>();
      services.AddScoped<IShowResultGradeListUseCase, ShowResultGradeListUseCase>();
      services.AddScoped<ShowResultGradeListRequestHandler, ShowResultGradeListRequestHandler>();
      services.AddScoped<ShowResultGradeListResponsePresenter, ShowResultGradeListResponsePresenter>();

      services.AddScoped<ShowStudentAttendanceListResponsePresenter, ShowStudentAttendanceListResponsePresenter>();

      services.AddScoped<ShowStudentAttendanceRequestMessageValidator, ShowStudentAttendanceRequestMessageValidator>();
      services.AddScoped<IShowStudentAttendanceUseCase, ShowStudentAttendanceUseCase>();
      services.AddScoped<ShowStudentAttendanceRequestHandler, ShowStudentAttendanceRequestHandler>();
      services.AddScoped<ShowStudentAttendanceResponsePresenter, ShowStudentAttendanceResponsePresenter>();

      services.AddScoped<EditStudentAttendanceRequestMessageValidator, EditStudentAttendanceRequestMessageValidator>();
      services.AddScoped<IEditStudentAttendanceUseCase, EditStudentAttendanceUseCase>();
      services.AddScoped<EditStudentAttendanceRequestHandler, EditStudentAttendanceRequestHandler>();
      services.AddScoped<EditStudentAttendanceResponsePresenter, EditStudentAttendanceResponsePresenter>();
      
      services.AddScoped<IShiftExistanceChecker, ShiftExistanceChecker>();
      services.AddScoped<IMediumExistanceChecker, MediumExistanceChecker>();

      services.AddScoped<ITransactionEntryTypeDecider, TransactionEntryDecider>();
      services.AddScoped<ITransactionEntryValueDecider, TransactionEntryDecider>();
 
      services.AddScoped<ImportCOAFromMasterRequestMessageValidator, ImportCOAFromMasterRequestMessageValidator>();
      services.AddScoped<IImportCOAFromMasterUseCase, ImportCOAFromMasterUseCase>();
      services.AddScoped<ImportCOAFromMasterRequestHandler, ImportCOAFromMasterRequestHandler>();
      services.AddScoped<IBranchMediumAccountsHeadPropertyChecker, BranchMediumAccountsHeadPropertyChecker>();

      services.AddScoped<ShowBranchMediumAccountsHeadListRequestMessageValidator, ShowBranchMediumAccountsHeadListRequestMessageValidator>();
      services.AddScoped<IShowBranchMediumAccountsHeadListUseCase, ShowBranchMediumAccountsHeadListUseCase>();
      services.AddScoped<ShowBranchMediumAccountsHeadListRequestHandler, ShowBranchMediumAccountsHeadListRequestHandler>();
      services.AddScoped<ShowBranchMediumAccountsHeadListResponsePresenter, ShowBranchMediumAccountsHeadListResponsePresenter>();

      services.AddScoped<ITransactionNoTrackingDataManager, TransactionNoTrackingDataManager>();
      services.AddScoped<IAddGenericJournalTransactionUseCase, AddGenericJournalTransactionUseCase>();

      services.AddScoped<AddJournalTransactionRequestMessageValidator,
        AddJournalTransactionRequestMessageValidator>();
      services.AddScoped<IAddJournalTransactionUseCase, AddJournalTransactionUseCase>();
      services.AddScoped<AddJournalTransactionRequestHandler, AddJournalTransactionRequestHandler>();

      services.AddScoped<AddCashVoucherJournalTransactionRequestMessageValidator,
        AddCashVoucherJournalTransactionRequestMessageValidator>();
      services.AddScoped<IAddCashVoucherJournalTransactionUseCase,
        AddCashVoucherJournalTransactionUseCase>();
      services.AddScoped<AddCashVoucherJournalTransactionRequestHandler,
        AddCashVoucherJournalTransactionRequestHandler>();

      services.AddScoped<AddBankVoucherJournalTransactionRequestMessageValidator,
        AddBankVoucherJournalTransactionRequestMessageValidator>();
      services.AddScoped<IAddBankVoucherJournalTransactionUseCase,
        AddBankVoucherJournalTransactionUseCase>();
      services.AddScoped<AddBankVoucherJournalTransactionRequestHandler,
        AddBankVoucherJournalTransactionRequestHandler>();
      
      services.AddScoped<GetSubjectsToAddInClassRequestMessageValidator, GetSubjectsToAddInClassRequestMessageValidator>();
      services.AddScoped<IGetSubjectsToAddInClassUseCase, GetSubjectsToAddInClassUseCase>();
      services.AddScoped<GetSubjectsToAddInClassRequestHandler, GetSubjectsToAddInClassRequestHandler>();
      
      services.AddScoped<GetAddedSubjectsOfClassRequestMessageValidator, GetAddedSubjectsOfClassRequestMessageValidator>();
      services.AddScoped<IGetAddedSubjectsOfClassUseCase, GetAddedSubjectsOfClassUseCase>();
      services.AddScoped<GetAddedSubjectsOfClassRequestHandler, GetAddedSubjectsOfClassRequestHandler>();

      services.AddScoped<ShowAutoPostingConfigurationListRequestMessageValidator, ShowAutoPostingConfigurationListRequestMessageValidator>();
      services.AddScoped<IShowAutoPostingConfigurationListUseCase, ShowAutoPostingConfigurationListUseCase>();
      services.AddScoped<ShowAutoPostingConfigurationListRequestHandler, ShowAutoPostingConfigurationListRequestHandler>();
      services.AddScoped<ShowBankAccountChildListResponsePresenter, ShowBankAccountChildListResponsePresenter>();
      
      
      services.AddScoped<GetBranchMediumAccountHeadsRequestMessageValidator, GetBranchMediumAccountHeadsRequestMessageValidator>();
      services.AddScoped<IGetBranchMediumAccountHeadsUseCase, GetBranchMediumAccountHeadsUseCase>();
      services.AddScoped<GetBranchMediumAccountHeadsRequestHandler, GetBranchMediumAccountHeadsRequestHandler>();

      services.AddScoped<ShowTrialBalanceRequestMessageValidator, ShowTrialBalanceRequestMessageValidator>();
      services.AddScoped<IShowTrialBalanceUseCase, ShowTrialBalanceUseCase>();
      services.AddScoped<ShowTrialBalanceRequestHandler, ShowTrialBalanceRequestHandler>();
      
      services.AddScoped<ShowIncomeStatementRequestMessageValidator, ShowIncomeStatementRequestMessageValidator>();
      services.AddScoped<IShowIncomeStatementUseCase, ShowIncomeStatementUseCase>();
      services.AddScoped<ShowIncomeStatementRequestHandler, ShowIncomeStatementRequestHandler>();

      services.AddScoped<ShowBalanceSheetRequestMessageValidator, ShowBalanceSheetRequestMessageValidator>();
      services.AddScoped<IShowBalanceSheetUseCase, ShowBalanceSheetUseCase>();
      services.AddScoped<ShowBalanceSheetRequestHandler, ShowBalanceSheetRequestHandler>();
      services.AddScoped<ShowBalanceSheetTransactionResponsePresenter,
      ShowBalanceSheetTransactionResponsePresenter>();
      
      services.AddScoped<GetAddClassRoutineViewModelDataRequestMessageValidator, GetAddClassRoutineViewModelDataRequestMessageValidator>();
      services.AddScoped<IGetAddClassRoutineViewModelDataUseCase, GetAddClassRoutineViewModelDataUseCase>();
      services.AddScoped<GetAddClassRoutineViewModelDataRequestHandler, GetAddClassRoutineViewModelDataRequestHandler>();

      services.AddScoped<ShowTrialBalanceTransactionResponsePresenter, ShowTrialBalanceTransactionResponsePresenter>();

      services.AddScoped<ShowTransactionRequestMessageValidator, ShowTransactionRequestMessageValidator>();
      services.AddScoped<IShowTransactionUseCase, ShowTransactionUseCase>();
      services.AddScoped<ShowTransactionRequestHandler, ShowTransactionRequestHandler>();
      services.AddScoped<ShowTransactionResponsePresenter, ShowTransactionResponsePresenter>();
      
      services.AddScoped<ShowIncomeStatementTransactionResponsePresenter, ShowIncomeStatementTransactionResponsePresenter>();
      
      services.AddScoped<GetEmployeeLeavesRequestMessageValidator, GetEmployeeLeavesRequestMessageValidator>();
      services.AddScoped<IGetEmployeeLeavesUseCase, GetEmployeeLeavesUseCase>();
      services.AddScoped<GetEmployeeLeavesRequestHandler, GetEmployeeLeavesRequestHandler>();
      
      services.AddScoped<AddStudentAddressRequestMessageValidator, AddStudentAddressRequestMessageValidator>();
      services.AddScoped<IAddStudentAddressUseCase, AddStudentAddressUseCase>();
      services.AddScoped<AddStudentAddressRequestHandler, AddStudentAddressRequestHandler>();
      services.AddScoped<AddStudentAddressResponsePresenter, AddStudentAddressResponsePresenter>();
      
      services.AddScoped<AddBankRequestMessageValidator, AddBankRequestMessageValidator>();
      services.AddScoped<IAddBankUseCase, AddBankUseCase>();
      services.AddScoped<AddBankRequestHandler, AddBankRequestHandler>();
      services.AddScoped<AddBankInfoResponsePresenter, AddBankInfoResponsePresenter>();
      services.AddScoped<IUniqueBankInfoPropertyChecker, UniqueBankInfoPropertyChecker>();
      
      services.AddScoped<AddBankBranchRequestMessageValidator, AddBankBranchRequestMessageValidator>();
      services.AddScoped<IAddBankBranchUseCase, AddBankBranchUseCase>();
      services.AddScoped<AddBankBranchRequestHandler, AddBankBranchRequestHandler>();
      services.AddScoped<AddBankBranchResponsePresenter, AddBankBranchResponsePresenter>();
      services.AddScoped<IUniqueBankInfoPropertyChecker, UniqueBankInfoPropertyChecker>();
      
      services.AddScoped<ShowBankRequestMessageValidator, ShowBankRequestMessageValidator>();
      services.AddScoped<IShowBankUseCase, ShowBankUseCase>();
      services.AddScoped<ShowBankRequestHandler, ShowBankRequestHandler>();
      services.AddScoped<ShowBankInfoResponsePresenter, ShowBankInfoResponsePresenter>();
      
      services.AddScoped<ShowBankBranchRequestMessageValidator, ShowBankBranchRequestMessageValidator>();
      services.AddScoped<IShowBankBranchUseCase, ShowBankBranchUseCase>();
      services.AddScoped<ShowBankBranchRequestHandler, ShowBankBranchRequestHandler>();
      services.AddScoped<ShowBankBranchResponsePresenter, ShowBankBranchResponsePresenter>();

      services.AddScoped<AccountDisplayResponsePresenter, AccountDisplayResponsePresenter>();

      services.AddScoped<ShowBranchMediumAccountHeadRequestMessageValidator, ShowBranchMediumAccountHeadRequestMessageValidator>();
      services.AddScoped<IShowBranchMediumAccountHeadUseCase, ShowBranchMediumAccountHeadUseCase>();
      services.AddScoped<ShowBranchMediumAccountHeadRequestHandler, ShowBranchMediumAccountHeadRequestHandler>();

      services.AddScoped<AddBranchMediumAccountHeadRequestMessageValidator, AddBranchMediumAccountHeadRequestMessageValidator>();
      services.AddScoped<IAddBranchMediumAccountHeadUseCase, AddBranchMediumAccountHeadUseCase>();
      services.AddScoped<AddBranchMediumAccountHeadRequestHandler, AddBranchMediumAccountHeadRequestHandler>();
      
      services.AddScoped<AddBankAccountRequestMessageValidator, AddBankAccountRequestMessageValidator>();
      services.AddScoped<IAddBankAccountUseCase, AddBankAccountUseCase>();
      services.AddScoped<AddBankAccountRequestHandler, AddBankAccountRequestHandler>();
      services.AddScoped<AddBankAccountResponsePresenter, AddBankAccountResponsePresenter>();
      
      services.AddScoped<IBankAccountAddPropertyChecker, BankAccountAddPropertyChecker>();

      services.AddScoped<ShowTermListRequestMessageValidator, ShowTermListRequestMessageValidator>();
      services.AddScoped<IShowTermListUseCase, ShowTermListUseCase>();
      services.AddScoped<ShowTermListRequestHandler, ShowTermListRequestHandler>();
      services.AddScoped<ShowTermListResponsePresenter, ShowTermListResponsePresenter>();

      services.AddScoped<AddCommitteeMemberRequestMessageValidator, AddCommitteeMemberRequestMessageValidator>();
      services.AddScoped<IAddCommitteeMemberUseCase, AddCommitteeMemberUseCase>();
      services.AddScoped<AddCommitteeMemberRequestHandler, AddCommitteeMemberRequestHandler>();
      services.AddScoped<AddCommitteeMemberResponsePresenter, AddCommitteeMemberResponsePresenter>();
      services.AddScoped<IUniqueCommitteeMemberPropertyChecker, UniqueCommitteeMemberPropertyChecker>();

      services.AddScoped<ShowCommitteeMemberRequestMessageValidator, ShowCommitteeMemberRequestMessageValidator>();
      services.AddScoped<IShowCommitteeMemberUseCase, ShowCommitteeMemberUseCase>();
      services.AddScoped<ShowCommitteeMemberRequestHandler, ShowCommitteeMemberRequestHandler>();
      services.AddScoped<ShowCommitteeMemberResponsePresenter, ShowCommitteeMemberResponsePresenter>();

      services.AddScoped<AddOrChangeCommitteeMemberImageRequestMessageValidator, AddOrChangeCommitteeMemberImageRequestMessageValidator>();
      services.AddScoped<IAddOrChangeCommitteeMemberImageUseCase, AddOrChangeCommitteeMemberImageUseCase>();
      services.AddScoped<AddOrChangeCommitteeMemberImageRequestHandler, AddOrChangeCommitteeMemberImageRequestHandler>();

      services.AddScoped<ShowClassTestListRequestMessageValidator, ShowClassTestListRequestMessageValidator>();
      services.AddScoped<IShowClassTestListUseCase, ShowClassTestListUseCase>();
      services.AddScoped<ShowClassTestListRequestHandler, ShowClassTestListRequestHandler>();
      services.AddScoped<ShowClassTestListResponsePresenter, ShowClassTestListResponsePresenter>();
      services.AddScoped<ShowTermTestEventListResponsePresenter, ShowTermTestEventListResponsePresenter>();
      
      services.AddScoped<GetAcademicFeesRequestMessageValidator, GetAcademicFeesRequestMessageValidator>();
      services.AddScoped<IGetAcademicFeesUseCase, GetAcademicFeesUseCase>();
      services.AddScoped<GetAcademicFeesRequestHandler, GetAcademicFeesRequestHandler>();
      services.AddScoped<IAddAcademicFeePropertyChecker, AddAcademicFeePropertyChecker>();

      services.AddScoped<DisplayAddTermTestPageRequestMessageValidator,
        DisplayAddTermTestPageRequestMessageValidator>();
      services.AddScoped<IDisplayAddTermTestPageUseCase, DisplayAddTermTestPageUseCase>();
      services.AddScoped<DisplayAddTermTestPageRequestHandler, DisplayAddTermTestPageRequestHandler>();
      
      services.AddScoped<AddWaiverRequestMessageValidator, AddWaiverRequestMessageValidator>();
      services.AddScoped<IAddWaiverUseCase, AddWaiverUseCase>();
      services.AddScoped<AddWaiverRequestHandler, AddWaiverRequestHandler>();
      services.AddScoped<AddWaiverResponsePresenter, AddWaiverResponsePresenter>();
      
      services.AddScoped<GetStudentWaiversRequestMessageValidator, GetStudentWaiversRequestMessageValidator>();
      services.AddScoped<IGetStudentWaiversUseCase, GetStudentWaiversUseCase>();
      services.AddScoped<GetStudentWaiversRequestHandler, GetStudentWaiversRequestHandler>();

      services.AddScoped<ShowTermTestListRequestMessageValidator, ShowTermTestListRequestMessageValidator>();
      services.AddScoped<IShowTermTestListUseCase, ShowTermTestListUseCase>();
      services.AddScoped<ShowTermTestListRequestHandler, ShowTermTestListRequestHandler>();
      services.AddScoped<ShowTermTestListResponsePresenter, ShowTermTestListResponsePresenter>();

      
      services.AddScoped<GetBankAccountAccountHeadsRequestMessageValidator, GetBankAccountAccountHeadsRequestMessageValidator>();
      services.AddScoped<IGetBankAccountAccountHeadsUseCase, GetBankAccountAccountHeadsUseCase>();
      services.AddScoped<GetBankAccountAccountHeadsRequestHandler, GetBankAccountAccountHeadsRequestHandler>();
      
      services.AddScoped<FeeCollectionRequestMessageValidator, FeeCollectionRequestMessageValidator>();
      services.AddScoped<IFeeCollectionUseCase, FeeCollectionUseCase>();
      services.AddScoped<FeeCollectionRequestHandler, FeeCollectionRequestHandler>();
      services.AddScoped<FeeCollectionResponsePresenter, FeeCollectionResponsePresenter>();
      services.AddScoped<IFeeCollectionValidationChecker, FeeCollectionValidationChecker>();

      services.AddScoped<ShowTransactionListRequestMessageValidator, ShowTransactionListRequestMessageValidator>();
      services.AddScoped<IShowTransactionListUseCase, ShowTransactionListUseCase>();
      services.AddScoped<ShowTransactionListRequestHandler, ShowTransactionListRequestHandler>();
      services.AddScoped<ShowTransactionListResponsePresenter, ShowTransactionListResponsePresenter>();

      services.AddScoped<DisplayBranchMediumRequestMessageValidator, DisplayBranchMediumRequestMessageValidator>();
      services.AddScoped<IDisplayBranchMediumUseCase, DisplayBranchMediumUseCase>();
      services.AddScoped<DisplayBranchMediumRequestHandler, DisplayBranchMediumRequestHandler>();
      services.AddScoped<DisplayBranchMediumResponsePresenter, DisplayBranchMediumResponsePresenter>();
      
      services.AddScoped<FilterTermTestAddMarkFieldsRequestMessageValidator, FilterTermTestAddMarkFieldsRequestMessageValidator>();
      services.AddScoped<IFilterTermTestAddMarkFieldsUseCase, FilterTermTestAddMarkFieldsUseCase>();
      services.AddScoped<FilterTermTestAddMarkFieldsRequestHandler, FilterTermTestAddMarkFieldsRequestHandler>();
      
      services.AddScoped<GetTermTestAddMarkObjectsRequestMessageValidator, GetTermTestAddMarkObjectsRequestMessageValidator>();
      services.AddScoped<IGetTermTestAddMarkObjectsUseCase, GetTermTestAddMarkObjectsUseCase>();
      services.AddScoped<GetTermTestAddMarkObjectsRequestHandler, GetTermTestAddMarkObjectsRequestHandler>();
      
      services.AddScoped<AddTermTestMarksRequestMessageValidator, AddTermTestMarksRequestMessageValidator>();
      services.AddScoped<IAddTermTestMarksUseCase, AddTermTestMarksUseCase>();
      services.AddScoped<AddTermTestMarksRequestHandler, AddTermTestMarksRequestHandler>();
      services.AddScoped<AddTermTestMarksResponsePresenter, AddTermTestMarksResponsePresenter>();
      
      services.AddScoped<GetTermTestMarksRequestMessageValidator, GetTermTestMarksRequestMessageValidator>();
      services.AddScoped<IGetTermTestMarksUseCase, GetTermTestMarksUseCase>();
      services.AddScoped<GetTermTestMarksRequestHandler, GetTermTestMarksRequestHandler>();
      
      services.AddScoped<GetBranchMediumSessionsRequestMessageValidator, GetBranchMediumSessionsRequestMessageValidator>();
      services.AddScoped<IGetBranchMediumSessionsUseCase, GetBranchMediumSessionsUseCase>();
      services.AddScoped<GetBranchMediumSessionsRequestHandler, GetBranchMediumSessionsRequestHandler>();
      
      services.AddScoped<GetExamTermsRequestMessageValidator, GetExamTermsRequestMessageValidator>();
      services.AddScoped<IGetExamTermsUseCase, GetExamTermsUseCase>();
      services.AddScoped<GetExamTermsRequestHandler, GetExamTermsRequestHandler>();
      
      services.AddScoped<GetExamTermMarksRequestMessageValidator, GetExamTermMarksRequestMessageValidator>();
      services.AddScoped<IGetExamTermMarksUseCase, GetExamTermMarksUseCase>();
      services.AddScoped<GetExamTermMarksRequestHandler, GetExamTermMarksRequestHandler>();

      services.AddScoped<GetClassTestMarkRequestMessageValidator, GetClassTestMarkRequestMessageValidator>();
      services.AddScoped<IGetClassTestMarkUseCase, GetClassTestMarkUseCase>();
      services.AddScoped<GetClassTestMarksRequestHandler, GetClassTestMarksRequestHandler>();
      
      services.AddScoped<GetOptionalSubjectListRequestMessageValidator, GetOptionalSubjectListRequestMessageValidator>();
      services.AddScoped<IGetOptionalSubjectListUseCase, GetOptionalSubjectListUseCase>();
      services.AddScoped<GetOptionalSubjectListRequestHandler, GetOptionalSubjectListRequestHandler>();
      
      services.AddScoped<AddOptionalSubjectRequestMessageValidator, AddOptionalSubjectRequestMessageValidator>();
      services.AddScoped<IAddOptionalSubjectUseCase, AddOptionalSubjectUseCase>();
      services.AddScoped<IAddOptionalSubjectPropertyChecker, AddOptionalSubjectPropertyChecker>();
      services.AddScoped<AddOptionalSubjectRequestHandler, AddOptionalSubjectRequestHandler>();
      services.AddScoped<AddOptionalSubjectResponsePresenter, AddOptionalSubjectResponsePresenter>();
      
      services.AddScoped<GetTermResultSheetRequestMessageValidator, GetTermResultSheetRequestMessageValidator>();
      services.AddScoped<IGetTermResultSheetUseCase, GetTermResultSheetUseCase>();
      services.AddScoped<GetTermResultSheetRequestHandler, GetTermResultSheetRequestHandler>();

      services.AddScoped<ShowBankListRequestMessageValidator, ShowBankListRequestMessageValidator>();
      services.AddScoped<IShowBankListUseCase, ShowBankListUseCase>();
      services.AddScoped<ShowBankListRequestHandler, ShowBankListRequestHandler>();
      services.AddScoped<ShowBankAccountInfoInfoResponsePresenter, ShowBankAccountInfoInfoResponsePresenter>();
      
      services.AddScoped<GetStudentAddressRequestMessageValidator, GetStudentAddressRequestMessageValidator>();
      services.AddScoped<IGetStudentAddressUseCase, GetStudentAddressUseCase>();
      services.AddScoped<GetStudentAddressRequestHandler, GetStudentAddressRequestHandler>();
      services.AddScoped<ShowStudentAddressResponsePresenter, ShowStudentAddressResponsePresenter>();
      
      services.AddScoped<GetStudentContactPersonsRequestMessageValidator, GetStudentContactPersonsRequestMessageValidator>();
      services.AddScoped<IGetStudentContactPersonsUseCase, GetStudentContactPersonsUseCase>();
      services.AddScoped<GetStudentContactPersonsRequestHandler, GetStudentContactPersonsRequestHandler>();
      services.AddScoped<ShowStudentContactPersonsResponsePresenter, ShowStudentContactPersonsResponsePresenter>();
      
      services.AddScoped<GetStudentAttendanceListResponsePresenter, GetStudentAttendanceListResponsePresenter>();

      services.AddScoped<AddNotificationConfigurationRequestMessageValidator, AddNotificationConfigurationRequestMessageValidator>();
      services.AddScoped<IAddNotificationConfigurationUseCase, AddNotificationConfigurationUseCase>();
      services.AddScoped<AddNotificationConfigurationRequestHandler, AddNotificationConfigurationRequestHandler>();
      services.AddScoped<AddNotificationConfigurationResponsePresenter, AddNotificationConfigurationResponsePresenter>();

      services.AddScoped<ShowNotificationConfigurationRequestMessageValidator, ShowNotificationConfigurationRequestMessageValidator>();
      services.AddScoped<IShowNotificationConfigurationUseCase, ShowNotificationConfigurationUseCase>();
      services.AddScoped<ShowNotificationConfigurationRequestHandler, ShowNotificationConfigurationRequestHandler>();
      services.AddScoped<ShowNotificationConfigurationResponsePresenter, ShowNotificationConfigurationResponsePresenter>();

      services.AddScoped<EditNotificationConfigurationRequestMessageValidator, EditNotificationConfigurationRequestMessageValidator>();
      services.AddScoped<IEditNotificationConfigurationUseCase, EditNotificationConfigurationUseCase>();
      services.AddScoped<EditNotificationConfigurationRequestHandler, EditNotificationConfigurationRequestHandler>();
      services.AddScoped<EditNotificationConfigurationResponsePresenter, EditNotificationConfigurationResponsePresenter>();
      
      services.AddScoped<GetEmployeeEducationalQualificationsRequestMessageValidator, GetEmployeeEducationalQualificationsRequestMessageValidator>();
      services.AddScoped<IGetEmployeeEducationalQualificationsUseCase, GetEmployeeEducationalQualificationsUseCase>();
      services.AddScoped<GetEmployeeEducationalQualificationsRequestHandler, GetEmployeeEducationalQualificationsRequestHandler>();
      services.AddScoped<ShowEmployeeEducationalQualificationsResponsePresenter, ShowEmployeeEducationalQualificationsResponsePresenter>();
      
      services.AddScoped<GetEmployeeAddressRequestMessageValidator, GetEmployeeAddressRequestMessageValidator>();
      services.AddScoped<IGetEmployeeAddressUseCase, GetEmployeeAddressUseCase>();
      services.AddScoped<GetEmployeeAddressRequestHandler, GetEmployeeAddressRequestHandler>();
      services.AddScoped<ShowEmployeeAddressResponsePresenter, ShowEmployeeAddressResponsePresenter>();

      services.AddScoped<ShowStudentSectionListRequestMessageValidator, ShowStudentSectionListRequestMessageValidator>();
      services.AddScoped<IShowStudentSectionListUseCase, ShowStudentSectionListUseCase>();
      services.AddScoped<ShowStudentSectionListRequestHandler, ShowStudentSectionListRequestHandler>();
      services.AddScoped<ShowStudentSectionListResponsePresenter, ShowStudentSectionListResponsePresenter>();
      
      services.AddScoped<ShowBranchMediumSectionListResponsePresenter, ShowBranchMediumSectionListResponsePresenter>();
      
      services.AddScoped<GetEmployeePersonalInfoRequestMessageValidator, GetEmployeePersonalInfoRequestMessageValidator>();
      services.AddScoped<IGetEmployeePersonalInfoUseCase, GetEmployeePersonalInfoUseCase>();
      services.AddScoped<GetEmployeePersonalInfoRequestHandler, GetEmployeePersonalInfoRequestHandler>();
      services.AddScoped<ShowEmployeePersonalInfoResponsePresenter, ShowEmployeePersonalInfoResponsePresenter>();
      
      services.AddScoped<GetAttendanceListByDateRangeRequestMessageValidator, 
        GetAttendanceListByDateRangeRequestMessageValidator>();
      services.AddScoped<IGetAttendanceListByDateRangeUseCase, GetAttendanceListByDateRangeUseCase>();
      services.AddScoped<GetAttendanceListByDateRangeRequestHandler, 
        GetAttendanceListByDateRangeRequestHandler>();
      
      services.AddScoped<EditEducationalQualificationRequestMessageValidator, EditEducationalQualificationRequestMessageValidator>();
      services.AddScoped<IEditEducationalQualificationUseCase, EditEducationalQualificationUseCase>();
      services.AddScoped<EditEducationalQualificationRequestHandler, EditEducationalQualificationRequestHandler>();
      services.AddScoped<EditEducationalQualificationResponsePresenter, EditEducationalQualificationResponsePresenter>();
      
      services.AddScoped<ShowEducationalQualificationRequestMessageValidator, ShowEducationalQualificationRequestMessageValidator>();
      services.AddScoped<IShowEducationalQualificationUseCase, ShowEducationalQualificationUseCase>();
      services.AddScoped<ShowEducationalQualificationRequestHandler, ShowEducationalQualificationRequestHandler>();
      services.AddScoped<ShowEducationalQualificationResponsePresenter, ShowEducationalQualificationResponsePresenter>();

      services.AddScoped<IUniqueRollPropertyChecker, UniqueRollPropertyChecker>();
      services.AddScoped<ShowStudentSectionRollRequestMessageValidator, ShowStudentSectionRollRequestMessageValidator>();
      services.AddScoped<IShowStudentSectionRollUseCase, ShowStudentSectionRollUseCase>();
      services.AddScoped<ShowStudentSectionRollRequestHandler, ShowStudentSectionRollRequestHandler>();

      services.AddScoped<DisplayEditStudentPageResponsePresenter, DisplayEditStudentPageResponsePresenter>();
      
      services.AddScoped<GetPendingLeavesRequestMessageValidator, GetPendingLeavesRequestMessageValidator>();
      services.AddScoped<IGetPendingLeavesUseCase, GetPendingLeavesUseCase>();
      services.AddScoped<GetPendingLeavesRequestHandler, GetPendingLeavesRequestHandler>();
      
      services.AddScoped<AddLoginUserRequestMessageValidator, AddLoginUserRequestMessageValidator>();
      services.AddScoped<IAddLoginUserUseCase, AddLoginUserUseCase>();
      services.AddScoped<AddLoginUserRequestHandler, AddLoginUserRequestHandler>();
      services.AddScoped<AddLoginUserResponsePresenter, AddLoginUserResponsePresenter>();
      services.AddScoped<IAddLoginUserPropertyChecker, AddLoginUserPropertyChecker>();
      
      services.AddScoped<ChangeLoginUserPasswordRequestMessageValidator, ChangeLoginUserPasswordRequestMessageValidator>();
      services.AddScoped<IChangeLoginUserPasswordUseCase, ChangeLoginUserPasswordUseCase>();
      services.AddScoped<ChangeLoginUserPasswordRequestHandler, ChangeLoginUserPasswordRequestHandler>();
      services.AddScoped<ChangeLoginUserPasswordResponsePresenter, ChangeLoginUserPasswordResponsePresenter>();
      services.AddScoped<IChangeLoginUserPasswordPropertyChecker, ChangeLoginUserPasswordPropertyChecker>();

      services.AddScoped<ITermOverlappingChecker, TermOverlappingChecker>();

      services.AddScoped<ShowSeatPlanListRequestMessageValidator, ShowSeatPlanListRequestMessageValidator>();
      services.AddScoped<IShowSeatPlanListUseCase, ShowSeatPlanListUseCase>();
      services.AddScoped<ShowSeatPlanListRequestHandler, ShowSeatPlanListRequestHandler>();
      services.AddScoped<ShowSeatPlanListResponsePresenter, ShowSeatPlanListResponsePresenter>();

      services.AddScoped<EditExamConfigurationRequestMessageValidator, EditExamConfigurationRequestMessageValidator>();
      services.AddScoped<IEditExamConfigurationUseCase, EditExamConfigurationUseCase>();
      services.AddScoped<EditExamConfigurationRequestHandler, EditExamConfigurationRequestHandler>();
      services.AddScoped<EditExamConfigurationResponsePresenter, EditExamConfigurationResponsePresenter>();

      services.AddScoped<EditClassTestRequestMessageValidator, EditClassTestRequestMessageValidator>();
      services.AddScoped<IEditClassTestUseCase, EditClassTestUseCase>();
      services.AddScoped<EditClassTestRequestHandler, EditClassTestRequestHandler>();

      services.AddScoped<EditClassRoutineRequestMessageValidator, EditClassRoutineRequestMessageValidator>();
      services.AddScoped<IEditClassRoutineUseCase, EditClassRoutineUseCase>();
      services.AddScoped<EditClassRoutineRequestHandler, EditClassRoutineRequestHandler>();
      services.AddScoped<EditClassRoutineResponsePresenter, EditClassRoutineResponsePresenter>();

      services.AddScoped<ShowClassRoutineRequestMessageValidator, ShowClassRoutineRequestMessageValidator>();
      services.AddScoped<IShowClassRoutineUseCase, ShowClassRoutineUseCase>();
      services.AddScoped<ShowClassRoutineRequestHandler, ShowClassRoutineRequestHandler>();
      services.AddScoped<ShowClassRoutineResponsePresenter, ShowClassRoutineResponsePresenter>();

      services.AddScoped<EditTermTestRequestMessageValidator, EditTermTestRequestMessageValidator>();
      services.AddScoped<IEditTermTestUseCase, EditTermTestUseCase>();
      services.AddScoped<EditTermTestRequestHandler, EditTermTestRequestHandler>();

      services.AddScoped<ShowTermTestRequestMessageValidator, ShowTermTestRequestMessageValidator>();
      services.AddScoped<IShowTermTestUseCase, ShowTermTestUseCase>();
      services.AddScoped<ShowTermTestRequestHandler, ShowTermTestRequestHandler>();
      
      services.AddScoped<EditBankRequestMessageValidator, EditBankRequestMessageValidator>();
      services.AddScoped<IEditBankUseCase, EditBankUseCase>();
      services.AddScoped<EditBankRequestHandler, EditBankRequestHandler>();
      services.AddScoped<EditBankResponsePresenter, EditBankResponsePresenter>();
      
      services.AddScoped<EditBankBranchRequestMessageValidator, EditBankBranchRequestMessageValidator>();
      services.AddScoped<IEditBankBranchUseCase, EditBankBranchUseCase>();
      services.AddScoped<EditBankBranchRequestHandler, EditBankBranchRequestHandler>();
      services.AddScoped<EditBankBranchResponsePresenter, EditBankBranchResponsePresenter>();

      services.AddScoped<ShowBankAccountRequestMessageValidator, ShowBankAccountRequestMessageValidator>();
      services.AddScoped<IShowBankAccountUseCase, ShowBankAccountUseCase>();
      services.AddScoped<ShowBankAccountRequestHandler, ShowBankAccountRequestHandler>();
      
      services.AddScoped<EditBankAccountRequestMessageValidator, EditBankAccountRequestMessageValidator>();
      services.AddScoped<IEditBankAccountUseCase, EditBankAccountUseCase>();
      services.AddScoped<EditBankAccountRequestHandler, EditBankAccountRequestHandler>();
      services.AddScoped<EditBankAccountResponsePresenter, EditBankAccountResponsePresenter>();

      services.AddScoped<AddServiceChargeRequestMessageValidator, AddServiceChargeRequestMessageValidator>();
      services.AddScoped<IAddServiceChargeUseCase, AddServiceChargeUseCase>();
      services.AddScoped<AddServiceChargeRequestHandler, AddServiceChargeRequestHandler>();
      services.AddScoped<AddServiceChargeResponsePresenter, AddServiceChargeResponsePresenter>();

      services.AddScoped<GetServiceChargeRequestMessageValidator, GetServiceChargeRequestMessageValidator>();
      services.AddScoped<IGetServiceChargeUseCase, GetServiceChargeUseCase>();
      services.AddScoped<GetServiceChargeRequestHandler, GetServiceChargeRequestHandler>();
      
      services.AddScoped<GetClassBreakTimeRequestMessageValidator, GetClassBreakTimeRequestMessageValidator>();
      services.AddScoped<IGetClassBreakTimeUseCase, GetClassBreakTimeUseCase>();
      services.AddScoped<GetClassBreakTimeRequestHandler, GetClassBreakTimeRequestHandler>();
      
      services.AddScoped<IWeekDaysConverter, WeekDaysConverter>();

      services.AddScoped<ShowServiceChargeListRequestMessageValidator, ShowServiceChargeListRequestMessageValidator>();
      services.AddScoped<IShowServiceChargeListUseCase, ShowServiceChargeListUseCase>();
      services.AddScoped<ShowServiceChargeListRequestHandler, ShowServiceChargeListRequestHandler>();
      services.AddScoped<ShowServiceChargeListResponsePresenter, ShowServiceChargeListResponsePresenter>();

      services.AddScoped<DisplayEditServiceChargeRequestMessageValidator, DisplayEditServiceChargeRequestMessageValidator>();
      services.AddScoped<IDisplayEditServiceChargeUseCase, DisplayEditServiceChargeUseCase>();
      services.AddScoped<DisplayEditServiceChargeRequestHandler, DisplayEditServiceChargeRequestHandler>();
      services.AddScoped<DisplayEditServiceChargeResponsePresenter, DisplayEditServiceChargeResponsePresenter>();

      services.AddScoped<IUniqueServiceChargePropertyChecker, UniqueServiceChargePropertyChecker>();

      services.AddScoped<GetBillingDataRequestMessageValidator, GetBillingDataRequestMessageValidator>();
      services.AddScoped<IGetBillingDataUseCase, GetBillingDataUseCase>();
      services.AddScoped<GetBillingDataRequestHandler, GetBillingDataRequestHandler>();
      
      services.AddScoped<GetBillingDataByYearRequestMessageValidator, GetBillingDataByYearRequestMessageValidator>();
      services.AddScoped<IGetBillingDataByYearUseCase, GetBillingDataByYearUseCase>();
      services.AddScoped<GetBillingDataByYearRequestHandler, GetBillingDataByYearRequestHandler>();

      services.AddScoped<EditServiceChargeRequestMessageValidator, EditServiceChargeRequestMessageValidator>();
      services.AddScoped<IEditServiceChargeUseCase, EditServiceChargeUseCase>();
      services.AddScoped<EditServiceChargeRequestHandler, EditServiceChargeRequestHandler>();
      services.AddScoped<EditServiceChargeResponsePresenter, EditServiceChargeResponsePresenter>();
      
      services.AddScoped<GetUnpaidYearsRequestMessageValidator, GetUnpaidYearsRequestMessageValidator>();
      services.AddScoped<IGetUnpaidYearsUseCase, GetUnpaidYearsUseCase>();
      services.AddScoped<GetUnpaidYearsRequestHandler, GetUnpaidYearsRequestHandler>();
      
      services.AddScoped<GetUnpaidMonthsRequestMessageValidator, GetUnpaidMonthsRequestMessageValidator>();
      services.AddScoped<IGetUnpaidMonthsUseCase, GetUnpaidMonthsUseCase>();
      services.AddScoped<GetUnpaidMonthsRequestHandler, GetUnpaidMonthsRequestHandler>();
      
      services.AddScoped<GetDueAmountRequestMessageValidator, GetDueAmountRequestMessageValidator>();
      services.AddScoped<IGetDueAmountUseCase, GetDueAmountUseCase>();
      services.AddScoped<GetDueAmountRequestHandler, GetDueAmountRequestHandler>();
      
      services.AddScoped<GetUserInfoRequestMessageValidator, GetUserInfoRequestMessageValidator>();
      services.AddScoped<IGetUserInfoUseCase, GetUserInfoUseCase>();
      services.AddScoped<GetUserInfoRequestHandler, GetUserInfoRequestHandler>();
      
      services.AddScoped<AddPaymentRequestMessageValidator, AddPaymentRequestMessageValidator>();
      services.AddScoped<IAddPaymentUseCase, AddPaymentUseCase>();
      services.AddScoped<AddPaymentRequestHandler, AddPaymentRequestHandler>();
      services.AddScoped<AddPaymentResponsePresenter, AddPaymentResponsePresenter>();
      
      services.AddScoped<IPaymentHelper, PaymentHelper>();
      services.AddScoped<GetPaymentInfoRequestMessageValidator, GetPaymentInfoRequestMessageValidator>();
      services.AddScoped<IGetPaymentInfoUseCase, GetPaymentInfoUseCase>();
      services.AddScoped<GetPaymentInfoRequestHandler, GetPaymentInfoRequestHandler>();
      
      services.AddScoped<UpdateTransactionInfoRequestMessageValidator, UpdateTransactionInfoRequestMessageValidator>();
      services.AddScoped<IUpdateTransactionInfoUseCase, UpdateTransactionInfoUseCase>();
      services.AddScoped<UpdateTransactionInfoRequestHandler, UpdateTransactionInfoRequestHandler>();
      
      services.AddScoped<AddBillingDataRequestMessageValidator, AddBillingDataRequestMessageValidator>();
      services.AddScoped<IAddBillingDataUseCase, AddBillingDataUseCase>();
      services.AddScoped<AddBillingDataRequestHandler, AddBillingDataRequestHandler>();
      
      services.AddScoped<DeleteFailedTransactionRequestMessageValidator, DeleteFailedTransactionRequestMessageValidator>();
      services.AddScoped<IDeleteFailedTransactionUseCase, DeleteFailedTransactionUseCase>();
      services.AddScoped<DeleteFailedTransactionRequestHandler, DeleteFailedTransactionRequestHandler>();

      services.AddScoped<ShowSalaryComponentTypeListRequestMessageValidator, ShowSalaryComponentTypeListRequestMessageValidator>();
      services.AddScoped<IShowSalaryComponentTypeListUseCase, ShowSalaryComponentTypeListUseCase>();
      services.AddScoped<ShowSalaryComponentTypeListRequestHandler, ShowSalaryComponentTypeListRequestHandler>();
      services.AddScoped<ShowSalaryComponentTypeListResponsePresenter, ShowSalaryComponentTypeListResponsePresenter>();
      
      services.AddScoped<IUniqueSalaryComponentTypePropertyChecker, UniqueSalaryComponentTypePropertyChecker>();
      services.AddScoped<AddSalaryComponentTypeRequestMessageValidator, AddSalaryComponentTypeRequestMessageValidator>();
      services.AddScoped<IAddSalaryComponentTypeUseCase, AddSalaryComponentTypeUseCase>();
      services.AddScoped<AddSalaryComponentTypeRequestHandler, AddSalaryComponentTypeRequestHandler>();
      services.AddScoped<AddSalaryComponentTypeResponsePresenter, AddSalaryComponentTypeResponsePresenter>();

      services.AddScoped<DisplayEditSalaryComponentTypeRequestMessageValidator, DisplayEditSalaryComponentTypeRequestMessageValidator>();
      services.AddScoped<IDisplayEditSalaryComponentTypeUseCase, DisplayEditSalaryComponentTypeUseCase>();
      services.AddScoped<DisplayEditSalaryComponentTypeRequestHandler, DisplayEditSalaryComponentTypeRequestHandler>();
      services.AddScoped<DisplayEditSalaryComponentTypeResponsePresenter, DisplayEditSalaryComponentTypeResponsePresenter>();

      services.AddScoped<EditSalaryComponentTypeRequestMessageValidator, EditSalaryComponentTypeRequestMessageValidator>();
      services.AddScoped<IEditSalaryComponentTypeUseCase, EditSalaryComponentTypeUseCase>();
      services.AddScoped<EditSalaryComponentTypeRequestHandler, EditSalaryComponentTypeRequestHandler>();
      services.AddScoped<EditSalaryComponentTypeResponsePresenter, EditSalaryComponentTypeResponsePresenter>();
      
      services.AddScoped<GetFeaturesAndAssignedUsersOfRoleRequestMessageValidator, GetFeaturesAndAssignedUsersOfRoleRequestMessageValidator>();
      services.AddScoped<IGetFeaturesAndAssignedUsersOfRoleUseCase, GetFeaturesAndAssignedUsersOfRoleUseCase>();
      services.AddScoped<GetFeaturesAndAssignedUsersOfRoleRequestHandler, GetFeaturesAndAssignedUsersOfRoleRequestHandler>();

      services.AddScoped<ShowSalaryComponentListRequestMessageValidator, ShowSalaryComponentListRequestMessageValidator>();
      services.AddScoped<IShowSalaryComponentListUseCase, ShowSalaryComponentListUseCase>();
      services.AddScoped<ShowSalaryComponentListRequestHandler, ShowSalaryComponentListRequestHandler>();
      services.AddScoped<ShowSalaryComponentListResponsePresenter, ShowSalaryComponentListResponsePresenter>();

      services.AddScoped<AddSalaryComponentRequestMessageValidator, AddSalaryComponentRequestMessageValidator>();
      services.AddScoped<IAddSalaryComponentUseCase, AddSalaryComponentUseCase>();
      services.AddScoped<AddSalaryComponentRequestHandler, AddSalaryComponentRequestHandler>();
      services.AddScoped<AddSalaryComponentResponsePresenter, AddSalaryComponentResponsePresenter>();

      services.AddScoped<DisplayAddSalaryComponentRequestMessageValidator, DisplayAddSalaryComponentRequestMessageValidator>();
      services.AddScoped<IDisplayAddSalaryComponentUseCase, DisplayAddSalaryComponentUseCase>();
      services.AddScoped<DisplayAddSalaryComponentRequestHandler, DisplayAddSalaryComponentRequestHandler>();
      services.AddScoped<DisplayAddSalaryComponentResponsePresenter, DisplayAddSalaryComponentResponsePresenter>();

      services.AddScoped<IUniqueSalaryComponentPropertyChecker, UniqueSalaryComponentPropertyChecker>();

      services.AddScoped<DisplayEditSalaryComponentRequestMessageValidator, DisplayEditSalaryComponentRequestMessageValidator>();
      services.AddScoped<IDisplayEditSalaryComponentUseCase, DisplayEditSalaryComponentUseCase>();
      services.AddScoped<DisplayEditSalaryComponentRequestHandler, DisplayEditSalaryComponentRequestHandler>();
      services.AddScoped<DisplayEditSalaryComponentResponsePresenter, DisplayEditSalaryComponentResponsePresenter>();

      services.AddScoped<EditSalaryComponentRequestMessageValidator, EditSalaryComponentRequestMessageValidator>();
      services.AddScoped<IEditSalaryComponentUseCase, EditSalaryComponentUseCase>();
      services.AddScoped<EditSalaryComponentRequestHandler, EditSalaryComponentRequestHandler>();
      services.AddScoped<EditSalaryComponentResponsePresenter, EditSalaryComponentResponsePresenter>();
      
      services.AddScoped<AddFeatureInRoleRequestMessageValidator, AddFeatureInRoleRequestMessageValidator>();
      services.AddScoped<IAddFeatureInRoleUseCase, AddFeatureInRoleUseCase>();
      services.AddScoped<AddFeatureInRoleRequestHandler, AddFeatureInRoleRequestHandler>();
      services.AddScoped<AddFeatureInRoleResponsePresenter, AddFeatureInRoleResponsePresenter>();
      
      services.AddScoped<GetFeaturesToAddInRoleRequestMessageValidator, GetFeaturesToAddInRoleRequestMessageValidator>();
      services.AddScoped<IGetFeaturesToAddInRoleUseCase, GetFeaturesToAddInRoleUseCase>();
      services.AddScoped<GetFeaturesToAddInRoleRequestHandler, GetFeaturesToAddInRoleRequestHandler>();
      
      services.AddScoped<GetFeatureTypeListRequestMessageValidator, GetFeatureTypeListRequestMessageValidator>();
      services.AddScoped<IGetFeatureTypeListUseCase, GetFeatureTypeListUseCase>();
      services.AddScoped<GetFeatureTypeListRequestHandler, GetFeatureTypeListRequestHandler>();
      
      services.AddScoped<RemoveFeatureFromRoleRequestMessageValidator, RemoveFeatureFromRoleRequestMessageValidator>();
      services.AddScoped<IRemoveFeatureFromRoleUseCase, RemoveFeatureFromRoleUseCase>();
      services.AddScoped<RemoveFeatureFromRoleRequestHandler, RemoveFeatureFromRoleRequestHandler>();
      services.AddScoped<RemoveFeatureFromRoleResponsePresenter, RemoveFeatureFromRoleResponsePresenter>();
      
      services.AddScoped<GetFeaturesInRoleRequestMessageValidator, GetFeaturesInRoleRequestMessageValidator>();
      services.AddScoped<IGetFeaturesInRoleUseCase, GetFeaturesInRoleUseCase>();
      services.AddScoped<GetFeaturesInRoleRequestHandler, GetFeaturesInRoleRequestHandler>();
      
      services.AddScoped<DefineSalaryRequestMessageValidator, DefineSalaryRequestMessageValidator>();
      services.AddScoped<IDefineSalaryUseCase, DefineSalaryUseCase>();
      services.AddScoped<DefineSalaryRequestHandler, DefineSalaryRequestHandler>();
      services.AddScoped<DefineSalaryResponsePresenter, DefineSalaryResponsePresenter>();
      services.AddScoped<IEmployeeSalaryPropertyChecker, EmployeeSalaryPropertyChecker>();

      services.AddScoped<AddCommitteeMemberAddressRequestMessageValidator, AddCommitteeMemberAddressRequestMessageValidator>();
      services.AddScoped<IAddCommitteeMemberAddressUseCase, AddCommitteeMemberAddressUseCase>();
      services.AddScoped<AddCommitteeMemberAddressRequestHandler, AddCommitteeMemberAddressRequestHandler>();
      services.AddScoped<AddCommitteeMemberAddressResponsePresenter, AddCommitteeMemberAddressResponsePresenter>();

      services.AddScoped<GetCommitteeMemberAddressRequestMessageValidator, GetCommitteeMemberAddressRequestMessageValidator>();
      services.AddScoped<IGetCommitteeMemberAddressUseCase, GetCommitteeMemberAddressUseCase>();
      services.AddScoped<GetCommitteeMemberAddressRequestHandler, GetCommitteeMemberAddressRequestHandler>();

      services.AddScoped<ShowCommitteeMemberAddressResponsePresenter, ShowCommitteeMemberAddressResponsePresenter>();

      services.AddScoped<AddCommitteeMemberPersonalInfoRequestMessageValidator, AddCommitteeMemberPersonalInfoRequestMessageValidator>();
      services.AddScoped<IAddCommitteeMemberPersonalInfoUseCase, AddCommitteeMemberPersonalInfoUseCase>();
      services.AddScoped<AddCommitteeMemberPersonalInfoRequestHandler, AddCommitteeMemberPersonalInfoRequestHandler>();
      services.AddScoped<AddCommitteeMemberPersonalInfoResponsePresenter, AddCommitteeMemberPersonalInfoResponsePresenter>();

      services.AddScoped<GetCommitteeMemberPersonalInfoRequestMessageValidator, GetCommitteeMemberPersonalInfoRequestMessageValidator>();
      services.AddScoped<IGetCommitteeMemberPersonalInfoUseCase, GetCommitteeMemberPersonalInfoUseCase>();
      services.AddScoped<GetCommitteeMemberPersonalInfoRequestHandler, GetCommitteeMemberPersonalInfoRequestHandler>();
      services.AddScoped<ShowCommitteeMemberPersonalInfoResponsePresenter, ShowCommitteeMemberPersonalInfoResponsePresenter>();
      
      services.AddScoped<GetEmployeeSalaryListRequestMessageValidator, GetEmployeeSalaryListRequestMessageValidator>();
      services.AddScoped<IGetEmployeeSalaryListUseCase, GetEmployeeSalaryListUseCase>();
      services.AddScoped<GetEmployeeSalaryListRequestHandler, GetEmployeeSalaryListRequestHandler>();
      
      services.AddScoped<GetSalarySheetYearsRequestMessageValidator, GetSalarySheetYearsRequestMessageValidator>();
      services.AddScoped<IGetSalarySheetYearsUseCase, GetSalarySheetYearsUseCase>();
      services.AddScoped<GetSalarySheetYearsRequestHandler, GetSalarySheetYearsRequestHandler>();
      
      services.AddScoped<SaveSalarySheetRequestMessageValidator, SaveSalarySheetRequestMessageValidator>();
      services.AddScoped<ISaveSalarySheetUseCase, SaveSalarySheetUseCase>();
      services.AddScoped<SaveSalarySheetRequestHandler, SaveSalarySheetRequestHandler>();
      services.AddScoped<SaveSalarySheetResponsePresenter, SaveSalarySheetResponsePresenter>();
      services.AddScoped<ISaveSalarySheetValidationChecker, SaveSalarySheetValidationChecker>();

      services.AddScoped<ShowBranchMediumEmployeeListRequestMessageValidator, ShowBranchMediumEmployeeListRequestMessageValidator>();
      services.AddScoped<IShowBranchMediumEmployeeListUseCase, ShowBranchMediumEmployeeListUseCase>();
      services.AddScoped<ShowBranchMediumEmployeeListRequestHandler, ShowBranchMediumEmployeeListRequestHandler>();

      services.AddScoped<ITaxCalculator, TaxCalculator>();
      
      services.AddScoped<GetOtherComponentsListRequestMessageValidator, GetOtherComponentsListRequestMessageValidator>();
      services.AddScoped<IGetOtherComponentsListUseCase, GetOtherComponentsListUseCase>();
      services.AddScoped<GetOtherComponentsListRequestHandler, GetOtherComponentsListRequestHandler>();
      
      services.AddScoped<GetSalarySheetRequestMessageValidator, GetSalarySheetRequestMessageValidator>();
      services.AddScoped<IGetSalarySheetUseCase, GetSalarySheetUseCase>();
      services.AddScoped<GetSalarySheetRequestHandler, GetSalarySheetRequestHandler>();
      
      services.AddScoped<ChangeButtonStatusRequestMessageValidator, ChangeButtonStatusRequestMessageValidator>();
      services.AddScoped<IChangeButtonStatusUseCase, ChangeButtonStatusUseCase>();
      services.AddScoped<ChangeButtonStatusRequestHandler, ChangeButtonStatusRequestHandler>();
      
      services.AddScoped<SalarySheetAccountPostingRequestMessageValidator, SalarySheetAccountPostingRequestMessageValidator>();
      services.AddScoped<ISalarySheetAccountPostingUseCase, SalarySheetAccountPostingUseCase>();
      services.AddScoped<SalarySheetAccountPostingRequestHandler, SalarySheetAccountPostingRequestHandler>();
      
      services.AddScoped<GetBankStatementRequestMessageValidator, GetBankStatementRequestMessageValidator>();
      services.AddScoped<IGetBankStatementUseCase, GetBankStatementUseCase>();
      services.AddScoped<GetBankStatementRequestHandler, GetBankStatementRequestHandler>();
      
      services.AddScoped<GetBankStatementInfoRequestMessageValidator, GetBankStatementInfoRequestMessageValidator>();
      services.AddScoped<IGetBankStatementInfoUseCase, GetBankStatementInfoUseCase>();
      services.AddScoped<GetBankStatementInfoRequestHandler, GetBankStatementInfoRequestHandler>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      //using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
      //{
      //  var context = serviceScope.ServiceProvider.GetRequiredService<EmsDbContext>();
      //  context.Database.EnsureCreated();
      //}

      var supportedCultures = new[]
      {
        new CultureInfo("en-US"),
        new CultureInfo("bn-BD")
      };
      var requestLocalizationOptions = new RequestLocalizationOptions
      {
        DefaultRequestCulture = new RequestCulture("en-US"),
        SupportedCultures = supportedCultures,
        SupportedUICultures = supportedCultures
      };
      app.UseRequestLocalization(requestLocalizationOptions);

      app.UseStaticFiles();
      app.UseCookiePolicy();

      app.UseAuthentication();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");
      });
      using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
      {
        var dbInitializer = serviceScope.ServiceProvider.GetService<IDbInitializer>();
        dbInitializer.InitializeDb();
      }
    }
  }
}
