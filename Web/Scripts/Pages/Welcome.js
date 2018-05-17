// Use global abatement technique by creating a single global variable for the application
var AVUTOXPortal = AVUTOXPortal || {};
AVUTOXPortal.Settings = AVUTOXPortal.Settings || {};
AVUTOXPortal.User = AVUTOXPortal.User || {};
//Session time out in seconds
AVUTOXPortal.Settings.SessionTimeOut = 1160; // based on configuration

AVUTOXPortal.User.ClearControls = function () {
    $('#UserName').val("");
    $('#UserName').removeAttr("readonly");
    $('#FirstName').val("");
    $('#LastName').val("");
    $('#SelectedRole').val("");
    $('#EmailAddress').val("");
    $('#EmailAddress').removeAttr("readonly");
    $('#UserId').val(0);
    $('.submit').val("Register");
    $('#registerNote').show();
    $(".validation-summary-errors").html("<ul></ul>");
    $(".validation-summary-errors").attr('data-valmsg-summary', true);
    $(".validation-summary-errors").attr('class', 'validation-summary-valid');
    //$(".validation-summary-errors").css("validation-summary-valid");
    $(".input-validation-error").removeClass("input-validation-error");
    $(".action").val(0);
};

AVUTOXPortal.resolveUrl = function (url) {
    var appRootUrl = $('body').attr('data-baseurl');

    var resolved = url;
    if (url.charAt(0) == '~')
        resolved = appRootUrl + url.substring(2);
    return resolved;
};
AVUTOXPortal.GetLabData = function () {
    $.ajax({
        type: 'POST',
        url: AVUTOXPortal.resolveUrl('~/Transaction/GetLabDataList'),
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
            renderUserTbl(data.aaData);
        }
    });
}
function renderUserTbl(tblData) {

    AVUTOXPortal.UserListTable = $('#tblUserList').dataTable({
        "sDom": "<'dataTable-header-wrapper'fpl>rt<'dataTable-footer-wrapper'ip>",
        "oLanguage": {
            //"sInfo":"Got a total of _TOTAL_ entries to show (_START_ to _END_)"
            "sInfo": "Showing _START_ to _END_ of _TOTAL_ records",
            "sLengthMenu": "Show _MENU_ records",
            "sEmptyTable": "No records found!",

        },

        "bFilter": true,
        //"bProcessing": true,
        //"bDeferRender": true,
        "sPaginationType": "full_numbers",
        "bJQueryUI": true,
        //"bServerSide": true,
        //"sAjaxSource": "~/Transaction/GetLabDataList",
        "bSortable": false,
        "bAutoWidth": false,
        "aaData": tblData,
        "fnServerData": function (sSource, aoData, fnCallback) {
            AVUTOXPortal.User.ClearControls();
            $.ajax({
                url: AVUTOXPortal.resolveUrl(sSource),
                type: 'POST',
                contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                dataType: 'json',
                data: aoData,
                success: function (result) {
                    console.log(result);

                    AVUTOXPortal.TransactionData = result["aaData"];
                    fnCallback(result);
                },
            });

        },


        "aoColumns": [
            {
                "mData": "ScanId",
                "sTitle": "Scan ID",
                "bSearchable": true,
                "bSortable": false,
                "sWrap": true,
                "mRender": function (data, type, full) {
                    return '<a href="' + AVUTOXPortal.resolveUrl("~/Transaction/HL7EntryDetails?ScaID=" + full.LabscanGuid + "&RecordStatus=" + full.Status + "&ScanID=" + full.ScanId + "&FileUrl=" + full.FileUrl) + '">' + data + '</a>';
                }
            },
            {
                "mData": "FileName",
                "sTitle": "File Name",
                "sWrap": true,
                "bSortable": false,
            },


            {
                "mData": "AssignedTo",
                "sTitle": "USER NAME",
                "sWrap": true,
                "bSortable": false,
            },
            {
                "mData": "Status",
                "sTitle": "Status",
                "sWrap": true,
                "bSortable": false,
                "mRender": function (data, type, full) {
                    var html = "";
                    if (full['Status'] === 800000003) {
                        html = '<div style=font-weight:bold;color:blue> Open</div>';
                    }
                    else if (full['Status'] === 800000007) {
                        html = '<div style=font-weight:bold;color:Red>Error</div>';
                    } else if (full['Status'] === 800000004) {
                        html = '<div style=font-weight:bold;color:Orange>Pending for Approval</div>';
                    } else if (full['Status'] === 800000006) {
                        html = '<div style=font-weight:bold;color:green>Closed</div>';
                    } else if (full['Status'] === 800000005) {
                        html = '<div style=font-weight:bold;color:green>Processing...</div>';
                    }
                    return html;
                }
            }

        ]


    }).css("width", "100%");


};

AVUTOXPortal.GetAckRection = function () {
    var htmlToAppend = '';
    var data1 = $.ajax({
        url: AVUTOXPortal.resolveUrl('~/Transaction/GetAckRejectionReport'),
        type: 'POST',
        // data: { id: 'value' },
        dataType: "json",
        async: false,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $("#ClaimRejectionReport").html(data.description);
        },
        error: function () {
            alert('error');
        }
    });

};
$(function () {
    AVUTOXPortal.GetLabData();
    // AVUTOXPortal.List();
    //  AVUTOXPortal.GetAckRection();
}
);