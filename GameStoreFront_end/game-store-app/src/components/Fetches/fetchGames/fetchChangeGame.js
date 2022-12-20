const fetchChangeGame = async ({ name, description, price, checkedState, imageUrl, GameId }) => {

    let ganresIds = [];
    for (let index = 0; index < checkedState.length; index++) {
        if (checkedState[index] !== false) {
            ganresIds.push(index);
        }

    }
    const res = await fetch('https://localhost:7025/api/Game', {
        method: 'PUT',
        headers: {
            'mode': "no-cors",
            'Accept': "application/json, text/plain, */*",
            'Content-Type': "application/json;charset=utf-8"
        },
        body: JSON.stringify({
            "id":GameId,
            "name": name,
            "description": description,
            "price": price,
            "imageUrl": imageUrl,
            "ganresIds": ganresIds
        })
    });
    const data = await res.json();
    return data;
};

export default fetchChangeGame;