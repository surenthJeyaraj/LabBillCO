
var AVUTOXPortal = AVUTOXPortal || {};
AVUTOXPortal.TransactionTable = AVUTOXPortal.TransactionTable || {};
AVUTOXPortal.TransactionData = [],
AVUTOXPortal.TransactionList = function () {
};
AVUTOXPortal.AssignPayerList = AVUTOXPortal.AssignPayerList || {}
//AVUTOXPortal.DownloadRequest = function (transactionId) {
//    var data = "TransactionID=" + transactionId;
//    var $tr = this;
//    $.get("/Transaction/RequestDownload", function (data, status) {

//        });  
//}
AVUTOXPortal.FetchCHResponseFile = function (transactionId) {
    var data = "TransactionID=" + transactionId;
    var t = $.get(AVUTOXPortal.resolveUrl("~/Transaction/DownloadCHResponse"), data);
    alert(data);

};
AVUTOXPortal.FetchIndividualResponse = function (transactionId) {

    alert(transactionId);
};
AVUTOXPortal.FetchTA1Status = function (TA1Value, e) {
    e.preventDefault();
    window.location.href = AVUTOXPortal.resolveUrl("~/Transaction/GetTA1Status") + "?TransactionID=" + TA1Value;
    // var t = $.get("~/Transaction/GetTA1Status" + "?TransactionID=" +TA1Value);
    return false;
    //alert(data);
};
         
AVUTOXPortal.AssignPayerList = function BuildPayerList() {
    var htmlToAppend = '';
    var data = $.ajax({
        type: 'POST',
        url: AVUTOXPortal.resolveUrl('~/Transaction/GetLabDataList'),
        dataType: "json",
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            htmlToAppend += '<option value=""></option>';
            for (var j = 0; j < data.length; j++) {
                htmlToAppend += '<option value="' + data[j].PayerId + '">' + data[j].PayerName + '</option>';
            }
            $('#ddlPayerList').append(htmlToAppend);
        }
    });
};


AVUTOXPortal.BuildTransactionTable = function () {
    //jUI Date Picker



    AVUTOXPortal.TransactionTable = $('#tblResult').dataTable({
        //Disabled the Common search Filter

        "oLanguage": {
            //"sInfo":"Got a total of _TOTAL_ entries to show (_START_ to _END_)"
            "sInfo": "Showing _START_ to _END_ of _TOTAL_ records",
            "sLengthMenu": "Show _MENU_ records",
            "sEmptyTable": "No records found!"
        },
        "bFilter": false,
        "bProcessing": true,
        "bDeferRender": true,
        //"iDeferLoading": function() {
        //    alert('test');
        //    return 0;
        //},
        "sPaginationType": "bootstrap",
        "bJQueryUI": true,
        "bServerSide": true,
        "sAjaxSource": AVUTOXPortal.resolveUrl("~/Transaction/PopulateResult"),
        "bSortable": false,
        "bAutoWidth": false,
        "fnServerData": function (sSource, aoData, fnCallback) {
            $.ajax({
                url: sSource,
                type: 'POST',
                contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                dataType: 'json',
                data: aoData,
                success: function (result) {
                    AVUTOXPortal.TransactionData = result["aaData"];
                    fnCallback(result);
                },
            });

        },

        "fnServerParams": function (aoData) {
            var ediversion5010A2 = "";
            var ediversion4010A1 = "";
            var transModeProd = "";
            var transModeTest = "";
            var typeclaim = "";
            var typePreD = "";
            if ($('#ediVersion5010A2').is(":checked"))
                ediversion5010A2 = $('#ediVersion5010A2').val();
            if ($('#ediVersion4010A1').is(":checked"))
                ediversion4010A1 = $('#ediVersion4010A1').val();

            var userId = $('#userId').val();
            var subscriberId = $('#subscriberId').val();
            var claimno = $('#claimNo').val();
            var billingNPI = $('#billingNPI').val();
            //var transactionMode = $('#transactionMode').val();

            if ($('#typeProd').is(":checked"))
                transModeProd = $('#typeProd').val();

            if ($('#typeTest').is(":checked"))
                transModeTest = $('#typeTest').val();

            var subscriberLastName = $('#subscriberLName').val();

            //var billingProviderLname = $('#billingProviderLname').val();

            //var payerId = $('#ddlPayerList').val();
            var payerId = $("#ddlPayerList option:selected").val();
            var subscriberFirstName = $('#subscriberFirstName').val();
            var patientFirstName = $('#patientFirstName').val();
            var patientLastName = $('#patientLastName').val();
            // var arrDateRange = $('#dateRange').val().split(' to ');
            //  var postedDateFrom = arrDateRange[0];
            // var postedDateTo = arrDateRange[1];

            //Condition added by Ravi on 07/02/2014 for the Ticket #4523 - MP-US-38 Number of transactions received for last transaction date
            var ackReajection = "";
            if ($('#hidPosteddate').val().length > 0 && $('#hidCheckStatus').val()=='Y') {
                 
                ackReajection = "R";
            }

            var postedDateFrom = $('#postedDateFrom').val();
            var postedDateTo = $('#postedDateTo').val();
            var fileName = $('#fileName').val();
            if ($('#chkClaim').is(":checked"))
                typeclaim = $('#chkClaim').val();

            if ($('#chkPreD').is(":checked"))
                typePreD = $('#chkPreD').val();

            aoData.push(
                {
                    name: "transactionSearchCriteria",
                    value: JSON.stringify(
                    {
                        EdiVersion5010A2: ediversion5010A2,
                        EdiVersion4010A1: ediversion4010A1,
                        UserId: userId,
                        ClaimNo: claimno,
                        BillingNPI: billingNPI,
                        TransactionModeProd: transModeProd, TransactionModeTest: transModeTest, SubscriberId: subscriberId, SubscriberLastName: subscriberLastName, PatientLastName: patientLastName, BillingNPI: billingNPI, PostedDateFrom: postedDateFrom, PostedDateTo: postedDateTo, PayerId: payerId, SubscriberFirstName: subscriberFirstName, PatientFirstName: patientFirstName, FileName: fileName, Claim: typeclaim, PreD: typePreD
                        , AckReajection: ackReajection
                    })
                }
            );
        },
        "aoColumns": [
            {
                "mData": "Id", "sTitle": "Transaction ID", "bSearchable": false, "bSortable": true, "sWidth": "12%", "sWrap":true,
                "mRender": function (a, b, oObj) {
                return oObj["Id"]
                }
            },
            {
                "mData": "FileName", "sTitle": "File Name", "sWidth": "16%", "sWrap": true, "bSortable": true,
                "mRender": function (a, b, oObj) {
                    //return '<i title="Posted file" class="icon-file-4 CHPosted" style="font-size:20px" data-transactionId=' + oObj["Id"] + " data-FileName=" + oObj["FileName"] + ' ></i>'
                    return '<a style="cursor:pointer;" class="CHPosted" data-transactionId=' + oObj["Id"] + " data-FileName=" + oObj["FileName"] + ' >' + oObj["FileName"] + '</a>'
                }
            },
            //{
            //    "mData": null, "sWidth": "8%","sTitle": "Posted File", "mRender": function (a, b, oObj) {
            //        //return "<i class='icon-download-2 on-right on-left' style='color: blue; font-size:samall; padding: 8px;'></i>" 
            //        return '<i title="Posted file" class="icon-file-4 CHPosted" style="font-size:20px" data-transactionId=' + oObj["Id"] + " data-FileName=" + oObj["FileName"] + ' ></i>'
            //    }
            //},
            { "mData": "UserId", "sTitle": "User ID", "sClass": "center", "sWidth": "10%",  "bSortable": true },
            {
                "mData": "PostedDate", "sTitle": "Date", "sClass": "center", "sWidth": "8%",  "bSortable": true, "mRender": function (a, b, oObj) {


                    var arDate = new Date(oObj['PostedDate']);
                    var srDate = (arDate.getMonth() + 1) + "/" + (arDate.getDate()) + "/" + arDate.getFullYear();
                    return srDate;

                    // return oObj["PostedDate"].substring(0, 10);

                }
            },


             { "mData": "EdiVersion", "sTitle": "Version", "sWidth": "8%", "sClass": "center", "bSortable": true },
            //{ "mData": "TransactionTypeString", "sTitle": "Type", "bSortable": false },
            { "mData": "TransactionModeString", "sTitle": "Mode", "sWidth": "4%", "sClass": "center", "bSortable": true },
           // { "mData": "TA1StatusString", "sTitle": "TA1", "bSortable": false,"sWidth": "10px", },
            {
                "mData": "TA1StatusString", "sTitle": "TA1", "sWidth": "4%", "sClass": "center", "mRender": function (a, b, oObj) {

                    //return oObj["TA1StatusString"].substring(0, 10);
                    var ta1 = oObj["TA1StatusString"].substring(0, 10);
                    if (ta1 == "A") return "<i class='icon-checkmark'; title='Accepted'; style='color:green;font-size:10px'></i>"; else if (ta1 == "R") return '<i class="icon-cancel-2 TA1Status" data-transactionId=' + oObj["Id"] + " style='color:red;font-size:10px;cursor:pointer;'></i>"; else return ta1;

                }
            },
           {
                "mData": "Acknowledgment997Or999StatusString", "sWidth": "5%",  "sTitle": "997/999", "sClass": "center", "mRender": function (a, b, oObj) {

                    if (oObj["Acknowledgment997Or999StatusString"] != null) {
                        var o997 = oObj["Acknowledgment997Or999StatusString"].substring(0, 10);
                        if (o997 == "A") return "<i class='icon-checkmark'; title='Accepted'; style='color:green;font-size:10px'></i>"; else if (o997 == "R") return '<i class="icon-cancel-2 Acknowledge" data-transactionId=' + oObj["Id"] + " style='color:red;font-size:10px;cursor:pointer;'></i>"; else return o997;
                    }
                    else
                        return "";
                }
            },
           {
                 "mData": "TransactionSummaryStatusString", "sWidth": "12%", "sTitle": "Report", "bSortable": false, "sClass": "center", "mRender": function (a, b, oObj) {

                     if (oObj["TransactionSummaryStatusString"] != null) {
                         var oTRN = oObj["TransactionSummaryStatusString"].substring(0, 10);
                         if (oTRN != "") {
                             if (oTRN == "A") return '<i class="icon-file-4 Summary" data-transactionId=' + oObj["Id"] + " data-FileName=" + oObj["FileName"] + " title='Accepted'; style='color:green;font-size:20px;cursor:pointer;'></i>"; else if (oTRN == "R") return '<i class="icon-file-4 Summary" data-transactionId=' + oObj["Id"] + " data-FileName=" + oObj["FileName"] + " title='Rejected'; style='color:red;font-size:20px;cursor:pointer;'></i>"; else return oTRN;

                         }
                     }
                     else
                         return "";
                 }
             },

          
            {
                "mData": "AcceptedClaims", "sTitle": "Total (Accept/Reject)", "sWidth": "8%", "sClass": "right", "bSortable": false, "mRender": function (a, b, oObj) {

                    if (oObj["TotalClaims"].toString() != 0)
                        return oObj["TotalClaims"].toString() + " (" + oObj["AcceptedClaims"].toString() + "/" + oObj["RejectedClaims"].toString() + ")";
                    else
                        return "";
                }
            },
            {
                "mData": "AcceptedClaims", "sTitle": "Status", "sWidth": "4%", "mRender": function (a, b, oObj) {

                    if (oObj["TotalClaims"].toString() != 0) {
                        var ta1 = oObj["TotalClaims"].toString() == oObj["AcceptedClaims"].toString() ? "Yes" : "No"
                        if (ta1 == "Yes")
                            return "<i title='Transaction accepted' class='icon-sun'; style='color:darkorange;font-size:20px'></i>";
                        else if (oObj["AcceptedClaims"].toString() == 0)
                            return "<i title='Transaction rejected' style='color:red'; class='icon-rainy-2';'></i>";
                        else if (ta1 == "No")
                            return "<i title='Partially accepted' style='color:blue'; class='icon-cloudy';'></i>";
                        else return ta1;
                    }
                    else
                        return "";
                }
            }
        ],
        "aaSorting": [[3, "desc"]],


    }).css("width", "100%");;

   

    $('#tblResult tbody').on('click', '.viewClaim', function (e) {
        e.preventDefault();
        var transactionId = $(this).data("transactionid");
        var claimNumber = $(this).data("claim-number");
        $.ajax({
            type: 'POST',
            url: AVUTOXPortal.resolveUrl('~/Transaction/GetClaimX12'),
            dataType: "json",
            async: false,
            data: JSON.stringify({ 'TransactionID': transactionId, 'ClaimID': claimNumber }),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.isValid) {
                    var style = "width: 530px;font-size: 13px;min-height: 600px;height: 600px;overflow:scroll;padding: 1px;margin-right: 7px;";
                    var html = "<div style='" + style + "'>";
                    html = html + data.ClaimX12;
                    html = html + "</div>";
                    $.Dialog({
                        shadow: true,
                        overlay: true,
                        title: 'Claim Number - ' + claimNumber,
                        width: 300,
                        height: 600,
                        padding: 8,
                        content: html
                    });
                }
                else {
                    alert('Error occured while retrieving request.');
                }
            }
        });
        return false;
    });

    $('#tblResult tbody').on('click', 'tr', function (ev) {

        // alert("Test" + AVUTOXPortal.TransactionData[1].value);
        var $tr = this;

        // to test the data using Fake table

        var TransactionID = $($tr).find('.multiselect').data('id');
        var oHtml = '';
        if (AVUTOXPortal.TransactionTable.fnIsOpen(this)) {
            AVUTOXPortal.TransactionTable.fnClose(this);
        } else {
            var id = $($tr).find("td:first").text().trim();

            var oTot= $($tr).find("td:eq(9)").text().trim();
            if (oTot == "") {
                return false;
            }



            var tran = null;
            $.each(AVUTOXPortal.TransactionData, function (index, value) {

                if (value.Id.trim() == id) {
                    tran = value;
                    return value;
                }

            });

            if (tran == null) {
                return;
            }
            var html = "<div style='width:100%;' class='shadow' ><table id='tblQuickview' class='Quickview' width='100%' border='1'><thead><tr><td style='width:5%;font-weight:bold;'>Type</td><td style='width:9%;font-weight:bold;'>Claim# / Trace#</td><td style='width:12%;font-weight:bold;'>Sub.Name</td><td style='width:8%;font-weight:bold;'>Sub.ID</td ><td style='width:10%;font-weight:bold;'>Pat.Name</td><td style='width:8%;font-weight:bold;'>Bill.Prov.ID</td><td style='width:6%;font-weight:bold;'>Payer ID</td><td style='width:4%;font-weight:bold;'>Amt.</td><td style='width:6%;font-weight:bold;'>DOS</td><td style='width:5%;font-weight:bold;'>Status</td><td style='width:8%;font-weight:bold;'>CH Response</td><td style='width:7%;font-weight:bold;'>Post.Status</td><td style='width:7%;font-weight:bold;'>Pay.Status</td><td align='center' style='width:8%;font-weight:bold;text-align:center'>TFL</td></tr></thead>";
            console.dir(tran);
            $.each(tran.ListOfMessages, function (index, value) {

                var ota1 = value.ClaimStatus
                if (ota1 != null) {
                    var ta1Icon

                    if (ota1 == "A") ta1Icon = "<i class='icon-checkmark'; title='Accepted'; style='color:green;font-size:10px'></i>";
                    else if (ota1 == "R") ta1Icon = "<i class='icon-cancel-2'; title='Rejected'; style='color:red;font-size:10px;cursor:pointer;'></i>";
                    else ta1Icon;
                    
                    var oPostedToPayer = value.PostedToPayer
                    var PostedToPayerIcon = "";
                    if (oPostedToPayer == "Y") PostedToPayerIcon = "<i class='icon-checkmark postedToPayer'; data-claimRecID=" + value.ClaimRecID + " data-stRecID=" + value.STRecID + " data-transactionid=" + value.transid + "  data-claimNumber=" + value.ClaimNumber + " title='Claim generated to payer'; style='color:green;font-size:10px;align:center;cursor:pointer;'></i>";
                    else if (oPostedToPayer == "N") PostedToPayerIcon = "<i class='icon-cancel-2'; title='Claim not generated to payer'; style='color:red;font-size:10px;align:center'></i>";
                    else PostedToPayerIcon;

                    var oPayerResponseStatus = value.PayerResponseStatus
                    var PayerResponseStatusIcon = "";
                    if (oPayerResponseStatus == "Y") PayerResponseStatusIcon = "<i class='icon-checkmark payerResponse'; data-claimRecID=" + value.ClaimRecID + " data-stRecID=" + value.STRecID + " data-transactionid=" + value.transid + "  data-claimNumber=" + value.ClaimNumber + " title='Response received from payer'; style='color:green;font-size:10px;cursor:pointer;'></i>";
                    else if (oPayerResponseStatus == "N") PayerResponseStatusIcon = "<i class='icon-cancel-2'; title='Response not received from payer'; style='color:red;font-size:10px'></i>";
                    else PayerResponseStatusIcon;

                    var oFillingLetters = value.PostedToPayer
                    var TimelyFillingLetters = "";
                    if (oFillingLetters == "Y") TimelyFillingLetters = "<i class='icon-file-pdf TimelyFilling'; data-claimRecID=" + value.ClaimRecID + " data-stRecID=" + value.STRecID + " data-transactionid=" + value.transid + "  data-claimNumber=" + value.ClaimNumber + " style='align:center;cursor:pointer;color:red;font-size:15px'></i>";
                    else if (oFillingLetters == "N") TimelyFillingLetters = "";
                    else TimelyFillingLetters;


                    var oIndivitualRes = value.transid
                    var IndivitualResIcon = "";
                    if (oIndivitualRes == "") IndivitualResIcon = "<i class='icon-cancel-2'; style='color:red;font-size:10px'></i>"
                    else IndivitualResIcon = "<i class='icon-checkmark IndivitualRes' style='color:green;font-size:10px;cursor:pointer;' data-claimRecID=" + value.ClaimRecID + " data-stRecID=" + value.STRecID + " data-transactionid=" + value.transid + "  data-claimNumber=" + value.ClaimNumber + " ></i>";
                    
                    

                    // html = html + "<tr><td>" + value.ClaimType + "<td><td>" + value.ClaimNumber + "<td><td>" + value.SubscriberLastName + " " + value.SubscriberFirstName + "</td><td>" + value.SubscriberId + "</td><td>" + value.PatientLastName + " " + value.PatientFirstName + "</td><td>" + value.BillingNPI + "</td><td>" + value.PayerId + "</td><td>" + value.ClaimAmount + "</td><td>" + value.DOS + "</td><td>" + ta1Icon + "</td><td><i class='icon-file-4 IndivitualRes' style='font-size:20px' data-claimRecID=" + value.ClaimRecID + " data-stRecID=" + value.STRecID + " data-transactionid=" + value.transid + " ></i></td><td><i class='icon-file-4 postedToPayer' style='font-size:20px' data-claimRecID=" + value.ClaimRecID + " data-stRecID=" + value.STRecID + " data-transactionid=" + value.transid + "  data-claimNumber=" + value.ClaimNumber + " ></i></td><td><i class='icon-file-4 payerResponse' style='font-size:20px' data-claimRecID=" + value.ClaimRecID + " data-stRecID=" + value.STRecID + " data-transactionid=" + value.transid + "  data-claimNumber=" + value.ClaimNumber + " ></i></tr>";
                    //html = html + "<tr><td>" + value.ClaimType + "</td><td>" + value.ClaimNumber + "</td><td>" + value.SubscriberLastName + " " + value.SubscriberFirstName + "</td><td>" + value.SubscriberId + "</td><td>" + value.PatientLastName + " " + value.PatientFirstName + "</td><td>" + value.BillingNPI + "</td><td>" + value.PayerId + "</td><td>" + value.ClaimAmount + "</td><td>" + value.DOS + "</td><td style='text-align:center'>" + ta1Icon + "</td><td style='text-align:center'>" + IndivitualResIcon + "</td><td style='text-align:center'>" + PostedToPayerIcon + "</td><td style='text-align:center'>" + PayerResponseStatusIcon + "</td><td style='text-align:center'>" + TimelyFillingLetters + "</td></tr>";

                    var claimNumberLink = "";
                    if (value.ClaimNumber != "") {
                        if(value.ch_TraceNo!="")
                            claimNumberLink = "<a data-claim-number='" + value.ClaimNumber + "' data-transactionid='" + value.transid + "' class='viewClaim' style='cursor:pointer;' title='ViewClaim'>" + value.ClaimNumber + "/" + value.ch_TraceNo + "</a>"
                        else
                            claimNumberLink = "<a data-claim-number='" + value.ClaimNumber + "' data-transactionid='" + value.transid + "' class='viewClaim' style='cursor:pointer;' title='ViewClaim'>" + value.ClaimNumber + "</a>"
                    }

                    html = html + "<tr><td>" + value.ClaimType + "</td><td>" + claimNumberLink + "</td><td>" + value.SubscriberLastName + " " + value.SubscriberFirstName + "</td><td>" + value.SubscriberId + "</td><td>" + value.PatientLastName + " " + value.PatientFirstName + "</td><td>" + value.BillingNPI + "</td><td>" + value.PayerId + "</td><td>" + value.ClaimAmount + "</td><td>" + value.DOS + "</td><td style='text-align:center'>" + ta1Icon + "</td><td style='text-align:center'>" + IndivitualResIcon + "</td><td style='text-align:center'>" + PostedToPayerIcon + "</td><td style='text-align:center'>" + PayerResponseStatusIcon + "</td><td style='text-align:center'>" + TimelyFillingLetters + "</td></tr>";
                }
                else
                    html = "<tr><td>No Records found</td></tr>";

            });
            html = html + "</table></div>";
            oHtml = html;
            AVUTOXPortal.TransactionTable.fnOpen($tr, oHtml, "info_row");

        }
    });
    $('#searchButton').click(function () {

        var fromDate = $("#postedDateFrom").val();
        var toDate = $("#postedDateTo").val();
        if (Date.parse(toDate) < Date.parse(fromDate)) {
            $.Notify({
                caption: "Posted Date",
                content: "Invalid Date Range!",

            });
            return false;
        }

        AVUTOXPortal.TransactionTable.fnDraw(true);
    });
   
    $('#ddlPayerList').on('change',function () {

        var fromDate = $("#postedDateFrom").val();
        var toDate = $("#postedDateTo").val();
        if (Date.parse(toDate) < Date.parse(fromDate)) {
            $.Notify({
                caption: "Posted Date",
                content: "Invalid Date Range!",

            });
            return false;
        }

        AVUTOXPortal.TransactionTable.fnDraw(true);
    });


    $('#cancelButton').click(function () {
        $('input:text').each(function (n, element) {
            if ($(element).val() != '') {
                $(element).val('')
            }
        });
        $('input:radio').each(function (n, element) {
            if ($(element).val() != '') {
                $(element).prop("checked", false);
            }
        });

        //to reset the dropdown..
        $("#ddlPayerList").val('');
        $('#ddlPayerList').trigger("chosen:updated");

        //This method is need to change...
        var aDate = new Date();
        var sDate = (aDate.getMonth() + 1) + "/" + (aDate.getDate()) + "/" + aDate.getFullYear();
        var postedDate = sDate + ' to ' + sDate;
        $('#postedDateFrom').val(sDate);
        $('#postedDateTo').val(sDate);
        AVUTOXPortal.TransactionTable.fnDraw(true);
        //$("#tblResult").dataTable().fnDestroy();

    });

};

AVUTOXPortal.resolveUrl = function (url) {
    var appRootUrl = $('body').attr('data-baseurl');

    var resolved = url;
    if (url.charAt(0) == '~')
        resolved = appRootUrl + url.substring(2);
    return resolved;
};

$(function () {

   
    $("[id='ediVersion5010A2']").bootstrapSwitch();
    $("[id='ediVersion4010A1']").bootstrapSwitch();

    $("[id='chkClaim']").bootstrapSwitch();
    $("[id='chkPreD']").bootstrapSwitch();

    $("[id='typeProd']").bootstrapSwitch();
    $("[id='typeTest']").bootstrapSwitch();

    var aDate = new Date();
    var sDate = (aDate.getMonth()+1) + "/" + (aDate.getDate()) + "/" + aDate.getFullYear();
    
    var hPosteddate = $('#hidPosteddate').val();
    if (hPosteddate.length > 0) {
        sDate = hPosteddate;
    }

    $("#datePickerFrom").datepicker(
        {
            date: sDate, // set init date
            format: "mm/dd/yyyy",
            effect: "slide", // none, slide, fade
            position: "bottom", // top or bottom,
            locale:'en'

        }
       );
    $("#datePickerTo").datepicker(
        {
            date: sDate, // set init date
            format: "mm/dd/yyyy", // set output format
            effect: "slide", // none, slide, fade
            position: "bottom" // top or bottom,
        }
       );

    //#4591	Default date in vendor portal is incorrect - Initialized Date in page load

    $("#postedDateFrom").val(sDate);
    $("#postedDateTo").val(sDate);
    
    //US-3.1  - Search options need to be implemented in top search panel - payername search added by comments
    AVUTOXPortal.AssignPayerList();
    $(".chzn-select").chosen({
        //no_results_text: "Oops, nothing found!",
        allow_single_deselect: true,
    });

    AVUTOXPortal.BuildTransactionTable();
    

    $('#tblResult tbody').on('click', '.CHRes', function () {
        var transactionId = $(this).attr("data-transactionid");
        AVUTOXPortal.FetchCHResponseFile(transactionId);
    });

    $('#tblResult tbody').on('click', '.TA1Status', function (e) {
        var transactionId = $(this).attr("data-transactionid");
        //AVUTOXPortal.FetchTA1Status(transactionId,e);
        $.ajax({
            type: 'POST',
            url: AVUTOXPortal.resolveUrl('~/Transaction/GetTA1Status'),
            dataType: "json",
            async: false,
            data: JSON.stringify({ 'TransactionID': transactionId }),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                //var html = $('#displayResultTemplate').render({ data: data }); //commented this and added the table. since jsrender error occured..
                var html = '<table class="gridtable">'
                html = html +'<caption style="font-size: small">Interchange Acknowledgment</caption>'
                html = html +'<thead><tr></tr><tr>'
                html = html +'<th>Status</th>'
                html = html +'<td>Rejected</td>'
                html = html +'</tr></thead><tbody><tr><th>Reason</th>'
                html = html +'<td>'+data.description+'</td>'
                html = html +'</tr></tbody></table>'
                $.Dialog({
                    shadow: true,
                    overlay: false,
                    title: 'Transaction Rejection Details',
                    width: 300,
                    padding: 8,
                    content: html
                    //onShow: function (_dialog) {
                    //    var content = _dialog.children('.content');
                    //    content.html('Set content from event onShow');
                    //}
                });
            }
        });
    });


    $('#tblResult tbody').on('click', '.Acknowledge', function (e) {
        var transactionId = $(this).attr("data-transactionid");
        //AVUTOXPortal.FetchTA1Status(transactionId,e);
        $.ajax({
            type: 'POST',
            url: AVUTOXPortal.resolveUrl('~/Transaction/GetAcknowledgmentHTML'),
            dataType: "json",
            async: false,
            data: JSON.stringify({ 'TransactionID': transactionId }),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                var description = data.description;
                var style = "width: 600px;font-size: 13px;min-height: 400px;height: 500px;padding: 1px;margin-right: 17px;";
                var html = "<div style='" + style + "'>";
                html = html + description;
                html = html + "</div>";
               
                $.Dialog({
                    shadow: true,
                    overlay: false,
                    title: 'Transaction Rejection Details',
                    width: 250,
                    html:true,
                    content: html
                });
            }
        });
    });
    $('#tblResult tbody').on('click', '.Summary', function (e) {
        var transactionId = $(this).attr("data-transactionid");
        var fileName = $(this).attr("data-FileName");
        e.preventDefault();
          $.ajax({
            type: 'POST',
            url: AVUTOXPortal.resolveUrl('~/Transaction/ShowSummaryReport'),  
            dataType: "json",
            async: false,
            data: JSON.stringify({ 'TransactionID': transactionId }),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                var description = data.description;
                var style = "width: 1200px;font-size: 13px;min-height: 400px;height: 600px;overflow:scroll;padding: 1px;margin-right: 17px;";
                var html = "<div style='" + style + "'>";
                html = html + description;
                html = html + "</div>";
                html = html + '<div><input type=submit class=button polaroid bd-white shadow id=downloadSR style=visibility:hidden; value=Download /></div>'

                $.Dialog({
                    shadow: true,
                    overlay: false,
                    html: true,
                    title: 'Summary Report for :' + fileName,
                    width: 650,
                    content: html,
                    onShow: function () {
                        $('#downloadSR').on('click', function (e) {
                            e.preventDefault();
                            window.location.href = AVUTOXPortal.resolveUrl("~/Transaction/getSumarryReport") + "?TransactionID=" + transactionId + "&FileName=" + fileName;
                            return false;
                        });
                    }
                });
            }
        });
        return false;
    });


    $('#tblResult tbody').on('click', '.IndivitualRes', function () {
        var transactionid = $(this).attr("data-transactionid");
        var claimRecID = $(this).attr("data-claimRecID");
        var stRecID = $(this).attr("data-stRecID");
        var claimNumber = $(this).attr("data-claimNumber");
        //window.location.href = AVUTOXPortal.resolveUrl("~/Transaction/DownloadCHResponse") + "?transID=" + transactionid + "&claimRecID=" + claimRecID + "&STRecID=" + stRecID;
        $.ajax({
            type: 'POST',
            url: AVUTOXPortal.resolveUrl('~/Transaction/ShowCHResponse'),  //Ticket # 4437 - MP-US-37 - Macpractice - Transaction details (insted of download modal popup used for show x12)
            dataType: "json",
            async: false,
            data: JSON.stringify({ 'transID': transactionid, 'claimRecID': claimRecID, 'STRecID': stRecID }),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                var description = data.description;
                var style = "width: 800px;font-size: 13px;min-height: 200px;height: 200px;overflow:scroll;padding: 1px;margin-right: 1px;";
                var html = "<div style='" + style + "'>";
                html = html + description;
                html = html + "</div>";
                html = html + '<div><input type=submit class=button polaroid bd-white shadow id=downloadCHResponse style=visibility:hidden; value=Download /></div>'
                $.Dialog({
                    shadow: true,
                    overlay: false,
                    html: true,
                    title: 'CH Response : ' + claimNumber,
                    content: html,
                    onShow: function () {
                        $('#downloadCHResponse').on('click', function (e) {
                            e.preventDefault();
                            window.location.href = AVUTOXPortal.resolveUrl("~/Transaction/DownloadCHResponse") + "?transID=" + transactionid + "&claimRecID=" + claimRecID + "&STRecID=" + stRecID;
                            return false;
                        });
                    }
                });
                
            }
        });
        return false;
    });

    $('#tblResult tbody').on('click', '.CHPosted', function (e) {
        var transactionId = $(this).attr("data-transactionid");
        var fileName = $(this).attr("data-FileName");
        e.preventDefault();
        $.ajax({
            type: 'POST',
            url: AVUTOXPortal.resolveUrl('~/Transaction/ShowX12'),   
            dataType: "json",
            async: false,
            data: JSON.stringify({ 'TransactionID': transactionId }),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                var description = data.description;
                var style = "width: 600px;font-size: 13px;min-height: 600px;height: 600px;overflow:scroll;padding: 2px;margin-right: 16px;";
                var html = "<div style='" + style + "'>";
                html = html + description;
                html = html + "</div>";
                html = html + '<div><input type=submit class=button polaroid bd-white shadow id=downloadCHPosted style=visibility:hidden; value=Download /></div>'

                $.Dialog({
                    shadow: true,
                    overlay: false,
                    html:true,
                    title: fileName,
                    content: html,
                    onShow: function () {
                        $('#downloadCHPosted').on('click', function (e) {
                            e.preventDefault();
                            window.location.href = AVUTOXPortal.resolveUrl("~/Transaction/RequestDownload") + "?TransactionID=" + transactionId + "&FileName=" + fileName;
                            return false;
                        });
                    }
                });
            }
        });
        return false;
    });


    
    

    $('#tblResult tbody').on('click', '.TimelyFilling', function (e) {


   
        var transactionId = $(this).attr("data-transactionid");
        var claimRecID = $(this).attr("data-claimRecID");
        var stRecID = $(this).attr("data-stRecID");
        e.preventDefault();
        window.location.href = AVUTOXPortal.resolveUrl("~/Transaction/TimelyFillingDownload") + "?TransactionID=" + transactionId + "&claimRecID=" + claimRecID + "&STRecID=" + stRecID;
        return false
    });
    
   
    $('#tblResult tbody').on('click', '.postedToPayer', function (e) {
        var transactionId = $(this).attr("data-transactionid");
        var claimRecId = $(this).attr("data-claimRecID");
        var stRecId = $(this).attr("data-stRecID");
        var claimNumber = $(this).attr("data-claimNumber");
        var fileName = claimNumber + ".txt";
        e.preventDefault();
        $.get(AVUTOXPortal.resolveUrl("~/Transaction/CheckForPayerResponse") + "?transactionId=" + transactionId + "&claimRecId=" + claimRecId + "&stRecId=" + stRecId + "&fileName=" + fileName, function (data) {
            if (data.IsValid) {
                $.ajax({  
                    type: 'POST',
                    url: AVUTOXPortal.resolveUrl('~/Transaction/PostedToPayerX12'),
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({ 'transactionId': transactionId, 'claimRecId': claimRecId, 'stRecId': stRecId }),
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        var description = data.description
                        var style = "width: 530px;font-size: 13px;min-height: 500px;height: 600px;overflow:auto;padding: 2px;margin-right: 16px;";
                        var html = "<div style='" + style + "'>";
                        html = html + description;
                        html = html + "</div>";
                        html = html + '<div><input type=submit class=button polaroid bd-white shadow id=downloadPostedPayer style=visibility:hidden; value=Download /></div>'

                        $.Dialog({
                            shadow: true,
                            overlay: false,
                            html: true,
                            title: 'Posted to payer :' + claimNumber,
                            content: html,
                            onShow: function () {
                                $('#downloadPostedPayer').on('click', function (e) {
                                    e.preventDefault();
                                    window.location.href = AVUTOXPortal.resolveUrl("~/Transaction/DownloadPostedToPayerResponse") + "?transactionId=" + transactionId + "&claimRecId=" + claimRecId + "&stRecId=" + stRecId + "&fileName=" + fileName;
                                    return false;
                                });
                            }
                        });
                    }
                });
            } else {
                $.Notify({
                    content: data.ErrorMessage,
                    icon: '<i class=icon - info - 2></i>',
                });
            }
    });
    return false
    });


    $('#tblResult tbody').on('click', '.payerResponse', function (e) {

        var transactionId = $(this).attr("data-transactionid");
        var claimRecId = $(this).attr("data-claimRecID");
        var stRecId = $(this).attr("data-stRecID");
        var claimNumber = $(this).attr("data-claimNumber");
        var returnData = ''
        $.ajax({
            type: 'POST',
            url: AVUTOXPortal.resolveUrl('~/Transaction/GetPayerStatus'),
            dataType: "json",
            async: false,
            data: JSON.stringify({ 'transactionId': transactionId, 'claimRecId': claimRecId, 'stRecId': stRecId }),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data != "") {
                    var html = '<div style="height:400px;overflow:auto;">';
                   
                    for (var i = 0, len = data.length; i < len; ++i) {
                        html += '<table class="gridtable"><caption style="align:left"><b>Claim Number :</b>' + claimNumber + '</caption><th>Response Date</th><th>Payer Status</th><th>Category</th><th>STC12 Message</th>';
                        html += '<tr>';
                        html += '<td>' + data[i].ResponseDate + '</td><td>' + data[i].Category + '</td><td>' + data[i].Status + '</td><td>' + data[i].Stc12Value + '</td>';
                        html += '</tr>';
                        if (data[i].LinelevelRejectionMessage.length > 0) {

                            html += '<tr>';
                            html += '<td colspan=4>';
                            html += '<table class="gridtable"><caption style="align:left"><b>Service Line Rejections</b></caption>';
                            html += '<th>ProcedureCode</th><th>Total Charges($)</th><th>Total Paid($)</th><th>Remarks</th>';
                            for (var rejection = 0; rejection < data[i].LinelevelRejectionMessage.length; rejection++) {
                                html += '<tr>';
                                html += '<td>' + data[i].LinelevelRejectionMessage[rejection].ProcedureCode + '</td>';
                                html += '<td>' + parseFloat(data[i].LinelevelRejectionMessage[rejection].TotalChargeAmount).toFixed(2) + '</td>';
                                html+='<td>' + parseFloat(data[i].LinelevelRejectionMessage[rejection].TotalPaidAmount).toFixed(2) + '</td>';
                                html+='<td>' + data[i].LinelevelRejectionMessage[rejection].LineRemarks + '</td>';
                                html += '</tr>';
                                }

                            html += '</table></td></tr>';

                        }
                        }
                    
                    html += '</table></div></br>';
                    returnData = html;
                    var dialog = $.Dialog({
                        shadow: true,
                        overlay: false,
                        html: true,
                        title: 'Payer Response Details',
                        container: 'body',
                        content: html,
                        position: 'center'

                    }).css('width', '800px');;
                    dialog.css('margin-left', '300px');
                }
                else {
                    $.Notify({
                        content: "No response received from payer!! for the claim #" + claimNumber,
                    });
                }
            }
        });

        //$(this).popover({
        //    html:true,
        //    placement: 'bottom',
        //    toggle: "popover",
        //    content: returnData,
        //   // title: '<span class="text-info"><strong>title!!</strong></span> <button type="button" id="close" class="close">&times;</button></span>',
        //    container: 'body',
        //    template: '<div class="popover special-class"><div class="arrow"></div><div class="popover-inner"><h3 class="popover-title"></h3><div class="popover-content"><p></p></div></div></div>'
        //}).popover('show');

    });






    // Commanded the Model Escape issue

    //$(document).keyup(function (e) {
    //    if (e.keyCode == 27) {
    //      //  $.Dialog.opened = true;

    //        var _overlay = METRO_DIALOG.parent(".window-overlay");
    //        _overlay.fadeOut(function () {
    //            $(this).remove();
    //            return false;
    //        });
    //    }

    //});


});
