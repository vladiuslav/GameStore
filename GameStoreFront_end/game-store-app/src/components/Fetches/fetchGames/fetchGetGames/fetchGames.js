const fetchGames = async () => {

    const result = await fetch(`https://localhost:7025/api/Game`);
    return result;
    
};

export default fetchGames;