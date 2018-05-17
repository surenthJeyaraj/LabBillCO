$(document).ready(function () {
    var AVUTOXPortal = AVUTOXPortal || {};
    AVUTOXPortal.Settings = AVUTOXPortal.Settings || {};
    AVUTOXPortal.User = AVUTOXPortal.User || {};
    //Session time out in seconds
    AVUTOXPortal.Settings.SessionTimeOut = 1160; // based on configuration
    var claimId, remittanceId;
    AVUTOXPortal.GetRemittanceId = function () {
        var params = (new URL(location)).searchParams;
        remittanceId = params.get('RemittanceId');
        if (remittanceId != null) {
            getRemittanceClaims(remittanceId);
        }
    };
    AVUTOXPortal.GetClaimId = function () {
        var params = (new URL(location)).searchParams;
        remittanceId = params.get('RemittanceId');
        claimId = params.get('ClaimId');
        if (claimId != null) {
            getClaimServices(remittanceId, claimId);
        }
    };
    AVUTOXPortal.resolveUrl = function (url) {
        var appRootUrl = $('body').attr('data-baseurl');

        var resolved = url;
        if (url.charAt(0) == '~')
            resolved = appRootUrl + url.substring(2);
        return resolved;
    };
   // AVUTOXPortal.GetRemittanceId();
    AVUTOXPortal.GetClaimId();
    function resolveUrl(url) {
        var appRootUrl = $('body').attr('data-baseurl');

        var resolved = url;
        if (url.charAt(0) == '~')
            resolved = appRootUrl + url.substring(2);
        return resolved;
    }

    var htmlToAppend = '';
    

    var aDate = new Date();
    var sDate = (aDate.getMonth() + 1) + "/" + (aDate.getDate()) + "/" + aDate.getFullYear();

    $("#ReceivedDateTimeFrom").val(sDate);
    $("#ReceivedDateTimeTo").val(sDate);

    function pushServerParameters(aoData) {
        var pushDatas = $("form").serializeArray();
        $.each(pushDatas, function (index, data) {
            aoData.push(data);
        });
    };


    //$("#ReceivedDateTimeFrom").val(sDate);
    //$("#ReceivedDateTimeTo").val(sDate);

    function pushServerParameters(aoData) {
        var pushDatas = $("form").serializeArray();
        $.each(pushDatas, function (index, data) {
            aoData.push(data);
        });
    };
     
    function getRemittanceClaims(remittanceId) {
        $("#goBack").attr('href', AVUTOXPortal.resolveUrl('~/Remittance/index'));

        //var claimsData = [
        //    {
        //        "ClaimNumber": "CLP1",
        //        "PatientName": "Will",
        //        "ClaimAmount": "100",
        //        "ChargedAmount": "90",
        //        "PatientId": "PA001",
        //        "Code": "CD0001"
        //    },
        //    {
        //        "ClaimNumber": "CLP2",
        //        "PatientName": "John",
        //        "ClaimAmount": "50",
        //        "ChargedAmount": "80",
        //        "PatientId": "PA002",
        //        "Code": "CD0002"
        //    },
        //    {
        //        "ClaimNumber": "CLP3",
        //        "PatientName": "John",
        //        "ClaimAmount": "50",
        //        "ChargedAmount": "80",
        //        "PatientId": "PA002",
        //        "Code": "CD0002"
        //    }
        //];
        var cTable = $("#tblClaims").dataTable({
            "oLanguage": {
                "sInfo": "Showing _START_ to _END_ of _TOTAL_ records",
                "sLengthMenu": "Show _MENU_ records",
                "sEmptyTable": "No records found!"
            },
            //"sPaginationType": "bootstrap",
            "bJQueryUI": true,
            "bFilter": false,
            "bProcessing": true,
            "bDeferRender": true,
            "bServerSide": true,
            "sAjaxSource": resolveUrl("~/remittance/GetClaims"),
            "aaData": claimsData,
            "fnServerParams": pushServerParameters,
            "bSortable": false,
            "bAutoWidth": false,
            "aoColumnDefs": [
                {
                    "bSortable": false,
                    "aTargets": [0],
                    "mData": "ClaimNumber",
                    "mRender": function (data, type, full) {
                        return "<a href='" + AVUTOXPortal.resolveUrl('~/Remittance/Services?RemittanceId=' + remittanceId + '&&ClaimId=' + data) + "'  data-id='" + full["Id"] + "'>" + data + "</a>";
                    }
                },
                {
                    "bSortable": false,
                    "aTargets": [1],
                    "mData": "PatientName"
                },
                {
                    "bSortable": false,
                    "aTargets": [2],
                    "mData": "ClaimAmount"
                },
                {
                    "bSortable": false,
                    "aTargets": [3],
                    "mData": "PaidAmount"
                },
                {
                    "bSortable": false,
                    "aTargets": [4],
                    "mData": "ClaimStatusCodeDescription"
                }]
        });
    }
    function getClaimServices(remittanceId, claimId) {
        $("#goBack").attr('href', AVUTOXPortal.resolveUrl('~/Remittance/Claims?RemittanceId=' + remittanceId));
        //var serviceData = [{
        //    "ServiceCode": "A1",
        //    "Procedure": "021",
        //    "Amount": "11.00"
        //},
        //{
        //    "ServiceCode": "B1",
        //    "Procedure": "031",
        //    "Amount": "21.00"
        //},
        //{
        //    "ServiceCode": "C1",
        //    "Procedure": "038",
        //    "Amount": "31.00"
        //}];
        var sTable = $("#tblServices").dataTable({
            "oLanguage": {
                "sInfo": "Showing _START_ to _END_ of _TOTAL_ records",
                "sLengthMenu": "Show _MENU_ records",
                "sEmptyTable": "No records found!"
            },
            //"sPaginationType": "bootstrap",
            "bJQueryUI": true,
            "bFilter": false,
            "bProcessing": true,
            //"bDeferRender": true,
            //"bServerSide": true,
            "sAjaxSource": resolveUrl("~/remittance/getremittances"),
            "aaData": serviceData,
            "fnServerParams": pushServerParameters,
            "bSortable": false,
            "bAutoWidth": false,
            "aoColumnDefs": [
                {
                    "bSortable": false,
                    "aTargets": [0],
                    "mData": "ServiceCode"
                },
                {
                    "bSortable": false,
                    "aTargets": [1],
                    "mData": "Procedure"
                },
                {
                    "bSortable": false,
                    "aTargets": [2],
                    "mData": "Amount"
                }]
        });
    }
    function clearForm() {
        var emptyString = "";
        $("#BillingLocationName").val(emptyString);
        $("#ReceivedDateTimeFrom").val(emptyString);
        $("#ReceivedDateTimeTo").val(emptyString);
        $("#ClaimNumber").val(emptyString);
        $("#CheckNumber").val(emptyString);
        $("#PaidAmount").val(emptyString);
        $("#BillingTaxId").val(emptyString);
        $("#PayerName").val('').trigger("chosen:updated");
        oTable.fnDraw();
    }



    $("#btnSearch").click(function () {
        oTable.fnDraw();
    });

    $("#btnClear").click(function () {
        clearForm();
    });
});