#pragma checksum "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\TermTest\ShowTermTestList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "622fd8330bfe7d132d4de9730e335612b1a64d32"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_TermTest_ShowTermTestList), @"mvc.1.0.view", @"/Views/TermTest/ShowTermTestList.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/TermTest/ShowTermTestList.cshtml", typeof(AspNetCore.Views_TermTest_ShowTermTestList))]
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
#line 1 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\TermTest\ShowTermTestList.cshtml"
using SinePulse.EMS.Domain.Features;

#line default
#line hidden
#line 2 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\TermTest\ShowTermTestList.cshtml"
using SinePulse.EMS.Messages.Model.Enums;

#line default
#line hidden
#line 3 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\TermTest\ShowTermTestList.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"622fd8330bfe7d132d4de9730e335612b1a64d32", @"/Views/TermTest/ShowTermTestList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9011cacf54d8ae45691a3deaab04c36239ace056", @"/Views/_ViewImports.cshtml")]
    public class Views_TermTest_ShowTermTestList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<SinePulse.EMS.Messages.TermTestMessages.ShowTermTestListResponseMessage.TermTest>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(256, 139, true);
            WriteLiteral("\r\n\r\n<table class=\"table table-striped table-hover\" id=\"sample_2\">\r\n  <thead class=\"filterCriteria\">\r\n    <tr class=\"uppercase\">\r\n      <th>");
            EndContext();
            BeginContext(396, 34, false);
#line 11 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\TermTest\ShowTermTestList.cshtml"
     Write(Localizer["ShowTermTestList.Date"]);

#line default
#line hidden
            EndContext();
            BeginContext(430, 17, true);
            WriteLiteral("</th>\r\n      <th>");
            EndContext();
            BeginContext(448, 39, false);
#line 12 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\TermTest\ShowTermTestList.cshtml"
     Write(Localizer["ShowTermTestList.StartTime"]);

#line default
#line hidden
            EndContext();
            BeginContext(487, 17, true);
            WriteLiteral("</th>\r\n      <th>");
            EndContext();
            BeginContext(505, 37, false);
#line 13 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\TermTest\ShowTermTestList.cshtml"
     Write(Localizer["ShowTermTestList.EndTime"]);

#line default
#line hidden
            EndContext();
            BeginContext(542, 17, true);
            WriteLiteral("</th>\r\n      <th>");
            EndContext();
            BeginContext(560, 35, false);
#line 14 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\TermTest\ShowTermTestList.cshtml"
     Write(Localizer["ShowTermTestList.Class"]);

#line default
#line hidden
            EndContext();
            BeginContext(595, 17, true);
            WriteLiteral("</th>\r\n      <th>");
            EndContext();
            BeginContext(613, 37, false);
#line 15 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\TermTest\ShowTermTestList.cshtml"
     Write(Localizer["ShowTermTestList.Subject"]);

#line default
#line hidden
            EndContext();
            BeginContext(650, 17, true);
            WriteLiteral("</th>\r\n      <th>");
            EndContext();
            BeginContext(668, 38, false);
#line 16 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\TermTest\ShowTermTestList.cshtml"
     Write(Localizer["ShowTermTestList.ExamType"]);

#line default
#line hidden
            EndContext();
            BeginContext(706, 17, true);
            WriteLiteral("</th>\r\n      <th>");
            EndContext();
            BeginContext(724, 36, false);
#line 17 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\TermTest\ShowTermTestList.cshtml"
     Write(Localizer["ShowTermTestList.Action"]);

#line default
#line hidden
            EndContext();
            BeginContext(760, 30, true);
            WriteLiteral("</th>\r\n    </tr>\r\n  </thead>\r\n");
            EndContext();
#line 20 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\TermTest\ShowTermTestList.cshtml"
    
    if (Model.Count > 0)
    {
      foreach (var termModel in Model)
      {
        var termStartTime = termModel.StartTime.Hour.ToString("D2") + ":" + termModel.StartTime.Minute.ToString("D2");
        var termEndTime = termModel.EndTime.Hour.ToString("D2") + ":" + termModel.EndTime.Minute.ToString("D2");
        var termDate = termModel.StartTime.ToString("dd MMMM, yyyy");

#line default
#line hidden
            BeginContext(1183, 42, true);
            WriteLiteral("        <tr>\r\n          <td>\r\n            ");
            EndContext();
            BeginContext(1226, 8, false);
#line 30 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\TermTest\ShowTermTestList.cshtml"
       Write(termDate);

#line default
#line hidden
            EndContext();
            BeginContext(1234, 33, true);
            WriteLiteral("\r\n          </td>\r\n          <td>");
            EndContext();
            BeginContext(1268, 13, false);
#line 32 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\TermTest\ShowTermTestList.cshtml"
         Write(termStartTime);

#line default
#line hidden
            EndContext();
            BeginContext(1281, 21, true);
            WriteLiteral("</td>\r\n          <td>");
            EndContext();
            BeginContext(1303, 11, false);
#line 33 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\TermTest\ShowTermTestList.cshtml"
         Write(termEndTime);

#line default
#line hidden
            EndContext();
            BeginContext(1314, 21, true);
            WriteLiteral("</td>\r\n          <td>");
            EndContext();
            BeginContext(1336, 45, false);
#line 34 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\TermTest\ShowTermTestList.cshtml"
         Write(Html.DisplayFor(model => termModel.ClassName));

#line default
#line hidden
            EndContext();
            BeginContext(1381, 21, true);
            WriteLiteral("</td>\r\n          <td>");
            EndContext();
            BeginContext(1403, 47, false);
#line 35 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\TermTest\ShowTermTestList.cshtml"
         Write(Html.DisplayFor(model => termModel.SubjectName));

#line default
#line hidden
            EndContext();
            BeginContext(1450, 21, true);
            WriteLiteral("</td>\r\n          <td>");
            EndContext();
            BeginContext(1472, 44, false);
#line 36 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\TermTest\ShowTermTestList.cshtml"
         Write(Html.DisplayFor(model => termModel.ExamType));

#line default
#line hidden
            EndContext();
            BeginContext(1516, 37, true);
            WriteLiteral("</td>\r\n          <td>\r\n            <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1553, "\"", 1602, 2);
            WriteAttributeValue("", 1560, "/SeatPlan/AddSeatPlan?testId=", 1560, 29, true);
#line 38 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\TermTest\ShowTermTestList.cshtml"
WriteAttributeValue("", 1589, termModel.Id, 1589, 13, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1603, 79, true);
            WriteLiteral(" class=\"action-link\"><i class=\"fa fa-plus action-icon\"></i></a>\r\n            <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1682, "\"", 1738, 2);
            WriteAttributeValue("", 1689, "/TermTest/UpdateTermTest?termTestId=", 1689, 36, true);
#line 39 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\TermTest\ShowTermTestList.cshtml"
WriteAttributeValue("", 1725, termModel.Id, 1725, 13, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1739, 97, true);
            WriteLiteral(" class=\"action-link\"><i class=\"fa fa-edit action-icon\"></i></a>\r\n          </td>\r\n        </tr>\r\n");
            EndContext();
#line 42 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\TermTest\ShowTermTestList.cshtml"
      }
    }
  

#line default
#line hidden
            BeginContext(1857, 10, true);
            WriteLiteral("</table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<SinePulse.EMS.Messages.TermTestMessages.ShowTermTestListResponseMessage.TermTest>> Html { get; private set; }
    }
}
#pragma warning restore 1591
