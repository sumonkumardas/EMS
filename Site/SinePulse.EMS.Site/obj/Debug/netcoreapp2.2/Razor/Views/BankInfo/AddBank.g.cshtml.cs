#pragma checksum "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\AddBank.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fcf7c5a360384acc979a614c94dc93e6cc2b7f0f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_BankInfo_AddBank), @"mvc.1.0.view", @"/Views/BankInfo/AddBank.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/BankInfo/AddBank.cshtml", typeof(AspNetCore.Views_BankInfo_AddBank))]
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
#line 1 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\AddBank.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fcf7c5a360384acc979a614c94dc93e6cc2b7f0f", @"/Views/BankInfo/AddBank.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9011cacf54d8ae45691a3deaab04c36239ace056", @"/Views/_ViewImports.cshtml")]
    public class Views_BankInfo_AddBank : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AddBankViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "text", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AddBank", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal box-shadow-form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("form_sample_3"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 4 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\AddBank.cshtml"
  
  ViewData["Title"] = "AddBank";
  Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(191, 231, true);
            WriteLiteral("\r\n\r\n<div class=\"page-container\">\r\n  <!-- BEGIN CONTENT -->\r\n  <div class=\"page-content-wrapper\">\r\n    <div class=\"page-content\">\r\n      <div class=\"portlet-body form\">\r\n        <!-- BEGIN FORM-->\r\n        <div class=\"title-on-top\">");
            EndContext();
            BeginContext(423, 26, false);
#line 16 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\AddBank.cshtml"
                             Write(Localizer["AddBank.Title"]);

#line default
#line hidden
            EndContext();
            BeginContext(449, 16, true);
            WriteLiteral("</div>\r\n        ");
            EndContext();
            BeginContext(465, 1423, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fcf7c5a360384acc979a614c94dc93e6cc2b7f0f7275", async() => {
                BeginContext(551, 49, true);
                WriteLiteral("\r\n          <div class=\"form-body\">\r\n            ");
                EndContext();
                BeginContext(600, 66, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fcf7c5a360384acc979a614c94dc93e6cc2b7f0f7708", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper);
#line 19 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\AddBank.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary = global::Microsoft.AspNetCore.Mvc.Rendering.ValidationSummary.ModelOnly;

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-summary", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(666, 14, true);
                WriteLiteral("\r\n            ");
                EndContext();
                BeginContext(680, 48, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "fcf7c5a360384acc979a614c94dc93e6cc2b7f0f9528", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#line 20 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\AddBank.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.BranchMediumId);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(728, 189, true);
                WriteLiteral("\r\n            <div class=\"row-fluid\">\r\n              <div class=\"col-md-12\">\r\n                <div class=\"form-group\">\r\n                  <div class=\"col-md-2\">\r\n                    <label>");
                EndContext();
                BeginContext(918, 29, false);
#line 25 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\AddBank.cshtml"
                      Write(Localizer["AddBank.BankName"]);

#line default
#line hidden
                EndContext();
                BeginContext(947, 131, true);
                WriteLiteral(" </label><span class=\"required\">*</span>\r\n                  </div>\r\n                  <div class=\"col-md-10\">\r\n                    ");
                EndContext();
                BeginContext(1078, 61, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "fcf7c5a360384acc979a614c94dc93e6cc2b7f0f12123", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#line 28 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\AddBank.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.BankName);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1139, 22, true);
                WriteLiteral("\r\n                    ");
                EndContext();
                BeginContext(1161, 63, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fcf7c5a360384acc979a614c94dc93e6cc2b7f0f14077", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#line 29 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\AddBank.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.BankName);

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
                BeginContext(1224, 283, true);
                WriteLiteral(@"
                  </div>
                </div>
              </div>
            </div>
            <div class=""clearfix""></div>
            <br />
            <div class=""btn-container"">
              <button type=""button"" class=""btn custom-btn custom-cancel-btn pull-left""");
                EndContext();
                BeginWriteAttribute("onclick", " onclick=\"", 1507, "\"", 1611, 5);
                WriteAttributeValue("", 1517, "location.href", 1517, 13, true);
                WriteAttributeValue(" ", 1530, "=", 1531, 2, true);
                WriteAttributeValue(" ", 1532, "\'/BranchMedium/ViewBranchMedium?branchMediumId=", 1533, 48, true);
#line 37 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\AddBank.cshtml"
WriteAttributeValue("", 1580, Model.BranchMediumId, 1580, 21, false);

#line default
#line hidden
                WriteAttributeValue("", 1601, "#tab_bank\'", 1601, 10, true);
                EndWriteAttribute();
                BeginContext(1612, 1, true);
                WriteLiteral(">");
                EndContext();
                BeginContext(1614, 33, false);
#line 37 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\AddBank.cshtml"
                                                                                                                                                                                           Write(Localizer["AddBank.CancleButton"]);

#line default
#line hidden
                EndContext();
                BeginContext(1647, 99, true);
                WriteLiteral("</button>\r\n              <button type=\"submit\" class=\"btn custom-btn custom-cancel-btn pull-right\">");
                EndContext();
                BeginContext(1747, 33, false);
#line 38 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\AddBank.cshtml"
                                                                                   Write(Localizer["AddBank.CreateButton"]);

#line default
#line hidden
                EndContext();
                BeginContext(1780, 101, true);
                WriteLiteral("</button>\r\n              <div class=\"clearfix\"></div>\r\n            </div>\r\n          </div>\r\n        ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1888, 74, true);
            WriteLiteral("\r\n        <!-- END FORM-->\r\n      </div>\r\n    </div>\r\n  </div>\r\n</div>\r\n\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(1980, 2, true);
                WriteLiteral("\r\n");
                EndContext();
#line 50 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\BankInfo\AddBank.cshtml"
    await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
            }
            );
            BeginContext(2051, 2, true);
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AddBankViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
