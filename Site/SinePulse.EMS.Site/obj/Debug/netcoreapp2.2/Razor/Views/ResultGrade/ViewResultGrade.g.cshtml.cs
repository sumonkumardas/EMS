#pragma checksum "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ResultGrade\ViewResultGrade.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "efaf1e275ca2fc48e1b34447c069f66657b57ac9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ResultGrade_ViewResultGrade), @"mvc.1.0.view", @"/Views/ResultGrade/ViewResultGrade.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/ResultGrade/ViewResultGrade.cshtml", typeof(AspNetCore.Views_ResultGrade_ViewResultGrade))]
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
#line 2 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ResultGrade\ViewResultGrade.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"efaf1e275ca2fc48e1b34447c069f66657b57ac9", @"/Views/ResultGrade/ViewResultGrade.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9011cacf54d8ae45691a3deaab04c36239ace056", @"/Views/_ViewImports.cshtml")]
    public class Views_ResultGrade_ViewResultGrade : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SinePulse.EMS.Site.Models.ResultGradeViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 4 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ResultGrade\ViewResultGrade.cshtml"
  
    ViewData["Title"] = "ViewResultGrade";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(233, 258, true);
            WriteLiteral(@"
<div class=""page-container"">
    <!-- BEGIN CONTENT -->
    <div class=""page-content-wrapper"">
        <div class=""page-content"">
            <div class=""portlet box form-conatiner"">
                <div class=""icon-container"">
                    <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 491, "\"", 552, 2);
            WriteAttributeValue("", 498, "/ResultGrade/UpdateResultGrade?resultGradeId=", 498, 45, true);
#line 15 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ResultGrade\ViewResultGrade.cshtml"
WriteAttributeValue("", 543, Model.Id, 543, 9, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(553, 475, true);
            WriteLiteral(@" class=""fa fa-edit action-link""></a>

                    <a href=""#"" class=""fa fa-tasks action-link""></a>
                </div>

                <div class=""portlet-body form"">

                    <div class=""container-fluid"">
                        <div class=""row-fluid "">
                            <div class=""col-md-6"">
                                <div class=""row custom-row"">
                                    <div class=""col-md-4 title""><strong> ");
            EndContext();
            BeginContext(1029, 40, false);
#line 26 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ResultGrade\ViewResultGrade.cshtml"
                                                                    Write(Localizer["ViewResultGrade.GradeLetter"]);

#line default
#line hidden
            EndContext();
            BeginContext(1069, 82, true);
            WriteLiteral(" </strong></div>\r\n                                    <div class=\"col-md-8 value\">");
            EndContext();
            BeginContext(1152, 43, false);
#line 27 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ResultGrade\ViewResultGrade.cshtml"
                                                           Write(Html.DisplayFor(model => model.GradeLetter));

#line default
#line hidden
            EndContext();
            BeginContext(1195, 271, true);
            WriteLiteral(@"</div>
                                </div>
                            </div>
                            <div class=""col-md-6"">
                                <div class=""row custom-row"">
                                    <div class=""col-md-4 title""><strong> ");
            EndContext();
            BeginContext(1467, 39, false);
#line 32 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ResultGrade\ViewResultGrade.cshtml"
                                                                    Write(Localizer["ViewResultGrade.GradePoint"]);

#line default
#line hidden
            EndContext();
            BeginContext(1506, 82, true);
            WriteLiteral(" </strong></div>\r\n                                    <div class=\"col-md-8 value\">");
            EndContext();
            BeginContext(1589, 42, false);
#line 33 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ResultGrade\ViewResultGrade.cshtml"
                                                           Write(Html.DisplayFor(model => model.GradePoint));

#line default
#line hidden
            EndContext();
            BeginContext(1631, 433, true);
            WriteLiteral(@"</div>
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
            BeginContext(2065, 38, false);
#line 43 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ResultGrade\ViewResultGrade.cshtml"
                                                                   Write(Localizer["ViewResultGrade.StartMark"]);

#line default
#line hidden
            EndContext();
            BeginContext(2103, 81, true);
            WriteLiteral("</strong></div>\r\n                                    <div class=\"col-md-8 value\">");
            EndContext();
            BeginContext(2185, 41, false);
#line 44 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ResultGrade\ViewResultGrade.cshtml"
                                                           Write(Html.DisplayFor(model => model.StartMark));

#line default
#line hidden
            EndContext();
            BeginContext(2226, 270, true);
            WriteLiteral(@"</div>
                                </div>
                            </div>
                            <div class=""col-md-6"">
                                <div class=""row custom-row"">
                                    <div class=""col-md-4 title""><strong>");
            EndContext();
            BeginContext(2497, 36, false);
#line 49 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ResultGrade\ViewResultGrade.cshtml"
                                                                   Write(Localizer["ViewResultGrade.EndMark"]);

#line default
#line hidden
            EndContext();
            BeginContext(2533, 81, true);
            WriteLiteral("</strong></div>\r\n                                    <div class=\"col-md-8 value\">");
            EndContext();
            BeginContext(2615, 39, false);
#line 50 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ResultGrade\ViewResultGrade.cshtml"
                                                           Write(Html.DisplayFor(model => model.EndMark));

#line default
#line hidden
            EndContext();
            BeginContext(2654, 431, true);
            WriteLiteral(@"</div>
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
            BeginContext(3086, 35, false);
#line 59 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ResultGrade\ViewResultGrade.cshtml"
                                                                   Write(Localizer["ViewResultGrade.Status"]);

#line default
#line hidden
            EndContext();
            BeginContext(3121, 81, true);
            WriteLiteral("</strong></div>\r\n                                    <div class=\"col-md-8 value\">");
            EndContext();
            BeginContext(3203, 38, false);
#line 60 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ResultGrade\ViewResultGrade.cshtml"
                                                           Write(Html.DisplayFor(model => model.Status));

#line default
#line hidden
            EndContext();
            BeginContext(3241, 232, true);
            WriteLiteral("</div>\r\n                                </div>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SinePulse.EMS.Site.Models.ResultGradeViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
