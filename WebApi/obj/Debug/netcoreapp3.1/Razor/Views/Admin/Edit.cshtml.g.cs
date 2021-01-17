#pragma checksum "C:\Users\Daniel Okpala\Documents\repos\School Kit\webapi\Views\Admin\Edit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "78f7875eab004a405519f6dc8648631c670da567"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_Edit), @"mvc.1.0.view", @"/Views/Admin/Edit.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"78f7875eab004a405519f6dc8648631c670da567", @"/Views/Admin/Edit.cshtml")]
    public class Views_Admin_Edit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<WebApi.Models.Admin>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Daniel Okpala\Documents\repos\School Kit\webapi\Views\Admin\Edit.cshtml"
  
    ViewData["Title"] = "Edit";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h1>Edit</h1>

<h4>Admin</h4>
<hr />
<div class=""row"">
    <div class=""col-md-4"">
        <form asp-action=""Edit"">
            <div asp-validation-summary=""ModelOnly"" class=""text-danger""></div>
            <div class=""form-group"">
                <label asp-for=""FirstName"" class=""control-label""></label>
                <input asp-for=""FirstName"" class=""form-control"" />
                <span asp-validation-for=""FirstName"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""LastName"" class=""control-label""></label>
                <input asp-for=""LastName"" class=""form-control"" />
                <span asp-validation-for=""LastName"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""MiddleName"" class=""control-label""></label>
                <input asp-for=""MiddleName"" class=""form-control"" />
                <span asp-validation-for=""MiddleName"" class=""text-danger"">");
            WriteLiteral(@"</span>
            </div>
            <div class=""form-group"">
                <label asp-for=""Address"" class=""control-label""></label>
                <input asp-for=""Address"" class=""form-control"" />
                <span asp-validation-for=""Address"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""Gender"" class=""control-label""></label>
                <select asp-for=""Gender"" class=""form-control""></select>
                <span asp-validation-for=""Gender"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""LgaID"" class=""control-label""></label>
                <select asp-for=""LgaID"" class=""form-control"" asp-items=""ViewBag.LgaID""></select>
                <span asp-validation-for=""LgaID"" class=""text-danger""></span>
            </div>
            <input type=""hidden"" asp-for=""Id"" />
            <div class=""form-group"">
                <label asp-for=""UserName"" c");
            WriteLiteral(@"lass=""control-label""></label>
                <input asp-for=""UserName"" class=""form-control"" />
                <span asp-validation-for=""UserName"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""NormalizedUserName"" class=""control-label""></label>
                <input asp-for=""NormalizedUserName"" class=""form-control"" />
                <span asp-validation-for=""NormalizedUserName"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""Email"" class=""control-label""></label>
                <input asp-for=""Email"" class=""form-control"" />
                <span asp-validation-for=""Email"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""NormalizedEmail"" class=""control-label""></label>
                <input asp-for=""NormalizedEmail"" class=""form-control"" />
                <span asp-validation-for=""Normalize");
            WriteLiteral("dEmail\" class=\"text-danger\"></span>\r\n            </div>\r\n            <div class=\"form-group form-check\">\r\n                <label class=\"form-check-label\">\r\n                    <input class=\"form-check-input\" asp-for=\"EmailConfirmed\" /> ");
#nullable restore
#line 68 "C:\Users\Daniel Okpala\Documents\repos\School Kit\webapi\Views\Admin\Edit.cshtml"
                                                                           Write(Html.DisplayNameFor(model => model.EmailConfirmed));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                </label>
            </div>
            <div class=""form-group"">
                <label asp-for=""PasswordHash"" class=""control-label""></label>
                <input asp-for=""PasswordHash"" class=""form-control"" />
                <span asp-validation-for=""PasswordHash"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""SecurityStamp"" class=""control-label""></label>
                <input asp-for=""SecurityStamp"" class=""form-control"" />
                <span asp-validation-for=""SecurityStamp"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""ConcurrencyStamp"" class=""control-label""></label>
                <input asp-for=""ConcurrencyStamp"" class=""form-control"" />
                <span asp-validation-for=""ConcurrencyStamp"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""PhoneNumber"" cl");
            WriteLiteral(@"ass=""control-label""></label>
                <input asp-for=""PhoneNumber"" class=""form-control"" />
                <span asp-validation-for=""PhoneNumber"" class=""text-danger""></span>
            </div>
            <div class=""form-group form-check"">
                <label class=""form-check-label"">
                    <input class=""form-check-input"" asp-for=""PhoneNumberConfirmed"" /> ");
#nullable restore
#line 93 "C:\Users\Daniel Okpala\Documents\repos\School Kit\webapi\Views\Admin\Edit.cshtml"
                                                                                 Write(Html.DisplayNameFor(model => model.PhoneNumberConfirmed));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </label>\r\n            </div>\r\n            <div class=\"form-group form-check\">\r\n                <label class=\"form-check-label\">\r\n                    <input class=\"form-check-input\" asp-for=\"TwoFactorEnabled\" /> ");
#nullable restore
#line 98 "C:\Users\Daniel Okpala\Documents\repos\School Kit\webapi\Views\Admin\Edit.cshtml"
                                                                             Write(Html.DisplayNameFor(model => model.TwoFactorEnabled));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                </label>
            </div>
            <div class=""form-group"">
                <label asp-for=""LockoutEnd"" class=""control-label""></label>
                <input asp-for=""LockoutEnd"" class=""form-control"" />
                <span asp-validation-for=""LockoutEnd"" class=""text-danger""></span>
            </div>
            <div class=""form-group form-check"">
                <label class=""form-check-label"">
                    <input class=""form-check-input"" asp-for=""LockoutEnabled"" /> ");
#nullable restore
#line 108 "C:\Users\Daniel Okpala\Documents\repos\School Kit\webapi\Views\Admin\Edit.cshtml"
                                                                           Write(Html.DisplayNameFor(model => model.LockoutEnabled));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                </label>
            </div>
            <div class=""form-group"">
                <label asp-for=""AccessFailedCount"" class=""control-label""></label>
                <input asp-for=""AccessFailedCount"" class=""form-control"" />
                <span asp-validation-for=""AccessFailedCount"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <input type=""submit"" value=""Save"" class=""btn btn-primary"" />
            </div>
        </form>
    </div>
</div>

<div>
    <a asp-action=""Index"">Back to List</a>
</div>

");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 128 "C:\Users\Daniel Okpala\Documents\repos\School Kit\webapi\Views\Admin\Edit.cshtml"
      await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
#nullable disable
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WebApi.Models.Admin> Html { get; private set; }
    }
}
#pragma warning restore 1591
