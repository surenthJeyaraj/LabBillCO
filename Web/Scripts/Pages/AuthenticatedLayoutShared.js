// Use global abatement technique by creating a single global variable for the application
var LabBillCoPortal = LabBillCoPortal || {};
LabBillCoPortal.Settings = LabBillCoPortal.Settings || {};
//Session time out in seconds
LabBillCoPortal.Settings.SessionTimeOut = 1160; // based on configuration

$("#idletimeout").slideUp();
LabBillCoPortal.ConfigureSession = function () {
    $.idleTimeout('#idletimeout', '#idletimeout a', {
        idleAfter: LabBillCoPortal.Settings.SessionTimeOut, // user is considered idle after this many seconds.
        AJAXTimeout: 10000,
        pollingInterval: 3000, // a polling request will be sent to the server every X seconds
        keepAliveURL: LabBillCoPortal.resolveUrl('~/Account/KeepAlive'),
        serverResponseEquals: 'OK',
        onTimeout: function () {
            $(this).slideUp();
            
            window.location = LabBillCoPortal.resolveUrl('~/Account/LogOff');
        },
        onIdle: function () {
            $(this).slideDown(); // show the warning bar
        },
        onCountdown: function (counter) {
            var ctx = $(this).find("span").length;
            $(this).find("span").html(counter); // update the counter
        },
        onResume: function () {
            $(this).slideUp(); // hide the warning bar
        }
    });
};

/*--------------------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------------------*/
$(function () {


    LabBillCoPortal.ConfigureSession();
 
});
/*--------------------------------------------------------------------------------------------*/










