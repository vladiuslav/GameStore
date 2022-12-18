

const GameImageSmall = (props) => {

  const ganreStyle = {
    backgroundImage: 'url(https://localhost:7025/img/' + props.GameImageUrl + ')'
  };

  return (
    <div className='game-image-small' style={ganreStyle}></div>
  )
}

export default GameImageSmall