import { useState, useEffect } from 'react'
import { Link, useParams } from 'react-router-dom'
import React from 'react'

import fetchGame from '../Fetches/fetchGame'
import fetchGanres from '../Fetches/fetchGanres'
import fetchChangeGame from '../Fetches/fetchChangeGame'

const ChangeGameComponent = () => {

    const [ganres, setGanres] = useState([]);
    const [imageUrl, setImage] = useState('');
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [price, setPrice] = useState('');
    const [checkedState, setCheckedState] = useState([]);
    const { GameId } = useParams(0);

    useEffect(() => {
        const getGame = async () => {
            const gameFromServer = await fetchGame(GameId);

            setName(gameFromServer.name);
            setDescription(gameFromServer.description);
            setPrice(gameFromServer.price);
            setImage(gameFromServer.imageUrl);
            let ganresChecks = [];
            for (let index = 0, arrayIndex = 0; arrayIndex < gameFromServer.ganresIds.length; index++) {
                if (gameFromServer.ganresIds[arrayIndex] == index) {
                    ganresChecks.push(true);
                    arrayIndex++;
                } else {
                    ganresChecks.push(false);
                }
            }
            setCheckedState(ganresChecks);
        }
        getGame();
        //get ganres
        const getGanres = async () => {
            const ganresFromServer = await fetchGanres();
            setGanres(ganresFromServer);
        }
        getGanres();
    }, [])

    // CheckedFunction
    const handleOnChange = (position) => {
        const updatedCheckedState = checkedState.map((item, index) =>
            index === position ? !item : item
        );
        setCheckedState(updatedCheckedState);
    };

    //File Picker
    const changeHandler = (event) => {
        setSelectedFile(event.target.files[0]);
        setIsSelected(true);
    };

    //CreateNewGame 
    const changeGame = (e) => {
        e.preventDefault()

        //add here new date check
        // if (!text) {
        //     alert('Please add a task')
        //     return
        // }

        fetchChangeGame({ name, description, price, checkedState, imageUrl, GameId });
    }

    //render
    return (
        <div className="game-add-form">
            <h1>Change Game</h1>
            <div >
                <p>Name</p>
                <input
                    type='text'
                    placeholder='Add name'
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                />
            </div>
            <div>
                <p>Game description</p>
                <input
                    type='text'
                    placeholder='Add description'
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                />
            </div>
            <div>
                <p>Game ganres</p>
                <ul>
                    {ganres.map((item) => (
                        <li key={item.id}>
                            <input type="checkbox"
                                checked={checkedState[item.id]}
                                onChange={() => handleOnChange(item.id)}
                            /><label>{item.name}</label></li>
                    ))}
                </ul>
            </div>
            <div>
                <p>Game price</p>
                <input
                    type='text'
                    placeholder='Add price'
                    value={price}
                    onChange={(e) => setPrice(e.target.value)}
                />
            </div>

            <button onClick={(e) => (changeGame(e))}>ChangeGame</button>
        </div>
    )
}

export default ChangeGameComponent