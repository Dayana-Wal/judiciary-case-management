$(document).ready(function () {
    $('#caseCreationForm').on('submit', function (event) {
        event.preventDefault(); // Prevention of default form submission

        if (!$(this).valid()) {
            console.warn("Form validation failed.");
            return;
        }

        console.log("Valid form, preparing to call the API...");

        // Serialize form data into an object
        const formData = {};
        $(this).serializeArray().forEach(item => { formData[item.name] = item.value || null; });// Handle empty fields


        console.log("Serialized form data:", formData);

        // Make the AJAX call
        $.ajax({
            url: `${apiBaseUrl}/CaseCreation`, // API endpoint for case creation
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(formData),
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

                let alertMessage = "An error occurred. Please try again.";

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

                // Display alert with error details
                alert(alertMessage);
            }
        });
    });
});
