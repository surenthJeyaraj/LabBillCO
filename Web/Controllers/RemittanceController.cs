using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain.Model;
using iTextSharp.text.html;
using Mvc.JQuery.Datatables;
using Web.Models.Account;
using Data.Repository;
using Web.Models.Remittance;
using Web.Service;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using Web.Models.Claim;
using Domain.Interfaces;
using Web.Filters;
using Web.Models.HL7;
using static Domain.Model.UserRole; 

namespace Web.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
    public partial class RemittanceController : Controller
    {
        private UsersContext _userContext;
        private RemittanceRepository _remittanceRepository;
        private IRemittanceReportGenerator _reportGenerator;

        public RemittanceController(UsersContext userContext, RemittanceRepository remittanceRepository, IRemittanceReportGenerator reportGenerator)
        {
            _userContext = userContext;
            _remittanceRepository = remittanceRepository;
            _reportGenerator = reportGenerator;
        }


        /// <summary>
        /// </summary>
        /// <param name="requestContext"></param>
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            var u = requestContext.HttpContext.User;
            var data = _userContext.UserProfiles.FirstOrDefault(x => x.UserName == u.Identity.Name);

            ViewBag.UserData = data;
        }
        //
        // GET: /Remittance/

        public virtual ActionResult Index()
        {
            //var welcomeViewModel = new WelcomeViewModel(); ;

            // return View(welcomeViewModel);
            return View();
        }



        public virtual ActionResult Claims()
        {
            var welcomeViewModel = new WelcomeViewModel(); ;

            return View(welcomeViewModel);
            // return View();
        }

        public virtual JsonResult GetClaims(string remittanceId)
        {

            var remit = _remittanceRepository.GetRemittancePayments(remittanceId);
            return Json(new
            {
               aaData = remit
            }, JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult GetService(string remittanceId, string ClaimId)
        {

            var remit = _remittanceRepository.GetServiceDetails(ClaimId);
            return Json(new
            {

                aaData = remit
            }, JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult Services()
        {
            var welcomeViewModel = new WelcomeViewModel(); ;

            return View(welcomeViewModel);
            // return View();
        }
        public virtual JsonResult GetRemittances( RemittanceSearch filters)
        {
            var remittances = _remittanceRepository.GetRemittances(filters);
            // var result = remittances.Skip(datatableParameters.iDisplayStart).Take(datatableParameters.iDisplayLength).ToList();
            return Json(new 
            {
                aaData = remittances
            }, JsonRequestBehavior.AllowGet);
        }

        public void DownloadPdfReport(int remittanceId)
        {

        }


        public virtual JsonResult ViewHtmlReport(int remittanceId)
        {
            return Json(new { report = "" }, JsonRequestBehavior.AllowGet);
        }
       


    }
}
