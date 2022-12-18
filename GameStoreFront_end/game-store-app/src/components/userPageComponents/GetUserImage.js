const GetUserImage = (props) => {

    const imageStyle = {
      backgroundImage: 'url(https://localhost:7025/img/' + props.avatarImageUrl + ')'
    };
  
    return (
      <div className='user-image' style={imageStyle}></div>
    )
    
};
  
  export default GetUserImage;