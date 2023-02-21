import React, { useState } from "react";
import { Link, useParams, useNavigate } from "react-router-dom";
import GameImage from "./GameImage";
import fetchDeleteGame from "../Fetches/fetchGames/fetchDeleteGame";
import FlashBlock from "../FlashBlock";
const GameEditFunctional = (props) => {
  const navigate = useNavigate();
  const [isShownFunctionalBlock, setIsShownFunctionalBlock] = useState(false);
  const [isShowErrorBlock, setIsShowErrorBlock] = useState(false);
  const [errorText, setErrorText] = useState("");
  const deleteGame = (e) => {
    e.preventDefault();

    const processFetch = async () => {
      let result = await fetchDeleteGame(props.gameId);
      if (result.status === 200) {
        navigate("/Game/" + GameId);
        return;
      } else if (result.status === 404) {
        setErrorText("Game doesn`t exist");
        setIsShowErrorBlock(true);
        return;
      } else {
        setErrorText("Error" + result.status);
        setIsShowErrorBlock(true);
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
          <div className="game-edit">
            <Link className="game-edit-link" to={"/ChangeGame/" + props.gameId}>
              <i className="fa-solid fa-pen-to-square"></i> Edit Game
            </Link>
          </div>
          <div
            className="game-delete"
            onClick={(e) => {
              deleteGame(e);
            }}
          >
            <i className="fa-solid fa-trash"></i> Delete Game
          </div>
        </div>
      )}
      <FlashBlock massage={errorText} isShow={isShowErrorBlock} />
    </div>
  );
};

export default GameEditFunctional;
