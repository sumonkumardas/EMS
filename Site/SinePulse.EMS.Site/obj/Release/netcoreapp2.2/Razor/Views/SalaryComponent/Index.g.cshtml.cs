#pragma checksum "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\SalaryComponent\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "eed99b6775a97ecef834747d4d9f0536bc407d8b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_SalaryComponent_Index), @"mvc.1.0.view", @"/Views/SalaryComponent/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/SalaryComponent/Index.cshtml", typeof(AspNetCore.Views_SalaryComponent_Index))]
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
#line 1 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\SalaryComponent\Index.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"eed99b6775a97ecef834747d4d9f0536bc407d8b", @"/Views/SalaryComponent/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9011cacf54d8ae45691a3deaab04c36239ace056", @"/Views/_ViewImports.cshtml")]
    public class Views_SalaryComponent_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ShowSalaryComponentListViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal box-shadow-form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 4 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\SalaryComponent\Index.cshtml"
  
  ViewData["Title"] = "Index";
  Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(205, 969, true);
            WriteLiteral(@"<div class=""page-container"">
  <!-- BEGIN CONTENT -->
  <div class=""page-content-wrapper"">
    <div class=""page-content"">
      <div class=""portlet-body form"">
        <div class=""title-on-top"">
          <div class=""page-toolbar pull-left"" style=""margin-top: -10px;"">
            <div class=""btn-group"">
              <ul style=""padding:0px;"">
                <li class=""custom-page-menu dropdown primary-menu-item-li"">
                  <a href=""#"" class=""show-dropdown-on-hover"" data-toggle=""custom-page-menu"">
                    <button type=""button"" class=""btn btn-fit-height dark-bg dropdown-toggle"" data-toggle=""dropdown"" data-hover=""dropdown"" data-delay=""100"" data-close-others=""true"">
                      <i class=""fa fa-bars""></i>
                    </button>
                  </a>
                  <ul class=""dropdown-menu light-arrow-only"">
                    <li>
                      <a href=""/SalaryComponent/AddSalaryComponent"">");
            EndContext();
            BeginContext(1175, 37, false);
#line 25 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\SalaryComponent\Index.cshtml"
                                                               Write(Localizer["Index.AddSalaryComponent"]);

#line default
#line hidden
            EndContext();
            BeginContext(1212, 150, true);
            WriteLiteral("</a>\r\n                    </li>\r\n                  </ul>\r\n                </li>\r\n              </ul>\r\n            </div>\r\n          </div>\r\n          ");
            EndContext();
            BeginContext(1363, 24, false);
#line 32 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\SalaryComponent\Index.cshtml"
     Write(Localizer["Index.Title"]);

#line default
#line hidden
            EndContext();
            BeginContext(1387, 26, true);
            WriteLiteral("\r\n        </div>\r\n        ");
            EndContext();
            BeginContext(1413, 1542, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "eed99b6775a97ecef834747d4d9f0536bc407d8b6303", async() => {
                BeginContext(1459, 226, true);
                WriteLiteral("\r\n          <div class=\"form-body\">\r\n            <table class=\"table table-striped table-hover\" id=\"sample_5\">\r\n\r\n              <thead class=\"filterCriteria\">\r\n                <tr>\r\n                  <th>\r\n                    ");
                EndContext();
                BeginContext(1686, 32, false);
#line 41 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\SalaryComponent\Index.cshtml"
               Write(Localizer["Index.ComponentName"]);

#line default
#line hidden
                EndContext();
                BeginContext(1718, 71, true);
                WriteLiteral("\r\n                  </th>\r\n                  <th>\r\n                    ");
                EndContext();
                BeginContext(1790, 32, false);
#line 44 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\SalaryComponent\Index.cshtml"
               Write(Localizer["Index.ComponentType"]);

#line default
#line hidden
                EndContext();
                BeginContext(1822, 50, true);
                WriteLiteral("\r\n                  </th>\r\n                  <th> ");
                EndContext();
                BeginContext(1873, 25, false);
#line 46 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\SalaryComponent\Index.cshtml"
                  Write(Localizer["Index.Action"]);

#line default
#line hidden
                EndContext();
                BeginContext(1898, 82, true);
                WriteLiteral(" </th>\r\n                </tr>\r\n              </thead>\r\n\r\n              <tbody>\r\n\r\n");
                EndContext();
#line 52 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\SalaryComponent\Index.cshtml"
                  
                  foreach (var componentType in Model.salaryComponents)
                  {

#line default
#line hidden
                BeginContext(2094, 78, true);
                WriteLiteral("                    <tr>\r\n                      <td>\r\n                        ");
                EndContext();
                BeginContext(2173, 49, false);
#line 57 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\SalaryComponent\Index.cshtml"
                   Write(Html.DisplayFor(s => componentType.ComponentName));

#line default
#line hidden
                EndContext();
                BeginContext(2222, 83, true);
                WriteLiteral("\r\n                      </td>\r\n                      <td>\r\n                        ");
                EndContext();
                BeginContext(2306, 67, false);
#line 60 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\SalaryComponent\Index.cshtml"
                   Write(Html.DisplayFor(s => componentType.componentTypeData.ComponentType));

#line default
#line hidden
                EndContext();
                BeginContext(2373, 85, true);
                WriteLiteral("\r\n                      </td>\r\n                      <td>\r\n                        <a");
                EndContext();
                BeginWriteAttribute("href", " href=\"", 2458, "\"", 2552, 2);
                WriteAttributeValue("", 2465, "/SalaryComponent/EditSalaryComponent?salaryComponentId=", 2465, 55, true);
#line 63 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\SalaryComponent\Index.cshtml"
WriteAttributeValue("", 2520, componentType.SalaryComponentId, 2520, 32, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(2553, 283, true);
                WriteLiteral(@" class=""action-link""><i class=""fa fa-edit action-icon""></i></a>
                        <a href=""#"" class=""action-link"">
                          <i class=""fa fa-minus-circle action-icon""></i>
                        </a>
                      </td>
                    </tr>
");
                EndContext();
#line 69 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\SalaryComponent\Index.cshtml"
                  }
                

#line default
#line hidden
                BeginContext(2876, 72, true);
                WriteLiteral("              </tbody>\r\n            </table>\r\n          </div>\r\n        ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2955, 44, true);
            WriteLiteral("\r\n      </div>\r\n    </div>\r\n  </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ShowSalaryComponentListViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591