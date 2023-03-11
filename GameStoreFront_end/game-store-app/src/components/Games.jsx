import React from "react";
import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import fetchGames from "./Fetches/fetchGames/fetchGetGames/fetchGames";
import fetchGamesByGenres from "./Fetches/fetchGames/fetchGetGames/fetchGamesByGenres";
import fetchGamesByName from "./Fetches/fetchGames/fetchGetGames/fetchGamesByName";
import fetchGenres from "./Fetches/fetchGaneres/fetchGenres";
import GameEditFunctional from "./GamePageComponents/GameEditFunctional";

const Games = () => {
  const [games, setGames] = useState([]);
  const [genres, setGenres] = useState([]);
  const [searchName, setSearchName] = useState("");
  const [checkedState, setCheckedState] = useState(new Map());
  const [showGenreResults, setShowGenreResults] = useState(false);
  useEffect(() => {
    //get games
    const getGames = async () => {
      const gamesFromServer = await fetchGames();
      let gamesJson = await gamesFromServer.json();
      setGames(gamesJson);
    };
    getGames();

    const setStarterGenres = async () => {
      const result = await fetchGenres();
      let genresJson = await result.json();
      await setGenres(genresJson);
      let genresChecks = checkedState;
      for (let index = 0; index < genresJson.length; index++) {
        genresChecks.set(genresJson[index].name, false);
      }
      setCheckedState(genresChecks);
    };

    setStarterGenres();
  }, []);

  const handleOnChange = (name) => {
    let updatedCheckedState = checkedState;
    let IsPressed = updatedCheckedState.get(name);
    updatedCheckedState.set(name, !IsPressed);
    setCheckedState(updatedCheckedState);
    FindByGenres();
  };

  const FindByName = () => {
    const getGames = async () => {
      const gamesFromServer = await fetchGamesByName(searchName);
      let jsonResult = await gamesFromServer.json();
      setGames(jsonResult);
    };
    getGames();
  };

  const FindByGenres = () => {
    const getGames = async () => {
      const gamesFromServer = await fetchGamesByGenres(checkedState, genres);
      let jsonResult = await gamesFromServer.json();
      setGames(jsonResult);
    };
    getGames();
  };

  //
  // const clearFilters = () => {
  //   const getGames = async () => {
  //     const gamesFromServer = await fetchGames();
  //     let jsonResult = await gamesFromServer.json();
  //     setGames(jsonResult);
  //   };
  //   getGames();
  // };

  const getGenresBlock = (ids) => {
    if (genres.length !== 0 && ids.length !== 0) {
      let genresString = "";
      genres.forEach((element) => {
        if (ids.find((id) => id === element.id) !== null) {
          genresString += element.name + "/";
        }
      });
      genresString = genresString.slice(0, genresString.length - 1);
      return <> {genresString} </>;
    }
    return <></>;
  };

  const getActiveGenres = () => {
    let GenresNameToShow = [];
    for (const [key, value] of checkedState) {
      if (value === true) {
        GenresNameToShow.push(key);
      }
    }

    const filteredGenres = genres.filter((element) =>
      GenresNameToShow.includes(element.name)
    );

    return filteredGenres.length > 0 ? (
      <div className="game-genres">
        {filteredGenres.map((genre) => (
          <div key={genre.id} className="game-genres-item">
            <i
              onClick={() => {
                handleOnChange(genre.name);
              }}
              className="fa-solid fa-xmark"
            ></i>
            {genre.name}
          </div>
        ))}
      </div>
    ) : (
      <></>
    );
  };

  return (
    <>
      <div className="games-filters">
        <div className="games-filters-left">
          <button
            className="genre-filter-button"
            onClick={() => {
              setShowGenreResults(!showGenreResults);
            }}
          >
            <i className="fa-solid fa-plus"></i> Add genre
          </button>
          {showGenreResults ? (
            <div className="genres-filter">
              <div className="genres-filter-grid">
                {genres.map((item) => {
                  return (
                    <div key={item.id}>
                      <input
                        type="checkbox"
                        value={checkedState.get(item.name)}
                        checked={checkedState.get(item.name)}
                        onChange={() => handleOnChange(item.name)}
                      />
                      <label>{item.name}</label>
                    </div>
                  );
                })}
              </div>
            </div>
          ) : null}
          <div>{getActiveGenres()}</div>
        </div>

        <div className="games-filters-right">
          <div className="game-search">
            <input
              type="text"
              placeholder="Search"
              value={searchName}
              onChange={(e) => setSearchName(e.target.value)}
              onKeyDown={(event) => {
                if (event.key === "Enter") {
                  FindByName();
                }
              }}
            />
            <button
              className="games-filters-button"
              onClick={() => {
                FindByName();
              }}
            >
              <i className="fa-solid fa-magnifying-glass"></i> Search
            </button>
          </div>
          <div className="game-add-div">
            <Link className="game-add-link-button" to={"/AddGame"}>
              Add Game
            </Link>
          </div>
        </div>
      </div>
      <div className="games">
        {games.length >= 1 ? (
          games.map((game) => (
            <div
              className={
                game.id % 4 > 0
                  ? "games-container-small"
                  : "games-container-big"
              }
              key={game.id}
            >
              <Link className="games-container-link" to={"/Game/" + game.id}>
                <GameEditFunctional
                  gameId={game.id}
                  classNameForOverideBlock={
                    game.id % 4 > 0
                      ? "game-functional-block-small"
                      : "game-functional-block-big"
                  }
                  classNameForImage={
                    game.id % 4 > 0 ? "game-image-small" : "game-image-big"
                  }
                  GameImageUrl={game.imageUrl}
                />
                <div
                  className={game.id % 4 > 0 ? "" : "games-gray-baground-part"}
                >
                  <div className="games-container-part">
                    <div className="games-left-price">{game.price}$</div>
                    <div className="games-right-buy">
                      <button className="green-button">BUY</button>
                    </div>
                  </div>
                  <div>
                    <div className="games-container-genres">
                      {getGenresBlock(game.genresIds)}
                    </div>
                    <div className="games-container-name">{game.name}</div>
                  </div>
                </div>
              </Link>
            </div>
          ))
        ) : (
          <></>
        )}
      </div>
    </>
  );
};

export default Games;
