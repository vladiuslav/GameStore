function ChangeCartItem(gameId, newQuantity) {
  const cartItems = JSON.parse(sessionStorage.getItem("cartItems")) || [];

  const itemIndex = cartItems.findIndex((item) => item.gameId === gameId);

  if (newQuantity <= 0) {
    cartItems.splice(itemIndex, 1);
    sessionStorage.setItem("cartItems", JSON.stringify(cartItems));
  } else {
    cartItems[itemIndex].quantity = newQuantity;
    sessionStorage.setItem("cartItems", JSON.stringify(cartItems));
  }
}
export default ChangeCartItem;
