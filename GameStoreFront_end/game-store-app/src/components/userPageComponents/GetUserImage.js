const GetUserImage = (props) => {

    let imageUrl;
    if(props.avatarImageUrl === null){
        imageUrl="noneuser.png";
    }else{
        imageUrl = props.avatarImageUrl;
    }
    const imageStyle = {
      backgroundImage: 'url(https://localhost:7025/img/' + imageUrl + ')'
    };

    return (
      <div className='user-image' style={imageStyle}></div>
    )
    
};
  
  export default GetUserImage;