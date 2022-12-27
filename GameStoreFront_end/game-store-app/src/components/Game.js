import { useState, useEffect } from 'react';
import { Link, useParams } from 'react-router-dom';
import ChangeGameComponent from './GamePageComponents/ChangeGameComponent';
import ChangeGameImage from './GamePageComponents/ChangeGameImage';
import fetchGame from './Fetches/fetchGames/fetchGetGames/fetchGame';
import fetchDeleteGame from './Fetches/fetchGames/fetchDeleteGame';
import fetchGanres from './Fetches/fetchGaneres/fetchGanres';
import GameImageBig from './GamePageComponents/GameImageBig';

const Game = () => {

  const [genres, setGenres] = useState([]);
  const [game, setGame] = useState(undefined);
  const { GameId } = useParams();

  useEffect(() => {
    const getGame = async () => {
      const result = await fetchGame(GameId);
      let resultJson = await result.json();
      setGame(resultJson);
    };
    getGame();

    const getGanres = async () => {
      const result = await fetchGanres();
      let resultJson = await result.json();
      setGenres(resultJson);
    };
    getGanres();

  }, [])

  //Delete Game 
  const deleteGame = () => {
    fetchDeleteGame(GameId);
  }

  const getGanres = (ids) => {
    if (genres.length != 0) {
      let GanresString = "";
      genres.forEach((element) => {
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
    (game!=undefined)?<div className='game-page'>
      <GameImageBig GameImageUrl={game.imageUrl} />
      <div className='game-info'>
        <div className='game-ganres'>{getGanres(game.genresIds)}</div>
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
    :<></>
  )
}

export default Game;