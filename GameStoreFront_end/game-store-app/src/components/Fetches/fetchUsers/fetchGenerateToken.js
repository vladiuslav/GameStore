import getCookie from "../../CokieFunctions/getCookie";
import setCookie from "../../CokieFunctions/setCookie";

const fetchGenerateToken = async () => {
  let refreshToken = getCookie("refresh_token");
  let token =
    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
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
    "https://localhost:7025/refreshToken",
    requestOptions
  );
  return result;
};

export default fetchGenerateToken;
