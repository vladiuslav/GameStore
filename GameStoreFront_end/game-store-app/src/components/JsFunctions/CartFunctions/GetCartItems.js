function GetCartItems() {
  const cartItems = JSON.parse(sessionStorage.getItem("cartItems")) || [];

  return cartItems;
}
export default GetCartItems;
