import React from "react";
import logo from "../Images/Logo.jpg";
const Footer = () => {
  return (
    <footer>
      <div className="footer-img-left">
        <img src={logo} className="site-logo" />
      </div>
      <div className="footer-text-left">
        <h1>Game store</h1>
      </div>
      <div className="footer-text-right">Copyrights - 2022</div>
    </footer>
  );
};

export default Footer;
