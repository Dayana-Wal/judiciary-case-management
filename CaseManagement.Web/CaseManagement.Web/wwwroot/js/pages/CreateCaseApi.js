$(document).ready(function () {
    $('#caseCreationForm').on('submit', function (event) {
        event.preventDefault(); // Prevent default form submission

        if (!$(this).valid()) {
            console.warn("Form validation failed.");
            return;
        }
        const doi = new Date($('#DateOfIncident').val())
        const today = new Date()
        if (doi > today) {
            $('#DateOfIncident').addClass('is-invalid'); // Add error class
            return alert("Date of incident should not be in future");
        }
        $('#DateOfIncident').removeClass('is-invalid');
        const token = getToken();
        const user = getUser();
        const currentUser = JSON.parse(user);

        const form = this;
        const formData = buildFormData(form, currentUser);

        sendAjaxRequest(`${apiBaseUrl}/file/upload`, formData,token, (response) => {
            if (response.status?.toUpperCase() === 'SUCCESS') {
                const fileIds = response.data;
                const jsonData = buildJsonData(form, fileIds);

                sendAjaxRequest(`${apiBaseUrl}/case/create`, JSON.stringify(jsonData),token,
                    (response) => {
                        if (response.status?.toUpperCase() === 'SUCCESS') {
                            alert(response.message || "Case created successfully!");
                            $('#caseCreationForm')[0].reset(); // Clear form fields
                        } else {
                            handleErrorResponse(response);
                        }
                    },
                    handleErrorResponse,
                    "application/json"
                );
            } else {
                handleErrorResponse(response);
            }
        }, handleErrorResponse);
    });

    function buildJsonData(form, fileIds) {
        const jsonData = {};
        $(form).serializeArray().forEach(item => {
            jsonData[item.name] = item.value || null; // Handle empty fields
        });
        jsonData.FileIds = fileIds;
        return jsonData;
    }

    function buildFormData(form,currentUser) {
        const formData = new FormData();
        const files = $(form).find('input[name="CaseFiles"]')[0].files;

        for (let i = 0; i < files.length; i++) {
            formData.append('Files', files[i]);
        }

        formData.append('uploadedBy', currentUser.userName);
        formData.append('fileTypeCode', 'CSD');
        return formData;
    }

    // Reusable AJAX request function
    function sendAjaxRequest(url, data, token, successCallback, errorCallback, contentType = false) {
        const isFormData = data instanceof FormData;
        $.ajax({
            url: url,
            type: "POST",
            headers: {
                Authorization: `Bearer ${token}`
            },
            data: data,
            processData: !isFormData,
            contentType: isFormData ? false : contentType,
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
