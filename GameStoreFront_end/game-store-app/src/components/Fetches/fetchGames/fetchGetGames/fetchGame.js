const fetchGame = async (GameId) => {
    
    const result = await fetch(`https://localhost:7025/api/Game/` + GameId);
    return result;

};

export default fetchGame;