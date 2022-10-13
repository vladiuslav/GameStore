import { useState, useEffect } from 'react'
import { Link } from 'react-router-dom'

const Games = () => {
  const [games, setGames] = useState([]);

  useEffect(() => {
    const getGames = async () => {
      const gamesFromServer = await fetchGames();
      console.log(gamesFromServer);
      setGames(gamesFromServer);
    }
    getGames()
  }, [])


  //Get Games
  const fetchGames = async () => {

    const res = await fetch(`https://localhost:7025/api/Game`);
    const data = await res.json();

    return data;
  }

  return (
    <>
      {games.map((game, id) => (
        <div className='game-container' key={id}>
          <Link to={"/Game/" + game.id}>
            {game.name}
          </Link>
        </div>
      ))}
    </>
  )
}

export default Games