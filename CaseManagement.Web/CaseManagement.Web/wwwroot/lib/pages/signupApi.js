$(document).ready(function () {
    $('#signUpForm').submit(function (event) {
        console.log("Triggered submit");
        event.preventDefault();

        // Serialize form data
        const formData = $(this).serialize();
        console.log("formData-", formData);

        $.ajax({
            url: apiBaseUrl + "/SignUp/signup",
            type: "POST",
            contentType: "application/x-www-form-urlencoded",
            data: formData,
            success: function (response) {
                console.log("Sign up Success");
                console.log(response);
            },
            error: function (xhr, status, error) {
                console.log("AJAX Request Failed");

                // Check if responseJSON exists
                if (xhr.responseJSON && xhr.responseJSON.errors) {
                    const errors = xhr.responseJSON.errors;
                    let alertMessage = "";

                    for (const [field, messages] of Object.entries(errors)) {
                        alertMessage += ` ${field}:`;
                        messages.forEach((message) => {
                            alertMessage += `- ${message}\n`;
                        });
                    }

                    // Display the alert message
                    if (alertMessage) {
                        alert(alertMessage);
                    }
                }
            }
        });
    });
});
