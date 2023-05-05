import React, { useEffect, useState } from "react";
import fetchGames from "./Fetches/fetchGames/fetchGetGames/fetchGames";
import ChangeCartItem from "./JsFunctions/CartFunctions/ChangeCartItem";
import RemoveCartItem from "./JsFunctions/CartFunctions/RemoveCartItem";
import GetCartItems from "./JsFunctions/CartFunctions/GetCartItems";
import GameImage from "./GamePageComponents/GameImage";

import { useNavigate } from "react-router-dom";

const Cart = () => {
  const navigate = useNavigate();
  const [cartItems, setCartItems] = useState([]);
  const [games, setGames] = useState([]);
  const [totalPrice, setTotalPrice] = useState(0);

  useEffect(() => {
    if (GetCartItems().length === 0) {
      navigate("/");
    }
    let carts = GetCartItems();
    setCartItems(carts);

    const getGames = async () => {
      const result = await fetchGames();
      let gamesJson = await result.json();
      setGames(gamesJson);

      const totalPrice = carts.reduce((acc, cartItem) => {
        let game = gamesJson.find((g) => g.id === cartItem.gameId);
        return (
          acc + cartItem.quantity * parseFloat(game.price.replace(",", "."))
        );
      }, 0);
      setTotalPrice(totalPrice);
    };
    getGames();
  }, []);

  const changeCartItem = (gameId, newQuantity) => {
    ChangeCartItem(gameId, newQuantity);
    if (GetCartItems().length === 0) {
      navigate("/");
    }
    setCartItems(GetCartItems());
    calculateTotalPrice();
  };

  const deleteItem = (gameId) => {
    RemoveCartItem(gameId);
    if (GetCartItems().length === 0) {
      navigate("/");
    }
    setCartItems(GetCartItems());
  };

  const getGameById = (gameId) => {
    return games.find((g) => g.id === gameId);
  };
  const calculateTotalPrice = () => {
    const totalPrice = cartItems.reduce((acc, cartItem) => {
      let game = getGameById(cartItem.gameId);
      return acc + cartItem.quantity * parseFloat(game.price.replace(",", "."));
    }, 0);
    setTotalPrice(totalPrice);
  };

  const renderCartItem = (cartItem) => {
    let game = getGameById(cartItem.gameId);
    return (
      <div className="cartItem">
        <div>
          <GameImage className="cartItem-image" GameImageUrl={game.imageUrl} />
        </div>
        <div>
          <div className="cartItem-name">{game.name}</div>
          <div className="cartItem-price">
            $
            {(
              parseFloat(game.price.replace(",", ".")) * cartItem.quantity
            ).toFixed(2)}
          </div>
        </div>
        <div className="cartItem-qantity">
          <button
            className="cartItem-qantity-minus"
            onClick={() => {
              changeCartItem(cartItem.gameId, --cartItem.quantity);
            }}
          >
            -
          </button>
          <div className="cartItem-qantity-number">{cartItem.quantity}</div>
          <button
            className="cartItem-qantity-plus"
            onClick={() => {
              changeCartItem(cartItem.gameId, ++cartItem.quantity);
            }}
          >
            +
          </button>
        </div>
        <div>
          <div className="cartItem-total">Total: </div>
          <h1 className="cartItem-total-price">
            $
            {(
              parseFloat(game.price.replace(",", ".")) * cartItem.quantity
            ).toFixed(2)}
          </h1>
        </div>
        <h1
          className="cartItem-remove"
          onClick={() => {
            deleteItem(cartItem.gameId);
          }}
        >
          <i className="fa-solid fa-xmark"></i>
        </h1>
      </div>
    );
  };

  return (
    <div>
      <div className="cart-top">
        <h1 className="cart-top-left-h1">Your cart</h1>
        <div className="cart-top-right">
          <h1 className="cart-top-left-price">
            Total: {totalPrice.toFixed(2)}$
          </h1>
          <div
            className="cart-top-right-button"
            onClick={() => {
              navigate("/Order");
            }}
          >
            Proceed
          </div>
        </div>
      </div>
      {cartItems.length !== 0 && games.length !== 0 ? (
        cartItems.map((cartItem) => {
          return renderCartItem(cartItem);
        })
      ) : (
        <></>
      )}
    </div>
  );
};

export default Cart;
