const fetchUserRegestration = async ({
  firstName,
  lastName,
  userName,
  email,
  password,
}) => {
  var myHeaders = new Headers();
  myHeaders.append("Content-Type", "application/json");

  var raw = JSON.stringify({
    firstName: firstName,
    lastName: lastName,
    userName: userName,
    email: email,
    password: password,
  });

  var requestOptions = {
    method: "POST",
    headers: myHeaders,
    body: raw,
    redirect: "follow",
  };

  let result = await fetch("https://localhost:7025/api/User", requestOptions);
  return result;
};
export default fetchUserRegestration;
