const fetchGame = async (GameId) => {
    const res = await fetch(`https://localhost:7025/api/Game/` + GameId);
    const data = await res.json();

    return data;
};

export default fetchGame;