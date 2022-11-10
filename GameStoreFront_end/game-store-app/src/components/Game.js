import { useState, useEffect } from 'react'
import { Link, useParams } from 'react-router-dom'
import ChangeGameComponent from './GamePage/ChangeGameComponent';
import ChangeGameImage from './GamePage/ChangeGameImage';
import fetchGame from './Fetches/fetchGame';
import fetchDeleteGame from './Fetches/fetchDeleteGame';
import fetchGanres from './Fetches/fetchGanres';
import GameImageBig from './GamePage/GameImageBig';
const Game = () => {
  const [ganres, setGanres] = useState([]);
  const [game, setGame] = useState([]);
  const { GameId } = useParams();

  useEffect(() => {
    const getGame = async () => {
      const gameFromServer = await fetchGame(GameId);
      setGame(gameFromServer);
    };
    getGame();

    const getGanres = async () => {
      const ganresFromServer = await fetchGanres();
      setGanres(ganresFromServer);
    };
    getGanres();

  }, [])

  //Delete Game 
  const deleteGame = () => {
    fetchDeleteGame(GameId);
  }

  const getGanres = (ids) => {
    if (ganres.length != 0) {
      let GanresString = "";
      ganres.forEach((element) => {
        if (ids.find((id) => id == element.id) != null) {
          GanresString += element.name + "/";
        }
      });
      GanresString = GanresString.slice(0, GanresString.length - 1);
      return <div className="ganre-name"> {GanresString} </div>;
    }
    return <></>;
  };

  return (
    <div className='game-page'>
      <GameImageBig GameImageUrl={game.imageUrl} />
      <div className='game-info'>
        <div className="game-ganres">{getGanres(game.ganresIds)}</div>
        <div className='game-price'>{game.price + "$"}</div>
        <div className='game-name'><h1>{game.name}</h1></div>
      </div>
      <div className='game-description'>
        <h1>Description</h1>
        <p>{game.description}</p>
      </div>
      <ChangeGameComponent />
      <button onClick={() => deleteGame()}>Delete game</button>
      <ChangeGameImage />
    </div>
  )
}

export default Game