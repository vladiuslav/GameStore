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
  const [checkedState, setCheckedState] = useState([]);
  const [showResults, setShowResults] = useState(false);
  useEffect(() => {
    //get games
    const getGames = async () => {
      const gamesFromServer = await fetchGames();
      setGames(gamesFromServer);
    };
    getGames();

    //get ganres
    const getGanres = async () => {
      const ganresFromServer = await fetchGanres();
      setGanres(ganresFromServer);
      return await ganresFromServer;
    };
    let ganresFromServer = getGanres();

    const getChecks = async (ganresFromServer) => {
      let ganresChecks = [];
      await ganresFromServer.then((value) => {
        ganresChecks = Array(value.length).fill(false);
      });

      setCheckedState(ganresChecks);
    };

    getChecks(ganresFromServer);
  }, []);

  const showGanresOnClick = () => {
    setShowResults(!showResults);
  };

  // CheckedFunction
  const handleOnChange = (position) => {
    const updatedCheckedState = checkedState.map((item, index) =>
      index === position ? !item : item
    );
    setCheckedState(updatedCheckedState);
  };

  const FindByName = () => {
    const getGames = async () => {
      const gamesFromServer = await fetchGamesByName(searchName);
      setGames(gamesFromServer);
    };
    getGames();
  };
  const FindByGanres = () => {
    const getGames = async () => {
      const gamesFromServer = await fetchGamesByGanres(checkedState);
      setGames(gamesFromServer);
    };
    getGames();
  };

  const clearFilters = () => {
    const getGames = async () => {
      const gamesFromServer = await fetchGames();
      setGames(gamesFromServer);
    };
    getGames();
  };

  const getGanres = (ids) => {
    if (ganres.length != 0) {
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
                    value={checkedState[item.id]}
                    onChange={() => handleOnChange(item.id)}
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
        {games.map((game) => (
          <div className="game-container" key={game.id}>
            <Link to={"/Game/" + game.id}>
              <GameImageSmall GameImageUrl={game.imageUrl} />
              <p>
                {game.name} {game.price}$
              </p>
              <div>{getGanres(game.ganresIds)}</div>
              <p>
                {game.description.length < 20
                  ? game.description
                  : game.description.slice(0, 40) + "..."}
              </p>
            </Link>
          </div>
        ))}
      </div>
      <AddGameComponent />
    </>
  );
};

export default Games;
