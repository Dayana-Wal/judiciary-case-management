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
            const url = apiBaseUrl + "/Login/user";
            const type = "POST";
            const contentType = "application/json"; 
            const data = JSON.stringify(formData);
            const onLoginSuccess = (response) => {
                if (response.status.toUpperCase() == 'SUCCESS') {
                    alert(response.message)

                    //store token and user details in localstorage
                    const token = response.data.token;
                    const user = response.data.user;

                    storeUserSession(token, user);
                    // Redirect based on the role
                    if (user.role.toLowerCase() === 'admin') {
                        window.location.href = '/Admin';
                    } else {
                        window.location.href = '/';
                    }
                }
            } 
            apiRequest(url, type, contentType, null, data, onLoginSuccess, handleError)
        }
    });
});