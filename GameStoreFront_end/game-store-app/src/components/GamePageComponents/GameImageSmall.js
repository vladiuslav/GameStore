

const GameImageSmall = (props) => {

  let imageUrl;
    if(props.GameImageUrl === null){
        imageUrl="nonegame.jpg";
    }else{
        imageUrl = props.GameImageUrl;
    }
  const ganreStyle = {
    backgroundImage: 'url(https://localhost:7025/img/' + imageUrl + ')'
  };

  return (
    <div className='game-image-small' style={ganreStyle}></div>
  )
}

export default GameImageSmall