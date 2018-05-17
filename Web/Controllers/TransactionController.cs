using System.Linq;
using System.Web.Mvc;
using Domain.Interfaces;
using Domain.Model;
using Mvc.JQuery.Datatables;
using Web.Models.Claim;
using Web.Filters;
using System.Web.UI;
using Web.Models.Account;
using Web.Models.HL7;
using static Domain.Model.UserRole;
using System.Collections.Generic;

namespace Web.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [OutputCache(Location = OutputCacheLocation.None,NoStore = true)]
    public partial class TransactionController : Controller
    { 
        private readonly ILabOrderRepository _getPayerResponse;
        private readonly IPayerMasterRepository _getpayerMasterData;
        private UsersContext _usersContext;
        public TransactionController( UsersContext usersContext,   ILabOrderRepository getPayerResponse, IPayerMasterRepository getpayerMasterData)
        { 
            _getPayerResponse = getPayerResponse;
            _usersContext = usersContext;
            _getpayerMasterData = getpayerMasterData;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            var u = requestContext.HttpContext.User;
            var data = _usersContext.UserProfiles.FirstOrDefault(x => x.UserName == u.Identity.Name);
            ViewBag.UserData = data;
        }

        
        [HttpGet]
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None,NoStore = true)]
        public virtual ActionResult List(string postedDate,string CheckStatus)
        {
           
            return View();
        }

        public virtual ActionResult Welcome()
        {
            var welcomeViewModel = new WelcomeViewModel(); ;
            
            return View(welcomeViewModel); 

        }
       
        // [HttpPost]
        public virtual ActionResult HL7EntryDetails()
        {
            var HL7EntryDetails = new HL7EntryFormViewModel(); ;

            return View(HL7EntryDetails);

        }
        [HttpPost]
        public virtual JsonResult GetPayerList()
        {
            List<PayerList> payerList = new List<PayerList>();
            payerList = _getpayerMasterData.GetPayerList().OrderBy(p => p.PayerName).ToList(); ;
            var data = payerList != null ? payerList : null;
            return Json(data);
        }
        [HttpPost]
        public virtual JsonResult GetLabDataList()
        {

            var viewUserDatadata = ViewBag.UserData;
            var role = viewUserDatadata.Role != "Maker" ? (viewUserDatadata.Role == "Checker" ? Checker : SuperAdmin) : Maker;

            var totalRecords = _usersContext.UserProfiles.Count();
            var payerList = _getPayerResponse.GetLabDataList(role).OrderBy(p => p.AssignedTo).ToList(); ;
            var data = payerList;

            return Json(new
            {
               // sEcho = dataTablesParam.sEcho,
              //  iTotalDisplayRecords = data.Count,
               // iTotalRecords = totalRecords,
                aaData = data
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual JsonResult PostHl7OrderForm(HL7EntryFormViewModel orderFormData)
        {
            var data = ViewBag.UserData; 

            var orderFormPost = new LabQueryModel
            {
                Status = orderFormData.Status,
                Hl7Stream = orderFormData.HL7Stream,
                OrderId = orderFormData.OrderId,
                UserRole = data.Role != "Maker" ? (data.Role == "Checker" ? Checker : SuperAdmin) : Maker,
                EntryOption = orderFormData.EntryOption

            };
          return Json(
           _getPayerResponse.PostHl7OrderForm(orderFormPost) ? new {Success = true} : new {Success = false});
        }

        public virtual JsonResult CheckandGetOrderJson(Hl7JsonloadViewModel hl7Jsonload)
        {

            var data = ViewBag.UserData;
            var orderFormPost = new LabQueryModel
            {
                OrderId = hl7Jsonload.OrderId,
                UserRole = data.Role != "Maker" ? (data.Role == "Checker" ? Checker : SuperAdmin) : Maker,
                Email = data.Email

            };
            var getJson = _getPayerResponse.CheckUserDataExist(orderFormPost);

            return Json(new {Success = true, OrderFormData = getJson});
        }

        public virtual JsonResult GetPatientDetails(string Username, int Searchtype)
        {
            //_getPayerResponse.PostHl7OrderForm(orderFormPost) ? new { Success = true } : new { Success = false });

            var PatientList = _getPayerResponse.GetPatientDetails(Username, Username == "" ? 3 : Searchtype);

            return Json(new
            {                
                aaData = PatientList
            }, JsonRequestBehavior.AllowGet);
             
        }
        public virtual JsonResult GetPatientInsurance(string PatientId)
        {
            //_getPayerResponse.PostHl7OrderForm(orderFormPost) ? new { Success = true } : new { Success = false });

            var PatientList = _getPayerResponse.GetPatientInsurance(PatientId);

            return Json(new
            {
                aaData = PatientList
            }, JsonRequestBehavior.AllowGet);

        }
        public virtual JsonResult GetSSN(string PatientId)
        {
            var PatientSSN = _getPayerResponse.GetSSN(PatientId);
            return Json(new
            {
                aaData = PatientSSN
            }, JsonRequestBehavior.AllowGet);

        } 

        public virtual ActionResult RejectionDetails(string postedDate)
        {
            if (postedDate != null)
            {
                ViewBag.PostedDate = postedDate;
            }
            return View();
        }
        public enum UserRole    

        {
            Checker=1,
            Maker=2
        }
         
    }

    
}