#pragma checksum "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ContactInfo\ViewContactInfo.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9ede8004e7e53835162e860d424a0c928eebe02c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ContactInfo_ViewContactInfo), @"mvc.1.0.view", @"/Views/ContactInfo/ViewContactInfo.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/ContactInfo/ViewContactInfo.cshtml", typeof(AspNetCore.Views_ContactInfo_ViewContactInfo))]
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
#line 2 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ContactInfo\ViewContactInfo.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9ede8004e7e53835162e860d424a0c928eebe02c", @"/Views/ContactInfo/ViewContactInfo.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9011cacf54d8ae45691a3deaab04c36239ace056", @"/Views/_ViewImports.cshtml")]
    public class Views_ContactInfo_ViewContactInfo : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<SinePulse.EMS.Site.Models.ContactInfoViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(135, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 5 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ContactInfo\ViewContactInfo.cshtml"
  
  ViewData["Title"] = "ViewContactInfo";
  Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(231, 518, true);
            WriteLiteral(@"



<div class=""page-container"">
  <!-- BEGIN CONTENT -->
  <div class=""page-content-wrapper"">
    <div class=""page-content"">
      <div class=""menu-icon-on-view"">
        <div class=""btn-group"">
          <button type=""button"" class=""btn btn-fit-height dark-bg dropdown-toggle"" data-toggle=""dropdown"" data-hover=""dropdown"" data-delay=""100"" data-close-others=""true"">
            <i class=""fa fa-bars""></i>
          </button>
          <ul class=""dropdown-menu dark-arrow"" role=""menu"">
            <li><a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 749, "\"", 834, 2);
            WriteAttributeValue("", 756, "/ContactInfo/AddPresentAddressInContactInfo?contactInfoId=", 756, 58, true);
#line 23 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ContactInfo\ViewContactInfo.cshtml"
WriteAttributeValue("", 814, Model.ContactInfoId, 814, 20, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(835, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(837, 46, false);
#line 23 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ContactInfo\ViewContactInfo.cshtml"
                                                                                                    Write(Localizer["ViewContactInfo.AddPresentAddress"]);

#line default
#line hidden
            EndContext();
            BeginContext(883, 29, true);
            WriteLiteral("</a></li>\r\n            <li><a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 912, "\"", 999, 2);
            WriteAttributeValue("", 919, "/ContactInfo/AddPermanentAddressInContactInfo?contactInfoId=", 919, 60, true);
#line 24 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ContactInfo\ViewContactInfo.cshtml"
WriteAttributeValue("", 979, Model.ContactInfoId, 979, 20, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1000, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(1002, 48, false);
#line 24 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ContactInfo\ViewContactInfo.cshtml"
                                                                                                      Write(Localizer["ViewContactInfo.AddPermanentAddress"]);

#line default
#line hidden
            EndContext();
            BeginContext(1050, 158, true);
            WriteLiteral("</a></li>\r\n          </ul>\r\n        </div>\r\n      </div>\r\n\r\n      <div class=\"portlet box form-conatiner\">\r\n        <div class=\"icon-container\">\r\n          <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1208, "\"", 1293, 1);
#line 31 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ContactInfo\ViewContactInfo.cshtml"
WriteAttributeValue("", 1215, Url.Action("UpdateContactInfo","ContactInfo", new {id = Model.ContactInfoId}), 1215, 78, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1294, 395, true);
            WriteLiteral(@" class=""fa fa-edit action-link""></a>

          <a href=""/ContactInfo/Index"" class=""fa fa-tasks action-link""></a>
        </div>

        <div class=""portlet-body form"">

          <div class=""container-fluid"">
            <div class=""row-fluid "">
              <div class=""col-md-6"">
                <div class=""row custom-row"">
                  <div class=""col-md-4 title""><strong>");
            EndContext();
            BeginContext(1690, 36, false);
#line 42 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ContactInfo\ViewContactInfo.cshtml"
                                                 Write(Localizer["ViewContactInfo.PhoneNo"]);

#line default
#line hidden
            EndContext();
            BeginContext(1726, 63, true);
            WriteLiteral("</strong></div>\r\n                  <div class=\"col-md-8 value\">");
            EndContext();
            BeginContext(1790, 39, false);
#line 43 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ContactInfo\ViewContactInfo.cshtml"
                                         Write(Html.DisplayFor(model => model.PhoneNo));

#line default
#line hidden
            EndContext();
            BeginContext(1829, 192, true);
            WriteLiteral("</div>\r\n                </div>\r\n              </div>\r\n              <div class=\"col-md-6\">\r\n                <div class=\"row custom-row\">\r\n                  <div class=\"col-md-4 title\"><strong>");
            EndContext();
            BeginContext(2022, 41, false);
#line 48 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ContactInfo\ViewContactInfo.cshtml"
                                                 Write(Localizer["ViewContactInfo.EmailAddress"]);

#line default
#line hidden
            EndContext();
            BeginContext(2063, 63, true);
            WriteLiteral("</strong></div>\r\n                  <div class=\"col-md-8 value\">");
            EndContext();
            BeginContext(2127, 44, false);
#line 49 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ContactInfo\ViewContactInfo.cshtml"
                                         Write(Html.DisplayFor(model => model.EmailAddress));

#line default
#line hidden
            EndContext();
            BeginContext(2171, 309, true);
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
            BeginContext(2481, 35, false);
#line 58 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ContactInfo\ViewContactInfo.cshtml"
                                                 Write(Localizer["ViewContactInfo.Status"]);

#line default
#line hidden
            EndContext();
            BeginContext(2516, 63, true);
            WriteLiteral("</strong></div>\r\n                  <div class=\"col-md-8 value\">");
            EndContext();
            BeginContext(2580, 38, false);
#line 59 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\ContactInfo\ViewContactInfo.cshtml"
                                         Write(Html.DisplayFor(model => model.Status));

#line default
#line hidden
            EndContext();
            BeginContext(2618, 152, true);
            WriteLiteral("</div>\r\n                </div>\r\n              </div>\r\n            </div>\r\n          </div>\r\n        </div>\r\n      </div>\r\n    </div>\r\n  </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SinePulse.EMS.Site.Models.ContactInfoViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
