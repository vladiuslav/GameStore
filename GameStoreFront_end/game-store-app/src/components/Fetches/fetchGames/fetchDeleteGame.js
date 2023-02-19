const fetchDeleteGame = async (gameid) => {
  var requestOptions = {
    method: "DELETE",
    redirect: "follow",
  };
  const result = await fetch(
    "https://localhost:7025/api/Game/" + gameid,
    requestOptions
  );
  return result;
};
export default fetchDeleteGame;
