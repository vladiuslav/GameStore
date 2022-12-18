const fetchUserRegestration = async ({ firstName, lastName, userName,email,password}) => {

    const res = await fetch('https://localhost:7025/api/User', {
        method: 'POST',
        headers: {
            'mode': "no-cors",
            'Accept': "application/json, text/plain, */*",
            'Content-Type': "application/json;charset=utf-8"
        },
        body: JSON.stringify({
            "firstName": firstName,
            "lastName": lastName,
            "userName": userName,
            "email": email,
            "password": password,
            "avatarImageUrl": "noneuser.png"
        })
    });
    const data = await res.json();
    return data;
};
export default fetchUserRegestration;