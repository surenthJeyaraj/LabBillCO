$(document).ready(function () {
    var AVUTOXPortal = AVUTOXPortal || {};
    AVUTOXPortal.Settings = AVUTOXPortal.Settings || {};
    AVUTOXPortal.User = AVUTOXPortal.User || {};
    //Session time out in seconds
    AVUTOXPortal.Settings.SessionTimeOut = 1160; // based on configuration
    var claimId, remittanceId;
    AVUTOXPortal.identifyPage = function () {
        var params = (new URL(location)).searchParams;
        remittanceId = params.get('RemittanceId');
        claimId = params.get('ClaimId');
        if (claimId != null && remittanceId != null) {
            getClaimServices(remittanceId, claimId);
        }
        else if (claimId == null && remittanceId != null) {
            getRemittanceClaims(remittanceId);
        }
        else if (claimId == null && remittanceId == null) {
            getRemittances();
        }
    }
    AVUTOXPortal.resolveUrl = function (url) {
        var appRootUrl = $('body').attr('data-baseurl');

        var resolved = url;
        if (url.charAt(0) == '~')
            resolved = appRootUrl + url.substring(2);
        return resolved;
    };
    AVUTOXPortal.identifyPage();
    function resolveUrl(url) {
        var appRootUrl = $('body').attr('data-baseurl');

        var resolved = url;
        if (url.charAt(0) == '~')
            resolved = appRootUrl + url.substring(2);
        return resolved;
    }

    var htmlToAppend = '';
    var htmlToAppend = '';
    var data = $.ajax({
        type: 'POST',
        url: resolveUrl('~/Transaction/GetPayerList'),
        dataType: "json",
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            htmlToAppend += '<option value=""></option>';
            for (var j = 0; j < data.length; j++) {
                htmlToAppend += '<option value="' + data[j].PayerId + '">' + data[j].PayerName + '</option>';
            }
            $('#PayerName').append(htmlToAppend);
        }
    });

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


    $("#btnSearch").click(function () {
        var searchFilters = $('form[name=formRemittanceFilters]').serializeArray();
        console.log(searchFilters);
        var aaData = {};
        $.each(searchFilters, function (index, data) {
            aaData[data.name] = data.value
        });
        var isValidFilters = validateRemittanceFilterForm();
        if (isValidFilters) {
            getRemittances(aaData);
        }
    });
    function validateRemittanceFilterForm() {
        var isValidForm = doValidate('formRemittanceFilters', '#VALIDATION_MESSAGES', "FORM_REMITTANCE_FILTER");
        console.log(isValidForm);
        return false;
    }

    function getRemittances(searchFilters) {
        console.log(searchFilters);
        $.ajax({
            type: 'POST',
            url: resolveUrl('~/remittance/getremittances'),
            dataType: "json",
            async: false,
            //  data: JSON.stringify({ 'BillingLocationName': '12334','BillingLocationName':'Dos Cliic','FileNumber':'123','PaidAmount':'12.00','ReceivedDateTimeFrom':'12/01/2017','ReceivedDateTimeTo':'02/21/2018','PayerName':'test','IdetificationCode':'222'}),
            data: JSON.stringify(searchFilters),
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                $('#ajax_loader').show();
            },
            complete: function () {
                $('#ajax_loader').hide();
            },
            success: function (data) {
                renderRemittanceTbl(data.aaData);
            }
        });
    }
    function renderRemittanceTbl(remittanceData) {
        var oTable = $("#tblRemittance").dataTable({
            "sDom": "<'dataTable-header-wrapper'fpl>rt<'dataTable-footer-wrapper'ip>",
            "oLanguage": {
                "sInfo": "Showing _START_ to _END_ of _TOTAL_ records",
                "sLengthMenu": "Show _MENU_ records",
                "sEmptyTable": "No records found!"
            },
            //"bDestroy": true,
            "bRetrieve": true,
            "bFilter": true,
            "bJQueryUI": true,
            //"bProcessing": true,
            //"bDeferRender": true,
            "sPaginationType": "full_numbers",
            "aaData": remittanceData,
            //"bJQueryUI": true,
            //"bServerSide": true,
            //"sAjaxSource": resolveUrl("~/Remittance/getremittances"),
            //"fnServerParams": pushServerParameters,
            "bSortable": false,
            "bAutoWidth": false,
            "aoColumnDefs": [
                {
                    "bSortable": false,
                    "aTargets": [0],
                    "mData": "RemittanceId",
                    "mRender": function (data, type, full) {
                        return "<a href='" + AVUTOXPortal.resolveUrl('~/Remittance/Claims?RemittanceId=' + full["Id"]) + "' class='checkNumber' data-id='" + full["Id"] + "'>" + data + "</a>";
                    }
                },
                {
                    "bSortable": false,
                    "aTargets": [1],
                    "mData": "PayerName"
                },
                {
                    "bSortable": false,
                    "aTargets": [2],
                    "mData": "PayeeName"
                },
                {
                    "bSortable": false,
                    "aTargets": [3],
                    "mData": "BillingTaxId"
                },
                {
                    "bSortable": false,
                    "aTargets": [4],
                    "mData": "PaidAmount",
                    "mRender": function (data, type, full) {
                        return parseFloat(data, 10).toFixed(2).toString();
                    }
                },
                {
                    "bSortable": false,
                    "aTargets": [5],
                    "mData": "NumberOfClaimsPaid"
                },
                {
                    "bSortable": false,
                    "aTargets": [6],
                    "mData": "ReceivedDateTime",
                    "mRender": function (data, type, full) {
                        var receivedDate = data;
                        var r = /\/Date\(([0-9]+)\)\//i;
                        var matches = receivedDate.match(r);
                        if (matches.length == 2) {
                            var date = new Date(parseInt(matches[1]));
                            return (date.getMonth() + 1) + "/" + (date.getDate()) + "/" + date.getFullYear();
                        }
                        else {
                            return receivedDate;
                        }
                    }
                },
                //{
                //    "bSortable": false,
                //    "aTargets": [7],
                //    "mData": "Id",
                //    "mRender": function (data, type, full) {
                //        return "<a href=" + resolveUrl("~/remittance/downloadpdfreport?remittanceId=" + data) + "><i class='icon-download-2'></i></a>";
                //    }
                //}
            ]
        });
    }
    function getRemittanceClaims(remittanceId) {
        $.ajax({
            type: 'GET',
            url: resolveUrl('~/remittance/GetClaims?RemittanceId=' + remittanceId),
            dataType: "json",
            async: true,
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                $('#ajax_loader').show();
            },
            complete: function () {
                $('#ajax_loader').hide();
            },
            success: function (data) {
                renderClaimsTbl(data.aaData);
            }
        });
        $("#goBack").attr('href', AVUTOXPortal.resolveUrl('~/Remittance/index'));
    }
    function renderClaimsTbl(claimsData) {
        var cTable = $("#tblClaims").dataTable({
            "sDom": "<'dataTable-header-wrapper'fpl>rt<'dataTable-footer-wrapper'ip>",
            "oLanguage": {
                "sInfo": "Showing _START_ to _END_ of _TOTAL_ records",
                "sLengthMenu": "Show _MENU_ records",
                "sEmptyTable": "No records found!"
            },
            "sPaginationType": "full_numbers",
            "bJQueryUI": true,
            "bFilter": true,
            //"bProcessing": true,
            //"bDeferRender": true,
            //"bServerSide": true,
            //"sAjaxSource": resolveUrl("~/remittance/GetClaims"),
            "aaData": claimsData,
            //"fnServerParams": pushServerParameters,
            "bSortable": false,
            "bAutoWidth": false,
            "aoColumnDefs": [
                {
                    "bSortable": false,
                    "aTargets": [0],
                    "mData": "ClaimNumber",
                    "mRender": function (data, type, full) {
                        return "<a href='" + AVUTOXPortal.resolveUrl('~/Remittance/Services?RemittanceId=' + remittanceId + '&&ClaimId=' + full["ClaimNumber"]) + "'  data-id='" + full["Id"] + "'>" + data + "</a>";
                    }
                },
                {
                    "bSortable": false,
                    "aTargets": [1],
                    "mData": "PatientLastName",
                    "mRender": function (data, type, full) {
                        return full["PatientLastName"] + " " + full["PatientFirstName"];
                    }
                    //"mRender": "PatientLastName" + "PatientLastName"
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
        $.ajax({
            type: 'GET',
            url: resolveUrl('~/remittance/GetService?RemittanceId=' + remittanceId + '&ClaimId=' + claimId),
            dataType: "json",
            async: true,
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                $('#ajax_loader').show();
            },
            complete: function () {
                $('#ajax_loader').hide();
            },
            success: function (data) {
                renderServicesTbl(data.aaData);
            }
        });
        $("#goBack").attr('href', AVUTOXPortal.resolveUrl('~/Remittance/Claims?RemittanceId=' + remittanceId));
    }
    function renderServicesTbl(serviceData) {
        var sTable = $("#tblServices").dataTable({
            "sDom": "<'dataTable-header-wrapper'fpl>rt<'dataTable-footer-wrapper'ip>",
            "oLanguage": {
                "sInfo": "Showing _START_ to _END_ of _TOTAL_ records",
                "sLengthMenu": "Show _MENU_ records",
                "sEmptyTable": "No records found!"
            },
            "sPaginationType": "full_numbers",
            "bJQueryUI": true,
            "bFilter": true,
            //"bProcessing": true,
            //"bDeferRender": true,
            //"bServerSide": true,
            //"sAjaxSource": resolveUrl("~/remittance/getremittances"),
            "aaData": serviceData,
            //"fnServerParams": pushServerParameters,
            "bSortable": false,
            "bAutoWidth": false,
            "aoColumnDefs": [
                {
                    "bSortable": false,
                    "aTargets": [0],
                    "mData": "ProcedureCode"
                },
                {
                    "bSortable": false,
                    "aTargets": [1],
                    "mData": "SubmittedAmount",
                    "mRender": function (data, type, full) {
                        var SubmittedAmount = data.Value;
                        return SubmittedAmount;
                    }
                },
                {
                    "bSortable": false,
                    "aTargets": [2],
                    "mData": "SubmittedAmount",
                    "mRender": function (data, type, full) {
                        var SubmittedAmount = "0.00";
                        if (data == null)
                        { return SubmittedAmount }
                        else
                            return SubmittedAmount = data.Value;
                    }

                },
                {

                    "bSortable": false,
                    "aTargets": [3],
                    "mData": "ProcedureDate",
                    "mRender": function (data, type, full) {
                        var receivedDate = data;
                        var r = /\/Date\(([0-9]+)\)\//i;
                        var matches = receivedDate.match(r);
                        if (matches.length == 2) {
                            var date = new Date(parseInt(matches[1]));
                            return (date.getMonth() + 1) + "/" + (date.getDate()) + "/" + date.getFullYear();
                        }
                        else {
                            return receivedDate;
                        }
                    }

                },
                {
                    "bSortable": false,
                    "aTargets": [4],
                    "mData": "ReasonCodes"
                },
                {
                    "bSortable": false,
                    "aTargets": [5],
                    "mData": "AllowedAmount",
                    "mRender": function (data, type, full) {
                        var allowedAmount = "0.00";
                        if (data == null)
                        { return allowedAmount }
                        else
                            return allowedAmount = data.Value;
                    }


                },
                {
                    "bSortable": false,
                    "aTargets": [6],
                    "mData": "UnitsPaid"
                }
            ]
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




    $("#btnClear").click(function () {
        clearForm();
    });
});