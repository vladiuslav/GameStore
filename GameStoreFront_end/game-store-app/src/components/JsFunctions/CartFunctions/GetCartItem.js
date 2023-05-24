function GetCartItem(gameId) {
  const cartItems = JSON.parse(sessionStorage.getItem("cartItems")) || [];

  const itemIndex = cartItems.findIndex((item) => item.gameId === gameId);

  if (itemIndex !== -1) {
    return cartItems[itemIndex];
  }
}
export default GetCartItem;
