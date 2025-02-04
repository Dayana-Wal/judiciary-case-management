$(document).ready(function () {
    const resultsContainer = $('#searchResults');

    // Function to fetch cases based on search criteria
    function fetchCases(searchCategory, searchValue) {
        const token = getToken();
        console.log("token", token);

        if (!token) {
            alert("You need to log in to view cases.");
            window.location.href = "/User/Login"; // Redirect to the login page
            return;
        }

        // If no search category or value, fetch all cases (no filter)
        const queryString = (searchCategory && searchValue) ? `?SearchCategory=${searchCategory}&SearchValue=${searchValue}` : `?SearchCategory=&SearchValue=`;

        // Perform AJAX GET request for cases
        $.ajax({
            url: `${apiBaseUrl}/CaseSearch/search${queryString}`,
            type: "GET",
            headers: {
                Authorization: `Bearer ${token}`
            },
            success: function (response) {
                if (response.status.toUpperCase() === 'SUCCESS') {
                    if (response.data && response.data.length > 0) {
                        displayResults(response.data); // Display results in a table
                    } else {
                        displayNoResults(); // No results found
                    }
                } else {
                    alert(response.message || 'An error occurred while fetching the data.');
                }
            },
            error: function (xhr) {
                console.error('AJAX Request Failed:', xhr);
                let alertMessage = 'An error occurred while fetching the data.';
                if (xhr.responseJSON && xhr.responseJSON.message) {
                    alertMessage = xhr.responseJSON.message;
                }
                alert(alertMessage);
            }
        });
    }

    // Handle form submission for filtered search
    $('#searchForm').submit(function (event) {
        event.preventDefault(); // Prevent default form submission

        const searchCategory = $('#searchCategory').val();
        const searchValue = $('#searchValue').val();

        // Clear previous results
        resultsContainer.empty();

        fetchCases(searchCategory, searchValue);
    });

    // Function to display search results in a table format
    function displayResults(results) {
        const table = $('<table class="table table-bordered table-hover"></table>');
        const thead = $('<thead></thead>');
        const tbody = $('<tbody></tbody>');

        // Define columns
        const columns = ['Case Number', 'Victim Name', 'Accused Name', 'Advocate Name', 'Case Status', 'DateOfIncident'];
        const headerRow = $('<tr></tr>');
        columns.forEach(col => headerRow.append(`<th>${col}</th>`));
        thead.append(headerRow);
        table.append(thead);

        // Append rows based on response data
        results.forEach(result => {
            //console.log(result)
            console.log(result.id);
            const row = $('<tr></tr>');
            columns.forEach(col => {
                row.append(`<td>${getValueForColumn(col, result)}</td>`);
            });
            // Add "View" icon in the last column
            const viewCell = $('<td></td>');
            const viewIcon = $('<i class="fas fa-eye" style="cursor: pointer;"></i>');
            viewIcon.on('click', function () {
                const caseId = result.id;
                window.location.href = `/ViewCase/CaseDetails?caseId=${caseId}`;
            });
            viewCell.append(viewIcon);
            row.append(viewCell);
            tbody.append(row);
        });

        table.append(tbody);
        resultsContainer.append(table);
    }

    // Function to display no results found
    function displayNoResults() {
        resultsContainer.empty();
        resultsContainer.append('<div class="alert alert-warning">No results found.</div>');
    }

    // Helper function to get the value for a specific column
    function getValueForColumn(column, result) {
        switch (column) {
            case 'Case Number': return result.caseNumber || 'N/A';
            case 'Victim Name': return result.victim?.name || 'N/A';
            case 'Accused Name': return result.accused?.name || 'N/A';
            case 'Advocate Name': return result.advocate?.name || 'N/A';
            case 'Case Status': return result.caseStatus?.text || 'N/A';
            case 'DateOfIncident':
                return result.dateOfIncident
                    ? new Date(result.dateOfIncident).toLocaleDateString()
                    : 'N/A';
            default: return 'N/A';
        }
    }
    fetchCases();
});
