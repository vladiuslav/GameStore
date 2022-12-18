import { useState, useEffect } from 'react'
import { Link, useParams } from 'react-router-dom'
import React from 'react'

import fetchUserGetCurrent from '../Fetches/fetchUsers/fetchUsersGet/fetchUserGetCurrent'
import fetchChangeUser from '../Fetches/fetchUsers/fetchChangeUser'
import getCookie from '../JsFunctions/getCookie'
const ChangeUser = () => {

    const [email, setEmail] = useState('');
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [password, setPassword] = useState('');
    const [userName, setUserName] = useState('');

    useEffect(() => {
        const getUser = async () => {
            const token = getCookie("access_token");
            const userFromServer = await fetchUserGetCurrent(token);
            setEmail(userFromServer.email);
            setFirstName(userFromServer.firstName);
            setLastName(userFromServer.lastName);
            setPassword(userFromServer.password);
            setUserName(userFromServer.userName);
        };
        getUser();

    }, []);

    const changeUser = (e) => {
        e.preventDefault()

        //add here new date check
        // if (!text) {
        //     alert('Please add a task')
        //     return
        // }

        fetchChangeUser({ firstName, lastName, userName,email,password });
        window.location.reload(false);
    }
    //render
    return (
        <div>
            <h1>Change User</h1>
            <div>
                <p>First name</p>
                <input
                    type='text'
                    placeholder='First name'
                    value={firstName}
                    onChange={(e) => setFirstName(e.target.value)}
                />
            </div>
            <div>
                <p>Last name</p>
                <input
                    type='text'
                    placeholder='Last name'
                    value={lastName}
                    onChange={(e) => setLastName(e.target.value)}
                />
            </div>
            <div>
                <p>User name</p>
                <input
                    type='text'
                    placeholder='User name'
                    value={userName}
                    onChange={(e) => setUserName(e.target.value)}
                />
            </div>
            <div>
                <p>Email</p>
                <input
                    type='text'
                    placeholder='Email'
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                />
            </div>
            <div>
                <p>Password</p>
                <input
                    type='text'
                    placeholder='Password'
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                />
            </div>

            <button onClick={(e) => (changeUser(e))}>Change user</button>
        </div>
    )
}

export default ChangeUser