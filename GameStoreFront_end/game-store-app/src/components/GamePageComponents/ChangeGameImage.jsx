import { useState, useEffect } from "react";
import { Link, useParams, useNavigate } from "react-router-dom";
import React from "react";
import Game from "../Game";
import fetchChangeGameImage from "../Fetches/fetchGames/fetchChangeGameImage";

import FlashBlock from "../FlashBlock";

const ChangeGameImage = () => {
  const navigate = useNavigate();
  const [isShowErrorBlock, setIsShowErrorBlock] = useState(false);
  const [errorText, setErrorText] = useState("");
  const [image, setImage] = useState(null);
  const { GameId } = useParams();

  const changeGame = (e) => {
    if (image.length < 1) {
      setErrorText("Image empty");
      setIsShowErrorBlock(true);
      return;
    }

    const processFetch = async () => {
      let result = await fetchChangeGameImage(image[0], GameId);
      if (result.status === 200) {
        navigate("/Game/" + GameId);
        return;
      } else if (result.status === 400) {
        setErrorText("Game name exist or wrong price number.");
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
    <div>
      <div
        onClick={(e) => {
          e.preventDefault();
          setIsShowErrorBlock(false);
        }}
      >
        <FlashBlock massage={errorText} isShow={isShowErrorBlock} />
      </div>
      <label>Image of game</label>
      <input
        className="game-form-input-file"
        type="file"
        onChange={(e) => setImage(e.target.files)}
      />
      <button className="green-button" onClick={() => changeGame()}>
        Change game image
      </button>
    </div>
  );
};

export default ChangeGameImage;
