import CheckIsTokenExpired from "../../JsFunctions/CheckIsTokenExpired";

const fetchDeleteComment = async ({ commentId }) => {
  CheckIsTokenExpired();
  const token = localStorage.getItem("token");
  var myHeaders = new Headers();

  myHeaders.append("Authorization", "Bearer " + token);

  var requestOptions = {
    method: "DELETE",
    headers: myHeaders,
    redirect: "follow",
  };

  const result = await fetch(
    "https://localhost:7025/api/Comment/" + commentId,
    requestOptions
  );
  return result;
};
export default fetchDeleteComment;
