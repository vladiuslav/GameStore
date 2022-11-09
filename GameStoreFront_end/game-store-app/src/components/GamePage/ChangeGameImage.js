import { useState, useEffect } from 'react'
import { json, Link, useParams } from 'react-router-dom'
import React from 'react'
import Game from '../Game';

const ChangeGameImage = () => {

    const [image, setImage] = useState(null);
    const { GameId } = useParams();

    const fetchImage = async () => {
        var data = new FormData()
        data.append("UploadedFile", image);
        console.log(data);

        const res = await fetch('https://localhost:7025/api/GameImage/' + GameId, {
            method: 'PUT',
            headers: {
                'Accept': '*/*',
                'Content-Type': "multipart/form-data"
            },
            body: JSON.stringify(data),
        });

    }

    return (
        <div >
            <label>Image of game</label>
            <input
                type='file'
                onChange={(e) => setImage(e.target.files[0])}
            />
            <button onClick={() => fetchImage()}>Change game image</button>
        </div>
    )
}

export default ChangeGameImage