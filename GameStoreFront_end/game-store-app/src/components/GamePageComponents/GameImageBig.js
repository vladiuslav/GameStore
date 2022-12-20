

const GameImageBig = (props) => {

    console.log(props.GameImageUrl);
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
        <div className='game-image-big' style={ganreStyle}></div>
    )
}

export default GameImageBig