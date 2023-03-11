import React from "react";
import { useState } from "react";
import FlashBlock from "./FlashBlock";
import fetchUserLogin from "./Fetches/fetchUsers/fetchUserLogin";
import setCookie from "./CokieFunctions/setCookie";

const LogIn = (props) => {
  const [isShowErrorBlock, setIsShowErrorBlock] = useState(false);
  const [errorText, setErrorText] = useState("");

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const loginAccount = async (e) => {
    e.preventDefault();

    if (email.length < 3) {
      setErrorText("Email must be more then 3 characters.");
      setIsShowErrorBlock(true);
      return;
    }
    if (password.length < 8) {
      setErrorText("Password must be more then 8 characters.");
      setIsShowErrorBlock(true);
      return;
    }

    const processFetch = async () => {
      let result = await fetchUserLogin({ email, password });
      if (result.status === 200) {
        let resultJson = await result.json();
        setCookie("token", resultJson.token, 5);
        setCookie("email", email, 5);
        setCookie("refresh_token", resultJson.refreshToken, 100000);
        props.checkIsLogged();
        props.setIsOpenForm();
        return;
      } else if (result.status === 400) {
        setErrorText("Wrong input or user don`t exist.");
        setIsShowErrorBlock(true);
        return;
      } else if (result.status === 404) {
        setErrorText("Wrong login or password");
        setIsShowErrorBlock(true);
        return;
      } else {
        setErrorText("Error" + result.status);
        setIsShowErrorBlock(true);
        return;
      }
    };
    processFetch();
  };

  return (
    <div className="central-form">
      <div
        onClick={(e) => {
          e.preventDefault();
          setIsShowErrorBlock(false);
        }}
      >
        <FlashBlock massage={errorText} isShow={isShowErrorBlock} />
      </div>
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
          <input
            className="user-form-input"
            type="text"
            placeholder="Password"
            value={password}
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
