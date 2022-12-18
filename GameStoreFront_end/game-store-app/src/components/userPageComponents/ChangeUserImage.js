import { useState, useEffect } from 'react'
import { json, Link, useParams } from 'react-router-dom'
import React from 'react'
import Game from '../Game';
import fetchChangeUserImage from '../Fetches/fetchUsers/fetchChangeUserImage';
import getCookie from '../JsFunctions/getCookie';

const ChangeUserImage = () => {
    const [image, setImage] = useState(null);

    return (
        <div >
            <label>Image of game</label>
            <input
                type='file'
                onChange={(e) => setImage(e.target.files)}
            />
            <button onClick={() => {
                const token = getCookie("access_token");
                fetchChangeUserImage(image[0],token)
            }}>Change user image</button>
        </div>
    )
}

export default ChangeUserImage