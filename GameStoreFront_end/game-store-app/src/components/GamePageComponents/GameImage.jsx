import React from "react";

const GameImage = (props) => {
  let imageUrl;
  if (props.GameImageUrl === null) {
    imageUrl = "nonegame.jpg";
  } else {
    imageUrl = props.GameImageUrl;
  }

  return (
    <img
      alt="Game"
      className={props.className}
      src={"https://localhost:7025/img/" + imageUrl}
    />
  );
};

export default GameImage;
