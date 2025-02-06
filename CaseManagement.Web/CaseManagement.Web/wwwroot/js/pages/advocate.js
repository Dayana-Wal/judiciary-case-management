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
                    <td><button class="btn btn-primary hire-btn" data-id ="${advocate.id}">Hire</button></td>
                </tr>
                `);
        });
    }
    apiRequest(url, type, null, headers, null, handleSuccess, handleError)

    const hireSuccess = (response) => {
        alert(response.Data || "Advocate hired successfully!");
        window.location.href = `/ViewCase/CaseDetails?caseId=${caseId}`;
    }
    $(document).on("click", ".hire-btn", function () {
        const advocateId = $(this).data("id");
        //TODO: Change caseId
        const data = JSON.stringify({
            advocateId: advocateId,
            caseId: caseId
        });


        apiRequest(apiBaseUrl + "/Case/assign-advocate", "POST", "application/json", headers, data, hireSuccess, handleError)
    })
}) 