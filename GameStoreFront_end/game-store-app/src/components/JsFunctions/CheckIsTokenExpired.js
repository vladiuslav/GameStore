import fetchGenerateToken from "../Fetches/fetchUsers/fetchGenerateToken";
const CheckIsTokenExpired = async () => {
  let expiresTime = new Date(localStorage.getItem("expiredTokenTime"));
  if (expiresTime < Date.now()) {
    let result = await fetchGenerateToken();
    if (result.status === 200) {
      let resultJson = await result.json();

      localStorage.setItem("token", resultJson.token);
      localStorage.setItem("refresh_token", resultJson.refreshToken);
      localStorage.setItem("expiredTokenTime", resultJson.expiresAt);
    }
  }
};

export default CheckIsTokenExpired;
