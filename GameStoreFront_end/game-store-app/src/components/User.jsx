import React from "react";
import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import fetchUserGetCurrent from "./Fetches/fetchUsers/fetchUsersGet/fetchUserGetCurrent";
import GetUserImage from "./userPageComponents/GetUserImage";
import fetchGenerateToken from "./Fetches/fetchUsers/fetchGenerateToken";

const User = () => {
  const [user, setUser] = useState([]);
  useEffect(() => {
    const getUser = async () => {
      let expiresTime = new Date(localStorage.getItem("expiredTokenTime"));
      if (expiresTime.getTime() < Date.now()) {
        let result = await fetchGenerateToken();
        if (result.status === 200) {
          let resultJson = await result.json();

          localStorage.setItem("token", resultJson.token);
          localStorage.setItem("refresh_token", resultJson.refreshToken);
          localStorage.setItem("expiredTokenTime", resultJson.expiresAt);
        }
      }
      const token = localStorage.getItem("token");
      let result = await fetchUserGetCurrent(token);
      let resultJson = await result.json();
      setUser(resultJson);
    };
    getUser();
  }, []);

  return (
    <div className="user-central-items">
      <div className="user-central-item">Email: {user.email}</div>
      <hr className="user-hr-max-width" />
      <div className="user-central-item">
        User full name: {user.firstName + " " + user.lastName}
      </div>
      <hr className="user-hr-max-width" />
      <GetUserImage
        className="user-image"
        avatarImageUrl={user.avatarImageUrl}
      />
      <Link className="user-change-link-button" to={"/ChangeUser"}>
        Change user info
      </Link>
    </div>
  );
};

export default User;
