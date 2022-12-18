

const GameImageBig = (props) => {

    const ganreStyle = {
        backgroundImage: 'url(https://localhost:7025/img/' + props.GameImageUrl + ')'
    };

    return (
        <div className='game-image-big' style={ganreStyle}></div>
    )
}

export default GameImageBig