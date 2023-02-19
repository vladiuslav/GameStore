import { useState, useEffect } from "react";
import { Link, useParams } from "react-router-dom";
import React from "react";
import ChangeUserImage from "./userPageComponents/ChangeUserImage";
import fetchUserGetCurrent from "./Fetches/fetchUsers/fetchUsersGet/fetchUserGetCurrent";
import fetchChangeUser from "./Fetches/fetchUsers/fetchChangeUser";
import getCookie from "./JsFunctions/getCookie";
import FlashBlock from "./FlashBlock";

const ChangeUser = () => {
  const [isShowErrorBlock, setIsShowErrorBlock] = useState(false);
  const [errorText, setErrorText] = useState("");

  const [email, setEmail] = useState("");
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [password, setPassword] = useState("");
  const [userName, setUserName] = useState("");

  useEffect(() => {
    const getUser = async () => {
      const token = getCookie("access_token");
      const result = await fetchUserGetCurrent(token);
      let userFromServer = await result.json();
      setEmail(userFromServer.email);
      setFirstName(userFromServer.firstName);
      setLastName(userFromServer.lastName);
      setPassword(userFromServer.password);
      setUserName(userFromServer.userName);
    };
    getUser();
  }, []);

  const changeUser = (e) => {
    e.preventDefault();

    if (
      firstName.length < 3 ||
      lastName.length < 3 ||
      userName.length < 3 ||
      email.length < 3
    ) {
      setErrorText("Some input is empty or have less then 3 letters");
      setIsShowErrorBlock(true);
      return;
    }
    if (password.length < 8) {
      setErrorText("Input is empty or have less 1then 8 letters");
      setIsShowErrorBlock(true);
      return;
    }

    const processFetch = async () => {
      let result = await fetchChangeUser({
        firstName,
        lastName,
        userName,
        email,
        password,
      });
      if (result.status === 200) {
        window.location.reload();
        return;
      } else if (result.status === 400) {
        setErrorText("Wrong input.");
        setIsShowErrorBlock(true);
        return;
      } else {
        setErrorText("Error" + result.status);
        setIsShowErrorBlock(true);
        return;
      }
    };
    processFetch();

    window.location.reload(false);
  };
  //render
  return (
    <div className="dark-background">
      <div
        onClick={(e) => {
          e.preventDefault();
          setIsShowErrorBlock(false);
        }}
      >
        <FlashBlock massage={errorText} isShow={isShowErrorBlock} />
      </div>
      <h1>Change User</h1>
      <div>
        <p>First name</p>
        <input
          type="text"
          placeholder="First name"
          value={firstName}
          onChange={(e) => setFirstName(e.target.value)}
        />
      </div>
      <div>
        <p>Last name</p>
        <input
          type="text"
          placeholder="Last name"
          value={lastName}
          onChange={(e) => setLastName(e.target.value)}
        />
      </div>
      <div>
        <p>User name</p>
        <input
          type="text"
          placeholder="User name"
          value={userName}
          onChange={(e) => setUserName(e.target.value)}
        />
      </div>
      <div>
        <p>Email</p>
        <input
          type="text"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
      </div>
      <div>
        <p>Password</p>
        <input
          type="text"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
      </div>

      <button className="green-button" onClick={(e) => changeUser(e)}>
        Change user
      </button>
      <ChangeUserImage />
    </div>
  );
};

export default ChangeUser;
