const fetchGetCommentsForGame = async (gameId) => {
  var requestOptions = {
    method: "GET",
    redirect: "follow",
  };

  const result = await fetch(
    "https://localhost:7025/api/Game/" + gameId + "/comments",
    requestOptions
  );

  return result;
};
export default fetchGetCommentsForGame;
