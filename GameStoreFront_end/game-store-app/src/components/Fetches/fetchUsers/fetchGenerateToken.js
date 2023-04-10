const fetchGenerateToken = async () => {
  let refreshToken = localStorage.getItem("refresh_token");
  let token = localStorage.getItem("token");
  var myHeaders = new Headers();
  myHeaders.append("Content-Type", "application/json");

  var raw = JSON.stringify({
    token: token,
    refreshToken: refreshToken,
  });

  var requestOptions = {
    method: "POST",
    headers: myHeaders,
    body: raw,
    redirect: "follow",
  };

  let result = await fetch(
    "https://localhost:7025/api/User/refreshToken",
    requestOptions
  );
  return result;
};

export default fetchGenerateToken;
