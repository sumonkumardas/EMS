#pragma checksum "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ServiceCharge\EditServiceCharge.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d7c8d1fe5c6a45a35c6a8919ee596287fb8fc625"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ServiceCharge_EditServiceCharge), @"mvc.1.0.view", @"/Views/ServiceCharge/EditServiceCharge.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/ServiceCharge/EditServiceCharge.cshtml", typeof(AspNetCore.Views_ServiceCharge_EditServiceCharge))]
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
#line 1 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ServiceCharge\EditServiceCharge.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d7c8d1fe5c6a45a35c6a8919ee596287fb8fc625", @"/Views/ServiceCharge/EditServiceCharge.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9011cacf54d8ae45691a3deaab04c36239ace056", @"/Views/_ViewImports.cshtml")]
    public class Views_ServiceCharge_EditServiceCharge : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EditServiceChargeViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("runat", new global::Microsoft.AspNetCore.Html.HtmlString("server"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "number", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "text", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control datepicker"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "EditServiceCharge", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal box-shadow-form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("form_sample_3"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 4 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ServiceCharge\EditServiceCharge.cshtml"
  
  ViewData["Title"] = "EditServiceCharge";
  Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(211, 229, true);
            WriteLiteral("\r\n<div class=\"page-container\">\r\n  <!-- BEGIN CONTENT -->\r\n  <div class=\"page-content-wrapper\">\r\n    <div class=\"page-content\">\r\n      <div class=\"portlet-body form\">\r\n        <!-- BEGIN FORM-->\r\n        <div class=\"title-on-top\">");
            EndContext();
            BeginContext(441, 36, false);
#line 15 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ServiceCharge\EditServiceCharge.cshtml"
                             Write(Localizer["EditServiceCharge.Title"]);

#line default
#line hidden
            EndContext();
            BeginContext(477, 16, true);
            WriteLiteral("</div>\r\n        ");
            EndContext();
            BeginContext(493, 3601, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d7c8d1fe5c6a45a35c6a8919ee596287fb8fc6258465", async() => {
                BeginContext(589, 49, true);
                WriteLiteral("\r\n          <div class=\"form-body\">\r\n            ");
                EndContext();
                BeginContext(638, 66, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d7c8d1fe5c6a45a35c6a8919ee596287fb8fc6258898", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper);
#line 18 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ServiceCharge\EditServiceCharge.cshtml"
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
                BeginContext(704, 2, true);
                WriteLiteral("\r\n");
                EndContext();
                BeginContext(738, 12, true);
                WriteLiteral("            ");
                EndContext();
                BeginContext(750, 64, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "d7c8d1fe5c6a45a35c6a8919ee596287fb8fc62510844", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#line 20 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ServiceCharge\EditServiceCharge.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.ServiceChargeId);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(814, 14, true);
                WriteLiteral("\r\n            ");
                EndContext();
                BeginContext(828, 63, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "d7c8d1fe5c6a45a35c6a8919ee596287fb8fc62512810", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#line 21 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ServiceCharge\EditServiceCharge.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.BranchMediumId);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(891, 2, true);
                WriteLiteral("\r\n");
                EndContext();
                BeginContext(925, 213, true);
                WriteLiteral("            <div class=\"row-fluid col-md-12 zero-padding-row\">\r\n              <div class=\"col-md-6\">\r\n                <div class=\"form-group\">\r\n                  <div class=\"col-md-4\">\r\n                    <label>");
                EndContext();
                BeginContext(1139, 47, false);
#line 27 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ServiceCharge\EditServiceCharge.cshtml"
                      Write(Localizer["EditServiceCharge.PerStudentCharge"]);

#line default
#line hidden
                EndContext();
                BeginContext(1186, 149, true);
                WriteLiteral(" </label><span class=\"required\">*</span>\r\n                  </div>\r\n                  <div class=\"col-md-8 .position-relative\">\r\n                    ");
                EndContext();
                BeginContext(1335, 71, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "d7c8d1fe5c6a45a35c6a8919ee596287fb8fc62515698", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#line 30 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ServiceCharge\EditServiceCharge.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.PerStudentCharge);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(1406, 22, true);
                WriteLiteral("\r\n                    ");
                EndContext();
                BeginContext(1428, 71, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d7c8d1fe5c6a45a35c6a8919ee596287fb8fc62517675", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#line 31 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ServiceCharge\EditServiceCharge.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.PerStudentCharge);

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
                BeginContext(1499, 72, true);
                WriteLiteral("\r\n                  </div>\r\n                  <p class=\"floating-label\">");
                EndContext();
                BeginContext(1572, 35, false);
#line 33 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ServiceCharge\EditServiceCharge.cshtml"
                                       Write(Localizer["EditServiceCharge.Taka"]);

#line default
#line hidden
                EndContext();
                BeginContext(1607, 201, true);
                WriteLiteral("</p>\r\n                </div>\r\n              </div>\r\n              <div class=\"col-md-6\">\r\n                <div class=\"form-group\">\r\n                  <div class=\"col-md-4\">\r\n                    <label>");
                EndContext();
                BeginContext(1809, 43, false);
#line 39 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ServiceCharge\EditServiceCharge.cshtml"
                      Write(Localizer["EditServiceCharge.BufferPeriod"]);

#line default
#line hidden
                EndContext();
                BeginContext(1852, 149, true);
                WriteLiteral(" </label><span class=\"required\">*</span>\r\n                  </div>\r\n                  <div class=\"col-md-8 .position-relative\">\r\n                    ");
                EndContext();
                BeginContext(2001, 74, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "d7c8d1fe5c6a45a35c6a8919ee596287fb8fc62520813", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#line 42 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ServiceCharge\EditServiceCharge.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.PaymentBufferPeriod);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2075, 22, true);
                WriteLiteral("\r\n                    ");
                EndContext();
                BeginContext(2097, 74, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d7c8d1fe5c6a45a35c6a8919ee596287fb8fc62522793", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#line 43 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ServiceCharge\EditServiceCharge.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.PaymentBufferPeriod);

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
                BeginContext(2171, 72, true);
                WriteLiteral("\r\n                  </div>\r\n                  <p class=\"floating-label\">");
                EndContext();
                BeginContext(2244, 35, false);
#line 45 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ServiceCharge\EditServiceCharge.cshtml"
                                       Write(Localizer["EditServiceCharge.Days"]);

#line default
#line hidden
                EndContext();
                BeginContext(2279, 327, true);
                WriteLiteral(@"</p>
                </div>
              </div>
            </div>
            <div class=""clearfix""></div>
            <div class=""row-fluid col-md-12 zero-padding-row"">
              <div class=""col-md-6"">
                <div class=""form-group"">
                  <div class=""col-md-4"">
                    <label>");
                EndContext();
                BeginContext(2607, 44, false);
#line 54 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ServiceCharge\EditServiceCharge.cshtml"
                      Write(Localizer["EditServiceCharge.EffectiveDate"]);

#line default
#line hidden
                EndContext();
                BeginContext(2651, 130, true);
                WriteLiteral(" </label><span class=\"required\">*</span>\r\n                  </div>\r\n                  <div class=\"col-md-8\">\r\n                    ");
                EndContext();
                BeginContext(2781, 75, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "d7c8d1fe5c6a45a35c6a8919ee596287fb8fc62526035", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#line 57 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ServiceCharge\EditServiceCharge.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.EffectiveDate);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_5.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2856, 22, true);
                WriteLiteral("\r\n                    ");
                EndContext();
                BeginContext(2878, 68, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d7c8d1fe5c6a45a35c6a8919ee596287fb8fc62528010", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#line 58 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ServiceCharge\EditServiceCharge.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.EffectiveDate);

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
                BeginContext(2946, 223, true);
                WriteLiteral("\r\n                  </div>\r\n                </div>\r\n              </div>\r\n              <div class=\"col-md-6\">\r\n                <div class=\"form-group\">\r\n                  <div class=\"col-md-4\">\r\n                    <label>");
                EndContext();
                BeginContext(3170, 38, false);
#line 65 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ServiceCharge\EditServiceCharge.cshtml"
                      Write(Localizer["EditServiceCharge.EndDate"]);

#line default
#line hidden
                EndContext();
                BeginContext(3208, 130, true);
                WriteLiteral(" </label><span class=\"required\">*</span>\r\n                  </div>\r\n                  <div class=\"col-md-8\">\r\n                    ");
                EndContext();
                BeginContext(3338, 69, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "d7c8d1fe5c6a45a35c6a8919ee596287fb8fc62530629", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#line 68 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ServiceCharge\EditServiceCharge.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.EndDate);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_5.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(3407, 22, true);
                WriteLiteral("\r\n                    ");
                EndContext();
                BeginContext(3429, 62, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d7c8d1fe5c6a45a35c6a8919ee596287fb8fc62532598", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#line 69 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ServiceCharge\EditServiceCharge.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.EndDate);

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
                BeginContext(3491, 241, true);
                WriteLiteral("\r\n                  </div>\r\n                </div>\r\n              </div>\r\n            </div>\r\n            <br />\r\n            <div class=\"btn-container\">\r\n              <button type=\"button\" class=\"btn custom-btn custom-cancel-btn pull-left\"");
                EndContext();
                BeginWriteAttribute("onclick", " onclick=\"", 3732, "\"", 3797, 5);
                WriteAttributeValue("", 3742, "location.href", 3742, 13, true);
                WriteAttributeValue(" ", 3755, "=", 3756, 2, true);
                WriteAttributeValue(" ", 3757, "\'", 3758, 2, true);
#line 76 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ServiceCharge\EditServiceCharge.cshtml"
WriteAttributeValue("", 3759, Url.Action("Index", "ServiceCharge"), 3759, 37, false);

#line default
#line hidden
                WriteAttributeValue("", 3796, "\'", 3796, 1, true);
                EndWriteAttribute();
                BeginContext(3798, 1, true);
                WriteLiteral(">");
                EndContext();
                BeginContext(3800, 43, false);
#line 76 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ServiceCharge\EditServiceCharge.cshtml"
                                                                                                                                                    Write(Localizer["EditServiceCharge.CancelButton"]);

#line default
#line hidden
                EndContext();
                BeginContext(3843, 99, true);
                WriteLiteral("</button>\r\n              <button type=\"submit\" class=\"btn custom-btn custom-cancel-btn pull-right\">");
                EndContext();
                BeginContext(3943, 43, false);
#line 77 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ServiceCharge\EditServiceCharge.cshtml"
                                                                                   Write(Localizer["EditServiceCharge.UpdateButton"]);

#line default
#line hidden
                EndContext();
                BeginContext(3986, 101, true);
                WriteLiteral("</button>\r\n              <div class=\"clearfix\"></div>\r\n            </div>\r\n          </div>\r\n        ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4094, 72, true);
            WriteLiteral("\r\n        <!-- END FORM-->\r\n      </div>\r\n    </div>\r\n  </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EditServiceChargeViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
