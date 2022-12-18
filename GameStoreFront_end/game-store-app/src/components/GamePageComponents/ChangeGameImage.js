import { useState, useEffect } from 'react'
import { json, Link, useParams } from 'react-router-dom'
import React from 'react'
import Game from '../Game';

const ChangeGameImage = () => {

    const [image, setImage] = useState(null);
    const { GameId } = useParams();

    const fetchImage = async () => {

        var formdata = new FormData();
        formdata.append("UploadedFile", image[0]);

        var requestOptions = {
        method: 'PUT',
        body: formdata,
        redirect: 'follow'
        };

        fetch("https://localhost:7025/api/GameImage/1", requestOptions)
        .then(response => response.text())
        .then(result => console.log(result))
        .catch(error => console.log('error', error));

    }

    return (
        <div >
            <label>Image of game</label>
            <input
                type='file'
                onChange={(e) => setImage(e.target.files)}
            />
            <button onClick={() => fetchImage()}>Change game image</button>
        </div>
    )
}

export default ChangeGameImage