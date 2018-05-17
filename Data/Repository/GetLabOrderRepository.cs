using System;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;
using System.Linq;
using Domain.Model;
    using Domain.Interfaces;
    using Microsoft.Xrm.Sdk.Client;
    using Microsoft.Xrm.Sdk.Query;
    using System.ServiceModel.Description;
    using Microsoft.Xrm.Client;
using Infrastructure.Helpers;
using static Domain.Model.CrmStatusReason;
using static Domain.Model.UserRole;
using System.Net;
using System.Text.RegularExpressions;
using Infrastructure.Extensions;

namespace Data.Repository
{
    public class GetLabOrderRepository : ILabOrderRepository
    {
        private readonly string _connectionString;

        public IOrganizationService Service { get; private set; }

        /// <inheritdoc />
        public GetLabOrderRepository(string connectionString)
        {
            _connectionString = connectionString;


            var connArr = connectionString.Split(';');
            if ((connArr.Length < 0)) return;
            var url = connArr[0];
            var userId = connArr[1];
            var password = connArr[2];
            Service = GetService(url, userId, password);
        }


        private IOrganizationService GetService(string url, string userId, string password)
        {
            var serviceUri = new Uri(url);
            var credentials = new ClientCredentials();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var proxy = new OrganizationServiceProxy(serviceUri, null, credentials, null);
            try
            {
                credentials.UserName.UserName = userId;
                credentials.UserName.Password = password;
                proxy.EnableProxyTypes();
                Service = (IOrganizationService)proxy;
                return Service;
            }
            catch
            {
                throw new Exception("Error while accessing CRM instance");
            }
        }

        public string PostHl7OrderForm()
        {

            return "";
        }

        public List<LabDataList> GetLabDataList(UserRole role)
        {

            var payerList = new List<LabDataList>();
            var querybyattribute = new QueryByAttribute("lab_orderscan")
            {
                ColumnSet = new ColumnSet("lab_assignedto", "lab_errordesc", "lab_json_checker",
                    "lab_json_maker", "lab_scanfilename", "lab_scanfileurl", "lab_scanid", "lab_token",
                    "statuscode")
            };
            try
            {
                switch (role)
                {
                    case Maker:
                        querybyattribute.AddAttributeValue("statuscode", (int)Open);
                        break;
                    case Checker:
                        querybyattribute.AddAttributeValue("statuscode", (int)PendingforApproval);
                        break;
                    case SuperAdmin:
                        break;
                    default:
                        querybyattribute.AddAttributeValue("statecode", 0);
                        break;
                }

                //  querybyattribute.AddAttributeValue("lab_assignedto", "surenth.j@sagitec.com"); 
                var orderCollection = Service.RetrieveMultiple(querybyattribute);


                if (orderCollection.Entities.Count > 0 && orderCollection.Entities != null)
                    payerList = orderCollection.Entities.Select(selectScaOrder => new LabDataList
                    {
                        AssignedTo = selectScaOrder.GetFormattedAttributeValue("lab_assignedto"),
                        FileName = selectScaOrder.GetAttributeValue("lab_scanfilename").ToString(),
                        ScanId = selectScaOrder.GetAttributeValue("lab_scanid").ToString(),
                        Status = selectScaOrder.GetEntityReferenceValue<OptionSetValue>("statuscode").Value,
                        LabToken = selectScaOrder.GetFormattedAttributeValue("lab_token"),
                        FileUrl = selectScaOrder.GetAttributeValue("lab_scanfileurl").ToString(),
                        LabscanGuid = selectScaOrder.GetAttributeValue<Guid>("lab_orderscanid")
                    }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return payerList;
        }

        public bool PostHl7OrderForm(LabQueryModel queryModel)
        {
            var orderformEntity = new Entity("lab_orderscan");
            orderformEntity["lab_orderscanid"] = new Guid(queryModel.OrderId);
            try
            {
                if (queryModel.EntryOption != null && (queryModel.Status != (int)Open || queryModel.UserRole != Maker ||
                                                       (int)queryModel.EntryOption.Value != 1))
                {
                    if (queryModel.Status == (int)Open && queryModel.UserRole == Maker &&
                        (int)queryModel.EntryOption.Value == 2)
                    {
                        orderformEntity["lab_json_maker"] = queryModel.Hl7Stream;
                        orderformEntity["statuscode"] = new OptionSetValue((int)PendingforApproval);
                    }
                }
                else
                    orderformEntity["lab_json_maker"] = queryModel.Hl7Stream;

                if (queryModel.EntryOption != null && (queryModel.Status == (int)PendingforApproval && queryModel.UserRole == Checker &&
                                                       (int)queryModel.EntryOption.Value == 2))
                {
                    orderformEntity["lab_json_checker"] = queryModel.Hl7Stream;
                    orderformEntity["statuscode"] = new OptionSetValue((int)Closed);
                }
                else if (queryModel.EntryOption != null && (queryModel.Status == (int)PendingforApproval && queryModel.UserRole == Checker &&
                                                            (int)queryModel.EntryOption.Value == 1))
                    orderformEntity["lab_json_checker"] = queryModel.Hl7Stream;

                Service.Update(orderformEntity);
                return true;
            }
            catch (Exception)
            {
                return false;
                // throw ex;
            }

        }



        public string CheckUserDataExist(LabQueryModel queryModel)
        {
            var isvalidate = new JsonValidator();
            var querybyattribute = new QueryByAttribute("lab_orderscan")
            {
                ColumnSet = new ColumnSet("lab_json_checker", "lab_json_maker", "lab_orderscanid")
            };
            try
            {
                querybyattribute.AddAttributeValue("lab_orderscanid", queryModel.OrderId);
                var labscanGuid = Guid.Parse(queryModel.OrderId);
                var orderCollection = Service.Retrieve("lab_orderscan", labscanGuid, new ColumnSet(true));
                var statuscode = orderCollection.GetEntityReferenceValue<OptionSetValue>("statuscode").Value;

                return queryModel.UserRole == Maker && (int)Open == statuscode && orderCollection.Contains("lab_json_maker")
                    ? (isvalidate.IsValidJson(orderCollection["lab_json_maker"].ToString())
                        ? orderCollection["lab_json_maker"].ToString()
                        : "")
                    : (queryModel.UserRole != null && (queryModel.UserRole == Checker && (int)PendingforApproval == statuscode &&
                                            !orderCollection.Contains("lab_json_checker") &&
                                            orderCollection.Contains("lab_json_maker"))
                        ? (isvalidate.IsValidJson(orderCollection[$"lab_json_maker"].ToString())
                            ? orderCollection[$"lab_json_maker"].ToString()
                            : "")
                        : (queryModel.UserRole == Checker && (int)PendingforApproval == statuscode &&
                           orderCollection.Contains("lab_json_checker")
                            ? (isvalidate.IsValidJson(orderCollection["lab_json_checker"].ToString())
                                ? orderCollection[$"lab_json_checker"].ToString()
                                : "")
                            : ""));
            }
            catch (Exception)
            {
                return "";
            }
        }

        public List<Patients> GetPatientDetails(string Username, int Searchtype)
        {
            var PatientList = new List<Patients>();
            var QEcontact = new QueryExpression("contact");
            QEcontact.TopCount = 50;
            QEcontact.ColumnSet.AllColumns = true;
            switch (Searchtype)
            {
                case 1:
                    QEcontact.Criteria.AddCondition("lastname", ConditionOperator.BeginsWith, Username);
                    break;
                case 2:
                    QEcontact.Criteria.AddCondition("firstname", ConditionOperator.BeginsWith, Username);
                    break; 
            }

            try
            {
                var orderCollection = Service.RetrieveMultiple(QEcontact);
                if (orderCollection.Entities.Count > 0 && orderCollection.Entities != null)
                    PatientList = orderCollection.Entities.Select(selectScaOrder => new Patients
                    {
                        PatientId = selectScaOrder.GetAttributeValue<Guid>("contactid"),
                        PatientLastName = selectScaOrder.GetCrmAttributeValue<string>("lastname"),
                        PatientFirstName = selectScaOrder.GetCrmAttributeValue<string>("firstname"),
                        DOB = selectScaOrder.GetCrmDateAttributeValue<DateTime>("birthdate"),
                        Sex = selectScaOrder.GetCrmAttributeValue<OptionSetValue>("gendercode")

                    }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PatientList;
        }

        public List<PatientInsurance> GetPatientInsurance(string patientID)
        {

            //patientID = "c68736e4-3437-e811-a952-000d3a35f53a";
            var PatientInsurance = new List<PatientInsurance>();


            // CRM Fetch Query

            var QEcontact = new QueryExpression("contact");
            QEcontact.TopCount = 50;
            QEcontact.ColumnSet.AllColumns = true;
            QEcontact.Criteria.AddCondition("lab_insurance", "lab_patientid", ConditionOperator.Equal, patientID);
            var QEcontact_lab_insurance = QEcontact.AddLink("lab_insurance", "contactid", "lab_patientid");
            QEcontact_lab_insurance.Columns.AllColumns = true;
            var QEcontact_lab_insurance_lab_payor = QEcontact_lab_insurance.AddLink("lab_payor", "lab_payorid", "lab_payorid");
            QEcontact_lab_insurance_lab_payor.Columns.AddColumns("lab_name");

            //---------------------

            try
            {
                var orderCollection = Service.RetrieveMultiple(QEcontact);

                //string theSSN = "123456789";
                // Regex ssnRegex = new Regex("(?[0 - 9]{ 3 })(?[0 - 9]{ 2})(?[0 - 9]{ 4})");
                // string formattedSSN = ssnRegex.Replace(theSSN, "XXX - XX -${ last}");

                if (orderCollection.Entities.Count > 0 && orderCollection.Entities != null)
                    PatientInsurance = orderCollection.Entities.Select(selectPatientInsurance => new PatientInsurance
                    {
                        Patients = new Patients
                        {
                            PatientId = selectPatientInsurance.GetAttributeValue<Guid>("contactid"),
                            PatientSSN = selectPatientInsurance.GetCrmAttributeValue<string>(("lab_patientssn"), 4),//Substring of four last four digit
                            PatientLastName = selectPatientInsurance.GetCrmAttributeValue<string>("lastname"),
                            PatientFirstName = selectPatientInsurance.GetCrmAttributeValue<string>("firstname"),
                            DOB = (DateTime)selectPatientInsurance.Attributes["birthdate"],
                            Sex = selectPatientInsurance.GetAttributeValue<OptionSetValue>("gendercode"),
                            PatientAddress = new Address
                            {
                                Address1 = selectPatientInsurance.GetCrmAttributeValue<string>("address1_line1"),
                                Address2 = selectPatientInsurance.GetCrmAttributeValue<string>("address1_line2"),
                                City = selectPatientInsurance.GetCrmAttributeValue<string>("address1_city"),
                                State = selectPatientInsurance.GetCrmAttributeValue<string>("address1_stateorprovince"),
                                Phone = selectPatientInsurance.GetCrmAttributeValue<string>("address1_telephone1"),
                                Zip = selectPatientInsurance.GetCrmAttributeValue<string>("address1_postalcode")
                            }

                        },
                        Subscriber = new Suscriber
                        {
                            SubscriberLastName = selectPatientInsurance.GetAliasedAttributeValue<string>("lab_insurance1.lab_sbrlastname"),
                            SubscriberFirstName = selectPatientInsurance.GetAliasedAttributeValue<string>("lab_insurance1.lab_sbrfirstname"),
                            SubscriberInsuranceId = selectPatientInsurance.GetAliasedAttributeValue<string>("lab_insurance1.lab_sbrid"),
                            SubscriberGroupId = selectPatientInsurance.GetAliasedAttributeValue<string>("lab_insurance1.lab_groupid"),
                            SubscriberRelationship = (OptionSetValue)selectPatientInsurance.GetAttributeValue<AliasedValue>("lab_insurance1.lab_relationship").Value
                        },
                        Payer = new Payer
                        {


                            PayerId = selectPatientInsurance.GetAliasedAttributeValue<string>("lab_payor2.lab_payorid"),
                            PayerName = selectPatientInsurance.GetAliasedAttributeValue<string>("lab_payor2.lab_name"),
                            PayerAddress = new Address
                            {
                                Address1 = selectPatientInsurance.GetAliasedAttributeValue<string>("lab_insurance1.lab_payor_addr1"),
                                Address2 = selectPatientInsurance.GetAliasedAttributeValue<string>("lab_insurance1.lab_payor_addr2"),
                                City = selectPatientInsurance.GetAliasedAttributeValue<string>("lab_insurance1.lab_payor_city"),
                                State = selectPatientInsurance.GetAliasedAttributeValue<string>("lab_insurance1.lab_payor_state"),
                                Phone = "1234-123-333",
                                Zip = selectPatientInsurance.GetAliasedAttributeValue<string>("lab_insurance1.lab_payor_zip")
                            }
                        }
                    }).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PatientInsurance;

        }

        public string GetSSN(string patientID)
        {
            var QEcontact = new QueryExpression("contact");
            QEcontact.TopCount = 1;
            QEcontact.ColumnSet.AddColumns("lab_patientssn");
            QEcontact.Criteria.AddCondition("contactid", ConditionOperator.Equal, patientID);
            var orderCollection = Service.RetrieveMultiple(QEcontact);
            return orderCollection.Entities.Count > 0 && orderCollection.Entities != null
                ? orderCollection.Entities[0].GetCrmAttributeValue<string>(("lab_patientssn"), 9)
                : "";

        }
    }
}
    

