import { useState, useEffect } from 'react';
import { Link, useParams } from 'react-router-dom';

import fetchUserGetCurrent from './Fetches/fetchUsers/fetchUsersGet/fetchUserGetCurrent';
import getCookie from './JsFunctions/getCookie';
import GetUserImage from './userPageComponents/GetUserImage';
import ChangeUser from './userPageComponents/ChangeUser';
import ChangeUserImage from './userPageComponents/ChangeUserImage';
const User = () => {
    const [user, setUser] = useState([]);
    
    useEffect(() => {
        const getUser = async () => {
            const token=getCookie("access_token");
            const result = await fetchUserGetCurrent(token);
            let resultJson= await result.json();
            setUser(resultJson);   
        };
        getUser();

    }, []);


    return (
        <div>
            <p>Email {user.email}</p>
            <p>Full name {user.firstName+" "+user.lastName}</p>
            <p>Password {user.password}</p>
            <p>User name {user.userName}</p>
            <GetUserImage avatarImageUrl={user.avatarImageUrl} />
            <ChangeUser/>
            <ChangeUserImage/>
        </div>
    )
}

export default User;