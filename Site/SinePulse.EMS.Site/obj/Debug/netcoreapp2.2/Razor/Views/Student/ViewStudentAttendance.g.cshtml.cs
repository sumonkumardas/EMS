#pragma checksum "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Student\ViewStudentAttendance.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5ede8068bf9461a59b866dccd1ef98a827298186"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Student_ViewStudentAttendance), @"mvc.1.0.view", @"/Views/Student/ViewStudentAttendance.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Student/ViewStudentAttendance.cshtml", typeof(AspNetCore.Views_Student_ViewStudentAttendance))]
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
#line 2 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Student\ViewStudentAttendance.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5ede8068bf9461a59b866dccd1ef98a827298186", @"/Views/Student/ViewStudentAttendance.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9011cacf54d8ae45691a3deaab04c36239ace056", @"/Views/_ViewImports.cshtml")]
    public class Views_Student_ViewStudentAttendance : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SinePulse.EMS.Site.Models.StudentAttendanceViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 4 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Student\ViewStudentAttendance.cshtml"
  
    ViewData["Title"] = "ViewStudentAttendance";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(245, 256, true);
            WriteLiteral(@"<div class=""page-container"">
    <!-- BEGIN CONTENT -->
    <div class=""page-content-wrapper"">
        <div class=""page-content"">
            <div class=""portlet box form-conatiner"">
                <div class=""icon-container"">
                    <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 501, "\"", 563, 2);
            WriteAttributeValue("", 508, "/Student/UpdateStudentAttendance?attendanceId=", 508, 46, true);
#line 14 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Student\ViewStudentAttendance.cshtml"
WriteAttributeValue("", 554, Model.Id, 554, 9, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(564, 499, true);
            WriteLiteral(@" class=""fa fa-edit action-link""></a>
                    <a href=""/StudentAttendance/ShowStudentAttendanceList"" class=""fa fa-tasks action-link""></a>
                </div>
                
                <div class=""portlet-body form"">
                  <div class=""container-fluid"">
                    <div class=""row-fluid "">
                      <div class=""col-md-6"">
                        <div class=""row custom-row"">
                          <div class=""col-md-4 title""><strong>");
            EndContext();
            BeginContext(1064, 42, false);
#line 23 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Student\ViewStudentAttendance.cshtml"
                                                         Write(Localizer["ViewStudentAttendance.Student"]);

#line default
#line hidden
            EndContext();
            BeginContext(1106, 71, true);
            WriteLiteral("</strong></div>\r\n                          <div class=\"col-md-8 value\">");
            EndContext();
            BeginContext(1178, 22, false);
#line 24 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Student\ViewStudentAttendance.cshtml"
                                                 Write(Model.Student.FullName);

#line default
#line hidden
            EndContext();
            BeginContext(1200, 573, true);
            WriteLiteral(@"</div>
                        </div>
                      </div>
                      <div class=""col-md-6"">
                        <div class=""row custom-row"">
                          
                        </div>
                      </div>
                    </div>
                  </div>

                  <div class=""container-fluid"">
                    <div class=""row-fluid "">
                      <div class=""col-md-6"">
                        <div class=""row custom-row"">
                          <div class=""col-md-4 title""><strong>");
            EndContext();
            BeginContext(1774, 41, false);
#line 39 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Student\ViewStudentAttendance.cshtml"
                                                         Write(Localizer["ViewStudentAttendance.InTime"]);

#line default
#line hidden
            EndContext();
            BeginContext(1815, 71, true);
            WriteLiteral("</strong></div>\r\n                          <div class=\"col-md-8 value\">");
            EndContext();
            BeginContext(1887, 12, false);
#line 40 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Student\ViewStudentAttendance.cshtml"
                                                 Write(Model.InTime);

#line default
#line hidden
            EndContext();
            BeginContext(1899, 232, true);
            WriteLiteral("</div>\r\n                        </div>\r\n                      </div>\r\n                      <div class=\"col-md-6\">\r\n                        <div class=\"row custom-row\">\r\n                          <div class=\"col-md-4 title\"><strong>");
            EndContext();
            BeginContext(2132, 42, false);
#line 45 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Student\ViewStudentAttendance.cshtml"
                                                         Write(Localizer["ViewStudentAttendance.OutTime"]);

#line default
#line hidden
            EndContext();
            BeginContext(2174, 71, true);
            WriteLiteral("</strong></div>\r\n                          <div class=\"col-md-8 value\">");
            EndContext();
            BeginContext(2246, 13, false);
#line 46 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Student\ViewStudentAttendance.cshtml"
                                                 Write(Model.OutTime);

#line default
#line hidden
            EndContext();
            BeginContext(2259, 206, true);
            WriteLiteral("</div>\r\n                        </div>\r\n                      </div>\r\n                    </div>\r\n                  </div>\r\n\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SinePulse.EMS.Site.Models.StudentAttendanceViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
