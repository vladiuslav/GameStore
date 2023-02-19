const fetchChangeGameImage = async (file, gameId) => {
  var formdata = new FormData();
  formdata.append("UploadedFile", file);

  var requestOptions = {
    method: "PUT",
    body: formdata,
    redirect: "follow",
  };

  let result = await fetch(
    "https://localhost:7025/api/GameImage/" + gameId,
    requestOptions
  );
  return result;
};
export default fetchChangeGameImage;
