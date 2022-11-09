import { useState, useEffect } from 'react'
import React from 'react'
import fetchGanres from '../Fetches/fetchGanres';
import fetchAddGame from '../Fetches/fetchAddGame';
const AddGameComponent = () => {


    const [ganres, setGanres] = useState([]);
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [price, setPrice] = useState('');
    const [checkedState, setCheckedState] = useState([]);

    useEffect(() => {
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

    //CreateNewGame 
    const createGame = (e) => {
        e.preventDefault()

        //add here new date check
        // if (!text) {
        //     alert('Please add a task')
        //     return
        // }

        fetchAddGame({ name, description, price, checkedState });

        setName('');
        setDescription('');
        setPrice('');
        setCheckedState([]);
    }


    //render
    return (
        <div className="game-add-form">
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

            <button onClick={(e) => (createGame(e))}>CreateNewGame</button>
        </div>
    )
}

export default AddGameComponent