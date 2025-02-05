$(document).ready(function () {
    const token = getToken();
    const url = `${apiBaseUrl}/Advocate/view`;
    const type = "GET";
    const headers = { Authorization: `Bearer ${token}` }

    const handleSuccess = (res) => {
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
    }
    apiRequest(url, type, null, headers, null, handleSuccess, handleError)

}) 