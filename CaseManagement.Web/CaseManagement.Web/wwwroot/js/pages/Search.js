$(document).ready(function () {
    $('#searchForm').submit(function (event) {
        event.preventDefault(); // Prevent default form submission

        // Serialize form data
        const formData = $(this).serialize();
        const searchCategory = $('#searchCategory').val();
        const searchValue = $('#searchValue').val();

        // Clear previous results
        const resultsContainer = $('#searchResults');
        resultsContainer.empty();

        // Perform AJAX GET request
        $.ajax({
            url: `${apiBaseUrl}/CaseSearch/search?${formData}`, // API endpoint with query params
            type: "GET",
            success: function (response) {
                if (response.status.toUpperCase() === 'SUCCESS') {
                    if (response.data && response.data.length > 0) {
                        // Create a table to display results
                        const table = $('<table class="table table-bordered table-hover"></table>');
                        const thead = $('<thead></thead>');
                        const tbody = $('<tbody></tbody>');

                        // set common columns based on search category
                        const columns = ['Case Number', 'Victim Name', 'Accused Name', 'Advocate Name', 'Case Status'];
                        const headerRow = $('<tr></tr>');
                        columns.forEach(col => headerRow.append(`<th>${col}</th>`));
                        thead.append(headerRow);
                        table.append(thead);

                        // Append rows based on response data
                        response.data.forEach(result => {
                            const row = $('<tr></tr>');
                            columns.forEach(col => {
                                row.append(`<td>${getValueForColumn(col, result)}</td>`);
                            });
                            tbody.append(row);
                        });

                        table.append(tbody);
                        resultsContainer.append(table);
                    } else {
                        resultsContainer.append('<div class="alert alert-warning">No results found.</div>');
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
    });

    // Helper function to get the value for a specific column
    function getValueForColumn(column, result) {
        switch (column) {
            case 'Case Number': return result.caseNumber || 'N/A';
            case 'Victim Name': return result.victim?.name || 'N/A';
            case 'Accused Name': return result.accused?.name || 'N/A';
            case 'Advocate Name': return result.advocate?.name || 'N/A';
            case 'Case Status': return result.caseStatus?.text || 'N/A';
            default: return 'N/A';
        }
    }
});
