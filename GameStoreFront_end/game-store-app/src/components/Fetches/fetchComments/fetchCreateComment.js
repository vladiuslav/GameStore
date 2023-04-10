import CheckIsTokenExpired from "../../JsFunctions/CheckIsTokenExpired";

const fetchCreateComment = async ({
  commentText,
  gameId,
  repliedCommentId,
}) => {
  CheckIsTokenExpired();
  const token = localStorage.getItem("token");
  var myHeaders = new Headers();

  myHeaders.append("Authorization", "Bearer " + token);
  myHeaders.append("Content-Type", "application/json");

  var raw = JSON.stringify({
    text: commentText,
    gameId: gameId,
    repliedCommentId: repliedCommentId,
  });

  var requestOptions = {
    method: "POST",
    headers: myHeaders,
    body: raw,
    redirect: "follow",
  };

  let result = await fetch(
    "https://localhost:7025/api/Comment",
    requestOptions
  );
  return result;
};
export default fetchCreateComment;
