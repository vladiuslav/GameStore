import React from "react";
import noneuser from "../../Images/noneuser.png";
const GetUserImage = (props) => {
  let imageUrl;
  if (props.avatarImageUrl === null) {
    imageUrl = noneuser;
  } else {
    imageUrl = "https://localhost:7025/img/" + props.avatarImageUrl;
  }

  return <img alt="User" className={props.className} src={imageUrl} />;
};
export default GetUserImage;
