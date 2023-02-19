const fetchGamesByName = async (searchName) => {
  var requestOptions = {
    method: "POST",
    redirect: "follow",
  };

  let result = await fetch(
    "https://localhost:7025/api/SearchFilterControler/Search/" + searchName,
    requestOptions
  );
  return result;
};
export default fetchGamesByName;
