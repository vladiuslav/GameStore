import './App.css';
import React from 'react'
import { useState, useEffect } from 'react'
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom'
import Header from './components/Header';
import About from './components/About';
import Games from './components/Games';
import Game from './components/Game';
import Community from './components/Community';
import Support from './components/Support';
import Footer from './components/Footer';
import ErrorPage from './components/ErrorPage';
import SignIn from './components/SignIn';

const App = () => {
      return (
            <Router>
                  <Header />
                  <article className='MainPage'>
                        <Routes>
                              <Route
                                    path='/'
                                    element={<Games />} />
                              <Route
                                    path='/Community'
                                    element={<Community />} />
                              <Route
                                    path='/Support'
                                    element={<Support />} />
                              <Route
                                    path='/About'
                                    element={<About />} />
                              <Route
                                    path='/Game/:GameId'
                                    element={<Game />} />
                              <Route
                                    path='/SignIn'
                                    element={<SignIn />} />
                              <Route
                                    path='*'
                                    element={<ErrorPage />} />
                        </Routes>
                  </article>
                  <Footer />
            </Router>
      );
}

export default App;
