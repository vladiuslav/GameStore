const fetchChangeUserImage=async (file,token)=>{
    var myHeaders = new Headers();
    myHeaders.append("Authorization", "Bearer "+ token);

    var formdata = new FormData();
    formdata.append("UploadedFile", file);

    var requestOptions = {
    method: 'PUT',
    headers: myHeaders,
    body: formdata,
    redirect: 'follow'
    };

    let result = await fetch("https://localhost:7025/api/UserImage/", requestOptions);
    return result;
}
export default fetchChangeUserImage;