async function getSessionItemAsync(key) {
    return sessionStorage.getItem(key);
}

async function setSessionItemAsync(key, value) {
    return sessionStorage.setItem(key, value);
}

async function removeSessionItemAsync(key) {
    return sessionStorage.removeItem(key);
}

async function clearSessionItemAsync() {
    return sessionStorage.clear();
}