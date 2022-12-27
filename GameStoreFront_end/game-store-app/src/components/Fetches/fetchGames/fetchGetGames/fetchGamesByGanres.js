const fetchGamesByGanres = async (checkedState,genres) => {

    
    let genresIds = [];
    checkedState.forEach(function(value, key) {
        if(value){
        genresIds.push(genres.find(ganre=>ganre.name==key).id);
        }
    })

    var myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");

    var raw = JSON.stringify({
    "genresIds": genresIds
    });

    var requestOptions = {
    method: 'POST',
    headers: myHeaders,
    body: raw,
    redirect: 'follow'
    };

    let result = await fetch("https://localhost:7025/api/SearchFilterControler/Filter", requestOptions)
    return result;

};
export default fetchGamesByGanres;