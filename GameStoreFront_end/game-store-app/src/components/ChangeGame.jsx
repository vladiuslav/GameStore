import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import React from "react";

import fetchGame from "./Fetches/fetchGames/fetchGetGames/fetchGame";
import fetchGenres from "./Fetches/fetchGaneres/fetchGenres";
import fetchChangeGame from "./Fetches/fetchGames/fetchChangeGame";
import fetchChangeGameImage from "./Fetches/fetchGames/fetchChangeGameImage";

const ChangeGame = () => {
  const navigate = useNavigate();
  const [isShowEmptyError, setIsShowEmptyError] = useState(false);
  const [genres, setGenres] = useState([]);
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [price, setPrice] = useState("");
  const [checkedState, setCheckedState] = useState(new Map());
  const { GameId } = useParams();
  const [image, setImage] = useState(null);

  useEffect(() => {
    const getGame = async () => {
      const result = await fetchGame(GameId);
      let gameJson = await result.json();

      setName(gameJson.name);
      setDescription(gameJson.description);
      setPrice(gameJson.price);

      const result2 = await fetchGenres();
      const genresJson = await result2.json();

      let genresChecks = checkedState;
      for (let index = 0; index < genresJson.length; index++) {
        genresChecks.set(
          genresJson[index].name,
          gameJson.genresIds.some((genreId) => genreId === genresJson[index].id)
            ? true
            : false
        );
      }
      setCheckedState(genresChecks);
      setGenres(genresJson);
    };
    getGame();
  }, []);

  // CheckedFunction
  const handleOnChange = (name) => {
    let updatedCheckedState = new Map(checkedState);
    let IsPressed = updatedCheckedState.get(name);
    updatedCheckedState.set(name, !IsPressed);
    setCheckedState(updatedCheckedState);
  };

  const changeGame = (e) => {
    e.preventDefault();

    if (name.length < 3 || description.length < 10) {
      setIsShowEmptyError(true);
      return;
    }
    const processFetch = async () => {
      let result = await fetchChangeGame({
        name,
        description,
        price,
        checkedState,
        genres,
        GameId,
      });
      if (result.status === 200) {
        await changeImage();
        alert("Game chenged");
        navigate("/Game/" + GameId);
        return;
      } else if (result.status === 400) {
        alert("Game name exist");
        return;
      } else if (result.status === 404) {
        alert("Game doesn`t exist");
        return;
      } else {
        alert("Error " + result.status);
        return;
      }
    };
    processFetch();
  };

  const changeImage = async () => {
    if (image === null || image.length < 1) {
      return;
    }

    const processFetch = async () => {
      let result = await fetchChangeGameImage(image[0], GameId);
      if (result.status === 200) {
        return;
      } else {
        alert("Error " + result.status);
        return;
      }
    };
    processFetch();
  };

  return (
    <div className="dark-background">
      <h1>Change Game</h1>
      <div>
        <p>Name</p>
        {isShowEmptyError && name.length < 3 ? (
          <p className="error-text">
            Name can`t be empty and have at least 3 letters
          </p>
        ) : (
          <></>
        )}
        <input
          className="game-form-input-text"
          type="text"
          placeholder="Add name"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
      </div>
      <div>
        <p>Game description</p>
        {isShowEmptyError && description.length < 10 ? (
          <p className="error-text">
            Description can`t be empty and have at least 10 letters
          </p>
        ) : (
          <></>
        )}
        <input
          className="game-form-input-text"
          type="text"
          placeholder="Add description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />
      </div>
      <div>
        <p>Game genres</p>
        <ul>
          {genres.map((item) => (
            <li key={item.id}>
              <input
                type="checkbox"
                value={checkedState.get(item.name)}
                checked={checkedState.get(item.name)}
                onChange={() => handleOnChange(item.name)}
              />
              <label>{item.name}</label>
            </li>
          ))}
        </ul>
      </div>
      <div>
        <p>Game price</p>
        <input
          className="game-form-input-text"
          type="text"
          placeholder="Add price"
          value={price}
          onChange={(e) => {
            if (e.target.value.includes(".")) {
              setPrice(e.target.value.replace(/\./g, ","));
            } else {
              setPrice(e.target.value);
            }
          }}
        />
      </div>
      <div>
        <label>Image of game</label>
        <input
          className="game-form-input-file"
          type="file"
          onChange={(e) => setImage(e.target.files)}
        />
      </div>
      <button className="green-button" onClick={(e) => changeGame(e)}>
        Change Game
      </button>
    </div>
  );
};

export default ChangeGame;
