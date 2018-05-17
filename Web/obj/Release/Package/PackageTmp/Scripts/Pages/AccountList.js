var AVTOXPortal = AVTOXPortal || {};
AVTOXPortal.Settings = AVTOXPortal.Settings || {};
AVTOXPortal.User = AVTOXPortal.User || {};
AVTOXPortal.User.ClearControls = function() {
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
    $('form').validate().resetForm();
    $(".validation-summary-errors").html("<ul></ul>");
    $(".validation-summary-errors").attr('data-valmsg-summary', true);
    $(".validation-summary-errors").attr('class', 'validation-summary-valid');
    //$(".validation-summary-errors").css("validation-summary-valid");
    $(".input-validation-error").removeClass("input-validation-error");
    $(".action").val(0);
};
AVTOXPortal.UserListTable = {},
    AVTOXPortal.User.Actions = function () {
        var select = $(this).val();
        var parent = $(this).parent().parent();
        switch (select) {
            case "0":
                AVTOXPortal.User.ClearControls();
                break;
            case "1":
                //Edit
                //TR
              
                $('#UserName').val($(parent).find('td:eq(0)').text());
                $('#UserName').attr("readonly", "readonly");
                $('#FirstName').val($(parent).find('td:eq(2)').text());
                $('#LastName').val($(parent).find('td:eq(1)').text() );
                $('#SelectedRole').val($(parent).find('td:eq(3)').text());
                $('#EmailAddress').val($(parent).find('td:eq(4)').text());
                $('#EmailAddress').attr("readonly", "readonly");
                $('#UserId').val($(parent).find('td:eq(6)').find('input[type=hidden]').val());
                //$(this).val(0);
                $('.submit').val("Save");
                $('#registerNote').hide();
                break;
            case "2":
                var r = confirm("Do you want to delete this user?");
                if (r == true) {
                    //AVTOXPortal.User.PerformAction(AVTOXPortal.resolveUrl("~/Account/DeleteUser"), parent, "User successfully deleted.");
                    AVTOXPortal.User.PerformAction(AVTOXPortal.resolveUrl("~/Account/DeleteUser"), parent, "User successfully deleted.", "Delete");
                } else {
                    $(this).val(0);
                }
                break;
            case "3":
                var rstConfirm = confirm("Do you want to reset user password?");
                if (rstConfirm == true) {
                    //AVTOXPortal.User.PerformAction(AVTOXPortal.resolveUrl("~/Account/ResetUserPassword"), parent, "Password reset email successfully sent.");
                    AVTOXPortal.User.PerformAction(AVTOXPortal.resolveUrl("~/Account/ResetUserPassword"), parent, "Password reset email successfully sent.", "ResetPassword");
                } else {
                    $(this).val(0);
                }
                break;
            case "4":
                var gntConfirm = confirm("Do you want to generate confirmation link?");
                if (gntConfirm == true) {
                    //AVTOXPortal.User.PerformAction(AVTOXPortal.resolveUrl("~/Account/GenerateConfirmationLink"), parent, "Confirmation email succesfully sent.");
                    AVTOXPortal.User.PerformAction(AVTOXPortal.resolveUrl("~/Account/GenerateConfirmationLink"), parent, "Confirmation email succesfully sent.", "ResendConfirm");
                } else {
                    $(this).val(0);
                }
                break;
            default:
        }


    
    };
AVTOXPortal.User.PerformAction = function (actionUrl, parent, message, actionName) {
    $('#loader').show();
    $.ajax({
        type: "POST",
        dataType: "json",
        data: { 'userName': $(parent).find('td:eq(0)').text() },
        url: actionUrl,
        success: function (serverData) {
            $('#loader').hide();
            if (serverData.Success == false) {
                $('.validation-summary-valid').parent().html('<div class="validation-summary-errors" data-valmsg-summary="true"><ul><li>' + serverData.ErrorMessage + '</li></ul></div>');
                $('.validation-summary-valid').css('validation-summary-errors');
            }
            if (serverData.Success == true) {
                if (actionName == "Delete" && serverData.IsOwnUser) {
                    window.location.href = AVTOXPortal.resolveUrl('~/Account/LogOff');
                } else {
                    toastr.info(message);
                    AVTOXPortal.UserListTable.fnDraw(true);
                    AVTOXPortal.User.ClearControls();
                }
            }
        },
        error: function(xhr, ajaxOptions, thrownError) {
            $('#loader').hide();
            toastr.error("Process failed: " + thrownError);
        }

    }); 
};
AVTOXPortal.User.List = function () {

    AVTOXPortal.UserListTable = $('#tblUserList').dataTable({
        //Disabled the Common search Filter
        //"sDom": "<'userAcctFilterBar well row col-md-12'f><'userAcctContextFilter col-md-6'l><'pull-right'p>rt<'bottom pull-right'p>",
        "sDom": "<'dataTable-header-wrapper'flp>rt<'dataTable-footer-wrapper'ip>",
        "oLanguage": {
            //"sInfo":"Got a total of _TOTAL_ entries to show (_START_ to _END_)"
            "sInfo": "Showing _START_ to _END_ of _TOTAL_ records",
            "sLengthMenu": "Show _MENU_ records",
            "sEmptyTable": "No records found!",

        },

        "bFilter": true,
        "bProcessing": true,
        "bDeferRender": true,
        "sPaginationType": "full_numbers",
        "bJQueryUI": true,
        "bServerSide": true,
        "sAjaxSource": "~/Account/Listusers",
        "bSortable": false,
        "bAutoWidth": false,
        "fnServerData": function (sSource, aoData, fnCallback) {
            AVTOXPortal.User.ClearControls();
            $.ajax({
                url: AVTOXPortal.resolveUrl(sSource),
                type: 'POST',
                contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                dataType: 'json',
                data: aoData,
                success: function (result) {
                    AVTOXPortal.TransactionData = result["aaData"];
                    fnCallback(result);
                },
            });

        },

        //"fnServerParams": function (aoData) {
        //    aoData.push(
        //    {
        //        name: "userName",
        //        value: ''
        //    }, {
        //        name: "email",
        //        value: ''
        //    });

        //},
        "aoColumns": [
            {
                "mData": "UserName",
                "sTitle": "User Name",
                "bSearchable": false,
                "bSortable": false,
                "sWrap": true,
            },
             {
                 "mData": "LastName",
                 "sTitle": "Last Name",
                 "sWrap": true,
                 "bSortable": false,
             },
            {
                "mData": "FirstName",
                "sTitle": "First Name",
                "sWrap": true,
                "bSortable": false,
            },
           
            {
                "mData": "Role",
                "sTitle": "Role",
                "sWrap": true,
                "bSortable": false,
            },
            {
                "mData": "EmailId",
                "sTitle": "Email",
                "sWrap": true,
                "bSortable": false,
            },
            {
                "mData": "IsConfirmed",
                "sTitle": "Status",
                "sWrap": true,
                "sWidth": "20px",
                "bSortable": false,
                "mRender": function (a, b, obj) {
                    if (obj["IsConfirmed"] == true) {
                        return "Active";
                    } else
                        return "InActive";

                }
            },
            {
                "mData": null,
                "sTitle": "Action",
                "sWrap": true,
                "bSortable": false,
                "sWidth": "50px",
                "mRender": function (a, b, obj) {
                    var resetPasswordOption = (obj["IsConfirmed"])
                                                ? '<option value="3">Change Password</option>'
                                                : '<option value="3" disabled>Change Password</option>';

                    var resendConfirmOption = (obj["IsConfirmed"])
                                                ? '<option value="4" disabled>Resend Confirmation Link</option>'
                                                : '<option value="4">Resend Confirmation Link</option>';


                    return "<input type='hidden' value='" + obj["UserId"] + "'><select class='action' > <option value='0'></option><option value='1'>Edit</option><option value='2'>Delete</option>" + resetPasswordOption + resendConfirmOption+"</select>";
                }
            }
        ],


    }).css("width", "100%");;

    $('div.userAcctFilterBar').append('<h4>Search</h4><div class="col-md-3" data-role="input-control"><input type="text" class="input-control text" id="srchUserId" placeholder="UserId" /></div><div class="col-md-3" data-role="input-control"><input type="text" class="input-control text" id="srchFirstName" placeholder="First Name" /></div><div class="col-md-3" data-role="input-control"><input type="text" class="input-control text" id="srchLastName" placeholder="Last Name" /></div><div class="col-md-3" data-role="input-control"><input type="email" class="input-control text" id="srchEmail" placeholder="Email" /></div>');

},
    AVTOXPortal.User.Register = function () {
        if (!$('form').valid()) {
            return false;
        } else {
            $(".validation-summary-errors").html("<ul></ul>");
            $(".validation-summary-errors").attr('data-valmsg-summary', true);
            $(".validation-summary-errors").attr('class', 'validation-summary-valid');
        }
        var url = AVTOXPortal.resolveUrl("~/account/register");
        var isNewUser = true;
        if ($('#UserId').val() > 0) {
            isNewUser = false;
            url =AVTOXPortal.resolveUrl( "~/account/edit");
        }
        //form validated
        $('#loader').show();
        var data = { 'UserName': $('#UserName').val(), 'FirstName': $('#FirstName').val(), 'LastName': $('#LastName').val(), 'SelectedRole': $('#SelectedRole').val(), 'EmailAddress': $('#EmailAddress').val() };
        $.ajax({
            type: "POST",
            dataType: "json",
            data: data,
            url: url,
            success: function (serverData) {
                $('#loader').hide();
               
                if (serverData.Success == false) {
                    $('.validation-summary-valid').parent().html('<div class="validation-summary-errors" data-valmsg-summary="true"><ul><li>' + serverData.ErrorMessage + '</li></ul></div>');
                    $('.validation-summary-valid').css('validation-summary-errors');
                }
                if (serverData.Success == true) {
                    if (isNewUser) {
                        toastr.success('User registered successfully.');
                    } else {
                        toastr.success('User modified successfully.');
                    }

                    AVTOXPortal.UserListTable.fnDraw(true);
                    AVTOXPortal.User.ClearControls();
                }
            },
            error: function(xhr, ajaxOptions, thrownError) {
                $('#loader').hide();
                toastr.error("Process failed!! - "+ thrownError);
            },

        });
        return false;

    };
AVTOXPortal.resolveUrl = function (url) {
    var appRootUrl = $('body').attr('data-baseurl');

    var resolved = url;
    if (url.charAt(0) == '~')
        resolved = appRootUrl + url.substring(2);
    return resolved;
};
$(function () {
    toastr.options = {
        "debug": false,
        "positionClass": "toast-top-right",
        "onclick": null,
        "fadeIn": 300,
        "fadeOut": 1000,
        "timeOut": 5000,
        "extendedTimeOut": 1000
    };
    $('#loader').hide();
    $('form').submit(AVTOXPortal.User.Register);
    AVTOXPortal.User.List();
    $('#tblUserList tbody').on('change', '.action', AVTOXPortal.User.Actions);
    $('.btn-clear').click(AVTOXPortal.User.ClearControls);
});