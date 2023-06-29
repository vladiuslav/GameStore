import React from "react";
import { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import fetchUserGetCurrent from "./Fetches/fetchUsers/fetchUsersGet/fetchUserGetCurrent";
import GetUserImage from "./userPageComponents/GetUserImage";
import CheckIsTokenExpired from "./JsFunctions/CheckIsTokenExpired";

const User = () => {
  const navigate = useNavigate();
  const [user, setUser] = useState([]);
  useEffect(() => {
    const getUser = async () => {
      if (!(await CheckIsTokenExpired())) {
        navigate("/");
        return;
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
