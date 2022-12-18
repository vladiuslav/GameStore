const fetchChangeUserImage=(file,token)=>{
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

    fetch("https://localhost:7025/api/UserImage/", requestOptions)
    .catch(error => console.log('error', error));
}
export default fetchChangeUserImage;