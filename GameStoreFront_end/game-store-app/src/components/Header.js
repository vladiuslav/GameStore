import React from 'react'
import { useState, useEffect } from "react";
import { Link  } from 'react-router-dom'
import logo from '../Logo.jpg';
import fetchUserRegestration from './Fetches/fetchUsers/fetchUserRegestration';
import fetchUserLogin from './Fetches/fetchUsers/fetchUserLogin';
import fetchUserGetCurrent from './Fetches/fetchUsers/fetchUsersGet/fetchUserGetCurrent';
import setCookie from './JsFunctions/setCookie';
import getCookie from './JsFunctions/getCookie';

const Header = () => {
  const [isLogged, setIsLogged] = useState(false);
  const [userName, setUserName] = useState(false);
  const [isOpenForm,setIsOpenForm] = useState(false);
  const [isOpenLoginForm,setIsOpenLoginForm] = useState(false);
  useEffect(() => {
    checkIsLogged();

  }, [])

  const logOut = ()=>{
    setCookie('access_token',' ',0);
    setCookie('email',' ',0);
    setIsLogged(false);
    checkIsLogged();
  }

  const checkIsLogged = async () =>{
    
    const email = getCookie("email");
    const access_token = getCookie("access_token");
    if(email!=null){
      setIsLogged(true);
      const data = await fetchUserGetCurrent(access_token);
      setUserName(data.firstName+" "+data.lastName);
    }
  }

  const changeIsOpenForm = () => {
    setIsOpenForm(!isOpenForm);
  };

  const SignIn = () => {

    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [userName, setUserName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const createAccount = (e) => {
      e.preventDefault()
  
      //add here new date check
      // if (!text) {
      //     alert('Please add a task')
      //     return
      // }
  
      let data = fetchUserRegestration({ firstName, lastName, userName, email, password });
      alert("account created");
      changeIsOpenForm();
    }

    return (
        <div className='central-form'>
          <div onClick={() => {changeIsOpenForm()}}>close</div>

          <div className="game-add-form">
              <div>
                  <p>First name</p>
                  <input
                      type='text'
                      placeholder='First name'
                      value={firstName}
                      onChange={(e) => setFirstName(e.target.value)}
                  />
              </div>
              <div>
                  <p>Last name</p>
                  <input
                      type='text'
                      placeholder='Last name'
                      value={lastName}
                      onChange={(e) => setLastName(e.target.value)}
                  />
              </div>
              <div>
                  <p>User name</p>
                  <input
                      type='text'
                      placeholder='User name'
                      value={userName}
                      onChange={(e) => setUserName(e.target.value)}
                  />
              </div>
              <div>
                  <p>Email</p>
                  <input
                      type='text'
                      placeholder='Email'
                      value={email}
                      onChange={(e) => setEmail(e.target.value)}
                  />
              </div>
              <div>
                  <p>Password</p>
                  <input
                      type='text'
                      placeholder='Password'
                      value={password}
                      onChange={(e) => setPassword(e.target.value)}
                  />
              </div>
              <button onClick={(e) => (createAccount(e))}>Create Account</button>
          </div>
        </div>
    )
  }
  
  const LogIn = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [rememberMe,setRememberMe] = useState(false);

    const loginAccount = async (e) => {
      e.preventDefault()
  
      //add here new date check
      // if (!text) {
      //     alert('Please add a task')
      //     return
      // }
  
      let data = await fetchUserLogin({email, password,rememberMe});
      setCookie('access_token',data.access_token,(rememberMe)?720:2);
      setCookie('email',data.email,(rememberMe)?720:2);
      checkIsLogged();
      changeIsOpenForm();
    }

    return (
      <div className='central-form'>
        <div onClick={() => {changeIsOpenForm()}}>close</div>

        <div className="game-add-form">
          <div>
              <p>Email</p>
              <input
                  type='text'
                  placeholder='Email'
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
              />
          </div>
          <div>
              <p>Password</p>
              <input
                  type='text'
                  placeholder='Password'
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
              />
          </div>
          <div>
              <p>Remember me</p>
              <input
                  type='checkbox'
                  value={rememberMe}
                  onChange={(e) => {setRememberMe(!rememberMe)}}
              />
          </div>
          <button onClick={(e) => (loginAccount(e))}>Login Account</button>
        </div>
      </div>
  )

  }

  return (
    <nav>
      <ul>
        <li><img src={logo} className="site-logo" /></li>
        <li><a className='nav-item' href='#'>Game store</a></li>
        <li><Link to='/' className='nav-item'>Games</Link></li>
        <li><Link to='/Community' className='nav-item'>Community</Link></li>
        <li><Link to='/About' className='nav-item'>About</Link></li>
        <li><Link to='/Support' className='nav-item'>Support</Link></li>
        {
          (!isLogged)
          ?(
            <>
              <li><a className='nav-item' href='#' onClick={() => {
                changeIsOpenForm();
                setIsOpenLoginForm(false);
                
                }}>Sign in</a></li>
              <li><a className='nav-item' href='#' onClick={() => {
                changeIsOpenForm();
                setIsOpenLoginForm(true);
                }}>Log in</a></li>
            </>
          )
          :(
            <>
              <li><Link to='/User' className='nav-item'>{userName}</Link></li>
              <li><a className='nav-item' href='#' onClick={()=>{logOut()}}>Log out</a></li>
            </>
          )
        }
      </ul>

      {(isOpenForm)?(<div className='background-gray'>{(isOpenLoginForm)?<LogIn/>:<SignIn/>}</div>):null}
    </nav>
  )
}

export default Header