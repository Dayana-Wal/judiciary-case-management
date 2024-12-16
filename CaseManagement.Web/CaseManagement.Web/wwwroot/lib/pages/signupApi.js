﻿$(document).ready(function () {
    $('#signUpForm').submit(function (event) {
        console.log("Triggered submit");
        event.preventDefault();

        // Serialize form data
        const formData = $(this).serialize();
        console.log("formData-", formData);

        $.ajax({
            url: apiBaseUrl + "/SignUp/person",
            type: "POST",
            contentType: "application/x-www-form-urlencoded",
            data: formData,
            success: function (response) {
                alert(response.message)
                console.log(response);
            },
            error: function (xhr, status, error) {
                console.log("AJAX Request Failed");
                console.log(xhr)
                let alertMessage
                if (xhr.responseJSON && xhr.responseJSON.message) {

                    alertMessage = xhr.responseJSON.message

                }
                // Check if responseJSON exists
                if (xhr.responseJSON && xhr.responseJSON.errors && xhr.responseJSON.errors.data) {
                    const errors = xhr.responseJSON.errors.data;
                    alertMessage =  errors.join("\n")
                }
                // Display the alert message
                if (alertMessage) {
                    alert(alertMessage);
                }
            }
        });
    });
});
