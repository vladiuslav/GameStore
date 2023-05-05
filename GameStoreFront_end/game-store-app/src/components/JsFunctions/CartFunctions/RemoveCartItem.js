function RemoveCartItem(gameId) {
  const cartItems = JSON.parse(sessionStorage.getItem("cartItems")) || [];

  const itemIndex = cartItems.findIndex((item) => item.gameId === gameId);

  if (itemIndex !== -1) {
    cartItems.splice(itemIndex, 1);
    sessionStorage.setItem("cartItems", JSON.stringify(cartItems));
  }
}
export default RemoveCartItem;
