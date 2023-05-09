import React from "react";
import nonegame from "../../Images/nonegame.png";
const GameImage = (props) => {
  let imageUrl;
  if (props.GameImageUrl === null) {
    imageUrl = nonegame;
  } else {
    imageUrl = "https://localhost:7025/img/" + props.GameImageUrl;
  }

  return <img alt="Game" className={props.className} src={imageUrl} />;
};

export default GameImage;
