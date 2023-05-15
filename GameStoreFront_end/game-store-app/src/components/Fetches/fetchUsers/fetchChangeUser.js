import CheckIsTokenExpired from "../../JsFunctions/CheckIsTokenExpired";

const fetchChangeUser = async (
  firstName,
  lastName,
  userName,
  email,
  password
) => {
  var myHeaders = new Headers();
  CheckIsTokenExpired();
  const token = localStorage.getItem("token");
  myHeaders.append("Authorization", "Bearer " + token);
  myHeaders.append("Content-Type", "application/json");

  var raw = JSON.stringify({
    firstName: firstName,
    lastName: lastName,
    userName: userName,
    avatarImageUrl: "",
    email: email,
    password: password,
  });
  console.log(raw);

  var requestOptions = {
    method: "PUT",
    headers: myHeaders,
    body: raw,
    redirect: "follow",
  };

  let result = await fetch("https://localhost:7025/api/User", requestOptions);
  return result;
};

export default fetchChangeUser;
