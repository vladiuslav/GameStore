import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import React from "react";
import fetchUserGetCurrent from "./Fetches/fetchUsers/fetchUsersGet/fetchUserGetCurrent";
import fetchChangeUser from "./Fetches/fetchUsers/fetchChangeUser";
import fetchChangeUserImage from "./Fetches/fetchUsers/fetchChangeUserImage";

const ChangeUser = () => {
  const navigate = useNavigate();
  const [isShowEmptyError, setIsShowEmptyError] = useState(false);

  const [email, setEmail] = useState("");
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [password, setPassword] = useState("");
  const [userName, setUserName] = useState("");
  const [image, setImage] = useState(null);

  useEffect(() => {
    const getUser = async () => {
      const result = await fetchUserGetCurrent();
      let userFromServer = await result.json();

      setEmail(userFromServer.email);
      setFirstName(userFromServer.firstName);
      setLastName(userFromServer.lastName);
      setUserName(userFromServer.userName);
    };
    getUser();
  }, []);

  const changeUser = (e) => {
    e.preventDefault();

    if (firstName.length < 3) {
      setIsShowEmptyError(true);
      return;
    }
    if (lastName.length < 3) {
      setIsShowEmptyError(true);
      return;
    }
    if (email.length < 3) {
      setIsShowEmptyError(true);
      return;
    }

    if (password.length > 0 && password.length < 8) {
      setIsShowEmptyError(true);
      return;
    }

    const processFetch = async () => {
      let result = await fetchChangeUser(
        firstName,
        lastName,
        userName,
        email,
        password.length === 0 ? "" : password
      );
      if (result.ok) {
        await changeImage();
        navigate("/");
        return;
      } else {
        let errorBody = await result.json();
        alert(
          errorBody.title +
            "\n" +
            (errorBody.detail !== undefined ? errorBody.detail : "")
        );
        return;
      }
    };
    processFetch();
  };

  const changeImage = async () => {
    if (image === null || image.length < 1) {
      return;
    }

    const processFetch = async () => {
      let result = await fetchChangeUserImage(image[0]);
      if (result.ok) {
        return;
      } else {
        let errorBody = await result.json();
        alert(
          errorBody.title +
            "\n" +
            (errorBody.detail !== undefined ? errorBody.detail : "")
        );
        return;
      }
    };
    processFetch();
  };

  //render
  return (
    <div className="dark-background">
      <h1>Change User</h1>
      <div>
        <p>First name</p>
        {isShowEmptyError && firstName.length < 3 ? (
          <p className="error-text">
            First name empty or have less then 3 letters
          </p>
        ) : (
          <></>
        )}
        <input
          type="text"
          placeholder="First name"
          value={firstName}
          onChange={(e) => setFirstName(e.target.value)}
        />
      </div>
      <div>
        <p>Last name</p>
        {isShowEmptyError && lastName.length < 3 ? (
          <p className="error-text">
            Last name empty or have less then 3 letters
          </p>
        ) : (
          <></>
        )}
        <input
          type="text"
          placeholder="Last name"
          value={lastName}
          onChange={(e) => setLastName(e.target.value)}
        />
      </div>
      <div>
        <p>User name</p>
        {isShowEmptyError && userName.length < 3 ? (
          <p className="error-text">
            User name empty or have less then 3 letters
          </p>
        ) : (
          <></>
        )}
        <input
          type="text"
          placeholder="User name"
          value={userName}
          onChange={(e) => setUserName(e.target.value)}
        />
      </div>
      <div>
        <p>Email</p>
        {isShowEmptyError && email.length < 3 ? (
          <p className="error-text">Email empty or have less then 3 letters</p>
        ) : (
          <></>
        )}
        <input
          type="email"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
      </div>
      <div>
        <p>Password</p>
        {isShowEmptyError && password.length < 8 ? (
          <p className="error-text">
            Password empty or have less then 8 letters
          </p>
        ) : (
          <></>
        )}
        <input
          type="password"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
      </div>
      <div>
        <label>Image of game</label>
        <input type="file" onChange={(e) => setImage(e.target.files)} />
      </div>
      <button className="green-button" onClick={(e) => changeUser(e)}>
        Change user
      </button>
    </div>
  );
};

export default ChangeUser;
