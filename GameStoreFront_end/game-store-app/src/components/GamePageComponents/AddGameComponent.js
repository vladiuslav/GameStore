import { useState, useEffect } from 'react'
import React from 'react'
import fetchGanres from '../Fetches/fetchGaneres/fetchGanres';
import fetchAddGame from '../Fetches/fetchGames/fetchAddGame';
import FlashBlock from '../FlashBlock';
const AddGameComponent = () => {

    const [isShowErrorBlock,setIsShowErrorBlock] = useState(false);
    const [errorText,setErrorText] = useState('');
    const [ganres, setGanres] = useState([]);
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [price, setPrice] = useState('');
    const [checkedState, setCheckedState] = useState(new Map());

    useEffect(() => {
        const getGanres = async () => {
            const ganresFromServer = await fetchGanres();
            let ganresJson = await ganresFromServer.json();
            setGanres(ganresJson);

            let ganresChecks=checkedState;
            for (let index = 0; index < ganresJson.length; index++) {
                ganresChecks.set(ganresJson[index].name,false);
            }
            setCheckedState(ganresChecks);
        }
        getGanres();
    }, [])

    // CheckedFunction
    const handleOnChange = (position) => {
        let updatedCheckedState =checkedState;
        let IsPressed = updatedCheckedState.get(name);
        updatedCheckedState.set(name,!IsPressed);
        setCheckedState(updatedCheckedState);
    }; 


    //CreateNewGame 
    const createGame = (e) => {
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
        let result = await fetchAddGame({ name, description, price, checkedState,genres });
            if(result.status === 201){
                setName('');
                setDescription('');
                setPrice('');
                setCheckedState([]);
                window.location.reload();
                return;
            }else if(result.status === 400){
                setErrorText('Game name exist or wrong price number.');
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


    //render
    return (
        <div className="game-add-form">
            <div onClick={(e)=>{
                e.preventDefault();
                setIsShowErrorBlock(false);
            }}>
            <FlashBlock massage={errorText} isShow={isShowErrorBlock}/>
            </div>
            <div>
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
                    {
                      (ganres.length>=1)?  
                    ganres.map((item) => (
                        <li key={item.id}>
                            <input type="checkbox"
                                value={checkedState.get(item.name)}
                                onChange={() => handleOnChange(item.name)}
                            /><label>{item.name}</label></li>
                    ))
                    :<></>
                    }
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

            <button onClick={(e) => (createGame(e))}>CreateNewGame</button>
        </div>
    )
}

export default AddGameComponent