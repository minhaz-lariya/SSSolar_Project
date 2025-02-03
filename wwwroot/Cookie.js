function setCookie(name, value, days) {
    let expires = "";
    if (days) {
        const date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000)); // days in milliseconds
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}

function getCookie(name) {
    const nameEQ = name + "=";
    const cookiesArray = document.cookie.split(";"); // Split all cookies into an array
    for (let i = 0; i < cookiesArray.length; i++) {
        let cookie = cookiesArray[i];
        while (cookie.charAt(0) === " ") cookie = cookie.substring(1); // Remove leading spaces
        if (cookie.indexOf(nameEQ) === 0) return cookie.substring(nameEQ.length);
    }
    return null; // Return null if the cookie is not found
}

function deleteCookie(name) {
    document.cookie = name + "=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
}

function checkCookie(name) {
    return getCookie(name) !== null;
}

