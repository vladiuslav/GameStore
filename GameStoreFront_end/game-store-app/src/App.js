import "./App.css";
import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Header from "./components/Header";
import About from "./components/About";
import Games from "./components/Games";
import Game from "./components/Game";
import AddGame from "./components/AddGame";
import ChangeGame from "./components/ChangeGame";
import User from "./components/User";
import ChangeUser from "./components/ChangeUser";
import Community from "./components/Community";
import Support from "./components/Support";
import Footer from "./components/Footer";

const App = () => {
  return (
    <Router>
      <Header />
      <article className="MainPage">
        <Routes>
          <Route path="/" element={<Games />} />
          <Route path="/Community" element={<Community />} />
          <Route path="/Support" element={<Support />} />
          <Route path="/About" element={<About />} />
          <Route path="/Game/:GameId" element={<Game />} />
          <Route path="/ChangeGame/:GameId" element={<ChangeGame />} />
          <Route path="/AddGame" element={<AddGame />} />
          <Route path="/User" element={<User />} />
          <Route path="/ChangeUser" element={<ChangeUser />} />
        </Routes>
      </article>
      <Footer />
    </Router>
  );
};

export default App;
