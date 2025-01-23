//function to get the token from localStorage
function getToken() {
    return localStorage.getItem('authToken');
}
function getUser() {
    return localStorage.getItem('user');
}
//function to set the token and user data in localStorage after login
function storeUserSession(token, user) {
    localStorage.setItem('authToken', token);
    localStorage.setItem('user', JSON.stringify(user));
}

//function to remove the token and user details from localStorage (e.g., on logout)
function removeToken() {
    localStorage.removeItem('authToken');
    localStorage.removeItem('user');
}

// Check if the user is authenticated by verifying if the token exists
function isAuthenticated() {
    return getToken() !== null;
}
