import React from "react";
import { useState } from "react";
import fetchUserLogin from "./Fetches/fetchUsers/fetchUserLogin";

const LogIn = (props) => {
  const [isShowEmptyError, setIsShowEmptyError] = useState(false);

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const loginAccount = async (e) => {
    e.preventDefault();

    if (email.length < 3) {
      setIsShowEmptyError(true);
      return;
    }
    if (password.length < 8) {
      setIsShowEmptyError(true);
      return;
    }

    const processFetch = async () => {
      let result = await fetchUserLogin({ email, password });
      if (result.status === 200) {
        let resultJson = await result.json();
        localStorage.setItem("token", resultJson.token);
        localStorage.setItem("refresh_token", resultJson.refreshToken);
        localStorage.setItem("expiredTokenTime", resultJson.expiresAt);
        localStorage.setItem("email", email);
        props.checkIsLogged();
        props.setIsOpenForm();
        return;
      } else if (result.status === 400) {
        alert("Wrong input or user don`t exist.");
        return;
      } else if (result.status === 404) {
        alert("Wrong login or password");
        return;
      } else {
        alert("Error" + result.status);
        return;
      }
    };
    processFetch();
  };

  return (
    <div className="central-form">
      <div className="user-form">
        <div
          onClick={() => {
            props.setIsOpenForm();
          }}
        >
          <i className="fa-solid fa-xmark"></i>
        </div>
        <h1>Please Login</h1>
        <div className="user-form-part">
          <div>Email</div>
          {isShowEmptyError && email.length < 3 ? (
            <p className="error-text">Email must be more then 3 characters.</p>
          ) : (
            <></>
          )}
          <input
            className="user-form-input"
            type="text"
            placeholder="Email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </div>
        <div className="user-form-part">
          <div>Password</div>
          {isShowEmptyError && password.length < 8 ? (
            <p className="error-text">
              Password must be more then 8 characters.
            </p>
          ) : (
            <></>
          )}
          <input
            className="user-form-input"
            type="text"
            placeholder="Password"
            value={"*".repeat(password.length)}
            onChange={(e) => setPassword(e.target.value)}
          />
        </div>
        <div className="user-form-part">
          <button className="green-button" onClick={(e) => loginAccount(e)}>
            Login Account
          </button>
        </div>
      </div>
    </div>
  );
};

export default LogIn;
