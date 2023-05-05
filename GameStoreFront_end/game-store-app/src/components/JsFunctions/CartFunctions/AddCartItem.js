function AddCartItem(gameId, quantity) {
  const cartItems = JSON.parse(sessionStorage.getItem("cartItems")) || [];

  const itemIndex = cartItems.findIndex((item) => item.gameId === gameId);

  if (itemIndex !== -1) {
    cartItems[itemIndex].quantity++;
  } else {
    cartItems.push({ gameId, quantity });
  }

  sessionStorage.setItem("cartItems", JSON.stringify(cartItems));
}

export default AddCartItem;
