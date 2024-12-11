$(document).ready(function () {
    $('#signUpForm').submit(function (event) {
        console.log("Triggered submit");
        event.preventDefault();

        // Serialize form data
        const formData = $(this).serialize();
        console.log("formData-", formData);

        $.ajax({
            url: apiBaseUrl + "/test/index",
            type: "POST",
            contentType: "application/x-www-form-urlencoded",
            data: formData,
            success: function (response) {
                console.log("Sign up Success");
                console.log(response);
            },
            error: function (xhr, status, error) {
                console.log("Error occurred:");
                console.log("XHR:", xhr);
                console.log("Status:", status);
                console.log("Error:", error, xhr?.responseText);
                alert("Error Occurred\n" + xhr?.responseText);
            }
        });
    });
});
