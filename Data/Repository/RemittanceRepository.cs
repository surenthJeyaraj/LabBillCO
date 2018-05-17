using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Model;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System.ServiceModel.Description;
using Microsoft.Xrm.Client;
using Microsoft.Xrm.Sdk;
using Infrastructure.Extensions;

namespace Data.Repository
{
    public class RemittanceRepository
    {
       // private string _connectionString;
        public IOrganizationService Service { get; private set; }
        public RemittanceRepository(string connectionstring)
        {
            var connArr = connectionstring.Split(';');
            if ((connArr.Length < 0)) return;
            var url = connArr[0];
            var userId = connArr[1];
            var password = connArr[2];
            Service = GetService(url, userId, password);
        }
        public IOrganizationService GetService(string url, string userId, string password)
        {
            var serviceUri = new Uri(url);
            var credentials = new ClientCredentials();
            try
            {
                var proxy = new OrganizationServiceProxy(serviceUri, null, credentials, null);
           
                credentials.UserName.UserName = userId;
                credentials.UserName.Password = password;
                proxy.EnableProxyTypes();
                Service = (IOrganizationService)proxy;
                return Service;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error while accessing CRM instance");
            }
        }
        public IList<RemittanceClaimPayment> GetRemittancePayments(string remittanceId)
        {
            var remittances = new List<RemittanceClaimPayment>();

            QueryExpression query = new QueryExpression()
            {
                EntityName = "lab_remittance",
                ColumnSet = new ColumnSet(true),

                LinkEntities =
                    {
                        new LinkEntity
                        {
                            EntityAlias ="ERA",
                            LinkFromEntityName = "Remittance",
                            LinkFromAttributeName = "lab_erafileid",
                            LinkToEntityName = "lab_era",
                            LinkToAttributeName = "lab_eraid",
                            Columns = new ColumnSet(true)
                        }
                    }
            };
            query.Criteria = new FilterExpression
            {
                FilterOperator = LogicalOperator.And,
                Conditions = { new ConditionExpression("lab_erafileid", ConditionOperator.Equal, Guid.Parse(remittanceId)) }
            };
            try
            {

                EntityCollection remitanceDetilas = Service.RetrieveMultiple(query);

                remittances = remitanceDetilas.Entities.Select(remit => new RemittanceClaimPayment
                {
                    RemittanceId = remit.GetAttributeValue<Guid>("avtrx_remittanceid"),
                    ClaimNumber = remit.GetAttributeValue("avtrx_identifier").ToString(),
                    ClaimAmount = remit.GetFormattedAttributeValue("avtrx_chargeamount"),
                    PaidAmount = remit.GetFormattedAttributeValue("avtrx_paymentamount"),
                    ClaimStatusCodeDescription = remit.GetAttributeValue("avtrx_claimstatuscode").ToString(),
                    DateofService = (DateTime)remit.Attributes["createdon"],
                    PatientLastName = remit.GetAttributeValue<string>("avtrx_patientlastname"),
                    PatientFirstName = remit.GetAttributeValue<string>("avtrx_patientfirstname")

                }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return remittances;

        }
        public IList<ClaimServiceLinePayment> GetServiceDetails(string ClaimId)
        {
            var remittances = new List<ClaimServiceLinePayment>();

            QueryExpression query = new QueryExpression()
            {
                EntityName = "avtrx_servicelines",
                ColumnSet = new ColumnSet(true),

            };


            var LinkEntitiesService =

                        new LinkEntity
                        {
                            // EntityAlias = "ERA",
                            //LinkFromEntityName = "avtrx_servicelines",
                            LinkFromAttributeName = "avtrx_name",
                            LinkToEntityName = "avtrx_remittanceservice",
                            LinkToAttributeName = "avtrx_remittanceserviceid",
                            Columns = new ColumnSet(true)

                        };

            var LinkEntitiesRemittance =
                        new LinkEntity
                        {
                            // EntityAlias ="ERA",
                            // LinkFromEntityName = "avtrx_remittanceservice",
                            LinkFromAttributeName = "avtrx_identifier",
                            LinkToEntityName = "avtrx_remittance",
                            LinkToAttributeName = "avtrx_remittanceid",
                            Columns = new ColumnSet(true),


                        };

            query.LinkEntities.Add(LinkEntitiesService);
            LinkEntitiesRemittance.LinkCriteria.AddCondition(new ConditionExpression("avtrx_identifier", ConditionOperator.Equal, ClaimId));
            LinkEntitiesService.LinkEntities.Add(LinkEntitiesRemittance);


            try
            {
                EntityCollection remitanceDetilas = Service.RetrieveMultiple(query);

                remittances = remitanceDetilas.Entities.Select(remitService => new ClaimServiceLinePayment
                {
                    Id = 12344,
                    ProcedureCode = remitService.GetFormattedAttributeValue("avtrx_name").ToString(),
                    SubmittedAmount = remitService.GetAliasedAttributeValue<Money>("avtrx_remittanceservice1.avtrx_chargeamount"),
                    ProcedureDate = (DateTime)remitService.Attributes["createdon"],
                    ReasonCodes = remitService.GetAttributeValue("avtrx_reasoncode").ToString(),
                    AllowedAmount = remitService.GetAliasedAttributeValue<Money>("avtrx_remittance2.avtrx_paymentamount"),
                    UnitsPaid = remitService.GetAttributeValue<decimal>("avtrx_adjustmentqty")

                }).ToList();


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return remittances;
        } 

        public IList<Remittance> GetRemittances(RemittanceSearch remittanceSearch)
        {

            var remittances = new List<Remittance>();
            QueryExpression query = new QueryExpression()
            {
                EntityName = "lab_era",
                ColumnSet = new ColumnSet(true),
            };
            try
            {

                EntityCollection remitanceDetilas = Service.RetrieveMultiple(query);

                remittances = remitanceDetilas.Entities.AsEnumerable().Select(sel => new Remittance
                {
                    Id = sel.GetAttributeValue<Guid>("lab_eraid"),
                    RemittanceId = sel.GetAttributeValue("lab_name").ToString(),
                    NumberOfClaimsPaid = 4,
                    PayerName = "ANTHEM",
                    PayeeName = "AVUTOX",
                    PaidAmount = new decimal(136.22),
                    ReceivedDateTime = (DateTime)sel.Attributes["createdon"],
                    BillingTaxId = "123456789"
                }).ToList();

                return remittances;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<Remittance> GetClaimPayments(int remittanceId)
        {

            var remittanceList = new List<Remittance>();
            var sqlStringBuilder = new StringBuilder();
            return remittanceList;
        } 
         

        private readonly IDictionary<string, string> _groupCodeDescription = new Dictionary<string, string>
        {
            {"CO", "Contractual Obligations"},
            {"OA", "Other adjustments"},
            {"PI", "Payor Initiated Reductions"},
            {"PR", "Patient Responsibility"}
        };

        private void FillDescriptions(RemittanceClaimPayment payment)
        {

        }

        private Remittance CloneRemittance(Remittance remittance)
        {
            if (remittance == null)
            {
                return null;
            }
            var result = new Remittance
            {
                Id = remittance.Id,
                CheckNumber = remittance.CheckNumber,
                NumberOfClaimsPaid = remittance.NumberOfClaimsPaid,
                PaidAmount = remittance.PaidAmount,
                PayeeName = remittance.PayeeName,
                PayerName = remittance.PayerName,
                ReceivedDateTime = remittance.ReceivedDateTime
            };
            return result;
        }


    }

    public class ReasonCode
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class RemarkCode
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

}
