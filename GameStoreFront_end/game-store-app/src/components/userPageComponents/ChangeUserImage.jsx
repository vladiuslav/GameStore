import { useState } from "react";
import { useNavigate } from "react-router-dom";
import React from "react";
import fetchChangeUserImage from "../Fetches/fetchUsers/fetchChangeUserImage";
import getCookie from "../CokieFunctions/getCookie";

const ChangeUserImage = () => {
  const navigate = useNavigate();
  const [image, setImage] = useState(null);

  return (
    <div>
      <label>Image of game</label>
      <input type="file" onChange={(e) => setImage(e.target.files)} />
      <button
        className="green-button"
        onClick={() => {
          const token = getCookie("access_token");
          fetchChangeUserImage(image[0], token);
          navigate("/");
        }}
      >
        Change user image
      </button>
    </div>
  );
};

export default ChangeUserImage;
