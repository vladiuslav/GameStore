import React, { useState } from "react";

const Cart = () => {
  const [cartItems, setCartItems] = useState();

  const renderCartItem = (cartItem) => {
    <div>
      <div>img</div>
      <div>
        <p>Game name</p>
        <p>10.99$</p>
      </div>
      <div>
        <button>-</button>
        <div>5</div>
        <button>+</button>
      </div>
      <div>
        <p>Total:</p>
        <p>10.99$</p>
      </div>
      <div>close</div>
    </div>;
  };

  return (
    <div>
      {cartItems.map((cartItem) => {
        return renderCartItem(cartItem);
      })}
    </div>
  );
};

export default Cart;
