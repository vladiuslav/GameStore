const fetchGanres = async () => {

    let result = await fetch(`https://localhost:7025/api/Genre`);
    return result;
    
};

export default fetchGanres;