import React from 'react'
import { Link } from 'react-router-dom'

const Header = () => {
  return (
    <nav>
        <ul>
          <li><Link to='/'>Games</Link></li>
          <li><Link to='/Community'>Community</Link></li>
          <li><Link to='/About'>About</Link></li>
          <li><Link to='/Support'>Support</Link></li>
        </ul>
    </nav>
  )
}

export default Header