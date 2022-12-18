const fetchGanres = async () => {

    const res = await fetch(`https://localhost:7025/api/Genre`);
    const data = await res.json();

    return data;
};

export default fetchGanres;