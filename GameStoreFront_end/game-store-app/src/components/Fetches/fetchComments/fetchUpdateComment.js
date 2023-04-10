import CheckIsTokenExpired from "../../JsFunctions/CheckIsTokenExpired";

const fetchUpdateComment = async ({ commentText, commentId }) => {
  CheckIsTokenExpired();
  const token = localStorage.getItem("token");
  var myHeaders = new Headers();

  myHeaders.append("Authorization", "Bearer " + token);
  myHeaders.append("Content-Type", "application/json");

  var raw = JSON.stringify({
    text: commentText,
    id: commentId,
  });

  var requestOptions = {
    method: "PUT",
    headers: myHeaders,
    redirect: "follow",
    body: raw,
  };

  let result = await fetch(
    "https://localhost:7025/api/Comment",
    requestOptions
  );
  return result;
};
export default fetchUpdateComment;
