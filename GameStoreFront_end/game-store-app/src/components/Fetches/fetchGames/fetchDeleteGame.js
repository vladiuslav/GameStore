const fetchDeleteGame = async (gameid) => {

    const res = await fetch('https://localhost:7025/api/Game/' + gameid, {
        method: 'DELETE'
    });
    const data = await res.json();

    return data;
};
export default fetchDeleteGame;