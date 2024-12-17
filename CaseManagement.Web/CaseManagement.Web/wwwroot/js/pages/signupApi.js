$(document).ready(function () {
    $('#signUpForm').submit(function (event) {
        event.preventDefault();
        if ($(this).valid()) {
            console.log("Valid form, calling the API")

            // Serialize form data
            const formDataArray = $(this).serializeArray();
            var formData = {}
            formDataArray.forEach(item => formData[item.name] = item.value)
            console.log("formData --",formData)
            $.ajax({
                url: apiBaseUrl + "/SignUp/person",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(formData),
                success: function (response) {
                    if (response.status.toUpperCase() == 'SUCCESS') {
                        alert(response.message)
                        // Reset the form fields
                        $('#signUpForm').trigger("reset");
                    }
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
                        alertMessage = errors.join("\n")
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
