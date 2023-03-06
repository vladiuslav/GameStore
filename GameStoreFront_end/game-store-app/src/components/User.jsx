import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import fetchUserGetCurrent from "./Fetches/fetchUsers/fetchUsersGet/fetchUserGetCurrent";
import getCookie from "./CokieFunctions/getCookie";
import GetUserImage from "./userPageComponents/GetUserImage";

const User = () => {
  const [user, setUser] = useState([]);
  const [isShowPassword, setIsShowPassword] = useState(false);
  useEffect(() => {
    const getUser = async () => {
      const token = getCookie("access_token");
      const result = await fetchUserGetCurrent(token);
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
      <div className="user-central-item">
        Password: {isShowPassword ? user.password : "*****"}
        <button
          onClick={() => {
            setIsShowPassword(!isShowPassword);
          }}
          className="green-button-margin"
        >
          {isShowPassword ? "Hide password" : "Show password"}
        </button>
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
