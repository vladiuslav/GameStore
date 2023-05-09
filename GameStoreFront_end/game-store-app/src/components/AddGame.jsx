import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import React from "react";
import fetchGenres from "./Fetches/fetchGaneres/fetchGenres";
import fetchAddGame from "./Fetches/fetchGames/fetchAddGame";
import fetchChangeGameImage from "./Fetches/fetchGames/fetchChangeGameImage";

const AddGame = () => {
  const navigate = useNavigate();
  const [genres, setGenres] = useState([]);
  const [isShowEmptyError, setIsShowEmptyError] = useState(false);
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [price, setPrice] = useState("");
  const [checkedState, setCheckedState] = useState(new Map());
  const [image, setImage] = useState(null);

  useEffect(() => {
    const getGenres = async () => {
      const genresFromServer = await fetchGenres();
      let genresJson = await genresFromServer.json();
      setGenres(genresJson);

      let genresChecks = checkedState;
      for (let index = 0; index < genresJson.length; index++) {
        genresChecks.set(genresJson[index].name, false);
      }
      setCheckedState(genresChecks);
    };
    getGenres();
  }, []);

  // CheckedFunction
  const handleOnChange = (name) => {
    let updatedCheckedState = new Map(checkedState);
    let IsPressed = updatedCheckedState.get(name);
    updatedCheckedState.set(name, !IsPressed);
    setCheckedState(updatedCheckedState);
  };

  //CreateNewGame
  const createGame = (e) => {
    e.preventDefault();

    if (name.length < 3 || description.length < 10) {
      setIsShowEmptyError(true);
      return;
    }

    if (price.includes(".")) {
      setPrice(price.replace(/\./g, ","));
    }

    const processFetch = async () => {
      let result = await fetchAddGame({
        name,
        description,
        price,
        checkedState,
        genres,
      });

      if (result.status === 200) {
        let resultJson = await result.json();
        changeImage(resultJson.id);
        alert("Game added");
        navigate("/");
        return;
      } else if (result.status === 400) {
        alert("Game name exist");
        return;
      } else {
        alert("Error " + result.status);
        return;
      }
    };

    processFetch();
  };

  const changeImage = (gameId) => {
    if (image === null || image.length < 1) {
      return;
    }

    const processFetch = async () => {
      let result = await fetchChangeGameImage(image[0], gameId);
      if (result.status === 200) {
        return;
      } else {
        alert("Error " + result.status);
        return;
      }
    };
    processFetch();
  };

  //render
  return (
    <div className="dark-background">
      <div>
        <p>Name *</p>
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
        <p>Game description *</p>
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
          {genres.length >= 1 ? (
            genres.map((item) => (
              <li key={item.id}>
                <input
                  type="checkbox"
                  value={checkedState.get(item.name)}
                  checked={checkedState.get(item.name)}
                  onChange={() => handleOnChange(item.name)}
                />
                <label>{item.name}</label>
              </li>
            ))
          ) : (
            <></>
          )}
        </ul>
      </div>
      <div>
        <p>Game price</p>
        <input
          className="game-form-input-text"
          type="text"
          placeholder="Add price"
          value={price}
          onChange={(e) => setPrice(e.target.value)}
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
      <button className="green-button" onClick={(e) => createGame(e)}>
        CreateNewGame
      </button>
    </div>
  );
};

export default AddGame;
