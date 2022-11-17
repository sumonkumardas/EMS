#pragma checksum "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ddfe6eac0c8f8e36dc9bf55679e148031b2f0e2b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Session_ViewSession), @"mvc.1.0.view", @"/Views/Session/ViewSession.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Session/ViewSession.cshtml", typeof(AspNetCore.Views_Session_ViewSession))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\_ViewImports.cshtml"
using SinePulse.EMS.Site;

#line default
#line hidden
#line 2 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\_ViewImports.cshtml"
using SinePulse.EMS.Site.Models;

#line default
#line hidden
#line 1 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#line 3 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
using SinePulse.EMS.Domain.Features;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ddfe6eac0c8f8e36dc9bf55679e148031b2f0e2b", @"/Views/Session/ViewSession.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9011cacf54d8ae45691a3deaab04c36239ace056", @"/Views/_ViewImports.cshtml")]
    public class Views_Session_ViewSession : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SessionViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 5 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
  
  ViewData["Title"] = "ViewSession";
  Layout = "~/Views/Shared/_Layout.cshtml";
  var startDate = Model.StartDate.ToString("dd MMMM, yyyy");
  var endDate = Model.EndDate.ToString("dd MMMM, yyyy");

#line default
#line hidden
            BeginContext(353, 537, true);
            WriteLiteral(@"<div class=""page-container"">
  <!-- BEGIN CONTENT -->
  <div class=""page-content-wrapper"">
    <div class=""page-content"">

      <!-- END PAGE HEADER-->
      <!-- BEGIN PAGE CONTENT-->
      <div class=""row"">
        <div class=""col-md-12"">
          <!-- BEGIN PROFILE SIDEBAR -->
          <div class=""profile-sidebar"">
            <!-- PORTLET MAIN -->
            <div class=""portlet light profile-sidebar-portlet"">
              <!-- SIDEBAR USERPIC -->
              <div class=""profile-userpic"">
                <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 890, "\"", 953, 2);
            WriteAttributeValue("", 897, "/Institute/ViewInstitute?instituteId=", 897, 37, true);
#line 26 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
WriteAttributeValue("", 934, Model.Institute.Id, 934, 19, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(954, 258, true);
            WriteLiteral(@"><img src=""../../img/school.png"" class=""img-responsive"" alt=""""></a>
              </div>
              <!-- END SIDEBAR USERPIC -->
              <!-- SIDEBAR USER TITLE -->
              <div class=""profile-usertitle-name small-title"">
                ");
            EndContext();
            BeginContext(1213, 43, false);
#line 31 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
           Write(Html.DisplayFor(model => model.SessionName));

#line default
#line hidden
            EndContext();
            BeginContext(1256, 87, true);
            WriteLiteral("\r\n              </div>\r\n              <div class=\"profile-stat-text\">\r\n                ");
            EndContext();
            BeginContext(1344, 32, false);
#line 34 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
           Write(Localizer["ViewSession.Session"]);

#line default
#line hidden
            EndContext();
            BeginContext(1376, 98, true);
            WriteLiteral("\r\n              </div>\r\n              <hr />\r\n              <div class=\"profile-usertitle-name\">\r\n");
            EndContext();
#line 38 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
                 if (Model.HasPermission(Feature.InstituteEnum.ViewInstitute.ToString()))
                {

#line default
#line hidden
            BeginContext(1584, 20, true);
            WriteLiteral("                  <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1604, "\"", 1667, 2);
            WriteAttributeValue("", 1611, "/Institute/ViewInstitute?instituteId=", 1611, 37, true);
#line 40 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
WriteAttributeValue("", 1648, Model.Institute.Id, 1648, 19, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1668, 23, true);
            WriteLiteral(">\r\n                    ");
            EndContext();
            BeginContext(1692, 58, false);
#line 41 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
               Write(Html.DisplayFor(model => model.Institute.OrganisationName));

#line default
#line hidden
            EndContext();
            BeginContext(1750, 2, true);
            WriteLiteral(" (");
            EndContext();
            BeginContext(1753, 51, false);
#line 41 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
                                                                            Write(Html.DisplayFor(model => model.Institute.ShortName));

#line default
#line hidden
            EndContext();
            BeginContext(1804, 27, true);
            WriteLiteral(")\r\n                  </a>\r\n");
            EndContext();
#line 43 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
                }
                else
                {
                  

#line default
#line hidden
            BeginContext(1910, 58, false);
#line 46 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
             Write(Html.DisplayFor(model => model.Institute.OrganisationName));

#line default
#line hidden
            EndContext();
            BeginContext(1970, 51, false);
#line 46 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
                                                                         Write(Html.DisplayFor(model => model.Institute.ShortName));

#line default
#line hidden
            EndContext();
#line 46 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
                                                                                                                                  
                }

#line default
#line hidden
            BeginContext(2042, 87, true);
            WriteLiteral("\r\n              </div>\r\n              <div class=\"profile-stat-text\">\r\n                ");
            EndContext();
            BeginContext(2130, 34, false);
#line 51 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
           Write(Localizer["ViewSession.Institute"]);

#line default
#line hidden
            EndContext();
            BeginContext(2164, 98, true);
            WriteLiteral("\r\n              </div>\r\n              <hr />\r\n              <div class=\"profile-usertitle-name\">\r\n");
            EndContext();
#line 55 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
                 if (Model.HasPermission(Feature.BranchEnum.ViewBranch.ToString()))
                {

#line default
#line hidden
            BeginContext(2366, 48, true);
            WriteLiteral("                  <a href=\"#\">Satic Branch</a>\r\n");
            EndContext();
#line 58 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
                }
                else
                {

#line default
#line hidden
            BeginContext(2474, 48, true);
            WriteLiteral("                  <a href=\"#\">Satic Branch</a>\r\n");
            EndContext();
#line 62 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
                }

#line default
#line hidden
            BeginContext(2541, 85, true);
            WriteLiteral("              </div>\r\n              <div class=\"profile-stat-text\">\r\n                ");
            EndContext();
            BeginContext(2627, 31, false);
#line 65 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
           Write(Localizer["ViewSession.Branch"]);

#line default
#line hidden
            EndContext();
            BeginContext(2658, 98, true);
            WriteLiteral("\r\n              </div>\r\n              <hr />\r\n              <div class=\"profile-usertitle-name\">\r\n");
            EndContext();
#line 69 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
                 if (Model.HasPermission(Feature.BranchMediumEnum.ViewBranchMedium.ToString()))
                {

#line default
#line hidden
            BeginContext(2872, 49, true);
            WriteLiteral("                  <a href=\"#\">Static Medium</a>\r\n");
            EndContext();
#line 72 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
                }
                else
                {

#line default
#line hidden
            BeginContext(2981, 49, true);
            WriteLiteral("                  <a href=\"#\">Static Medium</a>\r\n");
            EndContext();
#line 76 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
                }

#line default
#line hidden
            BeginContext(3049, 85, true);
            WriteLiteral("              </div>\r\n              <div class=\"profile-stat-text\">\r\n                ");
            EndContext();
            BeginContext(3135, 31, false);
#line 79 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
           Write(Localizer["ViewSession.Medium"]);

#line default
#line hidden
            EndContext();
            BeginContext(3166, 213, true);
            WriteLiteral("\r\n              </div>\r\n              <hr />\r\n              <div class=\"profile-usertitle-name\">\r\n                Static Shift\r\n              </div>\r\n              <div class=\"profile-stat-text\">\r\n                ");
            EndContext();
            BeginContext(3380, 30, false);
#line 86 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
           Write(Localizer["ViewSession.Shift"]);

#line default
#line hidden
            EndContext();
            BeginContext(3410, 1286, true);
            WriteLiteral(@"
              </div>
              <hr />
              <br />
            </div>
            <!-- END PORTLET MAIN -->
          </div>
          <!-- END BEGIN PROFILE SIDEBAR -->
          <!-- BEGIN PROFILE CONTENT -->
          <div class=""profile-content"">
            <div class=""row"">
              <div class=""col-md-12"">
                <!-- BEGIN PORTLET -->
                <div class=""portlet light"">
                  <div class=""portlet-title tabbable-line"">
                    <div class=""page-toolbar custom-page-menu-bar"">
                      <div class=""btn-group"">
                        <ul style=""padding:0px;"">

                          <li class=""custom-page-menu dropdown primary-menu-item-li"">
                            <a href=""#"" class=""show-dropdown-on-hover"" data-toggle=""custom-page-menu"">
                              <button type=""button"" class=""btn btn-fit-height dark-bg dropdown-toggle"" data-toggle=""dropdown"" data-hover=""dropdown"" data-delay=""100"" data-clos");
            WriteLiteral(@"e-others=""true"">
                                <i class=""fa fa-bars""></i>
                              </button>
                            </a>
                            <ul class=""dropdown-menu light-arrow-only"">
                              <li><a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 4696, "\"", 4752, 1);
#line 112 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
WriteAttributeValue("", 4703, Url.Action("UpdateSession", new {id = Model.Id}), 4703, 49, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(4753, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(4755, 36, false);
#line 112 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
                                                                                         Write(Localizer["ViewSession.EditSession"]);

#line default
#line hidden
            EndContext();
            BeginContext(4791, 47, true);
            WriteLiteral("</a></li>\r\n                              <li><a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 4838, "\"", 4915, 4);
            WriteAttributeValue("", 4845, "/Session/Index?sessionType=", 4845, 27, true);
#line 113 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
WriteAttributeValue("", 4872, Model.SessionType, 4872, 18, false);

#line default
#line hidden
            WriteAttributeValue("", 4890, "&objectId=", 4890, 10, true);
#line 113 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
WriteAttributeValue("", 4900, Model.ObjectId, 4900, 15, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(4916, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(4918, 36, false);
#line 113 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
                                                                                                              Write(Localizer["ViewSession.AllSessions"]);

#line default
#line hidden
            EndContext();
            BeginContext(4954, 1202, true);
            WriteLiteral(@"</a></li>
                            </ul>
                          </li>

                        </ul>

                      </div>

                    </div>
                    <ul class=""nav nav-tabs custom-page-tab"">
                      <li class=""active"">
                        <a href=""#tab_general_info"" data-toggle=""tab"">
                          <h5 class=""caption-subject font-blue-madison bold uppercase"">General Info</h5>
                        </a>
                      </li>
                    </ul>
                  </div>
                  <div class=""portlet-body"">
                    <!--BEGIN TABS-->
                    <div class=""tab-content"">

                      <div class=""tab-pane active"" id=""tab_general_info"">
                        <div style=""min-height: 337px;"">
                          <div class=""row"">

                            <div class=""col-md-12"">
                              <div class=""row list-separated profile-stat"">
          ");
            WriteLiteral("                      <div class=\"col-md-6 col-sm-6 col-xs-6\">\r\n                                  <div class=\"uppercase profile-stat-title\">\r\n                                    ");
            EndContext();
            BeginContext(6157, 35, false);
#line 142 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
                               Write(Html.DisplayFor(model => startDate));

#line default
#line hidden
            EndContext();
            BeginContext(6192, 157, true);
            WriteLiteral("\r\n                                  </div>\r\n                                  <div class=\"uppercase profile-stat-text\">\r\n                                    ");
            EndContext();
            BeginContext(6350, 34, false);
#line 145 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
                               Write(Localizer["ViewSession.StartDate"]);

#line default
#line hidden
            EndContext();
            BeginContext(6384, 272, true);
            WriteLiteral(@"
                                  </div>
                                </div>
                                <div class=""col-md-6 col-sm-6 col-xs-6"">
                                  <div class=""uppercase profile-stat-title"">
                                    ");
            EndContext();
            BeginContext(6657, 33, false);
#line 150 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
                               Write(Html.DisplayFor(model => endDate));

#line default
#line hidden
            EndContext();
            BeginContext(6690, 157, true);
            WriteLiteral("\r\n                                  </div>\r\n                                  <div class=\"uppercase profile-stat-text\">\r\n                                    ");
            EndContext();
            BeginContext(6848, 32, false);
#line 153 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
                               Write(Localizer["ViewSession.EndDate"]);

#line default
#line hidden
            EndContext();
            BeginContext(6880, 557, true);
            WriteLiteral(@"
                                  </div>
                                </div>

                              </div>
                            </div>
                          </div>
                          <div class=""row"">
                            <div class=""col-md-12"">
                              <div class=""row list-separated profile-stat"">
                                <div class=""col-md-6 col-sm-6 col-xs-6"">
                                  <div class=""uppercase profile-stat-title"">
                                    ");
            EndContext();
            BeginContext(7438, 43, false);
#line 165 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
                               Write(Html.DisplayFor(model => model.SessionType));

#line default
#line hidden
            EndContext();
            BeginContext(7481, 157, true);
            WriteLiteral("\r\n                                  </div>\r\n                                  <div class=\"uppercase profile-stat-text\">\r\n                                    ");
            EndContext();
            BeginContext(7639, 36, false);
#line 168 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
                               Write(Localizer["ViewSession.SessionType"]);

#line default
#line hidden
            EndContext();
            BeginContext(7675, 236, true);
            WriteLiteral("\r\n                                  </div>\r\n                                </div>\r\n                                <div class=\"col-md-6 col-sm-6 col-xs-6\">\r\n                                  <div class=\"uppercase profile-stat-title\">\r\n");
            EndContext();
#line 173 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
                                      
                                      if (Model.IsSessionClosed)
                                      {

#line default
#line hidden
            BeginContext(8058, 102, true);
            WriteLiteral("                                        <i class=\"fa fa-check-circle\" style=\"font-size: 1.5em;\"></i>\r\n");
            EndContext();
#line 177 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
                                      }
                                      else
                                      {

#line default
#line hidden
            BeginContext(8286, 95, true);
            WriteLiteral("                                        <i class=\"fa fa-close\" style=\"font-size: 1.5em;\"></i>\r\n");
            EndContext();
#line 181 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
                                      }
                                    

#line default
#line hidden
            BeginContext(8461, 155, true);
            WriteLiteral("                                  </div>\r\n                                  <div class=\"uppercase profile-stat-text\">\r\n                                    ");
            EndContext();
            BeginContext(8617, 34, false);
#line 185 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Session\ViewSession.cshtml"
                               Write(Localizer["ViewSession.IsCurrent"]);

#line default
#line hidden
            EndContext();
            BeginContext(8651, 571, true);
            WriteLiteral(@"
                                  </div>
                                </div>
                              </div>
                            </div>
                          </div>
                        </div>
                      </div>

                    </div>
                    <!--END TABS-->
                  </div>
                </div>
                <!-- END PORTLET -->
              </div>
            </div>

          </div>
          <!-- END PROFILE CONTENT -->
        </div>
      </div>
    </div>
  </div>
</div>
");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IViewLocalizer Localizer { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SessionViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
