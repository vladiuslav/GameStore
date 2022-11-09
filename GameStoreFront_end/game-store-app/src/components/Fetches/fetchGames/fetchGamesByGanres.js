const fetchGamesByGanres = async (checkedState) => {

    let ganresIds = [];
    for (let index = 0; index < checkedState.length; index++) {
        if (checkedState[index] !== false) {
            ganresIds.push(index);
        }
    }

    const res = await fetch('https://localhost:7025/api/SearchFilterControler/Filter', {
        method: 'POST',
        headers: {
            'mode': "no-cors",
            'Accept': "application/json, text/plain, */*",
            'Content-Type': "application/json;charset=utf-8"
        },

        body: JSON.stringify({
            ganresIds
        })
    });
    const data = await res.json();
    return data;
};
export default fetchGamesByGanres;