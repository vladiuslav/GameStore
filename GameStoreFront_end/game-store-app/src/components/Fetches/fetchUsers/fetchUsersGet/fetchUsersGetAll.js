
const fetchUserGetAll = async ({access_token}) => {

    const res = await fetch('https://localhost:7025/api/User/current', {
        method: 'GET',
        headers: {

            'Access-Control-Allow-Origin': '*',
            'Accept': "application/json, text/plain, */*",
            'Content-Type': "application/json;charset=utf-8",
            'Authorization': 'Bearer '+ access_token
        }
    });
    console.log(res);
    const data = await res.json();
    return data;
};
export default fetchUserGetAll;