#pragma checksum "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Student\TermTestMarkSheet.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "882d0ad0e52ba8efb4c2b5e77c201293c36a462d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Student_TermTestMarkSheet), @"mvc.1.0.view", @"/Views/Student/TermTestMarkSheet.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Student/TermTestMarkSheet.cshtml", typeof(AspNetCore.Views_Student_TermTestMarkSheet))]
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
#line 1 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Student\TermTestMarkSheet.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"882d0ad0e52ba8efb4c2b5e77c201293c36a462d", @"/Views/Student/TermTestMarkSheet.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9011cacf54d8ae45691a3deaab04c36239ace056", @"/Views/_ViewImports.cshtml")]
    public class Views_Student_TermTestMarkSheet : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<StudentMarkSheetViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(114, 93, true);
            WriteLiteral("<div class=\"col-md-6\">\r\n  <div class=\"form-group\">\r\n    <div class=\"col-md-4\">\r\n      <label>");
            EndContext();
            BeginContext(208, 38, false);
#line 7 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Student\TermTestMarkSheet.cshtml"
        Write(Localizer["TermTestMarkSheet.Session"]);

#line default
#line hidden
            EndContext();
            BeginContext(246, 87, true);
            WriteLiteral("</label><span class=\"required\">*</span>\r\n    </div>\r\n    <div class=\"col-md-8\">\r\n      ");
            EndContext();
            BeginContext(334, 218, false);
#line 10 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Student\TermTestMarkSheet.cshtml"
 Write(Html.DropDownListFor(x => x.SessionId, new SelectList(Model.Sessions, "Id", "SessionName"), @Localizer["TermTestMarkSheet.SelectSession"].Value, new { @class = "form-control", id = "sessionDropDown", required = true }));

#line default
#line hidden
            EndContext();
            BeginContext(552, 8, true);
            WriteLiteral("\r\n      ");
            EndContext();
            BeginContext(560, 64, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "882d0ad0e52ba8efb4c2b5e77c201293c36a462d5086", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#line 11 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Student\TermTestMarkSheet.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.SessionId);

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(624, 125, true);
            WriteLiteral("\r\n    </div>\r\n  </div>\r\n</div>\r\n<div class=\"col-md-6\">\r\n  <div class=\"form-group\">\r\n    <div class=\"col-md-4\">\r\n      <label>");
            EndContext();
            BeginContext(750, 39, false);
#line 18 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Student\TermTestMarkSheet.cshtml"
        Write(Localizer["TermTestMarkSheet.ExamTerm"]);

#line default
#line hidden
            EndContext();
            BeginContext(789, 87, true);
            WriteLiteral("</label><span class=\"required\">*</span>\r\n    </div>\r\n    <div class=\"col-md-8\">\r\n      ");
            EndContext();
            BeginContext(877, 196, false);
#line 21 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Student\TermTestMarkSheet.cshtml"
 Write(Html.DropDownListFor(x => x.TermId, Enumerable.Empty<SelectListItem>(), @Localizer["TermTestMarkSheet.SelectTerm"].Value, new { @class = "form-control", id = "examTermDropDown", required = true }));

#line default
#line hidden
            EndContext();
            BeginContext(1073, 8, true);
            WriteLiteral("\r\n      ");
            EndContext();
            BeginContext(1081, 61, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "882d0ad0e52ba8efb4c2b5e77c201293c36a462d7978", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#line 22 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Student\TermTestMarkSheet.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.TermId);

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1142, 193, true);
            WriteLiteral("\r\n    </div>\r\n  </div>\r\n</div>\r\n<br style=\"line-height:5;\">\r\n<div>\r\n  <table class=\"table table-striped table-hover\" id=\"sample_3\">\r\n    <thead class=\"filterCriteria\">\r\n      <tr>\r\n        <th>");
            EndContext();
            BeginContext(1336, 38, false);
#line 31 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Student\TermTestMarkSheet.cshtml"
       Write(Localizer["TermTestMarkSheet.Subject"]);

#line default
#line hidden
            EndContext();
            BeginContext(1374, 19, true);
            WriteLiteral("</th>\r\n        <th>");
            EndContext();
            BeginContext(1394, 39, false);
#line 32 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Student\TermTestMarkSheet.cshtml"
       Write(Localizer["TermTestMarkSheet.ExamType"]);

#line default
#line hidden
            EndContext();
            BeginContext(1433, 19, true);
            WriteLiteral("</th>\r\n        <th>");
            EndContext();
            BeginContext(1453, 40, false);
#line 33 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Student\TermTestMarkSheet.cshtml"
       Write(Localizer["TermTestMarkSheet.PassMarks"]);

#line default
#line hidden
            EndContext();
            BeginContext(1493, 19, true);
            WriteLiteral("</th>\r\n        <th>");
            EndContext();
            BeginContext(1513, 40, false);
#line 34 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Student\TermTestMarkSheet.cshtml"
       Write(Localizer["TermTestMarkSheet.FullMarks"]);

#line default
#line hidden
            EndContext();
            BeginContext(1553, 19, true);
            WriteLiteral("</th>\r\n        <th>");
            EndContext();
            BeginContext(1573, 44, false);
#line 35 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Student\TermTestMarkSheet.cshtml"
       Write(Localizer["TermTestMarkSheet.ObtainedMarks"]);

#line default
#line hidden
            EndContext();
            BeginContext(1617, 19, true);
            WriteLiteral("</th>\r\n        <th>");
            EndContext();
            BeginContext(1637, 41, false);
#line 36 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Student\TermTestMarkSheet.cshtml"
       Write(Localizer["TermTestMarkSheet.GraceMarks"]);

#line default
#line hidden
            EndContext();
            BeginContext(1678, 19, true);
            WriteLiteral("</th>\r\n        <th>");
            EndContext();
            BeginContext(1698, 38, false);
#line 37 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Student\TermTestMarkSheet.cshtml"
       Write(Localizer["TermTestMarkSheet.Remarks"]);

#line default
#line hidden
            EndContext();
            BeginContext(1736, 93, true);
            WriteLiteral("</th>\r\n      </tr>\r\n    </thead>\r\n    <tbody id=\"marksTableBody\"></tbody>\r\n  </table>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<StudentMarkSheetViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591