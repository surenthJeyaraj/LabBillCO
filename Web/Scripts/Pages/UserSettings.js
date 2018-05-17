var LabBillCoPortal = LabBillCoPortal || {};
LabBillCoPortal.resolveUrl = function (url) {
    var appRootUrl = $('body').attr('data-baseurl');

    var resolved = url;
    if (url.charAt(0) == '~')
        resolved = appRootUrl + url.substring(2);
    return resolved;
};
$(function () {
    if ($('#IsSavedSuccessfully').val() == "true") {
        $.Notify({ style: { background: '#1ba1e2', color: 'white' }, content: "User settings saved successfully.", caption: 'Success' });
    }
    var html = '<div class="row" style="width:500px;">'+
                 '<div class="col-md-12">'+
                     '<div class=form-group">'+
                         '<label for="OldPassword">Old password</label>'+
                         '<input class="form-control" id="OldPassword" name="OldPassword" placeholder="Please type your current password." type="password">' +
                     '</div>'+
                     '<div class="form-group">'+
                         '<label for="NewPassword">New password</label>'+
                         '<input class="form-control" data-hint="The password should be 8 to 16 characters long with atleast one number, one upper & lowercase and one symbol" data-hint-position="right" id="NewPassword" name="NewPassword" placeholder="Please type your new password." type="password">' +
                     '</div>'+
                     '<div class="form-group">'+
                         '<label for="ConfirmNewPassword">Confirm new password</label>'+
                         '<input class="form-control" id="ConfirmNewPassword" name="ConfirmNewPassword" placeholder="Please re-type your new password." type="password">' +
                     '</div>'+
					 '<div class="form-group">'+
                         '<input type="button" class="btn btn-success" value="Save" id="btnSavePassword">'+
					  '</div>'+
                 '</div>'+
			  '</div>';
    $("#btnChangePassword").on('click', function () {
        $.Dialog({
            overlay: true,
            shadow: true,
            flat: true,
            title: 'Change Password',
            content: html,
            onShow: function (dialog) {
                $('#NewPassword').hint();
                $('#NewPassword').trigger('mouseover');
                $('.btn-close').click(function () {
                    $('.hint').remove();
                });
                $("#btnSavePassword").on('click', function () {
                    $('.hint').remove();
                    if (validate()) {
                        $.ajax({
                            type: "POST",
                            dataType: "json",
                            data: { 'currentPassword': $('#OldPassword').val(), 'newPassword': $('#NewPassword').val() },
                            url: LabBillCoPortal.resolveUrl( "~/account/saveuserpassword"),
                            success: function (serverData) {
                                if (serverData.result == 'success') {
                                    $.Notify({ style: { background: '#1ba1e2', color: 'white' }, content: "Password successfully changed.", caption: 'Success' });
                                    $.Dialog.close();
                                } else {
                                    $.Notify({ style: { background: 'red', color: 'white' }, content: "Password change failed. Please check your current password.", caption: 'Error' });
                                }
                                
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                $.Notify({ style: { background: 'red', color: 'white' }, content: "Password change failed." + thrownError, caption: 'Error' });
                            },

                        });
                    }
                });
            }
        });
    });

    function clearErrors() {
        $('input').removeClass('input-validation-error');
    }

    function validate() {
        var oldPassword = $('#OldPassword').val();
        var newPassword = $('#NewPassword').val();
        var confirmNewPassword = $('#ConfirmNewPassword').val();
        if (oldPassword == '') {
            $.Notify({ style: { background: 'red', color: 'white' }, content: "Old password cannot be left blank.", caption: 'Error' });
            clearErrors();
            $('#OldPassword').addClass('input-validation-error');
            return false;
        }
        if (newPassword == '') {
            $.Notify({ style: { background: 'red', color: 'white' }, content: "New password cannot be left blank.", caption: 'Error' });
            clearErrors();
            $('#NewPassword').addClass('input-validation-error');
            return false;
        }
        if (oldPassword == newPassword) {
            $.Notify({ style: { background: 'red', color: 'white' }, content: "Your new password cannot be the same as your current password.", caption: 'Error' });
            clearErrors();
            $('#NewPassword').addClass('input-validation-error');
            return false;
        }
        var pwdPattern = /^(?=.*\d{1,})(?=.*[A-Z]{1,})(?=.*[a-z]{1,})(?=.*[$-/:-?{-~!^@#_`\[\]]{1,})(?=.*\w).{8,16}$/;
        if (!pwdPattern.test(newPassword)) {
            $.Notify({ style: { background: 'red', color: 'white' }, content: "Your new password does not meet the required format.", caption: 'Error' });
            clearErrors();
            $('#NewPassword').addClass('input-validation-error');
            return false;
        }
        if (newPassword != confirmNewPassword) {
            $.Notify({ style: { background: 'red', color: 'white' }, content: "Your new password and confirm password does not match.", caption: 'Error' });
            clearErrors();
            $('#NewPassword').addClass('input-validation-error');
            $('#ConfirmNewPassword').addClass('input-validation-error');
            return false;
        }
        clearErrors();
        return true;
    }
});