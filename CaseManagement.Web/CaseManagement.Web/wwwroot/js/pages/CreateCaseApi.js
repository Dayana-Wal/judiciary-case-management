$(document).ready(function () {
    $('#caseCreationForm').on('submit', function (event) {
        event.preventDefault(); // Prevention of default form submission

        if (!$(this).valid()) {
            console.warn("Form validation failed.");
            return;
        }

        const form = this;
        const formData = buildFormData(form);

        sendAjaxRequest(`${apiBaseUrl}/file/upload`, formData, (response) => {
            if (response.status?.toUpperCase() === 'SUCCESS') {
                const fileIds = response.data;
                const jsonData = buildJsonData(form, fileIds);
                sendAjaxRequest(`${apiBaseUrl}/case/create`, JSON.stringify(jsonData), (response) => {
                    if (response.status?.toUpperCase() === 'SUCCESS') {
                        alert(response.message || "Case created successfully!");
                        $('#caseCreationForm')[0].reset(); // Clear form fields
                    } else {
                        handleErrorResponse(response);
                    }
                }, handleErrorResponse(response), "application/json");
            }
            else {
                handleErrorResponse(response)
            }
        }, handleErrorResponse(response), false, false);
    });

    function buildJsonData() {
        constjsonData = {};
        $(form).serializeArray().forEach(item => {
            jsonData[item.name] = item.value || null; // Handle empty fields
        });
        jsonData.FileIds = fileIds;
        return jsonData;
    }

    function buildFormData(form) {
        const formData = new FormData();
        const files = $(form).find('input[name="CaseFiles"]')[0].files;

        for (let i = 0; i < files.length; i++) {
            formData.append('Files', files[i])
        }

        //TODO: Pass the uploadedBy and fileTypeId
        formData.append('uploadedBy', "anudeepthi");
        formData.append('fileTypeId', 18);
        return formData;
    }

    //Reusable AJAX request function
    function sendAjaxRequest(url, data, successCallback, errorCallback, processData = true, contentType = "application/www-formData") {
        $.ajax({
            url: url,
            type: "POST",
            data: data,
            processData: processData,
            contentType: contentType,
            success: successCallback,
            error: errorCallback
        });
    }
    // Common error handler
    function handleErrorResponse(xhr) {
        console.error("AJAX request failed:", xhr);

        let alertMessage = "An error occurred. Please try again.";

        if (xhr.responseJSON) {
            const { message, errors, data } = xhr.responseJSON;

            if (message) {
                alertMessage = `${message}\n`;
            }

            if (errors) {
                const errorMessages = Object.entries(errors)
                    .map(([field, msgs]) => `${field}: ${msgs.join(", ")}`)
                    .join("\n");
                alertMessage += errorMessages;
            }

            if (data) {
                alertMessage += data.join("\n");
            }
        }

        alert(alertMessage);
    }
});

//        for (let [key, value] of formData.entries()) {
//            console.log(`${key}:`, value);
//        }

//        $.ajax({
//            url: `${apiBaseUrl}/file/upload`,
//            type: "POST",
//            data: formData,
//            processData: false,
//            contentType: false,
//            success: function (response) {
//                if (response.status?.toUpperCase() === 'SUCCESS') {
//                    let fileIds = response.data
//                    const jsonData = {};
//                    $(form).serializeArray().forEach(item => {
//                        jsonData[item.name] = item.value || null; // Handle empty fields
//                    });
//                    jsonData.FileIds = fileIds;
//                    $.ajax({
//                        url: `${apiBaseUrl}/Case/create`, // API endpoint for case creation
//                        type: "POST",
//                        contentType: "application/json",
//                        data: JSON.stringify(jsonData),
//                        success: function (response) {
//                            if (response.status?.toUpperCase() === 'SUCCESS') {
//                                alert(response.message || "Case created successfully!");
//                                $('#caseCreationForm')[0].reset(); // Clear form fields
//                            } else {
//                                console.warn("API response received but not successful:", response);
//                                alert(response.message || "Something went wrong. Please try again.");
//                            }
//                        },
//                        error: function (xhr) {
//                            console.error("AJAX request failed:", xhr);

//                            let alertMessage = "An error occurred. Please try again. ";

//                            // Extract detailed error messages, if available
//                            if (xhr.responseJSON) {
//                                const { message, errors, data } = xhr.responseJSON;

//                                if (message) {
//                                    alertMessage = `${message}\n`;
//                                }

//                                if (errors) {
//                                    const errorMessages = Object.entries(errors)
//                                        .map(([field, msgs]) => `${field}: ${msgs.join(", ")}`)
//                                        .join("\n");
//                                    alertMessage += errorMessages;
//                                }

//                                if (data) {
//                                    alertMessage += data.join("\n");
//                                }
//                            }

//                            alert(alertMessage);
//                        }
//                    });

//                    //$('#caseCreationForm')[0].reset(); // Clear form fields
//                } else {
//                    console.warn("API response received but not successful:", response);
//                    alert(response.message || "Something went wrong. Please try again.");
//                }
//            },
//            error: function (xhr) {
//                console.error("AJAX request failed:", xhr);

//                let alertMessage = "An error occurred. Please try again. ";

//                // Extract detailed error messages, if available
//                if (xhr.responseJSON) {
//                    const { message, errors, data } = xhr.responseJSON;

//                    if (message) {
//                        alertMessage = `${message}\n`;
//                    }

//                    if (errors) {
//                        const errorMessages = Object.entries(errors)
//                            .map(([field, msgs]) => `${field}: ${msgs.join(", ")}`)
//                            .join("\n");
//                        alertMessage += errorMessages;
//                    }

//                    if (data) {
//                        alertMessage += data.join("\n");
//                    }
//                }

//                alert(alertMessage);
//            }
//        })
//    });
//});
