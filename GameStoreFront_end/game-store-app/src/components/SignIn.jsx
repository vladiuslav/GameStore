import { useState } from "react";
import FlashBlock from "./FlashBlock";
import fetchUserRegestration from "./Fetches/fetchUsers/fetchUserRegestration";

const SignIn = (props) => {
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
        navigate("/");
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
            props.setIsOpenForm();
          }}
        >
          <i className="fa-solid fa-xmark"></i>
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

export default SignIn;
