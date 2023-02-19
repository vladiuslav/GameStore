const GetUserImage = (props) => {
  let imageUrl;
  if (props.avatarImageUrl === null) {
    imageUrl = "noneuser.png";
  } else {
    imageUrl = props.avatarImageUrl;
  }

  return (
    <img
      className={props.className}
      src={"https://localhost:7025/img/" + imageUrl}
    />
  );
};
export default GetUserImage;
