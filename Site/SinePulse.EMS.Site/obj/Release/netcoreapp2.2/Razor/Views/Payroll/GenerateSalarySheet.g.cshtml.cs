#pragma checksum "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Payroll\GenerateSalarySheet.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7a8efb8dd2010ae84a69f02cd15083187e2ac60b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Payroll_GenerateSalarySheet), @"mvc.1.0.view", @"/Views/Payroll/GenerateSalarySheet.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Payroll/GenerateSalarySheet.cshtml", typeof(AspNetCore.Views_Payroll_GenerateSalarySheet))]
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
#line 1 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Payroll\GenerateSalarySheet.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#line 2 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Payroll\GenerateSalarySheet.cshtml"
using SinePulse.EMS.ProjectPrimitives;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7a8efb8dd2010ae84a69f02cd15083187e2ac60b", @"/Views/Payroll/GenerateSalarySheet.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9011cacf54d8ae45691a3deaab04c36239ace056", @"/Views/_ViewImports.cshtml")]
    public class Views_Payroll_GenerateSalarySheet : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<GenerateSalarySheetViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "number", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("step", new global::Microsoft.AspNetCore.Html.HtmlString("any"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "OtherDeductions", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "0", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("autocomplete", new global::Microsoft.AspNetCore.Html.HtmlString("off"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("text-align: right; width: 90px"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "GenerateSalarySheet", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal box-shadow-form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_11 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("form_sample_3"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_12 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/assets/global/scripts/Payroll/generateSalarySheet.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_13 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/javascript"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 5 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Payroll\GenerateSalarySheet.cshtml"
  
    ViewData["Title"] = "GenerateSalarySheet";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(259, 330, true);
            WriteLiteral(@"<!-- BEGIN CONTAINER -->

<div class=""page-container"">
    <!-- BEGIN CONTENT -->
    <div class=""page-content-wrapper"">
        <div class=""page-content"">
            <div class=""portlet-body form"">
                <!-- BEGIN FORM-->
                <div class=""title-on-top"">Generate Salary Sheet</div>
                ");
            EndContext();
            BeginContext(589, 6871, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7a8efb8dd2010ae84a69f02cd15083187e2ac60b9972", async() => {
                BeginContext(687, 71, true);
                WriteLiteral("\r\n                    <div class=\"form-body\">\r\n                        ");
                EndContext();
                BeginContext(758, 47, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "7a8efb8dd2010ae84a69f02cd15083187e2ac60b10427", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 20 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Payroll\GenerateSalarySheet.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.BranchMediumId);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(805, 453, true);
                WriteLiteral(@"
                        <div class=""row-fluid col-md-12 zero-padding-row"">
                            <div class=""col-md-6"">
                                <div class=""form-group"">
                                    <div class=""col-md-4"">
                                        <label>Year</label>
                                    </div>
                                    <div class=""col-md-8"">
                                        ");
                EndContext();
                BeginContext(1259, 127, false);
#line 28 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Payroll\GenerateSalarySheet.cshtml"
                                   Write(Html.DropDownListFor(x => x.Year, new SelectList(Model.Years), "Select Year", new {@class = "form-control", id="yearDropDown"}));

#line default
#line hidden
                EndContext();
                BeginContext(1386, 498, true);
                WriteLiteral(@"
                                    </div>
                                </div>
                            </div>
                            <div class=""col-md-6"">
                                <div class=""form-group"">
                                    <div class=""col-md-4"">
                                        <label>Month</label>
                                    </div>
                                    <div class=""col-md-8"">
                                        ");
                EndContext();
                BeginContext(1884, 219, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7a8efb8dd2010ae84a69f02cd15083187e2ac60b13783", async() => {
                    BeginContext(1977, 46, true);
                    WriteLiteral("\r\n                                            ");
                    EndContext();
                    BeginContext(2023, 29, false);
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7a8efb8dd2010ae84a69f02cd15083187e2ac60b14234", async() => {
                        BeginContext(2031, 12, true);
                        WriteLiteral("Select Month");
                        EndContext();
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    EndContext();
                    BeginContext(2052, 42, true);
                    WriteLiteral("\r\n                                        ");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
#line 38 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Payroll\GenerateSalarySheet.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Month);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
#line 38 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Payroll\GenerateSalarySheet.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = Html.GetEnumSelectList<MonthType>();

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-items", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2103, 667, true);
                WriteLiteral(@"
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class=""clearfix""></div>
                        <div class=""row-fluid col-md-12 zero-padding-row"">
                            <div class=""col-md-6"">
                                <div class=""form-group"">
                                    <div class=""col-md-4"">
                                        <label>Bank Account</label>
                                    </div>
                                    <div class=""col-md-8"">
                                        ");
                EndContext();
                BeginContext(2771, 168, false);
#line 53 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Payroll\GenerateSalarySheet.cshtml"
                                   Write(Html.DropDownListFor(x => x.BankAccount, new SelectList(Model.BankAccounts, "AccountHeadName", "AccountHeadName"), "Select Bank Account", new {@class = "form-control"}));

#line default
#line hidden
                EndContext();
                BeginContext(2939, 312, true);
                WriteLiteral(@"
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class=""clearfix""></div>
                        <div class=""row-fluid col-md-12 zero-padding-row"">
                            ");
                EndContext();
                BeginContext(3251, 66, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7a8efb8dd2010ae84a69f02cd15083187e2ac60b19121", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper);
#line 60 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Payroll\GenerateSalarySheet.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary = global::Microsoft.AspNetCore.Mvc.Rendering.ValidationSummary.ModelOnly;

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-summary", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(3317, 389, true);
                WriteLiteral(@"
                            <table class=""table table-striped table-hover"">
                                <thead>
                                <tr>
                                    <th>Employee Name</th>
                                    <th>Employee Code</th>
                                    <th>Designation</th>
                                    <th>A/C No</th>
");
                EndContext();
#line 68 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Payroll\GenerateSalarySheet.cshtml"
                                      
                                        if (Model.EmployeeSalaries.SalaryComponentNames.Any())
                                        {
                                            foreach (var salaryComponentName in Model.EmployeeSalaries.SalaryComponentNames)
                                            {

#line default
#line hidden
                BeginContext(4058, 52, true);
                WriteLiteral("                                                <th>");
                EndContext();
                BeginContext(4111, 19, false);
#line 73 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Payroll\GenerateSalarySheet.cshtml"
                                               Write(salaryComponentName);

#line default
#line hidden
                EndContext();
                BeginContext(4130, 7, true);
                WriteLiteral("</th>\r\n");
                EndContext();
#line 74 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Payroll\GenerateSalarySheet.cshtml"
                                            }
                                        }
                                    

#line default
#line hidden
                BeginContext(4266, 305, true);
                WriteLiteral(@"                                    <th>Tax Deduction</th>
                                    <th>Other Deduction</th>
                                    <th>Total Amount</th>

                                </tr>
                                </thead>
                                <tbody>
");
                EndContext();
#line 84 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Payroll\GenerateSalarySheet.cshtml"
                                  
                                    if (Model.EmployeeSalaries.EmployeeSalaries.Any())
                                    {
                                        var index = -1;
                                        foreach (var salaryComponent in Model.EmployeeSalaries.EmployeeSalaries)
                                        {
                                            index++;

#line default
#line hidden
                BeginContext(5002, 132, true);
                WriteLiteral("                                            <tr style=\"align-content: center\">\r\n                                                <td>");
                EndContext();
                BeginContext(5135, 28, false);
#line 92 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Payroll\GenerateSalarySheet.cshtml"
                                               Write(salaryComponent.EmployeeName);

#line default
#line hidden
                EndContext();
                BeginContext(5163, 59, true);
                WriteLiteral("</td>\r\n                                                <td>");
                EndContext();
                BeginContext(5223, 28, false);
#line 93 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Payroll\GenerateSalarySheet.cshtml"
                                               Write(salaryComponent.EmployeeCode);

#line default
#line hidden
                EndContext();
                BeginContext(5251, 59, true);
                WriteLiteral("</td>\r\n                                                <td>");
                EndContext();
                BeginContext(5311, 27, false);
#line 94 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Payroll\GenerateSalarySheet.cshtml"
                                               Write(salaryComponent.Designation);

#line default
#line hidden
                EndContext();
                BeginContext(5338, 59, true);
                WriteLiteral("</td>\r\n                                                <td>");
                EndContext();
                BeginContext(5398, 29, false);
#line 95 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Payroll\GenerateSalarySheet.cshtml"
                                               Write(salaryComponent.BankAccountNo);

#line default
#line hidden
                EndContext();
                BeginContext(5427, 7, true);
                WriteLiteral("</td>\r\n");
                EndContext();
#line 96 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Payroll\GenerateSalarySheet.cshtml"
                                                  
                                                    foreach (var componentAmount in salaryComponent.SalaryComponentsAmount)
                                                    {

#line default
#line hidden
                BeginContext(5666, 60, true);
                WriteLiteral("                                                        <td>");
                EndContext();
                BeginContext(5727, 15, false);
#line 99 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Payroll\GenerateSalarySheet.cshtml"
                                                       Write(componentAmount);

#line default
#line hidden
                EndContext();
                BeginContext(5742, 7, true);
                WriteLiteral("</td>\r\n");
                EndContext();
#line 100 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Payroll\GenerateSalarySheet.cshtml"
                                                    }
                                                

#line default
#line hidden
                BeginContext(5855, 52, true);
                WriteLiteral("                                                <td>");
                EndContext();
                BeginContext(5908, 28, false);
#line 102 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Payroll\GenerateSalarySheet.cshtml"
                                               Write(salaryComponent.TaxDeduction);

#line default
#line hidden
                EndContext();
                BeginContext(5936, 59, true);
                WriteLiteral("</td>\r\n                                                <td>");
                EndContext();
                BeginContext(5995, 196, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "7a8efb8dd2010ae84a69f02cd15083187e2ac60b27845", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
#line 103 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Payroll\GenerateSalarySheet.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.OtherDeductions);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.Name = (string)__tagHelperAttribute_5.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
                BeginWriteTagHelperAttribute();
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __tagHelperExecutionContext.AddHtmlAttribute("required", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.Value = (string)__tagHelperAttribute_6.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
                BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "onchange", 3, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                AddHtmlAttributeValue("", 6105, "decreaseTotalAmount(", 6105, 20, true);
#line 103 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Payroll\GenerateSalarySheet.cshtml"
AddHtmlAttributeValue("", 6125, index, 6125, 6, false);

#line default
#line hidden
                AddHtmlAttributeValue("", 6131, ")", 6131, 1, true);
                EndAddHtmlAttributeValues(__tagHelperExecutionContext);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(6191, 61, true);
                WriteLiteral("</td>\r\n                                                <td>\r\n");
                EndContext();
                BeginContext(6396, 58, true);
                WriteLiteral("                                                    <input");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 6454, "\"", 6490, 1);
#line 106 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Payroll\GenerateSalarySheet.cshtml"
WriteAttributeValue("", 6462, salaryComponent.TotalAmount, 6462, 28, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(6491, 48, true);
                WriteLiteral(" style=\"text-align: right; width: 90px\" readonly");
                EndContext();
                BeginWriteAttribute("id", " id=\"", 6539, "\"", 6550, 1);
#line 106 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Payroll\GenerateSalarySheet.cshtml"
WriteAttributeValue("", 6544, index, 6544, 6, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(6551, 130, true);
                WriteLiteral(" name=\"TotalAmounts\"/>\r\n                                                </td>\r\n                                            </tr>\r\n");
                EndContext();
#line 109 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Payroll\GenerateSalarySheet.cshtml"
                                        }
                                    }
                                

#line default
#line hidden
                BeginContext(6798, 265, true);
                WriteLiteral(@"                                </tbody>
                            </table>
                        </div>
                        <div class=""btn-container"">
                            <button type=""button"" class=""btn custom-btn custom-cancel-btn pull-left""");
                EndContext();
                BeginWriteAttribute("onclick", " onclick=\"", 7063, "\"", 7183, 5);
                WriteAttributeValue("", 7073, "location.href", 7073, 13, true);
                WriteAttributeValue(" ", 7086, "=", 7087, 2, true);
                WriteAttributeValue(" ", 7088, "\'", 7089, 2, true);
#line 116 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\Payroll\GenerateSalarySheet.cshtml"
WriteAttributeValue("", 7090, Url.Action("ViewBranchMedium", "BranchMedium", new {branchMediumId = Model.BranchMediumId}), 7090, 92, false);

#line default
#line hidden
                WriteAttributeValue("", 7182, "\'", 7182, 1, true);
                EndWriteAttribute();
                BeginContext(7184, 269, true);
                WriteLiteral(@">Cancel</button>
                            <button type=""submit"" class=""btn custom-btn custom-cancel-btn pull-right"">Save</button>
                            <div class=""clearfix""></div>
                        </div>
                    </div>
                ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_9.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_10);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_11);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(7460, 94, true);
            WriteLiteral("\r\n                <!-- END FORM-->\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(7573, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(7579, 107, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7a8efb8dd2010ae84a69f02cd15083187e2ac60b36145", async() => {
                    BeginContext(7671, 6, true);
                    WriteLiteral("\r\n    ");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_12);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_13);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(7686, 2, true);
                WriteLiteral("\r\n");
                EndContext();
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<GenerateSalarySheetViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591