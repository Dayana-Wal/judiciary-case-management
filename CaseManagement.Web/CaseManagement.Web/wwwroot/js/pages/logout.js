function logout() {
    // Clear session-related data
    localStorage.removeItem('authToken');
    localStorage.removeItem('user');

    // Show a confirmation message
    alert('You have been logged out successfully.');

    // Redirect to the login page
    window.location.href = '/User/Login';
}

function renderNavLinks() {
    const authToken = localStorage.getItem("authToken");
    const user = localStorage.getItem("user");
    const navLinks = document.getElementById("navLinks");

    if (authToken && user) {
        navLinks.innerHTML = `
            <li class="nav-item">
                <a class="nav-link text-dark" href="/CaseSearch/Search">Search</a>
            </li>
            <li class="nav-item ms-auto">
                <a class="nav-link text-dark" href="#" onclick="logout()">Logout</a>
            </li>
        `;
    } else {
        navLinks.innerHTML = `
            <li class="nav-item">
                <a class="nav-link text-dark" href="/User/Signup">Signup</a>
            </li>
            <li class="nav-item dropdown">
                <a class="nav-link dropdown-toggle text-dark" href="#" id="loginDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                    Login
                </a>
                <ul class="dropdown-menu" aria-labelledby="loginDropdown">
                    <li>
                        <a class="dropdown-item" href="/User/Login?role=user">Login</a>
                    </li>
                    <li>
                        <a class="dropdown-item" href="/User/Login?role=admin">Login as Admin</a>
                    </li>
                </ul>
            </li>
        `;
    }
}

// Initialize navigation links
renderNavLinks();
