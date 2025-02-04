$(document).ready(function () {
    const token = getToken();
    const url = `${apiBaseUrl}/Advocate/view`
    $.ajax({
        url: url,
        type: "GET",
        headers: {
            Authorization: `Bearer ${token}`
        },
        success: function (res) {
            advocateslist = res.data;
            var tableBody = $("#advocatesBody");
            tableBody.empty();

            $.each(advocateslist, function (index, advocate) {
                tableBody.append(`<tr>
                    <td>${advocate.name}</td>
                    <td>${advocate.contact}</td>
                    <td>${advocate.email}</td>
                    <td>${advocate.activeCases} </td>
                    <td><button class="btn btn-primary">Hire</button></td>
                </tr>
                `);
            });
        },
        error: function (xhr, status, error) {
            console.log("AJAX Request Failed");
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

}) 