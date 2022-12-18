
const fetchUserLogin = async ({ email, password, rememberMe}) => {
    
    const res = await fetch('https://localhost:7025/login', {
        method: 'POST',
        headers: {
            'mode': "no-cors",
            'Accept': "application/json, text/plain, */*",
            'Content-Type': "application/json;charset=utf-8"
        },
        body: JSON.stringify({
            "login": email,
            "password": password,
            "rememberMe": rememberMe
        })
    });
    const data = await res.json();  
    return data;
};
export default fetchUserLogin;