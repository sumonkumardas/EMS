#pragma checksum "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\TermTest\ShowExamRoutine.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6dc4e6634a497369990951ea3dc657a751cb0162"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_TermTest_ShowExamRoutine), @"mvc.1.0.view", @"/Views/TermTest/ShowExamRoutine.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/TermTest/ShowExamRoutine.cshtml", typeof(AspNetCore.Views_TermTest_ShowExamRoutine))]
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
#line 2 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\TermTest\ShowExamRoutine.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6dc4e6634a497369990951ea3dc657a751cb0162", @"/Views/TermTest/ShowExamRoutine.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9011cacf54d8ae45691a3deaab04c36239ace056", @"/Views/_ViewImports.cshtml")]
    public class Views_TermTest_ShowExamRoutine : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<SectionViewModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(111, 102, true);
            WriteLiteral("<div>\r\n  <div class=\"form-group\">\r\n    <div class=\"col-md-12\">\r\n      <div class=\"col-md-4\">\r\n        ");
            EndContext();
            BeginContext(214, 191, false);
#line 8 "C:\EMS\EducationManagementSystem\src\SinePulseEMS\Site\SinePulse.EMS.Site\Views\TermTest\ShowExamRoutine.cshtml"
   Write(Html.DropDownListFor(x => x.Count, new SelectList(@Model, "Id", "SectionName"), @Localizer["ShowExamRoutine.SelectSection"].Value, new { @class = "form-control", id = "sectionNameDropDown" }));

#line default
#line hidden
            EndContext();
            BeginContext(405, 910, true);
            WriteLiteral(@"
      </div>
      <div id=""examRoutineCalender""></div>
    </div>

  </div>
</div>

<script>
  $(document).ready(function () {
    var date = new Date();
    var branchMediumId = parseInt($('#showBranchMediumViewId').val());
    var eventList = [];
    $.ajax({
      type: ""GET"",
      url: ""/PublicHoliday/GetAcademicYearEvents?branchMediumId="" + branchMediumId,
      dataType: ""JSON"",
      contentType: ""application/json;charset=utf-8"",
      success: function (data) {
        if (data.length > 0) {
          eventList = data;
        }

        $('#mediumCalendar').fullCalendar({
          defaultDate: date,
          editable: false,
          eventLimit: false, // allow ""more"" link when too many events
          events: eventList,
          defaultView: 'agendaWeek'
        });
      },
      error: function (a, b, c) {

      }
    });


  });
</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<SectionViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591