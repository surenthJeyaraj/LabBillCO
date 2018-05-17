VALIDATION_CONFIG = {
    'OrderDate': {
        'required': true,
        'RegEx': 'date',
        'lang': 'Order Date'
    },
    'PatientFirstName': {
        'required': true,
        'RegEx': 'name',
        'lang': 'Patient First Name'
    },
    'PatientLastName': {
        'required': true,
        'RegEx': 'name',
        'lang': 'Patient Last Name'
    },
    'DOB': {
        'required': true,
        'RegEx': 'date',
        'lang': 'DOB'
    },
    'sex': {
        'required': true,
        'RegEx': 'numeric',
        'min': 1,
        'max': 1,
        'lang': 'Sex'
    },
    'PatientAddressLine1': {
        'required': false,
        'RegEx': 'address',
        'lang': 'Patient Address Line1'
    },
    'PatientAddressLine2': {
        'required': false,
        'RegEx': 'address',
        'lang': 'Patient Address Line2'
    },
    'PatientCity': {
        'requiredIf': 'PatientAddressLine1',
        'RegEx': 'address',
        'lang': 'Patient City'
    },
    'PatientState': {
        'requiredIf': 'PatientAddressLine1',
        'min': 0,
        'max': 50,
        'RegEx': 'alphabet',
        'lang': 'Patient State'
    },
    'PatientZIP': {
        'requiredIf': 'PatientAddressLine1',
        'RegEx': 'zipcode',
        'lang': 'Patient ZIP'
    },
    'PatientPhone': {
        'required': false,
        'RegEx': 'phone',
        'lang': 'Patient Phone'
    },
    'patientRace': {
        'required': false,
        'RegEx': 'alphanumeric',
        'min': 0,
        'max': 10,
        'lang': 'Patient Race'
    },
    'patientEthnicity': {
        'required': false,
        'RegEx': 'alphanumeric',
        'min': 0,
        'max': 10,
        'lang': 'Patient Ethnicity'
    },
    'patientLang': {
        'required': true,
        'RegEx': 'alphanumeric',
        'min': 0,
        'max': 10,
        'lang': 'Patient Language'
    },
    'dg1_code': {
        'required': true,
        'RegEx': 'numeric',
        'min': 0,
        'max': 10,
        'lang': 'Diagnosis Code'
    },
    'PayerID': {
        'required': true,
        'min': 0,
        'max': 10,
        'RegEx': 'alphanumeric',
        'lang': 'Payer ID'
    },
    'PayerName': {
        'required': true,
        'RegEx': 'name',
        'lang': 'Payer Name'
    },
    'PayerAddressLine1': {
        'required': true,
        'RegEx': 'address',
        'lang': 'Payer Address Line1'
    },
    'PayerAddressLine2': {
        'required': false,
        'RegEx': 'address',
        'lang': 'Payer Address Line2'
    },
    'PayerCity': {
        'requiredIf': 'PayerAddressLine1',
        'RegEx': 'address',
        'lang': 'Payer City'
    },
    'PayerState': {
        'requiredIf': 'PayerAddressLine1',
        'min': 0,
        'max': 50,
        'RegEx': 'alphabet',
        'lang': 'Payer State'
    },
    'PayerZIP': {
        'requiredIf': 'PayerAddressLine1',
        'RegEx': 'zipcode',
        'lang': 'Payer ZIP'
    },
    'PayerPhone': {
        'required': true,
        'RegEx': 'phone',
        'lang': 'Payer Phone'
    },
    'RelationshiptoSubscriber': {
        'required': true,
        'RegEx': 'numeric',
        'min': 0,
        'max': 10,
        'lang': 'Relationship to Subscriber'
    },
    'SubscriberFirstName': {
        'requiredIf': 'RelationshiptoSubscriber',
        'RegEx': 'name',
        'lang': 'Subscriber First Name'
    },
    'SubscriberLastName': {
        'requiredIf': 'RelationshiptoSubscriber',
        'RegEx': 'name',
        'lang': 'Subscriber Last Name'
    },
    'InsuranceID': {
        'requiredIf': 'RelationshiptoSubscriber',
        'min': 0,
        'max': 20,
        'RegEx': 'alphanumeric',
        'lang': 'Insurance ID'
    },
    'GroupID': {
        'requiredIf': 'RelationshiptoSubscriber',
        'min': 0,
        'max': 15,
        'RegEx': 'alphanumeric',
        'lang': 'Group ID'
    },
    'ReferringPhysician': {
        'required': true,
        'RegEx': 'name',
        'lang': 'Referring Physician'
    },
    'uni_id': {
        'required': true,
        'min': 0,
        'max': 10,
        'RegEx': 'alphanumeric',
        'lang': 'NPI'
    }
};

var REGEX_CONFIG = {
    'name': /^[a-zA-Z ]+$/,
    'phone': /^\(?(\d{3})\)?[- ]?(\d{3})[- ]?(\d{4})$/,
    'address': /^[ A-Za-z0-9_@./#&,+-]*$/,
    'zipcode': /(^\d{5}$)|(^\d{5}-\d{4}$)/,
    'date': /^(\d{1,2})\/(\d{1,2})\/(\d{4})$/,
    'ssn': /^\(?(\d{3})\)?[- ]?(\d{3})[- ]?(\d{4})$/,
    'gender': /^1?2/,
    'numeric': /^\d+$/,
    'alphabet': /^[a-zA-Z]*$/,
    'alphanumeric': /^[A-Za-z0-9 _.-]+$/,
};
function doValidate(formName, msgElement) {
    var VALIDATION_MESSAGES = [];
    $(msgElement).removeClass("alert alert-danger");
    $(msgElement).html("");
    $('form[name=' + formName + '] input[type=text],form[name=' + formName + '] select').each(function (key, value) {
        if (VALIDATION_CONFIG[value.name] != undefined) {
            // VALIDATE FOR REQUIRED ATTR
            if (VALIDATION_CONFIG[value.name].required == true && value.value.length == 0) {
                VALIDATION_MESSAGES.push(VALIDATION_CONFIG[value.name].lang + ' is required.');
            }

            // VALIDATE FOR REQUIRED IF PARENT HAS VALUE
            if (VALIDATION_CONFIG[value.name].requiredIf != undefined) {
                if (($('form[name=' + formName + '] input[name=' + VALIDATION_CONFIG[value.name].requiredIf + ']').val().length > 0) && value.value.length == 0) {
                    VALIDATION_MESSAGES.push(VALIDATION_CONFIG[value.name].lang + ' is required.');
                }
            }

            //VALIDATE FOR REGEX
            switch (VALIDATION_CONFIG[value.name].RegEx) {
                case 'name':
                    if (value.value.length > 0 && ((REGEX_CONFIG.name).test(value.value) == false)) {
                        VALIDATION_MESSAGES.push('Please enter valid ' + VALIDATION_CONFIG[value.name].lang + '.');
                    }
                    else if (value.value.length > 80) {
                        VALIDATION_MESSAGES.push(VALIDATION_CONFIG[value.name].lang + ' length should not reach 80 characters');
                    }
                    break;
                case 'phone':
                    if (value.value.length > 0 && ((REGEX_CONFIG.phone).test($(value).cleanVal(value.value)) == false)) {
                        VALIDATION_MESSAGES.push('Please enter valid ' + VALIDATION_CONFIG[value.name].lang + '.');
                    }
                    break;
                case 'address':
                    if (value.value.length > 0 && ((REGEX_CONFIG.address).test(value.value) == false)) {
                        VALIDATION_MESSAGES.push('Please enter valid ' + VALIDATION_CONFIG[value.name].lang + '.');
                    }
                    else if (value.value.length > 150) {
                        VALIDATION_MESSAGES.push(VALIDATION_CONFIG[value.name].lang + ' length should not reach 150 characters');
                    }
                    break;
                case 'zipcode':
                    if (value.value.length > 0 && ((REGEX_CONFIG.zipcode).test(value.value) == false)) {
                        VALIDATION_MESSAGES.push('Please enter valid ' + VALIDATION_CONFIG[value.name].lang + '.');
                    }
                    break;
                case 'date':
                    if (value.value.length > 0 && ((REGEX_CONFIG.date).test(value.value) == false)) {
                        VALIDATION_MESSAGES.push('Please enter valid ' + VALIDATION_CONFIG[value.name].lang + '.');
                    }
                    break;
                case 'ssn':
                    if (value.value.length > 0 && ((REGEX_CONFIG.ssn).test($(value).cleanVal(value.value)) == false)) {
                        VALIDATION_MESSAGES.push('Please enter valid ' + VALIDATION_CONFIG[value.name].lang + '.');
                    }
                    break;
                case 'numeric':
                    if (value.value.length > 0 && ((REGEX_CONFIG.numeric).test(value.value) == false)) {
                        VALIDATION_MESSAGES.push('Please enter valid ' + VALIDATION_CONFIG[value.name].lang + '.');
                    }
                    else if (value.value.length < VALIDATION_CONFIG[value.name].min || value.value.length > VALIDATION_CONFIG[value.name].max) {
                        VALIDATION_MESSAGES.push(VALIDATION_CONFIG[value.name].lang + ' length should be between ' + VALIDATION_CONFIG[value.name].min + ' and ' + VALIDATION_CONFIG[value.name].max);
                    }
                    break;
                case 'alphabet':
                    if (value.value.length > 0 && ((REGEX_CONFIG.alphabet).test(value.value) == false)) {
                        VALIDATION_MESSAGES.push('Please enter valid ' + VALIDATION_CONFIG[value.name].lang + '.');
                    }
                    else if (value.value.length < VALIDATION_CONFIG[value.name].min || value.value.length > VALIDATION_CONFIG[value.name].max) {
                        VALIDATION_MESSAGES.push(VALIDATION_CONFIG[value.name].lang + ' length should be between ' + VALIDATION_CONFIG[value.name].min + ' and ' + VALIDATION_CONFIG[value.name].max);
                    }
                    break;
                case 'alphanumeric':
                    if (value.value.length > 0 && ((REGEX_CONFIG.alphanumeric).test(value.value) == false)) {
                        VALIDATION_MESSAGES.push('Please enter valid ' + VALIDATION_CONFIG[value.name].lang + '.');
                    }
                    else if (value.value.length < VALIDATION_CONFIG[value.name].min || value.value.length > VALIDATION_CONFIG[value.name].max) {
                        VALIDATION_MESSAGES.push(VALIDATION_CONFIG[value.name].lang + ' length should be between ' + VALIDATION_CONFIG[value.name].min + ' and ' + VALIDATION_CONFIG[value.name].max);
                    }
                    break;
                default:
                    //console.log('default regex');
            }
            isValid = false;
        }
    });
    var testedInputs = [];
    $('form[name=' + formName + '] input[type=radio],form[name=' + formName + '] input[type=checkbox]').each(function (key, value) {
        if (VALIDATION_CONFIG[value.name] != undefined) {
            // VALIDATE FOR REQUIRED ATTR
            if (VALIDATION_CONFIG[value.name].required == true && $('form[name=' + formName + '] input[name=' + value.name + ']:checked').val() == undefined && testedInputs.indexOf(value.name) == -1) {
                VALIDATION_MESSAGES.push(VALIDATION_CONFIG[value.name].lang + ' is required.');
                testedInputs.push(value.name);
            }
        }
    });
    if (VALIDATION_MESSAGES.length > 0) {
        showValidationErrors(VALIDATION_MESSAGES, msgElement);
        return false;
    }
    else {
        return true;
    }
}
function showValidationErrors(errArr, msgElement) {
    $(msgElement).addClass("alert alert-danger");
    var content = errArr.join('<br>');
    $(msgElement).html(content);
    $('html, body').animate({
        scrollTop: $(msgElement).offset().top - 200
    }, 500);
}