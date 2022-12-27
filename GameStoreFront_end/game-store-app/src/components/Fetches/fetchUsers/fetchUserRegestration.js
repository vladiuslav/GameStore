const fetchUserRegestration = async ({ firstName, lastName, userName,email,password}) => {

    var myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");

    var raw = JSON.stringify({
    "firstName": "string1234",
    "lastName": "string1234",
    "userName": "string1234",
    "email": "string1234",
    "password": "stringst"
    });

    var requestOptions = {
    method: 'POST',
    headers: myHeaders,
    body: raw,
    redirect: 'follow'
    };

    let result = await fetch("https://localhost:7025/api/User", requestOptions)
    return result;
};
export default fetchUserRegestration;