$(document).ready(function () {
    $('#signUpForm').submit(function (event) {
        event.preventDefault();
        if ($(this).valid()) {
            console.log("Valid form, calling the API")
            const dob = new Date($('#DateOfBirth').val());
            const today = new Date();
            if (dob > today) {
                $('#DateOfBirth').addClass('is-invalid'); // Add error class
                return alert("Date of birth should not be in future");
            }
            $('#DateOfBirth').removeClass('is-invalid'); // Add error class
            // Serialize form data
            const formDataArray = $(this).serializeArray();
            var formData = {}
            formDataArray.forEach(item => formData[item.name] = item.value)
            if (!formData.DateOfBirth) {
                formData.DateOfBirth = null;
            }  
            const url = apiBaseUrl + "/SignUp/person";
            const type = "POST";
            const contentType = "application/json";
            const data = JSON.stringify(formData);
            const onSignUpSuccess = (response) => {
                if (response.status.toUpperCase() == 'SUCCESS') {
                    alert(response.message)
                    $('#signUpForm').trigger("reset");
                }
            }
            apiRequest(url, type, contentType, null, data, onSignUpSuccess, handleError);
        }
    });
});