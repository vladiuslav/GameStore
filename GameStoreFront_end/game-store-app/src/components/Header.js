import React from 'react'
import { Link } from 'react-router-dom'
import logo from '../Logo.jpg';
const Header = () => {
  return (
    <nav>
      <ul>
        <li><img src={logo} className="site-logo" /></li>
        <li><a href='#'>Game store</a></li>
        <li><Link to='/'>Games</Link></li>
        <li><Link to='/Community'>Community</Link></li>
        <li><Link to='/About'>About</Link></li>
        <li><Link to='/Support'>Support</Link></li>
        <li><Link to='/SignIn'>Sign in</Link></li>
      </ul>
    </nav>
  )
}

export default Header