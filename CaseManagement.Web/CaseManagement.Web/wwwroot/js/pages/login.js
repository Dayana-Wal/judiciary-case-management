$(document).ready(function () {
    $('#loginForm').submit(function (event) {
        event.preventDefault();
        if ($(this).valid()) {
            console.log("Valid form, calling the API")

            // Serialize form data
            const formDataArray = $(this).serializeArray();
            var formData = {}
            formDataArray.forEach(item => formData[item.name] = item.value)
            if (!formData.DateOfBirth) {
                formData.DateOfBirth = null;
            }
            console.log("formData --", formData)
            $.ajax({
                url: apiBaseUrl + "/Login/user",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(formData),
                success: function (response) {
                    if (response.status.toUpperCase() == 'SUCCESS') {
                        alert(response.message)
                        // Reset the form fields
                        $('#loginForm').trigger("reset");
                    }
                },
                error: function (xhr, status, error) {
                    console.log("AJAX Request Failed");
                    console.log(xhr)
                    let alertMessage
                    if (xhr.responseJSON && xhr.responseJSON.message) {

                        alertMessage = xhr.responseJSON.message + "\n"

                    }
                    // Check if responseJSON exists
                    if (xhr.responseJSON && xhr.responseJSON.errors) {
                        const errors = xhr.responseJSON.errors;
                        const errorMessages = [];
                        for (const field in errors) {
                            if (errors[field] && errors[field].length > 0) {
                                errorMessages.push(`${field}: ${errors[field].join(", ")}`);
                            }
                        }

                        // Combine errors into a single alert message
                        if (errorMessages.length > 0) {
                            alertMessage = errorMessages.join("\n");
                        }
                    }
                    if (xhr.responseJSON && xhr.responseJSON.data) {
                        alertMessage += xhr.responseJSON.data.join("\n")
                    }
                    // Display the alert message
                    if (alertMessage) {
                        alert(alertMessage);
                    }
                }
            });
        }
    });
});