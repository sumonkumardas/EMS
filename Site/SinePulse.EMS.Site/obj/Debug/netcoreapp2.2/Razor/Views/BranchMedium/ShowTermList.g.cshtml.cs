#pragma checksum "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BranchMedium\ShowTermList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cee3b83e22554f72a00e01100649d3e206fa46b7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_BranchMedium_ShowTermList), @"mvc.1.0.view", @"/Views/BranchMedium/ShowTermList.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/BranchMedium/ShowTermList.cshtml", typeof(AspNetCore.Views_BranchMedium_ShowTermList))]
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
#line 1 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BranchMedium\ShowTermList.cshtml"
using SinePulse.EMS.Domain.Features;

#line default
#line hidden
#line 2 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BranchMedium\ShowTermList.cshtml"
using SinePulse.EMS.Messages.Model.Enums;

#line default
#line hidden
#line 4 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BranchMedium\ShowTermList.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cee3b83e22554f72a00e01100649d3e206fa46b7", @"/Views/BranchMedium/ShowTermList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9011cacf54d8ae45691a3deaab04c36239ace056", @"/Views/_ViewImports.cshtml")]
    public class Views_BranchMedium_ShowTermList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ShowTermListViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(191, 147, true);
            WriteLiteral("\r\n<table class=\"table table-striped table-hover\" id=\"sample_2\">\r\n    <thead class=\"filterCriteria\">\r\n        <tr class=\"uppercase\">\r\n          <th>");
            EndContext();
            BeginContext(339, 34, false);
#line 10 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BranchMedium\ShowTermList.cshtml"
         Write(Localizer["ShowTermList.TermName"]);

#line default
#line hidden
            EndContext();
            BeginContext(373, 21, true);
            WriteLiteral("</th>\r\n          <th>");
            EndContext();
            BeginContext(395, 35, false);
#line 11 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BranchMedium\ShowTermList.cshtml"
         Write(Localizer["ShowTermList.StartDate"]);

#line default
#line hidden
            EndContext();
            BeginContext(430, 21, true);
            WriteLiteral("</th>\r\n          <th>");
            EndContext();
            BeginContext(452, 33, false);
#line 12 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BranchMedium\ShowTermList.cshtml"
         Write(Localizer["ShowTermList.EndDate"]);

#line default
#line hidden
            EndContext();
            BeginContext(485, 21, true);
            WriteLiteral("</th>\r\n          <th>");
            EndContext();
            BeginContext(507, 33, false);
#line 13 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BranchMedium\ShowTermList.cshtml"
         Write(Localizer["ShowTermList.Session"]);

#line default
#line hidden
            EndContext();
            BeginContext(540, 21, true);
            WriteLiteral("</th>\r\n          <th>");
            EndContext();
            BeginContext(562, 32, false);
#line 14 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BranchMedium\ShowTermList.cshtml"
         Write(Localizer["ShowTermList.Action"]);

#line default
#line hidden
            EndContext();
            BeginContext(594, 36, true);
            WriteLiteral("</th>\r\n        </tr>\r\n    </thead>\r\n");
            EndContext();
#line 17 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BranchMedium\ShowTermList.cshtml"
      
        if (Model.Terms.Count > 0)
        {
            foreach (var termModel in Model.Terms)
            {
                var termStartDate = termModel.StartDate.ToString("dd MMMM, yyyy");
                var termEndDate = termModel.EndDate.ToString("dd MMMM, yyyy");

#line default
#line hidden
            BeginContext(916, 48, true);
            WriteLiteral("                <tr>\r\n                    <td>\r\n");
            EndContext();
#line 26 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BranchMedium\ShowTermList.cshtml"
                         if (Model.HasPermission(Feature.ExaminationEnum.ViewExamTerm.ToString()))
                        {

#line default
#line hidden
            BeginContext(1091, 30, true);
            WriteLiteral("                            <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1121, "\"", 1167, 2);
            WriteAttributeValue("", 1128, "/Term/ViewTerm?termId=", 1128, 22, true);
#line 28 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BranchMedium\ShowTermList.cshtml"
WriteAttributeValue("", 1150, termModel.TermId, 1150, 17, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1168, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(1170, 18, false);
#line 28 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BranchMedium\ShowTermList.cshtml"
                                                                         Write(termModel.TermName);

#line default
#line hidden
            EndContext();
            BeginContext(1188, 6, true);
            WriteLiteral("</a>\r\n");
            EndContext();
#line 29 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BranchMedium\ShowTermList.cshtml"
                        }
                        else
                        {
                            

#line default
#line hidden
            BeginContext(1307, 18, false);
#line 32 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BranchMedium\ShowTermList.cshtml"
                       Write(termModel.TermName);

#line default
#line hidden
            EndContext();
#line 32 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BranchMedium\ShowTermList.cshtml"
                                               
                        }

#line default
#line hidden
            BeginContext(1354, 51, true);
            WriteLiteral("                    </td>\r\n                    <td>");
            EndContext();
            BeginContext(1406, 13, false);
#line 35 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BranchMedium\ShowTermList.cshtml"
                   Write(termStartDate);

#line default
#line hidden
            EndContext();
            BeginContext(1419, 31, true);
            WriteLiteral("</td>\r\n                    <td>");
            EndContext();
            BeginContext(1451, 11, false);
#line 36 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BranchMedium\ShowTermList.cshtml"
                   Write(termEndDate);

#line default
#line hidden
            EndContext();
            BeginContext(1462, 31, true);
            WriteLiteral("</td>\r\n                    <td>");
            EndContext();
            BeginContext(1494, 59, false);
#line 37 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BranchMedium\ShowTermList.cshtml"
                   Write(Html.DisplayFor(model => termModel.SessionData.SessionText));

#line default
#line hidden
            EndContext();
            BeginContext(1553, 33, true);
            WriteLiteral("</td>\r\n                    <td>\r\n");
            EndContext();
#line 39 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BranchMedium\ShowTermList.cshtml"
                         if (Model.HasPermission(Feature.ExaminationEnum.EditExamTerm.ToString()))
                        {

#line default
#line hidden
            BeginContext(1713, 30, true);
            WriteLiteral("                            <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1743, "\"", 1787, 2);
            WriteAttributeValue("", 1750, "/Term/UpdateTerm?id=", 1750, 20, true);
#line 41 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BranchMedium\ShowTermList.cshtml"
WriteAttributeValue("", 1770, termModel.TermId, 1770, 17, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1788, 65, true);
            WriteLiteral(" class=\"action-link\"><i class=\"fa fa-edit action-icon\"></i></a>\r\n");
            EndContext();
#line 42 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BranchMedium\ShowTermList.cshtml"
                        }

#line default
#line hidden
            BeginContext(1880, 50, true);
            WriteLiteral("                    </td>\r\n                </tr>\r\n");
            EndContext();
#line 45 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BranchMedium\ShowTermList.cshtml"
            }
        }
    

#line default
#line hidden
            BeginContext(1963, 10, true);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ShowTermListViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
