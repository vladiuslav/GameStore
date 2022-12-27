import React from 'react';
function FlashBlock(props) {
    
    const show = {
        display : 'block'
    };

    const dontShow = {
        display : 'none'
    }
    
    return (
        <div className='background-gray' style={(props.isShow)?show:dontShow}>
            <div className='central-form'>
                <p>{props.massage}</p>
            </div>
        </div>
    )
}

export default FlashBlock;