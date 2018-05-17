using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using Domain.Model;
using System.Text;
using iTextSharp.tool.xml.html;
using WebGrease.Css.Extensions;

namespace Web.Service
{
    public class RemittanceReportGenerator : IRemittanceReportGenerator
    {
        public string GenerateHtml(Remittance remittance)
        {
            //            var writer = new StringWriter();
            //            using (var htmlWriter = new HtmlTextWriter(writer))
            //            {
            //                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Type, "text/css");
            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Style);
            //                htmlWriter.Write(".nb-table td,.nb-table th {border: 1px solid gray !important;}");
            //                htmlWriter.Write(".nb-table {border-collapse: collapse !important;}");
            //                htmlWriter.Write("td{padding-left:5px;}");
            //                htmlWriter.Write("table {border-collapse:collapse !important;}");
            //                htmlWriter.Write(".leftBorder{border: 0;border-color: gray;border-left-width: 1px;border-left-style: solid;}");
            //                htmlWriter.Write(".rightBorder{border: 0;border-color: gray;border-right-width: 1px;border-right-style: solid;}");
            //                htmlWriter.RenderEndTag();

            //                htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.FontFamily, "calibri");
            //                htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "14px");
            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Div);//<div>
            //                htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, "#DDDDDD");
            //                htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");

            //                #region Payer,Date, Check, Billing location
            //                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "nb-table");
            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Table);//<table>

            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
            //                htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.PaddingRight, "10px");
            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Span);
            //                htmlWriter.Write("Payer");
            //                htmlWriter.RenderEndTag();
            //                htmlWriter.RenderEndTag();//</td>
            //                htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.PaddingLeft, "10px");
            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Span);
            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.B);
            //                htmlWriter.Write(remittance.PayerName);
            //                htmlWriter.RenderEndTag();
            //                htmlWriter.RenderEndTag();
            //                htmlWriter.RenderEndTag();//</td>
            //                htmlWriter.RenderEndTag();//<tr>

            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                htmlWriter.RenderEndTag();//</td>
            //                htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.PaddingLeft, "10px");
            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                htmlWriter.Write("For questions regarding this ERA, please contact the payer");
            //                htmlWriter.RenderEndTag();//</td>
            //                htmlWriter.RenderEndTag();//<tr>

            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
            //                htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.PaddingRight, "10px");
            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Span);
            //                htmlWriter.Write("Remittance Date");
            //                htmlWriter.RenderEndTag();
            //                htmlWriter.RenderEndTag();//</td>
            //                htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.PaddingLeft, "10px");
            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Span);
            //                htmlWriter.Write(remittance.ReceivedDateTime.ToString("d"));
            //                htmlWriter.RenderEndTag();
            //                htmlWriter.RenderEndTag();//</td>
            //                htmlWriter.RenderEndTag();//<tr>

            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
            //                htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.PaddingRight, "10px");
            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Span);
            //                htmlWriter.Write("Trace/Check");
            //                htmlWriter.RenderEndTag();
            //                htmlWriter.RenderEndTag();//</td>
            //                htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.PaddingLeft, "10px");
            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Span);
            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.B);
            //                htmlWriter.Write(remittance.CheckNumber);
            //                htmlWriter.RenderEndTag();
            //                htmlWriter.RenderEndTag();
            //                htmlWriter.RenderEndTag();//</td>
            //                htmlWriter.RenderEndTag();//<tr>

            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                htmlWriter.RenderEndTag();//</td>
            //                htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.PaddingLeft, "10px");
            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Span);
            //                htmlWriter.Write("Remittance Information Only via Automated Clearinghouse");
            //                htmlWriter.RenderEndTag();
            //                htmlWriter.RenderEndTag();//</td>
            //                htmlWriter.RenderEndTag();//<tr>

            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
            //                htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.PaddingRight, "10px");
            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Span);
            //                htmlWriter.Write("To");
            //                htmlWriter.RenderEndTag();
            //                htmlWriter.RenderEndTag();//</td>
            //                htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.PaddingLeft, "10px");
            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Span);
            //                htmlWriter.Write(remittance.BillingLocationName);
            //                htmlWriter.RenderEndTag();
            //                htmlWriter.RenderEndTag();//</td>
            //                htmlWriter.RenderEndTag();//<tr>

            //                htmlWriter.RenderEndTag();//</table>
            //# endregion

            //                #region Payment Table
            //                foreach (var payment in remittance.RemittanceClaimPayment)
            //                {
            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.Height, "15px");
            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Div);
            //                    htmlWriter.RenderEndTag();
            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, "#DDDDDD");
            //                    htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "nb-table");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Table);
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);

            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.Width, "20%");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.B);
            //                    htmlWriter.Write("Name: ");
            //                    htmlWriter.RenderEndTag();
            //                    htmlWriter.WriteBreak();
            //                    htmlWriter.Write("{0} {1}", payment.PatientLastName, payment.PatientFirstName);
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.Width, "10%");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.B);
            //                    htmlWriter.Write("Account#: ");
            //                    htmlWriter.WriteBreak();
            //                    htmlWriter.RenderEndTag();
            //                    htmlWriter.Write(payment.AccountNumber);
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.Width, "10%");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.B);
            //                    htmlWriter.Write("HIC: ");
            //                    htmlWriter.WriteBreak();
            //                    htmlWriter.RenderEndTag();
            //                    htmlWriter.Write(payment.PatientIdentificationNumber);
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.Width, "10%");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.B);
            //                    htmlWriter.Write("ICN: ");
            //                    htmlWriter.WriteBreak();
            //                    htmlWriter.RenderEndTag();
            //                    htmlWriter.Write(payment.PayerInternalControlNumber);
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.Width, "10%");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.B);
            //                    htmlWriter.Write("Claim#: ");
            //                    htmlWriter.WriteBreak();
            //                    htmlWriter.RenderEndTag();
            //                    htmlWriter.Write(payment.ClaimNumber);
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.Width, "40%");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.B);
            //                    htmlWriter.Write("Status: ");
            //                    htmlWriter.WriteBreak();
            //                    htmlWriter.RenderEndTag();
            //                    htmlWriter.Write(payment.ClaimStatusCodeDescription);
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.RenderEndTag();
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Table);
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Thead);
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "left");
            //                    htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "leftBorder");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Th);
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.B);
            //                    htmlWriter.Write("Procedure");
            //                    htmlWriter.RenderEndTag();
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Th);
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Th);
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.B);
            //                    htmlWriter.Write("Units Paid");
            //                    htmlWriter.RenderEndTag();
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Th);
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.B);
            //                    htmlWriter.Write("Original Units");
            //                    htmlWriter.RenderEndTag();
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Th);
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.B);
            //                    htmlWriter.Write("Procedure Date");
            //                    htmlWriter.RenderEndTag();
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Th);
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.B);
            //                    htmlWriter.Write("Charge");
            //                    htmlWriter.RenderEndTag();
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Th);
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.B);
            //                    htmlWriter.Write("Allowed");
            //                    htmlWriter.RenderEndTag();
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Th);
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.B);
            //                    htmlWriter.Write("Patient Portion");
            //                    htmlWriter.RenderEndTag();
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Th);
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.B);
            //                    htmlWriter.Write("Deductible");
            //                    htmlWriter.RenderEndTag();
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Th);
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.B);
            //                    htmlWriter.Write("Co-Insurance");
            //                    htmlWriter.RenderEndTag();
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Th);
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.B);
            //                    htmlWriter.Write("Disallowed");
            //                    htmlWriter.RenderEndTag();
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Th);
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.B);
            //                    htmlWriter.Write("Reason");
            //                    htmlWriter.RenderEndTag();
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                    htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "rightBorder");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Th);
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.B);
            //                    htmlWriter.Write("Paid");
            //                    htmlWriter.RenderEndTag();
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.RenderEndTag();
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tbody);
            //                    foreach (var servicePayment in payment.ServiceLinePayments)
            //                    {
            //                        for (var i = 0; i < servicePayment.ReasonCodes.Count; i++)
            //                        {
            //                            var isFirstRow = i == 0;
            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);

            //                            htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "left");
            //                            htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "leftBorder");
            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                            if (isFirstRow)
            //                            {
            //                                htmlWriter.Write(servicePayment.ProcedureCode);
            //                            }
            //                            htmlWriter.RenderEndTag();

            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                            htmlWriter.RenderEndTag();

            //                            htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                            if (isFirstRow)
            //                            {
            //                                htmlWriter.Write(servicePayment.UnitsPaid);
            //                            }
            //                            htmlWriter.RenderEndTag();

            //                            htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                            if (isFirstRow)
            //                            {
            //                                htmlWriter.Write("-");
            //                            }
            //                            htmlWriter.RenderEndTag();

            //                            htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                            if (isFirstRow)
            //                            {
            //                                htmlWriter.Write(servicePayment.ProcedureDate.ToShortDateString());
            //                            }
            //                            htmlWriter.RenderEndTag();

            //                            htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                            if (isFirstRow)
            //                            {
            //                                htmlWriter.Write(servicePayment.SubmittedAmount);
            //                            }
            //                            htmlWriter.RenderEndTag();

            //                            htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                            if (isFirstRow)
            //                            {
            //                                htmlWriter.Write(servicePayment.AllowedAmount);
            //                            }
            //                            htmlWriter.RenderEndTag();

            //                            htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                            if (isFirstRow)
            //                            {
            //                                htmlWriter.Write(servicePayment.PatientPortion);
            //                            }
            //                            htmlWriter.RenderEndTag();

            //                            htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                            if (isFirstRow)
            //                            {
            //                                htmlWriter.Write(servicePayment.Deductibles);
            //                            }
            //                            htmlWriter.RenderEndTag();

            //                            htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                            if (isFirstRow)
            //                            {
            //                                htmlWriter.Write(servicePayment.CoInsurance);
            //                            }
            //                            htmlWriter.RenderEndTag();

            //                            htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                            htmlWriter.Write(servicePayment.ReasonCodes[i].Amount.Value.ToString("0.00"));
            //                            htmlWriter.RenderEndTag();

            //                            htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                            htmlWriter.Write("{0}-{1}",servicePayment.ReasonCodes[i].GroupCode, servicePayment.ReasonCodes[i].ReasonCode);
            //                            htmlWriter.RenderEndTag();

            //                            htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                            htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "rightBorder");
            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                            if (isFirstRow)
            //                            {
            //                                htmlWriter.Write(servicePayment.PaidAmount);
            //                            }
            //                            htmlWriter.RenderEndTag();

            //                            htmlWriter.RenderEndTag();
            //                        }
            //                        foreach (var remark in servicePayment.RemarkCodes)
            //                        {
            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);

            //                            htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "leftBorder");
            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                            htmlWriter.RenderEndTag();

            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.B);
            //                            htmlWriter.Write("Remarks: {0}",remark.RemarkCode);
            //                            htmlWriter.RenderEndTag();
            //                            htmlWriter.RenderEndTag();

            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                            htmlWriter.RenderEndTag();

            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                            htmlWriter.RenderEndTag();

            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                            htmlWriter.RenderEndTag();

            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                            htmlWriter.RenderEndTag();

            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                            htmlWriter.RenderEndTag();

            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                            htmlWriter.RenderEndTag();

            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                            htmlWriter.RenderEndTag();

            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                            htmlWriter.RenderEndTag();

            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                            htmlWriter.RenderEndTag();

            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                            htmlWriter.RenderEndTag();

            //                            htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "rightBorder"); ;
            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                            htmlWriter.RenderEndTag();

            //                            htmlWriter.RenderEndTag();
            //                        }
            //                    }

            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.Color, "red");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);

            //                    htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "leftBorder");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                    htmlWriter.Write("(Post Payment)");
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                    htmlWriter.Write("SubTotal:");
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                    htmlWriter.Write(payment.ServiceLinePayments.Sum(x => x.SubmittedAmount));
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                    htmlWriter.Write(payment.ServiceLinePayments.Sum(x => x.AllowedAmount));
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                    htmlWriter.Write(payment.ServiceLinePayments.Sum(x => x.PatientPortion));
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                    htmlWriter.Write(payment.ServiceLinePayments.Sum(x => x.Deductibles));
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                    htmlWriter.Write(payment.ServiceLinePayments.Sum(x => x.CoInsurance));
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                    htmlWriter.Write(GetDisallowedTotal(payment));
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
            //                    htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "rightBorder");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                    htmlWriter.Write(payment.ServiceLinePayments.Sum(x => x.PaidAmount));
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.RenderEndTag();
            //                    htmlWriter.RenderEndTag();

            //                    htmlWriter.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
            //                    htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "nb-table");
            //                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Table);
            //                    var descriptionRepetitionLookup = new List<string>();
            //                    foreach (var payReasonCode in payment.ReasonCodes)
            //                    {
            //                        var code = string.Format("{0}-{1}", payReasonCode.GroupCode, payReasonCode.ReasonCode);
            //                        var codeDescription = (string.IsNullOrWhiteSpace(payReasonCode.GroupCodeDescription))
            //                            ? payReasonCode.ReasonCodeDescription
            //                            : string.Format("{0} : {1}", payReasonCode.GroupCodeDescription,
            //                                payReasonCode.ReasonCodeDescription);
            //                        if (!descriptionRepetitionLookup.Contains(code))
            //                        {
            //                            descriptionRepetitionLookup.Add(code);
            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                            htmlWriter.Write(code);
            //                            htmlWriter.RenderEndTag();
            //                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                            htmlWriter.Write(codeDescription);
            //                            htmlWriter.RenderEndTag();
            //                            htmlWriter.RenderEndTag();    
            //                        }
            //                    }
            //                    foreach (var servicePayment in payment.ServiceLinePayments)
            //                    {
            //                        foreach (var serviceReasonCode in servicePayment.ReasonCodes)
            //                        {
            //                            var code = string.Format("{0}-{1}", serviceReasonCode.GroupCode, serviceReasonCode.ReasonCode);
            //                            var codeDescription = (string.IsNullOrWhiteSpace(serviceReasonCode.GroupCodeDescription))
            //                          ? serviceReasonCode.ReasonCodeDescription
            //                          : string.Format("{0} : {1}", serviceReasonCode.GroupCodeDescription,
            //                              serviceReasonCode.ReasonCodeDescription);
            //                            if (!descriptionRepetitionLookup.Contains(code))
            //                            {
            //                                descriptionRepetitionLookup.Add(code);
            //                                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
            //                                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                                htmlWriter.Write(code);
            //                                htmlWriter.RenderEndTag();
            //                                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                                htmlWriter.Write(codeDescription);
            //                                htmlWriter.RenderEndTag();
            //                                htmlWriter.RenderEndTag();
            //                            }
            //                        }
            //                        foreach (var serviceRemarkCode in servicePayment.RemarkCodes)
            //                        {
            //                             var code = serviceRemarkCode.RemarkCode;
            //                            if (!descriptionRepetitionLookup.Contains(code))
            //                            {
            //                                descriptionRepetitionLookup.Add(code);
            //                                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
            //                                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                                htmlWriter.Write(serviceRemarkCode.RemarkCode);
            //                                htmlWriter.RenderEndTag();
            //                                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
            //                                htmlWriter.Write(serviceRemarkCode.RemarkCodeDescription);
            //                                htmlWriter.RenderEndTag();
            //                                htmlWriter.RenderEndTag();
            //                            }
            //                        }
            //                    }

            //                    htmlWriter.RenderEndTag();
            //                }
            //                #endregion
            //                htmlWriter.RenderEndTag();
            //            }
            //            return writer.ToString();

            return null;
        }   
        
        private string GetDisallowedTotal(RemittanceClaimPayment payment)
        {
            //var sumOfPaymentDisallowed = payment.ReasonCodes.Sum(x => x.Amount);
            // var sumOfServiceDisallowed = payment.ServiceLinePayments.Select(x => x.ReasonCodes.Sum(y => y.Amount)).Sum(z => z);
            // return (sumOfServiceDisallowed.HasValue) ? sumOfServiceDisallowed.Value.ToString("0.00") : "0.00";
            return "";
        }
    }
}