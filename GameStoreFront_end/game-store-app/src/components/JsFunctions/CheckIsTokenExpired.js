import fetchGenerateToken from "../Fetches/fetchUsers/fetchGenerateToken";
const CheckIsTokenExpired = async () => {
  let tokenTime = localStorage.getItem("expiredTokenTime");
  if (tokenTime === null) {
    return false;
  }
  let expiresTime = new Date(tokenTime);

  if (expiresTime < Date.now()) {
    let refreshTokenExpiresionTime = localStorage.getItem(
      "refreshTokenExpiresionTime"
    );
    if (refreshTokenExpiresionTime === null) {
      return false;
    }

    let expiresRFTokenTime = new Date(refreshTokenExpiresionTime);
    if (expiresRFTokenTime < Date.now()) {
      localStorage.removeItem("token");
      localStorage.removeItem("refresh_token");
      localStorage.removeItem("expiredTokenTime");
      localStorage.removeItem("refreshTokenExpiresionTime");
      localStorage.removeItem("email");
      return false;
    }

    let result = await fetchGenerateToken();
    if (result.ok) {
      let resultJson = await result.json();

      localStorage.setItem("token", resultJson.token);
      localStorage.setItem("refresh_token", resultJson.refreshToken);
      localStorage.setItem("expiredTokenTime", resultJson.expiresAt);
      localStorage.setItem(
        "refreshTokenExpiresionTime",
        resultJson.refreshTokenExpiresAt
      );
    }
  }
};

export default CheckIsTokenExpired;
