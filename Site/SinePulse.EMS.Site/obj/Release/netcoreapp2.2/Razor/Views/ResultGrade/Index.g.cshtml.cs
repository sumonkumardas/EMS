#pragma checksum "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ResultGrade\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1793a7d9fe51d0da99664ac3d14a6a7b03340197"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ResultGrade_Index), @"mvc.1.0.view", @"/Views/ResultGrade/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/ResultGrade/Index.cshtml", typeof(AspNetCore.Views_ResultGrade_Index))]
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
#line 1 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ResultGrade\Index.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1793a7d9fe51d0da99664ac3d14a6a7b03340197", @"/Views/ResultGrade/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9011cacf54d8ae45691a3deaab04c36239ace056", @"/Views/_ViewImports.cshtml")]
    public class Views_ResultGrade_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ResultGradeListViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(113, 453, true);
            WriteLiteral(@"

<div class=""page-container"">
  <!-- BEGIN CONTENT -->
  <div class=""page-content-wrapper"">
    <div class=""page-content"">
      <div class=""row"">
        <div class=""col-md-12"">
          <div class=""page-bar margin-top-15"">
            
          </div>


          <table class=""table table-striped table-hover"" id=""sample_5"">

            <thead class=""filterCriteria"">
              <tr>

                <th>
                  ");
            EndContext();
            BeginContext(567, 30, false);
#line 23 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ResultGrade\Index.cshtml"
             Write(Localizer["Index.GradeLetter"]);

#line default
#line hidden
            EndContext();
            BeginContext(597, 145, true);
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    Grade Point\r\n                </th>\r\n                <th>\r\n                    ");
            EndContext();
            BeginContext(743, 28, false);
#line 29 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ResultGrade\Index.cshtml"
               Write(Localizer["Index.StartMark"]);

#line default
#line hidden
            EndContext();
            BeginContext(771, 67, true);
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
            EndContext();
            BeginContext(839, 26, false);
#line 32 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ResultGrade\Index.cshtml"
               Write(Localizer["Index.EndMark"]);

#line default
#line hidden
            EndContext();
            BeginContext(865, 68, true);
            WriteLiteral("\r\n                    \r\n                </th>\r\n                <th> ");
            EndContext();
            BeginContext(934, 25, false);
#line 35 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ResultGrade\Index.cshtml"
                Write(Localizer["Index.Action"]);

#line default
#line hidden
            EndContext();
            BeginContext(959, 74, true);
            WriteLiteral(" </th>\r\n              </tr>\r\n            </thead>\r\n\r\n            <tbody>\r\n");
            EndContext();
#line 40 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ResultGrade\Index.cshtml"
               foreach (var resultGradeModel in Model.ResultGrades)
              {

#line default
#line hidden
            BeginContext(1119, 68, true);
            WriteLiteral("                <tr>\r\n\r\n                  <td>\r\n                    ");
            EndContext();
            BeginContext(1188, 55, false);
#line 45 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ResultGrade\Index.cshtml"
               Write(Html.DisplayFor(model => @resultGradeModel.GradeLetter));

#line default
#line hidden
            EndContext();
            BeginContext(1243, 71, true);
            WriteLiteral("\r\n                  </td>\r\n                  <td>\r\n                    ");
            EndContext();
            BeginContext(1315, 54, false);
#line 48 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ResultGrade\Index.cshtml"
               Write(Html.DisplayFor(model => @resultGradeModel.GradePoint));

#line default
#line hidden
            EndContext();
            BeginContext(1369, 71, true);
            WriteLiteral("\r\n                  </td>\r\n                  <td>\r\n                    ");
            EndContext();
            BeginContext(1441, 53, false);
#line 51 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ResultGrade\Index.cshtml"
               Write(Html.DisplayFor(model => @resultGradeModel.StartMark));

#line default
#line hidden
            EndContext();
            BeginContext(1494, 73, true);
            WriteLiteral("\r\n                  </td>\r\n\r\n                  <td>\r\n                    ");
            EndContext();
            BeginContext(1568, 51, false);
#line 55 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ResultGrade\Index.cshtml"
               Write(Html.DisplayFor(model => @resultGradeModel.EndMark));

#line default
#line hidden
            EndContext();
            BeginContext(1619, 73, true);
            WriteLiteral("\r\n                  </td>\r\n                  <td>\r\n                    <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1692, "\"", 1764, 2);
            WriteAttributeValue("", 1699, "/ResultGrade/UpdateResultGrade?resultGradeId=", 1699, 45, true);
#line 58 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ResultGrade\Index.cshtml"
WriteAttributeValue("", 1744, resultGradeModel.Id, 1744, 20, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1765, 217, true);
            WriteLiteral(" class=\"action-link\"><i class=\"fa fa-edit action-icon\"></i></a>\r\n                    <a href=\"#\" class=\"action-link\"><i class=\"fa fa-minus-circle action-icon\"></i></a>\r\n                  </td>\r\n                </tr>\r\n");
            EndContext();
#line 62 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ResultGrade\Index.cshtml"
              }

#line default
#line hidden
            BeginContext(1999, 104, true);
            WriteLiteral("            </tbody>\r\n          </table>\r\n        </div>\r\n      </div>\r\n\r\n    </div>\r\n  </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ResultGradeListViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591