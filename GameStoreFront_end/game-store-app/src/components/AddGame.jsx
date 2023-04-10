import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import React from "react";
import fetchGenres from "./Fetches/fetchGaneres/fetchGenres";
import fetchAddGame from "./Fetches/fetchGames/fetchAddGame";
import FlashBlock from "./FlashBlock";
const AddGame = () => {
  const navigate = useNavigate();
  const [isShowErrorBlock, setIsShowErrorBlock] = useState(false);
  const [errorText, setErrorText] = useState("");
  const [genres, setGenres] = useState([]);
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [price, setPrice] = useState("");
  const [checkedState, setCheckedState] = useState(new Map());

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

    if (name.length < 1 || description.length < 1 || price.length < 1) {
      setErrorText("Some input is empty");
      setIsShowErrorBlock(true);
      return;
    }

    if (name.length < 3 || name.length > 40) {
      setErrorText("Name too short or too long");
      setIsShowErrorBlock(true);
      return;
    }
    if (description.length < 10 || description.length > 400) {
      setErrorText("Description too short or too long");
      setIsShowErrorBlock(true);
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
      if (result.status === 201) {
        setName("");
        setDescription("");
        setPrice("");
        setCheckedState([]);
        navigate("/Games");
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

  //render
  return (
    <div className="dark-background">
      <div
        onClick={(e) => {
          e.preventDefault();
          setIsShowErrorBlock(false);
        }}
      >
        <FlashBlock massage={errorText} isShow={isShowErrorBlock} />
      </div>
      <div>
        <p>Name</p>
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

      <button className="green-button" onClick={(e) => createGame(e)}>
        CreateNewGame
      </button>
    </div>
  );
};

export default AddGame;
