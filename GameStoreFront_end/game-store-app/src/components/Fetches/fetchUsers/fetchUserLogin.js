const fetchUserLogin = async ({ email, password }) => {
  var myHeaders = new Headers();
  myHeaders.append("Content-Type", "application/json");

  var raw = JSON.stringify({
    email: email,
    password: password,
  });

  var requestOptions = {
    method: "POST",
    headers: myHeaders,
    body: raw,
    redirect: "follow",
  };

  let result = await fetch(
    "https://localhost:7025/api/User/login",
    requestOptions
  );
  return result;
};

export default fetchUserLogin;
