// Function to render a dynamic table with headers, data, and actions
function renderDynamicTable(headersConfig, data, actions) {
    let html = '';

    // Check if headersConfig and data are valid
    if (Array.isArray(headersConfig) && headersConfig.length > 0 && Array.isArray(data)) {
        // Create table header row dynamically
        html += `
            <table class="table table-bordered table-striped">
                <thead style="background-color: #d9edf7;">
                    <tr>
        `;

        // Add each header from headersConfig
        headersConfig.forEach(header => {
            html += `<th>${header.header}</th>`; // header is the display name
        });

        // Add a column for actions
        html += `<th>Actions</th>`;

        html += `</tr></thead><tbody>`;

        // Add each row of data dynamically
        data.forEach(row => {
            html += `<tr>`;
            headersConfig.forEach(header => {
                html += `<td>${row[header.key]}</td>`; // use header.key to get the corresponding data
            });

            // Add actions dynamically
            html += `<td>`;
            actions.forEach(action => {
                html += `<button class="btn btn-sm btn-${action.class}" 
                            onclick="(${action.callback})(this, ${JSON.stringify(row)})">
                            ${action.actionName}
                          </button>`;
            });
            html += `</td>`;

            html += `</tr>`;
        });

        html += `</tbody></table>`;
    } else {
        html = '<p class="text-center">No data available.</p>';
    }

    return html;
}
