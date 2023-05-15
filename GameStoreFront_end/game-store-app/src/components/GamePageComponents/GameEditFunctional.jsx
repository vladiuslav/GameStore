import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import GameImage from "./GameImage";
import fetchDeleteGame from "../Fetches/fetchGames/fetchDeleteGame";
const GameEditFunctional = (props) => {
  const navigate = useNavigate();
  const [isShownFunctionalBlock, setIsShownFunctionalBlock] = useState(false);
  const deleteGame = (e) => {
    e.preventDefault();

    const processFetch = async () => {
      let result = await fetchDeleteGame(props.gameId);
      if (result.status === 200) {
        navigate("/Game/" + props.gameId);
        return;
      } else if (result.status === 404) {
        alert("Game doesn`t exist");
        return;
      } else {
        alert("Error" + result.status);
        return;
      }
    };
    processFetch();
  };

  return (
    <div
      className="games-image-block"
      onMouseEnter={() => setIsShownFunctionalBlock(true)}
      onMouseLeave={() => setIsShownFunctionalBlock(false)}
    >
      <GameImage
        className={props.classNameForImage}
        GameImageUrl={props.GameImageUrl}
      />
      {isShownFunctionalBlock && (
        <div className={props.classNameForOverideBlock}>
          <div
            className={props.IsSmallGame ? "game-edit-small" : "game-edit-big"}
          >
            <Link className="game-edit-link" to={"/ChangeGame/" + props.gameId}>
              <i className="fa-solid fa-pen-to-square"></i> Edit Game
            </Link>
          </div>
          <div
            className={
              props.IsSmallGame ? "game-delete-small" : "game-delete-big"
            }
            onClick={(e) => {
              deleteGame(e);
            }}
          >
            <i className="fa-solid fa-trash"></i> Delete Game
          </div>
        </div>
      )}
    </div>
  );
};

export default GameEditFunctional;
