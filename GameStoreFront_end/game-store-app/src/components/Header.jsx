import React from "react";
import { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import logo from "../Images/Logo.jpg";
import fetchUserGetCurrent from "./Fetches/fetchUsers/fetchUsersGet/fetchUserGetCurrent";
import GetUserImage from "./userPageComponents/GetUserImage";
import SignIn from "./SignIn";
import LogIn from "./LogIn";
import CheckIsTokenExpired from "./JsFunctions/CheckIsTokenExpired";

const Header = () => {
  const navigate = useNavigate();
  const [isLogged, setIsLogged] = useState(false);
  const [user, setUser] = useState(false);
  const [isOpenForm, setIsOpenForm] = useState(false);
  const [isOpenLoginForm, setIsOpenLoginForm] = useState(false);
  useEffect(() => {
    checkIsLogged();
  }, []);

  const getName = () => {
    if ((user.firstName + user.lastName).length > 20) {
      return (user.firstName + user.lastName).slice(0, 20);
    } else {
      return user.firstName + " " + user.lastName;
    }
  };

  const logOut = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("refresh_token");
    localStorage.removeItem("expiredTokenTime");
    localStorage.removeItem("email");
    setIsLogged(false);
    checkIsLogged();
    navigate("/");
  };

  const checkIsLogged = async () => {
    CheckIsTokenExpired();
    const token = localStorage.getItem("token");
    if (token !== null) {
      setIsLogged(true);
      const result = await fetchUserGetCurrent(token);
      if (result.ok) {
        let resultjson = await result.json();
        setUser(resultjson);
      }
    }
  };

  const changeIsOpenForm = () => {
    setIsOpenForm(!isOpenForm);
  };

  return (
    <nav>
      <ul>
        <li className="site-logo-margin">
          <Link to="/">
            <img alt="Logo" src={logo} className="site-logo" />
          </Link>
        </li>
        <li>
          <Link to="/" className="site-name">
            Game store
          </Link>
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
                <Link to="/Cart" className="nav-item">
                  <i className="fa-solid fa-cart-shopping"></i>
                </Link>
              </li>
              <li>
                <Link
                  className="nav-item"
                  onClick={() => {
                    setIsOpenForm(!isOpenForm);
                    setIsOpenLoginForm(false);
                  }}
                >
                  Sign in
                </Link>
              </li>
              <li>
                <Link
                  className="nav-item"
                  onClick={() => {
                    setIsOpenForm(!isOpenForm);
                    setIsOpenLoginForm(true);
                  }}
                >
                  Log in
                </Link>
              </li>
            </>
          ) : (
            <>
              <li>
                <Link to="/User" className="nav-item">
                  <div className="user-small-image">
                    {user !== undefined ? (
                      <GetUserImage avatarImageUrl={user.avatarImageUrl} />
                    ) : (
                      <></>
                    )}
                  </div>
                  {getName()}
                </Link>
              </li>
              <li>
                <Link to="/Cart" className="nav-item">
                  <i className="fa-solid fa-cart-shopping"></i>
                </Link>
              </li>
              <li>
                <Link
                  className="nav-item"
                  onClick={() => {
                    logOut();
                  }}
                >
                  <i className="fa-solid fa-right-from-bracket"></i>
                </Link>
              </li>
            </>
          )}
        </div>
      </ul>

      {isOpenForm ? (
        <div className="background-gray">
          {isOpenLoginForm ? (
            <LogIn
              checkIsLogged={checkIsLogged}
              setIsOpenForm={changeIsOpenForm}
            />
          ) : (
            <SignIn setIsOpenForm={changeIsOpenForm} />
          )}
        </div>
      ) : null}
    </nav>
  );
};

export default Header;
