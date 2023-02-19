import { useState, useEffect } from "react";
import { Link, useParams } from "react-router-dom";
import fetchGame from "./Fetches/fetchGames/fetchGetGames/fetchGame";
import fetchDeleteGame from "./Fetches/fetchGames/fetchDeleteGame";
import fetchGenres from "./Fetches/fetchGaneres/fetchGenres";
import GameImage from "./GamePageComponents/GameImage.jsx";

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

    const getGenres = async () => {
      const result = await fetchGenres();
      let resultJson = await result.json();
      setGenres(resultJson);
    };
    getGenres();
  }, []);

  //Delete Game
  const deleteGame = () => {
    fetchDeleteGame(GameId);
  };

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

  return game != undefined ? (
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
        Comments
        <p>
          Lorem ipsum dolor sit amet consectetur adipisicing elit. Numquam,
          repellendus? Saepe obcaecati laborum doloremque enim magni incidunt
          molestiae voluptates dolore! Sunt minima dolorem inventore saepe
          obcaecati, fuga omnis ducimus est! Officia aliquid molestias natus
          quam nam, incidunt suscipit blanditiis sit.
        </p>
        <p>
          Lorem ipsum dolor sit amet consectetur adipisicing elit. Numquam,
          repellendus? Saepe obcaecati laborum doloremque enim magni incidunt
          molestiae voluptates dolore! Sunt minima dolorem inventore saepe
          obcaecati, fuga omnis ducimus est! Officia aliquid molestias natus
          quam nam, incidunt suscipit blanditiis sit.
        </p>
      </div>
    </div>
  ) : (
    <></>
  );
};

export default Game;
