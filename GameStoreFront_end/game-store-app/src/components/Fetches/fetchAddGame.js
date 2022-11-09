const fetchAddGame = async ({ name, description, price, checkedState }) => {
    let ganresIds = [];
    for (let index = 0; index < checkedState.length; index++) {
        if (checkedState[index] !== false) {
            ganresIds.push(index + 1);
        }

    }

    const res = await fetch('https://localhost:7025/api/Game', {
        method: 'POST',
        headers: {
            'mode': "no-cors",
            'Accept': "application/json, text/plain, */*",
            'Content-Type': "application/json;charset=utf-8"
        },
        body: JSON.stringify({
            "name": name,
            "description": description,
            "price": price,
            "imageUrl": "None.jpg",
            "ganresIds": ganresIds
        })
    });
    const data = await res.json();
    return data;
};
export default fetchAddGame;