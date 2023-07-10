const CheckIsUserLogin = function() {
  if (localStorage.getItem("token") === null) {
    return false;
  } else {
    return true;
  }
};

export default CheckIsUserLogin;
