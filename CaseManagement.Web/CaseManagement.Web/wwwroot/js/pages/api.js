const apiRequest = (url, type, contentType, headers, data, successCallBack, errorCallBack) => {
    $.ajax({
        url: url,
        type: type,
        headers: headers,
        contentType: contentType,
        data: data,
        success: function (response) {
            if (successCallBack) {
                successCallBack(response)
            }
        },
        error: function (xhr, status, error) {
            if (xhr.responseJSON &&
                xhr.responseJSON.status === "Failed" &&
                xhr.responseJSON.message.includes("token is expired")) {
                handleTokenExpiration();
                return;
            }
            if (errorCallBack) {
                errorCallBack(xhr,status,error)
            }
        }

    });
}

const handleError = (xhr, status, error) => {
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

const handleTokenExpiration = () => {
    alert("Your session has expired. Please log in again.");
    removeToken();
    window.location.href = "/User/Login";
};