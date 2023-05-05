import GetCartItems from "../../JsFunctions/CartFunctions/GetCartItems";

const fetchCreateComment = async (
  firstName,
  lastName,
  email,
  phone,
  paymentType,
  comment
) => {
  var myHeaders = new Headers();
  myHeaders.append("Content-Type", "application/json");

  var cardsList = GetCartItems();
  var raw = JSON.stringify({
    firstName: firstName,
    lastName: lastName,
    email: email,
    phone: phone,
    paymentType: paymentType,
    comment: comment,
    cartModelsIds: cardsList,
  });
  console.log(raw);
  var requestOptions = {
    method: "POST",
    headers: myHeaders,
    body: raw,
    redirect: "follow",
  };

  let result = await fetch("https://localhost:7025/api/Order", requestOptions);
  return result;
};

export default fetchCreateComment;
