import { useState, useEffect } from 'react'
import { Link, useParams } from 'react-router-dom'
import React from 'react'
import Game from '../Game';

const ChangeGameImage = () => {

    const [image, setImage] = useState(null);
    const { GameId } = useParams();

    const fetchImage = async () => {
        var data = new FormData()
        data.append("file", image);
        const res = await fetch('https://localhost:7025/ImgUpload/' + GameId, {
            method: 'POST',
            headers: {
                'mode': 'cors',
                'Accept': "*/*",
                'Content-Type': "multipart/form-data"
            },
            body: data
        });
        console.log(res);

    }

    return (
        <div >
            <label>Image of game</label>
            <input
                type='file'
                value={image}
                onChange={(e) => setImage(e.target.value)}
            />
            <button onClick={() => fetchImage()}>Change game image</button>
        </div>
    )
}

export default ChangeGameImage