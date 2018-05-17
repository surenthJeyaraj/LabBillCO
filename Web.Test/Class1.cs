using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Web.Service;
using Domain.Model;
using Data.Repository;

namespace Web.Test
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void GenerateReportProperly()
        {
            var remittance = new Remittance();
            remittance.NumberOfClaimsPaid = 2;
            remittance.PaidAmount = 350;
            remittance.PayeeName = "Ragland Solomon";
            remittance.PayerName = "Derickson Timothy";
            remittance.ProductCode = "008";
            remittance.ReceivedDateTime = DateTime.Now;
            remittance.BillingLocationName = "Ashburn Dentistry";
            remittance.RemittanceClaimPayment = new List<RemittanceClaimPayment>
            {
                new RemittanceClaimPayment
                {
                    AccountNumber = "345000987",
                    ClaimAmount = 350,
                    PaidAmount = 25,
                    PatientFirstName = "Venugopal",
                    PatientLastName = "Sakthivel",
                    PatientIdentificationNumber = "500046",
                    ClaimNumber = "54356",
                    
                    ReasonCodes = new List<RemittanceClaimPaymentReasonCode>
                    {
                        new RemittanceClaimPaymentReasonCode
                        {
                            Amount = 35,
                            GroupCode = "CO",
                            ReasonCode = "45"
                        }
                    },
                    ServiceLinePayments = new List<ClaimServiceLinePayment>
                    {
                        new ClaimServiceLinePayment
                        {
                            CoInsurance = 10,
                            Deductibles = 4,
                            PaidAmount = 2,
                            PatientPortion = 6,
                            ProcedureCode = "D4502",
                            SubmittedAmount = 15,
                            Disallowed = 5,
                            AllowedAmount = 4,
                            UnitsPaid = 2,
                            ReasonCodes = new List<RemittanceClaimPaymentReasonCode>
                            {
                                new RemittanceClaimPaymentReasonCode
                                {
                                    Amount = 35,
                                    GroupCode = "PR",
                                    ReasonCode = "3"
                                }
                            },
                            RemarkCodes = new List<RemittanceClaimPaymentRemarkCode>
                            {
                                new RemittanceClaimPaymentRemarkCode
                                {
                                    RemarkCode = "N19"
                                }   
                            }
                        }
                    }
                }
            };
            var repo = new RemittanceRepository("Data Source=172.17.17.20;Initial Catalog=HPSCH_Provider;User ID=app_dn;Password=zqZwPbfqQFj7");

            var rem = repo.GetRemittanceForReport(44);
            var generator = new RemittanceReportGenerator();
            var html = generator.GenerateHtml(rem);
            Assert.Equals(string.Empty, html);
        }
    }
}
