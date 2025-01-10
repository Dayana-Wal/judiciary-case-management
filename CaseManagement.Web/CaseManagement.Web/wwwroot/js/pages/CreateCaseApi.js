$(document).ready(function () {
    $('#caseCreationForm').on('submit', function (event) {
        event.preventDefault(); // Prevention of default form submission

        if (!$(this).valid()) {
            console.warn("Form validation failed.");
            return;
        }

        //My code
        const form = this;
        const formData = new FormData();
        const files = $(form).find('input[name="CaseFiles"]')[0].files;

        for (let i = 0; i < files.length; i++) {
            formData.append('Files',files[i])
        }
        formData.append('uploadedBy',"anudeepthi");
        formData.append('fileTypeId', 18);

        for (let [key, value] of formData.entries()) {
            console.log(`${key}:`, value);
        }

        $.ajax({
            url: `${apiBaseUrl}/file/upload`,
            type: "POST",
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                if (response.status?.toUpperCase() === 'SUCCESS') {
                    let fileIds = response.data
                    const jsonData = {};
                    $(form).serializeArray().forEach(item => {
                        jsonData[item.name] = item.value || null; // Handle empty fields
                    });
                    jsonData.FileIds = fileIds;
                    $.ajax({
                        url: `${apiBaseUrl}/Case/create`, // API endpoint for case creation
                        type: "POST",
                        contentType: "application/json",
                        data: JSON.stringify(jsonData),
                        success: function (response) {
                            if (response.status?.toUpperCase() === 'SUCCESS') {
                                alert(response.message || "Case created successfully!");
                                $('#caseCreationForm')[0].reset(); // Clear form fields
                            } else {
                                console.warn("API response received but not successful:", response);
                                alert(response.message || "Something went wrong. Please try again.");
                            }
                        },
                        error: function (xhr) {
                            console.error("AJAX request failed:", xhr);

                            let alertMessage = "An error occurred. Please try again. ";

                            // Extract detailed error messages, if available
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

                    //$('#caseCreationForm')[0].reset(); // Clear form fields
                } else {
                    console.warn("API response received but not successful:", response);
                    alert(response.message || "Something went wrong. Please try again.");
                }
            },
            error: function (xhr) {
                console.error("AJAX request failed:", xhr);

                let alertMessage = "An error occurred. Please try again. ";

                // Extract detailed error messages, if available
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
        })





        //Previous code
        // Serialize form data into an object
        //const formData = {};



        //console.log("Serialized form data:", formData);

        //// Make the AJAX call
        
    });
});
