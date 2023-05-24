import React from "react";
import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import fetchGames from "./Fetches/fetchGames/fetchGetGames/fetchGames";
import fetchGamesByGenres from "./Fetches/fetchGames/fetchGetGames/fetchGamesByGenres";
import fetchGamesByName from "./Fetches/fetchGames/fetchGetGames/fetchGamesByName";
import fetchGenres from "./Fetches/fetchGaneres/fetchGenres";
import GameEditFunctional from "./GamePageComponents/GameEditFunctional";
import AddCartItem from "./JsFunctions/CartFunctions/AddCartItem";

const Games = () => {
  const [games, setGames] = useState([]);
  const [genres, setGenres] = useState([]);
  const [searchName, setSearchName] = useState("");
  const [checkedState, setCheckedState] = useState(new Map());
  const [showGenreResults, setShowGenreResults] = useState(false);
  useEffect(() => {
    const getGames = async () => {
      const result = await fetchGames();
      let gamesJson = await result.json();
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

  const handleOnChangeGenreFilterItem = (name) => {
    let updatedCheckedState = new Map(checkedState);
    let IsPressed = updatedCheckedState.get(name);
    updatedCheckedState.set(name, !IsPressed);
    setCheckedState(updatedCheckedState);
    FindByGenres(updatedCheckedState);
  };

  const FindByName = () => {
    const getGames = async () => {
      if (searchName.length !== 0) {
        const gamesFromServer = await fetchGamesByName(searchName);
        let jsonResult = await gamesFromServer.json();
        setGames(jsonResult);
      } else {
        const result = await fetchGames();
        let gamesJson = await result.json();
        setGames(gamesJson);
      }
    };
    getGames();
  };

  const FindByGenres = (checkedState) => {
    const getGames = async () => {
      let genresIds = [];
      checkedState.forEach(function(value, key) {
        if (value) {
          genresIds.push(genres.find((genre) => genre.name === key).id);
        }
      });
      if (genresIds.length === 0) {
        const result = await fetchGames();
        let gamesJson = await result.json();
        setGames(gamesJson);
      } else {
        const gamesFromServer = await fetchGamesByGenres(genresIds);
        let jsonResult = await gamesFromServer.json();
        setGames(jsonResult);
      }
    };
    getGames();
  };

  const getGenresBlock = (ids) => {
    if (genres.length !== 0 && ids.length !== 0) {
      let genresString = "";
      let elementNumber = 0;
      genres.forEach((element) => {
        if (ids.find((id) => id === element.id) !== undefined) {
          if (elementNumber++ === 3) {
            genresString += element.name + " ... ";
          } else if (elementNumber <= 3) {
            genresString += element.name + "/";
          }
        }
      });
      genresString = genresString.slice(0, genresString.length - 1);
      return <> {genresString} </>;
    }
    return <></>;
  };

  const gamesmaped = () => {
    let next = 0;

    return games.map((game) => (
      <div
        className={
          next % 4 > 0 ? "games-container-small" : "games-container-big"
        }
        key={game.id}
      >
        <Link className="games-container-link" to={"/Game/" + game.id}>
          <GameEditFunctional
            IsSmallGame={next % 4 > 0}
            gameId={game.id}
            classNameForOverideBlock={
              next % 4 > 0
                ? "game-functional-block-small"
                : "game-functional-block-big"
            }
            classNameForImage={
              next % 4 > 0 ? "game-image-small" : "game-image-big"
            }
            GameImageUrl={game.imageUrl}
          />
        </Link>
        <div className={next++ % 4 > 0 ? "" : "games-gray-baground-part"}>
          <div className="games-container-part">
            <Link className="games-container-link" to={"/Game/" + game.id}>
              <div className="games-left-price">{game.price}$</div>
            </Link>
            <div className="games-right-buy">
              <button
                className="green-button"
                onClick={() => {
                  AddCartItem(game.id, 1);
                }}
              >
                BUY
              </button>
            </div>
          </div>
          <Link className="games-container-link" to={"/Game/" + game.id}>
            <div>
              <div className="games-container-genres">
                {getGenresBlock(game.genresIds)}
              </div>
              <div className="games-container-name">
                {game.name.length > 20
                  ? game.name.slice(0, 20) + "..."
                  : game.name}
              </div>
            </div>
          </Link>
        </div>
      </div>
    ));
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
                handleOnChangeGenreFilterItem(genre.name);
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

  const getGenresWithParent = function() {
    let newGenres = [];
    genres.forEach((element) => {
      if (element.parentGenreId === null) {
        newGenres.push(element);
      } else {
        let id = newGenres.findIndex((g) => g.id === element.parentGenreId);
        newGenres[id].childGenre = element;
      }
    });
    return newGenres;
  };

  const GenreFilterBlock = (props) => {
    let item = props.genre;
    let isChild = props.isChild;

    return (
      <>
        <div key={item.id} className={isChild ? "child-genre" : "parent-genre"}>
          <input
            type="checkbox"
            value={checkedState.get(item.name)}
            checked={checkedState.get(item.name)}
            onChange={() => handleOnChangeGenreFilterItem(item.name)}
          />
          <label>{item.name}</label>
        </div>
        {item.childGenre !== undefined ? (
          <GenreFilterBlock genre={item.childGenre} isChild={true} />
        ) : (
          <></>
        )}
      </>
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
                {getGenresWithParent().map((item) => {
                  return item.parentGenreId === null ? (
                    <GenreFilterBlock genre={item} isChild={false} />
                  ) : (
                    <></>
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
      <div className="games">{games.length >= 1 ? gamesmaped() : <></>}</div>
    </>
  );
};

export default Games;
