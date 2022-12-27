import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import AddGameComponent from "./GamePageComponents/AddGameComponent";
import fetchGames from "./Fetches/fetchGames/fetchGetGames/fetchGames";
import fetchGamesByGanres from "./Fetches/fetchGames/fetchGetGames/fetchGamesByGanres";
import fetchGamesByName from "./Fetches/fetchGames/fetchGetGames/fetchGamesByName";
import fetchGanres from "./Fetches/fetchGaneres/fetchGanres";
import GameImageSmall from "./GamePageComponents/GameImageSmall";

const Games = () => {
  
  const [games, setGames] = useState([]);
  const [ganres, setGanres] = useState([]);
  const [searchName, setSearchName] = useState("");
  const [checkedState, setCheckedState] = useState(new Map());
  const [showResults, setShowResults] = useState(false);

  useEffect(() => {
    //get games
    const getGames = async () => {
      const gamesFromServer = await fetchGames();
      let gamesJson = await gamesFromServer.json();
      setGames(gamesJson);
    };
    getGames();

    const setStarterGanres=async()=>{

      const result = await fetchGanres();
      let ganresJson = await result.json();
      await setGanres(ganresJson);
      let ganresChecks=checkedState;
      for (let index = 0; index < ganresJson.length; index++) {
        ganresChecks.set(ganresJson[index].name,false);
      }
      setCheckedState(ganresChecks);

    }

    setStarterGanres();

  }, []);

  const showGanresOnClick = () => {
    setShowResults(!showResults);
  };

  // CheckedFunction
  const handleOnChange = (name) => {

    let updatedCheckedState =checkedState;
    let IsPressed = updatedCheckedState.get(name);
    updatedCheckedState.set(name,!IsPressed);
    setCheckedState(updatedCheckedState);
  };

  const FindByName = () => {
    const getGames = async () => {
      const gamesFromServer = await fetchGamesByName(searchName);
      let jsonResult = await gamesFromServer.json();
      setGames(jsonResult);
    };
    getGames();
  };
  
  const FindByGanres = () => {
    const getGames = async () => {
      const gamesFromServer = await fetchGamesByGanres(checkedState,ganres);
      let jsonResult = await gamesFromServer.json();
      setGames(jsonResult);
    };
    getGames();
  };

  const clearFilters = () => {
    const getGames = async () => {
      const gamesFromServer = await fetchGames();
      let jsonResult = await gamesFromServer.json();
      setGames(jsonResult);
    };
    getGames();
  };

  const getGanresBlock = (ids) => {
    if (ganres.length != 0&& ids.length != 0) {
      let GanresString = "";
        ganres.forEach((element) => {
          if (ids.find((id) => id == element.id) != null) {
            GanresString += element.name + "/";
          }
        });
        GanresString = GanresString.slice(0, GanresString.length - 1);
        return <div className="ganre-name"> {GanresString} </div>;
        
    }
    return <></>;
  };

  //render
  return (
    <>
      <ul className="games-filters">
        <li>
          <input
            type="text"
            placeholder="Game name"
            value={searchName}
            onChange={(e) => setSearchName(e.target.value)}
          />
          <button
            onClick={() => {
              FindByName();
            }}
          >
            Search
          </button>
        </li>
        <li>
          <button
            onClick={() => {
              showGanresOnClick();
            }}
          >
            {showResults ? "Hide game ganres" : "Show game ganres"}
          </button>
          {showResults ? <div id="ganresFiltersDiv">
            <ul>
              {ganres.map((item) => (
                <li key={item.id}>
                  <input
                    type="checkbox"
                    value={checkedState.get(item.name)}
                    onChange={() => handleOnChange(item.name)}
                  />
                  <label>{item.name}</label>
                </li>
              ))}
            </ul>
            <button
              onClick={() => {
                FindByGanres();
              }}
            >
              Search by ganres
            </button>
          </div> : null
          }
        </li>
        <li>
          <button
            onClick={() => {
              clearFilters();
            }}
          >
            Clear filters
          </button>
        </li>
      </ul>
      <div className="games">
        {
        (games.length>=1)?
        games.map((game) => (
          <div className="game-container" key={game.id}>
            <Link to={"/Game/" + game.id}>
              <GameImageSmall GameImageUrl={game.imageUrl} />
              <p>
                {game.name} {game.price}$
              </p>
              <div>{getGanresBlock(game.genresIds)}</div>
              <p>
                {game.description.length < 20
                  ? game.description
                  : game.description.slice(0, 40) + "..."}
              </p>
            </Link>
          </div>
        ))
      :<></>
      }
      </div>
      <AddGameComponent />
    </>
  );
};

export default Games;
