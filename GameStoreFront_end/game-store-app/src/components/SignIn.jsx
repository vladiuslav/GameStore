import React from "react";
import { useNavigate } from "react-router-dom";
import { useState } from "react";
import fetchUserRegestration from "./Fetches/fetchUsers/fetchUserRegestration";

const SignIn = (props) => {
  const navigate = useNavigate();
  const [isShowEmptyError, setIsShowEmptyError] = useState(false);

  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [userName, setUserName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const createAccount = (e) => {
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

    if (password.length < 8) {
      setIsShowEmptyError(true);
      return;
    }

    const processFetch = async () => {
      let result = await fetchUserRegestration({
        firstName,
        lastName,
        userName,
        email,
        password,
      });
      if (result.ok) {
        alert("Account created");
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
        <h1>User registration</h1>
        <div className="user-form-part">
          <div className="user-form-label">First name</div>
          {isShowEmptyError && firstName.length < 3 ? (
            <p className="error-text">
              First name empty or have less then 3 letters
            </p>
          ) : (
            <></>
          )}
          <input
            className="user-form-input"
            type="text"
            placeholder="First name"
            value={firstName}
            onChange={(e) => setFirstName(e.target.value)}
          />
        </div>
        <div className="user-form-part">
          <div>Last name</div>
          {isShowEmptyError && lastName.length < 3 ? (
            <p className="error-text">
              Last name empty or have less then 3 letters
            </p>
          ) : (
            <></>
          )}
          <input
            className="user-form-input"
            type="text"
            placeholder="Last name"
            value={lastName}
            onChange={(e) => setLastName(e.target.value)}
          />
        </div>
        <div className="user-form-part">
          <div>User name</div>
          {isShowEmptyError && userName.length < 3 ? (
            <p className="error-text">
              User name empty or have less then 3 letters
            </p>
          ) : (
            <></>
          )}
          <input
            className="user-form-input"
            type="text"
            placeholder="User name"
            value={userName}
            onChange={(e) => setUserName(e.target.value)}
          />
        </div>
        <div className="user-form-part">
          <div>Email</div>
          {isShowEmptyError && email.length < 3 ? (
            <p className="error-text">
              Email empty or have less then 3 letters
            </p>
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
              Password empty or have less then 8 letters
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
          <button className="green-button" onClick={(e) => createAccount(e)}>
            Create account
          </button>
        </div>
      </div>
    </div>
  );
};

export default SignIn;
