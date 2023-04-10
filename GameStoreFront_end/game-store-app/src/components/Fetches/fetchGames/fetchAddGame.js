const fetchAddGame = async ({
  name,
  description,
  price,
  checkedState,
  genres,
}) => {
  let genresIds = [];
  checkedState.forEach(function(value, key) {
    if (value) {
      genresIds.push(genres.find((genre) => genre.name === key).id);
    }
  });

  var myHeaders = new Headers();
  myHeaders.append("Content-Type", "application/json");

  var raw = JSON.stringify({
    name: name,
    description: description,
    price: price,
    genresIds: genresIds,
    commentsIds: [],
  });

  var requestOptions = {
    method: "POST",
    headers: myHeaders,
    body: raw,
    redirect: "follow",
  };

  let result = await fetch("https://localhost:7025/api/Game", requestOptions);
  return result;
};
export default fetchAddGame;
