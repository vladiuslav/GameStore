import { useState } from "react";
import { useNavigate } from "react-router-dom";
import React from "react";
import RemoveAllCartItems from "./JsFunctions/CartFunctions/RemoveAllCartItems";
import fetchCreateComment from "./Fetches/fetchCart/fetchCreateOrder";

const Order = () => {
  const navigate = useNavigate();
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [email, setEmail] = useState("");
  const [phone, setPhone] = useState("");
  const [paymentType, setPaymentType] = useState("card");
  const [comment, setComment] = useState("");

  const handlePaymentChange = (event) => {
    setPaymentType(event.target.value);
  };

  function sendOrder() {
    if (firstName === "" || lastName === "" || email === "" || phone === "") {
      alert("Please fill all fields");
      return;
    }

    const processFetch = async () => {
      let result = await fetchCreateComment(
        firstName,
        lastName,
        email,
        phone,
        paymentType,
        comment
      );
      if (result.ok) {
        RemoveAllCartItems();
        alert("Order completed");
        navigate("/");
      } else {
        let errorBody = await result.json();
        alert(
          errorBody.title +
            "\n" +
            (errorBody.detail !== undefined ? errorBody.detail : "")
        );
      }
    };

    processFetch();
  }

  //render
  return (
    <div className="order-center">
      <h1>Completing your order</h1>
      <div>
        <div>First name</div>
        <input
          className="order-input"
          type="text"
          value={firstName}
          onChange={(e) => setFirstName(e.target.value)}
        />
      </div>
      <div>
        <div>Last name</div>
        <input
          className="order-input"
          type="text"
          value={lastName}
          onChange={(e) => setLastName(e.target.value)}
        />
      </div>
      <div>
        <div>Email</div>
        <input
          className="order-input"
          type="email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
      </div>
      <div>
        <div>Phone</div>
        <input
          className="order-input"
          type="text"
          value={phone}
          onChange={(e) => setPhone(e.target.value)}
        />
      </div>
      <div>
        <div>Payment type</div>
        <select
          className="order-payment-input"
          value={paymentType}
          onChange={handlePaymentChange}
        >
          <option value="card">Card</option>
          <option value="cash">Cash</option>
        </select>
      </div>
      <div>
        <div>Comment (optional)</div>
        <textarea
          className="order-text-input"
          type="text"
          value={comment}
          onChange={(e) => setComment(e.target.value)}
        />
      </div>

      <button
        className="green-button"
        onClick={() => {
          sendOrder();
        }}
      >
        Complete order
      </button>
    </div>
  );
};

export default Order;
