import { useState, useEffect } from 'react'
import { useParams } from 'react-router-dom'

const Game = () => {
  const [game, setGame] = useState([]);
  const { GameId } = useParams();

  useEffect(() => {
    const getGame = async () => {
      const gamesFromServer = await fetchGames();
      setGame(gamesFromServer);
    }
    getGame()
  }, [])


  //Get Games
  const fetchGames = async () => {

    const res = await fetch(`https://localhost:7025/api/Game/` + GameId);
    const data = await res.json();

    return data;
  }
  return (
    <>
      <p>{game.name}</p>
      <p>{game.id}</p>
    </>
  )
}

export default Game