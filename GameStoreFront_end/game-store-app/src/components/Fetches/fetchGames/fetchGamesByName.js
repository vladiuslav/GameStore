const fetchGamesByName = async (searchName) => {
    const res = await fetch('https://localhost:7025/api/SearchFilterControler/Search/' + searchName, {
        method: 'POST'
    })
    const data = await res.json();
    return data;
};
export default fetchGamesByName;