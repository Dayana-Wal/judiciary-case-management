$(document).ready(function () {
    let currentPage = 1;
    let pageSize = 5;

    const token = getToken();

    loadUsers(currentPage, pageSize);
    function loadUsers(page, pageSize) {
        $.ajax({
            url: `https://localhost:7123/api/admin/users?PageNumber=${page}&PageSize=${pageSize}`,
            type: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`
            },
            success: function (response) {
                if (response.status === 'Success') {
                    renderUsers(response.data.items, response.data.totalCount, response.data.currentPage);
                } else {
                    alert(response.message || 'An error occurred while fetching users.');
                }
            },
            error: function (xhr) {
                alert('Error fetching users: ' + xhr.responseJSON?.message || 'Please try again.');
            }
        });
    }

    function renderUsers(users, totalCount, currentPage) {
        let html = '<table class="table table-bordered table-striped">';
        html += `<thead><tr>
        <th>User Name</th><th>Name</th><th>Role</th><th>Action</th>
    </tr></thead><tbody>`;

        users.forEach(user => {
            html += `<tr data-user-id="${user.id}" data-role="${user.role}">
            <td>${user.userName}</td>
            <td>${user.name}</td>
            <td>${user.role}</td>
            <td>
                <button class="btn btn-warning edit-btn">Edit</button>
            </td>
        </tr>`;
        });

        html += '</tbody></table>';

        // Add pagination controls
        const totalPages = Math.ceil(totalCount / pageSize);
        let paginationHtml = '<nav><ul class="pagination mb-0">';
        if (currentPage > 1) {
            paginationHtml += `<li class="page-item"><a class="page-link" href="#" data-page="${currentPage - 1}">Previous</a></li>`;
        }
        paginationHtml += `<li class="page-item disabled"><span class="page-link">Page ${currentPage} of ${totalPages}</span></li>`;
        if (currentPage < totalPages) {
            paginationHtml += `<li class="page-item"><a class="page-link" href="#" data-page="${currentPage + 1}">Next</a></li>`;
        }
        paginationHtml += '</ul></nav>';

        // Add page size selection
        const pageSizeHtml = `
        <div class="d-flex align-items-center">
            <label for="pageSize" class="mr-2 mb-0">Page Size:</label>
            <select id="pageSize" class="form-control form-control-sm" style="width: auto;">
                <option value="5" ${pageSize === 5 ? 'selected' : ''}>5</option>
                <option value="10" ${pageSize === 10 ? 'selected' : ''}>10</option>
                <option value="15" ${pageSize === 15 ? 'selected' : ''}>15</option>
                <option value="50" ${pageSize === 50 ? 'selected' : ''}>50</option>
                <option value="100" ${pageSize === 100 ? 'selected' : ''}>100</option>
            </select>
        </div>`;

        // Combine pagination and page size dropdown in a single row
        const controlsHtml = `
        <div class="d-flex justify-content-between align-items-center">
            ${pageSizeHtml}
            ${paginationHtml}
        </div>`;

        $('#users-container').html(html + controlsHtml);

        attachEvents();
    }

    function attachEvents() {
        $('.edit-btn').click(function () {
            const userId = $(this).closest('tr').data('user-id');
            const currentRole = $(this).closest('tr').data('role');
            openEditPopup(userId, currentRole);
        });

        $('.pagination a').click(function (e) {
            e.preventDefault();
            const page = $(this).data('page');
            if (page) {
                currentPage = page;
                loadUsers(currentPage, pageSize);
            }
        });

        $('#pageSize').change(function () {
            pageSize = parseInt($(this).val(), 10);
            currentPage = 1; // Reset to the first page
            loadUsers(currentPage, pageSize);
        });
    }

    function openEditPopup(userId, currentRole) {
        const popupHtml = `
            <div id="edit-popup" class="popup-overlay">
                <div class="popup-content">
                    <button class="popup-close">&times;</button>
                    <h5>Edit User Role</h5>
                    <select id="edit-role" class="form-control">
                        <option value="admin" ${currentRole === 'admin' ? 'selected' : ''}>Admin</option>
                        <option value="general" ${currentRole === 'general' ? 'selected' : ''}>General</option>
                        <option value="hold" ${currentRole === 'hold' ? 'selected' : ''}>Hold</option>
                    </select>
                    <button id="update-btn" class="btn btn-success mt-3">Update</button>
                </div>
            </div>`;

        $('body').append(popupHtml);
        $('#edit-popup').fadeIn();

        $('#update-btn').click(function () {
            const newRole = $('#edit-role').val();
            $.ajax({
                url: `https://localhost:7123/api/admin/users/${userId}`,
                type: 'PATCH',
                headers: {
                    'Authorization': 'Bearer <your-token-here>',
                    'Content-Type': 'application/json'
                },
                data: JSON.stringify({ role: newRole }),
                success: function () {
                    alert('User role updated successfully.');
                    $('#edit-popup').fadeOut(() => $('#edit-popup').remove());
                    loadUsers(currentPage, pageSize);
                },
                error: function () {
                    alert('Error updating user role.');
                }
            });
        });

        $('.popup-close').click(function () {
            $('#edit-popup').fadeOut(() => $('#edit-popup').remove());
        });
    }
});
