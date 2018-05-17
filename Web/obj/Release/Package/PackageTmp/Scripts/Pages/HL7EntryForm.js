// Use global abatement technique by creating a single global variable for the application
var AVUTOXPortal = AVUTOXPortal || {};
AVUTOXPortal.Settings = AVUTOXPortal.Settings || {};
AVUTOXPortal.User = AVUTOXPortal.User || {};
//Session time out in seconds
AVUTOXPortal.Settings.SessionTimeOut = 1160; // based on configuration
var gloalguid, RecordStatus, FileUrl, globalScanID;


AVUTOXPortal.User.ClearControls = function () {
    $(".validation-summary-errors").html("<ul></ul>");
    $(".validation-summary-errors").attr('data-valmsg-summary', true);
    $(".validation-summary-errors").attr('class', 'validation-summary-valid');
};
AVUTOXPortal.resolveUrl = function (url) {
    var appRootUrl = $('body').attr('data-baseurl');

    var resolved = url;
    if (url.charAt(0) === '~')
        resolved = appRootUrl + url.substring(2);
    return resolved;
};
function resolveUrl(url) {
    var appRootUrl = $('body').attr('data-baseurl');

    var resolved = url;
    if (url.charAt(0) == '~')
        resolved = appRootUrl + url.substring(2);
    return resolved;
}

AVUTOXPortal.Getguid = function () {

    console.log(window.location.search.slice(1));
    var params = (new URL(location)).searchParams;
    globalguid = params.get('ScaID');
    RecordStatus = params.get('RecordStatus');
    FileUrl = params.get('FileUrl');
    globalScanID = params.get('ScanID');
    console.log(globalguid + "  " + RecordStatus + "  " + FileUrl);
    var hidUserData = $('#hidUserEmail').val();


    //var ext = FileUrl.substring(FileUrl.lastIndexOf('.') + 1);

    //if (ext === "pdf") {
    //    return true;
    //}
    //else {
    //    toastr.error("Please check the Pdf file url in CRM.");
    //    return false;
    //} 

}



AVUTOXPortal.User.CheckandGetOrderJson = function () {
    var url = AVUTOXPortal.resolveUrl("~/Transaction/CheckandGetOrderJson");
    var ext = FileUrl.substring(FileUrl.lastIndexOf('.') + 1);
    if (ext === "pdf") {
        $('#pdf_iframe').attr('src', FileUrl + "#toolbar=0&navpanes=0&overflow=hidden");
    }
    else {
        $('#pdf_iframe').attr('srcdoc', "<div style=font-weight:bold;color:Red> Error!!! Please check the Pdf file url in CRM....</div>");
    }


    $('#loader').show();
    var data = {
        OrderId: globalguid
    };
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: data,
        url: url,
        success: function (serverData) {
            $('#loader').hide();

            if (serverData.Success == false) {
                $('.validation-summary-valid').parent().html('<div class="validation-summary-errors" data-valmsg-summary="true"><ul><li>' + serverData.ErrorMessage + '</li></ul></div>');
                $('.validation-summary-valid').css('validation-summary-errors');
            }
            if (serverData.Success === true) {

                AVUTOXPortal.User.populateAll(serverData.OrderFormData);

                AVUTOXPortal.User.ClearControls();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $('#loader').hide();
            toastr.error("Process failed!! - " + thrownError);
        },

    });
    return false;

};


AVUTOXPortal.User.OrderFormsubmit = function (postData, clickbutton) {

    var url = AVUTOXPortal.resolveUrl("~/Transaction/PostHl7OrderForm");
    var entryOption = clickbutton === "saveButton" ? 1 : 2;

    console.log(entryOption);

    var odataOrder = JSON.stringify(postData);
    $('#loader').show();
    var data = {
        'HL7Stream': odataOrder,
        'Status': RecordStatus,
        OrderId: globalguid,
        'entryOption': entryOption

    };
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: data,
        url: url,
        success: function (serverData) {
            window.$('#loader').hide();

            if (serverData.Success == false) {
                window.$('.validation-summary-valid').parent().html('<div class="validation-summary-errors" data-valmsg-summary="true"><ul><li>' + serverData.ErrorMessage + '</li></ul></div>');
                window.$('.validation-summary-valid').css('validation-summary-errors');
            }
            if (serverData.Success === true) {
                var message = entryOption === 1 ? "Saved" : "Submitted";
                toastr.success("Order " + message + " Successfully.");

                if (entryOption === 2) {

                    window.$(location).attr('href', "welcome");
                }

                AVUTOXPortal.User.ClearControls();
            }
            else toastr.error("Sorry!! something went wrong");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $('#loader').hide();
            toastr.error("Process failed!! - " + thrownError);
        }

    });
    return false;

};



function navTab(form_name, target) {
    console.log(form_name);
    var isValidForm = doValidate(form_name, '#VALIDATION_MESSAGES');
    if (isValidForm) {
        $('.nav-tabs a[href="#' + target + '"]').tab('show')
    }
    //$('.nav-tabs a[href="#' + target + '"]').tab('show')
}
function checkAllForms() {
    var isAllValid = true;
    var formList = [
        "order-form",
        "pid-form",
        "dg1-form",
        "in1-form",
        "in2-form",
        "pv1-form",
        "ft1-form"
    ];
    $.each(formList, function (key, val) {
        $('#VALIDATION_MESSAGES' + key).remove();
        console.log(key);
        $('<div class="col-md-6" style="margin:0px auto" id="VALIDATION_MESSAGES' + key + '"> </div>').insertAfter("#VALIDATION_MESSAGES");
        var isValidForm = doValidate(val, '#VALIDATION_MESSAGES' + key);
        console.log(isValidForm);
        if (isValidForm == false) {
            isAllValid = false;
        }
        else {
            $('#VALIDATION_MESSAGES' + key).remove();
        }
    });
    return isAllValid;
}
$(function () {
    $("#saveButton,#SubmitButton").click(function (e) {
        var isAllFormsValid = checkAllForms();
        if (isAllFormsValid == false) {
            return false;
        }
        var postData = {};
        var idClicked = e.target.id;
        console.log(idClicked);

        //Order Section Data build
        var Order = getOrderDatas();
        if (Order != undefined) {
            postData.Order = Order;
        }

        //DG1 Section Data build
        var DGCODE = getDg1Datas();
        if (DGCODE.length > 0) {
            postData.DG1 = {
                "DgCode": DGCODE
            };
        }

        //Insurance Section Data build
        var Insurance = {};
        var in1Data = getInsurance1Datas();
        if (in1Data != undefined) {
            Insurance.IN1 = in1Data;
        }
        if ($('.insurance-tabs').length == 2) {
            var in2Data = getInsurance2Datas();
            if (in2Data != undefined) {
                Insurance.IN2 = in2Data;
            }
        }

        if (Insurance != undefined) {
            postData.Insurance = Insurance;
        }

        //PID Section Data build
        var PID = getPidDatas();
        if (PID != undefined) {
            postData.PID = PID;
        }

        //PV1 Section Data build
        var pv1 = getPv1Datas();
        if (pv1 != undefined) {
            postData.PV1 = pv1;
        }

        //FT1 Section data build
        var FT1 = getFt1Datas();
        if (FT1 != undefined) {
            postData.FT1 = FT1;
        }

        //Return whole Object
        console.log(postData);

        AVUTOXPortal.User.OrderFormsubmit(postData, idClicked);

    });

    function getOrderDatas() {
        var order = {};
        var orderFields = $('form[name=order-form]').serializeArray();
        $(orderFields).each(function (key, val) {
            if (val.value.length > 0) {
                var objName = val.name;
                var objVal = val.value;
                order[objName + ""] = val.value;
            }
        });
        return order;
    }

    function getDg1Datas() {
        var DGCODE = [];
        var checked_codes = $('form[name=dg1-form]').serializeArray();
        $(checked_codes).each(function (key, val) {
            DGCODE.push(val.value)
        });
        return DGCODE;
    }

    function getInsurance1Datas() {
        var IN1 = {};
        // Payer fields
        var Payer = {};
        var BillTo = [];
        var checked_payers = $('form[name=in1-form] .billto-options :input').serializeArray();
        $(checked_payers).each(function (key, val) {
            BillTo.push(val.value)
        });
        if (BillTo.length > 0) {
            Payer.BillTo = BillTo;
        } else {
            delete Payer.BillTo;
        }

        if ($('form[name=in1-form] input[name=PayerID]').val().length > 0) {
            Payer.PayerID = $('form[name=in1-form] input[name=PayerID]').val();
        } else {
            delete Payer.PayerID;
        }

        if ($('form[name=in1-form] input[name=PayerName]').val().length > 0) {
            Payer.PayerName = $('form[name=in1-form] input[name=PayerName]').val();
        } else {
            delete Payer.PayerName;
        }

        if (Payer != undefined) {
            IN1.Payer = Payer;
        }

        // Payer Contact fields
        var PayerContact = {};
        var PayerContact_fields = $('form[name=in1-form] .payercontact-fields :input').serializeArray();
        $(PayerContact_fields).each(function (key, val) {
            if (val.value.length > 0) {
                var objName = val.name;
                var objVal = val.value;
                PayerContact[objName + ""] = val.value;
                if ($('form[name=in1-form] .payercontact-fields input[name=' + val.name + ']').hasClass("masked-input")) {
                    PayerContact[objName + ""] = $('form[name=in1-form] .payercontact-fields input[name=' + val.name + ']').cleanVal(val.value);
                }
            }
        });
        if (PayerContact != undefined) {
            IN1.PayerContact = PayerContact;
        } else {
            delete IN1.PayerContact;
        }

        // Subscriber fields
        var Subscriber = {};
        /*var RelationshiptoSubscriber = [];
        var checked_rel = $('form[name=in1-form] .subrel-options :input').serializeArray();
        $(checked_rel).each(function (key, val) {
            RelationshiptoSubscriber.push(val.value)
        });
        if (RelationshiptoSubscriber.length > 0) {
            Subscriber.RelationshiptoSubscriber = RelationshiptoSubscriber;
        } else {
            delete Subscriber.RelationshiptoSubscriber;
        }*/
        if ($('form[name=in1-form] input[name=RelationshiptoSubscriber]').val().length > 0) {
            Subscriber.RelationshiptoSubscriber = $('form[name=in1-form] input[name=RelationshiptoSubscriber]:checked').val();
        } else {
            delete Subscriber.RelationshiptoSubscriber;
        }
        if ($('form[name=in1-form] input[name=SubscriberFirstName]').val().length > 0) {
            Subscriber.SubscriberFirstName = $('form[name=in1-form] input[name=SubscriberFirstName]').val();
        } else {
            delete Subscriber.SubscriberFirstName;
        }

        if ($('form[name=in1-form] input[name=SubscriberLastName]').val().length > 0) {
            Subscriber.SubscriberLastName = $('form[name=in1-form] input[name=SubscriberLastName]').val();
        } else {
            delete Subscriber.SubscriberLastName;
        }

        if ($('form[name=in1-form] input[name=InsuranceID]').val().length > 0) {
            Subscriber.InsuranceID = $('form[name=in1-form] input[name=InsuranceID]').val();
        } else {
            delete Subscriber.InsuranceID;
        }

        if ($('form[name=in1-form] input[name=GroupID]').val().length > 0) {
            Subscriber.GroupID = $('form[name=in1-form] input[name=GroupID]').val();
        } else {
            delete Subscriber.GroupID;
        }

        if (Subscriber != undefined) {
            IN1.Subscriber = Subscriber;
        }
        return IN1;
    }

    function getInsurance2Datas() {
        var IN2 = {};
        // Payer fields
        var Payer = {};
        var BillTo = [];
        var checked_payers = $('form[name=in2-form] .billto-options :input').serializeArray();
        $(checked_payers).each(function (key, val) {
            BillTo.push(val.value)
        });
        if (BillTo.length > 0) {
            Payer.BillTo = BillTo;
        } else {
            delete Payer.BillTo;
        }

        if ($('form[name=in2-form] input[name=PayerID]').val().length > 0) {
            Payer.PayerID = $('form[name=in2-form] input[name=PayerID]').val();
        } else {
            delete Payer.PayerID;
        }

        if ($('form[name=in2-form] input[name=PayerName]').val().length > 0) {
            Payer.PayerName = $('form[name=in2-form] input[name=PayerName]').val();
        } else {
            delete Payer.PayerName;
        }

        if (Payer != undefined) {
            IN2.Payer = Payer;
        }

        // Payer Contact fields
        var payerContact = {};
        var payerContactFields = $('form[name=in2-form] .payercontact-fields :input').serializeArray();
        $(payerContactFields).each(function (key, val) {
            if (val.value.length > 0) {
                var objName = val.name;
                var objVal = val.value;
                payerContact[objName + ""] = val.value;
                if ($('form[name=in2-form] .payercontact-fields input[name=' + val.name + ']').hasClass("masked-input")) {
                    payerContact[objName + ""] = $('form[name=in2-form] .payercontact-fields input[name=' + val.name + ']').cleanVal(val.value);
                }
            }
        });
        if (payerContact != undefined) {
            IN2.PayerContact = payerContact;
        } else {
            delete IN2.PayerContact;
        }

        // Subscriber fields
        var Subscriber = {};
        /*var RelationshiptoSubscriber = [];
        var checked_rel = $('form[name=in2-form] .subrel-options :input').serializeArray();
        $(checked_rel).each(function (key, val) {
            RelationshiptoSubscriber.push(val.value)
        });
        if (RelationshiptoSubscriber.length > 0) {
            Subscriber.RelationshiptoSubscriber = RelationshiptoSubscriber;
        } else {
            delete Subscriber.RelationshiptoSubscriber;
        }*/
        if ($('form[name=in2-form] input[name=RelationshiptoSubscriber]').val().length > 0) {
            Subscriber.RelationshiptoSubscriber = $('form[name=in2-form] input[name=RelationshiptoSubscriber]:checked').val();
        } else {
            delete Subscriber.RelationshiptoSubscriber;
        }

        if ($('form[name=in2-form] input[name=SubscriberFirstName]').val().length > 0) {
            Subscriber.SubscriberFirstName = $('form[name=in2-form] input[name=SubscriberFirstName]').val();
        } else {
            delete Subscriber.SubscriberFirstName;
        }

        if ($('form[name=in2-form] input[name=SubscriberLastName]').val().length > 0) {
            Subscriber.SubscriberLastName = $('form[name=in2-form] input[name=SubscriberLastName]').val();
        } else {
            delete Subscriber.SubscriberLastName;
        }

        if ($('form[name=in2-form] input[name=InsuranceID]').val().length > 0) {
            Subscriber.InsuranceID = $('form[name=in2-form] input[name=InsuranceID]').val();
        } else {
            delete Subscriber.InsuranceID;
        }

        if ($('form[name=in2-form] input[name=GroupID]').val().length > 0) {
            Subscriber.GroupID = $('form[name=in2-form] input[name=GroupID]').val();
        } else {
            delete Subscriber.GroupID;
        }

        if (Subscriber != undefined) {
            IN2.Subscriber = Subscriber;
        }
        return IN2;
    }

    function getPidDatas() {
        var PID = {};
        //Patient Section fields
        var patient = {};
        var patientFields = $('form[name=pid-form] .patient-fields :input').serializeArray();
        $(patientFields).each(function (key, val) {
            if (val.value.length > 0) {
                var objName = val.name;
                var objVal = val.value;
                patient[objName + ""] = val.value;
                if ($('form[name=pid-form] .patient-fields input[name=' + val.name + ']').hasClass("masked-input")) {
                    patient[objName + ""] = $('form[name=pid-form] .patient-fields input[name=' + val.name + ']').cleanVal(val.value);
                }
            }
        });
        if (patient != undefined) {
            PID.Patient = patient;
        } else {
            delete PID.Patient;
        }

        //Patient Contact Section fields
        var patientContact = {};
        var patientContactFields = $('form[name=pid-form] .patientcontact-fields :input').serializeArray();
        $(patientContactFields).each(function (key, val) {
            if (val.value.length > 0) {
                var objName = val.name;
                var objVal = val.value;
                patientContact[objName + ""] = val.value;
                if ($('form[name=pid-form] .patientcontact-fields input[name=' + val.name + ']').hasClass("masked-input")) {
                    patientContact[objName + ""] = $('form[name=pid-form] .patientcontact-fields input[name=' + val.name + ']').cleanVal(val.value);
                }
            }
        });
        if (patientContact != undefined) {
            PID.PatientContact = patientContact;
        } else {
            delete PID.PatientContact;
        }

        //Demographics Section fields
        var demographics = {};

        var checkedRace = getCheckboxArray('form[name=pid-form] .patientrace-fields :input');
        if (checkedRace.length > 0) {
            demographics.PatientRace = checkedRace;
        } else {
            delete demographics.PatientRace;
        }

        var checkedEthnicity = getCheckboxArray('form[name=pid-form] .patientethnicity-fields :input');
        if (checkedEthnicity.length > 0) {
            demographics.PatientEthnicity = checkedEthnicity;
        } else {
            delete demographics.PatientEthnicity;
        }

        var checkedLang = getCheckboxArray('form[name=pid-form] .patientlang-fields :input');
        if (checkedLang.length > 0) {
            demographics.PatientLang = checkedLang;
        } else {
            delete demographics.PatientLang;
        }

        if (demographics != undefined) {
            PID.Demographics = demographics;
        } else {
            delete PID.Demographics;
        }

        return PID;
    }

    function getPv1Datas() {
        var pv1 = {};
        if ($('form[name=pv1-form] input[name=ReferringPhysician]').val().length > 0) {
            pv1.ReferringPhysician = $('form[name=pv1-form] input[name=ReferringPhysician]').val();
        } else {
            delete pv1.ReferringPhysician;
        }

        if ($('form[name=pv1-form] input[name=uni_id]:checked').val() == 'npi' &&
            $('.npi-field input[type=text]').val().length > 0) {
            pv1.NPI = $('.npi-field input[type=text]').val();
        } else if ($('form[name=pv1-form] input[name=uni_id]:checked').val() == 'other' &&
            $('.oth-field input[type=text]').val().length > 0) {
            pv1.OTHER = $('.oth-field input[type=text]').val();
        } else {
            delete pv1.NPI;
            delete pv1.OTHER;
        }
        return pv1;
    }

    function switchRadio(target) {
        $('.npi-field input[type=text]').attr('disabled', 'true');
        $('.oth-field input[type=text]').attr('disabled', 'true');
        $('.' + target + ' input[type=text]').removeAttr('disabled');
    }

    function getFt1Datas() {
        var FT1 = {};

        //FT1 fields
        var checkedTestOpt = getCheckboxArray('form[name=ft1-form] .testoptions-fields :input');
        if (checkedTestOpt.length > 0) {
            FT1.TestOption = checkedTestOpt;
        } else {
            delete FT1.TestOption;
        }

        //PointOfCareTestResults fields
        var checkedPoc = getCheckboxArray('form[name=ft1-form] .poc-fields :input');
        if (checkedPoc.length > 0) {
            FT1.PointOfCareTestResults = checkedPoc;
        } else {
            delete FT1.PointOfCareTestResults;
        }

        //LC/MS/MS Testing, Confirm fields
        var lcmsmsTest = {};

        var checkedOpiatesGroup = getCheckboxArray('form[name=ft1-form] .OpiatesGroup-fields :input');
        if (checkedOpiatesGroup.length > 0) {
            lcmsmsTest.OpiatesGroup = checkedOpiatesGroup;
        } else {
            delete lcmsmsTest.OpiatesGroup;
        }

        var checkedSyntheticOpioidsGroup =
            getCheckboxArray('form[name=ft1-form] .SyntheticOpioidsGroup-fields :input');
        if (checkedSyntheticOpioidsGroup.length > 0) {
            lcmsmsTest.SyntheticOpioidsGroup = checkedSyntheticOpioidsGroup;
        } else {
            delete lcmsmsTest.SyntheticOpioidsGroup;
        }

        var checkedBenzodiazepinesGroup = getCheckboxArray('form[name=ft1-form] .BenzodiazepinesGroup-fields :input');
        if (checkedBenzodiazepinesGroup.length > 0) {
            lcmsmsTest.BenzodiazepinesGroup = checkedBenzodiazepinesGroup;
        } else {
            delete lcmsmsTest.BenzodiazepinesGroup;
        }

        var checkedOthersGroup = getCheckboxArray('form[name=ft1-form] .OthersGroup-fields :input');
        if (checkedOthersGroup.length > 0) {
            lcmsmsTest.OthersGroup = checkedOthersGroup;
        } else {
            delete lcmsmsTest.OthersGroup;
        }

        var checkedAntiDepressantsGroup = getCheckboxArray('form[name=ft1-form] .AntiDepressantsGroup-fields :input');
        if (checkedAntiDepressantsGroup.length > 0) {
            lcmsmsTest.AntiDepressantsGroup = checkedAntiDepressantsGroup;
        } else {
            delete lcmsmsTest.AntiDepressantsGroup;
        }

        var checkedAntiPsychoticGroup = getCheckboxArray('form[name=ft1-form] .AntiPsychoticGroup-fields :input');
        if (checkedAntiPsychoticGroup.length > 0) {
            lcmsmsTest.AntiPsychoticGroup = checkedAntiPsychoticGroup;
        } else {
            delete lcmsmsTest.AntiPsychoticGroup;
        }

        var checkedDrugsOfAbuseGroupGroup =
            getCheckboxArray('form[name=ft1-form] .DrugsOfAbuseGroupGroup-fields :input');
        if (checkedDrugsOfAbuseGroupGroup.length > 0) {
            lcmsmsTest.DrugsOfAbuseGroupGroup = checkedDrugsOfAbuseGroupGroup;
        } else {
            delete lcmsmsTest.DrugsOfAbuseGroupGroup;
        }

        var checkedAdditionalTesting = getCheckboxArray('form[name=ft1-form] .AdditionalTesting-fields :input');
        if (checkedAdditionalTesting.length > 0) {
            lcmsmsTest.AdditionalTesting = checkedAdditionalTesting;
        } else {
            delete lcmsmsTest.AdditionalTesting;
        }

        if (lcmsmsTest != undefined) {
            FT1.LCMSMSTest = lcmsmsTest;
        } else {
            delete FT1.LCMSMSTest;
        }

        return FT1;
    }

    function getCheckboxArray(ele) {
        var result = [];
        var checkedItems = $(ele).serializeArray();
        $(checkedItems).each(function (key, val) {
            result.push(val.value)
        });
        return result;
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


        //  $('form').submit(AVUTOXPortal.User.OrderFormsubmit);
    });

});

$(function () {
    $('#AddInsurance').click(function () {
        console.log('Entry the AddInsurancemethod');
        var id = $('.insurance-tabs').length;
        console.log(id);
        if (id == 1) {
            id = id + 1;
            var tpl = getInsTpl(id);
            $('<li class="nav-item nav-item-btn"> <a class="nav-link insurance-tabs" id="in' + id + '-tab" data-toggle="tab" href="#in' + id + 'tab" role="tab" aria-controls="contact" aria-selected="false">IN ' + id + '</a><button class="cycle-button mini-button danger" onclick="removeInsurance(' + id + ');"><span class="mif-minus"></span></button></li>').insertAfter($('#in' + (id - 1) + '-tab').parent());
            $(tpl).appendTo('.custom-tab-content');
            $('.custom-tab-head a#in' + id + '-tab').tab('show');
        }
        else {
            alert('Maximum 2 Insurance allowed');
        }
    });
}
);
function addDiagnosisCode() {
    //alert($('#new_diagnosis_code').val());
    var code = $('#new_diagnosis_code').val();
    if (code != '') {
        var elem = ' <label class="input-control checkbox small-check"> <input type="checkbox" name="dg1_code" value="' + code + '" checked=true> <span class="check"></span>  <span class="caption">' + code + '</span> </label>'
        $('#all_diagnosis_codes').append(elem);
        $('#new_diagnosis_code').val('');
    }
    else {
        alert('Please enter valid Diagnosis code');
    }
}
function removeInsurance(id) {
    $('#in' + (id) + '-tab').parent().remove();
    $('#in' + (id) + 'tab').remove();
    $('.custom-tab-head a#in' + (id - 1) + '-tab').tab('show');
}
function switchRadio(target) {
    $('.npi-field input[type=text]').attr('disabled', 'true');
    $('.oth-field input[type=text]').attr('disabled', 'true');
    $('.' + target + ' input[type=text]').removeAttr('disabled');
}
function getInsTpl(id) {
    var tpl = '<div class="tab-pane fade show" id="in' + id + 'tab" role="tabpanel" aria-labelledby="in1-tab" data-repeater-list="group-a">';
    tpl += '<form name="in2-form"> ';
    tpl += '<div class="row form-section">';
    tpl += '<div class="col-md-11">';
    tpl += '<div class="row field-container payer-fields">';
    tpl += '<h5 class="section-title">Payer</h5>';
    tpl += '<div class="col-md-4"><label class="input-control">Bill To</label></div>';
    tpl += '<div class="col-md-8 billto-options">';
    tpl += '<label class="input-control checkbox small-check">';
    tpl += '<input type="checkbox" name="bill_to" value="Medicare" > <span class="check"></span>';
    tpl += '<span class="caption">Medicare</span>';
    tpl += '</label>';
    tpl += '<label class="input-control checkbox small-check">';
    tpl += '<input type="checkbox" name="bill_to" value="Medicaid" > <span class="check"></span>';
    tpl += '<span class="caption">Medicaid</span>';
    tpl += '</label>';
    tpl += '<label class="input-control checkbox small-check">';
    tpl += '<input type="checkbox" name="bill_to" value="Insurance" > <span class="check"></span>';
    tpl += '<span class="caption">Insurance</span>';
    tpl += '</label>';
    tpl += '<label class="input-control checkbox small-check">';
    tpl += '<input type="checkbox" name="bill_to" value="FFT" > <span class="check"></span>';
    tpl += '<span class="caption">FFT</span>';
    tpl += '</label>';
    tpl += '<label class="input-control checkbox small-check">';
    tpl += '<input type="checkbox" name="bill_to" value="Self" > <span class="check"></span>';
    tpl += '<span class="caption">Self Pay</span>';
    tpl += '</label>';
    tpl += '</div>';
    tpl += '<!-- Payer Field -->';
    tpl += '<div class="col-md-4"><label class="input-control">Payer ID</label></div>';
    tpl += '<div class="col-md-8">';
    tpl += '<div class="input-group input-group-sm mb-1">';
    tpl += '<input type="text" name="PayerID" class="form-control" aria-label="Small" aria-describedby="inputGroup-sizing-sm">';
    tpl += '</div>';
    tpl += '</div>';
    tpl += '<div class="col-md-4"><label class="input-control">Payer Name</label></div>';
    tpl += '<div class="col-md-8">';
    tpl += '<div class="input-group input-group-sm mb-1">';
    tpl += '<input type="text" name="PayerName" class="form-control" aria-label="Small" aria-describedby="inputGroup-sizing-sm">';
    tpl += '</div>';
    tpl += '</div>';
    tpl += '</div>';
    tpl += '</div>';
    tpl += '<div class="col-md-11">';
    tpl += '<div class="row field-container payercontact-fields">';
    tpl += '<h5 class="section-title">PayerContact</h5>';
    tpl += '<!-- Payer Contact Field -->';
    tpl += '<div class="col-md-4"><label class="input-control">Payer Address Line1</label></div>';
    tpl += '<div class="col-md-8">';
    tpl += '<div class="input-group input-group-sm mb-1">';
    tpl += '<input type="text" name="PayerAddressLine1" class="form-control" aria-label="Small" aria-describedby="inputGroup-sizing-sm">';
    tpl += '</div>';
    tpl += '</div>';
    tpl += '<div class="col-md-4"><label class="input-control">Payer Address Line2</label></div>';
    tpl += '<div class="col-md-8">';
    tpl += '<div class="input-group input-group-sm mb-1">';
    tpl += '<input type="text" name="PayerAddressLine2" class="form-control" aria-label="Small" aria-describedby="inputGroup-sizing-sm">';
    tpl += '</div>';
    tpl += '</div>';
    tpl += '<div class="col-md-4"><label class="input-control">Payer City</label></div>';
    tpl += '<div class="col-md-8">';
    tpl += '<div class="input-group input-group-sm mb-1">';
    tpl += '<input type="text" name="PayerCity" class="form-control" aria-label="Small" aria-describedby="inputGroup-sizing-sm">';
    tpl += '</div>';
    tpl += '</div>';
    tpl += '<div class="col-md-4"><label class="input-control">Payer State</label></div>';
    tpl += '<div class="col-md-8">';
    tpl += '<div class="input-group input-group-sm mb-1">';
    tpl += '<select class="form-control" name="PayerState">';
    tpl += '<option ></option>';
    tpl += '<option value="CA">CA</option>';
    tpl += '</select>';
    tpl += '</div>';
    tpl += '</div>';
    tpl += '<div class="col-md-4"><label class="input-control">Payer ZIP</label></div>';
    tpl += '<div class="col-md-8">';
    tpl += '<div class="input-group input-group-sm mb-1">';
    tpl += '<input type="text" name="PayerZIP" class="form-control masked-input mask-zipcode" aria-label="Small" aria-describedby="inputGroup-sizing-sm">';
    tpl += '</div>';
    tpl += '</div>';
    tpl += '<div class="col-md-4"><label class="input-control">Payer Phone</label></div>';
    tpl += '<div class="col-md-8">';
    tpl += '<div class="input-group input-group-sm mb-1">';
    tpl += '<input type="text" name="PayerPhone" class="form-control masked-input mask-phone" aria-label="Small" aria-describedby="inputGroup-sizing-sm">';
    tpl += '</div>';
    tpl += '</div>';
    tpl += '</div>';
    tpl += '</div>';
    tpl += '<div class="col-md-11">';
    tpl += '<div class="row field-container subscriber-fields">';
    tpl += '<h5 class="section-title">Subscriber</h5>';
    tpl += '<div class="col-md-4"><label class="input-control">Relationship to Subscriber</label></div>';
    tpl += '<div class="col-md-8">';
    tpl += '<label class="input-control radio small-check">';
    tpl += '<input type="radio" name="RelationshiptoSubscriber" value="800000000"> <span class="check"></span>';
    tpl += '<span class="caption">Self</span>';
    tpl += '</label>';
    tpl += '<label class="input-control radio small-check">';
    tpl += '<input type="radio" name="RelationshiptoSubscriber" value="800000001"> <span class="check"></span>';
    tpl += '<span class="caption">Spouse</span>';
    tpl += '</label>';
    tpl += '<label class="input-control radio small-check">';
    tpl += '<input type="radio" name="RelationshiptoSubscriber" value="800000002"> <span class="check"></span>';
    tpl += '<span class="caption">Child</span>';
    tpl += '</label>';
    tpl += '<label class="input-control radio small-check">';
    tpl += '<input type="radio" name="RelationshiptoSubscriber" value=""> <span class="check"></span>';
    tpl += '<span class="caption">Others</span>';
    tpl += '</label>';
    tpl += '</div>';
    tpl += '<!-- Subscriber First Name -->';
    tpl += '<div class="col-md-4"><label class="input-control">Subscriber First Name</label></div>';
    tpl += '<div class="col-md-8">';
    tpl += '<div class="input-group input-group-sm mb-1">';
    tpl += '<input type="text" name="SubscriberFirstName" class="form-control" aria-label="Small" aria-describedby="inputGroup-sizing-sm">';
    tpl += '</div>';
    tpl += '</div>';
    tpl += '<!-- Subscriber Last Name -->';
    tpl += '<div class="col-md-4"><label class="input-control">Subscriber Last Name</label></div>';
    tpl += '<div class="col-md-8">';
    tpl += '<div class="input-group input-group-sm mb-1">';
    tpl += '<input type="text" name="SubscriberLastName" class="form-control" aria-label="Small" aria-describedby="inputGroup-sizing-sm">';
    tpl += '</div>';
    tpl += '</div>';
    tpl += '<!-- Insurance ID -->';
    tpl += '<div class="col-md-4"><label class="input-control">Insurance ID</label></div>';
    tpl += '<div class="col-md-8">';
    tpl += '<div class="input-group input-group-sm mb-1">';
    tpl += '<input type="text" name="InsuranceID" class="form-control" aria-label="Small" aria-describedby="inputGroup-sizing-sm">';
    tpl += '</div>';
    tpl += '</div>';
    tpl += '<!-- Group ID -->';
    tpl += '<div class="col-md-4"><label class="input-control">Group ID</label></div>';
    tpl += '<div class="col-md-8">';
    tpl += '<div class="input-group input-group-sm mb-1">';
    tpl += '<input type="text" name="GroupID" class="form-control" aria-label="Small" aria-describedby="inputGroup-sizing-sm">';
    tpl += '</div>';
    tpl += '</div>';
    tpl += '</div>';
    tpl += '</div>';
    tpl += '<div class="col-md-12 tab-nav-group">';
    tpl += '<button type="button" class="btn btn-primary btn-sm" onclick="navTab(`in2-form`,`in11tab`);">< Previous</button>';
    tpl += '<button type="button" class="btn btn-success btn-sm" onclick="navTab(`in2-form`,`pv1tab`);">Next ></button>';
    tpl += '</div>';
    tpl += '</div>';
    tpl += '</form>';
    tpl += '</div>';
    return tpl;
}
//populateAll(data);
AVUTOXPortal.User.populateAll = function (data) {


    data = JSON.parse(data);

    //Order Section Binding
    if (data.Order != undefined) { populateOrderDatas(data.Order); }

    //DG1 Section Binding
    if (data.DG1 != undefined) { populateDg1Datas(data.DG1); }

    //Insurance Section Binding
    if (data.Insurance != undefined) { populateInsuranceDatas(data.Insurance); }

    //PID Section Binding
    if (data.PID != undefined) { populatePidDatas(data.PID); }

    //PV1 Section binding
    if (data.PV1 != undefined) { populatePv1Datas(data.PV1); }

    //FT1 Section binding
    if (data.FT1 != undefined) { populateFt1Datas(data.FT1); }
}
function populateOrderDatas(orderData) {
    $('form[name=order-form] input').each(function (key, value) {
        value.value = orderData[value.name];
    });
    $('form[name=order-form] input[name=OrderID]').val(globalScanID);
}
function populateDg1Datas(dg1Data) {
    var allChecks = getAllcheckboxes('form[name=dg1-form] input[type=checkbox]:not(:checked)');
    $(dg1Data.DgCode).each(function (key, value) {
        if ($.inArray(value, allChecks) != '-1') {
            $('form[name=dg1-form] input[type=checkbox][value=' + value + ']').prop('checked', true);
        }
        else {
            var chkTpl = '<label class="input-control checkbox small-check">';
            chkTpl += '<input type="checkbox" value="' + value + '" name="dg1_code" checked=true> <span class="check"></span>';
            chkTpl += '<span class="caption">' + value + '</span>';
            chkTpl += '</label>';
            $(chkTpl).insertAfter($('form[name=dg1-form] label').last());
        }
    });
}
function populateInsuranceDatas(InsuranceDatas) {
    //IN1 Section Binding
    if (InsuranceDatas.IN1 != undefined) { setIn1Dats(InsuranceDatas.IN1); }

    //IN2 Section Binding
    if (InsuranceDatas.IN2 != undefined) {
        var tpl = getInsTpl(2);
        $('<li class="nav-item nav-item-btn"> <a class="nav-link insurance-tabs" id="in' + 2 + '-tab" data-toggle="tab" href="#in' + 2 + 'tab" role="tab" aria-controls="contact" aria-selected="false">IN ' + 2 + '</a><button class="cycle-button mini-button danger" onclick="removeInsurance(' + 2 + ');"><span class="mif-minus"></span></button></li>').insertAfter($('#in' + (2 - 1) + '-tab').parent());
        $(tpl).appendTo('.custom-tab-content');
        $('form[name=in2-form] .payercontact-fields input[type=text][name=PayerZIP]').mask('00000');
        $('form[name=in2-form] .payercontact-fields input[type=text][name=PayerPhone]').mask('AAA-AAA-AAAA');
        setIn2Dats(InsuranceDatas.IN2);
    }
}
function setIn1Dats(in1Datas) {
    // Set Payer fields
    if (in1Datas.Payer.BillTo != undefined) { setCheckboxVal('form[name=in1-form] .billto-options input[type=checkbox]', in1Datas.Payer.BillTo); }
    $('form[name=in1-form] .payer-fields input[type=text]').each(function (key, value) {
        if (in1Datas.Payer[value.name] != undefined) { value.value = in1Datas.Payer[value.name]; }
    });

    //Set Payer contact fields
    if (in1Datas.PayerContact != undefined) {
        $('form[name=in1-form] .payercontact-fields input[type=text]').each(function (key, value) {
            if (in1Datas.PayerContact[value.name] != undefined) { value.value = in1Datas.PayerContact[value.name]; }
            if ($(value).hasClass("masked-input")) { $(value).val($(value).masked(value.value)); }
        });
        $('form[name=in1-form] .payercontact-fields select').each(function (key, value) {
            if (in1Datas.PayerContact[value.name] != undefined) { value.value = in1Datas.PayerContact[value.name]; }
        });
    }

    //Set Subscriber fields
    if (in1Datas.Subscriber.RelationshiptoSubscriber != undefined) {
        //setCheckboxVal('form[name=in1-form] .subrel-options input[type=checkbox]', in1Datas.Subscriber.RelationshiptoSubscriber);
        $('form[name=in1-form] .subscriber-fields input[name=RelationshiptoSubscriber]').val([in1Datas.Subscriber.RelationshiptoSubscriber]);
    }
    $('form[name=in1-form] .subscriber-fields input[type=text]').each(function (key, value) {
        if (in1Datas.Subscriber[value.name] != undefined) { value.value = in1Datas.Subscriber[value.name]; }
    });
    checkForSelfSubscriber();
}
function setIn2Dats(in2Datas) {
    // Set Payer fields
    if (in2Datas.Payer.BillTo != undefined) { setCheckboxVal('form[name=in2-form] .billto-options input[type=checkbox]', in2Datas.Payer.BillTo); }
    $('form[name=in2-form] .payer-fields input[type=text]').each(function (key, value) {
        if (in2Datas.Payer[value.name] != undefined) { value.value = in2Datas.Payer[value.name]; }
    });

    //Set Payer contact fields
    if (in2Datas.PayerContact != undefined) {
        $('form[name=in2-form] .payercontact-fields input[type=text]').each(function (key, value) {
            if (in2Datas.PayerContact[value.name] != undefined) { value.value = in2Datas.PayerContact[value.name]; }
            console.log(value);
            if ($(value).hasClass("masked-input")) { $(value).val($(value).masked(value.value)); }
        });
        $('form[name=in2-form] .payercontact-fields select').each(function (key, value) {
            if (in2Datas.PayerContact[value.name] != undefined) { value.value = in2Datas.PayerContact[value.name]; }
        });
    }

    //Set Subscriber fields
    if (in2Datas.Subscriber.RelationshiptoSubscriber != undefined) {
        console.log(in2Datas.Subscriber.RelationshiptoSubscriber);
        //setCheckboxVal('form[name=in2-form] .subrel-options input[type=checkbox]', in2Datas.Subscriber.RelationshiptoSubscriber);
        $('form[name=in2-form] .subscriber-fields input[name=RelationshiptoSubscriber]').val([in2Datas.Subscriber.RelationshiptoSubscriber]);
    }
    $('form[name=in2-form] .subscriber-fields input[type=text]').each(function (key, value) {
        if (in2Datas.Subscriber[value.name] != undefined) { value.value = in2Datas.Subscriber[value.name]; }
    });
    checkForSelfSubscriber();
}
function populatePidDatas(pidDatas) {
    //Set Patiend fields
    if (pidDatas.Patient != undefined) {
        $('form[name=pid-form] .patient-fields input[type=text]').each(function (key, value) {
            if (pidDatas.Patient[value.name] != undefined) { value.value = pidDatas.Patient[value.name]; }
            if ($(value).hasClass("masked-input")) { $(value).val($(value).masked(value.value)); }
        });
    }
    $('form[name=pid-form] .patient-fields input[type=radio]').each(function (i, val) {
        if (val.value == pidDatas.Patient.sex) {
            $(this).prop('checked', true);
        }
    });

    //Set Patient contact
    if (pidDatas.PatientContact != undefined) {
        $('form[name=pid-form] .patientcontact-fields input[type=text]').each(function (key, value) {
            if (pidDatas.PatientContact[value.name] != undefined) { value.value = pidDatas.PatientContact[value.name]; }
            if ($(value).hasClass("masked-input")) { $(value).val($(value).masked(value.value)); }
        });
        $('form[name=pid-form] .patientcontact-fields select').each(function (key, value) {
            if (pidDatas.PatientContact[value.name] != undefined) { value.value = pidDatas.PatientContact[value.name]; }
        });
    }

    //Set Demographics fields
    if (pidDatas.Demographics.PatientRace != undefined) { setCheckboxVal('form[name=pid-form] .patientrace-fields input[type=checkbox]', pidDatas.Demographics.PatientRace); }
    if (pidDatas.Demographics.PatientEthnicity != undefined) { setCheckboxVal('form[name=pid-form] .patientethnicity-fields input[type=checkbox]', pidDatas.Demographics.PatientEthnicity); }
    if (pidDatas.Demographics.PatientLang != undefined) { setCheckboxVal('form[name=pid-form] .patientlang-fields input[type=checkbox]', pidDatas.Demographics.PatientLang); }
}
function populatePv1Datas(pv1Datas) {
    if ($('form[name=pv1-form] input[type=text][name=ReferringPhysician]').val.length > 0) { $('form[name=pv1-form] input[type=text][name=ReferringPhysician]').val(pv1Datas.ReferringPhysician); }
    if (pv1Datas.NPI != undefined) {
        $('.npi-field input[type=text]').attr('readonly', 'true');
        $('form[name=pv1-form] input[type=text][name=npi]').removeAttr('readonly');
        $('form[name=pv1-form] input[type=radio][name=uni_id][value=npi]').prop('checked', true);
        $('form[name=pv1-form] input[type=text][name=npi]').val(pv1Datas.NPI);
    }
    else if (pv1Datas.NPI != undefined) {
        $('.npi-field input[type=text]').attr('readonly', 'true');
        $('form[name=pv1-form] input[type=text][name=oth]').removeAttr('readonly');
        $('form[name=pv1-form] input[type=radio][name=uni_id][value=oth]').prop('checked', true);
        $('form[name=pv1-form] input[type=text][name=oth]').val(pv1Datas.OTHERS);
    }
}
function populateFt1Datas(ft1Datas) {
    //Set FT1 Fields
    if (ft1Datas.TestOption != undefined) { setCheckboxVal('form[name=ft1-form] .testoptions-fields input[type=checkbox]', ft1Datas.TestOption); }
    if (ft1Datas.PointOfCareTestResults != undefined) { setCheckboxVal('form[name=ft1-form] .poc-fields input[type=checkbox]', ft1Datas.PointOfCareTestResults); }

    //Set LC/MS/MS Testing, Confirm fields
    if (ft1Datas.LCMSMSTest != undefined) {
        if (ft1Datas.LCMSMSTest.OpiatesGroup != undefined) { setCheckboxVal('form[name=ft1-form] .OpiatesGroup-fields input[type=checkbox]', ft1Datas.LCMSMSTest.OpiatesGroup); }
        if (ft1Datas.LCMSMSTest.SyntheticOpioidsGroup != undefined) { setCheckboxVal('form[name=ft1-form] .SyntheticOpioidsGroup-fields input[type=checkbox]', ft1Datas.LCMSMSTest.SyntheticOpioidsGroup); }
        if (ft1Datas.LCMSMSTest.BenzodiazepinesGroup != undefined) { setCheckboxVal('form[name=ft1-form] .BenzodiazepinesGroup-fields input[type=checkbox]', ft1Datas.LCMSMSTest.BenzodiazepinesGroup); }
        if (ft1Datas.LCMSMSTest.OthersGroup != undefined) { setCheckboxVal('form[name=ft1-form] .OthersGroup-fields input[type=checkbox]', ft1Datas.LCMSMSTest.OthersGroup); }
        if (ft1Datas.LCMSMSTest.AntiDepressantsGroup != undefined) { setCheckboxVal('form[name=ft1-form] .AntiDepressantsGroup-fields input[type=checkbox]', ft1Datas.LCMSMSTest.AntiDepressantsGroup); }
        if (ft1Datas.LCMSMSTest.AntiPsychoticGroup != undefined) { setCheckboxVal('form[name=ft1-form] .AntiPsychoticGroup-fields input[type=checkbox]', ft1Datas.LCMSMSTest.AntiPsychoticGroup); }
        if (ft1Datas.LCMSMSTest.DrugsOfAbuseGroupGroup != undefined) { setCheckboxVal('form[name=ft1-form] .DrugsOfAbuseGroupGroup-fields input[type=checkbox]', ft1Datas.LCMSMSTest.DrugsOfAbuseGroupGroup); }
        if (ft1Datas.LCMSMSTest.AdditionalTesting != undefined) { setCheckboxVal('form[name=ft1-form] .AdditionalTesting-fields input[type=checkbox]', ft1Datas.LCMSMSTest.AdditionalTesting); }
    }
}
function setCheckboxVal(ele, dataArr) {
    $(ele).each(function (i, val) {
        if ($.inArray(val.value, dataArr) != '-1') {
            $(this).prop('checked', true);
        }
    });
}
function getAllcheckboxes(ele) {
    var res = [];
    $(ele).each(function (key, value) {
        res.push(value.value);
    });
    return res;
}
function loadPatientsModal(type) {
    var searchParams
    if (type == '1') {
        searchParams = {
            "Username": $('form[name=pid-form] input[type=text][name=PatientLastName]').val(),
            "Searchtype": type
        };
    }
    else if (type == '2') {
        searchParams = {
            "Username": $('form[name=pid-form] input[type=text][name=PatientFirstName]').val(),
            "Searchtype": type
        };
    }
    //******AJAX CALL HERE*******//

    $.ajax({
        type: 'GET',
        url: resolveUrl('~/Transaction/GetPatientDetails'),
        dataType: "json",
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: searchParams,
        async: true,
        contentType: 'application/json; charset=utf-8',
        beforeSend: function () {
            $('#ajax_loader').show();
        },
        complete: function () {
            $('#ajax_loader').hide();
        },
        success: function (data) {
            console.log(data);
            renderPatientTable(data.aaData);
        }
    });
    //renderPatientTable(patientData);
}
function renderPatientTable(patientData) {
    var pTable = $("#tblPatients").dataTable({
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
        "aaData": [],
        "bRetrieve": true,
        "bDestroy": true,
        //"fnServerParams": pushServerParameters,
        "bSortable": false,
        "bAutoWidth": false,
        "aoColumnDefs": [
            {
                "bSortable": false,
                "aTargets": [0],
                "mData": "PatientId",
                "mRender": function (data, type, full) {
                    return "<input type='radio' class='selectedPatient' name='selectedPatient' value='" + data + "'>";
                }
            },

            {
                "bSortable": false,
                "aTargets": [1],
                "mData": "PatientLastName"
            },
            {
                "bSortable": false,
                "aTargets": [2],
                "mData": "PatientFirstName"
            },
            {
                "bSortable": false,
                "aTargets": [3],
                "mData": "DOB",
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
                "mData": "Sex",
                "mRender": function (data, type, full) {
                    if (data == "1") {
                        return "Male";
                    }
                }
            }
        ]
    });
    $("#tblPatients").dataTable().fnClearTable();
    $("#tblPatients").dataTable().fnAddData(patientData);
    //$('#tblPatients').dataTable().fnDraw()
    $("#selectPatientPopup").modal("show");
}
function submitSelectedPatient() {
    var resData = {
        "aaData": [
            {
                "_PatientDetails": {
                    "DOB": "/Date(-54365400000)/",
                    "PatientId": "123456",
                    "PatientLastName": "lastname",
                    "PatientFirstName": "First Name",
                    "Sex": "Male"
                },
                "_PayerAddress": {
                    "Address1": "Payer Address1",
                    "Address2": "Payer Address2",
                    "City": "New York",
                    "State": "CA",
                    "Zip": "12345",
                    "Phone": "12345"
                },
                "_subscriber": {
                    "SubscriberRelationship": "Self",
                    "SubscriberLastName": "Subscriber last name",
                    "SubscriberFirstName": "Subscriber first name",
                    "SubscriberInsuranceId": "INS1000",
                    "SubscriberGroupId": "GR1000"
                },

                "PayerId": "Anthem",
                "PayerName": "Anthem BCBS"
            }
        ]
    }
    var selectedPid = $('form[name=pid-pick-form] input[type=radio][name=selectedPatient]:checked').val();
    console.log(selectedPid);
    if (selectedPid != undefined) {
        var searchParams = {
            "PatientId": selectedPid,
        };
        $.ajax({
            type: 'GET',
            url: resolveUrl('~/Transaction/GetPatientInsurance'),
            dataType: "json",
            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
            data: searchParams,
            async: true,
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                $('#ajax_loader').show();
            },
            complete: function () {
                $('#ajax_loader').hide();
            },
            success: function (data) {
                console.log(data);
                populatePayerInsurance(data.aaData);
                $("#selectPatientPopup").modal("hide");
            }
        });
    }
}
function populatePayerInsurance(data) {
    if (data[0].Patients != undefined) {
        console.log(data[0].Patients);
        $('form[name=pid-form] .patient-fields input[name= PatientID]').val(data[0].Patients.PatientId);
        $('form[name=pid-form] .patient-fields input[name= PatientSSN]').val('XXXXXX' + data[0].Patients.PatientSSN);
        $('form[name=pid-form] .patient-fields input[name= PatientSSN]').mask('AAA-AAA-AAAA');
        $('form[name=pid-form] .patient-fields input[name= PatientFirstName]').val(data[0].Patients.PatientFirstName);
        $('form[name=pid-form] .patient-fields input[name= PatientLastName]').val(data[0].Patients.PatientLastName);
        $('form[name=pid-form] .patient-fields input[name= DOB]').val(data[0].Patients.DOB);
        var receivedDate = data[0].Patients.DOB;
        var r = /\/Date\(([0-9]+)\)\//i;
        var matches = receivedDate.match(r);
        if (matches.length == 2) {
            var date = new Date(parseInt(matches[1]));
            $('form[name=pid-form] .patient-fields input[name= DOB]').val((date.getMonth() + 1) + "/" + (date.getDate()) + "/" + date.getFullYear());
        }

        $('form[name=pid-form] .patient-fields input[name= sex][value= 2]').prop('checked', true);
        $('form[name=pid-form] .patientcontact-fields input[name= PatientAddressLine1]').val(data[0].Patients.PatientAddress.Address1);
        $('form[name=pid-form] .patientcontact-fields input[name= PatientAddressLine2]').val(data[0].Patients.PatientAddress.Address2);
        $('form[name=pid-form] .patientcontact-fields input[name= PatientCity]').val(data[0].Patients.PatientAddress.City);
        $('form[name=pid-form] .patientcontact-fields input[name= PatientState]').val(data[0].Patients.PatientAddress.State);
        $('form[name=pid-form] .patientcontact-fields input[name= PatientZIP]').val(data[0].Patients.PatientAddress.Zip);
        $('form[name=pid-form] .patientcontact-fields input[name= PatientPhone]').val(data[0].Patients.PatientAddress.phone);
        $('form[name=pid-form] .patient-fields input[name=PatientFirstName]').prop("readonly", true);
        $('form[name=pid-form] .patient-fields input[name=PatientLastName]').prop("readonly", true);
        $('form[name=pid-form] .patient-fields input[name=DOB]').prop("readonly", true);
        $('form[name=pid-form] .patient-fields input[name=sex]').prop("readonly", true);
    }
    if (data[0] != undefined) {
        //IN1 Section Binding
        var insuranceDatas = {
            "Payer": {
                "BillTo": "",
                "PayerID": data[0].Payer.PayerId,
                "PayerName": data[0].Payer.PayerName
            },
            "PayerContact": {
                "PayerAddressLine1": data[0].Payer.PayerAddress.Address1,
                "PayerAddressLine2": data[0].Payer.PayerAddress.Address2,
                "PayerCity": data[0].Payer.PayerAddress.City,
                "PayerState": data[0].Payer.PayerAddress.State,
                "PayerZIP": data[0].Payer.PayerAddress.Zip,
                "PayerPhone": data[0].Payer.PayerAddress.Phone
            },
            "Subscriber": {
                "RelationshiptoSubscriber": data[0].Subscriber.SubscriberRelationship,
                "SubscriberFirstName": data[0].Subscriber.SubscriberFirstName,
                "SubscriberLastName": data[0].Subscriber.SubscriberLastName,
                "InsuranceID": data[0].Subscriber.SubscriberInsuranceId,
                "GroupID": data[0].Subscriber.SubscriberGroupId
            }
        };
        setIn1Dats(insuranceDatas);
    }
    if (data[1] != undefined) {
        //removeInsurance(2);
        $('#in2-tab').parent().remove();
        $('#in2tab').remove();
        var tpl = getInsTpl(2);
        $('<li class="nav-item nav-item-btn"> <a class="nav-link insurance-tabs" id="in' + 2 + '-tab" data-toggle="tab" href="#in' + 2 + 'tab" role="tab" aria-controls="contact" aria-selected="false">IN ' + 2 + '</a><button class="cycle-button mini-button danger" onclick="removeInsurance(' + 2 + ');"><span class="mif-minus"></span></button></li>').insertAfter($('#in' + (2 - 1) + '-tab').parent());
        $(tpl).appendTo('.custom-tab-content');
        $('form[name=in2-form] .payercontact-fields input[type=text][name=PayerZIP]').mask('00000');
        $('form[name=in2-form] .payercontact-fields input[type=text][name=PayerPhone]').mask('AAA-AAA-AAAA');
        //IN2 Section Binding
        var insuranceDatas = {
            "Payer": {
                "BillTo": "",
                "PayerID": data[1].Payer.PayerId,
                "PayerName": data[1].Payer.PayerName
            },
            "PayerContact": {
                "PayerAddressLine1": data[1].Payer.PayerAddress.Address1,
                "PayerAddressLine2": data[1].Payer.PayerAddress.Address2,
                "PayerCity": data[1].Payer.PayerAddress.City,
                "PayerState": data[1].Payer.PayerAddress.State,
                "PayerZIP": data[1].Payer.PayerAddress.Zip,
                "PayerPhone": data[1].Payer.PayerAddress.Phone
            },
            "Subscriber": {
                "RelationshiptoSubscriber": data[1].Subscriber.SubscriberRelationship,
                "SubscriberFirstName": data[1].Subscriber.SubscriberFirstName,
                "SubscriberLastName": data[1].Subscriber.SubscriberLastName,
                "InsuranceID": data[1].Subscriber.SubscriberInsuranceId,
                "GroupID": data[1].Subscriber.SubscriberGroupId
            }
        };
        setIn2Dats(insuranceDatas);
    }
}
function checkForSelfSubscriber() {
    if ($('form[name=in1-form] .subscriber-fields input[name=RelationshiptoSubscriber]:checked').val() == '800000000') {
        alert('Patient itself as subscriber 1');
        //****** FILLING PID FORM  DATAS*********
        $('form[name=pid-form] .patient-fields input[name=PatientFirstName]').val($('form[name=in1-form] .subscriber-fields input[name=SubscriberFirstName]').val());
        $('form[name=pid-form] .patient-fields input[name=PatientLastName]').val($('form[name=in1-form] .subscriber-fields input[name=SubscriberLastName]').val());
        $('form[name=pid-form] .patient-fields input[name=PatientFirstName]').prop("readonly", true);
        $('form[name=pid-form] .patient-fields input[name=PatientLastName]').prop("readonly", true);
    }
    else if ($('form[name=in2-form] .subscriber-fields input[name=RelationshiptoSubscriber]:checked').val() == '800000000') {
        alert('Patient itself as subscriber 2');
        //****** FILLING PID FORM  DATAS*********
        $('form[name=pid-form] .patient-fields input[name=PatientFirstName]').val($('form[name=in2-form] .subscriber-fields input[name=SubscriberFirstName]').val());
        $('form[name=pid-form] .patient-fields input[name=PatientLastName]').val($('form[name=in2-form] .subscriber-fields input[name=SubscriberLastName]').val());
        $('form[name=pid-form] .patient-fields input[name=PatientFirstName]').prop("readonly", true);
        $('form[name=pid-form] .patient-fields input[name=PatientLastName]').prop("readonly", true);
    }
    else {
        $('form[name=pid-form] .patient-fields input[name=PatientFirstName]').prop("readonly", false);
        $('form[name=pid-form] .patient-fields input[name=PatientLastName]').prop("readonly", false);
    }
}
function getSSN() {
    var searchParams = {
        "PatientId": $('form[name=pid-form] .patient-fields input[name=PatientID]').val(),
    };
    $.ajax({
        type: 'GET',
        url: resolveUrl('~/Transaction/GetSSN'),
        dataType: "json",
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: searchParams,
        async: true,
        contentType: 'application/json; charset=utf-8',
        beforeSend: function () {
            $('#ajax_loader').show();
        },
        complete: function () {
            $('#ajax_loader').hide();
        },
        success: function (data) {
            console.log(data);
            $('form[name=pid-form] .patient-fields input[name=PatientSSN]').val(data.aaData);
            $('form[name=pid-form] .patient-fields input[name=PatientSSN]').mask('000-000-0000');
        }
    });
}
$(function () {
    AVUTOXPortal.Getguid();
    AVUTOXPortal.User.CheckandGetOrderJson();

    // AVUTOXPortal.List();
    //  AVUTOXPortal.GetAckRection();
});
$(document).ready(function () {
    $("form[name=in1-form] .subscriber-fields input[name=RelationshiptoSubscriber]").on('change', function () {
        checkForSelfSubscriber();
    });
    $(document).on("change", "form[name=in2-form] .subscriber-fields input[name=RelationshiptoSubscriber]", function () {
        checkForSelfSubscriber();
    });
    $("form[name=in1-form] .subscriber-fields input[type=text]").on('change', function () {
        if ($("form[name=in1-form] .subscriber-fields input[name=RelationshiptoSubscriber]").val() == '800000000') {
            $('form[name=pid-form] .patient-fields input[name=PatientFirstName]').val($('form[name=in1-form] .subscriber-fields input[name=SubscriberFirstName]').val());
            $('form[name=pid-form] .patient-fields input[name=PatientLastName]').val($('form[name=in1-form] .subscriber-fields input[name=SubscriberLastName]').val());
        }
    });
    $(document).on("change", "form[name=in2-form] .subscriber-fields input[type=text]", function () {
        if ($("form[name=in2-form] .subscriber-fields input[name=RelationshiptoSubscriber]").val() == '800000000') {
            $('form[name=pid-form] .patient-fields input[name=PatientFirstName]').val($('form[name=in2-form] .subscriber-fields input[name=SubscriberFirstName]').val());
            $('form[name=pid-form] .patient-fields input[name=PatientLastName]').val($('form[name=in2-form] .subscriber-fields input[name=SubscriberLastName]').val());
        }
    });
    $('.nav-tabs .nav-item').click(function () {
        var activeTab = $('.nav-tabs .nav-item .nav-link.active').attr('href');
        var activeForm = $(activeTab + ' form').attr('name');
        console.log(activeForm);
        var isValidForm = doValidate(activeForm, '#VALIDATION_MESSAGES');
        if (isValidForm) {
            $('#' + $(this).children(".nav-link").attr('id')).tab('show');
        }
    })

    $('.mask-ssn').mask('000-000-0000');
    $('.mask-phone').mask('AAA-AAA-AAAA');
    $('.mask-zipcode').mask('00000');
});