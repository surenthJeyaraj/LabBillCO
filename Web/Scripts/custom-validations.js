VALIDATION_CONFIG = {
    "FORM_HL7": {
        'OrderDate':
        {
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
    },
    "FORM_REMITTANCE_FILTER": {
        'PayeeName': {
            'required': false,
            'RegEx': 'name',
            'lang': 'Payee Name'
        },
        'PaidAmount': {
            'required': false,
            'RegEx': 'decimal',
            'lang': 'Paid Amount'
        },
        'ReceivedDateTimeFrom': {
            'required': false,
            'RegEx': 'date',
            'lang': 'Received Date, Time From'
        },
        'ReceivedDateTimeTo': {
            'required': false,
            'RegEx': 'date',
            'lang': 'Received Date, Time To'
        },
        'IdetificationCode': {
            'required': false,
            'RegEx': 'alphanumeric',
            'lang': 'Idetification Code'
        }
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
    'decimal': /^(\d+\.?\d{0,9}|\.\d{1,9})$/
};
function doValidate(formName, msgElement, formConfig) {
    var VALIDATION_MESSAGES = [];
    $(msgElement).removeClass("alert alert-danger");
    $(msgElement).html("");
    $('form[name=' + formName + '] input[type=text],form[name=' + formName + '] select').each(function (key, value) {
        if (VALIDATION_CONFIG[formConfig][value.name] != undefined) {
            // VALIDATE FOR REQUIRED ATTR
            if (VALIDATION_CONFIG[formConfig][value.name].required == true && value.value.length == 0) {
                VALIDATION_MESSAGES.push(VALIDATION_CONFIG[formConfig][value.name].lang + ' is required.');
            }

            // VALIDATE FOR REQUIRED IF PARENT HAS VALUE
            if (VALIDATION_CONFIG[formConfig][value.name].requiredIf != undefined) {
                if (($('form[name=' + formName + '] input[name=' + VALIDATION_CONFIG[formConfig][value.name].requiredIf + ']').val().length > 0) && value.value.length == 0) {
                    VALIDATION_MESSAGES.push(VALIDATION_CONFIG[formConfig][value.name].lang + ' is required.');
                }
            }

            //VALIDATE FOR REGEX
            switch (VALIDATION_CONFIG[formConfig][value.name].RegEx) {
                case 'name':
                    if (value.value.length > 0 && ((REGEX_CONFIG.name).test(value.value) == false)) {
                        VALIDATION_MESSAGES.push('Please enter valid ' + VALIDATION_CONFIG[formConfig][value.name].lang + '.');
                    }
                    else if (value.value.length > 80) {
                        VALIDATION_MESSAGES.push(VALIDATION_CONFIG[formConfig][value.name].lang + ' length should not reach 80 characters');
                    }
                    break;
                case 'phone':
                    if (value.value.length > 0 && ((REGEX_CONFIG.phone).test($(value).cleanVal(value.value)) == false)) {
                        VALIDATION_MESSAGES.push('Please enter valid ' + VALIDATION_CONFIG[formConfig][value.name].lang + '.');
                    }
                    break;
                case 'address':
                    if (value.value.length > 0 && ((REGEX_CONFIG.address).test(value.value) == false)) {
                        VALIDATION_MESSAGES.push('Please enter valid ' + VALIDATION_CONFIG[formConfig][value.name].lang + '.');
                    }
                    else if (value.value.length > 150) {
                        VALIDATION_MESSAGES.push(VALIDATION_CONFIG[formConfig][value.name].lang + ' length should not reach 150 characters');
                    }
                    break;
                case 'zipcode':
                    if (value.value.length > 0 && ((REGEX_CONFIG.zipcode).test(value.value) == false)) {
                        VALIDATION_MESSAGES.push('Please enter valid ' + VALIDATION_CONFIG[formConfig][value.name].lang + '.');
                    }
                    break;
                case 'date':
                    if (value.value.length > 0 && ((REGEX_CONFIG.date).test(value.value) == false)) {
                        VALIDATION_MESSAGES.push('Please enter valid ' + VALIDATION_CONFIG[formConfig][value.name].lang + '.');
                    }
                    break;
                case 'ssn':
                    if (value.value.length > 0 && ((REGEX_CONFIG.ssn).test($(value).cleanVal(value.value)) == false)) {
                        VALIDATION_MESSAGES.push('Please enter valid ' + VALIDATION_CONFIG[formConfig][value.name].lang + '.');
                    }
                    break;
                case 'numeric':
                    if (value.value.length > 0 && ((REGEX_CONFIG.numeric).test(value.value) == false)) {
                        VALIDATION_MESSAGES.push('Please enter valid ' + VALIDATION_CONFIG[formConfig][value.name].lang + '.');
                    }
                    else if (value.value.length < VALIDATION_CONFIG[formConfig][value.name].min || value.value.length > VALIDATION_CONFIG[formConfig][value.name].max) {
                        VALIDATION_MESSAGES.push(VALIDATION_CONFIG[formConfig][value.name].lang + ' length should be between ' + VALIDATION_CONFIG[formConfig][value.name].min + ' and ' + VALIDATION_CONFIG[formConfig][value.name].max);
                    }
                    break;
                case 'decimal':
                    if (value.value.length > 0 && ((REGEX_CONFIG.decimal).test(value.value) == false)) {
                        VALIDATION_MESSAGES.push('Please enter valid ' + VALIDATION_CONFIG[formConfig][value.name].lang + '.');
                    }
                    else if (value.value.length < VALIDATION_CONFIG[formConfig][value.name].min || value.value.length > VALIDATION_CONFIG[formConfig][value.name].max) {
                        VALIDATION_MESSAGES.push(VALIDATION_CONFIG[formConfig][value.name].lang + ' length should be between ' + VALIDATION_CONFIG[formConfig][value.name].min + ' and ' + VALIDATION_CONFIG[formConfig][value.name].max);
                    }
                    break;
                case 'alphabet':
                    if (value.value.length > 0 && ((REGEX_CONFIG.alphabet).test(value.value) == false)) {
                        VALIDATION_MESSAGES.push('Please enter valid ' + VALIDATION_CONFIG[formConfig][value.name].lang + '.');
                    }
                    else if (value.value.length < VALIDATION_CONFIG[formConfig][value.name].min || value.value.length > VALIDATION_CONFIG[formConfig][value.name].max) {
                        VALIDATION_MESSAGES.push(VALIDATION_CONFIG[formConfig][value.name].lang + ' length should be between ' + VALIDATION_CONFIG[formConfig][value.name].min + ' and ' + VALIDATION_CONFIG[formConfig][value.name].max);
                    }
                    break;
                case 'alphanumeric':
                    if (value.value.length > 0 && ((REGEX_CONFIG.alphanumeric).test(value.value) == false)) {
                        VALIDATION_MESSAGES.push('Please enter valid ' + VALIDATION_CONFIG[formConfig][value.name].lang + '.');
                    }
                    else if (value.value.length < VALIDATION_CONFIG[formConfig][value.name].min || value.value.length > VALIDATION_CONFIG[formConfig][value.name].max) {
                        VALIDATION_MESSAGES.push(VALIDATION_CONFIG[formConfig][value.name].lang + ' length should be between ' + VALIDATION_CONFIG[formConfig][value.name].min + ' and ' + VALIDATION_CONFIG[formConfig][value.name].max);
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
        if (VALIDATION_CONFIG[formConfig][value.name] != undefined) {
            // VALIDATE FOR REQUIRED ATTR
            if (VALIDATION_CONFIG[formConfig][value.name].required == true && $('form[name=' + formName + '] input[name=' + value.name + ']:checked').val() == undefined && testedInputs.indexOf(value.name) == -1) {
                VALIDATION_MESSAGES.push(VALIDATION_CONFIG[formConfig][value.name].lang + ' is required.');
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
function doJsonValidate(jsonObj, msgElement) {
    var userSchema = {
        "type": "object",
        "properties": {
            "users": {
                "type": "array", // remember that arrays are objects
                "items": { // "items" represents the items within the "users" array
                    "type": "object",
                    "properties": {
                        "id": { "type": "number" },
                        "username": { "type": "string" },
                        "numPosts": { "type": "number" },
                        "realName": { "type": "string", optional: true }
                    }
                }
            }
        }
    };
    var userData = {
        users: [
            { id: 1, username: "davidwalsh", numPosts: "dsd", realName: "David Walsh" },
            { id: 2, username: "russianprince", numPosts: 12, realName: "Andrei Arshavin" }
        ]
    };
    var hl7Schema = {
        "type": "object",
        "properties": {
            "Order": {
                "type": "object", // remember that arrays are objects
                "properties": {
                    "OrderID": {
                        "type": "number",
                        "errorMessages": {
                            "type": "size should be a number",
                            "minimum": "size should be bigger or equal to 4",
                            "multipleOf": "size should be even"
                        }
                    }
                }
            },
            "PID": {
                "type": "object", // remember that arrays are objects
                "properties": {
                    "Patient": {
                        "type": "object",
                        "properties": {
                            "PatientFirstName": { "type": "string", "pattern": "^[a-zA-Z ]+$" },
                            "PatientLastName": { "type": "string", "pattern": "^[a-zA-Z ]+$" },
                            "DOB": { "type": "string", "pattern": '^(0[1-9]|1[0-2])\/(0[1-9]|1\\d|2\\d|3[01])\/(19|20)\\d{2}$' },
                            "sex": { "type": ["number", "string"] }
                        }
                    },
                }
            }
        }
    };
    var hl7Data = {
        "Order": {
            "OrderID": "SC0000009",
            "OrderDate": "04/26/2018"
        },
        "DG1": {
            "DgCode": [
                "1002"
            ]
        },
        "Insurance": {
            "IN1": {
                "Payer": {
                    "BillTo": [
                        "Medicare"
                    ],
                    "PayerID": "12345676",
                    "PayerName": "BCBSSN"
                },
                "PayerContact": {
                    "PayerAddressLine1": "#1234 West Street",
                    "PayerCity": "NewJercy",
                    "PayerState": "CA",
                    "PayerZIP": "12346",
                    "PayerPhone": "2323232333"
                },
                "Subscriber": {
                    "RelationshiptoSubscriber": "800000000",
                    "SubscriberFirstName": "Miller",
                    "SubscriberLastName": "Tom",
                    "InsuranceID": "232333",
                    "GroupID": "3232"
                }
            }
        },
        "PID": {
            "Patient": {
                "PatientFirstName": "Miller",
                "PatientLastName": "Tom",
                "DOB": "04/26/2018",
                "sex": "1"
            },
            "PatientContact": {
                "PatientAddressLine1": "234",
                "PatientCity": "New York",
                "PatientState": "CA",
                "PatientZIP": "12345"
            },
            "Demographics": {
                "PatientRace": [
                    "option1"
                ],
                "PatientEthnicity": [
                    "option2"
                ],
                "PatientLang": [
                    "option2"
                ]
            }
        },
        "PV1": {
            "ReferringPhysician": "Test User",
            "NPI": "23232323"
        },
        "FT1": {
            "TestOption": [
                "1"
            ],
            "LCMSMSTest": {
                "OpiatesGroup": [
                    "Codeine"
                ],
                "OthersGroup": [
                    "Zolpidem"
                ],
                "AntiPsychoticGroup": [
                    "Aripiprazole",
                    "Clozapine"
                ],
                "DrugsOfAbuseGroupGroup": [
                    "PCP"
                ],
                "AdditionalTesting": [
                    "Norpropoxyphen"
                ]
            }
        }
    };
    var ajv = Ajv({ allErrors: true, jsonPointers: true });
    var isValid = ajv.validate(hl7Schema, hl7Data);
    var errArray = [];
    if (isValid) {
        console.log('User data is valid');
    } else {
        console.log('User data is INVALID!');
        console.log(ajv);
        $.each(ajv.errors, function (key, val) {
            var fieldArr = val.schemaPath.split("/");
            errArray.push(fieldArr[fieldArr.length - 2] + " " + val.message);
        });
        //console.log(errArray);
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