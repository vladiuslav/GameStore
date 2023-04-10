import React from "react";
import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import fetchGame from "./Fetches/fetchGames/fetchGetGames/fetchGame";
import fetchGenres from "./Fetches/fetchGaneres/fetchGenres";
import GameImage from "./GamePageComponents/GameImage.jsx";
import GameComments from "./GamePageComponents/GameComments";
const Game = () => {
  const navigate = useNavigate();
  const [genres, setGenres] = useState([]);
  const [game, setGame] = useState(undefined);
  const { GameId } = useParams();

  useEffect(() => {
    const getGame = async () => {
      const result = await fetchGame(GameId);
      if (result.status !== 200) {
        navigate("/");
      }
      let resultJson = await result.json();
      setGame(resultJson);
    };
    getGame();

    const getGenres = async () => {
      const result = await fetchGenres();
      let resultJson = await result.json();
      setGenres(resultJson);
    };
    getGenres();
  }, []);

  const getGenres = (ids) => {
    const filteredGenres = genres.filter((element) => ids.includes(element.id));

    return filteredGenres.length > 0 ? (
      <div className="game-genres">
        {filteredGenres.map((genre) => (
          <div key={genre.id} className="game-genres-item">
            {genre.name}
          </div>
        ))}
      </div>
    ) : (
      <></>
    );
  };

  return game !== undefined ? (
    <div className="game-page">
      <GameImage className="game-page-image" GameImageUrl={game.imageUrl} />
      <div className="game-name">{game.name}</div>
      <div className="game-price-buy-block">
        <div className="game-price">{game.price + "$"}</div>
        <div className="game-buy">
          <button>BUY</button>
        </div>
      </div>
      <hr />
      <div>{getGenres(game.genresIds)}</div>
      <div className="game-description">{game.description}</div>

      <div>
        <GameComments gameId={game.id} />
      </div>
    </div>
  ) : (
    <></>
  );
};

export default Game;
