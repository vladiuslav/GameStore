import { useState, useEffect } from 'react'
import { Link, useParams } from 'react-router-dom'
import React from 'react'

import fetchGame from '../Fetches/fetchGames/fetchGetGames/fetchGame'
import fetchGanres from '../Fetches/fetchGaneres/fetchGanres'
import fetchChangeGame from '../Fetches/fetchGames/fetchChangeGame'

import FlashBlock from '../FlashBlock';

const ChangeGameComponent = () => {
    const [isShowErrorBlock,setIsShowErrorBlock] = useState(false);
    const [errorText,setErrorText] = useState('');
    const [genres, setGenres] = useState([]);
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [price, setPrice] = useState('');
    const [checkedState, setCheckedState] = useState(new Map());
    const { GameId } = useParams(0);

    useEffect(() => {
        const getGame = async () => {
            const result = await fetchGame(GameId);
            let gameJson = await result.json();

            setName(gameJson.name);
            setDescription(gameJson.description);
            setPrice(gameJson.price);
            

            const result2 = await fetchGanres();
            const ganresJson = await result2.json();
            setGenres(ganresJson);
            let ganresChecks=checkedState;
            for (let index = 0; index < ganresJson.length; index++) {
                ganresChecks.set
                (
                    ganresJson[index].name,
                    (gameJson.genresIds.some(genreId=>genreId==ganresJson[index].id)) ? true : false 
                );
            }
            
            setCheckedState(ganresChecks);

        }
        getGame();
    }, [])

    // CheckedFunction
    const handleOnChange = (name) => {
        let updatedCheckedState =checkedState;
        let IsPressed = updatedCheckedState.get(name);
        updatedCheckedState.set(name,!IsPressed);
        setCheckedState(updatedCheckedState);
    };

    const changeGame = (e) => {
        e.preventDefault();

        if(name.length<1 || description.length<1 || price.length<1){
            setErrorText('Some input is empty');
            setIsShowErrorBlock(true);
            return ;
        }

        if(name.length<3||name.length>40){
            setErrorText('Name too short or too long');
            setIsShowErrorBlock(true);
            return ;
        }
        if(description.length<10||description.length>400){
            setErrorText('Description too short or too long');
            setIsShowErrorBlock(true);
            return ;
        }
        const processFetch = async()=> {
            let result = await fetchChangeGame({ name, description, price, checkedState , genres, GameId });
                if(result.status === 200){
                    window.location.reload();
                    return;
                }else if(result.status === 400){
                    setErrorText('Game name exist or wrong price number.');
                    setIsShowErrorBlock(true);
                    return;
                }else if(result.status === 404){
                    setErrorText('Game doesn`t exist');
                    setIsShowErrorBlock(true);
                    return;
                }else{
                    setErrorText('Error'+result.status);
                    setIsShowErrorBlock(true);
                    return;
                }
        }
        processFetch();
    }

    return (
        <div className="game-add-form">
            <div onClick={(e)=>{
                e.preventDefault();
                setIsShowErrorBlock(false);
            }}>
            <FlashBlock massage={errorText} isShow={isShowErrorBlock}/>
            </div>
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
                    {genres.map((item) => (
                        <li key={item.id}>
                            <input type="checkbox"
                                value={checkedState.get(item.name)}
                                onChange={() => handleOnChange(item.name)}
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