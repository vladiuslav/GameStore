import React from "react";
import { useState } from "react";
import fetchUserLogin from "./Fetches/fetchUsers/fetchUserLogin";
import { useNavigate } from "react-router-dom";

const LogIn = (props) => {
  const navigate = useNavigate();
  const [isShowEmptyError, setIsShowEmptyError] = useState(false);
  const [isShowEmailValidError, setIsShowEmailValidError] = useState(false);

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const loginAccount = async (e) => {
    e.preventDefault();

    const emailRegex = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i;
    if (!emailRegex.test(email)) {
      setIsShowEmailValidError(true);
      return;
    }

    if (password.length < 8) {
      setIsShowEmptyError(true);
      return;
    }

    const processFetch = async () => {
      let result = await fetchUserLogin({ email, password });
      if (result.ok) {
        let resultJson = await result.json();
        localStorage.setItem("token", resultJson.token);
        localStorage.setItem("refresh_token", resultJson.refreshToken);
        localStorage.setItem("expiredTokenTime", resultJson.expiresAt);
        localStorage.setItem("email", email);
        props.checkIsLogged();
        props.setIsOpenForm();
        navigate("/User");
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
          {isShowEmailValidError ? (
            <p className="error-text">Email invalid.</p>
          ) : (
            <></>
          )}
          <input
            className="user-form-input"
            type="email"
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
            type="password"
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
