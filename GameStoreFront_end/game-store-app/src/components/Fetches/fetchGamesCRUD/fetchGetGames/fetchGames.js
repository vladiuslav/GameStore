const fetchGames = async () => {

    const res = await fetch(`https://localhost:7025/api/Game`);
    const data = await res.json();

    return data;
};

export default fetchGames;