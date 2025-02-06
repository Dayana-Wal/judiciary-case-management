$(document).ready(function () {
    const urlParams = new URLSearchParams(window.location.search);
    const caseId = urlParams.get('caseId');

    // Check if caseId is valid
    if (!caseId) {
        alert('Case ID is missing.');
        window.location.href = '/CaseSearch/search';
        return;
    }
    const token = getToken();
    console.log("caseid", caseId)
    // Fetch and render case details
    const url = `${apiBaseUrl}/ViewCase?CaseId=${caseId}`
    const type = "GET";
    const headers = {
        Authorization: `Bearer ${token}`
    }
    const handleSuccess = (response) => {
        if (response.status.toUpperCase() === 'SUCCESS') {
            console.log(response)
            const data = response.data;
            if (!data) {
                alert('No case found.');
                window.location.href = '/CaseSearch/search';
                return;
            }

            // Map data to fields in the page
            $('#caseNumber').text(data.caseNumber);
            $('#caseStatus').text(data.caseStatus);
            $('#caseType').text(data.caseType);
            $('#dateOfIncident').text(data.dateOfIncident);
            $('#description').text(data.description);

            // Victim details
            $('#victimName').text(data.victim?.name || 'N/A');
            $('#victimEmail').text(data.victim?.email || 'N/A');
            $('#victimContact').text(data.victim?.contact || 'N/A');

            // Accused details
            $('#accusedName').text(data.accused?.name || 'N/A');
            $('#accusedEmail').text(data.accused?.email || 'N/A');
            $('#accusedContact').text(data.accused?.contact || 'N/A');

            // Advocate details
            $('#advocateName').text(data.advocate?.name || 'N/A');
            $('#advocateEmail').text(data.advocate?.email || 'N/A');
            $('#advocateContact').text(data.advocate?.contact || 'N/A');

            if (!data.advocate) {
                $('#hireAdvocateBtn').show();
            }

        }

    }

    apiRequest(url, type, null, headers, null, handleSuccess, handleError)

    $('#hireAdvocateBtn').on('click', function () {
        const advocateUrl = `/Advocate/Details?caseId=${caseId}`;
        window.location.href = advocateUrl;
    });
});
