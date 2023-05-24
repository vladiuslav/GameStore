const fetchGamesByGenres = async (genresIds) => {
  var myHeaders = new Headers();
  myHeaders.append("Content-Type", "application/json");

  var raw = JSON.stringify({
    genresIds: genresIds,
  });

  var requestOptions = {
    method: "POST",
    headers: myHeaders,
    body: raw,
    redirect: "follow",
  };

  let result = await fetch(
    "https://localhost:7025/api/SearchFilterControler/Filter",
    requestOptions
  );
  return result;
};
export default fetchGamesByGenres;
