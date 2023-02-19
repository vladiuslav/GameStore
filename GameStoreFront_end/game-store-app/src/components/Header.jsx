import React from "react";
import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import logo from "../Images/Logo.jpg";
import fetchUserRegestration from "./Fetches/fetchUsers/fetchUserRegestration";
import fetchUserLogin from "./Fetches/fetchUsers/fetchUserLogin";
import fetchUserGetCurrent from "./Fetches/fetchUsers/fetchUsersGet/fetchUserGetCurrent";
import setCookie from "./JsFunctions/setCookie";
import getCookie from "./JsFunctions/getCookie";
import FlashBlock from "./FlashBlock";
import GetUserImage from "./userPageComponents/GetUserImage";

const Header = () => {
  const [isLogged, setIsLogged] = useState(false);
  const [user, setUser] = useState(false);
  const [isOpenForm, setIsOpenForm] = useState(false);
  const [isOpenLoginForm, setIsOpenLoginForm] = useState(false);

  useEffect(() => {
    checkIsLogged();
  }, []);

  const logOut = () => {
    setCookie("access_token", " ", 0);
    setCookie("email", " ", 0);
    setIsLogged(false);
    checkIsLogged();
  };

  const checkIsLogged = async () => {
    const email = getCookie("email");
    const access_token = getCookie("access_token");
    if (email != null) {
      setIsLogged(true);
      const result = await fetchUserGetCurrent(access_token);
      let resultjson = await result.json();
      setUser(resultjson);
    }
  };

  const changeIsOpenForm = () => {
    setIsOpenForm(!isOpenForm);
  };

  const SignIn = () => {
    const [isShowErrorBlock, setIsShowErrorBlock] = useState(false);
    const [errorText, setErrorText] = useState("");

    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [userName, setUserName] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const createAccount = (e) => {
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
        let result = await fetchUserRegestration({
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
              changeIsOpenForm();
            }}
          >
            <i class="fa-solid fa-xmark"></i>
          </div>
          <h1>User registration</h1>
          <div className="user-form-part">
            <div className="user-form-label">First name</div>
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
            <button className="green-button" onClick={(e) => createAccount(e)}>
              Create Account
            </button>
          </div>
        </div>
      </div>
    );
  };

  const LogIn = () => {
    const [isShowErrorBlock, setIsShowErrorBlock] = useState(false);
    const [errorText, setErrorText] = useState("");

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [rememberMe, setRememberMe] = useState(false);

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
        let result = await fetchUserLogin({ email, password, rememberMe });
        if (result.status === 200) {
          let resultJson = await result.json();
          setCookie(
            "access_token",
            resultJson.access_token,
            rememberMe ? 720 : 2
          );
          setCookie("email", resultJson.email, rememberMe ? 720 : 2);
          checkIsLogged();
          changeIsOpenForm();
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
              changeIsOpenForm();
            }}
          >
            <i class="fa-solid fa-xmark"></i>
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
            Remember me
            <input
              type="checkbox"
              value={rememberMe}
              onChange={(e) => {
                setRememberMe(!rememberMe);
              }}
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

  return (
    <nav>
      <ul>
        <li>
          <img src={logo} className="site-logo" />
        </li>
        <li>
          <a className="site-name" href="#">
            Game store
          </a>
        </li>
        <div className="left-nav-items">
          <li>
            <Link to="/" className="nav-item">
              Games
            </Link>
          </li>
          <li>
            <Link to="/Community" className="nav-item">
              Community
            </Link>
          </li>
          <li>
            <Link to="/About" className="nav-item">
              About
            </Link>
          </li>
          <li>
            <Link to="/Support" className="nav-item">
              Support
            </Link>
          </li>
        </div>
        <div className="right-nav-items">
          {!isLogged ? (
            <>
              <li>
                <a
                  className="nav-item"
                  href="#"
                  onClick={() => {
                    changeIsOpenForm();
                    setIsOpenLoginForm(false);
                  }}
                >
                  Sign in
                </a>
              </li>
              <li>
                <a
                  className="nav-item"
                  href="#"
                  onClick={() => {
                    changeIsOpenForm();
                    setIsOpenLoginForm(true);
                  }}
                >
                  Log in
                </a>
              </li>
            </>
          ) : (
            <>
              <li>
                <Link to="/User" className="nav-item">
                  <div class="user-small-image">
                    <GetUserImage avatarImageUrl={user.avatarImageUrl} />
                  </div>
                  {user.firstName + " " + user.lastName}
                </Link>
              </li>
              <li>
                <a className="nav-item" href="#">
                  <i class="fa-solid fa-cart-shopping"></i>
                </a>
              </li>
              <li>
                <a
                  className="nav-item"
                  href="#"
                  onClick={() => {
                    logOut();
                  }}
                >
                  <i class="fa-solid fa-right-from-bracket"></i>
                </a>
              </li>
            </>
          )}
        </div>
      </ul>

      {isOpenForm ? (
        <div className="background-gray">
          {isOpenLoginForm ? <LogIn /> : <SignIn />}
        </div>
      ) : null}
    </nav>
  );
};

export default Header;
