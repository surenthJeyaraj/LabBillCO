// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace Web.Controllers
{
    public partial class RemittanceController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RemittanceController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult GetClaims()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetClaims);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult GetService()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetService);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult GetRemittances()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetRemittances);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult ViewHtmlReport()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.ViewHtmlReport);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public RemittanceController Actions { get { return MVC.Remittance; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Remittance";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Remittance";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string Claims = "Claims";
            public readonly string GetClaims = "GetClaims";
            public readonly string GetService = "GetService";
            public readonly string Services = "Services";
            public readonly string GetRemittances = "GetRemittances";
            public readonly string ViewHtmlReport = "ViewHtmlReport";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string Claims = "Claims";
            public const string GetClaims = "GetClaims";
            public const string GetService = "GetService";
            public const string Services = "Services";
            public const string GetRemittances = "GetRemittances";
            public const string ViewHtmlReport = "ViewHtmlReport";
        }


        static readonly ActionParamsClass_GetClaims s_params_GetClaims = new ActionParamsClass_GetClaims();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetClaims GetClaimsParams { get { return s_params_GetClaims; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetClaims
        {
            public readonly string remittanceId = "remittanceId";
        }
        static readonly ActionParamsClass_GetService s_params_GetService = new ActionParamsClass_GetService();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetService GetServiceParams { get { return s_params_GetService; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetService
        {
            public readonly string remittanceId = "remittanceId";
            public readonly string ClaimId = "ClaimId";
        }
        static readonly ActionParamsClass_GetRemittances s_params_GetRemittances = new ActionParamsClass_GetRemittances();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetRemittances GetRemittancesParams { get { return s_params_GetRemittances; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetRemittances
        {
            public readonly string filters = "filters";
        }
        static readonly ActionParamsClass_ViewHtmlReport s_params_ViewHtmlReport = new ActionParamsClass_ViewHtmlReport();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ViewHtmlReport ViewHtmlReportParams { get { return s_params_ViewHtmlReport; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ViewHtmlReport
        {
            public readonly string remittanceId = "remittanceId";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string Claims = "Claims";
                public readonly string Index = "Index";
                public readonly string Services = "Services";
            }
            public readonly string Claims = "~/Views/Remittance/Claims.cshtml";
            public readonly string Index = "~/Views/Remittance/Index.cshtml";
            public readonly string Services = "~/Views/Remittance/Services.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_RemittanceController : Web.Controllers.RemittanceController
    {
        public T4MVC_RemittanceController() : base(Dummy.Instance) { }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ClaimsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Claims()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Claims);
            ClaimsOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void GetClaimsOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, string remittanceId);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetClaims(string remittanceId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetClaims);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "remittanceId", remittanceId);
            GetClaimsOverride(callInfo, remittanceId);
            return callInfo;
        }

        [NonAction]
        partial void GetServiceOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, string remittanceId, string ClaimId);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetService(string remittanceId, string ClaimId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetService);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "remittanceId", remittanceId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "ClaimId", ClaimId);
            GetServiceOverride(callInfo, remittanceId, ClaimId);
            return callInfo;
        }

        [NonAction]
        partial void ServicesOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Services()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Services);
            ServicesOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void GetRemittancesOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, Domain.Model.RemittanceSearch filters);

        [NonAction]
        public override System.Web.Mvc.JsonResult GetRemittances(Domain.Model.RemittanceSearch filters)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.GetRemittances);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "filters", filters);
            GetRemittancesOverride(callInfo, filters);
            return callInfo;
        }

        [NonAction]
        partial void ViewHtmlReportOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, int remittanceId);

        [NonAction]
        public override System.Web.Mvc.JsonResult ViewHtmlReport(int remittanceId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.ViewHtmlReport);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "remittanceId", remittanceId);
            ViewHtmlReportOverride(callInfo, remittanceId);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
